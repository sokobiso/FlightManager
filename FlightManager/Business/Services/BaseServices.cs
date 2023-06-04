using FlightManager.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FlightManager.Business.Services
{
    public class BaseServices<TE> where TE : class
    {
        private IUnitOfWork Uow;
        public BaseServices(IUnitOfWork unitOfWork)
        {
            Uow = unitOfWork;
        }
        public void Dispose()
        {
            if (Uow != null)
            {
                Uow.Dispose();
                Uow = null;
            }
        }

        public IRepository<TE> Repository
        {
            get
            {
                if (Uow == null)
                    return null;
                return Uow.Repository<TE>();
            }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return Uow.Repository<T>();
        }
    }
}
