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
        [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = BusinessLayer.ComomDataService.ListOfEmployee(page, pageSize, searchValue, out rowCount);

            Models.EmployeePaginationResultModel model = new Models.EmployeePaginationResultModel
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
        /// Thêm mới một nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên mới";
            Employee model = new Employee()
            {
                EmployeeID = 0
            };
            return View(model);
        }
        /// <summary>
        /// Sửa nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Employee model)
        {
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.LastName))
                ModelState.AddModelError("LastName", "Họ không được để trống");
            if (string.IsNullOrWhiteSpace(model.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.BirthDate))
                ModelState.AddModelError("BirthDate", "Ngày sinh không được để trống");
            if (string.IsNullOrWhiteSpace(model.Photo))
                model.Photo = "";
            if (string.IsNullOrWhiteSpace(model.Notes))
                model.Notes = "";
            if (string.IsNullOrWhiteSpace(model.Email))
                model.Email = "";
            //Nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.EmployeeID > 0)
                    ViewBag.Title = "Cập nhật thông tin nhân viên";
                else
                    ViewBag.Title = "Bổ sung thông tin nhân viên";
                return View("Create", model);
            }
            //Xử lí lưu dữ liệu vào CSDL
            if (model.EmployeeID > 0)
            {
                ComomDataService.UpdateEmployee(model);
                return RedirectToAction("Index");
            }
            else
            {
                ComomDataService.AddEmployee(model);
                return RedirectToAction("Index");
            }
        }

        [Route("edit/{employeeID}")]
        public ActionResult Edit(int employeeID, string abc)
        {
            ViewBag.Title = "Cập nhật thông tin nhân viên";
            var model = ComomDataService.GetEmployee(employeeID);
            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);
        }
        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// 

        [Route("delete/{employeeID}")]
        public ActionResult Delete(int employeeID)
        {
            ViewBag.Title = "Xóa nhân viên";
            if (Request.HttpMethod == "POST")
            {
                ComomDataService.DeleteEmployee(employeeID);
                return RedirectToAction("Index");
            }
            var model = ComomDataService.GetEmployee(employeeID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }

   
}