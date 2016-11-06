using PetStore.Importer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Importer
{
    public class PetStoreImporter
    {
        private static PetStoreImporter instance = null;

        private PetStoreImporter()
        {
        }

        public static PetStoreImporter CreateInstance()
        {
            if (instance == null)
            {
                instance = new PetStoreImporter();
            }

            return instance;
        }

        public void Import()
        {
            Assembly.GetAssembly(typeof(IImporter))
                    .GetTypes()
                    .Where(t => typeof(IImporter).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                    .Select(t => (IImporter)Activator.CreateInstance(t))
                    .OrderBy(t => t.Order)
                    .ToList()
                    .ForEach(i =>
                    {
                        Console.WriteLine(i.Message);
                        i.Import(new Data.Data.PetStoreDBEntities());
                        Console.WriteLine("Done!");
                    });
        }
    }
}
