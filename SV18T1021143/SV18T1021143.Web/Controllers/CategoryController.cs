using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021143.DomainModel;
using SV18T1021143.BusinessLayer;
namespace SV18T1021143.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = BusinessLayer.ComomDataService.ListOfCategory(page, pageSize, searchValue, out rowCount);

            Models.CategoryPaginationResultModel model = new Models.CategoryPaginationResultModel
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data

            };
            return View(model);
        }
        /// <summary>
        /// Thêm mới một loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng mới";
            Category model = new Category()
            {
                CategoryID = 0
            };
            return View(model);

        }


        [HttpPost]
        public ActionResult Save(Category model)
        {
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName", "Tên loại hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.Description))
                ModelState.AddModelError("Description", "Mô tả không được để trống");
            //Nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.CategoryID > 0)
                    ViewBag.Title = "Cập nhật thông tin loại hàng";
                else
                    ViewBag.Title = "Bổ sung thông tin loại hàng";
                return View("Create", model);
            }
            //Xử lí dữ liệu
            if (model.CategoryID > 0)
            {
                ComomDataService.UpdateCategory(model);
                return RedirectToAction("Index");
            }
            else
            {
                ComomDataService.AddCategory(model);
                return RedirectToAction("Index");
            }
        }



        /// <summary>
        /// Sửa một loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>

        [Route("edit/{categoryID}")]
        public ActionResult Edit(int categoryID, string abc)
        {
            ViewBag.Title = "Cập nhật thông tin loại hàng";
            var model = ComomDataService.GetCategory(categoryID);
            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);
        }
        /// <summary>
        /// Xóa một loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        /// 

        [Route("delete/{categoryID}")]
        public ActionResult Delete(int categoryID)
        {
            ViewBag.Title = "Xóa loại hàng";
            if (Request.HttpMethod == "POST")
            {
                ComomDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
            var model = ComomDataService.GetCategory(categoryID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}