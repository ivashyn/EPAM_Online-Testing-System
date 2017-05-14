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

        //GET: Certificate/Search/?certificateNumber=CN2

        [HttpGet]
        public ActionResult Search(string certificateNumber)
        {
            var s = _certificateService.GetAllCertificates();
            try
            {
                var certificate = _certificateService.GetCertificateByNumber(certificateNumber.ToString());
                return View(certificate);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { @errorText = ex.Message });
            }
        }
    }
}