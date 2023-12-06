using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using attendanceApiIntergration.RequestModel;
using System.Data;

namespace attendanceApiIntergration
{
    public class SQLConnector
    {
        private string _connectionString;

        public SQLConnector(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void UpdateData(List<RequestModels> requests)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "update ATDEventTrn_Log set IsPushed=1 where [IndexNo]<=" + requests[requests.Count - 1].IndexNo;
                connection.Open();
                SqlCommand cmd = new SqlCommand(query,connection);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {

                }
            }
        }


        public List<RequestModels> GetRequestModelsData()
        {
            List<RequestModels> requestModels = new List<RequestModels>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand("GETPUNCHDATA", connection);
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RequestModels requestModel = new RequestModels();
                        requestModel.IndexNo = Convert.ToInt32(reader["IndexNo"]);
                        requestModel.UserId = reader["UserId"].ToString();
                        requestModel.DID = Convert.ToInt32(reader["DID"]);
                        requestModel.DeviceId = string.Empty;
                        requestModel.IsPublished = Convert.ToBoolean(reader["IsPushed"]);
                        requestModels.Add(requestModel);
                    }
                }
            }
            return requestModels;
        }
        
    }
}
