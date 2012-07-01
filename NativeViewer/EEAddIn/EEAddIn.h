#pragma once

#include "custview.h"

extern "C" {
  HRESULT WINAPI CvMatViewer(DWORD dwAddress, DEBUGHELPER *pHelper, 
    int nBase, BOOL bUniStrings, char *pResult, size_t max, DWORD reserved);
}