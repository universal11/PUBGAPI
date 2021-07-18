using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PUBGAPI
{
    public class Client
    {
        public string apiKey{get;set;}
        public bool enableDebugMode {get;set;} = false;
        private int requestDelayInSeconds {get;set;} = 1;
        private int requestLimit {get;set;} = 30;
        private int requestCounter {get;set;} = 0;
        private HttpClient httpClient = new HttpClient();

    
        public Client(string apiKey){
            this.Init(apiKey);
        }

        public void Init(string apiKey){
            this.apiKey = apiKey;
        }

        /*
        public void Authenticate(){
            this.accessToken = this.GetAccessToken();
        }
        public string GetAccessToken(){
            string url = $"oauth/token";
            Dictionary<string, string> values = new Dictionary<string, string>{
                { "grant_type", "client_credentials" }
            };
            FormUrlEncodedContent data = new FormUrlEncodedContent(values);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.clientId}:{this.secretKey}")) );
            if(this.enableDebugMode){
                System.Console.WriteLine($"POST: {url}");
            }
            HttpResponseMessage response = this.httpClient.PostAsync($"{url}", data).Result;
            string accessToken = JObject.Parse(response.Content.ReadAsStringAsync().Result)["access_token"].ToString();
            return accessToken;

        }
        */

        public HttpResponseMessage HttpGetWithAuth(string url){
            if(this.requestCounter == this.requestLimit){
                System.Threading.Thread.Sleep(this.requestDelayInSeconds * 1000);
                this.requestCounter = 0;
            }

            if(this.enableDebugMode){
                System.Console.WriteLine($"GET: {url}");
            }

            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);
            HttpResponseMessage response = this.httpClient.GetAsync(url).Result;
            if(!response.IsSuccessStatusCode){
                System.Console.WriteLine($"Error Code: {response.StatusCode} | {response.Content.ReadAsStringAsync().Result}");
                return null;
            }
            this.requestCounter++;
            return response;
        }

        public void getPlayerByAccountId(string accountId){

        }

        public void getPlayers(){
            string url = $"https://api.pubg.com/shards/steam";
        }
        
        
        

    }
}
