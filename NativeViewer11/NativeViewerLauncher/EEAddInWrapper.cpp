#include "stdafx.h"

#include "EEAddInWrapper.h"

#include "custview.h"

struct Helper : public DEBUGHELPER
{
  DWORD dwAddress;
};

//////////////////////////////////////////////////////////////////////////
/// Defined in NativeViewer assembly
HRESULT WINAPI CvMatViewer(DWORD dwAddress, DEBUGHELPER *pHelper, 
  int nBase, BOOL bUniStrings, char *pResult, size_t max, DWORD reserved);

//////////////////////////////////////////////////////////////////////////
///
HRESULT WINAPI ReadDebuggeeMemory(
  struct tagDEBUGHELPER *pThis, DWORD dwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot)
{
  if (memcpy_s(pWhere, nWant, reinterpret_cast<void*>(dwAddr), nWant) != 0)
  {
    return S_FALSE;
  }

  *nGot = nWant;

  return S_OK;
}

//////////////////////////////////////////////////////////////////////////
///
DWORDLONG WINAPI GetRealAddress(struct tagDEBUGHELPER *pThis)
{
  return static_cast<Helper*>(pThis)->dwAddress;
}

//////////////////////////////////////////////////////////////////////////
///
HRESULT WINAPI ReadDebuggeeMemoryEx(
  struct tagDEBUGHELPER *pThis, DWORDLONG qwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot)
{
  return ReadDebuggeeMemory(pThis, static_cast<DWORD>(qwAddr), nWant, pWhere, nGot);
}

//////////////////////////////////////////////////////////////////////////
///
int WINAPI GetProcessorType(struct tagDEBUGHELPER *pThis)
{
  return System::Environment::Is64BitProcess ? 2 : 0;
}

//////////////////////////////////////////////////////////////////////////
///
System::String^ CallEEAddIn(const cv::Mat& image)
{
  Helper helper;
  helper.dwVersion = 0x20000;
  helper.dwAddress = reinterpret_cast<DWORD>(&image);
  helper.GetProcessorType = GetProcessorType;
  helper.ReadDebuggeeMemory = ReadDebuggeeMemory;
  helper.GetRealAddress = GetRealAddress;
  helper.ReadDebuggeeMemoryEx = ReadDebuggeeMemoryEx;

  const size_t max = 1024;
  char result[max] = "{???}";

  CvMatViewer(helper.dwAddress, &helper, 10, true, result, max, 0);

  msclr::interop::marshal_context context;

  return context.marshal_as<System::String^>(result);
}
