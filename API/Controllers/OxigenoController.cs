using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OxigenoController : ControllerBase
    {
        // GET: api/<OxigenoController>
        [HttpGet]
        public IEnumerable<Oxigeno> Get([FromBody] Paciente paciente)
        {
            return OxigenoBLL.Current.GetAll(paciente);
        }

        // GET api/<OxigenoController>/5
        [HttpGet("{id}")]
        public Oxigeno Get(int id)
        {
            return OxigenoBLL.Current.GetOne(id);
        }

        // POST api/<OxigenoController>
        [HttpPost]
        public void Post([FromBody] Oxigeno Oxigeno)
        {
            OxigenoBLL.Current.Add(Oxigeno);
        }

        // PUT api/<OxigenoController>/5
        [HttpPut]
        public void Put([FromBody] Oxigeno Oxigeno)
        {
            OxigenoBLL.Current.Update(Oxigeno);
        }

        // DELETE api/<OxigenoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
