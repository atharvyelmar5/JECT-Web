using Microsoft.AspNetCore.Mvc;

namespace JECT.Controllers
{
    public class AccountController : Controller
    {
        // ======================
        // TEACHER LOGIN
        // ======================

        public IActionResult TeacherLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeacherLogin(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                HttpContext.Session.SetString("teacher", username);
                return RedirectToAction("Dashboard", "Teacher");
            }

            ViewBag.Error = "Invalid teacher login";
            return View();
        }

        // ======================
        // STUDENT LOGIN
        // ======================

        public IActionResult StudentLogin(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Account/StudentLogin.cshtml");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("TeacherLogin");
        }


        [HttpPost]
        public IActionResult StudentLogin(string studentId, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                ViewBag.Error = "Enter Student ID";
                return View();
            }

            HttpContext.Session.SetString("studentId", studentId);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Success", "Attendance");
        }
    }
}
