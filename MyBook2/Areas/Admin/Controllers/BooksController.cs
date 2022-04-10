using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBook2.Areas.Admin.Controllers
{

    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AdminRoleName)]
    public class BooksController : AdminController
    {
        public IActionResult Index() => View();
    }
}
