using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AxionBiosystemsChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            //PopulateDB("Employees.csv");
            //Read CSV
            CsvFileReader reader = new CsvFileReader("Employees.csv");
            CsvRow row = new CsvRow();
            reader.ReadRow(row);  // throw away the first row that labels the columns
            var db = new SmallCoEmployeeDb();

            while (reader.ReadRow(row))
            {
                //If the employee ID already exists in the database, don't add the employee.
                if(db.Employees.Find(Int32.Parse(row[1])) != null) continue;

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
                db.Add(new Employee { Name = employee.Name, ID = employee.ID, Birthday = employee.Birthday,
                    Department = employee.Department, FavBook = employee.FavBook});
            }
            db.SaveChanges();

            for(int i = 0; i < args.Length; i++)
            {
                // Console.WriteLine("args[{0}] is {1}", i, args[i]);
                if(args[i].Equals("--h") || args[i].Equals("-help"))
                {
                    Console.WriteLine("Enter --h or -help for the help menu.");
                    Console.WriteLine("-name or -Name followed by a sequence of upper and lowercase letters will return a matching employee from the smallco employee database if one exists.");
                    Console.WriteLine("-id or -ID followed by a positive integer will return a matching employee from the smallco employee database if one exists.");
                    Console.WriteLine("If both -id and -name are specified, all matching employee database entries will be returned ");
                    Console.WriteLine("and marked with the key that matched: -id, -name, or both.");
                    break;
                }else if(args[i].Equals("-name") || args[i].Equals("-Name"))
                {
                    //TODO: update model.cs and migrate db to separate name field into
                    //first name and last name parsing on first white space.
                    string pattern = @"[A-Z]+[ ]?[A-Z]+";
                    if((i < args.Length - 1) && 
                        Regex.Match(args[i+1], pattern, RegexOptions.IgnoreCase).Success)
                    {
                        var e = db.Employees.Where(item => item.Name == args[i+1]).First();
                        if(e != null)
                        {
                            Console.WriteLine("Name: {0}", e.Name);
                            Console.WriteLine("ID: {0}", e.ID);
                            Console.WriteLine("Birthday: {0}", e.Birthday);
                            Console.WriteLine("Department: {0}", e.Department);
                            Console.WriteLine("Favorite Book: {0}", e.FavBook);
                        }else
                        {
                            Console.WriteLine("No matching database entry for Name = {0}.", args[i+1]);
                        }
                    }else
                    {
                        Console.WriteLine("Invalid name.  Must be upper and lower case letters only.");
                        break;
                    }
                }else if(args[i].Equals("-ID") || args[i].Equals("-id"))
                {
                    string pattern = @"\d+";
                    if(Regex.Match(args[i+1], pattern).Success)
                    {
                        //need to implement try catch b/c if no match, the find will throw an exception that should be handled.
                        var e = db.Employees.Where(item => item.ID == Int32.Parse(args[i+1])).First();
                        if(e != null)
                        {
                            Console.WriteLine("Name: {0}", e.Name);
                            Console.WriteLine("ID: {0}", e.ID);
                            Console.WriteLine("Birthday: {0}", e.Birthday);
                            Console.WriteLine("Department: {0}", e.Department);
                            Console.WriteLine("Favorite Book: {0}", e.FavBook);
                        }else
                        {
                            Console.WriteLine("No matching database entry for ID = {0}.", args[i+1]);
                        }
                    }else
                    {
                        Console.WriteLine("Invalid id.  Must be a positive integer.");
                        break;
                    }
                }else
                {
                    if(i % 2 == 1) continue;
                    Console.WriteLine("Invalid argument. Try --h or -help for the help menu.");
                    break;
                }
            }
        }
    }
}