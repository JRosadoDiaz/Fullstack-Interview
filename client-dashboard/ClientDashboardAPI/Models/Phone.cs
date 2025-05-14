using System;
using System.ComponentModel.DataAnnotations;

namespace ClientDashboardAPI.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber  { get; set; }
    }
}
