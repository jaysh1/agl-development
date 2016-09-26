using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agl.developer.test.parser
{
    public class AglPetOwnerParser
    {
        #region implementation 1

        private string Pet { get; set; }

        public StringBuilder Result;
        public AglPetOwnerParser(string pet)
        {
            Result = new StringBuilder();
            Pet = pet;
        }
        /// <summary>
        /// Parses and prints output
        /// </summary>
        public bool ParseAndPrint(string json)
        {
            try
            {
                List<PetOwner> petOwners = JsonConvert.DeserializeObject<List<PetOwner>>(json);
                List<JToken> petNames = new List<JToken>();

                var query = from ownerGroup in petOwners
                            where ownerGroup.Pets != null
                            group ownerGroup by ownerGroup.Gender into genderGroups
                            select new
                            {
                                Gender = genderGroups.Key,
                                Pets = from pg in genderGroups
                                       select new { cats = pg.Pets.Where(c => c.Type.ToLowerInvariant() == Pet.ToLower()).OrderBy(c => c.Name).ToList() }.cats.ToList()
                            };

                foreach (var grouping in query)
                {
                    Result.AppendLine(grouping.Gender);
                    foreach (var cats in grouping.Pets)
                    {
                        foreach (var cat in cats)
                        {
                            Result.AppendFormat("\t{0}", cat.Name);
                            Result.AppendLine();
                        }
                    }
                }
                Print();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new ArgumentException() { Source = "Error parsing json string" };
            }
            return true;
        }

        private void Print()
        {
            Console.WriteLine(Result.ToString());
        }
        #endregion

        #region implementation 2
        private static Dictionary<string, List<string>> _petsByOwner = new Dictionary<string, List<string>>();
        private static void Build(string json)
        {
            JArray result = JsonConvert.DeserializeObject<JArray>(json);
            List<JToken> petNames = new List<JToken>();

            foreach (var grouping in result.ToList().GroupBy(g => g["gender"]))
            {
                string key = grouping.Key.ToString();
                if (!_petsByOwner.ContainsKey(key))
                {
                    _petsByOwner.Add(key, new List<string>());
                }

                foreach (var pets in grouping.Select(x => x["pets"]).ToList())
                {
                    foreach (var pet in pets)
                    {
                        if (pet.Value<string>("type").ToLower() == "cat")
                        {
                            petNames.Add(pet.Value<string>("name"));
                        }
                    }
                }

                petNames.Sort();
                foreach (string pName in petNames)
                {
                    _petsByOwner[key].Add(pName);
                }
                petNames.Clear();
            }
        }

        private static void Display()
        {
            foreach (var op in _petsByOwner)
            {
                Console.WriteLine(string.Format("{0}", op.Key));

                foreach (string s in op.Value)
                {
                    Console.WriteLine(string.Format("\t {0}", s));
                }
            }
        }
        #endregion
    }
}
