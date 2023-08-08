using Aula1AspNetMvc.Context;
using Aula1AspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula1AspNetMvc.Controllers
{
    public class ClienteOldController : Controller
    {
        // GET: Cliente
        public ActionResult Teste()
        {
            var cliente = new Aula1Context().Cliente.SingleOrDefault(c => c.Id == 1);

            ViewBag.Cliente = "Texto";
            //ViewData["Cliente"] = cliente;

            return View("Index",cliente);
        }

        public ActionResult Lista()
        {
            var listaClientes = new List<Cliente>()
            {
                new Cliente(){ Nome = "João", SobreNome = "Pedro", DataCadastro = DateTime.Now, Id = 1 },
                new Cliente(){ Nome = "Fulano", SobreNome = "Beltrano", DataCadastro = DateTime.Now, Id = 2 },
            };

            return View(listaClientes);
        }

        public ActionResult Pesquisa(int? id, string nome)
        {
            var listaClientes = new List<Cliente>()
            {
                new Cliente(){ Nome = "João", SobreNome = "Pedro", DataCadastro = DateTime.Now, Id = 1 },
                new Cliente(){ Nome = "Fulano", SobreNome = "Beltrano", DataCadastro = DateTime.Now, Id = 2 },
                new Cliente(){ Nome = "Eduardo", SobreNome = "Pires", DataCadastro = DateTime.Now, Id = 3 },
                new Cliente(){ Nome = "Dayane", SobreNome = "Lima", DataCadastro = DateTime.Now, Id = 4 },
                new Cliente(){ Nome = "Leonardo", SobreNome = "Carmonio", DataCadastro = DateTime.Now, Id = 5 }
            };

            var cliente = listaClientes.Where(c => c.Id == id).ToList();

            if (!cliente.Any())
            {
                TempData["erro"] = "Nenhum cliente selecionado";
                return RedirectToAction("ErroPesquisa");
            }

            return View("Lista", cliente);
        }

        public ActionResult ErroPesquisa()
        {
            return View("Error");
        }
    }
}