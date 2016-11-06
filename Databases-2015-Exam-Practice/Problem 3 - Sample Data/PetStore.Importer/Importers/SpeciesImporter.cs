using PetStore.Importer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetStore.Data.Data;
using Generator;

namespace PetStore.Importer.Importers
{
    public class SpeciesImporter : IImporter
    {
        private readonly int NumberOfSpecies = 100;

        public string Message
        {
            get
            {
                return "Importing Species";
            }
        }

        public int Order
        {
            get
            {
                return 2;
            }
        }

        public void Import(PetStoreDBEntities db)
        {
            var uniqueSpecies = new HashSet<string>();
            while (uniqueSpecies.Count < this.NumberOfSpecies)
            {
                uniqueSpecies.Add(RandomGenerator.RandomString(5, 50));
            }

            var countries = db.Countries.Select(x => x.Id).ToList();
            var species = uniqueSpecies.ToList();
            var currentSpecieIndex = 0;

            foreach (var countryId in countries)
            {
                var speciesForCountry = RandomGenerator.RandomNumber(2, 8);
                if (currentSpecieIndex + speciesForCountry > species.Count)
                {
                    speciesForCountry = species.Count - currentSpecieIndex;
                }
                for (int i = 0; i < speciesForCountry; i++)
                {
                    db.Species.Add(new Species()
                    {
                        Name = species[currentSpecieIndex],
                        CountryId = countryId
                    });

                    currentSpecieIndex++;
                }
            }

            if (currentSpecieIndex < species.Count)
            {
                var leftSpecies = species.Count - currentSpecieIndex;
                for (int i = 0; i < leftSpecies; i++)
                {
                    var countryIdIndex = RandomGenerator.RandomNumber(0, countries.Count - 1);
                    db.Species.Add(new Species()
                    {
                        Name = species[currentSpecieIndex],
                        CountryId = countries[countryIdIndex]
                    });

                    currentSpecieIndex++;
                }
            }

            db.SaveChanges();
        }
    }
}
