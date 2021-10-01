using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable<Notification>,
        IHandler<CreateBoletoSubscriptionCommand>
        //, IHandler<CreatePayPalSubscriptionCommand> TODO, implementar outros handlers, validações e etc
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        // Injeção de dependencia por construtor
        // Ou seja, para que o SubscriptionHandler funcione
        // é necessario que o repository seja passado
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validate();

            if (command.IsValid == false)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar a sua assinatura");
            }

            // Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Esse CPF já está em uso");

            // Verificar se e-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Document", "Esse Email já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.PayerDocument, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood,
                command.City, command.State, command.Country, command.ZipCode);

            // Gerar as Entidades
            var student = new Student(name, document, email, address);

            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new BoletoPayment(
                command.Barcode, 
                command.BoletoNumber,
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid,
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Aplicar as Validações

            // Salvar as Informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo a plataforma", "Sua assinatura foi criada!");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}