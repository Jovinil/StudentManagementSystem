using Microsoft.Data.SqlClient;
using StudentManagementSystem.Helper;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    public interface IAddStudentsService
    {
        bool AddStudents(int BlockId, int CourseId);
    }

    public class AddStudentService : DatabaseConnection, IAddStudentsService
    {
        public bool AddStudents(int BlockId, int CourseId)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "insert into studentGrades(course_id, student_id, user_id)" +
                    "\n select @CourseId, id, @UserId" +
                    "\n from students where block_id = @BlockId";
                using(var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("CourseId", CourseId);
                    cmd.Parameters.AddWithValue("UserId", Auth.CurrentUser.Id);
                    cmd.Parameters.AddWithValue("BlockId", BlockId);

                    return cmd.ExecuteNonQuery() == 1;
                }

            }
            return false;
        }
        
    }

}
