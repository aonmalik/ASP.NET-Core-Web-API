using ContactsApi.Data;
using ContactsApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Controllers
{
    [ApiController]
    [Route("API/[Controller]")]
    public class ContactController : Controller
    {
        private readonly ContactDbContext dbContext;

        public ContactController(ContactDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
                        [HttpGet]   //getting all the contact records
                        public async  Task<IActionResult> GetContacts()
                        {

                            return Ok(await dbContext.contact.ToListAsync()); 
                        }


                        [HttpGet] //getting single contact record

                        [Route("GetSingle{id:guid}")]
                        public async Task<IActionResult> GetContact([FromRoute] Guid id)
                        {
                          var contact= await dbContext.contact.FindAsync(id);
                            if (contact == null)
                            {
                                return BadRequest();
                            }
                            return Ok(contact);   
                        }


                        [HttpPost]  
                        public async Task<IActionResult> AddContact(Contact addcontact)
                        {
                            var contact1 = new Contact()
                            {
                                Id = Guid.NewGuid(),
                                Name = addcontact.Name,
                                Email = addcontact.Email, 
                                Phone = addcontact.Phone,
                                Address = addcontact.Address,
                            };
                            await  dbContext.contact.AddAsync(contact1);
                            await  dbContext.SaveChangesAsync();
                            return Ok(contact1);
                          
            
                        }


                        [HttpPut]

                        [Route("update{id:guid}")]
                        public async Task<IActionResult> Update([FromRoute]Guid id, Contact updatecontact)
                        {
                           var cont= await dbContext.contact.FindAsync(id);
                            if (cont != null)
                            {
                                cont.Name = updatecontact.Name;
                                cont.Email = updatecontact.Email;
                                cont.Phone = updatecontact.Phone;
                                cont.Address = updatecontact.Address;
                                await dbContext.SaveChangesAsync();
                                return Ok(cont);
                            }
                                return NotFound();

                        }


                        [HttpDelete]

                        [Route("Delete{id:guid}")]

                        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
                        {
                            var contact=await dbContext.contact.FindAsync(id);
                            if(contact != null)
                            {
                                 dbContext.Remove(contact);
                                 dbContext.SaveChanges();    
                                 return Ok();
                            }
                            return BadRequest();
                        }

    }
}
