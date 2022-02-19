using AutoMapper;
using SE214L22.Core.ViewModels.Home.Dtos;
using SE214L22.Core.ViewModels.Orders.Dtos;
using SE214L22.Core.ViewModels.Settings.Dtos;
using SE214L22.Core.ViewModels.Sells.Dtos;
using SE214L22.Core.ViewModels.Users.Dtos;
using SE214L22.Core.ViewModels.Products.Dtos;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Entity.AppUser;
using SE214L22.Data.Repository.AggregateDto;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Core.ViewModels.Warranties.Dtos;
using System;
using SE214L22.Core.ViewModels.Customers.Dtos;

namespace SE214L22.Core
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            // Dto for home
            CreateMap<Order, ProcessingOrderDto>()
                .ForMember(dest => dest.CreatedUser, opt =>
                    opt.MapFrom(src => src.CreationUser.Name))
                .ForMember(dest => dest.ProviderName, opt =>
                    opt.MapFrom(src => src.Provider.Name))
                .ForMember(dest => dest.Status, opt =>
                    opt.MapFrom(src => ProcessingOrderDto.MapEnumToStatus((OrderStatus)src.Status)));

            CreateMap<ProductAggregateDto, HotProductDto>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ManufacturerName, opt =>
                    opt.MapFrom(src => src.Product.Manufacturer.Name))
                .ForMember(dest => dest.Sales, opt =>
                    opt.MapFrom(src => src.SalesNo));

            // User
            CreateMap<UserForCreationDto, User>()
                .ForMember(dest => dest.Role, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.RoleId, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.Id, opt =>
                    opt.Ignore());
            CreateMap<User, UserForCreationDto>()
                .ForMember(dest => dest.Role, opt =>
                    opt.MapFrom(src => src.Role.Name));

            // Product for order
            CreateMap<Product, ProductForOrderCreationDto>()
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ManufacturerName, opt =>
                    opt.MapFrom(src => src.Manufacturer.Name));
            CreateMap<ProductForOrderCreationDto, SelectingProductDto>();
            CreateMap<OrderForCreationDto, Order>()
                .ForMember(dest => dest.Status, opt =>
                    opt.MapFrom(src => (int)OrderStatus.WaitForSent))
                .ForMember(dest => dest.Id, opt =>
                    opt.Ignore());
            CreateMap<Order, OrderForCreationDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.Ignore());
            CreateMap<SelectingProductDto, OrderProduct>()
                .ForMember(dest => dest.ProductId, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt =>
                    opt.MapFrom(src => src.SelectedNumber));
            CreateMap<OrderProduct, SelectingProductDto>()
                 .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Product.Id))
                 .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Product.Category.Name))
                 .ForMember(dest => dest.PriceOut, opt =>
                    opt.MapFrom(src => src.Product.PriceOut))
                 .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Product.Name))
                 .ForMember(dest => dest.SelectedNumber, opt =>
                    opt.MapFrom(src => src.Number));
            CreateMap<OrderProduct, ProductForOrderListDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Product.Category.Name));

            // Product for sell
            CreateMap<Product, ProductForSellDto>()
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ManufacturerName, opt =>
                    opt.MapFrom(src => src.Manufacturer.Name));
            CreateMap<ProductForSellDto, SelectingProductForSellDto>()
                .ForMember(dest => dest.SelectedNumber, opt =>
                    opt.MapFrom(src => 1));

            // Product for receipt
            CreateMap<OrderProduct, ProductForReceiptCreation>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.PriceIn, opt =>
                    opt.MapFrom(src => src.Product.PriceIn));
            CreateMap<ProductForReceiptCreation, ReceiptProduct>()
                .ForMember(dest => dest.ProductId, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReceiptId, opt =>
                    opt.Ignore());
            CreateMap<ReceiptProduct, ProductForReceiptCreation>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Product.Category.Name));


            // Provider
            CreateMap<ProviderForCreationDto, Provider>();

            // Manufacturer
            CreateMap<ManufacturerForCreationDto, Manufacturer>();

            // Category
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryForDisplayDto, Category>();
            CreateMap<Category, CategoryForDisplayDto>();

            //Product
            CreateMap<Product, ProductDisplayDto>()
                .ForMember(dest => dest.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ReturnRate, opt =>
                    opt.MapFrom(src => src.ReturnRate == null ? src.Category.ReturnRate : src.ReturnRate));

            CreateMap<ProductForCreationDto, Product>();

            CreateMap<ProductDisplayDto, Product>();


            // Warranty
            CreateMap<InvoiceProduct, ProductForWarrantyDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ManufacturerName, opt =>
                    opt.MapFrom(src => src.Product.Manufacturer.Name))
                .ForMember(dest => dest.InvoiceTime, opt =>
                    opt.MapFrom(src => src.Invoice.CreationTime))
                .ForMember(dest => dest.WarrantyTimeRemaining, opt =>
                    opt.MapFrom(src => ProductForWarrantyDto.CalcWarrantyMonthRemaining(src.Invoice.CreationTime, (int)src.Product.WarrantyPeriod)))
                .ForMember(dest => dest.CustomerName, opt => 
                    opt.MapFrom(src => src.Invoice.Customer.Name))
                .ForMember(dest => dest.PhoneNumber, opt =>
                    opt.MapFrom(src => src.Invoice.Customer.PhoneNumber))
                .ForMember(dest => dest.CustomerId, opt =>
                    opt.MapFrom(src => src.Invoice.CustomerId))
                .ForMember(dest => dest.InvoiceId, opt =>
                    opt.MapFrom(src => src.InvoiceId));
            CreateMap<ProductForWarrantyDto, WarrantyOrder>()
                .ForMember(dest => dest.ProductId, opt =>
                    opt.MapFrom(src => src.Id));
            CreateMap<WarrantyOrder, ProductForListWarrantyDto>()
                .ForMember(dest => dest.CustomerName, opt =>
                    opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.PhoneNumber, opt =>
                    opt.MapFrom(src => src.Customer.PhoneNumber))
                .ForMember(dest => dest.ProductName, opt =>
                    opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.WarrantyStatus, opt =>
                    opt.MapFrom(src => src.Status));
            CreateMap<ProductForListWarrantyDto, WarrantyOrder>()
                .ForMember(dest => dest.Status, opt =>
                    opt.MapFrom(src => src.WarrantyStatus));

            // Order
            CreateMap<Order, OrderForListDto>()
                .ForMember(dest => dest.CreationTime, opt =>
                    opt.MapFrom(src => src.CreationTime.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.CreationUser, opt =>
                    opt.MapFrom(src => src.CreationUser.Name))
                .ForMember(dest => dest.ProviderName, opt =>
                    opt.MapFrom(src => src.Provider.Name));

            // Receipt
            CreateMap<Receipt, ReceiptForListDto>()
                .ForMember(dest => dest.CreationTime, opt =>
                    opt.MapFrom(src => src.CreationTime.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.CreationUser, opt =>
                    opt.MapFrom(src => src.CreationUser.Name))
                .ForMember(dest => dest.ProviderName, opt =>
                    opt.MapFrom(src => src.Order.Provider.Name));

            // CustomerLevel
            CreateMap<CustomerLevelForDisplayDto, CustomerLevel>();
            CreateMap<CustomerLevel, CustomerLevelForDisplayDto>();

            // Customer
            CreateMap<CustomerForCreationDto, Customer>();
            CreateMap<Customer, CustomerForCreationDto>();

            CreateMap<CustomerDisplayDto, Customer>();
            CreateMap<Customer, CustomerDisplayDto>();
        }
    }
}
