https://dotnetfiddle.net/NTStqL
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Factory en C#.
// Se utiliza para la creación de objetods y cuando queremos tener un alto
// nivel de flexibilidad en nuestro código. 
//Github Nickname: SalverEndeik 
using System;

namespace RefactoringGuru.DesignPatterns.FactoryMethod.Conceptual
{
	//Se declara el método Factory que regresará una Clase Producto.
    abstract class Creator
    {

        public abstract IProduct FactoryMethod();

        public string SomeOperation()
        {
			//Se manda a llamar al método Factory para crear un Producto Objeto
            var product = FactoryMethod();
            
			//Ahora se usa el producto
            var result = "¡Sí funcionó el código que saque de internet! Debes ver esto: "
                + product.Operation();

            return result;
        }
    }


    class ConcreteCreator1 : Creator
    {
		//Se regresa el Producto Concreto del método
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }

    class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }

	// Iproduct declara las operaciones que el producto concreto
	// va a implementar
    public interface IProduct
    {
        string Operation();
    }

    class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }

    class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }

    class Client
    {
        public void Main()
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new ConcreteCreator1());
            
            Console.WriteLine("");

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new ConcreteCreator2());
        }

		// Mientras el código cliente siga en una instancia de Concrete Creator.
		// mediante la interfaz base, seguirá pasando como una subclase del creador
        public void ClientCode(Creator creator)
        {
            // ...
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" + creator.SomeOperation());
            // ...
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
