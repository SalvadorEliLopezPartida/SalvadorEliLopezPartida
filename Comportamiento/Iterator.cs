//https://dotnetfiddle.net/y0KI4n

// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Iterator en C#.
// Permite el recorrido secuencial por una estructura de datos
// compleja sin exponer sus detalles internos.
//Github Nickname: SalverEndeik

using System;
using System.Collections;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Iterator.Conceptual
{
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        // Iterator Retorna la llave de este elemento
        public abstract int Key();
        
        // Retorna el elemento actual
        public abstract object Current();
        
        // Se mueve al elemento siguiente
        public abstract bool MoveNext();
        
        // Regresa al primer elemento
        public abstract void Reset();
    }

    abstract class IteratorAggregate : IEnumerable
	{

		// Regresa un Iterator al objeto implementado
        public abstract IEnumerator GetEnumerator();
    }

    // Concrete Iterators implement various traversal algorithms. These classes
    // store the current traversal position at all times.
    class AlphabeticalOrderIterator : Iterator
    {
        private WordsCollection _collection;
       
		
        private int _position = -1; //Guarda la posición actual del movimiento
        
        private bool _reverse = false;

        public AlphabeticalOrderIterator(WordsCollection collection, bool reverse = false)
        {
            this._collection = collection;
            this._reverse = reverse;

            if (reverse)
            {
                this._position = collection.getItems().Count;
            }
        }
        
        public override object Current()
        {
            return this._collection.getItems()[_position];
        }

        public override int Key()
        {
            return this._position;
        }
        
        public override bool MoveNext()
        {
            int updatedPosition = this._position + (this._reverse ? -1 : 1); //cuenta la posición en la que se hizo el movimiento para actualizarlo.

            if (updatedPosition >= 0 && updatedPosition < this._collection.getItems().Count)
            {
                this._position = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public override void Reset()
        {
            this._position = this._reverse ? this._collection.getItems().Count - 1 : 0;
        }
    }

    class WordsCollection : IteratorAggregate
    {
        List<string> _collection = new List<string>();
        
        bool _direction = false;
        
        public void ReverseDirection()
        {
            _direction = !_direction;
        }
        
        public List<string> getItems()
        {
            return _collection;
        }
        
        public void AddItem(string item)
        {
            this._collection.Add(item);
        }
        
        public override IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, _direction);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var collection = new WordsCollection();
            collection.AddItem("First");
            collection.AddItem("Second");
            collection.AddItem("Third");

            Console.WriteLine("Straight traversal:"); //código para mover el iterator hacia el frente 

            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("\nReverse traversal:"); //código para mover el iterator hacia atrás 

            collection.ReverseDirection();

            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }
        }
    }
}
