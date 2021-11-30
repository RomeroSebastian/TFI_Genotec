using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        // GET: api/<PacienteController>
        [HttpGet]
        public IEnumerable<Paciente> Get()
        {
            return PacienteBLL.Current.GetAll();
        }

        // GET api/<PacienteController>/5
        [HttpGet("{id}")]
        public Paciente GetOne(int id)
        {
            return PacienteBLL.Current.GetOne(id);
        }

        // POST api/<PacienteController>
        [HttpPost]
        public void Post([FromBody] Paciente paciente)
        {
            PacienteBLL.Current.Add(paciente);
        }

        // PUT api/<PacienteController>/5
        [HttpPut]
        public void Put([FromBody] Paciente paciente)
        {
            PacienteBLL.Current.Update(paciente);
        }

        // DELETE api/<PacienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
