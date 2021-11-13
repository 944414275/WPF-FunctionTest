#include "quickopencv.h"

void QuickDemo::peopledetect() {
	VideoCapture capture;
	capture.open("G:\\ԭ��������E��\\�½��ļ���\\video\\2021-09-11 16-56-41.mkv");
	namedWindow("fengbi", WINDOW_AUTOSIZE); 
	//1������Hog����
	cv::HOGDescriptor hog;//����Ĭ�ϲ���
	// 2. ����SVM������  
	hog.setSVMDetector(cv::HOGDescriptor::getDefaultPeopleDetector());   // �����Ѿ�ѵ���õ����˼������� 
	// 3. �ڲ���ͼ���ϼ����������  
	std::vector<cv::Rect> regions;
	
	while (1)
	{
		Mat frame;
		capture >> frame;
		if (frame.empty()) break;
		hog.detectMultiScale(frame, regions, 0, cv::Size(8, 8), cv::Size(32, 32), 1.05, 1);
		// ��ʾ  
		for (size_t i = 0; i < regions.size(); i++)
		{
			cv::rectangle(frame, regions[i], cv::Scalar(0, 0, 255), 2);
		}
		imshow("fengbi", frame);
		if (cv::waitKey(25) >= 0)break;
	}
	   
	capture.release();
}