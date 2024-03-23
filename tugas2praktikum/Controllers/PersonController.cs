using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using tugas2praktikum.Models;

namespace tugas2praktikum.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        private string __constr;

        public PersonController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        [HttpGet("api/person")]
        public ActionResult<IEnumerable<Person>> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> listPerson = context.ListPerson();
            return Ok(listPerson);
        }

        [HttpPost("api/person")]
        public ActionResult<Person> AddPerson(Person person)
        {
            try
            {
                PersonContext context = new PersonContext(this.__constr);
                context.AddPerson(person);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add person: {ex.Message}");
            }
        }

        [HttpPut("api/person/{id}")]
        public ActionResult<Person> UpdatePerson(int id, Person person)
        {
            try
            {
                if (id != person.id_murid)
                    return BadRequest("ID mismatch");

                PersonContext context = new PersonContext(this.__constr);
                context.UpdatePerson(person);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update person: {ex.Message}");
            }
        }

        [HttpDelete("api/person/{id}")]
        public ActionResult DeletePerson(int id)
        {
            try
            {
                PersonContext context = new PersonContext(this.__constr);
                context.DeletePerson(id);
                return Ok($"Person with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete person: {ex.Message}");
            }
        }
    }
}
