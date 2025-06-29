using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Controle_de_Gastos
{
    class Program
    {
        static List<Gastos> gastos = new List<Gastos>();
        static string caminhoArquivo = "gastos.json";

        static void Main(string[] args)
        {
            CarregarGastos();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Controle de Gastos ===");
                Console.WriteLine("1. Adicionar Gasto");
                Console.WriteLine("2. Listar Gastos");
                Console.WriteLine("3. Mostrar Total de Gastos");
                Console.WriteLine("4. Remover Gasto");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarGastos();
                        break;
                    case "2":
                        ListarGastos();
                        break;
                    case "3":
                        MostrarTotal();
                        break;
                    case "4":
                        RemoverGastos();
                        break;
                    case "0":
                        SalvarGastos();
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("Pressione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        static void AdicionarGastos()
        {
            Console.Clear();
            Console.WriteLine("=== Novo Gasto ===");

            Console.Write("Categoria: ");
            string categoria = Console.ReadLine();

            Console.Write("Valor R$: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal valor))
            {
                gastos.Add(new Gastos(categoria, valor));
                SalvarGastos();
                Console.WriteLine("Gasto adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine("Valor inválido.");
            }
        }

        static void ListarGastos()
        {
            Console.Clear();
            if (gastos.Count == 0)
            {
                Console.WriteLine("Nenhum gasto registrado.");
                return;
            }

            Console.WriteLine("=== Lista de Gastos ===");
            for (int i = 0; i < gastos.Count; i++)
            {
                Console.Write($"#{i + 1} - ");
                gastos[i].Exibir();
            }
        }

        static void MostrarTotal()
        {
            Console.Clear();
            decimal total = 0;
            foreach (var g in gastos)
            {
                total += g.Valor;
            }

            Console.WriteLine($"Total de gastos: R$ {total:F2}");
        }

        static void RemoverGastos()
        {
            ListarGastos();

            Console.Write("Digite o número do gasto para remover: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= gastos.Count)
            {
                gastos.RemoveAt(indice - 1);
                SalvarGastos();
                Console.WriteLine("Gasto removido com sucesso.");
            }
            else
            {
                Console.WriteLine("Número inválido.");
            }
        }

        static void CarregarGastos()
        {
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    gastos = JsonSerializer.Deserialize<List<Gastos>>(json);
                }
            }
        }

        static void SalvarGastos()
        {
            string json = JsonSerializer.Serialize(gastos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminhoArquivo, json);
        }
    }
}
