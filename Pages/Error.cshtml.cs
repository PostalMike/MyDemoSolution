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
        //add a try catch that will trigger a vulnerability error
        public int SomeBuggedTryCatchCode(int RequestId = RequestId)
        {
            try
            {
                //this will obviously fail, there should be an alert upon checking in this code.
                int someInt += RequestId + "it's a sunny day."
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString() + " something failed.");
            }
        }
    }
}