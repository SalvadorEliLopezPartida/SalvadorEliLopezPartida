//https://dotnetfiddle.net/nwSmhx
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Mediator en C#.
// reduce el acoplamiento entre los componentes de un programa haciendo 
// que se comuniquen indirectamente a través de un objeto mediador especial.
//Github Nickname: SalverEndeik 

using System;

namespace RefactoringGuru.DesignPatterns.Mediator.Conceptual
{
	//Se declara el método que usan los componentes para notificar al mediator
	// sobre diversos eventos que puedan ocurrir, reaccionando a estos
	// pasando la ejecución a otros componentes.
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }


	// Los Concrete Mediators poseen comportamiento cooperativo
	// a la hora de coordinar los componentes en sus operaciones
    class ConcreteMediator : IMediator
    {
        private Component1 _component1;

        private Component2 _component2;

        public ConcreteMediator(Component1 component1, Component2 component2)
        {
            this._component1 = component1;
            this._component1.SetMediator(this);
            this._component2 = component2;
            this._component2.SetMediator(this);
        } 

        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                Console.WriteLine("El cliente reacciona al acusado A y le pide a su abogado la siguiente información:");
                this._component2.DoC();
            }
            if (ev == "D")
            {
                Console.WriteLine("El cliente reacciona al acusado D y le pide a su abogado la siguiente información:");
                this._component1.DoB();
                this._component2.DoC();
            }
        }
    }

	// Esta parte del código guarda la instancia del mediator
	// dentro de los componentes del objeto
    class BaseComponent
    {
        protected IMediator _mediator;

        public BaseComponent(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }


    class Component1 : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("El sospechoso 1 es A");

            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("El sospechoso 1 es A");

            this._mediator.Notify(this, "B");
        }
    }

    class Component2 : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("El sospechoso 2 es C");

            this._mediator.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("El sospechoso 1 es d");

            this._mediator.Notify(this, "D");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {

            Component1 component1 = new Component1();
            Component2 component2 = new Component2();
            new ConcreteMediator(component1, component2);

            Console.WriteLine("El cliente llama al acusado A.");
            component1.DoA();

            Console.WriteLine();

            Console.WriteLine("El cliente llama al acusado D.");
            component2.DoD();
        }
    }
}
