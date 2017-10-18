using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IAddressService : IBaseService<AddressModel, Address>
    {
    }

    public class AddressService : BaseService<AddressModel, Address>, IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
            : base(addressRepository)
        {
            _addressRepository = addressRepository;
        }
    }
}
