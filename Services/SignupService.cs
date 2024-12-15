using Microsoft.Data.SqlClient;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    public interface ISignupService
    {
        bool Signup(User user);
    }

    public class SignupService : DatabaseConnection, ISignupService
    {
        public bool Signup(User user)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = @"insert into Users(first_name, middle_name, last_name, username, password) 
                                values(@first_name, @middle_name, @last_name, @username, hashbytes('sha2_512', @password));";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("first_name", user.FirstName);
                    cmd.Parameters.AddWithValue("middle_name", user.MiddleName == null ? "" : user.MiddleName);
                    cmd.Parameters.AddWithValue("last_name", user.LastName);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);
                        
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}