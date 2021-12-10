using BP_CalHFA.Data;
using BP_CalHFA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BP_CalHFA.Controllers
{
    /* HomeController 
     * Purpose: Create a system to interface with the DB.
     * Used in Razor Pages/HTML deployment.
     * Easy to scale and cross-reference with CSHTML code.
    */
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var display = _context.CalHFADB.ToList();
            return View(display);
        }

        public IActionResult JoinedTable()
        {
            var display = _context.CalHFADB.ToList();
            return View(display);
        }

        // Catches any invalid responses created through the database requests.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

        /* JSON Controller
         * Purpose: Host the logic used in recovering the accurate counts and dates from the DB.
         * The same logic is cloned inside the Index Razor Page for clarity and consistency. 
        */
        [Route("api/[controller]")]
        [ApiController]
        public class JSONController : Controller
        {
        private readonly MyDbContext _context;

        public JSONController(MyDbContext context)
        {
            _context = context;
        }
        
        /*
         *  Uncomment this method and comment out the second request
         *  to retrieve the JSON for the entire joined table.
         */

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<CalHFA>>> GetJSON()
        {
            return await _context.CalHFADB.ToListAsync();
        }*/

        [HttpGet]
        public async Task<string> GetJSON()
        {
            var display = _context.CalHFADB.ToListAsync();

            int complianceLoansInLineCounter = 0;
            string complianceLoansInLineStatusDate = "";
            int suspenseLoansInLineCounter = 0;
            string suspenseLoansInLineDate = "";
            int postClosingLoansInLineCounter = 0;
            string postClosingLoansInDate = "";
            int postClosingSuspenseLoansInLineCounter = 0;
            string postClosingSuspenseLoansInDate = "";

            int dateHolder1 = 0;
            int dateHolder2 = 0;
            int dateHolder3 = 0;
            int dateHolder4 = 0;

            foreach (var item in await display)
            {
                if (@item.StatusCode.ToString().EndsWith("10") && item.LoanCategoryID == 1)
                {
                    complianceLoansInLineCounter++;
                    if (dateHolder1 < 1)
                    {
                        complianceLoansInLineStatusDate = @item.StatusDate;
                        dateHolder1++;
                    }
                }
                if (@item.StatusCode.ToString().EndsWith("22") && item.LoanCategoryID == 1)
                {
                    suspenseLoansInLineCounter++;
                    if (dateHolder2 < 1)
                    {
                        suspenseLoansInLineDate = @item.StatusDate;
                        dateHolder2++;
                    }
                }
                if (@item.StatusCode.ToString().EndsWith("10") && item.LoanCategoryID == 2)
                {
                    postClosingLoansInLineCounter++;
                    if (dateHolder3 < 1)
                    {
                        postClosingLoansInDate = @item.StatusDate;
                        dateHolder3++;
                    }
                }
                if (@item.StatusCode.ToString().EndsWith("22") && item.LoanCategoryID == 2)
                {
                    postClosingSuspenseLoansInLineCounter++;
                    if (dateHolder4 < 1)
                    {
                        postClosingSuspenseLoansInDate = @item.StatusDate;
                        dateHolder4++;
                    }
                }
            }
            var count = new Count
            {
                ComplianceCount = complianceLoansInLineCounter,
                ComplianceDate = complianceLoansInLineStatusDate,
                ComplianceSuspenseCount = suspenseLoansInLineCounter,
                ComplianceSuspenseDate = suspenseLoansInLineDate,
                PurchaseCount = postClosingLoansInLineCounter,
                PurchaseDate = postClosingLoansInDate,
                PurchaseSuspenseCount = postClosingSuspenseLoansInLineCounter,
                PurchaseSuspenseDate = postClosingSuspenseLoansInDate
            };
            string jsonString = JsonSerializer.Serialize(count);
            return jsonString;
        }
    }
}
