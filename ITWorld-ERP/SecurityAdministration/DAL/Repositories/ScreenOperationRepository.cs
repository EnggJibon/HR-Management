namespace SecurityAdministration.DAL.Repositories
{
    public class ScreenOperationRepository:GenericRepository<ScreenOperation>
    {
        public ScreenOperationRepository(ERP_Security context) : base(context)
        {
        }
    }
}