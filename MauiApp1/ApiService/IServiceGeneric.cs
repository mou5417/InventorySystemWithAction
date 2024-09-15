using Entities;

namespace ApiService
{
    public interface IServiceGeneric

    {

        Task CreateDataAsync<T, TId>(T item) where T : class;
        Task<T> GetDataByMatchAsync<T, TId>(string type, Guid Id);
        Task<List<T>> GetStore<T>(string filtertext) where T : new();
        Task<AuthResponseModel> Login(LoginModel loginModel);
        Task RemoveDataAsync<T, TId>(string type, Guid id) where T : class;
        Task UpdateDataAsync<T, TId>(T item, Guid id) where T : class;



    }
}
