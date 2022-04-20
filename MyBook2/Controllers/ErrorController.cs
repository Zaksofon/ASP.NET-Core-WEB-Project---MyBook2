using System;
using Microsoft.AspNetCore.Mvc;

namespace MyBook2.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode:int}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage =
                        "Sorry, the page you are trying to reach has not been found!";
                    break;

                case 500:
                    ViewBag.ErrorMessage =
                        "Sorry, the page you are trying to reach has not been found!";

                    break;
            }

            return View("PageNotFound");
        }
    }
}
