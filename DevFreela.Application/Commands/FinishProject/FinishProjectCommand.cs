﻿using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommand : IRequest<Unit>
    {
        public FinishProjectCommand(int id, string fullName, string creditCardNumber, string cvv, string expiresAt)
        {
            Id = id;
            FullName = fullName;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
        }

        public int Id { get; private set; }
        public string CreditCardNumber { get; private set; }
        public string Cvv { get; private set; }
        public string ExpiresAt { get; private set; }
        public string FullName { get; private set; }
    }
}
