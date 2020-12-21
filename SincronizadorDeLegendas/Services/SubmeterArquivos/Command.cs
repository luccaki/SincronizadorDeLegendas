using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SincronizadorDeLegendas.Services
{
    public partial class SubmeterArquivos
    {
        public class Command
        {
            public IFormFile Arquivo { get; set; }
            public char Offset { get; set; }
            public int Horas { get; set; }
            public int Minutos { get; set; }
            public int Segundos { get; set; }
            public int Milisegundos { get; set; }
        }
    }
}
