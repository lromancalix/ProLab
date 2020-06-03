using ProLab.Repository;
using ProLab.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using U = ProLab.UnitOfWork;


namespace ProLab.DataAccess
{
    public class ProLabUnitOfWork : U.IUnitOfWork
    {

        public ProLabUnitOfWork(string connecionString)
        {
            this.User = new UserRepository(connecionString);
            
        }
        public IUserRepository User { get; private set; }

    }
}
