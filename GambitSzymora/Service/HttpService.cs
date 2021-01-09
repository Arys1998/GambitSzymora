using GambitSzymora.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GambitSzymora.ViewModels
{
    public class HttpService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<string> GetEndpoitResponse(string url)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                string responseBody = "Bad Request";
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return responseBody;
            }
        }

        public async Task postMovesToDB(MoveModel moveModel)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                var json = JsonConvert.SerializeObject(moveModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://history-service.azurewebsites.net/api/SaveMoveToDB?", data);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nFailed to post move!");
                Console.WriteLine("Message :{0} ", e.Message);       
            }
        }



    }
}
