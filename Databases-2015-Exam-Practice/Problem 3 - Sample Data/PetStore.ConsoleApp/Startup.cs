using PetStore.Importer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp
{
    public class Startup
    {
        public static void Main()
        {
            var importer = PetStoreImporter.CreateInstance();

            importer.Import();
        }
    }
}
