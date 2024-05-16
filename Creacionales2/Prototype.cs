//https://dotnetfiddle.net/GY9gwx
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Abstract Factory en C#.
// Permite la clonación de objetos tanto simples como complejos, esto
// sin necesidad de permanecer en una clase específica.
//Github Nickname: SalverEndeik 

using System;

namespace RefactoringGuru.DesignPatterns.Prototype.Conceptual
{
    public class Person
    {
        public int Age;
        public DateTime BirthDate;
        public string Name;
        public IdInfo IdInfo;

        public Person ShallowCopy()
        {
            return (Person) this.MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person clone = (Person) this.MemberwiseClone();
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);
            clone.Name = String.Copy(Name);
            return clone;
        }
    }
		//se crea la clase con la que se creará
	// el id para identificar los objetos
    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            this.IdNumber = idNumber;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            p1.Age = 20;
            p1.BirthDate = Convert.ToDateTime("2008-07-09");
            p1.Name = "Salvador";
            p1.IdInfo = new IdInfo(666);

			//Copia el objeto de p1 a p2
            Person p2 = p1.ShallowCopy();
			//Copia el objeto de p1 y se  manda a p3
            Person p3 = p1.DeepCopy();

            //Muestra los valores de p1, p2 y p3
            Console.WriteLine("Valores originales de p1, p2, p3:");
            Console.WriteLine("   p1 Datos: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 Datos:");
            DisplayValues(p2);
            Console.WriteLine("   p3 Datos:");
            DisplayValues(p3);

			// Cambia el valor de p1 y sus propiedades y muestra de nuevo
			// los valores de p1, p2 y p3.
            p1.Age = 23;
            p1.BirthDate = Convert.ToDateTime("2000-11-03");
            p1.Name = "Elizabeth";
            p1.IdInfo.IdNumber = 1670;
            Console.WriteLine("\nDatos de p1, p2 and p3 después de modificar p1:");
            Console.WriteLine("   p1 Datos: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 Datos(los datos de referencia cambiaron):");
            DisplayValues(p2);
            Console.WriteLine("   p3 Datos(todos los datos se quedaron igual):");
            DisplayValues(p3);
        }

        public static void DisplayValues(Person p)
        {
            Console.WriteLine("      Nombre: {0:s}, Edad: {1:d}, Decha de nacimiento: {2:MM/dd/yy}",
                p.Name, p.Age, p.BirthDate);
            Console.WriteLine("      ID#: {0:d}", p.IdInfo.IdNumber);
        }
    }
}
