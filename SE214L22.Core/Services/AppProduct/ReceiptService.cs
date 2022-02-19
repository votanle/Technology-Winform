using SE214L22.Core.AppSession;
using SE214L22.Core.ViewModels.Orders.Dtos;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.Services.AppProduct
{
    public class ReceiptService : BaseService
    {
        private readonly ReceiptRepository _receipRepository;
        private readonly ReceiptProductRepository _receiptProductRepository;
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;

        public ReceiptService()
        {
            _receipRepository = new ReceiptRepository();
            _receiptProductRepository = new ReceiptProductRepository();
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
        }

        public void AddNewReceipt(OrderForListDto order, IEnumerable<ProductForReceiptCreation> receiptProducts)
        {
            // Add new receipt
            var total = 0;
            foreach (var item in receiptProducts) total += item.PriceIn;

            var receipt = new Receipt
            {
                OrderId = order.Id,
                CreationTime = DateTime.Now.Date,
                UserId = Session.CurrentUser.Id,
                Total = total
            };

            var storedReceipt = _receipRepository.Create(receipt);

            // Add Receipt product and update product's information (priceIn, priceOut, Number)
            foreach (var item in receiptProducts)
            {
                // 1. Add Receipt product
                var receiptProduct = Mapper.Map<ReceiptProduct>(item);
                receiptProduct.ReceiptId = storedReceipt.Id;
                _receiptProductRepository.Create(receiptProduct);

                // 2. Update product's information (priceIn, priceOut, Number)
                _productRepository.UpdateSaleProperty(item.Id, item.Number, item.PriceIn);
            }

            // Update corresponding order status to done
            _orderRepository.UpdateOrderStatusById(order.Id, OrderStatus.Done);
        }

        public IEnumerable<ProductForReceiptCreation> GetReceiptProducts(int id)
        {
            var receipt = _receiptProductRepository.GetAllByReceiptId(id);
            return Mapper.Map<IEnumerable<ProductForReceiptCreation>>(receipt);
        }

        public IEnumerable<ReceiptForListDto> GetAllReceipts(DateRangeDto dateRange)
        {
            var receipt = _receipRepository.GetAll(dateRange);
            return Mapper.Map <IEnumerable<ReceiptForListDto>>(receipt);
        }
    }
}
