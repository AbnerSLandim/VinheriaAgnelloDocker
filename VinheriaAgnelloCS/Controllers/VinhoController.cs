using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VinheriaAgnelloCS.Models;

namespace VinheriaAgnelloCS.Controllers;

public class VinhoController : Controller
{
    private List<VinhoModel> _vinhos;
    
    public IActionResult Index()
    {
        return View(VinhoRepository.Vinhos);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(VinhoModel vinhoModel)
    {
        vinhoModel.id = VinhoRepository.Vinhos.Max(v => v.id) + 1;
        VinhoRepository.Vinhos.Add(vinhoModel);
        
        TempData["mensagemSucesso"] = "Vinho cadastrado com sucesso";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vinhoConsultado = VinhoRepository.Vinhos.FirstOrDefault(v => v.id == id);

        if (vinhoConsultado == null)
            return NotFound();

        return View(vinhoConsultado);
    }

    [HttpPost]
    public IActionResult Edit(VinhoModel vinhoModel)
    {
        var index = VinhoRepository.Vinhos.FindIndex(v => v.id == vinhoModel.id);
        if (index == -1) return NotFound();

        VinhoRepository.Vinhos[index] = vinhoModel;

        TempData["mensagemSucesso"] = $"Vinho {vinhoModel.nome} atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Detail(int id)
    {
        // Simulando a busca no "banco de dados" (repositório de vinhos)
        var vinhoConsultado = VinhoRepository.Vinhos.FirstOrDefault(v => v.id == id);

        if (vinhoConsultado == null)
            return NotFound();

        // Retornando o vinho consultado para a View
        return View(vinhoConsultado);
    }
    
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var vinhoConsultado = VinhoRepository.Vinhos.FirstOrDefault(v => v.id == id);
        if (vinhoConsultado != null)
        {
            VinhoRepository.Vinhos.Remove(vinhoConsultado);
            TempData["mensagemSucesso"] = $"O vinho {vinhoConsultado.nome} foi removido com sucesso!";
        }
        else
        {
            TempData["mensagemErro"] = "OPS !!! Vinho inexistente.";
        }

        return RedirectToAction(nameof(Index));
    }

    
    public static class VinhoRepository
    {
        public static List<VinhoModel> Vinhos { get; set; } = GerarVinhosMocados();

        private static List<VinhoModel> GerarVinhosMocados()
        {
            var vinhos = new List<VinhoModel>();
            Random random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                vinhos.Add(new VinhoModel
                {
                    id = i,
                    nome = $"Vinho{i}",
                    descricao = $"Descrição do Vinho {i}",
                    categoria = "Seco",
                    imagem = $"vinho{i}.jpg",
                    marca = $"Marca{i}",
                    preco = random.Next(100, 351)
                });
            }

            return vinhos;
        }
    }
}