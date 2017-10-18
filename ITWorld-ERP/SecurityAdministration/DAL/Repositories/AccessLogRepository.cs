namespace SecurityAdministration.DAL.Repositories
{
    public class AccessLogRepository: GenericRepository<AccessLog>
    {
        public AccessLogRepository(ERP_Security context) : base(context)
        {
        }
    }
}