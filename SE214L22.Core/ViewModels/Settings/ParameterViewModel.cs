using SE214L22.Core.Services.AppProduct;
using SE214L22.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Settings
{
    public class ParameterViewModel : BaseViewModel
    {
        // private service fields
        private readonly ParameterService _parameterService;

        // private data fields
        private int _minInputProductNumber;
        private int _maxInputProductNumber;
        private int _minAge;
        private int _maxAge;


        // public data properties
        public int MinInputProductNumber
        {
            get => _minInputProductNumber;
            set
            {
                _minInputProductNumber = value;
                OnPropertyChanged();
            }
        }
        public int MaxInputProductNumber
        {
            get => _maxInputProductNumber;
            set
            {
                _maxInputProductNumber = value;
                OnPropertyChanged();
            }
        }
        public int MinAge
        {
            get => _minAge;
            set
            {
                _minAge = value;
                OnPropertyChanged();
            }
        }
        public int MaxAge
        {
            get => _maxAge;
            set
            {
                _maxAge = value;
                OnPropertyChanged();
            }
        }
        // public command properties
        public ICommand UpdateParameter { get; set; }
        public ParameterViewModel()
        {
            // service
            _parameterService = new ParameterService();


            // data            
            MinInputProductNumber = _parameterService.GetParameterByName(ParameterType.MinInputProductNumber).Value;
            MaxInputProductNumber = _parameterService.GetParameterByName(ParameterType.MaxInputProductNumber).Value;
            MinAge = _parameterService.GetParameterByName(ParameterType.MinAge).Value;
            MaxAge = _parameterService.GetParameterByName(ParameterType.MaxAge).Value;


            // command            
            UpdateParameter = new RelayCommand<object>
            (
              p => true,
              p =>
              {
                  _parameterService.UpdateParameterByName(ParameterType.MinInputProductNumber, MinInputProductNumber);
                  _parameterService.UpdateParameterByName(ParameterType.MaxInputProductNumber, MaxInputProductNumber);
                  _parameterService.UpdateParameterByName(ParameterType.MinAge, MinAge);
                  _parameterService.UpdateParameterByName(ParameterType.MaxAge, MaxAge);
                  MessageBox.Show("Sửa tham số thành công");
              });


        }
    }
}
