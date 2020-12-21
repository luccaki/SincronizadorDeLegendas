using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SincronizadorDeLegendas.Models;
using SincronizadorDeLegendas.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SincronizadorDeLegendas.Controllers
{
    public record Entrada(
        [Required(ErrorMessage = "Necessário inserir um arquivo .srt")] IFormFile Arquivos,
        [Required(ErrorMessage = "Necessário inserir um offset positivo ou negativo")] char Offset,
        [Required(ErrorMessage = "Necessário escolher as horas de Offset"), Range(0, 999, ErrorMessage = "Horas apenas entre 0 e 999")] int Horas,
        [Required(ErrorMessage = "Necessário escolher os minutos de Offset"), Range(0, 59, ErrorMessage = "Minutos apenas entre 0 e 59")] int Minutos,
        [Required(ErrorMessage = "Necessário escolher os segundos de Offset"), Range(0, 59, ErrorMessage = "Segundos apenas entre 0 e 59")] int Segundos,
        [Required(ErrorMessage = "Necessário escolher os milisegundos de Offset"), Range(0, 999, ErrorMessage = "Milisegundos apenas entre .0 e .999")] int Milisegundos
    );
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context Db;

        public HomeController(ILogger<HomeController> logger, Context db)
        {
            _logger = logger;
            Db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(
            Entrada entrada,
            SubmeterArquivos.CommandHandler handler)
        {
            Arquivos saida = handler.Handle(new SubmeterArquivos.Command
            {
                Arquivo = entrada.Arquivos,
                Offset = entrada.Offset,
                Horas = entrada.Horas,
                Minutos = entrada.Minutos,
                Segundos = entrada.Segundos,
                Milisegundos = entrada.Milisegundos
            });
            Db.Add(saida);
            Db.SaveChanges();
            return File(saida.Conteudo, entrada.Arquivos.ContentType, saida.Nome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
