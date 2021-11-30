using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturaController : ControllerBase
    {
        // GET: api/<TemperaturaController>
        [HttpGet]
        public IEnumerable<Temperatura> Get([FromBody] Paciente paciente)
        {
            return TemperaturaBLL.Current.GetAll(paciente);
        }

        // GET api/<TemperaturaController>/5
        [HttpGet("{id}")]
        public Temperatura Get(int id)
        {
            return TemperaturaBLL.Current.GetOne(id);
        }

        // POST api/<TemperaturaController>
        [HttpPost]
        public void Post([FromBody] Temperatura Temperatura)
        {
            TemperaturaBLL.Current.Add(Temperatura);
        }

        // PUT api/<TemperaturaController>/5
        [HttpPut]
        public void Put([FromBody] Temperatura Temperatura)
        {
            TemperaturaBLL.Current.Update(Temperatura);
        }

        // DELETE api/<TemperaturaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
