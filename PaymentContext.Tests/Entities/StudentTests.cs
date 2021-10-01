using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Student _student;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("35111507795", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _address = new Address("Rua 1", "1234", "Bairro Legal", "Gotham", "SP", "BR", "13400000");
            _student = new Student(_name, _document, _email, _address);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);
            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);

            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);
            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            // Dica 1: (S)OLID = Single Responsability Principle
            // Problema: arquivo, classe ou metodo com mais de um objetivo
            // Solução: Cada item deve somente fazer uma coisa, deve-se isolar o maximo
            // Cada um no seu quadrado

            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);
            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            if (_student.Notifications.Count > 0)
                Console.WriteLine(string.Join(", ", _student.Notifications.Select(a => a.Key + " " + a.Message).ToList()));

            Assert.IsTrue(_student.IsValid);
        }
    }
}

// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using PaymentContext.Domain.Entities;
// using PaymentContext.Domain.ValueObjects;
// using PaymentContext.Domain.Enums;
// using System;

// namespace PaymentContext.Tests
// {
//     [TestClass]
//     public class StudentTest
//     {
//         [TestMethod]
//         public void ShouldReturnErrorWhenHadActiveSubscriptionHasNoPayment()
//         {
//             var name = new Name("João", "Kleber");
//             var document = new Document("85782525002", EDocumentType.CPF);
//             var email = new Email("joaokleber@record.com.br");
//             var address = new Address("Rua Foo", "1", "Bairro Top", "SP", "SP", "BR", "12331-000");
//             var student = new Student(name, document, email, address);

//             var subscription = new Subscription(null);
//             var payment = new PayPalPayment("1282139873", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Record", document, address, email);

//             subscription.AddPayment(payment);
//             student.AddSubscription(subscription);
//             student.AddSubscription(subscription);

//             Assert.IsFalse(!student.IsValid);
//         }

//         [TestMethod]
//         public void ShouldReturnSuccessWhenHadActiveSubscription()
//         {
//             Assert.Fail();
//         }

//         //[TestMethod]
//         // public void AdicionarAssinatura()
//         // {
//         //     var student = new Student(
//         //         new Name("", ""), 
//         //         new Document("000.000.000-00", EDocumentType.CPF), 
//         //         new Email("igormax2008@gmail.com"),
//         //         new Address("Rua Unica", "123", "Jardim Santos", "São Paulo", "SP", "Brasil", "00000-000")
//         //     );

//         //     foreach(var item in student.Notifications){
//         //         Console.WriteLine(item.Message);                
//         //     }

//         //     // Dica 1: (S)OLID = Single Responsability Principle
//         //     // Problema: arquivo, classe ou metodo com mais de um objetivo
//         //     // Solução: Cada item deve somente fazer uma coisa, deve-se isolar o maximo
//         //     // Cada um no seu quadrado

//         //     // Errado:
//         //     //student.Subscriptions.Add(new Subscription());

//         //     // Correto:
//         //     //student.AddSubscription();
//         // }
//     }
// }