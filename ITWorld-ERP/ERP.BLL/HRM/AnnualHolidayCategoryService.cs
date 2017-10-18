using System.Collections.Generic;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IAnnualHolidayCategoryService : IBaseService<AnnualHolidayCategoryModel, AnnualHolidayCategory>
    {


    }

    public class AnnualHolidayCategoryService : BaseService<AnnualHolidayCategoryModel, AnnualHolidayCategory>, IAnnualHolidayCategoryService
    {
        private readonly IAnnualHolidayCategoryRepository _annualHolidayCategoryRepository;

        public AnnualHolidayCategoryService(IAnnualHolidayCategoryRepository annualHolidayCategoryRepository)
            : base(annualHolidayCategoryRepository)
        {
            _annualHolidayCategoryRepository = annualHolidayCategoryRepository;
        }
    }
}
