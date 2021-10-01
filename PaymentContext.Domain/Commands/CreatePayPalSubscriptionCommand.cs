using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PaymentNumber { get; set; }
        public string Email { get; set; }
        public string TransactionCode { get; set; }

        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }

        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerAddress { get; set; }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreatePayPalSubscriptionCommand>()
                .Requires()
                .IsGreaterThan(FirstName, 2, "CreatePayPalSubscriptionCommand.FirstName", "Nome deve ter ao menos 3 caracteres")
                .IsGreaterThan(LastName, 2, "CreatePayPalSubscriptionCommand.LastName", "Nome deve ter ao menos 3 caracteres")
            );
        }
    }
}