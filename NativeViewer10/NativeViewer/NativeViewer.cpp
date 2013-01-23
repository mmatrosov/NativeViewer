#include "stdafx.h"

#include "custview.h"

//////////////////////////////////////////////////////////////////////////
/// Unmanaged functions
//////////////////////////////////////////////////////////////////////////

#pragma unmanaged

class CvMatHeader
{
public:
  int flags;
  int dims;
  int rows, cols;
  DWORDLONG pdata;
  std::vector<int> size;
  std::vector<__int64> step;
};

enum ImageFormat
{
  ifRGB = 0, 
  ifBGR = 1
};

// The value of the _MSC_VER is equal to 1600 for VS10 and 1700 for VS11
const int VS_MAJOR_VER = _MSC_VER / 100 - 6;

//////////////////////////////////////////////////////////////////////////
///
void CheckPrerequisites(DEBUGHELPER* pHelper)
{
  if (pHelper->dwVersion < 0x20000)
  {
    throw std::runtime_error("too old version of Visual Studio");
  }
}

//////////////////////////////////////////////////////////////////////////
///
void ReadDebuggeeMemoryChecked(
  DEBUGHELPER* pHelper, DWORDLONG qwAddr, DWORD nWant, void* pWhere)
{
  DWORD nGot;
  HRESULT result = pHelper->ReadDebuggeeMemoryEx(pHelper, qwAddr, nWant, pWhere, &nGot);

  if (result != S_OK || nGot != nWant)
  {
    throw std::runtime_error("cannot read debuggee memory");
  }
}

//////////////////////////////////////////////////////////////////////////
///
void ReadHeader(DEBUGHELPER* pHelper, CvMatHeader* pHeader)
{
  // Size of pointer in the debuggee context, may be 32 bits (4 bytes) or 
  // 64 bits (8 bytes) depending on the platform (processor type)
  const int ptr_size = pHelper->GetProcessorType(pHelper) == 0 ? 4 : 8;

  DWORDLONG addr = pHelper->GetRealAddress(pHelper);

  // Read the bunch of fields that are not pointers: flags, dims, cols and rows
  ReadDebuggeeMemoryChecked(pHelper, addr, 4 * sizeof(int), pHeader);
  addr += 4 * sizeof(int);

  // Read data field and skip through refcount, datastart, dataend, datalimit and allocator fields
  pHeader->pdata = 0;
  ReadDebuggeeMemoryChecked(pHelper, addr, ptr_size, &pHeader->pdata);
  addr += 6 * ptr_size;

  // Check number of dimensions
  const int dims = pHeader->dims;

  // Minimum number of dimensions is 2, or 0 for empty cv::Mat
  if (dims < 0 || dims > CV_MAX_DIM || dims == 1)
  {
    throw std::logic_error("illegal number of dimensions");
  }

  if (dims == 0)
  {
    // Empty cv::Mat, no need to read size and step
    return;
  }

  // Read MSize field consisting of the only int* member
  DWORDLONG psize = 0;
  ReadDebuggeeMemoryChecked(pHelper, addr, ptr_size, &psize);
  addr += ptr_size;

  // Read MStep field containing the size_t* member at the beginning
  DWORDLONG pstep = 0;
  ReadDebuggeeMemoryChecked(pHelper, addr, ptr_size, &pstep);  

  // Read size values directly
  pHeader->size.resize(dims);
  ReadDebuggeeMemoryChecked(pHelper, psize, dims * sizeof(int), &pHeader->size[0]);

  // Read step values depending on size of the size_t type
  pHeader->step.resize(dims);
  if (ptr_size == 8)
  {
    // In 64-bit case values might be directly read into __int64 elements vector
    ReadDebuggeeMemoryChecked(pHelper, pstep, dims * ptr_size, &pHeader->step[0]);
  }
  else
  {
    // In 32-bit case need to use intermediate __int32 buffer
    std::vector<__int32> buf(dims);
    ReadDebuggeeMemoryChecked(pHelper, pstep, dims * ptr_size, &buf[0]);
    std::copy(buf.begin(), buf.end(), pHeader->step.begin());
  }
}

