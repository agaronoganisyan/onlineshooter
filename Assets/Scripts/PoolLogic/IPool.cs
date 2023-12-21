namespace PoolLogic
{
    public interface IPool<T>
    {
        T Pull();
        void Push(T t);
        void PushBackAll();
    }
}