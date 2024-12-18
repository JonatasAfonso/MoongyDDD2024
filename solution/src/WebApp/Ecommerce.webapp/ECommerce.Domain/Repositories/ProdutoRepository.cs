using Ecommerce.webapp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Repositories
{
    public class ProdutoRepository : IRepository<Produto>
    {
        public readonly ECommerceDbContext _context;


        public void Add(Produto entity)
        {
            _context.Produtos.Add(entity);
            _context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var prod = _context.Produtos.Find(id);
            _context.Produtos.Remove(prod);
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _context.Produtos.ToList();
        }

        public Produto? GetById(Guid id)
        {
            return _context.Produtos.Find(id);
        }

        public bool Update(Produto entity, Guid id)
        {
            var prod = _context.Produtos.Find(id);
            prod.Nome = entity.Nome;
            prod.SKU = entity.SKU;
            
            return _context.SaveChanges() >= 0;
        }
    }
}
