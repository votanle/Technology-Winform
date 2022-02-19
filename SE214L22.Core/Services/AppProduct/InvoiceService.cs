using SE214L22.Core.AppSession;
using SE214L22.Core.ViewModels.Home.Dtos;
using SE214L22.Core.ViewModels.Sells.Dtos;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Repository;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.Services.AppProduct
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly InvoiceProductRepository _invoiceProductRepository;
        private readonly ProductRepository _productRepository;
        private readonly CustomerLevelRepository _customerLevelRepository;

        public InvoiceService()
        {
            _invoiceRepository = new InvoiceRepository();
            _customerRepository = new CustomerRepository();
            _invoiceProductRepository = new InvoiceProductRepository();
            _productRepository = new ProductRepository();
            _customerLevelRepository = new CustomerLevelRepository();
        }

        public ReportByDayDto GetReportByDay(DateTime day)
        {
            return _invoiceRepository.GetReportByDay(day);
        }

        public ReportByMonthDto GetReportByMonth(DateTime selectedMonth)
        {
            return _invoiceRepository.GetReportByMonth(selectedMonth);
        }

        /// <summary>
        /// Get general infomation about renenue and sales statistics
        /// </summary>
        /// <param name="date">Datetime context</param>
        /// <param name="type">Date or Month or Year?</param>
        public RevenueDto GetRevenue(DateTime dateTime, TimeType type = TimeType.Day)
        {
            RevenueDto dto = new RevenueDto();
            if (type == TimeType.Day)
            { // Get revenue of the day

                // 1. Get sales number
                dto.Sales = _invoiceRepository.GetSalesByDay(dateTime);

                // 2. Get revenue
                dto.Revenue = _invoiceRepository.GetRevenueByDay(dateTime);
            }
            else  // Get revenue of the month
            {
                // 1. Get sales number
                dto.Sales = _invoiceRepository.GetSalesByMonth(dateTime);

                // 2. Get revenue
                dto.Revenue = _invoiceRepository.GetRevenueByMonth(dateTime);
            }

            return dto;
        }

        public void AddInvoice(InvoiceForCreationDto invoice, List<SelectingProductForSellDto> products)
        {
            // 1. add custom?
            var phoneNumber = invoice.PhoneNumber;

            // check if it's existing?
            var customer = _customerRepository.GetCustomByPhoneNumber(phoneNumber);
            var customerId = -1;

            if (customer == null)
            {
                customer = new Customer { Name = invoice.CustomerName, PhoneNumber = phoneNumber, CreationTime = DateTime.Now, AccumulatedPoint = invoice.Price / 100000 };
                if (customer.AccumulatedPoint >= _customerLevelRepository.GetCustomerLevelByName("Hạng Vàng").PointLevel)
                    customer.CustomerLevelId = 3;
                else if (customer.AccumulatedPoint >= _customerLevelRepository.GetCustomerLevelByName("Hạng Bạc").PointLevel)
                    customer.CustomerLevelId = 2;
                else
                    customer.CustomerLevelId = 1;
                var storedCustomer = _customerRepository.Create(customer);
                customerId = storedCustomer.Id;
            }
            else
            {
                customerId = customer.Id;
            }

            // 2. add invoice
            var storedInvoice = _invoiceRepository.Create(new Invoice
            {
                CustomerId = customerId,
                UserId = Session.CurrentUser.Id,
                CreationTime = DateTime.Now,
                Total = invoice.Total,
                Discount = invoice.Discount,
                Price = invoice.Price
            }); ;

            // 3. add invoice's products and decrease no. each product
            foreach (var product in products)
            {
                var invoiceProduct = new InvoiceProduct
                {
                    ProductId = product.Id,
                    Number = product.SelectedNumber,
                    InvoiceId = storedInvoice.Id
                };

                _invoiceProductRepository.Create(invoiceProduct);
                _productRepository.UpdateNumberById(product.Id, product.SelectedNumber);
            }
        }
    }
}
