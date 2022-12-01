using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using mvcStudy.Models;
using mvcStudy.Repository;

namespace mvcStudy.Controllers
{
    public class CarsController : Controller
    {
        private CarRepository CarRepo{ get; set; }
        public CarsController()
        {
            this.CarRepo= new CarRepository();
        }
        public IActionResult Index()
        {
            var list = this.CarRepo.GetCars();

            return View(list);
        }
        public IActionResult Cadastrar()
        {
            return View(new Car());
        }
        [HttpPost]
        public ActionResult Cadastrar(Car novoCarro)
        {
            @TempData["mensagem"] = "Carro Adicionado com sucesso!";
            this.CarRepo.InsertCar(novoCarro);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            this.CarRepo.DeleteCar(id);
            @TempData["mensagem"] = "Carro removido com sucesso!";
            return RedirectToAction("Index");

        }

        public IActionResult Editar(int id)
        {
            Car carToEdit = this.CarRepo.FindCarById(id);
            return View(carToEdit);
        }

        [HttpPost]
        public IActionResult Editar(Car carroEditar) 
        { 
            this.CarRepo.UpdateCar(carroEditar);
            return RedirectToAction("Index");
        }

    }
}
