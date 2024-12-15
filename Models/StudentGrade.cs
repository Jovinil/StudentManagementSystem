using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class StudentGrade
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public User User { get; set; }
        public SqlDecimal Grade { get; set; }

    }
}
