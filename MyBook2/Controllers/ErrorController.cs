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
                        "Sorry the resource you are trying to reach is no longer available!" + Environment.NewLine +
                        "The Administrator has been worn and it will be solved soon!";
                    break;

                case 500:
                    ViewBag.ErrorMessage =
                        "Sorry the resource you are trying to reach is no longer available!" +
                        "The Administrator has been worn and it will be solved soon!";
                    break;
            }

            return View("PageNotFound");
        }
    }
}
