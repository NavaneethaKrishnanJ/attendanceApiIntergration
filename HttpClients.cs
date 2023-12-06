using attendanceApiIntergration.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace attendanceApiIntergration
{
    public class HttpClients
    {
        HttpRequestMessage request;

        string url = "https://api.cosecpro.com/api/v1/attendance/punch";
        public HttpClients()
        {
        }

        public void SendData(List<RequestModels> requestModels)
        {
            Parallel.ForEach(requestModels, async requestModel =>
            {
                _= PushPunchData(requestModel);
            });
        }

        public async  Task PushPunchData(RequestModels requestModels)
        {
          
            var client = new HttpClient();
            request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", "Basic cGxhbnRpdDpBcHBsaWNhdGlvbkA5OTg4");
            request.Headers.Add("Content-Type", "application/json");
            var content = "[{ \"id\": \"" + requestModels.DeviceId + "\", \"v\":"+ requestModels.UserId+"}]";
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
