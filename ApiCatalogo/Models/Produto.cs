using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Nome { get; set; }
        [Required]
        [MaxLength(300)]
        public string? Descricao { get; set; }
        [Required]
        [Column(TypeName="decimal(10,2)")]
        public decimal Preco { get; set; }
        [Required]
        [MaxLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }

        //com isso esta propriedade vai ser ignorada e não vai mostrar as propriedades da classe categoria no boddy do swagger
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
