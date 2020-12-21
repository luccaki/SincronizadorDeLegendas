using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SincronizadorDeLegendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SincronizadorDeLegendas.Controllers
{
    public class HistoricoController : Controller
    {
        public Context db;

        public HistoricoController(Context _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            var lista = db.Arquivos.OrderByDescending(x => x.EnviadoEm).ToList();

            return View(lista);
        }

        public IActionResult Download(int id)
        {
            var Arquivo = db.Arquivos.SingleOrDefault(x => x.Id == id);
            Arquivo.QtdDownloads++;
            db.SaveChanges();

            return File(Arquivo.Conteudo, "application/octet-stream", Arquivo.Nome);
        }

        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            var Arquivo = db.Arquivos.SingleOrDefault(x => x.Id == id);
            if(Arquivo != null)
            {
                db.Arquivos.Remove(Arquivo);
                db.SaveChanges();
            }

            var lista = db.Arquivos.OrderByDescending(x => x.EnviadoEm).ToList();
            return View("Index", lista);
        }
    }
}
