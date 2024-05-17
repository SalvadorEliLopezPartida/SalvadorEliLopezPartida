//https://dotnetfiddle.net/CNKHui
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Command en C#.
// Se encarga de convertir solicitudes y operaciones simples en objetos.
//Github Nickname: SalverEndeik 
using System;

namespace RefactoringGuru.DesignPatterns.Command.Conceptual
{
	
    public interface ICommand
    {
        void Execute(); // Se declara un método en la interfaz principal para ejecutar comandos.
    }

    class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;

        public SimpleCommand(string payload)
        {
            this._payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: Puedo hacer cosas sencillas como hablar en inglés. ({this._payload})");
        }
    }


    class ComplexCommand : ICommand
    {
        private Receiver _receiver; // Receiver es un comando capaz 
		// de realizar operaciones complejas


   //Se crean datos para que el receiver pueda ser utilizado.
        private string _a; 
        private string _b;

		// Sí se da el contexto suficiente, se pueden aceptar más de un 
		// comando complejo via el constructor.
        public ComplexCommand(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }

		//Los comandos pueden delegar cualquier métodod e un receiver
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Déjale las cosas complejas a un receiver");
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }


    class Receiver //Contiene la lógica necesaria para resolver operaciones
	// que se asocien a cualquier pedido.
	
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Trabajando con:({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: También trabajamos con:({b}.)");
        }
    }


    class Invoker //Se asocia con uno o más comandos y es quien envía
				  // el pedido a estos.
    {
        private ICommand _onStart;

        private ICommand _onFinish;

        // Initialize commands.s
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }

        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }

        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Antes de iniciar, ¿quieren algo de la tienda?");
            if (this._onStart is ICommand)
            {
                this._onStart.Execute();
            }
            
            Console.WriteLine("Invoker: ...haciendo cosas importantes, enserio...");
            
            Console.WriteLine("Invoker: Volví de la tienda. ¿Seguros que no querían algo?");
            if (this._onFinish is ICommand)
            {
                this._onFinish.Execute();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
			//El código del cliente puede darle parametros al invoker con cualquier comando.
            Invoker invoker = new Invoker();
            invoker.SetOnStart(new SimpleCommand("Pattern design!"));
            Receiver receiver = new Receiver();
            invoker.SetOnFinish(new ComplexCommand(receiver, "Ventanilla 9.", "Rellenando máquina de refrescos."));

            invoker.DoSomethingImportant();
        }
    }
}
