#include "quickopencv.h"

void QuickDemo::peopledetect() {
	VideoCapture capture;
	capture.open("G:\\原工作电脑E盘\\新建文件夹\\video\\2021-09-11 16-56-41.mkv");
	namedWindow("fengbi", WINDOW_AUTOSIZE); 
	//1、定义Hog对象
	cv::HOGDescriptor hog;//采用默认参数
	// 2. 设置SVM分类器  
	hog.setSVMDetector(cv::HOGDescriptor::getDefaultPeopleDetector());   // 采用已经训练好的行人检测分类器 
	// 3. 在测试图像上检测行人区域  
	std::vector<cv::Rect> regions;
	
	while (1)
	{
		Mat frame;
		capture >> frame;
		if (frame.empty()) break;
		hog.detectMultiScale(frame, regions, 0, cv::Size(8, 8), cv::Size(32, 32), 1.05, 1);
		// 显示  
		for (size_t i = 0; i < regions.size(); i++)
		{
			cv::rectangle(frame, regions[i], cv::Scalar(0, 0, 255), 2);
		}
		imshow("fengbi", frame);
		if (cv::waitKey(25) >= 0)break;
	}
	   
	capture.release();
}