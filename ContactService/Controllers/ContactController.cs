using System.Collections.Generic;
using ContactService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase {
        

        [HttpGet]
        public ActionResult<IEnumerable<ContactReadDto>> GetContacts(){
              return Ok();
        }
        
    }
}