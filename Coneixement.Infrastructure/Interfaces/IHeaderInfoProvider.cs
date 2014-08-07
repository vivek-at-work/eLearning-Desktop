namespace Coneixement.Infrastructure.Interfaces
{
    public interface IHeaderInfoProvider<T>
    {
        T HeaderInfo { get; }
    }
}
