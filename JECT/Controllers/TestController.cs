using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;

namespace JECT.Controllers
{
    public class TestController : Controller
    {
        public async Task<IActionResult> Test()
        {
            FirestoreDb db = FirestoreDb.Create("attendance-84add");

            await db.Collection("test")
                .AddAsync(new { msg = "Firebase Connected!" });

            return Content("Firebase OK");
        }
    }
}
