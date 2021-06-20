using ModelEF.Dao;
using ModelEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;
using PagedList;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserAccountController : BaseController
    {
        // GET: Admin/UserAccount
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var userAccount = new UserDao().ViewDetail(id);
            return View(userAccount);
        }
        [HttpPost]
        public ActionResult Create(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMd5Pas = Encryptor.MD5Hash(userAccount.Password);
                userAccount.Password = encryptedMd5Pas;
                long id = dao.Insert(userAccount);
                if (id > 0)
                {
                    return RedirectToAction("Index", "UserAccount");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm User thành công");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(userAccount.Password))
                {
                    var encrytedMd5Pas = Encryptor.MD5Hash(userAccount.Password);
                    userAccount.Password = encrytedMd5Pas;
                }


                var result = dao.Update(userAccount);
                if (result)
                {
                    return RedirectToAction("Index", "UserAccount");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật User thành thông");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}