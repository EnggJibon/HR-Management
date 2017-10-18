using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IUserTypeService : IBaseService<UserTypeModel, UserType>
    {
    }

    public class UserTypeService : BaseService<UserTypeModel, UserType>, IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeService(IUserTypeRepository userTypeRepository)
            : base(userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }
    }
}
