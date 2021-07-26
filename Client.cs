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
        /*
        public bool enableDebugMode {get;set;} = false;
        private int requestDelayInSeconds {get;set;} = 1;
        private int requestLimit {get;set;} = 30;
        private int requestCounter {get;set;} = 0;
        */
        private HttpClient httpClient = new HttpClient();

    
        public Client(string apiKey){
            this.Init(apiKey);
        }

        public void Init(string apiKey){
            this.apiKey = apiKey;
        }

        public HttpResponseMessage HttpGetWithAuth(string url){
            /*
            if(this.requestCounter == this.requestLimit){
                System.Threading.Thread.Sleep(this.requestDelayInSeconds * 1000);
                this.requestCounter = 0;
            }

            if(this.enableDebugMode){
                System.Console.WriteLine($"GET: {url}");
            }
            */

            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
            HttpResponseMessage response = this.httpClient.GetAsync(url).Result;
            /*
            if(!response.IsSuccessStatusCode){
                System.Console.WriteLine($"Error Code: {response.StatusCode} | {response.Content.ReadAsStringAsync().Result}");
                return null;
            }
            
            this.requestCounter++;
            */
            return response;
        }

        public HttpResponseMessage getPlayerByAccountId(string accountId){
            string url = $"https://api.pubg.com/shards/steam/players/{accountId}";
            return this.HttpGetWithAuth(url);
        }

        public HttpResponseMessage getPlayers(List<string> accountIds, List<string> playerNames){
            string url = $"https://api.pubg.com/shards/steam/players?filter[playerIds]={String.Join(",", accountIds)}&filter[playerNames]={String.Join(",", playerNames)}";
            return this.HttpGetWithAuth(url);
        }
        
        
        

    }
}
