using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Model
{
    public class Contact
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }   
        public string Address { get; set; }
    }
}
