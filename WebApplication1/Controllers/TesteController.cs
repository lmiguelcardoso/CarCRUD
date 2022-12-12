using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        public Repository.Repository TesteRepo { get; set; }
        public TesteController()
        {
            this.TesteRepo = new Repository.Repository();
        }
        [HttpPost]
        [Route("/inserir")]
        public IActionResult Inserir(Teste newteste)
        {
            TesteRepo.InserirTeste(newteste);
            return Created("OK",newteste);

        }

        [HttpGet]
        [Route("/consultar")]
        public IActionResult Consultar()
        {
            var lista = TesteRepo.ListarTodos();
            return Ok(lista);
        }
    }
}
