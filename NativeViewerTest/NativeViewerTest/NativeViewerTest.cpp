// NativeViewerTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"



int _tmain(int argc, _TCHAR* argv[])
{
  cv::Mat img = cv::imread("../data/mountains.jpg", -1);

  int cn_mask = CV_MAT_CN_MASK;
  int type_mask = CV_MAT_TYPE_MASK;
  int depth_mask = CV_MAT_DEPTH_MASK;

	return 0;
}