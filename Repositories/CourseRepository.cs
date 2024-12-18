using Microsoft.Data.SqlClient;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Repositories
{
    public interface ICourseRepository
    {
        IAsyncEnumerable<Course> GetAllCourses();
    }

    public class CourseRepository : DatabaseConnection, ICourseRepository
    { 
        public async IAsyncEnumerable<Course> GetAllCourses()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select id,name from courses;";
                using(var cmd = new SqlCommand(query, conn))
                {
                    using(var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while(await reader.ReadAsync())
                        {
                            yield return new Course
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                    }
                }
            }
        }
    }

}
