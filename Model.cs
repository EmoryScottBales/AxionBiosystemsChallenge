using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AxionBiosystemsChallenge
{
    public class SmallCoEmployeeDb : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=smallco.db");
    }

    public class Employee
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int ID { get; set;}
        public string Birthday { get; set;} // might be a number but it has slashes
        public string Department { get; set;}
        public string FavBook { get; set;}
    }
}