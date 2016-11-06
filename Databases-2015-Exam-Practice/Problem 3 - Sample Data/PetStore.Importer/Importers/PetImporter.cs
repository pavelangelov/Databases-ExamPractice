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
    public class PetImporter : IImporter
    {
        private readonly int NumberOfPets = 5000;

        public string Message
        {
            get
            {
                return "Importing pets.";
            }
        }

        public int Order
        {
            get
            {
                return 3;
            }
        }

        public void Import(PetStoreDBEntities db)
        {
            var species = db.Species.Select(x => x.Id).ToList();
            var colors = db.Colors.Select(x => x.Id).Count();
            var addedPets = 0;
            foreach (var speciesId in species)
            {
                var petsPerSpecies = NumberOfPets / species.Count();
                for (int i = 0; i < petsPerSpecies; i++)
                {
                    var price = RandomGenerator.RandomNumber(5, 2500);
                    var colorId = RandomGenerator.RandomNumber(1, colors);
                    var beforeDate = DateTime.Now.AddDays(-60);
                    var afterDate = new DateTime(2010, 12, 31);
                    var dateOfBirth = RandomGenerator.RandomDateTime(afterDate, beforeDate);

                    db.Pets.Add(new Pet()
                    {
                        BirthDate = dateOfBirth,
                        ColorId = colorId,
                        Price = price,
                        SpeciesId = speciesId
                    });

                    if (addedPets % 10 == 0)
                    {
                        Console.Write('.');
                    }

                    if (addedPets % 100 == 0)
                    {
                        Console.Write($"Added {addedPets} pets.");
                        Console.WriteLine();
                        db.SaveChanges();

                        db = new PetStoreDBEntities();
                    }

                    addedPets++;
                }
            }

            if (addedPets < NumberOfPets)
            {
                var leftPets = NumberOfPets - addedPets;
                for (int i = 0; i < leftPets; i++)
                {
                    var price = RandomGenerator.RandomNumber(5, 2500);
                    var colorId = RandomGenerator.RandomNumber(1, colors);
                    var beforeDate = DateTime.Now.AddDays(-60);
                    var afterDate = new DateTime(2010, 12, 31);
                    var dateOfBirth = RandomGenerator.RandomDateTime(afterDate, beforeDate);
                    var speciesIdIndex = RandomGenerator.RandomNumber(0, species.Count - 1);

                    db.Pets.Add(new Pet()
                    {
                        BirthDate = dateOfBirth,
                        ColorId = colorId,
                        Price = price,
                        SpeciesId = species[speciesIdIndex]
                    });

                    addedPets++;
                }
            }

            db.SaveChanges();
        }
    }
}
