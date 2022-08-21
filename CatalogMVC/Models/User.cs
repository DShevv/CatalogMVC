using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogMVC.Models
{
    public class User: IdentityUser
    {
        
        public string? Login { get; set; }
        
        public string? Password { get; set; }
        
   
    }
}
