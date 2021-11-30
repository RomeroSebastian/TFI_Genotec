using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnsayoClinicoController : ControllerBase
    {
        // GET: api/<EnsayoClinicoController>
        [HttpGet]
        public IEnumerable<EnsayoClinico> Get()
        {
            return EnsayoClinicoBLL.Current.GetAll();
        }

        // GET api/<EnsayoClinicoController>/5
        [HttpGet("{id}")]
        public EnsayoClinico Get(int id)
        {
            return EnsayoClinicoBLL.Current.GetOne(id);
        }

        // POST api/<EnsayoClinicoController>
        [HttpPost]
        public void Post([FromBody] EnsayoClinico ensayoClinico)
        {
            EnsayoClinicoBLL.Current.Add(ensayoClinico);
        }

        // PUT api/<EnsayoClinicoController>/5
        [HttpPut]
        public void Put([FromBody] EnsayoClinico ensayoClinico)
        {
            EnsayoClinicoBLL.Current.Update(ensayoClinico);
        }

        // DELETE api/<EnsayoClinicoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
