using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Personal_Expense_Tracker
{
    internal class IncomeData
    {
        string StringConnection = Db.ConnectionString;
        public int ID { set; get; }
        public string Category { set; get; }
        public string Item { set; get; }
        public string Cost { set; get; }
        public string Description { set; get; }
        public string DateIncome { set; get; }

        public List<IncomeData> incomeListData()
        {
            List<IncomeData> listData = new List<IncomeData>();
            using (SqlConnection connnect = new SqlConnection(StringConnection))
            {
                connnect.Open();

                string selectData = "SELECT * FROM income";
                using (SqlCommand cmd = new SqlCommand(selectData, connnect))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IncomeData iData = new IncomeData();
                        iData.ID = (int)reader["id"];
                        iData.Category = reader["category"].ToString();
                        iData.Item = reader["item"].ToString();
                        iData.Cost = reader["income"].ToString();
                        iData.Description = reader["description"].ToString();
                        iData.DateIncome = reader["date_income"] == DBNull.Value
                        ? "" : ((DateTime)reader["date_income"]).ToString("MM-dd-yyyy");

                        listData.Add(iData);
                    }
                }
            }
            return listData;
        }
    }
}
