using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;

        public Subscription(DateTime? expireDate)
        {
            Active = true;
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            _payments = new List<Payment>();
        }

        public bool Active { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

        public void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Inactivate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract<Subscription>()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscriptions.Payments", "A data de pagamento está invalida"));

            //if (IsValid) // Só pode adicionar se for valido
                _payments.Add(payment);
        }
    }
}