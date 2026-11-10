using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Personal_Expense_Tracker
{
    internal class CategoryData
    {
        string StringConnection = Db.ConnectionString;
        public int ID { set; get; } //0
        public string Category { set; get; } //1
        public string Type { set; get; } //2
        public string Status { set; get; } //3
        public string Date { set; get; } //4

        public List<CategoryData> categoryListData()
        {
            List<CategoryData> listData = new List<CategoryData>();

            using (SqlConnection connect = new SqlConnection(StringConnection))
            {
                connect.Open();

                string selectData = "SELECT * FROM Categories";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CategoryData cData = new CategoryData();
                        cData.ID = (int)reader["id"];
                        cData.Category = reader["category"].ToString();
                        cData.Type = reader["type"].ToString();
                        cData.Status = reader["status"].ToString();
                        cData.Date = reader["date_insert"] == DBNull.Value
                            ? "" : ((DateTime)reader["date_insert"]).ToString("MM-dd-yyyy");

                        listData.Add(cData);
                    }
                }
            }
            return listData;
        }
    }
}
