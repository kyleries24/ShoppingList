using System;
using System.Web.Mvc;
using ShoppingListApp.Models;
using ShoppingListApp.Services;
using Microsoft.AspNet.Identity;
using ShoppingListApp.Data;

namespace ShoppingList.Web.Controllers
{
    public class ShoppingListItemAppController : Controller
    {
        private readonly Lazy<ShoppingListItemAppService> _svc;

        public ShoppingListItemAppController()
        {
            _svc =
                new Lazy<ShoppingListItemAppService>(
                    () =>
                    {
                        return new ShoppingListItemAppService();
                    });
        }

        public ActionResult Index(int id)
        {
            var ShoppingListItems = _svc.Value.GetItems(id);

            return View(ShoppingListItems);
        }

        public ActionResult Create()
        {
            try
            {
                var vm = new ShoppingListItemCreateModel();

                return View(vm);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument Exception. Redirecting to Shopping List.");
                return RedirectToAction("Index", "ShoppingListApp", null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingListItemCreateModel vm, int Id)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.CreateItem(vm, Id))
            {
                ModelState.AddModelError("", "Unable to create note");
                return View(vm);
            }

            return RedirectToAction("Index", new { Id = Url.RequestContext.RouteData.Values["id"] });
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int Id, int ShoppingListId)
        {
            try
            {
                var detail = _svc.Value.GetItemById(Id, ShoppingListId);

                return View(detail);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument Exception. Redirecting to Shopping List.");
                return RedirectToAction("Index","ShoppingListApp",null);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int Id, int ShoppingListId)
        {
            _svc.Value.DeleteItem(Id, ShoppingListId);

            return RedirectToAction("Index", new { Id = Url.RequestContext.RouteData.Values["id"] });
        }

        public ActionResult DeleteAllItems()
        {
            _svc.Value.DeleteAllItems();

            return RedirectToAction("Index","ShoppingListApp");
        }
    }
}
