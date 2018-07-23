﻿using HomeShoppe.Common;
using Model.DAO;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using System.Web.Mvc;

namespace HomeShoppe.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        #region method
        public ActionResult Index(int page = 1, int sizePage = 10)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaging(page, sizePage);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }
        #endregion

        #region httpGet
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region httpPost
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var encrytorMD5Pass = Encrytor.MD5Hash(user.Password);
                user.Password = encrytorMD5Pass;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới user thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(User user)
       {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encrytorMD5Pass = Encrytor.MD5Hash(user.Password);
                    user.Password = encrytorMD5Pass;
                }
                
                var  result= dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user thành công");
                }
            }
            return View("Index");
        }
        #endregion

    }
}