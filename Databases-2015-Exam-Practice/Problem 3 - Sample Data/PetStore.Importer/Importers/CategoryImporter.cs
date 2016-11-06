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
    public class CategoryImporter : IImporter
    {
        private readonly int NumberOfCategories = 50;

        public string Message
        {
            get
            {
                return "Importing categories.";
            }
        }

        public int Order
        {
            get
            {
                return 4;
            }
        }

        public void Import(PetStoreDBEntities db)
        {
            var categories = new HashSet<string>();
            while (categories.Count < this.NumberOfCategories)
            {
                categories.Add(RandomGenerator.RandomString(5, 20));
            }

            foreach (var category in categories)
            {
                db.Categories.Add(new Category()
                {
                    Name = category
                });
            }

            db.SaveChanges();
        }
    }
}
