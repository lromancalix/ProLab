using System;
using System.Collections.Generic;
using System.Text;
using R = ProLab.Repository;

namespace ProLab.UnitOfWork
{
    public interface IUnitOfWork
    {

        R.IUserRepository User { get; }

        R.ILoginRepository Login { get; }

    }
}
