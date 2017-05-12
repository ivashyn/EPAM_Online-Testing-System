using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestingSystem.WebUI.Models.PaginationModels
{
    public class IndexCertificatesViewModel
    {
        public IEnumerable<CertificateDTO> Certificates { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}