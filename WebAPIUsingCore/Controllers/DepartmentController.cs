using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPIUsingCore.Models;

namespace WebAPIUsingCore.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class DepartmentController : ControllerBase
    {
       
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ILogger<DepartmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public IEnumerable<Department> GetAllDepartments()
        {

            OfficeDBContext dBContext = new OfficeDBContext();

            var res = dBContext.Departments.ToList();


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
                OfficeDBContext dBContext = new OfficeDBContext();
                int id = dBContext.Departments.Max(x => x.DepartmentId);
                Department obj = new Department{ DepartmentId = id+1 , DepartmentName = dep.DepartmentName };
                dBContext.Add(obj);
                dBContext.SaveChanges();
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
                OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in dBContext.Departments
                          where ci.DepartmentId == department.DepartmentId
                          select ci).FirstOrDefault();
                if(obj == null)
                {
                    return "Not found data with departmentId " + department.DepartmentId;
                }
                obj.DepartmentName = department.DepartmentName;
                //dBContext.Add(obj);
                dBContext.SaveChanges();
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
                OfficeDBContext dBContext = new OfficeDBContext();
                var obj = (from ci in dBContext.Departments
                           where ci.DepartmentName == departmentName
                           select ci).FirstOrDefault();
                if (obj == null)
                {
                    return "Not found data with department " + departmentName;
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
