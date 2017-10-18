using System.Collections.Generic;
using AutoMapper;
using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;


namespace ERP.BLL.Security
{
    public partial interface IScreenOperationService : IBaseService<ScreenOperationModel, ScreenOperation>
    {
        List<ScreenOperationModel> GetScreenOperationDetailsList(long? id, long? screenId);
    }

    public class ScreenOperationService : BaseService<ScreenOperationModel, ScreenOperation>, IScreenOperationService
    {
        private readonly IScreenOperationRepository _screenOperationRepository;

        public ScreenOperationService(IScreenOperationRepository screenOperationRepository)
            : base(screenOperationRepository)
        {
            _screenOperationRepository = screenOperationRepository;
        }

        public List<ScreenOperationModel> GetScreenOperationDetailsList(long? id, long? screenId)
        {
            var screenOperationDetailsList = _screenOperationRepository.GetScreenOperationDetails(id, screenId);
            return Mapper.Map<List<ScreenOperationModel>>(screenOperationDetailsList);
        }
    }
}
