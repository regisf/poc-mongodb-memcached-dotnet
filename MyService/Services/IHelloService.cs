using MyService.Models;

namespace MyService.Services;

public interface IHelloService
{
    Task<List<HelloModel>> GetAllAsync(int pageIndex);
    Task<List<string>> GetAllGuidsAsync();
    Task<HelloModel> GetByUid(string uid);
}