using OnlineTestingSystem.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestingSystem.BLL.Interfaces
{
    public interface ISertificateService
    {
        IEnumerable<SertificateDTO> GetAllSertificates();

        IEnumerable<SertificateDTO> GetSertificatesByUserId(int userId);

        SertificateDTO GetSertificateById(int id);

        SertificateDTO GetSertificateByNumber(string number);

        void CreateSertificate(SertificateDTO sertificate);

        void DeleteSertificate(int id);

    }
}
