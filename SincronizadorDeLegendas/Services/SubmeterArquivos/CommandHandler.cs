using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SincronizadorDeLegendas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorDeLegendas.Services
{
    public partial class SubmeterArquivos
    {
        public class CommandHandler
        {
            public Arquivos Handle(Command command)
            {
                StreamReader file = new StreamReader(command.Arquivo.OpenReadStream());
                StringBuilder saida = new StringBuilder();

                String line = file.ReadLine();
                for (int i = 0; line != null; i++, line = file.ReadLine())
                {
                    if (line.Contains("-->"))
                    {
                        int horas = int.Parse(line.Substring(0, 2));
                        int minutos = int.Parse(line.Substring(3, 2));
                        int segundos = int.Parse(line.Substring(6, 2));
                        int milisegundos = int.Parse(line.Substring(9, 3));

                        int fimHoras = int.Parse(line.Substring(17, 2));
                        int fimMinutos = int.Parse(line.Substring(20, 2));
                        int fimSegundos = int.Parse(line.Substring(23, 2));
                        int fimMilisegundos = int.Parse(line.Substring(26, 3));

                        saida.Append(processarHoras(horas,minutos,segundos,milisegundos,command) + " --> " + processarHoras(fimHoras, fimMinutos, fimSegundos, fimMilisegundos, command) + "\n");
                    }
                    else
                    {
                        saida.Append(line + "\n");
                    }
                }
                file.Close();

                byte[] fileBytes = Encoding.ASCII.GetBytes(saida.ToString());

                return new Arquivos(fileBytes, command.Arquivo.FileName);
            }

            String processarHoras(int horas, int minutos, int segundos, int milisegundos, Command command)
            {
                double total;
                if (command.Offset == '+')
                    total = ((command.Horas + horas) * 3600) + ((command.Minutos + minutos) * 60) + (command.Segundos + segundos) + (((double)command.Milisegundos + (double)milisegundos) / 1000);
                else
                    total = ((horas - command.Horas) * 3600) + ((minutos - command.Minutos) * 60) + (segundos - command.Segundos) + (((double)milisegundos - (double)command.Milisegundos) / 1000);
                
                //Zera caso seja um número negativo, pode trazer problemas conter um tempo negativo
                if(total < 0)
                    total = 0;

                int horaFinal = ((int)total / 3600);
                total -= horaFinal * 3600;
                int minutoFinal = ((int)total / 60);
                total -= minutoFinal * 60;
                int segundoFinal = (int)total;
                total -= segundoFinal;
                int milisegundoFinal = (int)(total * 1000);

                return horaFinal.ToString("D2") + ":" + minutoFinal.ToString("D2") + ":" + segundoFinal.ToString("D2") + "," + milisegundoFinal.ToString("D3");
            }
        }
    }
}
