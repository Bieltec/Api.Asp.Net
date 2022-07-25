using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("primeiro")]
        [HttpGet("/primeiro")]
        [HttpGet("teste")]
        public async Task<ActionResult<Produto>> GetPrimeiroAsync()
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync();
            if (produto is null)
            {
                return NotFound();
            }
            return Ok(produto);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            if (produtos is null )
            {
                return NotFound("Produtos não encontrados");
            }
            return produtos;
        }
        [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
        public async Task<ActionResult<Produto>> GetAsync(int id)
        {
            
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.ProdutoID == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            return produto;
        }
        [HttpPost]
        public ActionResult<Produto> Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();
             //adicionar o produto passado no body ao produto localizado no Context
             _context.Produtos.Add(produto);
             //este metodo SaveChances permite salver os dados no bando de dados
             _context.SaveChanges();
             // agora tenho que passar no header que o produto foi criado para isso temo o metodo CreatedRouteResult que retorna 201 created
             return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoID }, produto);
        }
        [HttpPut("{id:int}")]
        public ActionResult<Produto> Put(int id, Produto produto)
        {
            if(id != produto.ProdutoID)
            {
                return BadRequest();
            }
            // metodo para que o entity entenda que esta sendo modificado os dados na tabela
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        [HttpDelete]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");

            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }


    }
}
