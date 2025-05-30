using Microsoft.EntityFrameworkCore;
using PersonalBlog.Models;

namespace PersonalBlog.Data;

public class PersonalBlogContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    
    public PersonalBlogContext(DbContextOptions options) : base(options)
    {
    }
}