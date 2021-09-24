using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentClass
    {
        [TestMethod]
        public void AdicionarAssinatura()
        {
            var student = new Student(
                new Name("Igor", "Santana"), 
                new Document("000.000.000-00", EDocumentType.CPF), 
                new Email("igormax2008@gmail.com"),
                new Address("Rua Unica", "123", "Jardim Santos", "São Paulo", "SP", "Brasil", "00000-000")
                );

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