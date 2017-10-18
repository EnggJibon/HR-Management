namespace SecurityAdministration.DAL.Repositories
{
    public class ApplicationPolicyRepository:GenericRepository<ApplicationPolicy>
    {
        public ApplicationPolicyRepository(ERP_Security context) : base(context)
        {
        }
    }
}