//////////////////////////////////////////////////////////////////////////
///
std::string FlagsToString(int flags)
{
  std::stringstream ss;

  const int depth = CV_MAT_DEPTH(flags);
  const int cn = CV_MAT_CN(flags);

  switch (depth)
  {
  case CV_8U:  ss << "8U";   break;
  case CV_8S:  ss << "8S";   break;
  case CV_16U: ss << "16U";  break;
  case CV_16S: ss << "16S";  break;
  case CV_32S: ss << "32S";  break;
  case CV_32F: ss << "32F";  break;
  case CV_64F: ss << "64F";  break;
  case CV_USRTYPE1: ss << "USR";  break;
  default:
    throw std::logic_error("unknown cv::Mat depth");
  }

  ss << "C" << cn;

  return ss.str();
}

//////////////////////////////////////////////////////////////////////////
///
void FormatResult(const CvMatHeader& header, int base, char* pResult, size_t max)
{
  if (header.dims == 0)
  {
    strcpy_s(pResult, max, "empty");
    return;
  }

  std::stringstream ss;

  ss << FlagsToString(header.flags) << " ";

  if (base == 16) ss << "(0x)";
  ss << "[";
  for (int i = 0; i < header.dims; ++i)
  {
    if (i > 0) ss << "x";
    ss << std::setbase(base) << header.size[i];
  }
  ss << "]";

  strcpy_s(pResult, max, ss.str().c_str());
}

//////////////////////////////////////////////////////////////////////////
///
template <class T>
void ReadMemoryAndConvertTyped(
  DEBUGHELPER* pHelper, DWORDLONG qwAddr, int count, unsigned char* pWhere)
{
  std::vector<T> buf(count);

  ReadDebuggeeMemoryChecked(pHelper, qwAddr, count * sizeof(T), &buf[0]);

  double scale;

  if (std::numeric_limits<T>::is_integer)
  {
    // Clip integral types to zero, map the whole range
    scale = 255.0 / std::numeric_limits<T>::max();
  }
  else
  {
    // Conversion from the [0; 1] range for floating point types
    scale = 255.0;
  }

  for (int i = 0; i < count; ++i)
  {
    pWhere[i] = static_cast<unsigned char>(std::min(std::max(0.0, buf[i] * scale), 255.0));
  }
}

//////////////////////////////////////////////////////////////////////////
///
void ReadMemoryAndConvert(
  DEBUGHELPER* pHelper, DWORDLONG qwAddr, int count, unsigned char* pWhere, int cvDepth)
{
  switch (cvDepth)
  {
  case CV_8U:  ReadMemoryAndConvertTyped< UINT8>(pHelper, qwAddr, count, pWhere); break;
  case CV_8S:  ReadMemoryAndConvertTyped<  INT8>(pHelper, qwAddr, count, pWhere); break;
  case CV_16U: ReadMemoryAndConvertTyped<UINT16>(pHelper, qwAddr, count, pWhere); break;
  case CV_16S: ReadMemoryAndConvertTyped< INT16>(pHelper, qwAddr, count, pWhere); break;
  case CV_32S: ReadMemoryAndConvertTyped< INT32>(pHelper, qwAddr, count, pWhere); break;
  case CV_32F: ReadMemoryAndConvertTyped< float>(pHelper, qwAddr, count, pWhere); break;
  case CV_64F: ReadMemoryAndConvertTyped<double>(pHelper, qwAddr, count, pWhere); break;
  default:
    throw std::logic_error("unknown cv::Mat depth");
  }
}

