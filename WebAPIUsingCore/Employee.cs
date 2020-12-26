using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIUsingCore
{
    public partial class Employee
    {
        public int RowId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string FilePath { get; set; }
    }
}
