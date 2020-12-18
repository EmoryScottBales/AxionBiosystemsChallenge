using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AxionBiosystemsChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Refactor to add public PopulateDB(string csvfile) method to SmallCoEmployeeDb class.
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
                            employee.FirstName = row[i].Substring(0, row[i].IndexOf(' '));
                            employee.LastName = row[i].Substring(row[i].IndexOf(' ') + 1);
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
                db.Add(new Employee { FirstName = employee.FirstName, LastName = employee.LastName, 
                    ID = employee.ID, Birthday = employee.Birthday, Department = employee.Department, 
                    FavBook = employee.FavBook});
            }
            db.SaveChanges();

            /**********Handle Command Line Arguments************/
            for(int i = 0; i < args.Length; i++)
            {
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
                    //last name parsing on an optional white space.
                    string pattern = @"[A-Z]+[ ]?[A-Z]+";
                    if((i < args.Length - 1) && 
                        Regex.Match(args[i+1], pattern, RegexOptions.IgnoreCase).Success)
                    {
                        try
                        {
                            var e = db.Employees.Where(item => item.LastName == args[i+1]).First();
                            Console.WriteLine("First Name: {0}", e.FirstName);
                            Console.WriteLine("Last Name: {0}", e.LastName);
                            Console.WriteLine("ID: {0}", e.ID);
                            Console.WriteLine("Birthday: {0}", e.Birthday);
                            Console.WriteLine("Department: {0}", e.Department);
                            Console.WriteLine("Favorite Book: {0}", e.FavBook);
                        }
                        catch(InvalidOperationException ioe)
                        {
                            // Console.WriteLine(ioe.Message);
                            Console.WriteLine("No matching database entry for Last Name = {0}.", args[i+1]);
                        }
                    }else
                    {
                        Console.WriteLine("Invalid last name.  Must be upper and lower case letters only.");
                        break;
                    }
                }else if(args[i].Equals("-ID") || args[i].Equals("-id"))
                {
                    string pattern = @"\d+";
                    if((i < args.Length - 1) && Regex.Match(args[i+1], pattern).Success)
                    {
                        try
                        {
                            var e = db.Employees.Where(item => item.ID == Int32.Parse(args[i+1])).First();
                            Console.WriteLine("First Name: {0}", e.FirstName);
                            Console.WriteLine("Last Name: {0}", e.LastName);
                            Console.WriteLine("ID: {0}", e.ID);
                            Console.WriteLine("Birthday: {0}", e.Birthday);
                            Console.WriteLine("Department: {0}", e.Department);
                            Console.WriteLine("Favorite Book: {0}", e.FavBook);
                        }
                        catch(InvalidOperationException ioe)
                        {
                            // Console.WriteLine(ioe.Message);
                            Console.WriteLine("No matching database entry for ID = {0}.", args[i+1]);
                        }
                    }else
                    {
                        Console.WriteLine("Invalid id.  Must be an integer.");
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