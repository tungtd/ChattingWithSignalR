using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ChattingWithSignalR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Recording()
        {
            return View();
        }
        public ActionResult RecordingAudio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostRecordedAudioVideo()
        {
            foreach (string upload in Request.Files)
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                var file = Request.Files[upload];
                if (file == null) continue;

                file.SaveAs(Path.Combine(path, upload));
            }
            return Json(new { success = "true" });
        }


        [HttpPost]
        public async Task<ActionResult> PostRecordedPart(string jsonInput)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var obj = serializer.Deserialize<string>(jsonInput);
                var bytes = Convert.FromBase64String(obj);
                string path = "~//uploads//Recordings//test.webm";

                //if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                //{
                //    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));
                //}
                using (var stream = new FileStream(System.Web.HttpContext.Current.Server.MapPath(path), FileMode.Append))
                {
                    try
                    {
                        await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {


                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return Json(new { success = "true" });
        }
    }
}