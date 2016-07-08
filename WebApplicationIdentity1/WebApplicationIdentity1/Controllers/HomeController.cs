using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationIdentity1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                // ovde ustvari proveravamo da li postoje vrednosti
                // npr ako nije uneto first_name ili last_name a imaju annotation [required]
                // onda ModelState nije validan
                return RedirectToAction("About");
            }

            if (file != null)
            {
                string checkextension = Path.GetExtension(file.FileName).ToLower();
                string exeExtension = "exe";
                bool isExeFile = checkextension.Contains(exeExtension);
                bool hasContent = file.ContentLength > 0;

                if (isExeFile)
                {
                    ModelState.AddModelError("exeFile", "This is exe file so we can not add it!");
                    //TempData["errorMsg"] = "This is exe file";
                }
                else if (!hasContent)
                {
                    ModelState.AddModelError("zero", "It doesn't exist or size is 0!");
                    //TempData["errorMsg"] = "It doesn't exist or size is 0!";
                }
                else
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Server.MapPath("~/App_Data/uploads");
                    var path = Path.Combine(filePath, fileName);
                    file.SaveAs(path);
                    TempData["successMsg"] = "File uploaded!";
                }

            }

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
        public ActionResult Upload()
        {
            
            return View("Upload");
        }
    }
}