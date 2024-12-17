using Microsoft.Data.SqlClient;
using StudentManagementSystem.Helper;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Repositories
{
    public interface IStudentRepository
    {
        IAsyncEnumerable<StudentGrade> GetAllWithGrade();
        StudentGrade GetStudent(int Id);
        IAsyncEnumerable<StudentGrade> GetStudentsByBlock(int Id);
    }

    public class StudentRepository : DatabaseConnection, IStudentRepository
    {
        public async IAsyncEnumerable<StudentGrade> GetAllWithGrade()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select CONCAT(first_name, middle_name, last_name) as fullname, b.name, s.year, c.name, sg.grade from studentGrades as sg" +
                    "\n join students as s on s.id = sg.student_id" +
                    "\n join blocks as b on s.block_id = b.id" +
                    "\n join courses as c on c.id = sg.course_id" +
                    "\n where sg.user_id = @UserId;";
                using(var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("UserId", Auth.CurrentUser.Id);

                    using (var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    { 
                        while (await reader.ReadAsync())
                        {
                            yield return new StudentGrade
                            {
                                Student = new Student
                                {
                                    FullName = reader.GetString(0),
                                    Block = new Block
                                    {
                                        Name = reader.GetString(1)
                                    },
                                    Year = reader.GetInt32(2)
                                },
                                Course = new Course
                                {
                                    Name = reader.GetString(3)
                                },
                                Grade = reader.IsDBNull(4) ? SqlDecimal.Parse("0") : reader.GetSqlDecimal(4)
                            };
                        }
                    }
                }
            }
        }

        public StudentGrade GetStudent(int Id)
        {
            using(var conn = SqlConn())
            {
                conn.Open();
                string query = "select  s.id, s.first_name, s.middle_name, s.last_name, sg.grade from studentGrades as sg" +
                    "\n join students as s on s.id = sg.student_id" +
                    "\n where s.id = @Id;";
                using(var cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("Id", Id);

                    using(var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            return new StudentGrade
                            {
                                Student = new Student
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    MiddleName = reader.GetString(2),
                                    LastName = reader.GetString(3)
                                },
                                Grade = reader.GetSqlDecimal(4)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async IAsyncEnumerable<StudentGrade> GetStudentsByBlock(int Id)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "Select concat(s.first_name, ' ', s.middle_name, ' ', s.last_name), sg.grade from students as s" +
                    "\n join studentGrades as sg on sg.student_id = s.id" +
                    "\n where s.block_id = @id;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", Id);

                    using(var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while(await reader.ReadAsync())
                        {
                            yield return new StudentGrade
                            {
                                Student = new Student
                                {
                                    FullName = reader.GetString(0),
                                },
                                Grade = reader.GetSqlDecimal(1)
                            };
                        }
                    }
                }
            }
        }
    }

}
