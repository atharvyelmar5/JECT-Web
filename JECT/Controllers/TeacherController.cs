using Microsoft.AspNetCore.Mvc;

namespace JECT.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("teacher") == null)
                return RedirectToAction("TeacherLogin", "Account");

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("TeacherLogin", "Account");
        }
    }
}
