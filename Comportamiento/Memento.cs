//https://dotnetfiddle.net/aQOWHD
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Memento en C#.
// Permite tomar instantáneas del estado de un objeto y restaurarlo en el futuro.
//Github Nickname: SalverEndeik 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RefactoringGuru.DesignPatterns.Memento.Conceptual
{

	// Define al método para que guarde el estado dentro de
	// un memento y otro método para restaurar su estado.
    class Originator
    {
		// Para que sea más fácil trabajar con ello, el estado de origen 
		// se guarda dentro de una misma variable.
        private string _state;

        public Originator(string state)
        {
            this._state = state;
            Console.WriteLine("Originator: My initial state is: " + state);
        }

		//  Mediante el método save() se puede crear un respaldo de la
		// información antes de ejecutar otros métodos.
        public void DoSomething()
        {
            Console.WriteLine("Originator: Estoy haciendo cosas importantes. Shu.");
            this._state = this.GenerateRandomString(30);
            Console.WriteLine($"Originator: de repente mí estado cambio a: {_state}");
        }

        private string GenerateRandomString(int length = 10) //se definen los símbolos con los que puede trabajar, en este caso el abecedario completo
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        // Guarda el estado actual dentro del memento
        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
        }

        // Restaura a su estado original mediante el uso de Memento
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Memento Desconocido. " + memento.ToString());
            }

            this._state = memento.GetState();
            Console.Write($"Originator: Mí estado ha cambiado a:{_state}");
        }
    }

    public interface IMemento
    {
        string GetName();

        string GetState();

        DateTime GetDate();
    }

    class ConcreteMemento : IMemento
    {
        private string _state;

        private DateTime _date;

        public ConcreteMemento(string state) //con esta parte se resguarda el estado original
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        // Originator usa este método para restaurar su estado.
        public string GetState()
        {
            return this._state;
        }
        
        public string GetName()
        {
            return $"{this._date} / ({this._state.Substring(0, 9)})...";
        }

        public DateTime GetDate() //se pueden utilizar información como fechas y nombres con el memento
        {
            return this._date;
        }
    }

    class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            this._originator = originator;
        }

        public void Backup()
        {
            Console.WriteLine("\nCaretaker: Guardando el estado del Originator...");
            this._mementos.Add(this._originator.Save());
        }

        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            Console.WriteLine("Caretaker: Reestableciendo el estado a: " + memento.GetName());

            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("Caretaker: Aquí tienes una lista de memorias, digo, mementos:");

            foreach (var memento in this._mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Client code.
            Originator originator = new Originator("Super-duper-super-puper-super.");
            Caretaker caretaker = new Caretaker(originator);

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            Console.WriteLine();
            caretaker.ShowHistory();

            Console.WriteLine("\nClient: ¡Es hora de viajar en el tiempo!!\n");
            caretaker.Undo();

            Console.WriteLine("\n\nClient:¡Anda! ¡Una vez más!\n");
            caretaker.Undo();

            Console.WriteLine();
        }
    }
}
