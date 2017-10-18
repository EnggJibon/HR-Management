using System;
using System.Threading;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.Utilities.Infrastructure
{
    [Serializable]
    public abstract class BaseDomainModel<T> : IBaseDomainModel where T : BaseDomainModel<T>
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }

        //public long skip { get; set; }
        //public long take { get; set; }
        //public long pageSize { get; set; }
        //public long page { get; set; }
        //public filter[] filters { get; set; }

        protected BaseDomainModel()
        {
            //InitializeModel();
        }

        public void SetCreateProperties(long userId)
        {
            CreatedOn = DateTime.UtcNow;            
            CreatedBy = userId;
           
            ModifiedOn = null;
            ModifiedBy = null;
        }

        public void SetUpdateProperties(long userId)
        {
            ModifiedOn = DateTime.UtcNow;
            ModifiedBy = userId;
        }

        //public void MarkAsDeleted(long userId)
        //{
        //    IsDeleted = true;
        //}

        //public void SetProperties()
        //{
        //    //CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
        //    IsDeleted = false;
        //    CreatedOn = DateTime.UtcNow;
        //    ModifiedOn = DateTime.UtcNow;
        //    //if (principal != null)
        //    //{
        //    //    CreatedBy = principal.PersonId;
        //    //    ModifiedBy = principal.PersonId;
        //    //}
        //}

        //private void InitializeModel()
        //{
        //    //CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
        //    //IsDeleted = false;
        //    CreatedOn = DateTime.UtcNow;
        //    ModifiedOn = DateTime.UtcNow;
        //    //if (principal != null)
        //    //{
        //    //    CreatedBy = principal.PersonId;
        //    //    ModifiedBy = principal.PersonId;
        //    //}
        //}
    }

    //public class filter
    //{
    //    public string field { get; set; }
    //    public string Operator { get; set; }
    //    public string value { get; set; }
    //    public string logic { get; set; }
    //}
}