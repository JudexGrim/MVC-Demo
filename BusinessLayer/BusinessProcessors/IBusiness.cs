using EntityModels;


namespace BusinessLayer.BusinessProcessors
{
    public interface IBusiness<T> where T : IEntityModel
    {
        Task<IEnumerable<T>> GetAll();
        Task<int> Update(T parameters);
    }
}
