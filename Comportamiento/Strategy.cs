//https://dotnetfiddle.net/CmGwKl
// Lopez Partida Salvador Eli 19211670 16/05/2024 Objetivo: comprender el funcionamiento de Strategy en C#.
// Se encarga de convertir solicitudes y operaciones simples en objetos.
//Github Nickname: SalverEndeik 

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Strategy.Conceptual
{

    class Context
    {
		//La clase Context ayuda a referenciar los objetos con los que Strategy
		//puede trabajar, siendo todos mediante el uso de uina Interfaz dedicada a Strategy
        private IStrategy _strategy;

        public Context()
        { }
		// Para que la Clase Context pueda aceptar un Strategy, es mediante 
		// un constructor-
        public Context(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }


        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");
            var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        { 
		//sortea la información contenida dentro de Strategy
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
			// Esta parte se usa para obtener una estrategia y pasarla al contexto sigueinte.
			// Para hacer la elección correcta,
			// se deben conocer las diferencias entre Strategies.
            var context = new Context();

            Console.WriteLine("Client: Strategy is set to normal sorting.");
            context.SetStrategy(new ConcreteStrategyA());
            context.DoSomeBusinessLogic();
            
            Console.WriteLine();
            
            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            context.SetStrategy(new ConcreteStrategyB());
            context.DoSomeBusinessLogic();
        }
    }
}
