//https://dotnetfiddle.net/Ow4VU5
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Builder en C#.
// Permite crear diferentes productos con el mismo proceso de construcción
//de forma directa con un constructor.
//Github Nickname: SalverEndeik 

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Builder.Conceptual
{
	// En esta parte del código se especifican los métodos para crear diferentes
	// Productos objeto
    public interface IBuilder
    {
        void BuildPartA();
        
        void BuildPartB();
        
        void BuildPartC();
    }
    
	// Concrete Builder provee a la interfaz Builder de implementación
	// de pasos para construir el programa con variaciones de Builders.
    public class ConcreteBuilder : IBuilder
    {
        private Product _product = new Product();
        
		// Una instancia nueva de Builder debe tener un objeto en blanco para
		// su unión posterior.
        public ConcreteBuilder()
        {
            this.Reset();
        }
        
        public void Reset()
        {
            this._product = new Product();
        }
        
		// Como se puede ver en el código, todos los pasos de producción
		// trabajan con la misma instancia del producto.
        public void BuildPartA()
        {
            this._product.Add("PartA1");
        }
        
        public void BuildPartB()
        {
            this._product.Add("PartB1");
        }
        
        public void BuildPartC()
        {
            this._product.Add("PartC1");
        }

        public Product GetProduct()
        {
            Product result = this._product;

            this.Reset();

            return result;
        }
    }
    
	// Los concrete builders pueden crear productos que no esten relacionados,
	// haciendo que no todos los resultados de disrtintos builders 
	// posean la misma interfaz.
    public class Product
    {
        private List<object> _parts = new List<object>();
        
        public void Add(string part)
        {
            this._parts.Add(part);
        }
        
        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); 

            return "Product parts: " + str + "\n";
        }
    }
    
    public class Director
    {
        private IBuilder _builder;
        
        public IBuilder Builder
        {
            set { _builder = value; } 
        }
        
		// Con esta parte del código se crean diferentes variantes
		// de un mismo paso de construcción.
        public void BuildMinimalViableProduct()
        {
            this._builder.BuildPartA();
        }
        
        public void BuildFullFeaturedProduct()
        {
            this._builder.BuildPartA();
            this._builder.BuildPartB();
            this._builder.BuildPartC();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
			//El código cliente crea un objeto builder, que pasa al director
			// para iniciar el proceso de construcción. Al final, 
			// el resultado se retira del objeto builder.
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;
            
            Console.WriteLine("Standard basic product:");
            director.BuildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standard full featured product:");
            director.BuildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Custom product:");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.Write(builder.GetProduct().ListParts());
        }
    }
}
