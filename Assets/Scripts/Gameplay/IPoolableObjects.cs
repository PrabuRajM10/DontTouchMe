namespace Gameplay
{
    public interface IPoolableObjects
    {
        public void Init(ObjectPooling pool);

        public void BackToPool();
    }
}