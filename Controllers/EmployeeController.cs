using System.Collections.Generic;
using DapperWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DapperWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeeController(IConfiguration configuration)
        {
            employeeRepository = new EmployeeRepository(configuration);
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