using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientoController : ControllerBase
    {
        // GET: api/<TratamientoController>
        [HttpGet]
        public IEnumerable<Tratamiento> Get()
        {
            return TratamientoBLL.Current.GetAll();
        }

        // GET api/<TratamientoController>/5
        [HttpGet("{id}")]
        public Tratamiento Get(int id)
        {
            return TratamientoBLL.Current.GetOne(id);
        }

        // POST api/<TratamientoController>
        [HttpPost]
        public void Post(Tratamiento tratamiento)
        {
            TratamientoBLL.Current.Add(tratamiento);
        }

        // PUT api/<TratamientoController>/5
        [HttpPut("{id}")]
        public void Put(Tratamiento tratamiento)
        {
            TratamientoBLL.Current.Update(tratamiento);
        }

        // DELETE api/<TratamientoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
