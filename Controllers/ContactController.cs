using ContactsApi.Data;
using ContactsApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactController : Controller
    {
        private readonly ContactDbContext dbContext;

        public ContactController(ContactDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetContact()
        {

            return Ok(dbContext.contact.ToList()); 
        }
        [HttpPost]  
        public async Task<IActionResult> AddContact(AddContactRequest addcontactrequest)
        {
            var contact1 = new Contact()
            {
                UserId = Guid.NewGuid(),
                Name = addcontactrequest.Name,
                Email = addcontactrequest.Email,
                Phone = addcontactrequest.Phone,
                Address = addcontactrequest.Address,
            };
          await  dbContext.contact.AddAsync(contact1);
            await dbContext.SaveChangesAsync();
            return Ok(contact1);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, Contact contact)
        {
           var cont= await dbContext.contact.FindAsync(id);
            if (cont != null)
            {
                cont.Name = contact.Name;
                cont.Email = contact.Email;
                cont.Phone = contact.Phone;
                cont.Address = contact.Address;
                dbContext.contact.Add(cont);
               await dbContext.SaveChangesAsync();
                return Ok(cont);
            }
            return BadRequest();

        }

    }
}
