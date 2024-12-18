using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.webapp.Models.Entities
{
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? Nome { get; set; }


        [MinLength(10, ErrorMessage = "SKU tem no mínimo 10 caracteres")]
        [MaxLength(1000)]
        public string? SKU { get; set; }

        public bool Ativo { get; set; }

    }
}
