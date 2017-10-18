using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IAddressRepository : IBaseRepository<Address>
    {
    }

    public class AddressRepository : BaseRepository<Address, ERP_HRM>, IAddressRepository
    {
        public AddressRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<Address> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
