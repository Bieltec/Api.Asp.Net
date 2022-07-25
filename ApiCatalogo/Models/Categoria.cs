using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Models;

public class Categoria
{
    //porque chamou a classe Collection
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
    public int CategoriaId { get; set; }
    //string? definindo propriedade como nullable
    [Required]
    [MaxLength(80)]
    public string? Nome { get; set; }
    [Required]
    [MaxLength(300)]
    public string? ImagemUrl { get; set; }

    public ICollection<Produto> Produtos {get; set;}

}
