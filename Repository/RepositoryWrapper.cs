using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repContext;
        private IOwnerRepository _ownerRep;
        private IAccountRepository _accountRep;

        public RepositoryWrapper(RepositoryContext repContext)
        {
            _repContext = repContext;
        }

        public IOwnerRepository OwnerRep
        {
            get
            {
                _ownerRep ??= new OwnerRepository(_repContext);
                return _ownerRep;
            }
        }
        public IAccountRepository AccountRep
        {
            get
            {
                _accountRep ??= new AccountRepository(_repContext);
                return _accountRep;
            }
        }
        public void Save()
        {
            _repContext.SaveChanges();
        }
    }
}
