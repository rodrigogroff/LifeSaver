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
        public static string baseUri = "", token = "";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Startup options:");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("0 CLEAN DATABASE");
                Console.WriteLine("1 QA");
                Console.WriteLine("2 localhost (padrao)");
                Console.WriteLine("(select)");

                bool bAbort = true;

                switch (Console.ReadLine())
                {
                    case "0":
                        baseUri = @"http://localhost:18524/";
                        CleanDB();
                        bAbort = false;
                        break;

                    case "2": case "":
                        baseUri = @"http://localhost:18524/";
                        bAbort = true;
                        break;

                    case "1":
                        baseUri = @"http://localhost:18524/";
                        bAbort = true;
                        break;
                }

                if (bAbort)
                    break;
            }

            Login();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Test Module options:");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("1 Registration");
                Console.WriteLine("2 Config");
                Console.WriteLine("(select)");
                
                switch (Console.ReadLine())
                {
                    case "1": 
                        Registration(); 
                        break;

                    case "2":
                        Login();

                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Config Module option:");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("");
                            Console.WriteLine("[0] << Back");
                            Console.WriteLine("[1] Folder Add             [5] Item Add");
                            Console.WriteLine("[2] Folder Edit            [6] Item Edit");
                            Console.WriteLine("[3] Folder get             [7] Item Get");
                            Console.WriteLine("[4] Folder listing         [8] Item List");
                            Console.WriteLine("(select)");
                            
                            bool bAbort = false;

                            switch (Console.ReadLine())
                            {
                                case "0": bAbort = true; break;
                                case "1": FolderAdd(); break;
                                case "2": FolderEdit(); break;
                                case "3": FolderGet(); break;
                                case "4": FolderListing(); break;
                                case "5": ItemAdd(); break;
                                case "6": ItemEdit(); break;
                                case "7": ItemGet(); break;
                                case "8": ItemListing(); break;
                            }

                            if (bAbort)
                                break;
                        }

                        break;
                }
            }
        }

        #region - authorization -

        static void Registration()
        {
            #region - code - 

            while (true)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Digite o celular:");

                var mobile = Console.ReadLine();

                if (mobile == "")
                {
                    mobile = "12345678901";
                    Console.WriteLine("[" + mobile + "]");
                }

                #region - code - 

                try
                {
                    var dest = baseUri + @"api/v1/auth/register";

                    var client = new RestClient(dest);
                    var request = new RestRequest();

                    request.AddHeader("Content-Type", "application/json");
                    request.RequestFormat = DataFormat.Json;
                    request.Method = Method.POST;

                    var email = "email" + new Random().Next(1000000, 9999999).ToString() + "_@teste.com";

                    if (mobile == "12345678901")
                    {
                        email = "teste@teste.com";
                    }

                    request.AddBody(new
                    {
                        name = "tst_" + new Random().Next(1000000, 9999999).ToString(),
                        email = email,
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
                        continue;
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                #endregion

                Console.WriteLine("--------------------");
                Console.WriteLine("Codigo:");

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

                Console.WriteLine("");
                Console.WriteLine("Continuar registrando?");

                var u = Console.ReadLine();

                if (u.ToLower() == "n")
                    break;
            }

            #endregion
        }

        static void Login()
        {
            #region - code - 

            try
            {
                Console.WriteLine("Logging...");
                    
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
                    Console.WriteLine(" # FAILED LOGIN (register first!)");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        static void CleanDB()
        {
            #region - code - 

            Console.WriteLine("Confirm?");

            if (Console.ReadLine().ToLower() != "y")
                return;

            try
            {
                var dest = baseUri + @"api/v1/auth/magic_clean_db";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    magic = "142536"
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== CLEANDB ".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED CLEANDB");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        #endregion

        #region - configuration -

        #region - folder - 

        static void FolderListing()
        {
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

                Console.WriteLine("Folder id: (empty for null)");

                var input = Console.ReadLine();
                long? folder = null;
                
                if (input != "") 
                    folder = Convert.ToInt64(input);

                request.AddBody(new
                {
                    fkFolder = folder
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

        static void FolderGet()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/folder_get";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                Console.WriteLine("Entre o ID do folder:");

                request.AddBody(new
                {
                    id = Convert.ToInt64(Console.ReadLine()),
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== FOLDER_GET".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED FOLDER_GET");
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

                Console.WriteLine("New folder name:");

                request.AddBody(new
                {
                    name = Console.ReadLine(),
                    income = false,
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

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
        }

        static void FolderEdit()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/folder_edit";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                Console.WriteLine("Folder id:");

                var id = Console.ReadLine();

                Console.WriteLine("New name:");

                request.AddBody(new
                {
                    id,
                    new_name = Console.ReadLine()
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== FOLDER_EDIT".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(response.Content.ToString());
                    Console.WriteLine(" # FAILED FOLDER_EDIT");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        #endregion

        #region - item -

        static void ItemListing()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/item_list";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                Console.WriteLine("Folder id:");

                var input = Console.ReadLine();
                long? folder = null;

                if (input != "")
                    folder = Convert.ToInt64(input);

                request.AddBody(new
                {
                    fkFolder = folder
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== ITEM_LIST".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED ITEM_LIST");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        static void ItemGet()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/item_get";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                Console.WriteLine("Entre o ID do item:");

                request.AddBody(new
                {
                    id = Convert.ToInt64(Console.ReadLine()),
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== ITEM_GET".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED ITEM_GET");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        static void ItemAdd()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/item_add";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                Console.WriteLine("New item folder id:");

                var fkFolder = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("New item name:");

                var name = Console.ReadLine();

                Console.WriteLine("New item time period:");

                var period = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("New item cents:");

                var cents = Convert.ToInt64(Console.ReadLine());

                request.AddBody(new
                {
                    fkFolder = fkFolder,
                    name = name,
                    timePeriod = period,
                    standardValue = cents,
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== ITEM_ADD".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(response.Content.ToString());
                    Console.WriteLine(" # FAILED ITEM_ADD");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        static void ItemEdit()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/config/item_edit";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                Console.WriteLine("Item id:");

                var id = Console.ReadLine();

                Console.WriteLine("New item name:");

                request.AddBody(new
                {
                    id,
                    new_name = Console.ReadLine()
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== ITEM_EDIT".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(response.Content.ToString());
                    Console.WriteLine(" # FAILED ITEM_EDIT");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        #endregion

        #endregion
    }
}
