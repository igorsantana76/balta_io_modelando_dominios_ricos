using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    /*
    Aula 11
    Objetivo de criar um ValueObject é abstrair a complexidade,
    falta de confiança no codigo e repetição que pode se ter ao utilizar apenas 
    tipos primitivos que podem trazer
    "São Objetos de Valor que compõem uma entidade"
    */

    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsGreaterThan(FirstName, 2, "Name.FirstName", "Nome deve ter ao menos 3 caracteres")
                .IsGreaterThan(LastName, 2, "Name.LastName", "Nome deve ter ao menos 3 caracteres")
                .IsLowerThan(FirstName, 100, "Name.FirstName", "Seu nome não pode ter mais de 100 caracteres")
            );
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}