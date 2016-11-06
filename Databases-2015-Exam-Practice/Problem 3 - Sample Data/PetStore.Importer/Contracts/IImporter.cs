using PetStore.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Importer.Contracts
{
    public interface IImporter
    {
        string Message { get; }

        int Order { get;}

        void Import(PetStoreDBEntities db);
    }
}
