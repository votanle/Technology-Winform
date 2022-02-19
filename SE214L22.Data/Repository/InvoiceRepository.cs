using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        public int GetSalesByDay(DateTime day)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.InvoiceProducts
                    .Where(ip => DbFunctions.TruncateTime(ip.Invoice.CreationTime) == day.Date);

                return query.Any() ? query.Sum(ip => ip.Number): 0;
            }
        }

        public int GetSalesByMonth(DateTime day)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.InvoiceProducts
                    .Where(ip => ip.Invoice.CreationTime.Month == day.Month && ip.Invoice.CreationTime.Year == day.Year);

                return query.Any() ? query.Sum(ip => ip.Number) : 0;
            }

        }

        public int GetRevenueByDay(DateTime day)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Invoices
                    .Where(i => DbFunctions.TruncateTime(i.CreationTime) == day.Date);

                return query.Any() ? query.Sum(i => i.Total) : 0;
            }
        }

        public int GetRevenueByMonth(DateTime day)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Invoices
                    .Where(i => i.CreationTime.Month == day.Month && i.CreationTime.Year == day.Year);

                return query.Any() ? query.Sum(i => i.Total) : 0;
            }
        }
        
        public ReportByDayDto GetReportByDay(DateTime day)
        {
            using (var ctx = new AppDbContext())
            {
                var report = new ReportByDayDto();

                // day revenue
                var query = ctx.Invoices
                   .Where(i => DbFunctions.TruncateTime(i.CreationTime) == day.Date);
                report.TotalRevenue = query.Any() ? query.Sum(i => i.Total) : 0;
                report.ReportDay = day.Date;

                // product sales
                var date = day.Date.ToString("MM/dd/yyyy");
                var nextDate = day.AddDays(1).ToString("MM/dd/yyyy");
                var rawQueryScript =
                    $"select f.Id, f.Name, f.CategoryName, f.PriceOut, sum(f.Number) as Number, sum(f.Total) as Total from " +
                    $"( " +
                    $"select ip.ProductId as Id, p.Name as Name, c.Name as CategoryName, ip.Number as Number, p.PriceOut as PriceOut, sum(ip.Number * p.PriceOut) as Total " +
                    $"from Invoices as i " +
                    $"join InvoiceProducts as ip on ip.InvoiceId = i.Id " +
                    $"join Products as p on ip.ProductId = p.Id " +
                    $"join Categories as c on p.CategoryId = c.Id " +
                    $"where '{date}' <= i.CreationTime and i.CreationTime < '{nextDate}' " +
                    $"group by ip.Id, ip.ProductId, p.Name, c.Name, ip.Number, p.PriceOut " +
                    $") as f " +
                    $"group by f.Id, f.Name, f.CategoryName, f.PriceOut";

                    report.Products = ctx.Database.SqlQuery<ProductReportByDayDto>(rawQueryScript).ToList();

                var count = 1;
                foreach (var item in report.Products)
                    item.Index = count++;

                return report;
            }
        }

        public ReportByMonthDto GetReportByMonth(DateTime selectedMonth)
        {
            using (var ctx = new AppDbContext())
            {
                var report = new ReportByMonthDto();

                // month revenue, profit
                var dateStart = new DateTime(selectedMonth.Year, selectedMonth.Month, 1);
                var nextDate = dateStart.AddMonths(1).AddDays(-1);

                var rawQueryScript =
                   $"select Day, sum(f.TotalRevenue) as TotalRevenue, sum(f.TotalRevenue) - sum(f.TotalCapital) as TotalProfit from " +
                   $"( " +
                   $"select CAST(i.CreationTime AS DATE) as day, ip.ProductId as Id, p.Name as Name, c.Name as CategoryName, ip.Number as Number, p.PriceIn as PriceIn, p.PriceOut as PriceOut, sum(ip.Number * p.PriceOut) as TotalRevenue, sum(ip.Number * p.PriceIn) as TotalCapital " +
                   $"from Invoices as i " +
                   $"join InvoiceProducts as ip on ip.InvoiceId = i.Id " +
                   $"join Products as p on ip.ProductId = p.Id " +
                   $"join Categories as c on p.CategoryId = c.Id " +
                   $"where '{dateStart.ToString("MM/dd/yyyy")}' <= i.CreationTime and i.CreationTime  <= '{nextDate.ToString("MM/dd/yyyy")}' " +
                   $"group by ip.Id, ip.ProductId, p.Name, c.Name, p.PriceIn, p.PriceOut, ip.Number, CAST(i.CreationTime AS DATE) " +
                   $") as f " +
                   $"group by Day ";
                var result = ctx.Database.SqlQuery<ItemReportByMonthDto>(rawQueryScript).ToList();
                report.DayStatistics = result;
                report.TotalRevenue = result.Sum(r => r.TotalRevenue);
                report.TotalProfit = result.Sum(r => r.TotalProfit);

                var count = 1;
                foreach (var item in report.DayStatistics)
                    item.Index = count++;

                return report;
            }
        }


    }
}
