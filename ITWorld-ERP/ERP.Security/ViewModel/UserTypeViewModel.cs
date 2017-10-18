using System.Collections.Generic;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class UserTypeViewModel
    {
        public IEnumerable<UserTypeModel> UserTypes { get; set; }
        public UserTypeModel UserType { get; set; }
    }
}
