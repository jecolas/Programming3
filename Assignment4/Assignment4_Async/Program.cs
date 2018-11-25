using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_Async
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculater();

        }

        static private async void Calculater()
        {
            //x = (a2 + b2 + c2) /(p2 – q2  + r2)
            decimal a, b, c, p, q, r;

            Console.Write("a: ");
            a = Convert.ToDecimal(Console.ReadLine());
            Task<decimal> task1 = Task.Run(() => CalcFactorial(a));
            Console.Write("b: ");
            b = Convert.ToDecimal(Console.ReadLine());
            Task<decimal> task2 = Task.Run(() => CalcFactorial(b));
            Console.Write("c: ");
            c = Convert.ToDecimal(Console.ReadLine());
            Task<decimal> task3 = Task.Run(() => CalcFactorial(c));
            Console.Write("p: ");
            p = Convert.ToDecimal(Console.ReadLine());
            Task<decimal> task4 = Task.Run(() => CalcFactorial(p));
            Console.Write("q: ");
            q = Convert.ToDecimal(Console.ReadLine());
            Task<decimal> task5 = Task.Run(() => CalcFactorial(q));
            Console.Write("r: ");
            r = Convert.ToDecimal(Console.ReadLine());
            Task<decimal> task6 = Task.Run(() => CalcFactorial(r));

            // wait until get all factorial
            //await Task.WhenAll(task1, task2, task3, task4, task5, task6); // wait for both to complete
            Console.WriteLine("=========== Finished factorial ===========");

            // calculate A+B+C & calculate P-Q+R
            Task<decimal> calculateABC = Task.Run(() => CalcABC(task1, task2, task3));
            Task<decimal> calculatePQR = Task.Run(() => CalcPQR(task4, task5, task6));


            try
            {
                Task<decimal> calcDev = Task.Run(() => CalcDevide(calculateABC, calculatePQR));
                await Task.WhenAll(calculateABC, calculatePQR, calcDev);
                Console.WriteLine($"Answer is {calcDev.Result}");
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("======= Done =====");
            }
            
        }

        static private decimal CalcFactorial(decimal num)
        {
            return num * num;
        }

        static private decimal CalcABC(Task<decimal> num1, Task<decimal> num2, Task<decimal> num3)
        {
            return num1.Result + num2.Result + num3.Result;
        }
        static private decimal CalcPQR(Task<decimal> num1, Task<decimal> num2, Task<decimal> num3)
        {
            return num1.Result - num2.Result + num3.Result;
        }
        static private decimal CalcDevide(Task<decimal> num1, Task<decimal> num2)
        {
            if(num2.Result != 0)
            {
                return num1.Result / num2.Result;
            }
            else
            {
                throw new Exception("the result of p-q+r shouldn't be 0");
            }
            
        }
    }
}
