using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SincronizadorDeLegendas.Models
{
    public class Arquivos
    {
        public int Id { get; set; }
        public byte[] Conteudo { get; set; }
        public String Nome { get; set; }
        public DateTime EnviadoEm { get; set; }
        public int QtdDownloads { get; set; }
        public Arquivos(byte[] conteudo, string nome)
        {
            Conteudo = conteudo;
            Nome = nome;
            EnviadoEm = DateTime.Now;
            QtdDownloads = 0;
        }
    }
}
