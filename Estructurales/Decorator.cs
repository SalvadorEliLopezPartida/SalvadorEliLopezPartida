https://dotnetfiddle.net/wBuCEB
//Lopez Partida Salvador Eli 19211670 13/05/2024 Objetivo: comprender el funcionamiento de Decorator en C#.
//Permiten envolver un mismo objeto una cantidad determinada de veces dada por el
//usuario, dando como resultado un objeto con comportamiendo de apilado envuelto.
//Github Nickname: SalverEndeik 
using System;

namespace RefactoringGuru.DesignPatterns.Composite.Conceptual
{
	//La interfaz Component base define las operaciones
	//que pueden ser alteradas por decorators
    public abstract class Component
    {
        public abstract string Operation();
    }

	// El Concrete Component provee implementaciones para las 
	// operaciones. Debido a alas clases, estos pueden presentar variaciones.
    class ConcreteComponent : Component
    {
        public override string Operation()
        {
            return "ConcreteComponent";
        }
    }

	// La clase base Decorator define la interfaz de envoltura para todos
	// los decorator concretos. La implementación por defecto del código de 
	// envoltura incluye un campo para guardar un comentario envuelto para 
	// inicializarlo.
    abstract class Decorator : Component
    {
        protected Component _component;

        public Decorator(Component component)
        {
            this._component = component;
        }

        public void SetComponent(Component component)
        {
            this._component = component;
        }

		// Decorator manda todo el trabajo al componente envuelto.
        public override string Operation()
        {
            if (this._component != null)
            {
                return this._component.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(Component comp) : base(comp)
        {
        }

		// En vez de llamar a la implementación padre, llaman directamnente
		// al objeto envuelto.
        public override string Operation()
        {
            return $"ConcreteDecoratorA({base.Operation()})";
        }
	}
	
	// Los Decorator pueden ejecutar su comportamiento antes o después
	// de llamar al objeto envuelto. 
    class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(Component comp) : base(comp)
        {
        }

        public override string Operation()
        {
            return $"ConcreteDecoratorB({base.Operation()})";
        }
    }
    
    public class Client
    {
     
        public void ClientCode(Component component)
        {
            Console.WriteLine("RESULT: " + component.Operation());
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            var simple = new ConcreteComponent();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(simple);
            Console.WriteLine();

			//Los decorators pueden envolver no solamente componentes, sino
			// también otros cdecorators 
            ConcreteDecoratorA decorator1 = new ConcreteDecoratorA(simple);
            ConcreteDecoratorB decorator2 = new ConcreteDecoratorB(decorator1);
            Console.WriteLine("Client: Now I've got a decorated component:");
            client.ClientCode(decorator2);
        }
    }
}
