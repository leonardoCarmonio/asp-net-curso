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
        public ActionResult Teste2()
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

        [HttpGet]
        public ActionResult Index() => View(db.Cliente.ToList());

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id is null)  return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"Recurso não poder ser encontrado.");

            Cliente cliente = db.Cliente.Find(id);
            if (cliente is null) return HttpNotFound("Recurso não encontrado.");

            return View(cliente);
        }

        [HttpGet]
        public ActionResult Create() => View();

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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null) return HttpNotFound("Recurso não encontrado.");

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,SobreNome")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var clienteAtualizacao = db.Cliente.Find(cliente.Id);
                if (clienteAtualizacao is null) return HttpNotFound("Recurso não encontrado.");

                clienteAtualizacao.Email = cliente.Email;
                clienteAtualizacao.Nome = cliente.Nome;
                clienteAtualizacao.SobreNome = cliente.SobreNome;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Cliente cliente = db.Cliente.Find(id);
            if (cliente is null) HttpNotFound("Recurso não encontrado.");

            return View(cliente);
        }

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
