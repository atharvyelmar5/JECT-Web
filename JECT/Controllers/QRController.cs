using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace JECT.Controllers
{
    public class QRController : Controller
    {
        public IActionResult Index()
        {
            string token = Guid.NewGuid().ToString();

            string scheme = HttpContext.Request.Scheme;
            string host = HttpContext.Request.Host.Host;
            int port = HttpContext.Request.Host.Port ?? 5000;

            // If request comes from localhost, replace with IPv4
            if (host == "localhost" || host == "127.0.0.1")
            {
                host = GetLocalIPv4();
            }

            string qrUrl = $"{scheme}://{host}:{port}/Attendance/Mark?token={token}";

            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(qrUrl, QRCodeGenerator.ECCLevel.Q);

            PngByteQRCode qrCode = new PngByteQRCode(data);
            byte[] qrBytes = qrCode.GetGraphic(20);

            ViewBag.QRCodeImage =
                "data:image/png;base64," + Convert.ToBase64String(qrBytes);

            return View("Index1");
        }

        private string GetLocalIPv4()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                      .AddressList
                      .First(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                      .ToString();
        }
    }
}
