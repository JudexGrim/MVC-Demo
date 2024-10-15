using AutoMapper.Configuration.Conventions;
using EntityModels;


namespace BusinessLayer.BusinessProcessors
{
    public interface IBusiness<T> where T : IEntityModel
    {
        Task<(IEnumerable<T>, object ReturnData)> GetAll();
        Task<(bool success, object ReturnData)> Update(T parameters);
        Task<int> Delete(int id);
         
    }
}
