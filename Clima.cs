using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Weather
{
    public class Clima
    {
        public double queryCost { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string resolvedAddress { get; set; }
        public string address { get; set; }
        public string timeszone { get; set; }
        public double tzoffset { get; set; }
        public string description { get; set; }
        public CurrentConditions currentConditions { get; set; }
        public List<CurrentConditions> days { get; set; }

        public static async Task<Clima> GetClima()
        {
            Console.Write("Digite a cidade que deseja saber o clima: ");
            string cidade = Console.ReadLine();
            Console.WriteLine();
            //Criar o cliente HTTP
            var cliente = new HttpClient();
            //Fazer a requisição
            var resposta = await cliente.GetAsync($"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/${cidade}?unitGroup=metric&key=5FQDS36YRY7PBV98ZGJ9GMYGX&contentType=json");
            //Ler os dados em JSON
            var json = await resposta.Content.ReadAsStringAsync();
            //Deserializar o JSON cria um objeto da classe <Tal> e o retorna com os dados do JSON
            Clima clima = JsonSerializer.Deserialize<Clima>(json);
            return clima;
        }
        public void MostrarTempoAtual()
        {
            currentConditions.InfoClima();
        }
        public void MostrarPrevisaoTempo()
        {
            //para cada dia do clima mostrar as mesmas informações
            foreach (CurrentConditions dia in days)
            {
                dia.InfoClima();
            }
        }
        public void MostrarPrevisaoDiaSeguinte()
        {
            if (days.Count > 1)
            {
                int indiceDiaAtual = days.FindIndex(d => d == days.First());
                if (indiceDiaAtual < days.Count - 1)
                {
                    CurrentConditions diaSeguinte = days[indiceDiaAtual + 1];
                    diaSeguinte.InfoClima();
                }
            }
        }
        public void PerguntarOQueQuerVer()
        {
            Console.WriteLine("O que você deseja ver?");
            Console.WriteLine("1- Tempo atual");
            Console.WriteLine("2- Previsão de amanhã");
            Console.WriteLine("3- Previsão de 15 dias");
            Console.Write("Digite a opção: ");
            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 1)
            {
                MostrarTempoAtual();
                Console.Write("Deseja ver a previsão de 15 dias? (s/n) : ");
                if (Console.ReadLine() == "s")
                {
                    MostrarPrevisaoTempo();
                }
            }
            if (opcao == 2)
            {
                MostrarPrevisaoDiaSeguinte();
                Console.Write("Deseja ver a previsão de 15 dias? (s/n) : ");
                if (Console.ReadLine() == "s")
                {
                    MostrarPrevisaoTempo();
                }
            }
            if (opcao == 3)
            {
                MostrarPrevisaoTempo();
            }
        }
    }
    public class CurrentConditions
    {
        public string datetime { get; set; }
        public double temp { get; set; }
        public double? tempmax { get; set; }
        public double? tempmin { get; set; }
        public double feelslike { get; set; }
        public double humidity { get; set; }
        public double dew { get; set; }
        public double precip { get; set; }
        public double precipprob { get; set; }
        public double snow { get; set; }
        public double? windgust { get; set; }
        public double? windspeed { get; set; }
        public double? winddir { get; set; }
        public double? pressure { get; set; }
        public double? visibility { get; set; }
        public double? cloudcover { get; set; }
        public double? solarradiation { get; set; }
        public double? solarenergy { get; set; }
        public double? uvindex { get; set; }
        public string? conditions { get; set; }
        public string? icon { get; set; }
        public List<string>? stations { get; set; }
        public string? source { get; set; }
        public string? sunrise { get; set; }
        public double? sunriseEpoch { get; set; }
        public string? sunset { get; set; }
        public double? sunsetEpoch { get; set; }
        public double? moonphase { get; set; }
        public void InfoClima()
        {
            Console.WriteLine();
            Console.WriteLine($"{datetime}");
            Console.WriteLine($"Temperatura: {temp}ºC");
            Console.WriteLine($"Sensação Térmica: {feelslike}ºC");
            Console.WriteLine($"Velocidade do vento: {windspeed}");
            Console.WriteLine($"Visibilidade: {visibility}");
            Console.WriteLine($"Nascer do Sol: {sunrise}");
            Console.WriteLine($"Pôr do Sol: {sunset}");
            Console.WriteLine($"Condições do tempo: {conditions}");
            Console.WriteLine($"Umidade relativa do ar: {humidity}");
            Console.WriteLine($"Chance de precipitação: {precipprob}");
            if (precipprob > 60.0)
            {
                Console.WriteLine("Será uma boa ideia levar um guarda-chuva!");
            }
            else
            {
                Console.WriteLine("Não precisa carregar o peso do guarda-chuva");
            }
            Console.WriteLine();
        }
    }
}
