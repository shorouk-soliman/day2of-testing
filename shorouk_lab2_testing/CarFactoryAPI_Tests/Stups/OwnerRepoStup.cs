using CarAPI.Entities;
using CarAPI.Repositories_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryAPI_Tests.Stups
{
    internal class OwnerRepoStup : IOwnersRepository
    {
        public bool AddOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public List<Owner> GetAllOwners()
        {
            throw new NotImplementedException();
        }

        public Owner GetOwnerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
