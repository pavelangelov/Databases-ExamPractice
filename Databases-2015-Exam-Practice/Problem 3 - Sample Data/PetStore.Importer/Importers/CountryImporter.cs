using Generator;
using PetStore.Data.Data;
using PetStore.Importer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Importer.Importers
{
    public class CountryImporter : IImporter
    {
        private readonly int NumberOfCountries = 20;

        public string Message
        {
            get
            {
                return "Importing Countries!";
            }
        }

        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Import(PetStoreDBEntities db)
        {
            var uniqueCountries = new HashSet<string>();
            while (uniqueCountries.Count < this.NumberOfCountries)
            {
                uniqueCountries.Add(RandomGenerator.RandomString(5, 50));
            }

            foreach (var country in uniqueCountries)
            {
                db.Countries.Add(new Country()
                {
                    Name = country
                });
            }

            db.SaveChanges();
        }
    }
}
