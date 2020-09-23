using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiniteIntegral
{
    class IntegralCalculate
    {
        public static double Integral(double a, double b, int N)
        {
            double h = (b - a) / N;
            double x;
            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                x = a + i * h;
                sum += Math.Pow(x,4);
            }

            return sum * h;
        }

        public static Task<double> Integral4CPUAsync(double a, double b, int N, IProgress<int> progress)
        {
            Task<double> taskMain = Task<double>.Factory.StartNew(() =>
            {
                Task<double> task1 = new Task<double>(
                    () =>
                    {
                        double h = ((b / 4) - a) / (N / 4);
                        double x;
                        double sum = 0;
                        for (int i = 0; i < (N / 4); i++)
                        {
                            x = a + i * h;
                            sum += Math.Pow(x,4);
                            if (i % 1000000 == 0)
                            progress.Report((int)(i / (double)(N / 4) * 100));
                        }

                        return sum * h;
                    });
                task1.Start();

                Task<double> task2 = Task<double>.Factory.StartNew(() =>
                {
                    return Integral(b / 4, b / 2, N / 4);
                });

                Task<double> task3 = Task<double>.Factory.StartNew(() =>
                {
                    return Integral(b / 2, b * 3.0 / 4.0, N / 4);
                });

                Task<double> task4 = Task<double>.Factory.StartNew(() =>
                {
                    return Integral(b * 3.0 / 4.0, b, N / 4);
                });

                Task.WaitAll(new[] { task1, task2, task3, task4 });
                return task1.Result + task2.Result + task3.Result + task4.Result;
            });

            return taskMain;
        }
    }
}
