using SV18T1021143.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021143.Web.Controllers
{
    [Authorize]
    [RoutePrefix("customer")]
   
    public class CustomerController : Controller
    {
        // GET: Customer
        /// <summary>
        /// Tìm kiếm hiển thị danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = BusinessLayer.ComomDataService.ListOfCustomer(page,pageSize,searchValue,out rowCount);

            Models.CustomerPaginationResultModel model = new Models.CustomerPaginationResultModel
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data
                
            };
            return View(model);
            //int pageCount = rowCount / pageSize;
            //if(rowCount % 2 > 0)
            //{
            //    pageCount += 1;
            //}
            //ViewBag.RowCount = rowCount;
            //ViewBag.PageCount = pageCount;
            //ViewBag.Page = page;
            //ViewBag.SearchValue = searchValue;
            //return View(model);
        }
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng mới";
            Customer model = new Customer()
            {
                CustomerID = 0

            };


            return View(model);
        }
        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>

        [Route("edit/{customerID}")]
        public ActionResult Edit(int customerID,string abc)
        {
            ViewBag.Title = "Cập nhật khách hàng mới";
            var model = BusinessLayer.ComomDataService.GetCustomer(customerID);
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            return View("Create",model);
        }

        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult Save(Customer model)
        {
            if(model.CustomerID > 0 )
            {
                BusinessLayer.ComomDataService.UpdateCustomer(model);
                return RedirectToAction("Index");
            }
            else
            {
                BusinessLayer.ComomDataService.AddCustomer(model);
                return RedirectToAction("Index");
            }
            
        }

      
        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        /// 

        [Route("delete/{customerID}")]
        public ActionResult Delete(int customerID)
        {
            if(Request.HttpMethod == "POST")
            {
                BusinessLayer.ComomDataService.DeleteCustomer(customerID);
                return RedirectToAction("Index");
            }
            var model = BusinessLayer.ComomDataService.GetCustomer(customerID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

    }
}
