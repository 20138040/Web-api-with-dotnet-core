using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIUsingCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly OfficeDBContext _officeDB;

        public EmployeeController(OfficeDBContext dBContext) 
        {
            _officeDB = dBContext;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public IEnumerable<Employee> GetAllEmployees()
        {

            //OfficeDBContext dBContext = new OfficeDBContext();

            var res = _officeDB.Employees.ToList();
            return res;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public string AddEmployee(Employee employee)
        {
            try
            {
                //OfficeDBContext dBContext = new OfficeDBContext();
                int id = _officeDB.Employees.Max(x => x.EmployeeId);
                //Employee obj = new Employee { EmployeeId = id + 1, DepartmentName = dep.DepartmentName };
                employee.EmployeeId = id + 1;
                _officeDB.Employees.Add(employee);
                _officeDB.SaveChanges();
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
                //OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in _officeDB.Employees
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

                _officeDB.SaveChanges();
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
                //OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in _officeDB.Employees
                           where ci.EmployeeName == name
                           select ci).FirstOrDefault();
                if (obj == null)
                {
                    return "Not found data with employee name " + name;
                }
                _officeDB.Employees.Remove(obj);
                _officeDB.SaveChanges();
                return "Deleted Successfully!!";
            }
            catch (Exception exp)
            {
                return "Failed to Delete!!";
            }
        }
    }
}
