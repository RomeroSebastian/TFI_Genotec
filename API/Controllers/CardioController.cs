using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardioController : ControllerBase
    {
        // GET: api/<CardioController>
        [HttpGet]
        public IEnumerable<Cardio> Get([FromBody] Paciente paciente)
        {
            return CardioBLL.Current.GetAll(paciente);
        }

        // GET api/<CardioController>/5
        [HttpGet("{id}")]
        public Cardio Get(int id)
        {
            return CardioBLL.Current.GetOne(id);
        }

        // POST api/<CardioController>
        [HttpPost]
        public void Post([FromBody] Cardio cardio)
        {
            CardioBLL.Current.Add(cardio);
        }

        // PUT api/<CardioController>/5
        [HttpPut]
        public void Put([FromBody] Cardio cardio)
        {
            CardioBLL.Current.Update(cardio);
        }

        // DELETE api/<CardioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
