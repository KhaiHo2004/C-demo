using SV18T1021143.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021143.DataLayer.FakeDB
{
    public class CategoryDAL : ICategoryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public bool Delete(int Category)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int employeeID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public IList<Category> List()
        //{
        //    List<Category> data = new List<Category>();
        //    data.Add(new Category()
        //    {
        //        CatagoryID = 1,
        //        CatagoryName = "Mỹ phẩm",
        //        Description = "Đẹp da"
        //    }) ;
        //    data.Add(new Category()
        //    {
        //        CatagoryID = 1,
        //        CatagoryName = "Bia ",
        //        Description = "Bản lĩnh đàn ông"
        //    });
        //    return data;
        //}

        public IList<Category> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
