using Ecommerce.webapp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Domain.Repositories
{
    public class ECommerceDbContext : DbContext
    {
        public  DbSet<Produto> Produtos { get; set; }
    }
}