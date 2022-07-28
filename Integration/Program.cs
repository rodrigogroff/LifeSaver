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
                Console.WriteLine("[1] CLEAN DATABASE");                
                Console.WriteLine("[2] localhost (padrao)");
                Console.WriteLine("[3] homolog");
                Console.WriteLine("(select)");

                bool bAbort = true;

                switch (Console.ReadLine())
                {
                    case "1":
                        baseUri = @"http://localhost:18524/";
                        CleanDB();
                        bAbort = false;
                        break;

                    case "2":
                    case "":
                        baseUri = @"http://localhost:18524/";
                        bAbort = true;
                        break;

                    case "3":
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
                Console.WriteLine("Interactive Test Modules");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("[1] Authorization");
                Console.WriteLine("[2] Configuration");
                Console.WriteLine("[3] Entries");
                Console.WriteLine("(select)");
                
                switch (Console.ReadLine())
                {
                    case "1":

                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Authorization Module options:");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("");
                            Console.WriteLine("[0] << Back");
                            Console.WriteLine("[1] Registration");
                            Console.WriteLine("[2] User List");
                            Console.WriteLine("(select)");

                            bool bAbort = false;

                            switch (Console.ReadLine())
                            {
                                case "0": bAbort = true; break;
                                case "1": Registration(); break;
                                case "2": UserListing(); break;                                
                            }

                            if (bAbort)
                                break;
                        }                        
                        break;

                    case "2":
                        Login();

                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Config Module options:");
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

                    case "3":

                        Login();

                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Entries Module options:");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("");
                            Console.WriteLine("[0] << Back");
                            Console.WriteLine("[1] ItemDrop Add");
                            Console.WriteLine("[2] ItemDrop List");
                            Console.WriteLine("(select)");

                            bool bAbort = false;

                            switch (Console.ReadLine())
                            {
                                case "0": bAbort = true; break;
                                case "1": ItemDropAdd(); break;
                                case "2": ItemDropListing(); break;
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

                Console.WriteLine("Digite o email:");

                var email = Console.ReadLine();

                if (email == "")
                {
                    email = "teste@teste.com";                    
                }
                else
                    email = "email" + new Random().Next(1000000, 9999999).ToString() + "_@teste.com";

                Console.WriteLine("[" + email + "]");

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

        static void UserListing()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/auth/magic_user_list";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                request.AddBody(new
                {
                    magic = "142536"
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== MAGIC_USER_LIST".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED MAGIC_USER_LIST");
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

                var _name = Console.ReadLine();

                Console.WriteLine("Incoming [f]:");

                var _inc = Console.ReadLine().ToLower();

                request.AddBody(new
                {
                    name = _name,
                    income = _inc == "t",
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

        #endregion

        #region - item -

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

        #endregion

        #endregion

        #region - entries -

        static void ItemDropAdd()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/entries/itemdrop_add";

                var client = new RestClient(dest);
                var request = new RestRequest();

                request.AddHeader("Content-Type", "application/json");

                client.AddDefaultHeader("Authorization", "Bearer " + token);

                request.RequestFormat = DataFormat.Json;
                request.Method = Method.POST;

                Console.WriteLine("New item day:");

                var _day = Console.ReadLine();

                if (_day == "")
                {
                    _day = DateTime.Now.Day.ToString();
                    Console.WriteLine("[" + _day + "]");
                }

                var day = Convert.ToInt64(_day);

                Console.WriteLine("New item month:");

                var _month = Console.ReadLine();

                if (_month == "")
                {
                    _month = DateTime.Now.Month.ToString();
                    Console.WriteLine("[" + _month + "]");
                }

                var month = Convert.ToInt64(_month);

                Console.WriteLine("New item year:");

                var _year = Console.ReadLine();

                if (_year == "")
                {
                    _year = DateTime.Now.Year.ToString();
                    Console.WriteLine("[" + _year + "]");
                }

                var year = Convert.ToInt64(_year);

                Console.WriteLine("New item folder id:");

                var fkFolder = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("New item id:");

                var fkItem = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Cents:");

                var cents = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Installments:");

                var i = Console.ReadLine();

                if (i == "")
                {
                    Console.WriteLine("[1]");
                    i = "1";
                }

                var installments = Convert.ToInt64(i);

                request.AddBody(new
                {
                    fkFolder,
                    fkItem,                    
                    cents,
                    installments,
                    day,
                    month,
                    year
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== ITEMDROP_ADD".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(response.Content.ToString());
                    Console.WriteLine(" # FAILED ITEMDROP_ADD");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        static void ItemDropListing()
        {
            #region - code - 

            try
            {
                var dest = baseUri + @"api/v1/entries/itemdrop_list";

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

                Console.WriteLine("item id:");

                var inputItem = Console.ReadLine();
                long? _item = null;

                if (inputItem != "")
                    _item = Convert.ToInt64(input);

                Console.WriteLine("day:");

                var _day = Console.ReadLine();

                if (_day == "")
                {
                    _day = DateTime.Now.Day.ToString();
                    Console.WriteLine("[" + _day + "]");
                }

                var day = Convert.ToInt64(_day);

                Console.WriteLine("month:");

                var _month = Console.ReadLine();

                if (_month == "")
                {
                    _month = DateTime.Now.Month.ToString();
                    Console.WriteLine("[" + _month + "]");
                }

                var month = Convert.ToInt64(_month);

                Console.WriteLine("year:");

                var _year = Console.ReadLine();

                if (_year == "")
                {
                    _year = DateTime.Now.Year.ToString();
                    Console.WriteLine("[" + _year + "]");
                }

                var year = Convert.ToInt64(_year);

                request.AddBody(new
                {
                    fkFolder = folder,
                    fkItem = _item,
                    day,
                    month,
                    year
                });

                var response = client.Execute(request);

                Console.WriteLine(response.Content.ToString());

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(" ==== ITEMDROP_LIST".PadRight(30, ' ') + "OK");
                }
                else
                {
                    Console.WriteLine(" # FAILED ITEMDROP_LIST");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
        }

        #endregion
    }
}
