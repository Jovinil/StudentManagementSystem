using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    public interface IEditProductService
    {
        void UpdateProduct(int id, string FirstName, string MiddleName, string LastName, SqlDecimal Grade);
    }

    public class EditProductService : DatabaseConnection, IEditProductService
    {
        public void UpdateProduct(int id, string FirstName, string MiddleName, string LastName, SqlDecimal Grade)
        {
            UpdateName(id, FirstName, MiddleName, LastName);
            UpdateGrade(id, Grade);
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
                    cmd.Parameters.AddWithValue("name", FirstName);
                    cmd.Parameters.AddWithValue("name", MiddleName);
                    cmd.Parameters.AddWithValue("name", LastName);

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
