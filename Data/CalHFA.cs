using Microsoft.EntityFrameworkCore;

namespace BP_CalHFA.Data
{
    // Called in HomeController
    [Keyless]
    public class CalHFA
    {
        public int StatusCode { get; set; }
        public int LoanID { get; set; }
        public int LoanCategoryID { get; set; }
        public string StatusDate { get; set; }
        public string Description { get; set; }
    }
    // Called in JSONController
    [Keyless]
    public class Count
    {
        public int ComplianceCount { get; set; }
        public string ComplianceDate { get; set; }
        public int ComplianceSuspenseCount { get; set; }
        public string ComplianceSuspenseDate { get; set; }
        public int PurchaseCount { get; set; }
        public string PurchaseDate { get; set; }
        public int PurchaseSuspenseCount { get; set; }
        public string PurchaseSuspenseDate { get; set; }
    }
}
