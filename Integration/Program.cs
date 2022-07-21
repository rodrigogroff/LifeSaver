using Newtonsoft.Json;
using RestSharp;
using System;

namespace Integration
{
    public class LoginResp
    {
        public string token { get; set; }
    }

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

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Module:");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1 Auth");
                Console.WriteLine("2 Config");

                switch (Console.ReadLine())
                {
                    case "1": Registration(); break;

                    case "2":

                        Console.WriteLine("");
                        Console.WriteLine("Config Module:");
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("0 [Back]");
                        Console.WriteLine("1 Folder Add");

                        switch (Console.ReadLine())
                        {
                            case "0": break;
                            case "1": FolderAdd(); break;
                        }

                        break;
                }
            }
        }

        static void Registration()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Digite o celular:");

            var mobile = Console.ReadLine();

            #region - code - 

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

                Console.WriteLine(response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== REGISTER ".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED REGISTER");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion

            Console.WriteLine("--------------------");
            Console.WriteLine("CODIGO:");

            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/auth/magic_sms_list";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    mobile = mobile,
                    magic = "142536"
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion

            Console.WriteLine("--------------------");
            Console.WriteLine("Digite o codigo recebido:");

            #region - code - 

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

            #endregion

            Console.ReadLine();
        }

        static void Login()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/auth/login";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    email = "teste@teste.com",
                    password = "1425369"
                });

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== LOGIN ".PadRight(30, ' ') + "OK");

                    var resp = JsonConvert.DeserializeObject<LoginResp>(response.Content);

                    token = resp.token;
                }
                else
                {
                    Console.WriteLine(" # FAILED LOGIN");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        static void FolderAdd()
        {
            Login();

            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/folder_add";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    name = "Contas a pagar",
                    income = false,
                });

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== FOLDER_ADD".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(response.Content.ToString());
                    Console.WriteLine(" # FAILED FOLDER_ADD");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion

            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/folder_list";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== FOLDER_LIST".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED FOLDER_LIST");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }
    }
}
