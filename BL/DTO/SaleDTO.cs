using System;

namespace BL.DTO
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Manager { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public double Amount { get; set; }
    }
}
