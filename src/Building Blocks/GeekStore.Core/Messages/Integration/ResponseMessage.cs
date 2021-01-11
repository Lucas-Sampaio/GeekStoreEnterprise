using FluentValidation.Results;

namespace GeekStore.Core.Messages.Integration
{
    public class ResponseMessage : Message
    {
        public ValidationResult ValidationResult { get; set; }

        public ResponseMessage(ValidationResult validationResult)
        {
            this.ValidationResult = validationResult;
        }
    }
}
