using System.Collections.Generic;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IWeekendService : IBaseService<WeekendModel, Weekend>
    {
        List<SelectListItem> GetDayList();
    }

    public class WeekendService : BaseService<WeekendModel, Weekend>, IWeekendService
    {
        private readonly IWeekendRepository _weekendRepository;

        public WeekendService(IWeekendRepository weekendRepository)
            : base(weekendRepository)
        {
            _weekendRepository = weekendRepository;
        }


        public List<SelectListItem> GetDayList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Saterday",
                    Value = "Sat"
                },
                new SelectListItem
                {
                    Text = "Sunday",
                    Value = "Sun"
                },

                new SelectListItem
                {
                    Text = "Monday",
                    Value = "Mon"
                },                
                 new SelectListItem
                {
                    Text = "Tuesday",
                    Value = "Tues"
                },    
                new SelectListItem
                {
                    Text = "Wednesday",
                    Value = "Wed"
                },
                new SelectListItem
                {
                    Text = "Thursday",
                    Value = "Thurs"
                },

                new SelectListItem
                {
                    Text = "Friday",
                    Value = "Fri"
                }
            };
        }







    }

}