//////////////////////////////////////////////////////////////////////////
///
void ReadImageAligned(DEBUGHELPER* pHelper, const CvMatHeader& header, 
  ImageFormat format, std::vector<unsigned char>& dst, int& dst_step)
{
  struct Color
  {
    unsigned char r, g, b;
  };

  const int height = header.rows;
  const int width = header.cols;
  const int src_step = static_cast<int>(header.step[0]);
  const int line_size = static_cast<int>(width * header.step[1]);

  const int depth = CV_MAT_DEPTH(header.flags);
  const int cn = CV_MAT_CN(header.flags);

  // Seems like .NET Bitmap requires step to be multiple of 4, so round it up 
  dst_step = (line_size + 3) & ~3;

  dst.resize(static_cast<size_t>(height * dst_step));

  DWORDLONG pSrc = header.pdata;
  unsigned char* pDst = &dst[0];

  for (int i = 0; i < height; ++i)
  {
    ReadMemoryAndConvert(pHelper, pSrc, width * cn, pDst, depth);

    // Internal .NET Bitmap format is BGR, so conversion is only needed for RGB
    if (format == ifRGB && cn == 3)
    {
      // I don't want to include OpenCV at this point, so the raw loop is used 
      // instead of cv::cvtColor()
      Color* pBegin = reinterpret_cast<Color*>(pDst);  
      Color* pEnd = pBegin + header.cols;
      for (Color* pPixel = pBegin; pPixel != pEnd; ++pPixel)
      {
        std::swap(pPixel->r, pPixel->b);        
      }
    }

    pSrc += src_step;
    pDst += dst_step;
  }
}

//////////////////////////////////////////////////////////////////////////
/// Managed functions
//////////////////////////////////////////////////////////////////////////

#pragma managed

//////////////////////////////////////////////////////////////////////////
///
ImageFormat LoadImageFormat(System::Reflection::Assembly^ GUI)
{
  using namespace System;
  using namespace System::Reflection;

  Type^ Settings = GUI->GetType("NativeViewerGUI.Settings");
  MethodInfo^ LoadSettings = Settings->GetMethod("Load", gcnew array<Type^>{});
  Object^ settings = LoadSettings->Invoke(nullptr, gcnew array<Object^>{});

  PropertyInfo^ ImageFormat = Settings->GetProperty("ImageFormat");
  Object^ format = ImageFormat->GetValue(settings, nullptr);
  
  // It's not really safe to cast enum directly to int, since the order of the values 
  // can be accidentally changed. However, Assembly::GetType method doesn't work for 
  // nested types, and I don't want to make code unnecessary complicated by parsing 
  // types collection returned by Assembly::GetTypes to access nested enum.
  return (int)format == 0 ? ifRGB : ifBGR;
}

// This class provide the DoWork method which shows the main GUI dialog
ref class DlgWork
{
public:
  DlgWork(System::Reflection::Assembly^ gui_assembly, System::Drawing::Bitmap^ image)
    : _gui_assembly(gui_assembly), _image(image), _exception(nullptr)
  {
  }

  // This method is called from a separate thread
  void DoWork()
  {
    try
    {
      DoWorkInternal();
    }
    catch (System::Exception^ e)
    {
      _exception = e;
    }
  }

  // Use this method to check for possible exceptions occurred while executing the DoWork
  // method after the calling thread had finished
  System::Exception^ GetException()
  {
    return _exception;
  }

private:
  void DoWorkInternal()
  {
    using namespace System;
    using namespace System::Reflection;

    Type^ FormMain = _gui_assembly->GetType("NativeViewerGUI.FormMain");
    MethodInfo^ ShowDialog = FormMain->GetMethod("ShowDialog", gcnew array<Type^>{});

    // Create GUI
    array<Object^>^ args = gcnew array<Object^>{ _image };
    Object^ form = Activator::CreateInstance(FormMain, args);

    try
    {
      // Show GUI
      ShowDialog->Invoke(form, nullptr);
    }
    catch (System::Reflection::TargetInvocationException^ e)
    {
      throw e->InnerException;
    }
  }

  System::Reflection::Assembly^ _gui_assembly;
  System::Drawing::Bitmap^ _image;

  System::Exception^ _exception;
};

