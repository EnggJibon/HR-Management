using System.Collections.Generic;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IWeekendCategoryService : IBaseService<WeekendCategoryModel, WeekendCategory>
    {

    }

    public class WeekendCategoryService : BaseService<WeekendCategoryModel, WeekendCategory>, IWeekendCategoryService
    {
        private readonly IWeekendCategoryRepository _weekendCategoryRepository;

        public WeekendCategoryService(IWeekendCategoryRepository weekendCategoryRepository)
            : base(weekendCategoryRepository)
        {
            _weekendCategoryRepository = weekendCategoryRepository;
        }
    }
}
