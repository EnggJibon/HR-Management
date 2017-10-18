using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IAnnualHolidayService : IBaseService<AnnualHolidayModel, AnnualHoliday>
    {

        List<SelectListItem> GetDayList();
        List<SelectListItem> GetAnnualHolidayType();


    }

    public class AnnualHolidayService : BaseService<AnnualHolidayModel, AnnualHoliday>, IAnnualHolidayService
    {

        private readonly IAnnualHolidayRepository _annualHolidayRepository;
        public AnnualHolidayService(IAnnualHolidayRepository annualHolidayRepository)
            : base(annualHolidayRepository)
        {
            _annualHolidayRepository = annualHolidayRepository;
        }

        public List<SelectListItem> GetDayList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Saturday",
                    Value = "Saturday"
                },
                new SelectListItem
                {
                    Text = "Sunday",
                    Value = "Sunday"
                },

                new SelectListItem
                {
                    Text = "Monday",
                    Value = "Monday"
                },                
                 new SelectListItem
                {
                    Text = "Tuesday",
                    Value = "Tuesday"
                },    
                new SelectListItem
                {
                    Text = "Wednesday",
                    Value = "Wednesday"
                },
                new SelectListItem
                {
                    Text = "Thursday",
                    Value = "Thursday"
                },

                new SelectListItem
                {
                    Text = "Friday",
                    Value = "Friday"
                }
            };
        }


        public List<SelectListItem> GetAnnualHolidayType()
        {
            return new List<SelectListItem>
            {
                
                new SelectListItem()
                {
                    Text = "SubjectToMoon",
                    Value = "SM"
                },

                new SelectListItem()
                { 
                    Text = "Fixed",
                    Value = "F"
                },
                new SelectListItem()
                {
                    Text = "Special",
                    Value="S"
                }
            };
        }


    }
}
