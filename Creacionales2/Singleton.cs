//https://dotnetfiddle.net/juzHSJ
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Singleton en C#.
// Permite que solamente exista un objeto de un solo tipo y provee
// una única eruta de acceso a este.
//Github Nickname: SalverEndeik 

using System;

namespace RefactoringGuru.DesignPatterns.Singleton.Conceptual.NonThreadSafe
{
	//SLa clase Singleton previene que la clase seleccionada no pueda ser usada
	// por clases externas a esta misma.
    public sealed class Singleton
    {
		//  Se crea un constructor Singleton
        private Singleton() { }

        private static Singleton _instance;

		// Este método estático controla el acceso a la instancia Singleton. 
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

		// El Singleton se define con la lógica adecuada y se ejecuta 
		// en su propia instancia. 
        public void someBusinessLogic()
        {
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton funciona, ambas variables poseen la misma instancia");
            }
            else
            {
                Console.WriteLine("Singleton falló, ambas variables poseen diferentes instancias.");
            }
        }
    }
}
