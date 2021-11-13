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
	vector<String> pic;  // ����cv��String
	glob(pattern, pic, false);
	size_t count = pic.size();
	cout << count << endl;  //��ʾһ���ж�����ͼƬ
	for (int i = 0; i < count; i++)
	{
		images.push_back(imread(pic[i]));
		Mat imageSource = images[i];
		Mat imageGrey;
		cvtColor(imageSource, imageGrey, COLOR_RGB2GRAY);

		Mat imageSobel;
		Laplacian(imageGrey, imageSobel, CV_16U);  //Laplacian�ݶȷ�����ֵԽ���ʾͼ��Խ����

		double meanValue = 0.0;
		meanValue = mean(imageSobel)[0];   //ͼƬ������

		//double to string  
		stringstream meanValueStream;
		string meanValueString;            //�ַ���
		meanValueStream << meanValue;
		meanValueStream >> meanValueString;
		meanValueString = "Articulation(Laplacian Method): " + meanValueString;
		putText(imageSource, meanValueString, Point(20, 20), FONT_HERSHEY_COMPLEX, 0.7, Scalar(255, 0, 0), 2);
		imshow("Articulation", imageSource);
		waitKey(1000);
		cout << meanValue << endl;  //��ʾͼƬ��������

		//�����������ݴ�Ϊxml�ļ�
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
