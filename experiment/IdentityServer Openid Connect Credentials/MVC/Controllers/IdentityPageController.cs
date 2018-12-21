using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class IdentityPageController : Controller
    {
        // GET: IdentityPage
        public ActionResult Index()
        {
            return View();
        }

        // GET: IdentityPage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IdentityPage/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }


        [Authorize]
        public async Task LoginOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }


        // POST: IdentityPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IdentityPage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IdentityPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IdentityPage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IdentityPage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}