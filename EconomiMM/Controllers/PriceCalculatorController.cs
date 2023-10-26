using EconomiMM.Data;
using EconomiMM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EconomiMM.ViewModels;
namespace EconomiMM.Controllers
{
    public class PriceCalculatorController : Controller
    {
        private readonly EconomiMMContext _context;

        public PriceCalculatorController(EconomiMMContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jointTypes = _context.ExpansionJointsMaterials.
                Select(e => e.Type)
            .Distinct()
            .ToList();

            var flangeTypes = _context.FlangeMaterials.
                Select(e => e.Type)
            .Distinct()
            .ToList();
            var innerlinerTypes = _context.LinerMaterials.
                Where(m => m.PartOfLiner == Enum.LinerParts.Inner).Select(e => e.Type)
            .Distinct()
            .ToList();
            var outerlinerTypes = _context.LinerMaterials.
                Where(m => m.PartOfLiner == Enum.LinerParts.Outer).Select(e => e.Type)
            .Distinct()
            .ToList();

            var jointMaterials = _context.ExpansionJointsMaterials.ToList();
            var flangeMaterials = _context.FlangeMaterials.ToList();
            var linerMaterials = _context.LinerMaterials.ToList();


            var calculatorViewModel = new CalculatorViewModel()
            {
                jointMaterialTypes = jointTypes,
                flangeMaterialTypes = flangeTypes,
                innerLinerMaterialTypes = innerlinerTypes,
                outerLinerMaterialTypes = outerlinerTypes,
                expansionJointMaterials = jointMaterials,
                flangeMaterials = flangeMaterials,
                linerExpansionJointMaterials = linerMaterials,
            };

            return View(calculatorViewModel);
        }
        public IActionResult MaterialEditor()
        {
            return View();
        }

    }
}
