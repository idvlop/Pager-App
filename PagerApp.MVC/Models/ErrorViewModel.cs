using System;

namespace PagerApp.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public bool ShowStatusCode => !string.IsNullOrEmpty(StatusCode);
        public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
    }
}
