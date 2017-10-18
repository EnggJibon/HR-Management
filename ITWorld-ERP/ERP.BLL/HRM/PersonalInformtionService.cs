using System.Collections.Generic;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IPersonalInformationService : IBaseService<PersonalInformationModel, PersonalInformation>
    {
        List<string> GetTitleList();
        List<SelectListItem> GetGenderList();
        List<SelectListItem> GetMaritalStatusList();
    }

    public class PersonalInformationService : BaseService<PersonalInformationModel, PersonalInformation>, IPersonalInformationService
    {
        private readonly IPersonalInformationRepository _personalInformationRepository;

        public PersonalInformationService(IPersonalInformationRepository personalInformationRepository)
            : base(personalInformationRepository)
        {
            _personalInformationRepository = personalInformationRepository;
        }

        public List<string> GetTitleList()
        {
            return new List<string>
            {
                "Mr.",
                "Mrs.",
                "Miss"
            };
        }

        public List<SelectListItem> GetGenderList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Male",
                    Value = "M"
                },
                new SelectListItem
                {
                    Text = "Female",
                    Value = "F"
                }
            };
        }
        public List<SelectListItem> GetMaritalStatusList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Married",
                    Value = "M"
                },
                new SelectListItem
                {
                    Text = "Unmarried",
                    Value = "U"
                }
            };
        }
    }
}
