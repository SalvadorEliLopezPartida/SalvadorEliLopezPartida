// https://dotnetfiddle.net/Nke5CE
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Facade en C#.
// Se utiliza para trabajar con bibliotecas y APIs de alta complejidad.
// Proporciona una interfaz compleja y limitada a un sistema complejo
// de bibliotecas, clases o _frameworks_.
//Github Nickname: SalverEndeik 

using System;

namespace RefactoringGuru.DesignPatterns.Facade.Conceptual
{
	//Facade provee una interfaz simple para el complejo lógico de varios
	//subsistemas. Delega las llamadas del cliente hacía los objetos
	//apropiados dentro del mismo subsistema.
    public class Facade
    {
        protected Subsystem1 _subsystem1;
        
        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
        {
            this._subsystem1 = subsystem1;
            this._subsystem2 = subsystem2;
        }
        
		// Facade solo ofrece al cliente una fracción de las capacidades
		// del sistema.
        public string Operation()
        {
            string result = "Facade initializes subsystems:\n";
            result += this._subsystem1.operation1();
            result += this._subsystem2.operation1();
            result += "Facade orders subsystems to perform the action:\n";
            result += this._subsystem1.operationN();
            result += this._subsystem2.operationZ();
            return result;
        }
    }
    
	// Para el subsistema, Facade se vuelve otro cliente que no forma parte
	// del subsistema
    public class Subsystem1
    {
        public string operation1()
        {
            return "Subsystem1: ¡Genial!!\n";
        }

        public string operationN()
        {
            return "Subsystem1: ¡Hora de comer!\n";
        }
    }
    
    public class Subsystem2
    {
        public string operation1()
        {
            return "Subsystem2: ¡Asombroso!\n";
        }

        public string operationZ()
        {
            return "Subsystem2: ¡Gracias ya comí!\n";
        }
    }


    class Client
    {
        public static void ClientCode(Facade facade)
        {
            Console.Write(facade.Operation());
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
			// Facade inicializa los objetos que posee el subsistema en vez 
			// de crear nuevas instancias.
            Subsystem1 subsystem1 = new Subsystem1();
            Subsystem2 subsystem2 = new Subsystem2();
            Facade facade = new Facade(subsystem1, subsystem2);
            Client.ClientCode(facade);
        }
    }
}
