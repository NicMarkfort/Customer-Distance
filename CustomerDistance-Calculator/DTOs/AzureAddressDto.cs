using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureAddressDto
    {
        public string StreetNumber { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string MunicipalitySubdivision { get; set; } = string.Empty;
        public string Municipality { get; set; } = string.Empty;
        public string CountrySecondarySubdivision { get; set; } = string.Empty;
        public string CountryTertiarySubdivision { get; set; } = string.Empty;
        public string CountrySubdivision { get; set; } = string.Empty;
        public string CountrySubdivisionName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string ExtendedPostalCode { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCodeISO3 { get; set; } = string.Empty;
        public string FreeformAddress { get; set; } = string.Empty;
    }
}
