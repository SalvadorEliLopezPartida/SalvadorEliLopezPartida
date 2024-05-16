//https://dotnetfiddle.net/uLJTrh
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Abstract Factory en C#.
// Define una interfaz con la cual se pueden crear productos donde el código cliente invoca
// las formas en las que se puede crear un objeto de fabrica en vez de hacerlo
//de forma directa con un constructor.
//Github Nickname: SalverEndeik 

using System;

namespace RefactoringGuru.DesignPatterns.AbstractFactory.Conceptual
{

	// Se declara en la interfaz de Abstral Factory los métodos
	// que regresaran diferentes productos abstractos.
    public interface IAbstractFactory
    {
        IAbstractProductA CreateProductA();

        IAbstractProductB CreateProductB();
    }

	//Se crea un producto perteneciente a una variable, garantizando
	// que los productos sean compatibles.
    class ConcreteFactory1 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA1();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB1();
        }
    }

	// Todas las clases de aquí poseen una variante producto.
    class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA2();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB2();
        }
    }

    // Each distinct product of a product family should have a base interface.
    // All variants of the product must implement this interface.
	//Se crea una interfaz 
    public interface IAbstractProductA
    {
        string UsefulFunctionA();
    }

    // Se crean Concrete Productos dentro de Concrete Factories para 
	// su llamado posterior
    class ConcreteProductA1 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A1.";
        }
    }

    class ConcreteProductA2 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A2.";
        }
    }

	//La interfaz base permite que todos los productos interactuen siendo
	// estos, de la misma variante de Concrete.
    public interface IAbstractProductB
    {
        // Product B is able to do its own thing...
        string UsefulFunctionB();

    //Esta línea ayuda a que los productos sean compatibles
        string AnotherUsefulFunctionB(IAbstractProductA collaborator);
    }

    // Concrete Products are created by corresponding Concrete Factories.
    class ConcreteProductB1 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B1.";
        }

		 // La variante Product B1	solo funciona con cualquier variante de 
		// Product A1 y es capaz de aceptar sus instancias como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B1 collaborating with the ({result})";
        }
    }

    class ConcreteProductB2 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B2.";
        }

	   //Al igual que la Variante B1, B2 solo trabaja con cualquier variante
	   // de A2, aceptando cualquier instancia de esta como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B2 collaborating with the ({result})";
        }
    }

	//Esta parte del código ayuda a pasar cualquier subclase del cliente 
	// si  necesidad de que el código se rompa y poder trabajar de forma abstracta
	//con AbstractFactory y AbstractProduct.
    class Client
    {
        public void Main()
        {
            // The client code can work with any concrete factory class.
            Console.WriteLine("Client: Testing client code with the first factory type...");
            ClientMethod(new ConcreteFactory1());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the second factory type...");
            ClientMethod(new ConcreteFactory2());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var productA = factory.CreateProductA();
            var productB = factory.CreateProductB();

            Console.WriteLine(productB.UsefulFunctionB());
            Console.WriteLine(productB.AnotherUsefulFunctionB(productA));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}
