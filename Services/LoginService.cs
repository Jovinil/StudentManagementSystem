using Avalonia;
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

            try
            {
                using (var conn = SqlConn())
                {
                    conn.Open();
                    string query = @"select id from users where username=@username and password=hashbytes('sha2_512', @password);";
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("username", username);
                        command.Parameters.AddWithValue("password", password);

                        using (var reader = await command.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                        {
                            return reader.Read() ? result = new(reader.GetInt32(0)) : null;
                        }
                    }
                }
            }
            catch (SqlException e)
            {

                System.Environment.Exit(1);
            }
            return null;
        }
    }

    public record AuthenticationResult(
        int Id
    );

}