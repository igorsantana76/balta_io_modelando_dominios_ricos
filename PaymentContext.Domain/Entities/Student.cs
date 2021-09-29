using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email, Address address)
        {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email, address);
        }

        // Dica: S(O)LID - Open-Close Principle
        // Principio do aberto-fechado
        // Toda modificação de propriedade precisa passar por algo que regule a regra

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }

        // Anticorrupção de codigo, proibe que codigos fujam da regra
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = Subscriptions.Any(a => a.Active == true);

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa.")
                .IsGreaterThan(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Esta assinatura não possui pagamentos")
            );

            //if (IsValid)
            _subscriptions.Add(subscription);

            // Alternativa
            // if (hasSubscriptionActive)
            //     AddNotification("Student.Subscriptions", "Você já possui uma assinatura ativa");
        }
    }
}