using System.Collections.Generic;
using AutoMapper;
using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IUserInformationService : IBaseService<UserInformationModel, UserInformation>
    {
        List<UserInformationModel> GetAllUsers();
        UserInformationModel GetUserDetails(long? id, string username);
        UserInformationModel GetUser(long employeeId);
        long CreateUser(UserInformationModel userInformation);
        void UpdateUser(UserInformationModel userInformation);
        void ChangePassword(UserInformationModel userInformation);
    }

    public class UserInformationService : BaseService<UserInformationModel, UserInformation>, IUserInformationService
    {
        private readonly IUserInformationRepository _userInformationRepository;

        public UserInformationService(IUserInformationRepository userInformationRepository)
            : base(userInformationRepository)
        {
            _userInformationRepository = userInformationRepository;
        }

        public List<UserInformationModel> GetAllUsers()
        {
            var user = _userInformationRepository.GetAllUsers();
            return Mapper.Map<List<UserInformationModel>>(user);
        }

        public UserInformationModel GetUserDetails(long? id, string username)
        {
            var user = _userInformationRepository.GetUserDetails(id, username);
            return Mapper.Map<UserInformationModel>(user);
        }

        public UserInformationModel GetUser(long employeeId)
        {
            var user = _userInformationRepository.GetUser(employeeId);
            return Mapper.Map<UserInformationModel>(user);
        }

        public long CreateUser(UserInformationModel userInformation)
        {
            userInformation.Password = Authenticator.GetHashPassword(userInformation.Password);
            return Insert(userInformation);
        }

        public void UpdateUser(UserInformationModel userInformation)
        {
            _userInformationRepository.UpdateUser(Mapper.Map<UserInformation>(userInformation));
        }

        public void ChangePassword(UserInformationModel userInformation)
        {
            var userFromDatabase = GetById(userInformation.Id);

            if (Authenticator.ValidatePassword(userInformation.OldPassword, userFromDatabase.Password))
            {
                userFromDatabase.Password = Authenticator.GetHashPassword(userInformation.NewPassword);
                userFromDatabase.IsPasswordChanged = true;
                _userInformationRepository.UpdateUserPassword(Mapper.Map<UserInformation>(userFromDatabase));
            }
        }
    }
}
