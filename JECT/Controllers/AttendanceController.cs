using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JECT.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly FirestoreDb _db;

        public AttendanceController()
        {
            _db = FirestoreDb.Create("attendance-84add");
        }

        public async Task<IActionResult> Mark(string token)
        {
            if (string.IsNullOrEmpty(token))
                return Content("Invalid QR");

            // Student must be logged in
            var studentId = HttpContext.Session.GetString("studentId");

            if (string.IsNullOrEmpty(studentId))
            {
                return RedirectToAction(
                    "StudentLogin",
                    "Account",
                    new { returnUrl = Url.Action("Mark", "Attendance", new { token }) }
                );
            }

            var data = new Dictionary<string, object>
            {
                { "token", token },
                { "studentId", studentId },
                { "date", DateTime.Now.ToString("yyyy-MM-dd") },
                { "time", DateTime.Now.ToString("HH:mm:ss") },
                { "createdAt", Timestamp.GetCurrentTimestamp() }
            };

            await _db.Collection("attendance").AddAsync(data);

            return View("Success");
        }
    }
}
