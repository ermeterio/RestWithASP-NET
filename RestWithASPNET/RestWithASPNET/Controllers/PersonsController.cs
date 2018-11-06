using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Model;
using RestWithASPNET.Services.Business;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonBusiness _personBusiness;
        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
                return NotFound();
            else
                return Ok(person);
        }
        // POST api/values
        [HttpPost("v1")]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
                return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));            
        }
        // PUT api/values/5
        [HttpPut("v1")]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null)
                return BadRequest();
            return new ObjectResult(_personBusiness.Update(person));
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();

        }
    }
}
