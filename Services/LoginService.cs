using Microsoft.Data.SqlClient;
using StudentManagementSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_open.Services
{
    public interface ILoginService
    {
        Task<AuthenticationResult?> Authenticate(string username, string password);

    }

    public class LoginService : DatabaseConnection, ILoginService
    {
        public async Task<AuthenticationResult?> Authenticate(string username, string password)
        {
            AuthenticationResult result = null;
            //if (username == null || password == null)
            //    return null;
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = @"select id,first_name,middle_name,last_name,username from users where username=@username and password=hashbytes('sha2_512', @password);";
                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("password", password);

                    using (var reader = await command.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        return reader.Read() ? result = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)) : null;
                    }
                }
            }
        }
    }

    public record AuthenticationResult(
        int Id,
        string FirstName,
        string? MiddleName,
        string LastName,
        string Username
        )
    {
        public string FullName = $"{FirstName} {MiddleName} {LastName}";
    }

}