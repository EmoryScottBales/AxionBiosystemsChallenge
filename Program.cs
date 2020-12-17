using System;
using System.Linq;

namespace AxionBiosystemsChallenge
{
    class Program
    {
        static void Main()
        {
            using (var db = new SmallCoEmployeeDb())
            {
                // Create
                // Console.WriteLine("Inserting a new blog");
                //Cletis Willshee,1,11/3/1970,Sales,Essays: English and American
                // db.Add(new Employee { Url = "http://blogs.msdn.com/adonet" });
                db.Add(new Employee { Name = "Cletis Willshee", ID = 1, Birthday = "11/3/1970", 
                    Department = "Sales", FavBook = "Essays: English and American" });
                db.SaveChanges();

                foreach(var e in db.Employees)
                {
                    Console.WriteLine(e.Department);
                }
                // Read
                // Console.WriteLine("Querying for a blog");
                var employee = db.Employees
                    .OrderBy(e => e.ID)
                    .First();

                // Update
                // Console.WriteLine("Updating the blog and adding a post");
                // blog.Url = "https://devblogs.microsoft.com/dotnet";
                // blog.Posts.Add(
                //     new Post
                //     {
                //         Title = "Hello World",
                //         Content = "I wrote an app using EF Core!"
                //     });
                employee.Department = "Human Resources";
                db.SaveChanges();

                foreach(var e in db.Employees)
                {
                    Console.WriteLine(e.Department);
                }

                // Delete
                // Console.WriteLine("Delete the blog");
                db.Remove(employee);
                db.SaveChanges();

                foreach(var e in db.Employees)
                {
                    Console.WriteLine(e.Department);
                }
            }
        }
    }
}