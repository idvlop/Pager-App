using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace PagerApp.MVC.Models
{
    public class ErrorViewModel : PageModel
    {
        public string RequestId { get; set; }

        public int HttpStatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public bool ShowStatusCode => HttpStatusCode != 0;
        public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        public string OriginalURL { get; set; }
        public bool ShowOriginalURL => !string.IsNullOrEmpty(OriginalURL);

        public void OnGet()//string code)
        {
            //StatusCode = code;

            var statusCodeReExecuteFeature = HttpContext.Features.Get<
                                                   IStatusCodeReExecuteFeature>();
            if (statusCodeReExecuteFeature != null)
            {
                OriginalURL =
                    statusCodeReExecuteFeature.OriginalPathBase
                    + statusCodeReExecuteFeature.OriginalPath
                    + statusCodeReExecuteFeature.OriginalQueryString;
            }
        }
    }
}
