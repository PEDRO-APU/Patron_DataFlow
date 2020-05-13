using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace CalculoIMC
{
    class Program
    {               
        static double calcularIMC(int peso, int altura)
        {
            double imc = 0;
            return imc = (peso / Math.Pow(altura, 2))*10000;  

        }
        static void Main(string[] args)
        {
            List<Task<double>> lista = new List<Task<double>>();
            for (int i = 0; i < 10; i++)
            {
                int peso = 0;
                int altura = 0;
                Task<double> t = Task.Factory.StartNew<double>(() =>
                {
                    peso = new Random().Next(1,50);//peso de 1 a 50 kg
                    altura = new Random().Next(1,150);//altura en cm 1 a 150

                    return calcularIMC(peso,altura);
                });
                lista.Add(t);
            }
            
            Task<double> finalTask = Task.Factory.ContinueWhenAll<double, double>(lista.ToArray(), tare => {
                double acum = 0;
                foreach (Task<double> t in tare)
                {
                    acum += t.Result;
                    Console.WriteLine("id {0}  IMC {1:0.00}", t.Id, t.Result );

                }
                return acum/tare.Length;
               
            });           

            Console.WriteLine("Promedio de la muestra de IMC {0:0.00}",finalTask.Result); 

            Console.ReadKey();     
        
                
                
        }
    }
}
