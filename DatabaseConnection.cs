using Microsoft.Data.SqlClient;

namespace StudentManagementSystem
{
    public class DatabaseConnection
    {
        private static string DATABASE = "student_management_system_db";
        private static string SERVER = "(local)\\sqlexpress";


        protected static SqlConnection SqlConn()
        {
            return new SqlConnection($"SERVER={SERVER};DATABASE={DATABASE};Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
