using RestSharp;
using System;

namespace Integration
{
    internal class Program
    {
        public static string baseUri = @"https://instalacao-qa.br.tkelevator.com/API/",
                             token = "";

        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("Target:");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1 QA");
            Console.WriteLine("2 localhost");

            switch (Console.ReadLine())
            {
                case "2": baseUri = @"http://localhost:18524/"; break;
            }

            Console.WriteLine("--------------------");
            Console.WriteLine("Digite o celular:");

            var mobile = Console.ReadLine();

            try
            {
                var dest = baseUri + @"api/v1/auth/register";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    name = "tst_" + new Random().Next(1000000, 9999999).ToString(),
                    email = "email" + new Random().Next(1000000, 9999999).ToString() + "_@teste.com",
                    mobile = mobile, 
                    password = "1425369"
                });

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== REGISTER ".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(response.Content);
                    Console.WriteLine(" # FAILED REGISTER");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("--------------------");
            Console.WriteLine("Digite o codigo recebido:");

            try
            {
                var dest = baseUri + @"api/v1/auth/register_confirm";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    mobile = mobile,
                    code = Console.ReadLine(),
                });

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== REGISTER CONFIRM ".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(response.Content);
                    Console.WriteLine(" # FAILED REGISTER CONFIRM");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
