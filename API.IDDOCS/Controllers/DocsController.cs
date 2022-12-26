using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Extentions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.IDDOCS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocsController : ControllerBase
    {
        private readonly EfRepository _db;
        public DocsController(EfRepository db) => _db = db;
       




        
        [HttpGet("/api/docs/get/{id}")]
        public IdDoc GetDoc(Guid id)
        {
            return _db.Get<IdDoc>(x => x.ID == id).Result;
        }



        //И автор, и получатель документа могут запрашивать список документов по API,
        //при этом имеется фильтрация документов по дате создания
        
        [HttpGet("/api/docs/list")]
        public IEnumerable<IdDoc> GetAll()
        {
            var curentUserId = _db.Get<Client>(x => x.IIN == User.Identity.Name).Result.ID;

            var docs = _db.GetAll<IdDoc>(x => x.CreatedUserId == curentUserId || x.ReceiverUserId == curentUserId)
                .OrderByDescending(x => x.CreatedDate);
            return docs;
        }




        
        [HttpPost("/api/docs/add")]
        public async Task<HttpResponce> Add([FromBody] IdDocDTO dto)
        {
            var curentUserId = _db.Get<Client>(x => x.IIN == User.Identity.Name).Result.ID;

            IdDoc newDoc = new IdDoc 
            {
              ID = dto.ID,
              Content = dto.Content,
              CreatedDate = DateTime.Now,
              CreatedUserId = curentUserId,
              Name = dto.Name,
              ReceiverUserId = dto.ReceiverUserId,
              Type = dto.Type
            };

            await _db.AddAsync<IdDoc>(newDoc);

            return new HttpResponce { IsSuccess = true };
        }




        
        [HttpPut("/api/docs/update")]
        public object Put([FromBody] IdDocDTO dto)
        {
            var curentUserId = _db.Get<Client>(x => x.IIN == User.Identity.Name).Result.ID;
            var updDoc = _db.Get<IdDoc>(x => x.ID == dto.ID && x.CreatedUserId == curentUserId).Result;


            if (updDoc != null)
            {
                updDoc.ReceiverUserId = dto.ReceiverUserId;
                updDoc.Name = dto.Name;
                updDoc.Content = dto.Content;
                updDoc.Type = dto.Type;

                _db.Update<IdDoc>(updDoc);

                return new HttpResponce { IsSuccess = true };
            }
            
            return BadRequest(new HttpResponce { IsSuccess = false, Error = "Изменять контент документа, удалять документ может только автор документа" });
        }




        
        [HttpDelete("/api/docs/remove/{id}")]
        public object Delete(Guid id)
        {
            var curentUserId = _db.Get<Client>(x => x.IIN == User.Identity.Name).Result.ID;

            var doc = _db.Get<IdDoc>(x => x.ID == id && x.CreatedUserId == curentUserId).Result;
            
            if(doc != null)
            {
                _db.DeleteAsync<IdDoc>(doc);
                return new HttpResponce { IsSuccess = true };
            }

            return BadRequest(new HttpResponce { IsSuccess = false, Error = "Изменять контент документа, удалять документ может только автор документа" });
        }




        
        [HttpGet("/api/docs/download/{id}")]
        public FileContentResult Download(Guid id)
        {
            var doc = _db.Get<IdDoc>(x => x.ID == id).Result;

            var result = doc.ToExcel();

            return result;
        }




        
        [HttpGet("/api/docs/download")]
        public FileContentResult Download()
        {
            var curentUserId = _db.Get<Client>(x => x.IIN == User.Identity.Name).Result.ID;

            var docs = _db.GetAll<IdDoc>(x => x.CreatedUserId == curentUserId || x.ReceiverUserId == curentUserId)
                .OrderByDescending(x => x.CreatedDate);

            return docs.ToList().ToExcel();
        }
    }
}
