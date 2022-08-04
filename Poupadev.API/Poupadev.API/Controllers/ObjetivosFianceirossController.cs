using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poupadev.API.Entities;
using Poupadev.API.Models;
using Poupadev.API.Persistence;

namespace Poupadev.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/objetivos-financeiros")]
    [ApiController]
    public class ObjetivosFianceirossController : ControllerBase
    {
        private readonly PoupaDevContext _context;
        public ObjetivosFianceirossController(PoupaDevContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var objetivos = _context.Objetivos;
            return Ok(objetivos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPorId(int id)
        {
            var objetivo = _context.Objetivos.Include(o => o.Operacoes).SingleOrDefault(x => x.Id == id);

            if (objetivo == null)
                return NotFound();


            return Ok(objetivo);
        }

        [HttpPost]
        public IActionResult Post(ObjetivoFinanceiroInputModel model)
        {
            var objetivo = new ObjetivoFinanceiro(model.Titulo, model.Descricao, model.ValorObjetivo);
            _context.Objetivos.Add(objetivo);
            _context.SaveChanges();

            var id = objetivo.Id;

            return CreatedAtAction("GetPorId", new { id }, model);
        }

        [HttpPost("{id}/operacoes")]
        public IActionResult PostOperacao(int id, OperacaoInputModel model)
        {
            var operacao = new Operacao(model.Valor, model.TipoOperacao, id);

            var objetivo = _context.Objetivos.Include(x => x.Operacoes).SingleOrDefault(x => x.Id == id);

            if (objetivo == null)
                return NotFound();

            objetivo.RealizarOperacao(operacao);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
