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
        }

        public string FirstName {get;set;}
        public string LastName {get;set;}

    }
}