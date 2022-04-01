using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ticketing_System.Models
{
    public class TicketType
    {
        public int Id { get; set; }

        public string TicketName { get; set; }
        
        public decimal Price { get; set; }
    }
}