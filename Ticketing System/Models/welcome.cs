using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ticketing_System.Models
{
    public class welcome
    {
        [Required(ErrorMessage = "This field is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string TicketType { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int TotalPeople { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public float Discount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public decimal NetAmount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public float Vat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public decimal TotalAmount { get; set; }

    }
}