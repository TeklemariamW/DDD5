namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository OwnerRep { get; }
        IAccountRepository AccountRep { get; }
        void Save();
    }
}
