using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021143.DomainModel;
namespace SV18T1021143.DataLayer.SQLServer
{
  public interface ICountryDAL
    {

       /// <summary>
       /// Lấy danh sách các quốc gia
       /// </summary>
       /// <returns></returns>
        IList<Country> List();
    }
}
