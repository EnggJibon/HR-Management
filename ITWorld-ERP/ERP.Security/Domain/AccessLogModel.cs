using System;
using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class AccessLogModel : BaseDomainModel<AccessLogModel>
    {
        public long Username { get; set; }
        public string UserType { get; set; }
        public string LoginDeviceName { get; set; }
        public string LoginIP { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public byte? PasswordAttemptCount { get; set; }
    }
}
