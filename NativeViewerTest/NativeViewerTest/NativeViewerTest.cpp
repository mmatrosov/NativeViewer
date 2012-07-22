// NativeViewerTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
  try
  {
    cv::Mat empty;
    cv::Mat img = cv::imread("../../data/mountains.jpg", -1);

    int ndims = CV_MAX_DIM;
    std::vector<int> sizes(ndims, 1);
    cv::Mat many_dims(ndims, &sizes[0], CV_8UC3);
  }
  catch (cv::Exception& e)
  {
    std::cout << e.what();
  }


	return 0;
}