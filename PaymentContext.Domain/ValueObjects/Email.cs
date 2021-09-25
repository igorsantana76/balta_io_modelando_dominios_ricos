using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    /*
    Aula 11
    Objetivo de criar um ValueObject é abstrair a complexidade,
    falta de confiança no codigo e repetição que pode se ter ao utilizar apenas 
    tipos primitivos que podem trazer
    */

    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract<Email>()
                .Requires()
                .IsEmail(Address, "Email.Address", "Email inválido")
            );
        }

        public string Address { get; set; }
    }
}