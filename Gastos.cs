using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controle_de_Gastos
{
    public class Gastos
    {
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public Gastos(string categoria, decimal valor)
        {
            Categoria = categoria;
            Valor = valor;
            Data = DateTime.Now;
        }

        public void Exibir()
        {
            Console.WriteLine($"{Data:dd/MM/yyyy HH:mm} - {Categoria}: R$ {Valor:F2}");
        }
    }
}