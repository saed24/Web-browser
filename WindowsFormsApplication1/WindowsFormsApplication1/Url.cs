using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Url
    {
        public Url()
        {

        }

        public string HttpGet(string URI)
        {
            HttpWebResponse response = null;
            string mystring;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URI);
                request.Method = "GET";

                response = (HttpWebResponse)request.GetResponse();

                StreamReader sr = new StreamReader(response.GetResponseStream());

                // return HTML code
                return sr.ReadToEnd();
            }

            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                int statusCode = (int)response.StatusCode;
                mystring = statusCode.ToString();

                // if webpage is not found
                if (statusCode == 404)
                {
                    mystring = mystring + " Not Found";
                }

                // if webpage is not accessible
                else if (statusCode == 403)
                {
                    mystring = mystring + " Forbidden";
                }

                // if webpage is a bad request
                else if (statusCode == 400)
                {
                    mystring = mystring + " Bad Request";
                }
                return mystring;
            }
        }
    }
}
