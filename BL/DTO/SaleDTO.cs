using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    class SaleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Manager { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public double Amount { get; set; }
    }
}
