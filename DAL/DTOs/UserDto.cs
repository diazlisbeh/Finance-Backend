using System.Text.Json.Serialization;

namespace Backend.DAL.DTOs
{
    public class UserRegisterDto
    {
        public string Name{ get; set; }    
        public string Email {get;set;}
        public string LastName{ get; set; }
        public string password{ get; set; }
    }
}