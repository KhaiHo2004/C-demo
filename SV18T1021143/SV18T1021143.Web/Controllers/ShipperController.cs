using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021143.DomainModel;
namespace SV18T1021143.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        // GET: Shipper
        public ActionResult Index(int page = 1, string searchValue = "")
        {

            int pageSize = 10;
            int rowCount = 0;
            var data = BusinessLayer.ComomDataService.ListOfShipper(page, pageSize, searchValue, out rowCount);

            Models.ShipperPaginationResultModel model = new Models.ShipperPaginationResultModel
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
        /// Thêm mới người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Shippers model = new Shippers()
            {
                ShipperID = 0
            };
            ViewBag.Title = "Bổ sung người giao hàng mới";
            return View(model);
        }
        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>

        [Route("edit/{shipperID}")]
        public ActionResult Edit(int shipperID, string abc)
        {
            ViewBag.Title = "Cập nhật thông tin người giao hàng";
            var model = BusinessLayer.ComomDataService.GetShipper(shipperID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View("Create", model);
        }

        /// <summary>
        /// Luu nguoi giao hang 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Shippers model)
        {
            if (model.ShipperID > 0)
            {
                BusinessLayer.ComomDataService.UpdateShipper(model);
                return RedirectToAction("Index");
            }
            else
            {
                BusinessLayer.ComomDataService.AddShipper(model);
                return RedirectToAction("Index");
            }

        }



        /// <summary>
        /// Xóa người giao hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        /// 

        [Route("delete/{shipperID}")]
        public ActionResult Delete(int shipperID)
        {
            if (Request.HttpMethod == "POST")
            {
                BusinessLayer.ComomDataService.DeleteShipper(shipperID);
                return RedirectToAction("Index");
            }
            var model = BusinessLayer.ComomDataService.GetShipper(shipperID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}