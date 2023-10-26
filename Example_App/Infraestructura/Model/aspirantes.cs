using Microsoft.EntityFrameworkCore;

namespace Example_App.Infraestructura.Model
{
    public class User
    {

        public int? IdControl { get; set; }
      
        public string? Name { get; set; }

        public string? Surname { get; set; }
        public string? Address { get; set; }

        public string? Age { get; set; }

        public string? Phone { get; set; }

        public bool? Enabled { get; set; }
    }
}
