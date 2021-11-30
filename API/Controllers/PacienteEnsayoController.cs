using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteEnsayoController : ControllerBase
    {
        // GET: api/<PacienteEnsayoController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<PacienteEnsayoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<PacienteEnsayoController>
        [HttpPost]
        public void Post([FromBody] PacienteEnsayo pacienteEnsayo)
        {
            PacienteEnsayoBLL.Current.Add(pacienteEnsayo);
        }

        // PUT api/<PacienteEnsayoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PacienteEnsayoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
