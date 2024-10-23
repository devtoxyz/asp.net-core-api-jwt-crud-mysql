using System.ComponentModel.DataAnnotations;

namespace AuthCrud.Models
{
    public class User
    {
        public int id { get; set; }
        public required string name { get; set; }

        [EmailAddress]
        public required string email { get; set; }
        public required string password { get; set; }
        public required string role { get; set; }
    }
}
