namespace PoolLogic
{
    public interface IPool<T>
    {
        void Initialize(T prefab, int initialSize);
        T Pull();
        void Push(T t);
        void PushBackAll();
    }
}