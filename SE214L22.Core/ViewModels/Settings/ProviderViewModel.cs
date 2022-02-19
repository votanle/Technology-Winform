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
    public class ProviderViewModel : BaseViewModel
    {
        // private service fields
        private readonly ProviderService _providerService;

        // private data fields
        private ObservableCollection<Provider> _providers;
        private Provider _chosenProvider;
        private ProviderForCreationDto _newProvider;


        // public data properties
        public ObservableCollection<Provider> Providers
        {
            get => _providers;
            set
            {
                _providers = value;
                OnPropertyChanged();
            }
        }
        public Provider ChosenProvider
        {
            get => _chosenProvider;
            set
            {
                _chosenProvider = value;
                OnPropertyChanged();
            }
        }
        public ProviderForCreationDto NewProvider
        {
            get => _newProvider;
            set
            {
                _newProvider = value;
                OnPropertyChanged();
            }
        }


        // public command properties
        public ICommand DeleteProvider { get; set; }
        public ICommand AddProvider { get; set; }
        public ICommand PrepareAddProvider { get; set; }

        public ProviderViewModel()
        {
            _providerService = new ProviderService();

            Providers = new ObservableCollection<Provider>(_providerService.GetProviders());
            NewProvider = new ProviderForCreationDto { };

            DeleteProvider = new RelayCommand<object>
            (
                p => ChosenProvider == null ? false : true,
                p =>
                {
                    _providerService.DeleteProvider(ChosenProvider);
                    Providers = new ObservableCollection<Provider>(_providerService.GetProviders());
                    MessageBox.Show("Xóa nhà cung cấp thành công");
                }
            );

            PrepareAddProvider = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    NewProvider = new ProviderForCreationDto { };
                }
             );

            AddProvider = new RelayCommand<object>
            (
                p =>
                {
                    if (NewProvider.Name == null || NewProvider.PhoneNumber == null || NewProvider.Email == null || NewProvider.Address == null)
                        return false;
                    return true;
                }
                    ,
                p =>
                {
                    _providerService.AddProvider(NewProvider);
                    Providers = new ObservableCollection<Provider>(_providerService.GetProviders());
                    MessageBox.Show("Thêm nhà cung cấp thành công");
                }
            );
        }
    }
}
