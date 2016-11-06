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
    public class ProductImporter : IImporter
    {
        private readonly int NumberOfProducts = 20000;

        public string Message
        {
            get
            {
                return "Importing products.";
            }
        }

        public int Order
        {
            get
            {
                return 5;
            }
        }

        public void Import(PetStoreDBEntities db)
        {
            var species = db.Species.ToList();
            var categories = db.Categories.Select(x => x.Id).ToList();
            var productsInCategory = this.NumberOfProducts / categories.Count;
            var addedProducts = 0;

            foreach (var categoryId in categories)
            {
                if (addedProducts + productsInCategory > this.NumberOfProducts)
                {
                    productsInCategory = this.NumberOfProducts - addedProducts;
                }
                for (int i = 0; i < productsInCategory; i++)
                {
                    this.AddProduct(db, categoryId, species);

                    if (addedProducts % 10 == 0)
                    {
                        Console.Write('.');
                    }

                    if (addedProducts % 100 == 0)
                    {
                        Console.WriteLine($"Added {addedProducts} products.");
                        db.SaveChanges();
                        db = new PetStoreDBEntities();
                        species = db.Species.ToList();
                    }

                    addedProducts++;
                }
            }

            if (addedProducts < this.NumberOfProducts)
            {
                var leftProducts = this.NumberOfProducts - addedProducts;
                for (int i = 0; i < leftProducts; i++)
                {
                    var categoryId = RandomGenerator.RandomNumber(0, categories.Count - 1);
                    this.AddProduct(db, categoryId, species);

                    addedProducts++;
                }
            }

            db.SaveChanges();
        }

        private void AddProduct(PetStoreDBEntities db, int categoryId, IList<Species> species)
        {
            var product = new Product
            {
                CategotyId = categoryId,
                Name = RandomGenerator.RandomString(5, 25),
                Price = RandomGenerator.RandomNumber(10, 1000)
            };

            var numberOfSpeciesPerProduct = RandomGenerator.RandomNumber(2, 10);
            for (int i = 0; i < numberOfSpeciesPerProduct; i++)
            {
                product.Species.Add(species[RandomGenerator.RandomNumber(0, species.Count - 1)]);
            }

            db.Products.Add(product);
        }
    }
}
