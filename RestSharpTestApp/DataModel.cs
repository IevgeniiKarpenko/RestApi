using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using RestSharp;

namespace RestSharpTestApp
{
    public class DataModel
    {
        public DataTable Data { get; set; }

        private RestClient client;
        public string Output { get; set; }
        public string Result { get; set; }

        public DataModel()
        {
            client = new RestClient("http://beacon.nist.gov");
            Result = string.Empty;
            Output = string.Empty;
            Data = new DataTable();
            Data.Columns.Add();
            Data.Columns.Add();
        }

        public void ProccessResponce()
        {
            if (string.IsNullOrEmpty(Result))
                return;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Result);
            for (var index = 0; index < doc.ChildNodes.Count; index++)
            {
                XmlNode node = doc.ChildNodes[index];
                if (node.Name == "OutputValue")
                    Output = node.InnerText;
            }

            FillDataTable();
        }

        private void FillDataTable()
        {
            Dictionary<char, int> outputCount = new Dictionary<char, int>();
            foreach (char ch in Output)
            {
                if (!outputCount.ContainsKey(ch))
                    outputCount[ch] = 0;
                else
                    outputCount[ch]++;
            }

            foreach (var pair in outputCount)
            {
                DataRow row = Data.NewRow();
                row.ItemArray = new[] { pair.Key as object, pair.Value };
                Data.Rows.Add(row);
            }
        }

        public void SendRequest(DateTime time)
        {
            //var request = new RestRequest("est/record/" + time.ToBinary());

            //// execute the request
            //IRestResponse response = client.Execute(request);
            //_result = response.Content; // raw content as string

            //set dummy data
            try
            {
                Result = File.ReadAllText(@"..\..\DummyResponse.xml", Encoding.UTF8); ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
