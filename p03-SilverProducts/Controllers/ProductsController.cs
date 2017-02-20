using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using p03_SilverProducts.Models;

namespace p03_SilverProducts.Controllers
{
    public class ProductsController : Controller
    {
        private SilverProductEntities db = new SilverProductEntities();
        private string searchstring;

        // GET: Products

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult Index(string items)
        {
            var itemlist = new List<string>();
            //linq query to get items
            var itemQuery = from d in db.Products
                            orderby d.Items
                            select d.Items;
            //add linq query
            itemlist.AddRange(itemQuery.Distinct());
            ViewBag.items = new SelectList(itemlist);

            var products = from i in db.Products
                           select i;

            if (!string.IsNullOrEmpty(searchstring))
            {
                products = products.Where(s => s.Items.Contains(searchstring));
            }

            if (!string.IsNullOrEmpty(items))
            {
                products = products.Where(x => x.Items == items);
            }
            return View(products);
        }



        //return View(db.Products.ToList());
    

    // GET: Products/Details/5
    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            Product maxProduct = db.Products.SqlQuery("select * from Product where id = (Select max(id) from Product)").FirstOrDefault<Product>();
            int nextId = maxProduct.Id + 1;

            Product model = new Product();
            model.Id = nextId;
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Items,Period,Price,Image,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Items,Period,Price,Image,ItemNumber,ManufactureDate,Status,Manufacturer,Description,Height,Width,Depth,Weight,History")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Contact()
        {

            return View();
        }
    }
}
