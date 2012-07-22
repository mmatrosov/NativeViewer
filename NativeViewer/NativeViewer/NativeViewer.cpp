// This is the main DLL file.

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

//////////////////////////////////////////////////////////////////////////
///
void CheckPrerequisites(DEBUGHELPER *pHelper)
{
  if (pHelper->dwVersion < 0x20000)
  {
    throw std::runtime_error("too old version of Visual Studio");
  }
}

//////////////////////////////////////////////////////////////////////////
///
void ReadDebuggeeMemoryChecked(
  DEBUGHELPER *pHelper, DWORDLONG qwAddr, DWORD nWant, void* pWhere)
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
void ReadHeader(DEBUGHELPER *pHelper, CvMatHeader* pHeader)
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
    throw std::runtime_error("illegal number of dimensions");
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
void FormatResult(const CvMatHeader& header, int base, char *pResult, size_t max)
{
  if (header.dims == 0)
  {
    strcpy_s(pResult, max, "empty");
    return;
  }

  std::stringstream ss;

  const int depth = CV_MAT_DEPTH(header.flags);
  const int cn = CV_MAT_CN(header.flags);

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
    throw std::runtime_error("unknown cv::Mat depth");
  }

  ss << "C" << cn;

  ss << " ";
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
void ReadImageAligned(DEBUGHELPER *pHelper, 
  const CvMatHeader& header, std::vector<unsigned char>& dst, int& dst_step)
{
  const int height = header.rows;
  const int src_step = static_cast<int>(header.step[0]);

  // Seems like .NET Bitmap requires step to be multiple of 4, so round it up 
  dst_step = (src_step + 3) & ~3;

  dst.resize(static_cast<size_t>(height * dst_step));

  DWORDLONG pSrc = header.pdata;
  unsigned char* pDst = &dst[0];

  for (int i = 0; i < height; ++i)
  {
    ReadDebuggeeMemoryChecked(pHelper, pSrc, src_step, pDst);
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
void ShowThumbnail(DEBUGHELPER *pHelper, const CvMatHeader& header)
{
  using namespace System;
  using namespace System::Reflection;
  using namespace System::Drawing;
  using namespace System::IO;

  if (header.dims == 0)
  {
    // No thumbnail for empty image
    return;
  }

  SHORT state = GetAsyncKeyState(VK_CONTROL);

  // Check whether the CTRL key is pressed (ignoring low-order bit which handles 
  // information on key toggle status
  if (state >> 1)
  {
    try
    {
      // Load GUI assembly and information from it. It cannot be added through project 
      // references since .NET framework will only search application path and GAC for
      // the reference, but it resides near the current dll.
      String^ path = Path::GetDirectoryName(
        Assembly::GetExecutingAssembly()->Location) + Path::DirectorySeparatorChar;
      Assembly^ GUI = Assembly::LoadFrom(path + "NativeViewerGUI.dll");
      Type^ FormMain = GUI->GetType("NativeViewerGUI.FormMain");
      MethodInfo^ ShowDialog = FormMain->GetMethod("ShowDialog", gcnew array<Type^>{});
      
      // Read image contents from debuggee memory
      int step;
      std::vector<unsigned char> img;
      ReadImageAligned(pHelper, header, img, step);

      // Initialize .NET image wrapper
      Bitmap^ bmp = gcnew Bitmap(header.cols, header.rows, static_cast<int>(step), 
        Imaging::PixelFormat::Format24bppRgb, IntPtr(&img[0]));

      // Show GUI
      array<Object^>^ args = gcnew array<Object^>{ bmp };
      Object^ form = Activator::CreateInstance(FormMain, args);
      ShowDialog->Invoke(form, nullptr);
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
HRESULT WINAPI CvMatViewer(DWORD dwAddress, DEBUGHELPER *pHelper, 
  int nBase, BOOL bUniStrings, char *pResult, size_t max, DWORD reserved)
{
  try
  {
    CheckPrerequisites(pHelper);

    CvMatHeader header;

    ReadHeader(pHelper, &header);

    FormatResult(header, nBase, pResult, max);

    ShowThumbnail(pHelper, header);
  }
  catch (std::exception& e)
  {
    sprintf_s(pResult, max, "NativeViewer error: %s.", e.what());
  }

  return S_OK;
}