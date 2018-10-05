using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MLWebTest
{
    public class Recommender
    {
        static String global_result="";

     

        //   InvokeRequestResponseService().Wait();
        public Recommender()
        {
           
        }
        public String Invoke()
        {
            InvokeRequestResponseService().Wait();
            return global_result;

        }

        static async Task InvokeRequestResponseService()
        {
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



                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                  

                }
                else
                {
                 



                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                }
            }
        }
    



}




}