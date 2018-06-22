using System;

namespace DAL.Models
{
    public class SaleInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public double Amount { get; set; }
    }
}
