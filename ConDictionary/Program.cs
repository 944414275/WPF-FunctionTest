using System;
using System.Collections.Generic;
using System.Linq;

namespace ConDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            //var meta = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            //meta["x-amz-meta-Content-Type"] = contentType;
            string contentType = "application/x-jpg";
            List<string> supportedHeaders = new List<string> { "cache-control", "content-encoding", "content-type", "x-amz-acl", "content-disposition" };             
            
            Dictionary<string, string> metaData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            metaData["x-amz-meta-Content-Type"] = "application/x-jpg";

            var meta = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            //如果metaData不为空
            if (metaData != null)
            {
                foreach (KeyValuePair<string, string> p in metaData)
                {
                    var key = p.Key;
                    if (!supportedHeaders.Contains(p.Key, StringComparer.OrdinalIgnoreCase) && !p.Key.StartsWith("x-amz-meta-", StringComparison.OrdinalIgnoreCase))
                    {
                        key = "x-amz-meta-" + key.ToLowerInvariant();
                    }
                    meta[key] = p.Value;

                }
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = "application/octet-stream";
            }
            if (!meta.ContainsKey("Content-Type"))
            {
                meta["Content-Type"] = contentType;
            }
        }
    }
}
