using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LecturaJsonResponse
{
    class Program
    {
        public class Data
        {
            public string status { get; set; }
            public IList<string> data { get; set; }
        }

        public static string JsonConver { get; private set; }

        static void Main(string[] args)
        {
            string url = "http://bibliotecum.herokuapp.com/api/getbook";

            string result = getPost(url);

            JObject jsonIdentado = JObject.Parse(result);

            JsonArrayAttribute dataList = JsonConvert.DeserializeObject<List<Data>>(result).ToArray();
            

            Console.WriteLine(dataList);

            Console.ReadKey();
        }

        public static string getPost (string url)
        {
            Book book = new Book() { isbn = "1234", api_key = "pepe" };
            string result = "";

            WebRequest request = WebRequest.Create(url);
            request.Method = "post";
            request.ContentType = "application/json; charset=UTF-8";

            using(var oSW = new StreamWriter(request.GetRequestStream()))
            {
                //string json = "{\"isbn\" : \"1234\", \"api_key\" : \"pepe\"}";
                string json = JsonConvert.SerializeObject(book, Formatting.Indented);

                oSW.Write(json);
                oSW.Flush();
                oSW.Close();
            }

            WebResponse response = request.GetResponse(); 

            using(var oSR = new StreamReader(response.GetResponseStream()))
            {
                result = oSR.ReadToEnd().Trim();
            }

            return result;
        }

        public class Book
        {
            public string isbn { get; set; }
            public string api_key { get; set; }
        }
    }
}
