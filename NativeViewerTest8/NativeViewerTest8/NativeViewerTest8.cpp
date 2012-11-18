#include "stdafx.h"

int _tmain(int argc, _TCHAR* argv[])
{
  try
  {
    IplImage* img_c = cvLoadImage("../../data/art.jpg");
    cv::Mat img(img_c);

    IplImage* gray_c = cvLoadImage("../../data/parrot.jpg");
    cv::Mat gray(gray_c);

    uchar rgb_data[] = { 
      255, 0, 0, 
      0, 255, 0, 
      0, 0, 255 
    };
    cv::Mat rgb(1, 3, CV_8UC3, rgb_data);

    int ndims = CV_MAX_DIM;
    std::vector<int> sizes(ndims, 1);
    cv::Mat many_dims(ndims, &sizes[0], CV_8UC3);

    cv::Scalar_<uchar> p = rgb.at<cv::Scalar_<uchar>>(0, 0);
  }
  catch (cv::Exception& e)
  {
    std::cout << e.what();
  }

  return 0;
}