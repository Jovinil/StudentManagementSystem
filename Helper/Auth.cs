using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Helper
{
    public class Auth
    {
        public static User? CurrentUser { get; set; }
    }
}
