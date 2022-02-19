
namespace SE214L22.Core.ViewModels.Orders.Dtos
{
    public class SelectingProductDto : BaseDto
    {
        public int _selectedNumber;

        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int SelectedNumber { get => _selectedNumber; set { _selectedNumber = value; OnPropertyChanged(); } }
        public int PriceOut { get; set; }

        SelectingProductDto()
        {
            SelectedNumber = 1;
        }
    }
}
