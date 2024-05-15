https://dotnetfiddle.net/aFlPtCv 
// Lopez Partida Salvador Eli 19211670 14/05/2024 Objetivo: comprender el funcionamiento de Proxy en C#.
// Proxy actua como sustituto de un servicio real, atendiendo las
// solicitudes del cliente para realizar tareas como control de acceso 
// y pasar la solicitud a un objeto de servicio. 
//Github Nickname: SalverEndeik 

using System;

namespace RefactoringGuru.DesignPatterns.Proxy.Conceptual
{
	//La interfaz Subject declara operaciones comunes para RealSubject y el Proxy.
	// No se debe dejar de trabajar con RealSubject o el Proxy no lo pasará como
	// un objeto real.
    public interface ISubject
    {
        void Request();
    }
    
	//Los RealSubjects pueden trabajar de forma muy lenta o sensible. Un Proxy
	// puede resolver estos problemas sin cambiar el código,
	
    class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }
    
	// Aquí se puede apreciar como Proxy
	// tiene una interfaz igual a la de RealSubjectSubjects 
    class Proxy : ISubject
    {
        private RealSubject _realSubject;
        
        public Proxy(RealSubject realSubject)
        {
            this._realSubject = realSubject;
        }
        
        public void Request()
        {
            if (this.CheckAccess())
            {
                this._realSubject.Request();

                this.LogAccess();
            }
        }
        
        public bool CheckAccess()
        {
            // Some real checks should go here.
            Console.WriteLine("Proxy: Revisando el acceso antes de lanzar una consulta real.");

            return true;
        }
        
        public void LogAccess()
        {
            Console.WriteLine("Proxy: Iniciando el tiempo de la consulta.");
        }
    }
    
    public class Client
    {
		//Con esta clase, se extiende el proxy para implementar
		// de forma más eficaz los patrones.
        public void ClientCode(ISubject subject)
        {

            
            subject.Request();
            

        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            
            Console.WriteLine("Client: Ejecutando el código cliente con un objeto real:");
            RealSubject realSubject = new RealSubject();
            client.ClientCode(realSubject);

            Console.WriteLine();

            Console.WriteLine("Client: Ejecutando el código cliente con un proxy:");
            Proxy proxy = new Proxy(realSubject);
            client.ClientCode(proxy);
        }
    }
}
