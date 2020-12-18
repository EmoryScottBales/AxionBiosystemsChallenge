using System;
using System.Linq;

namespace AxionBiosystemsChallenge
{
    class Program
    {
        static void Main()
        {
            //Read CSV
            CsvFileReader reader = new CsvFileReader("Employees.csv");
            CsvRow row = new CsvRow();
            reader.ReadRow(row);  // throw away the first row
            var db = new SmallCoEmployeeDb();

            while (reader.ReadRow(row))
            {
                if(db.Employees.Find(Int32.Parse(row[1])) != null) continue;

                Console.WriteLine("in here");
                var employee = new Employee();
                for( int i = 0; i < row.Count; i++){
                    switch(i)
                    {
                        case 0:
                            employee.Name = row[i];
                            break;
                        case 1:
                            employee.ID = Int32.Parse(row[i]);
                            break;
                        case 2:
                            employee.Birthday = row[i];
                            break;
                        case 3:
                            employee.Department = row[i];
                            break;
                        case 4:
                            employee.FavBook = row[i];
                            break;
                    }
                }
                //If the ID already exists in the database, don't add the employee.
                // if(db.Employees.Find(employee.ID) == null){
                    db.Add(new Employee { Name = employee.Name, ID = employee.ID, Birthday = employee.Birthday,
                        Department = employee.Department, FavBook = employee.FavBook});
                // }
            }
            db.SaveChanges();

            // using (var db = new SmallCoEmployeeDb())
            // {
                //Implement command line flags --ID and --Name to read the database and print the entry.
                // Create
                // db.Add(new Employee { Name = "Cletis Willshee", ID = 1, Birthday = "11/3/1970", 
                //     Department = "Sales", FavBook = "Essays: English and American" });
                // db.SaveChanges();

                foreach(var e in db.Employees)
                {
                    Console.WriteLine(e.Name);
                }
                // Read
                // var employee = db.Employees
                //     .OrderBy(e => e.ID)
                //     .First();

                // // Update
                // employee.Department = "Human Resources";
                // db.SaveChanges();

                // foreach(var e in db.Employees)
                // {
                //     Console.WriteLine(e.Department);
                // }

                // Delete
                // Console.WriteLine("Delete the blog");
                // db.Remove(employee);
                // db.SaveChanges();

                // foreach(var e in db.Employees)
                // {
                //     Console.WriteLine(e.Department);
                // }
            // }
        }
    }
}