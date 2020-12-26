using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPIUsingCore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/")]
    public class DepartmentController : ControllerBase
    {

        private readonly OfficeDBContext _officeDB;

        public DepartmentController(OfficeDBContext dBContext)
        {
            _officeDB = dBContext;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public IEnumerable<Department> GetAllDepartments()
        {

            //OfficeDBContext dBContext = new OfficeDBContext();

            var res = _officeDB.Departments.ToList();


            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();

            return res;
        }

        [HttpPost]
        [Route("AddDepartment")]
        public string AddDepartment(Department dep)
        {
            try
            {
                //OfficeDBContext dBContext = new OfficeDBContext();
                int id = _officeDB.Departments.Max(x => x.DepartmentId);
                Department obj = new Department{ DepartmentId = id+1 , DepartmentName = dep.DepartmentName };
                _officeDB.Departments.Add(obj);
                _officeDB.SaveChanges();
                return "Added Successfully!!";
            }
            catch (Exception exp)
            {
                return "Failed to Add!!";
            }
        }

        [HttpPut]
        [Route("UpdateDepartmentName")]
        public string UpdateDepartmentName(Department department)
        {
            try
            {
                //OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in _officeDB.Departments
                          where ci.DepartmentId == department.DepartmentId
                          select ci).FirstOrDefault();
                if(obj == null)
                {
                    return "Not found data with departmentId " + department.DepartmentId;
                }
                obj.DepartmentName = department.DepartmentName;
                //dBContext.Add(obj);
                _officeDB.SaveChanges();
                return "Updated Successfully!!";
            }
            catch (Exception exp)
            {
                return "Failed to Update!!";
            }
        }

        [HttpDelete]
        [Route("DeleteDepartment")]
        public string DeleteDepartment(string departmentName)
        {
            try
            {
                //OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in _officeDB.Departments
                           where ci.DepartmentName == departmentName
                           select ci).FirstOrDefault();
                if (obj == null)
                {
                    return "Not found data with department " + departmentName;
                }
                _officeDB.Departments.Remove(obj);
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
