using OnlineTestingSystem.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestingSystem.WebUI.Controllers
{
    public class CertificateController : Controller
    {
        ICertificateService _certificateService;
        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }
        // GET: Certificate
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Search(int certificateNumber)
        {
            //certificateNumber = "C#0000004";
            var s = _certificateService.GetAllCertificates();
            var certificate = _certificateService.GetCertificateByNumber(certificateNumber.ToString());
            if (certificate == null)
                return RedirectToAction("Index","Home");//modelStateisValid
            return View();
        }
    }
}