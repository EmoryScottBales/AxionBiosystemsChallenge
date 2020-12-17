using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AxionBiosystemsChallenge
{
    public class SmallCoEmployeeDb : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        // public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=smallco.db");
    }

    public class Employee
    {
        public string Name { get; set;}
        public int ID { get; set;}
        public string Birthday { get; set;} // might be a number but it has slashes
        public string Department { get; set;}
        public string FavBook { get; set;}

    //     public int BlogId { get; set; }
    //     public string Url { get; set; }

    //     public List<Post> Posts { get; } = new List<Post>();
    }

    // public class Post
    // {
    //     public int PostId { get; set; }
    //     public string Title { get; set; }
    //     public string Content { get; set; }

    //     public int BlogId { get; set; }
    //     public Blog Blog { get; set; }
    // }
}