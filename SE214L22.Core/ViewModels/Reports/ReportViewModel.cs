using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using SE214L22.Core.Services.AppProduct;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Workbook = Microsoft.Office.Interop.Excel.Workbook;
using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace SE214L22.Core.ViewModels.Reports
{
    public class ReportViewModel : BaseViewModel
    {
        private static ReportViewModel _instance;
        public static ReportViewModel getInstance()
        {
            return _instance;
        }
        // service
        private readonly InvoiceService _invoiceService;

        // private fields
        private DateTime _selectedDate;
        private int _totalDayRevenue;
        private ObservableCollection<ProductReportByDayDto> _products;

        private DateTime _selectedMonth;
        private int _totalRevenue;
        private int _totalProfit;
        private ObservableCollection<ItemReportByMonthDto> _dayStatistics;

        // public property
        public DateTime SelectedDate 
        { 
            get => _selectedDate; 
            set 
            { 
                _selectedDate = value; 
                OnPropertyChanged();
                LoadReportByDay();
            } 
        }
        public int TotalDayRevenue
        {
            get => _totalDayRevenue;
            set
            {
                _totalDayRevenue = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ProductReportByDayDto> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedMonth
        { 
            get => _selectedMonth; 
            set 
            {
                _selectedMonth = value;
                LoadReportByMonth();
                OnPropertyChanged(); 
            } 
        }
        public int TotalRevenue { get => _totalRevenue; set { _totalRevenue = value; OnPropertyChanged(); } } 
        public int TotalProfit { get => _totalProfit; set { _totalProfit = value; OnPropertyChanged(); } }
        public ObservableCollection<ItemReportByMonthDto> DayStatistics
        {
            get => _dayStatistics;
            set
            {
                _dayStatistics = value;
                OnPropertyChanged();
            }
        }

        public void Update()
        {
            LoadReportByDay();
            LoadReportByMonth();
        }
        // command
        public ICommand CDayReportToExcel { get; set; }
        public ICommand CMonthReportToExcel { get; set; }
        public ReportViewModel()
        {
            if (_instance == null)
                _instance = this;
            // service
            _invoiceService = new InvoiceService();

            // init data
            SelectedDate = DateTime.Now;
            SelectedMonth = DateTime.Now;
            CDayReportToExcel = new RelayCommand<object>((p) => { return true; }, (p) => { DayReportToExcel(); });
            CMonthReportToExcel = new RelayCommand<object>((p) => { return true; }, (p) => { MonthReportToExcel(); });
        }

        private void LoadReportByDay()
        {
            var reportByDay = _invoiceService.GetReportByDay(SelectedDate);
            Products = new ObservableCollection<ProductReportByDayDto>(reportByDay.Products);
            TotalDayRevenue = reportByDay.TotalRevenue;
        }

        private void LoadReportByMonth()
        {
            var reportByMonth = _invoiceService.GetReportByMonth(SelectedMonth);
            DayStatistics = new ObservableCollection<ItemReportByMonthDto>(reportByMonth.DayStatistics);
            TotalRevenue = reportByMonth.TotalRevenue;
            TotalProfit = reportByMonth.TotalProfit;
        }

        // Export to excel
        private void DayReportToExcel()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet reportSheet = (Worksheet)workbook.Sheets[1];

                for (int j = 0; j < 7; j++)
                {
                    Range reportRange = (Range)reportSheet.Cells[1, j + 1];
                    ((Range)reportSheet.Cells[1, j + 1]).Font.Bold = true;
                    ((Range)reportSheet.Columns[j + 1]).ColumnWidth = 15;
                    switch(j)
                    {
                        case 0:
                            reportRange.Value2 = "STT";
                            break;
                        case 1:
                            reportRange.Value2 = "Mã sản phẩm";
                            break;
                        case 2:
                            reportRange.Value2 = "Tên mặt hàng";
                            break;
                        case 3:
                            reportRange.Value2 = "Loại mặt hàng";
                            break;
                        case 4:
                            reportRange.Value2 = "Số lượng";
                            break;
                        case 5:
                            reportRange.Value2 = "Đơn giá";
                            break;
                        case 6:
                            reportRange.Value2 = "Thành tiền";
                            break;

                    }
                }
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < Products.Count; j++)
                    {
                        Range reportRange = (Range)reportSheet.Cells[j + 2, i + 1];
                        switch (i)
                        {
                            case 0:
                                reportRange.Value2 = Products[j].Index;
                                break;
                            case 1:
                                reportRange.Value2 = Products[j].Id;
                                break;
                            case 2:
                                reportRange.Value2 = Products[j].Name;
                                break;
                            case 3:
                                reportRange.Value2 = Products[j].CategoryName;
                                break;
                            case 4:
                                reportRange.Value2 = Products[j].Number;
                                break;
                            case 5:
                                reportRange.Value2 = Products[j].PriceOut;
                                break;
                            case 6:
                                reportRange.Value2 = Products[j].Total;
                                break;

                        }
                    }
                }
                Range revenueRange = (Range)reportSheet.Cells[2 + Products.Count, 1];
                ((Range)reportSheet.Cells[2 + Products.Count, 1]).Font.Bold = true;
                ((Range)reportSheet.Columns[1]).ColumnWidth = 15;
                revenueRange.Value2 = "Tổng Doanh Thu: " + TotalRevenue;

            }
            catch (Exception e)
            {
                MessageBox.Show("Không thể xuất file excel!" + e.ToString());
            }


        }
        private void MonthReportToExcel()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet reportSheet = (Worksheet)workbook.Sheets[1];

                for (int j = 0; j < 4; j++)
                {
                    Range reportRange = (Range)reportSheet.Cells[1, j + 1];
                    ((Range)reportSheet.Cells[1, j + 1]).Font.Bold = true;
                    ((Range)reportSheet.Columns[j + 1]).ColumnWidth = 15;
                    switch (j)
                    {
                        case 0:
                            reportRange.Value2 = "STT";
                            break;
                        case 1:
                            reportRange.Value2 = "Ngày";
                            break;
                        case 2:
                            reportRange.Value2 = "Doanh Thu";
                            break;
                        case 3:
                            reportRange.Value2 = "Lợi Nhuận";
                            break;

                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < DayStatistics.Count; j++)
                    {
                        Range reportRange = (Range)reportSheet.Cells[j + 2, i + 1];
                        switch (i)
                        {
                            case 0:
                                reportRange.Value2 = DayStatistics[j].Index;
                                break;
                            case 1:
                                reportRange.Value2 = DayStatistics[j].Day;
                                break;
                            case 2:
                                reportRange.Value2 = DayStatistics[j].TotalRevenue;
                                break;
                            case 3:
                                reportRange.Value2 = DayStatistics[j].TotalProfit;
                                break;

                        }
                    }
                }
                Range revenueRange = (Range)reportSheet.Cells[2 + DayStatistics.Count, 1];
                ((Range)reportSheet.Cells[2 + DayStatistics.Count, 1]).Font.Bold = true;
                ((Range)reportSheet.Columns[1]).ColumnWidth = 15;
                revenueRange.Value2 = "Tổng Doanh Thu: " + TotalRevenue;
                Range profitRange = (Range)reportSheet.Cells[3 + DayStatistics.Count, 1];
                ((Range)reportSheet.Cells[3 + DayStatistics.Count, 1]).Font.Bold = true;
                ((Range)reportSheet.Columns[1]).ColumnWidth = 15;
                profitRange.Value2 = "Tổng Lợi Nhuận: " + TotalProfit;

            }
            catch (Exception e)
            {
                MessageBox.Show("Không thể xuất file excel!" + e.ToString());
            }


        }
    }
}
