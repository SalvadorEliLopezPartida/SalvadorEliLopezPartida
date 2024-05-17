//https://dotnetfiddle.net/7CoK6X
// Lopez Partida Salvador Eli 19211670 16/05/2024 Objetivo: comprender el funcionamiento de State en C#.
// Le da la habilidad a un objeto para cambiar de comportamiento cuando cambia su estado interno.
//Github Nickname: SalverEndeik 

using System;

namespace RefactoringGuru.DesignPatterns.State.Conceptual
{
	// La clase Context representa el estado actual del Contexto si se dan cambios.
    class Context
    {
        // Referencia actual de Context
        private State _state = null;

        public Context(State state)
        {
            this.TransitionTo(state);
        }

        // Permite el cambio del estado actual de objeto de Context
        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

		// Context brinda una parte de su comportamiento al estado actual del objeto
        public void Request1()
        {
            this._state.Handle1();
        }

        public void Request2()
        {
            this._state.Handle2();
        }
    }
    

    abstract class State
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            this._context = context;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }

	// El estado de Concrete implementa comportamientos asociados al estado de Context 
    class ConcreteStateA : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA handles request1.");
            Console.WriteLine("ConcreteStateA wants to change the state of the context.");
            this._context.TransitionTo(new ConcreteStateB());
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA handles request2.");
        }
    }

    class ConcreteStateB : State
    {
        public override void Handle1()
        {
            Console.Write("ConcreteStateB handles request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB handles request2.");
            Console.WriteLine("ConcreteStateB wants to change the state of the context.");
            this._context.TransitionTo(new ConcreteStateA());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            var context = new Context(new ConcreteStateA());
            context.Request1();
            context.Request2();
        }
    }
}
