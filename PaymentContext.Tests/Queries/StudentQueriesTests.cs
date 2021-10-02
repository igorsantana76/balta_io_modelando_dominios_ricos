using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System.Collections.Generic;
using PaymentContext.Domain.Queries;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();

            for (var i = 0; i <= 100; i++)
            {
                _students.Add(
                    new Student(
                        new Name("Igor", "Santana"),
                        new Document("1111111111" + i, EDocumentType.CPF),
                        new Email(i + "@foobarbaz.com"),
                        new Address("", "", "", "", "", "", "")
                    )
                );
            }
        }

        [TestMethod]
        public void ShoudReturnNullWhenDocumentNotExists()
        {
            var getStudentExpression = StudentQueries.GetStudentInfoByDocument("91111111111");
            var student = _students.AsQueryable().Where(getStudentExpression).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        
        [TestMethod]
        public void ShoudReturnStudentWhenDocumentExists()
        {
            var getStudentExpression = StudentQueries.GetStudentInfoByDocument("11111111119");
            var student = _students.AsQueryable().Where(getStudentExpression).FirstOrDefault();

            Assert.AreEqual("11111111119", student.Document.Number);
        }

    }
}