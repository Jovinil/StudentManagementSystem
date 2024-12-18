using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    public interface IEditProductService
    {
        void UpdateStudent(int id, string FirstName, string MiddleName, string LastName, SqlDecimal Grade);
    }

    public class EditProductService : DatabaseConnection, IEditProductService
    {
        public void UpdateStudent(int id, string FirstName, string MiddleName, string LastName, SqlDecimal Grade)
        {
            Debug.WriteLine(id);
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "update students set first_name=@first_name, middle_name=@middle_name, last_name=@last_name where id=@id;";
                string query1 = "update studentGrades set grade=@Grade where student_id=@id;";
                using (var cmd1 = new SqlCommand(query1, conn))
                {
                    cmd1.Parameters.AddWithValue("id", id);
                    cmd1.Parameters.AddWithValue("Grade", Grade);

                    cmd1.ExecuteNonQuery();
                }
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("first_name", FirstName);
                    cmd.Parameters.AddWithValue("middle_name", MiddleName);
                    cmd.Parameters.AddWithValue("last_name", LastName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool UpdateName(int id, string FirstName, string MiddleName,string LastName)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "update students set first_name=@first_name, middle_name=@middle_name, last_name=@last_name where id=@id;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("first_name", FirstName);
                    cmd.Parameters.AddWithValue("middle_name", MiddleName);
                    cmd.Parameters.AddWithValue("last_name", LastName);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool UpdateGrade(int id,  SqlDecimal Grade)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "update studentGrades set grade=@Grade where student_id=@id;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("Grade", Grade);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}
