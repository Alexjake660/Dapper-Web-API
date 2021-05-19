using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
        }

        //Get All
        [HttpGet]
        [Route("Get")]
        public IEnumerable<Employee> GetAll()
        {
            return employeeRepository.GetAll();
        }

        //Get By Id
        [HttpGet]
        [Route("Get/{id}")]
        public Employee GetById(int id)
        {
            return employeeRepository.GetById(id);
        }

        //Insert
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
                employeeRepository.Add(employee);
        }

        //Update
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
            employee.Id = id;
            if (ModelState.IsValid)
                employeeRepository.Update(employee);
        }

        //Delete
        [HttpDelete]
        public void Delete(int id)
        {
            employeeRepository.Delete(id);
        }
    }
}