using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIUsingCore.Models
{
    public partial class Department
    {
        public int RowId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
