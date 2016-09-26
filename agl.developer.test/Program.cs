using agl.developer.test.parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace agl.developer.test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WebClient webClient = new WebClient();
                AglPetOwnerParser parser = new AglPetOwnerParser("cat");
                parser.ParseAndPrint(webClient.DownloadString("http://agl-developer-test.azurewebsites.net/people.json"));
                Console.ReadLine();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}