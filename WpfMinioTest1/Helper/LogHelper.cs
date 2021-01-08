using GalaSoft.MvvmLight.Messaging;
using Minio;
using Minio.DataModel.Tracing;
using System.Net;

namespace WpfMinioTest1.Helper
{
    public class LogHelper : IRequestLogger
    {
        public void LogRequest(RequestToLog requestToLog, ResponseToLog responseToLog, double durationMs)
        {
            if (responseToLog.statusCode == HttpStatusCode.OK)
            {
                foreach (var header in requestToLog.parameters)
                {
                    if (!string.Equals(header.name, "partNumber")) continue;
                    if (header.value == null) continue;
                    int.TryParse(header.value.ToString(), out var partNumber);//minio遇到上传文件大于5MB时，会进行分块传输，这里就是当前块的编号（递增）
                    Messenger.Default.Send(partNumber, "process");//发送给主界面计算上传进度
                    break;
                }
            }
        }
    }
}
