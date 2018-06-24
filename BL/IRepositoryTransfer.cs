using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IRepositoryTransfer
    {
        void AddSaleInfo(SaleDTO saleDto);

        IEnumerable<SaleDTO> GetSales();
        IEnumerable<ManagerDTO> GetManagers();
        IEnumerable<ClientDTO> GetClients();
        IEnumerable<ProductDTO> GetProducts();
        void UpdateManager(ManagerDTO managerDTO);
        void UpdateSaleInfo(SaleDTO saleDTO);
    }
}
