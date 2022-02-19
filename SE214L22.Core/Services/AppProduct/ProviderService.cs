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
    public class ProviderService : BaseService
    {
        private readonly ProviderRepository _providerRepository;

        public ProviderService()
        {
            _providerRepository = new ProviderRepository();
        }

        public IEnumerable<Provider> GetProviders()
        {
            return _providerRepository.GetProviders();
        }

        public Provider AddProvider(ProviderForCreationDto provider)
        {
            var newProvider = Mapper.Map<Provider>(provider);

            return _providerRepository.Create(newProvider);
        }

        public bool DeleteProvider(Provider provider)
        {
            return _providerRepository.Delete(provider.Id);
        }
    }
}
