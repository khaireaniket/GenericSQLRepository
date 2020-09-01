using HowToConsume.GenericRepository.SQL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HowToConsume.GenericRepository.SQL.Data
{
    public class GenericRepositoryDBContext : DbContext
    {
        public GenericRepositoryDBContext(DbContextOptions<GenericRepositoryDBContext> options) : base(options)
        {

        }

        public virtual DbSet<Person> Person { get; set; }
    }
}
