// NativeViewerTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int _tmain(int argc, _TCHAR* argv[])
{
  try
  {
    cv::Mat img = cv::imread("../../data/opencv-logo-white.png", -1);

    uchar rgb_data[] = { 
      255, 0, 0, 
      0, 255, 0, 
      0, 0, 255 
    };
    cv::Mat rgb(1, 3, CV_8UC3, rgb_data);
    
    int ndims = CV_MAX_DIM;
    std::vector<int> sizes(ndims, 1);
    cv::Mat many_dims(ndims, &sizes[0], CV_8UC3);

    auto p = rgb.at<cv::Scalar_<uchar>>(0, 0);
  }
  catch (cv::Exception& e)
  {
    std::cout << e.what();
  }

	return 0;
}