using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Customers.Dtos
{
    public class CustomerForCreationDto : BaseDto        
    {
        private int? _id;
        private string _name;
        private int _customerLevelId;
        private string _phoneNumber;
        private float _accumulatedPoint;
        private DateTime _creationTime;

        public int? Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public int CustomerLevelId { get => _customerLevelId; set { _customerLevelId = value; OnPropertyChanged(); } }
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(); } }
        public float AccumulatedPoint { get => _accumulatedPoint; set { _accumulatedPoint = value; OnPropertyChanged(); } }
        public DateTime CreationTime { get => _creationTime; set { _creationTime = value; OnPropertyChanged(); } }
    }
}
