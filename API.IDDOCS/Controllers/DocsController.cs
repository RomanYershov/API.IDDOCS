using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.IDDOCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocsController : ControllerBase
    {
        private readonly EfRepository _db;
        public DocsController(EfRepository db) => _db = db;
       
        [Authorize]
        [HttpGet("/api/docs/get/{id}")]
        public IdDoc GetDoc(Guid id)
        {
            return _db.Get<IdDoc>(x => x.Number == id).Result;
        }





        [Authorize]
        [HttpPost("/api/docs/add")]
        public async Task<IdDoc> Add([FromBody] IdDoc doc)
        {
            var user = HttpContext;

            return await _db.AddAsync<IdDoc>(doc);
        }





        [Authorize]
        [HttpPut("/api/docs/update")]
        public void Put([FromBody] IdDoc doc)
        {
            _db.Update<IdDoc>(doc);
        }

        // DELETE api/<DocsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