//////////////////////////////////////////////////////////////////////////
///
void ShowThumbnail(DEBUGHELPER* pHelper, const CvMatHeader& header)
{
  using namespace System;
  using namespace System::Threading;
  using namespace System::IO;
  using namespace System::Reflection;
  using namespace System::Drawing;

  SHORT state = GetAsyncKeyState(VK_CONTROL);

  // Check whether the CTRL key is pressed (ignoring low-order bit which handles 
  // information on key toggle status
  if (state >> 1)
  {
    const int depth = CV_MAT_DEPTH(header.flags);
    const int cn = CV_MAT_CN(header.flags);

    if (header.dims != 2 || cn != 1 && cn != 3)
    {
      throw std::logic_error(
        "thumbnails are supported only for 2D arrays with one or three channels");
    }

    try
    {
      // Load GUI assembly and information from it. It cannot be added through project 
      // references since .NET framework will only search application path and GAC for
      // the reference, but it resides near the current dll.
      String^ path = Path::GetDirectoryName(
        Assembly::GetExecutingAssembly()->Location) + Path::DirectorySeparatorChar;      
      Assembly^ gui_assembly = Assembly::LoadFrom(
        path + "NativeViewerGUI" + Int32(VS_MAJOR_VER).ToString() + ".dll");

      // Retrieve format settings
      ImageFormat format = LoadImageFormat(gui_assembly);

      // Read image contents from debuggee memory. It should be read from the current 
      // thread, because otherwise it's not working under Visual Studio 2012.
      int step;
      std::vector<unsigned char> buffer;
      ReadImageAligned(pHelper, header, format, buffer, step);

      // Initialize .NET image wrapper
      Imaging::PixelFormat pixel_format = CV_MAT_CN(header.flags) == 1 ? 
        Imaging::PixelFormat::Format8bppIndexed : Imaging::PixelFormat::Format24bppRgb;
      Bitmap^ image = gcnew Bitmap(
        header.cols, header.rows, static_cast<int>(step), pixel_format, IntPtr(&buffer[0]));

      // Pass information on underlying image format
      image->Tag = gcnew String(FlagsToString(header.flags).c_str());

      // GUI must be shown in a separate thread with the STA apartment state. Visual 
      // Studio loads the library in a thread with the MTA apartment state, which 
      // conflicts with some GDI+ features, like modal dialogs.
      DlgWork^ dlg_work = gcnew DlgWork(gui_assembly, image);
      Thread^ dlg_thread = gcnew Thread(gcnew ThreadStart(dlg_work, &DlgWork::DoWork));
      dlg_thread->SetApartmentState(ApartmentState::STA);

      // Run the thread and wait for it to terminate after the dialog is closed
      dlg_thread->Start();
      dlg_thread->Join();

      // Check for possible exceptions occurred while executing the thread
      if (dlg_work->GetException() != nullptr)
      {
        throw dlg_work->GetException();
      }
    }
    catch (Exception^ e)
    {
      msclr::interop::marshal_context context;
      throw std::runtime_error(context.marshal_as<std::string>(e->ToString()));
    }
  }
}

//////////////////////////////////////////////////////////////////////////
///
HRESULT WINAPI CvMatViewer(DWORD dwAddress, DEBUGHELPER* pHelper, 
  int nBase, BOOL bUniStrings, char* pResult, size_t max, DWORD reserved)
{
  try
  {
    CheckPrerequisites(pHelper);

    CvMatHeader header;

    ReadHeader(pHelper, &header);

    FormatResult(header, nBase, pResult, max);

    ShowThumbnail(pHelper, header);
  }
  catch (std::logic_error& e)
  {
    // This type of exception indicates an improper use and should be displayed 
    // to the user as an information message
    _snprintf_s(pResult, max, _TRUNCATE, "NativeViewer: %s.", e.what());
  }
  catch (std::exception& e)
  {
    // This type of exception indicates some real error
    _snprintf_s(pResult, max, _TRUNCATE, "NativeViewer error: %s.", e.what());
  }

  return S_OK;
}