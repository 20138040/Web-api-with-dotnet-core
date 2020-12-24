using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIUsingCore.Models;

namespace WebAPIUsingCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        [HttpGet]
        [Route("GetAllEmployees")]
        public IEnumerable<Employee> GetAllEmployees()
        {

            OfficeDBContext dBContext = new OfficeDBContext();

            var res = dBContext.Employees.ToList();
            return res;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public string AddEmployee(Employee employee)
        {
            try
            {
                OfficeDBContext dBContext = new OfficeDBContext();
                int id = dBContext.Employees.Max(x => x.EmployeeId);
                //Employee obj = new Employee { EmployeeId = id + 1, DepartmentName = dep.DepartmentName };
                employee.EmployeeId = id + 1;
                dBContext.Add(employee);
                dBContext.SaveChanges();
                return "Added Successfully!!";
            }
            catch (Exception exp)
            {
                return "Failed to Add!!";
            }
        }

        [HttpPut]
        [Route("UpdateEmployeeDetail")]
        public string UpdateEmployeeDetail(Employee employee)
        {
            try
            {
                OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in dBContext.Employees
                           where ci.EmployeeId == employee.EmployeeId
                           select ci).FirstOrDefault();
                if (obj == null)
                {
                    return "Not found data with employeeId " + employee.EmployeeId;
                }
                obj.EmployeeName = employee.EmployeeName;
                obj.Department = employee.Department;
                obj.DateOfJoining = employee.DateOfJoining;
                obj.FilePath = employee.FilePath;
           
                dBContext.SaveChanges();
                return "Updated Successfully!!";
            }
            catch (Exception exp)
            {
                return "Failed to Update!!";
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public string DeleteEmployee(string name)
        {
            try
            {
                OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in dBContext.Employees
                           where ci.EmployeeName == name
                           select ci).FirstOrDefault();
                if (obj == null)
                {
                    return "Not found data with employee name " + name;
                }
                dBContext.Remove(obj);
                dBContext.SaveChanges();
                return "Deleted Successfully!!";
            }
            catch (Exception exp)
            {
                return "Failed to Delete!!";
            }
        }
    }
}
