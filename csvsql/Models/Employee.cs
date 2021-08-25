using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace csvsql.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(100)]
        public string Designation { get; set; }
        public int Salary { get; set; }
    }
}