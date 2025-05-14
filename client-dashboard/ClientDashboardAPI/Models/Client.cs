using System;
using System.ComponentModel.DataAnnotations;

namespace ClientDashboardAPI.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public DateTime? Archived { get; set; }
    }
}
