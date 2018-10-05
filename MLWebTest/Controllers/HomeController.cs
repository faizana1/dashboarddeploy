using MLWebTest.Models;
using Newtonsoft.Json.Linq;
using ParameterIO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MLWebTest.Controllers

{
    public class HomeController : Controller
    {
        private static String global_result = "";
        private AMLParameterObject paramObj = new AMLParameterObject();



        public ActionResult Index()
        {



            InvokeRequestResponseService_A().Wait();

            ViewBag.Message = global_result;


            return View();
        }

        private static List<OutputObject> ExtractValuesObject(string jsonStr)
        {
            try
            {
                List<OutputObject> listOutput = new List<OutputObject>();
                var objects = JObject.Parse(JObject.Parse(jsonStr)["Results"].ToString());

                foreach (var output in objects)
                {
                    OutputObject tmpOutput = new OutputObject();
                    tmpOutput.Name = output.Key;
                    try
                    {
                        JArray outputArray = JArray.Parse(output.Value["value"]["Values"][0].ToString());
                        foreach (var outputValue in outputArray)
                            tmpOutput.Values.Add(outputValue.ToString());
                    }
                    catch (Exception) { }
                    finally { listOutput.Add(tmpOutput); };
                }
                return listOutput;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        public Dictionary<string, string> FeatureVector { get; set; }
        public Dictionary<string, string> GlobalParameters { get; set; }

        public class ScoreData
        {
            public Dictionary<string, string> FeatureVector { get; set; }
            public Dictionary<string, string> GlobalParameters { get; set; }
        }

        public class ScoreRequest
        {
            public string Id { get; set; }
            public ScoreData Instance { get; set; }
        }

        private async Task InvokeRequestResponseService_A()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });


            var listInputs = new Dictionary<string, StringTable>();




            using (var client = new HttpClient())
            {



                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "userID", "U1057"
                                            },
                                            {
                                                "placeID", "Pop"
                                            },
                                            {
                                                "rating", "0"
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };



                const string apiKey = "DG0reKIiAMfiGvqxNNcn0KXZmKMvYlDqf6RxxrPxtESwtQA2QLM0d8fHay02p4+eE3g6aOPbvbcRMzz3Ci6I0A=="; // Replace this with the API key for the web service

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/subscriptions/d3b7858857b24cb1843553556cf97c98/services/ddc1873b49d548d5bdefe24b06ab3f18/execute?api-version=2.0&format=swagger");

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/subscriptions/d3b7858857b24cb1843553556cf97c98/services/ddc1873b49d548d5bdefe24b06ab3f18/execute?api-version=2.0&format=swagger");


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    string apiResult = await response.Content.ReadAsStringAsync();
                    global_result = apiResult;
                  
                    List<OutputObject> listOutputObject = ExtractValuesObject(apiResult);


                }
                else
                {

                 
                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                }
            }
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }
        public ActionResult Artists()
        {

            InvokeRequestResponseService_A().Wait();

            ViewBag.Message = global_result;

            return View();
        }
    }
}