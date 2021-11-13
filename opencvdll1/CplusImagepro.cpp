#include"CplusImagepro.h"
#include "opencv2/opencv.hpp"    
#include <iostream>
#include "opencv2/core/core.hpp"
#include <opencv2/imgproc/imgproc.hpp>  
#include "opencv2/highgui/highgui.hpp"
#include <opencv.hpp>


using namespace std;
using namespace cv;

void AutoFocusA()
{
	string pattern = "C:/Users/Administrator/Pictures/opencvpicture/*.jpg";
	vector<Mat> images;
	vector<String> pic;  // 必须cv的String
	glob(pattern, pic, false);
	size_t count = pic.size();
	cout << count << endl;  //显示一共有多少张图片
	for (int i = 0; i < count; i++)
	{
		images.push_back(imread(pic[i]));
		Mat imageSource = images[i];
		Mat imageGrey;
		cvtColor(imageSource, imageGrey, COLOR_RGB2GRAY);

		Mat imageSobel;
		Laplacian(imageGrey, imageSobel, CV_16U);  //Laplacian梯度法，数值越大表示图像越清晰

		double meanValue = 0.0;
		meanValue = mean(imageSobel)[0];   //图片清晰度

		//double to string  
		stringstream meanValueStream;
		string meanValueString;            //字符串
		meanValueStream << meanValue;
		meanValueStream >> meanValueString;
		meanValueString = "Articulation(Laplacian Method): " + meanValueString;
		putText(imageSource, meanValueString, Point(20, 20), FONT_HERSHEY_COMPLEX, 0.7, Scalar(255, 0, 0), 2);
		imshow("Articulation", imageSource);
		waitKey(1000);
		cout << meanValue << endl;  //显示图片的清晰度

		//将清晰度数据存为xml文件
		FileStorage mul_wr("D:\\pikaqiu.xml", FileStorage::APPEND);
		mul_wr << "meanValue" << meanValue;
		mul_wr.release();
	}
}

void ThresholdImage()
{
	string pattern = "C:/Users/Administrator/Pictures/opencvpicture/beads.jpg";
	Mat image = imread(pattern);
	Mat grayImage, thresholdImage;
	cvtColor(image, grayImage, COLOR_BGR2GRAY);
	threshold(grayImage, thresholdImage, 100, 150, 1);
	imshow("testimage", thresholdImage);
	waitKey(1000);
}
