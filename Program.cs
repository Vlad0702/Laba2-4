using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MM
{

   public class quadrangle
    {
        //координаты точек
        public double x1, y1, x2, y2, x3, y3, x4, y4;
        
        public quadrangle(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            //конструктор. Полученные параметры присваиваются полям класса
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.x3 = x3;
            this.y3 = y3;
            this.x4 = x4;
            this.y4 = y4;

        }


        public bool isExist()
        {
            //проверка на существование
            double a = side(1), b = side(2), c = side(3), d = side(4);
            //большая сторона должна быть меньше суммы трех остальных
            double max = Math.Max(Math.Max(a, b), Math.Max(c, d));

            double sum = a + b + c + d - max;

            if (Math.Round(sum - max) <= 0) return false;


            return true;
        }


        public string getCords()
        {
            string o;
            //функция возвращает строку с координатами в читабельном для человека виде
            o = "";
            o += "(";
            o += x1;
            o += ";";
            o += y1;
            o += ")";
            o += ",";

            o += "(";
            o += x2;
            o += ";";
            o += y2;
            o += ")";
            o += ",";

            o += "(";
            o += x3;
            o += ";";
            o += y3;
            o += ")";
            o += ",";

            o += "(";
            o += x4;
            o += ";";
            o += y4;
            o += ")";
            

            return o;
        }

        public double side(int n)
        {

            //нахождение сторон
            //1-4 - стороны 1-4 соответсвенно
            //5 и 6 - диагонали 1 и 2

            double result = 0;
            switch (n)
            {
                ///находится через стандартную формулу растояния между 2-мя точками
                ///для, например, первой стороны это будет между первой и четвертой, а для, например, четвертой - медлу четвертой и первой
                case 1:
                    result = Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));

                    break;

                case 2:
                    result = Math.Sqrt(Math.Pow((x2 - x3), 2) + Math.Pow((y2 - y3), 2));

                    break;

                case 3:
                    result = Math.Sqrt(Math.Pow((x3 - x4), 2) + Math.Pow((y3 - y4), 2));

                    break;

                case 4:
                    result = Math.Sqrt(Math.Pow((x4 - x1), 2) + Math.Pow((y4 - y1), 2));

                    break;

                case 5:
                    result = Math.Sqrt(Math.Pow((x4 - x2), 2) + Math.Pow((y4 - y2), 2));
                    break;

                case 6:
                    result = Math.Sqrt(Math.Pow((x3 - x1), 2) + Math.Pow((y3 - y1), 2));
                    break;

                default:
                    break;

            }

            return result;
        }


        public double perim()
        {
            //нахождение периметра
            return side(1) + side(2) + side(3) + side(4);
        }

        public double area()
        {
            //нахождение площади
            double p = perim() / 2;
            double a = side(1);
            double b = side(2);
            double c = side(3);
            double d = side(4);
            double e = side(5);
            double f = side(6);

            double result = (p - a) * (p - b) * (p - c) * (p - d) - (0.25) * (a * c + b * d + e * f) * (a * c + b * d - e * f);

            return result;
        }

    }

    public class trapetion : quadrangle
    {

        public trapetion(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4) : base(x1, y1, x2, y2, x3, y3, x4, y4)
        {
            //все параметры передаются в класс родитель
        }

        public bool isExist()
        {
            //проверка существования
            //первая и третья сторона (ребра) должны быть равны
            //вторая и четвертая параллельны. То есть разница высоты по координатам должна быть равной
            //но прежде проверка на то, четырехугольник ли это вообще
            if (base.isExist() == false) return false;
            double delta1, delta2;
            delta1 = y1 - y2;
            delta2 = y4 - y3;
            if (delta1 != delta2) return false;
            if (side(1) != side(3)) return false;

            return true;
        }
    }
    static class Program
    {



        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //стандартная инициализация программы, окна и прочео
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Window win = new Window();
            Application.Run(win);
            
        }


       public static int findMaxArea(quadrangle[] mass)
        {
            //нахождение максимальной площади в массиве четырехугольников
            //алогитм перебора всех элементов с поиском больше большего
            double max=0;
            int index=0;
            for(int i=0; i!=mass.Length; i++)
            {
                if (mass[i].area() > max)
                {
                    max = mass[i].area();
                    index = i;
                }
            }
            //возвращается индекс конечного элемента
            return index;
        }

        public static int findMinD(trapetion[] mass)
        {
            //нахождение минимальной диагонали в массиве трапеций
            //алгоритм перебора всех элементов с поиском меньше меньшего
            int index=0;
            double min=Double.MaxValue;
            for(int i=0; i!=mass.Length; i++)
            {
                if (min > mass[i].side(5))
                {
                    index = i;
                    min = mass[i].side(5);
                }
                if (min > mass[i].side(6))
                {
                    index = i;
                    min = mass[i].side(6);
                }
            }
            //возвращается индекс конечного элемента
            return index;
        }

    }
}
