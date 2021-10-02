using System;
using System.Linq.Expressions;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Queries
{
    public static class StudentQueries
    {
        // Forma de armazenar Queries
        public static Expression<Func<Student, bool>> GetStudentInfoByDocument(string document)
        {
            return x => x.Document.Number == document;
        }
        
    }
}