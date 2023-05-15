using System.Text.Json;
using System.Text.Json.Serialization;
using Weather;

namespace weatherRha
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int opcao = 0;
            Console.WriteLine("BEM VINDO(A)!");
            while (opcao != 2)
            {
                var clima = await Clima.GetClima();
                clima.PerguntarOQueQuerVer();
                Console.WriteLine("1-Nova pesquisa ou 2- Encerrar programa");
                Console.Write("Digite a opção: ");
                opcao = int.Parse(Console.ReadLine());
                if (opcao == 2)
                {
                    Console.WriteLine("Aperte 'enter' para fechar o programa.");
                    Console.ReadLine();
                }
            }
        }
    }
}
