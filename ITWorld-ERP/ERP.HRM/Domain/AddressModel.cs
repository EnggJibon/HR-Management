using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class AddressModel : BaseDomainModel<AddressModel>
    {
        public long PersonId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public long CountryId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
