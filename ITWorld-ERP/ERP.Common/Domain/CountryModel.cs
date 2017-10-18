using ERP.Utilities.Infrastructure;

namespace ERP.Common.Domain
{
    public class CountryModel : BaseDomainModel<CountryModel>
    {
        public string CountryName { get; set; }
        public string CountryCodeAlpha2 { get; set; }
        public string CountryCodeAlpha3 { get; set; }
        public string CountryCodeNumeric { get; set; }
        public string ISOCode { get; set; }
    }
}
