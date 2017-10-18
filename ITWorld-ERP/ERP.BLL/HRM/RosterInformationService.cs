using System.Collections.Generic;
using System.Web.Mvc;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IRosterInformationService : IBaseService<RosterInformationModel, RosterInformation>
    {


    }

    public class RosterInformationService : BaseService<RosterInformationModel, RosterInformation>, IRosterInformationService
    {
        private readonly IRosterInformationRepository _rosterInformationRepository;

        public RosterInformationService(IRosterInformationRepository rosterInformationRepository)
            : base(rosterInformationRepository)
        {
            _rosterInformationRepository = rosterInformationRepository;
        }
    }
}
