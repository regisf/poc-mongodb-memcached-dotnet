using Microsoft.AspNetCore.Mvc;
using MyService.Models;
using MyService.Services;
using Newtonsoft.Json;

namespace MyService.Controllers;

[ApiController]
[Route("/hello")]
public class HelloController : Controller
{
    private readonly IHelloService _service;

    public HelloController(IHelloService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<HelloModel>> Get([FromQuery(Name="PageIndex")] int pageIndex)
    {
        return await _service.GetAllAsync(pageIndex);
    }

    [HttpGet("guids")]
    public async Task<List<string>> GetGuids()
    {
        return await _service.GetAllGuidsAsync();
    }

    [HttpGet("{uid}")]
    public async Task<HelloModel> GetByUid(string uid)
    {
        return await _service.GetByUid(uid);
    }
}