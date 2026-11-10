using System;

namespace Personal_Expense_Tracker
{
    /// <summary>Remembers who is logged in and what they are allowed to do.</summary>
    internal static class Session
    {
        public static int UserId;
        public static string Username = "";
        public static string Role = "";

        public static bool IsAdmin
        {
            get { return string.Equals(Role, Db.RoleAdmin, StringComparison.OrdinalIgnoreCase); }
        }

        public static void Start(int userId, string username, string role)
        {
            UserId = userId;
            Username = username ?? "";
            Role = string.IsNullOrWhiteSpace(role) ? Db.RoleStaff : role;
        }

        public static void Clear()
        {
            UserId = 0;
            Username = "";
            Role = "";
        }
    }
}
