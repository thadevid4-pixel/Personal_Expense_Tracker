using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Personal_Expense_Tracker
{
    internal class ExpensesData
    {
        string StringConnection = Db.ConnectionString;
        public int ID { set; get; }
        public string Category { set; get; }
        public string Item { set; get; }
        public string Cost { set; get; }
        public string Description { set; get; }
        public string DateExpense { set; get; }

        public List<ExpensesData> expenseListData()
        {
            List<ExpensesData> listData = new List<ExpensesData>();
            using (SqlConnection connnect = new SqlConnection(StringConnection))
            {
                connnect.Open();

                string selectData = "SELECT * FROM expenses";
                using (SqlCommand cmd = new SqlCommand(selectData, connnect))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ExpensesData eData = new ExpensesData();
                        eData.ID = (int)reader["id"];
                        eData.Category = reader["category"].ToString();
                        eData.Item = reader["item"].ToString();
                        eData.Cost = reader["cost"].ToString();
                        eData.Description = reader["description"].ToString();
                        eData.DateExpense = reader["date_expense"] == DBNull.Value
                            ? "" : ((DateTime)reader["date_expense"]).ToString("MM-dd-yyyy");

                        listData.Add(eData);
                    }
                }
            }
            return listData;
        }
    }
}
