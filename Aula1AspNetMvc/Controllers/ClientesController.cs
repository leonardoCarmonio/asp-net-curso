using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aula1AspNetMvc.Context;
using Aula1AspNetMvc.Models;

namespace Aula1AspNetMvc.Controllers
{
    public class ClientesController : Controller
    {
        private Aula1Context db = new Aula1Context();

        [OutputCache(Duration = 30, VaryByParam = "id")]
        // Faz um cache do request, enquanto a duração não passar, o request não é chamado
        public ActionResult Teste2(int id)
        {
            return Content(DateTime.Now.ToString());

            // #### Tipos de retornos #######
            //return Json(db.Cliente.ToList(), JsonRequestBehavior.AllowGet);
            //return JavaScript("<script>alert('oie')</script>");
            //return File()
            //return new HttpUnauthorizedResult();
        }

        [ValidateInput(false)] 
        public ActionResult Teste3()
        {
            // Recebe qualquert tipo de dado malicioso
            return Content("");
        }

        public ActionResult Teste()
        {
            ViewBag.Ola = "<h2>Ola</h2>";

            ViewBag.Id = new SelectList(db.Cliente.ToList(), "Id", "Nome", 2);

            return View(db.Cliente.ToList());
        }

        // GET: Clientes
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,SobreNome,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (!cliente.Email.Contains(".br"))
                {
                    // Através dessa função, podemos add mensagem de erro em tempo de execução
                    ModelState.AddModelError(string.Empty, "E-mail não pode ser internacional");
                    return View(cliente);
                }

                cliente.DataCadastro = DateTime.Now;
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,SobreNome,DataCadastro")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
    }
}
