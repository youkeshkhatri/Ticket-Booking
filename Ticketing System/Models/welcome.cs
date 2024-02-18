using System.ComponentModel.DataAnnotations;

namespace Ticketing_System.Models
{
    public class welcome
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Date { get; set; }

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