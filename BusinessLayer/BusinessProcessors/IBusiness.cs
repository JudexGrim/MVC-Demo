using AutoMapper.Configuration.Conventions;
using EntityModels;


namespace BusinessLayer.BusinessProcessors
{
    public interface IBusiness<T> where T : IEntityModel
    {
        Task<(IEnumerable<T>, int maxID)> GetAll();
        Task<(bool success, int ID)> Update(T parameters);
        Task<int> Delete(int id);
         
    }
}
