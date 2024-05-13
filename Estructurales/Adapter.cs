//https://dotnetfiddle.net/hRl66o
//Lopez Partida Salvador Eli 19211670 13/05/2024 Objetivo: comprender el funcionamiento de Adapter en C#
//Actua como puente entre 2 objetos. Las llamadas se transportan en un objeto
//y se transforma en una interfaz reconocible para el segundo objeto
//Github Nickname: SalverEndeik 
using System;

namespace RefactoringGuru.DesignPatterns.Adapter.Conceptual
{
	// Target define del dominio específico que usara el código cliente
    public interface ITarget
    {
        string GetRequest();
    }

	// Adaptee contiene acciones funncionales pero este debe adaptarse
	// al código cliente para poder usarse.
    class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "El Himno Nacional";
        }
    }

	//Adapter hace usable la interfaz de Adaptee con la interfaz Target
    class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            this._adaptee = adaptee;
        }

        public string GetRequest()
        {
            return $"Esto es:'{this._adaptee.GetSpecificRequest()}'";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Adaptee adaptee = new Adaptee();
            ITarget target = new Adapter(adaptee);

            Console.WriteLine("Maseosare");
            Console.WriteLine("Un extraño enemigo");

            Console.WriteLine(target.GetRequest()); //combina la frase de GetSpecificRequest con GetRequest
        }
    }
}
