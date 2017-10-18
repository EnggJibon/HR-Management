using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;


namespace ERP.BLL.Security
{
    public partial interface IScreenService : IBaseService<ScreenModel, Screen>
    {
        List<SelectListItem> GetScreenTypeList();

        List<ScreenModel> GetScreenList(long? id, long? moduleId);
    }

    public class ScreenService : BaseService<ScreenModel, Screen>, IScreenService
    {
        private readonly IScreenRepository _screenRepository;

        public ScreenService(IScreenRepository screenRepository)
            : base(screenRepository)
        {
            _screenRepository = screenRepository;
        }

        public List<SelectListItem> GetScreenTypeList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Screen",
                    Value = "S"
                },
                new SelectListItem
                {
                    Text = "Report",
                    Value = "R"
                }
            };
        }

        public List<ScreenModel> GetScreenList(long? id, long? moduleId)
        {
            var screenList = _screenRepository.GetScreens(id, moduleId);
            return Mapper.Map<List<ScreenModel>>(screenList);
        }

    }
}
