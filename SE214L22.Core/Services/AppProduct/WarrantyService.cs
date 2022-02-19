using SE214L22.Core.ViewModels.Warranties.Dtos;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.Services.AppProduct
{
    public class WarrantyService : BaseService
    {
        private readonly InvoiceProductRepository _invoiceProductRepository;
        private readonly WarrantyOrderRepository _warrantyOrderRepository;

        public WarrantyService()
        {
            _invoiceProductRepository = new InvoiceProductRepository();
            _warrantyOrderRepository = new WarrantyOrderRepository(); 
        }

        public IEnumerable<ProductForWarrantyDto> GetCustomerProductsForWarranty(string phoneNumber)
        {
            return Mapper.Map<IEnumerable<ProductForWarrantyDto>>(_invoiceProductRepository.GetInvoiceProductsByCustomerPhoneNumber(phoneNumber));
        }

        public WarrantyOrder AddNewWarrantyOrder(ProductForWarrantyDto customerProduct)
        {
            // check if this product has already been add to warranty order? (through InvoiceId, ProductId)
            var noProductsBought = _invoiceProductRepository.GetNumberOfProductByInvoiceId(customerProduct.InvoiceId, customerProduct.Id);

            var noProductsOnWarratyOrders = _warrantyOrderRepository.GetNumberOfWarrantyOrderByInvoiceIdAndProductId(customerProduct.InvoiceId, customerProduct.Id);

            if (noProductsOnWarratyOrders == noProductsBought)
            {
                throw new Exception("Sản phẩm này đang được bảo hành rồi!");
            }

            // now we can actually add it
            var warrantyOrder = Mapper.Map<WarrantyOrder>(customerProduct);
            warrantyOrder.Status = (int)WarrantyOrderStatus.WaitForSent;
            warrantyOrder.CreationTime = DateTime.Now;
            return _warrantyOrderRepository.Create(warrantyOrder);
        }

        public IEnumerable<ProductForListWarrantyDto> GetWarrantyOrders(List<WarrantyOrderStatus> filter)
        {
            return Mapper.Map<IEnumerable<ProductForListWarrantyDto>>(_warrantyOrderRepository.GetAllWithStatusFilter(filter));
        }

        public void UpdateWarrantyOrderStatus(ProductForListWarrantyDto input)
        {
            var warrantyOrder = Mapper.Map<WarrantyOrder>(input);
            _warrantyOrderRepository.Update(warrantyOrder);
        }
    }
}
