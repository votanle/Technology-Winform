using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Settings.Dtos;
using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Settings
{
    public class ManufacturerViewModel : BaseViewModel
    {
        // private service fields
        private readonly ManufacturerService _manufacturerService;

        // private data fields
        private ObservableCollection<Manufacturer> _manufacturers;
        private Manufacturer _chosenManufacturer;
        private ManufacturerForCreationDto _newManufacturer;


        // public data properties
        public ObservableCollection<Manufacturer> Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged();
            }
        }
        public Manufacturer ChosenManufacturer
        {
            get => _chosenManufacturer;
            set
            {
                _chosenManufacturer = value;
                OnPropertyChanged();
            }
        }
        public ManufacturerForCreationDto NewManufacturer
        {
            get => _newManufacturer;
            set
            {
                _newManufacturer = value;
                OnPropertyChanged();
            }
        }


        // public command properties
        public ICommand DeleteManufacturer { get; set; }
        public ICommand AddManufacturer { get; set; }
        public ICommand PrepareAddManufacturer { get; set; }

        public ManufacturerViewModel()
        {
            _manufacturerService = new ManufacturerService();

            Manufacturers = new ObservableCollection<Manufacturer>(_manufacturerService.GetManufacturers());
            NewManufacturer = new ManufacturerForCreationDto { };

            DeleteManufacturer = new RelayCommand<object>
            (
                p => ChosenManufacturer == null ? false : true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _manufacturerService.DeleteManufacturer(ChosenManufacturer);
                        Manufacturers = new ObservableCollection<Manufacturer>(_manufacturerService.GetManufacturers());
                        MessageBox.Show("Xóa hãng sản xuất thành công");
                    }
                }
            );

            PrepareAddManufacturer = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    NewManufacturer = new ManufacturerForCreationDto { Name = null, Description = null };

                }
             );

            AddManufacturer = new RelayCommand<object>
            (
                p =>
                {
                    if (NewManufacturer.Name == null || NewManufacturer.Description == null )
                        return false;
                    return true;
                }
                    ,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _manufacturerService.AddManufacturer(NewManufacturer);
                        Manufacturers = new ObservableCollection<Manufacturer>(_manufacturerService.GetManufacturers());
                        MessageBox.Show("Thêm hãng sản xuất thành công");
                    }
                }
            );
        }
    }
}
