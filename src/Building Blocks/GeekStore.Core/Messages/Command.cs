﻿using FluentValidation.Results;
using MediatR;
using System;

namespace GeekStore.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public Command()
        {
            Timestamp = DateTime.Now;
        }
        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
