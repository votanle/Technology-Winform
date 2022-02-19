using SE214L22.Core.ViewModels.Settings.Dtos;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.Services.AppProduct
{
    public class ManufacturerService : BaseService
    {
        private readonly ManufacturerRepository _manufacturerRepository;

        public ManufacturerService()
        {
            _manufacturerRepository = new ManufacturerRepository();
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return _manufacturerRepository.GetManufacturers();
        }

        public Manufacturer AddManufacturer(ManufacturerForCreationDto manufacturer)
        {
            var newManufacturer = Mapper.Map<Manufacturer>(manufacturer);

            return _manufacturerRepository.Create(newManufacturer);
        }

        public bool DeleteManufacturer(Manufacturer manufacturer)
        {
            return _manufacturerRepository.Delete(manufacturer.Id);
        }
    }
}
