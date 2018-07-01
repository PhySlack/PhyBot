using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Http
{
    public class HTTPManager
    {
        public HttpCustomResponse GetRequest(string url, string contentType)
        {
            HttpCustomResponse httpCustomResponse = null;

            try
            {
                string html = string.Empty;

                // create URL
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                // GZIP
                request.AutomaticDecompression = DecompressionMethods.GZip;

                // ContentType
                request.ContentType = contentType;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                }

                //Console.WriteLine(html);
                httpCustomResponse = new HttpCustomResponse(html, Models.SuccessResponse.Successful);
            }
            catch (Exception exception)
            {
                httpCustomResponse = new HttpCustomResponse(null, Models.SuccessResponse.Fail);
                Console.WriteLine(exception);
            }

            return httpCustomResponse;
        }

        public HttpCustomResponse GetRequest(string url, string contentType, Dictionary<string, string> parameters)
        {
            HttpCustomResponse httpCustomResponse = null;

            try
            {
                string html = string.Empty;

                string newUrl = url;

                bool first = true;
                foreach (var parameter in parameters)
                {
                    if (first)
                    {
                        newUrl += string.Format("?{0}={1}", parameter.Key, parameter.Value);
                    }
                    else
                    {
                        newUrl += string.Format("&{0}={1}", parameter.Key, parameter.Value);
                    }
                    first = false;
                }

                // create URL
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newUrl);

                // GZIP
                request.AutomaticDecompression = DecompressionMethods.GZip;

                // ContentType
                request.ContentType = contentType;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                }

                //Console.WriteLine(html);
                httpCustomResponse = new HttpCustomResponse(html, Models.SuccessResponse.Successful);
            }
            catch (Exception exception)
            {
                httpCustomResponse = new HttpCustomResponse(null, Models.SuccessResponse.Fail);
                Console.WriteLine(exception);
            }
            return httpCustomResponse;
        }
    }
}
