namespace NetworkLogic.PoolLogic
{
    public interface INetworkPoolable
    {
        void PoolInitialize();
        void ReturnToPool();
    }
}