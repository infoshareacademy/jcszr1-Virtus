﻿using Microsoft.AspNetCore.Mvc;
using VirtusFitWeb.Models;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConfirmOperation()
        {
            return View();
        }

        //public ActionResult OperationFailed()
        //{
        //    return View();
        //}
        public ActionResult ListUsers()
        {
            var model = _adminService.ListAllUsers();
            return View(model);
        }

        // GET: AdminController/ChangePassword/5
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: AdminController/ChangePassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel newPasswordModel)
        {
            try
            {
                _adminService.ChangePassword(newPasswordModel.Email, newPasswordModel.Password);
                return RedirectToAction(nameof(ConfirmOperation));
            }
            catch
            {
                return RedirectToAction(nameof(Index)); /*RedirectToAction(nameof(OperationFailed))*/;
            }
        }

        //// GET: AdminController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



        //// GET: AdminController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
