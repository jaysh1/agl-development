using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agl.developer.test.parser
{
    [JsonObject]
    public class PetOwner
    {
        public PetOwner()
        {
            Pets = new List<Pet>();
        }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }
    }    
}
