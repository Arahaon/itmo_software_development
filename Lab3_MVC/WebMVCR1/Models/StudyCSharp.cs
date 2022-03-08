using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebMVCR1.Models
{
    public enum AccountType { Checking, Deposit }

    public struct BankAccount
    {
        public long accNo;
        public decimal accBal;
        public AccountType accType;

        public override string ToString() { string str = $"Номер счета {accNo}, баланс {accBal}, тип {accType}"; return str; }
    }

    public class StudyCsharp
    {
        public static string SetStatus(int age)
        {
            string status = "junior developer";
            if ((age > 2) && (age < 7)) status = "middle developer"; else if ((age >= 7) && (age < 15)) status = "senior developer"; else if ((age >= 15)) status = "sensei";
            return status;
        }

        public static string ExeSwitch(string status)
        {
            string res;
            switch (status)
            {
                case "junior developer": res = "Набирайся знаний"; break;
                case "middle developer": res = "Набирайся опыта"; break;
                case "senior developer": res = "Руководи другими"; break;
                case "sensei": res = "Учи других"; break;
                default: res = "Не знаю, что делать"; break;
            }
            return res;
        }

        public static string GetFunction(double x1, double x2)
        {
            StringBuilder str = new StringBuilder();
            double x = x1; do { str.AppendFormat("x = {0:0.##} : y = {1:0.##}; <br>", x, Math.Pow(x, 3)); x = x + 0.5; } while (x <= x2);
            return str.ToString(); ;
        }

        public static bool Factorial(int n, out int answer)
        {
            int k;
            int f = 1;
            bool ok = true;
            try
            {
                checked
                {
                    for (k = 2; k <= n; ++k)
                    { f = f * k; }
                }
            }
            catch (Exception) { f = 0; ok = false; }
            answer = f;
            return ok;
        }

    }

    public class Shape
    {
        public double St { get; set; }
        virtual public string Name { get { return String.Format("\"Фигура\""); } }

        public virtual double Area => 0;
    }

    public class Triangle : Shape, IComparable<Shape>
    {
        public double Sta { get; set; }
        public double Stb { get; set; }
        public double Stc { get; set; }

        public Triangle(double a, double b, double c) { Sta = a; Stb = b; Stc = c; }

        public double Perimeter => Math.Round(Sta + Stb + Stc);
        public override string Name => $"\"Треугольник со сторонами {Sta},{Stb} и {Stc}\"";
        public override double Area { get { double sq = Math.Sqrt(Perimeter / 2 * (Perimeter / 2 - Sta) * (Perimeter / 2 - Stb) * (Perimeter / 2 - Stc)); return sq; } }

        public int CompareTo(Shape other) { if (this.Area == other.Area) return 0; else if (this.Area > other.Area) return 1; else return -1; }
    }

    public class Circle : Shape, IComparable<Shape>
    {
        public double St { get; set; }
        public override string Name
        {
            get { return String.Format("\"Окружность с радиусом {0}\"", St); }
        }
        public Circle(double a) { St = a; }
        public double Dlina { get { double p = 2 * Math.PI * St; return p; } }
        public override double Area { get { double sq = Math.PI * St * St; return sq; } }

        public int CompareTo(Shape other) { if (this.Area == other.Area) return 0; else if (this.Area > other.Area) return 1; else return -1; }
    }

}