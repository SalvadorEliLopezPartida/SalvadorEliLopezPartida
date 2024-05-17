//https://dotnetfiddle.net/hF97Ji
// Lopez Partida Salvador Eli 19211670 16/05/2024 Objetivo: comprender el funcionamiento de Visitor en C#.
// Permite añadir nuevos comportamientos a una jerarquía de clases existente sin alterar el código.
//Github Nickname: SalverEndeik 

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Visitor.Conceptual
{
	//Se declara 'accept' como un método para que la base visitor
	// lo use como argumento
    public interface IComponent
    {
        void Accept(IVisitor visitor);
    }

	// Cada comoponente de Concrete implementa 'accept' como un método
	//para llamar al método de visitor correspondiende a la clase del componente
    public class ConcreteComponentA : IComponent
    {
		// Con esta clase el visitor conoce la clase de componente 
		// con la que está trabajando
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentA(this);
        }

        public string ExclusiveMethodOfConcreteComponentA()
        {
            return "A";
        }
    }

    public class ConcreteComponentB : IComponent
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentB(this);
        }

        public string SpecialMethodOfConcreteComponentB()
        {
            return "B";
        }
    }

	// En la interfaz de Visitor se declaran métodos visitantes que corresponden
	// a las clases de componentes a usar.El visitar un método ayuda a queel visitor
	// identifique la clase exacta con la que el componente está trabajando. 
    public interface IVisitor
    {
        void VisitConcreteComponentA(ConcreteComponentA element);

        void VisitConcreteComponentB(ConcreteComponentB element);
    }

    // Los Concrete Visitors implementan varias versiones de un mismo algoritmo
	// para trabajar con todas las clases concrete
  
    class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor1");
        }
    }

    class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor2");
        }
    }

    public class Client
    {
		// Se puede correr cualquier set de elementos en las operaciones del visitor
		// sin tener que usar sus clases cocnrete
        public static void ClientCode(List<IComponent> components, IVisitor visitor)
        {
            foreach (var component in components)
            {
                component.Accept(visitor);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<IComponent> components = new List<IComponent>
            {
                new ConcreteComponentA(),
                new ConcreteComponentB()
            };

            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
            var visitor1 = new ConcreteVisitor1();
            Client.ClientCode(components,visitor1);

            Console.WriteLine();

            Console.WriteLine("It allows the same client code to work with different types of visitors:");
            var visitor2 = new ConcreteVisitor2();
            Client.ClientCode(components, visitor2);
        }
    }
}
