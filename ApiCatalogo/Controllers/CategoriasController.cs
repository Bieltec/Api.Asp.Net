using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet("saudacao/{nome}")]
    public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuservico, string nome)
    {
        return meuservico.Saudacao(nome);
    }
    [HttpGet]
    //metodo assincrono
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {
        //metodo await que devemos aguardar a operacao e enquanto isso pode realizar outras operações.
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }
    //nao entendi ObterCategoria
    [HttpGet("{id:int}", Name ="ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {
        try
        {
            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.CategoriaId == id);
            if (categoria is null)
            {
                return  NotFound("Categoria não encontrada.");
            }
            return categoria;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorrou um problema ao tratar a sua solicitação. ");
        }
       
    }
    //metodo para retornar as categorias e tambem os produtos que estão nas categorias
    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return _context.Categorias.Include(p => p.Produtos).ToList();
    } 
    
    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest("Dados inválidos");
        }
        _context.Categorias.Add(categoria);

        //201 created.
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest($"Categoria com id={id} não foi localizada.");
        }
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(categoria);
    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound($"Categoria com id={id} não foi localizada.");
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges(); 

        return Ok();
    }
}
