using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021143.DomainModel;
using SV18T1021143.DataLayer;
using System.Configuration;
namespace SV18T1021143.Web.Controllers
{/// <summary>
/// 
/// </summary>
    [Authorize]
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        // GET: Supplier

        /// <summary>
        ///  Tìm kiếm hiển thị danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = BusinessLayer.ComomDataService.ListOfSupplier(page, pageSize, searchValue, out rowCount);

            Models.SupplierPaginationResultModel model = new Models.SupplierPaginationResultModel
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data

            };
            return View(model);
            
        }

        public ActionResult Create()
        {
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp mới";
            return View(model);
        }

        [Route("edit/{supplierID}")]
        public ActionResult Edit(int supplierID, string abc)
        {
            ViewBag.Title = "Cập nhật nhà cung cấp";
            var model = BusinessLayer.ComomDataService.GetSupplier(supplierID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View("Create", model);
         
        }

        [HttpPost]
        public ActionResult Save(Supplier model)
        {
            if (model.SupplierID > 0)
            {
                BusinessLayer.ComomDataService.UpdateSupplier(model);
                return RedirectToAction("Index");
            }
            else
            {
                BusinessLayer.ComomDataService.AddSupplier(model);
                return RedirectToAction("Index");
            }

        }


        [Route("delete/{supplierID}")]
        public ActionResult Delete(int supplierID)
        {
            if (Request.HttpMethod == "POST")
            {
                BusinessLayer.ComomDataService.DeleteSupplier(supplierID);
                return RedirectToAction("Index");
            }
            var model = BusinessLayer.ComomDataService.GetSupplier(supplierID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

    }
}
