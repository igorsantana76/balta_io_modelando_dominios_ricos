using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentClass
    {
        [TestMethod]
        public void AdicionarAssinatura()
        {
            var student = new Student("Igor", "Santana", "000.000.000-00", "igormax2008@gmail.com");

            // Dica 1: (S)OLID = Single Responsability Principle
            // Problema: arquivo, classe ou metodo com mais de um objetivo
            // Solução: Cada item deve somente fazer uma coisa, deve-se isolar o maximo
            // Cada um no seu quadrado

            // Errado:
            //student.Subscriptions.Add(new Subscription());

            // Correto:
            //student.AddSubscription();
        }
    }
}