using Exam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Interface
{
    public interface ITiKuService
    {
        void CreateTiKu(TiKuMaster master);
        void updateTiKU(TiKuMaster master);
    }
}
