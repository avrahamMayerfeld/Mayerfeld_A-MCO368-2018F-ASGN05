using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _368._05LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee c = new Employee("Cedric", 80000);
            Employee d = new Employee("Devon", 70000);
            Employee e = new Employee("Eric", 90000);
            Employee f = new Employee("Fanny", 100000);
            Employee g = new Employee("Grinch", 110000);

            List<Employee> employees = new List<Employee>
            {
                c,
                e,
                d,
                g,
                f
            };

            IEnumerable<Employee> maxOverP = employees.MaxOverPrevious(emp => emp.Salary * 6.78);
            foreach (Employee emp in maxOverP)
                Console.WriteLine(emp.Name);

            IEnumerable<Employee> localMax = employees.LocalMaxima(emp => emp.Salary);
            foreach (Employee emp in localMax)
                Console.WriteLine(emp.Name);

            if (employees.AtLeastK(2,  emp => emp.Name.Contains("e")))
                Console.WriteLine("at least k");

            if (employees.AtLeastHalf(emp => emp.Name.Contains("e")))
                Console.WriteLine("at least half");

            Console.ReadKey();
        }
    }
}
