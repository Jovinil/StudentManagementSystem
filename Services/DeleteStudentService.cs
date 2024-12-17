using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    public interface IDeleteStudentService
    {
        bool DeleteStudent(int studentId);
    }

    public class DeleteStudentService: DatabaseConnection, IDeleteStudentService
    { 
        public bool DeleteStudent(int studentId)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "delete from students where id = @studentId";
                using(var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("studentId", studentId);
                    
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}
