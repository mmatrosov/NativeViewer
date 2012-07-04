// This is the main DLL file.

#include "stdafx.h"

#include "NativeViewer.h"

#include "custview.h"

void ShowGUI()
{
  System::Windows::Forms::MessageBox::Show("Hello, Windows Forms");
}

void CallManaged()
{
  ShowGUI();
}

extern "C" 
{
#pragma unmanaged
  HRESULT WINAPI CvMatViewer(DWORD dwAddress, DEBUGHELPER *pHelper, 
    int nBase, BOOL bUniStrings, char *pResult, size_t max, DWORD reserved)
  {
    strcpy_s(pResult, max, "Managed NativeViewer was here :)");

    CallManaged();

    return S_OK;
  }
}