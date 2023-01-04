using System.Text.Json.Serialization;

namespace ApiCatalago.Models
{
    public class Categoria
    { 
        public int CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        [JsonIgnore]
        public ICollection<Produto>? produtos { get; set; }
    }
}
