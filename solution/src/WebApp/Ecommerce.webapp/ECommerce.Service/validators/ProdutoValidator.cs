using Ecommerce.webapp.Models.Entities;
using ECommerce.Domain.Repositories;

namespace ECommerce.Service.validators
{
    public class ProdutoValidator
    {
        public readonly IRepository<Produto> repository;

        public ProdutoValidator(IRepository<Produto> repository)
        {
            this.repository = repository;
        }


        public bool ValidaSePodeVender(Guid id)
        {
            var prod = repository.GetById(id);
            return prod.Ativo;
        }
    }
}
