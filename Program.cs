using attendanceApiIntergration.RequestModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceApiIntergration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connection = ConfigurationSettings.AppSettings["connectionString"].ToString();
            SQLConnector sqlConnector =  new SQLConnector(connection);
            List<RequestModels> requestModels = sqlConnector.GetRequestModelsData();
            List<RequestModels> requestModels1 = MappingRequest(requestModels);

            HttpClients httpClients = new HttpClients();
            httpClients.SendData(requestModels1);
            sqlConnector.UpdateData(requestModels1);
        }

        private static List<RequestModels> MappingRequest(List<RequestModels> requestsdata)
        {
            var deviceList = ConfigurationSettings.AppSettings["DeviceList"].Split(',').ToList();
            Dictionary<int,string> deviceDetails = new Dictionary<int, string>();
            Parallel.ForEach(deviceList, device =>
            {
                var deviceDetailsList = device.Split(':').ToList();
                deviceDetails.Add(Convert.ToInt32(deviceDetailsList[0]), deviceDetailsList[1]);
            });
            Parallel.ForEach(requestsdata,data =>
            {
                data.DeviceId = deviceDetails[data.DID];
            });
            return requestsdata;
        }
        
    }
}
