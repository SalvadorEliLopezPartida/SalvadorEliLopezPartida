https://dotnetfiddle.net/FQbxMx
//Lopez Partida Salvador Eli 19211670 13/05/2024 Objetivo: comprender el funcionamiento de Composite en C#.
//Permite componer elementos en estructura de árbol para trabajar con ellos 
//como si fuera un solo objeto.
//Github Nickname: SalverEndeik 
using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Composite.Conceptual
{
	//Se crea la clase Component donde se declaran operaciones para objetos
	//simples y complejos para crear la composición
    abstract class Component
    {
        public Component() { }

        // The base Component may implement some default behavior or leave it to
        // concrete classes (by declaring the method containing the behavior as
        // "abstract").
		//Se declara el comportamiento principal como abstract	
        public abstract string Operation();


        public virtual void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Component component)
        {
            throw new NotImplementedException();
        }

		//Este método ayuda a saber si los componentes pueden tener
		//un componente hijo
        public virtual bool IsComposite()
        {
            return true;
        }
    }


	//La clase hoja representa donde termina una composición. Una hoja no
	//puede tener hijos. 
    class Leaf : Component
    {
        public override string Operation()
        {
            return "Leaf";
        }

        public override bool IsComposite()
        {
            return false;
        }
    }

    // The Composite class represents the complex components that may have
    // children. Usually, the Composite objects delegate the actual work to
    // their children and then "sum-up" the result.
	// La clase Composite representa componentes complejos que pueden tener hijos.
    class Composite : Component
    {
        protected List<Component> _children = new List<Component>();
        
        public override void Add(Component component)
        {
            this._children.Add(component);
        }

        public override void Remove(Component component)
        {
            this._children.Remove(component);
        }

		// Cuando Composite ejecuta su lóogica principal, viaja
		// de forma recursiva a través de todos sus hijos para recolectar y 
		// sumar los resultados, atravesando por todo el árbol.
        public override string Operation()
        {
            int i = 0;
            string result = "Branch(";

            foreach (Component component in this._children)
            {
                result += component.Operation();
                if (i != this._children.Count - 1)
                {
                    result += "+";
                }
                i++;
            }
            
            return result + ")";
        }
    }

    class Client
    {
        // The client code works with all of the components via the base
        // interface.
		// El código del cliente sirve con todos los componentes mediante
		// una interfaz.
        public void ClientCode(Component leaf)
        {
            Console.WriteLine($"RESULT: {leaf.Operation()}\n");
        }

		// Al declarar las operaciones hijo-managament en la clase base Component,
		// el código puede trabajar con cualquier componente simple o complejo.
        public void ClientCode2(Component component1, Component component2)
        {
            if (component1.IsComposite())
            {
                component1.Add(component2);
            }
            
            Console.WriteLine($"RESULT: {component1.Operation()}");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Leaf leaf = new Leaf();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(leaf);

            Composite tree = new Composite();
            Composite branch1 = new Composite();
            branch1.Add(new Leaf());
            branch1.Add(new Leaf());
            Composite branch2 = new Composite();
            branch2.Add(new Leaf());
            tree.Add(branch1);
            tree.Add(branch2);
            Console.WriteLine("Client: Now I've got a composite tree:");
            client.ClientCode(tree);

            Console.Write("Client: I don't need to check the components classes even when managing the tree:\n");
            client.ClientCode2(tree, leaf);
        }
    }
}
