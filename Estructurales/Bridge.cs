//https://dotnetfiddle.net/0ANIuG

//Lopez Partida Salvador Eli 19211670 13/05/2024 Objetivo: comprender el funcionamiento de Bridge en C#
//Descripción de funcionamiento: Permite dividir una estructura grande en jerarquias de clases pequeñas funcionan de forma indeoendiente 
//Github Nickname: SalverEndeik 
using System;

namespace RefactoringGuru.DesignPatterns.Bridge.Conceptual
{

	//Abstraction define la interfaz para la parte de control de la clase hierarchies
	//mientras mantiene de referencia un ovbjeto de implementacion que delega todo el trabajo
	//del objeto
    class Abstraction
    {
        protected IImplementation _implementation;
        
        public Abstraction(IImplementation implementation)
        {
            this._implementation = implementation;
        }
        
        public virtual string Operation()
        {
            return "Llegaste tarde joven.\n" + 
                _implementation.OperationImplementation();
        }
    }

    class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(IImplementation implementation) : base(implementation)
        {
        }
        
        public override string Operation()
        {
            return "Llegaste temprano joven.\n" +
                base._implementation.OperationImplementation();
        }
    }

	//Implementation define la interfaz para la implementación de todas las clases
	// ofreciendo operaciones primitivas, mientras que Abstraccion define operaciones}
	//de gran nivel.
    public interface IImplementation
    {
        string OperationImplementation();
    }

    class ConcreteITamalesCarne : IImplementation
    {
        public string OperationImplementation()
        {
            return "Solo hay: Tamales de carne.\n";
        }
    }

    class ConcreteTamalesElote : IImplementation
    {
        public string OperationImplementation()
        {
            return "Solo hay: Tamales de elote.\n";
        }
    }

    class Client
    {
		//El codigo cliente depende unicamente del código cliente, permitiendo
		// aoyar cualquier combinación de abstracción e implementación
        public void ClientCode(Abstraction abstraction)
        {
            Console.Write(abstraction.Operation());
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Abstraction abstraction;
            abstraction = new Abstraction(new  ConcreteITamalesCarne());
            client.ClientCode(abstraction);
            
            Console.WriteLine();
            
            abstraction = new ExtendedAbstraction(new ConcreteTamalesElote());
            client.ClientCode(abstraction);
        }
    }
}
