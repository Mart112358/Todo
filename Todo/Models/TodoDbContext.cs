using System.Data.Entity;

namespace Todo.Models
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
    }
}