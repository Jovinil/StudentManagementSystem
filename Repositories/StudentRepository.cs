﻿using Microsoft.Data.SqlClient;
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
        Student GetStudent(int Id);
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

        public Student GetStudent(int Id)
        {
            return null;
        }
    }

}