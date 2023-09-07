using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace AngularSecurityExerciseDemo.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        /*Without a code scanning yaml file that also checks C#, this will not be picked up by code scanning*/
        //add a try catch that will trigger a code scan error
        public int SomeBuggedTryCatchCode(int RequestId = 5)
        {
            try
            {
                int someInt = RequestId;
                return someInt;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString() + " something failed.");
                return 7;
            }
        }
    }
}