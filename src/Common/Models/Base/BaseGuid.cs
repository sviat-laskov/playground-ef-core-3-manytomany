using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models.Base
{
    public abstract class BaseGuid : IValidatableObject
    {
        [Required]
        public Guid Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Id == default) yield return new ValidationResult($"{nameof(Id)} is null.", new[] {nameof(Id)});
        }

        public Guid GenerateIdIfDefault()
        {
            if (Id == default) Id = Guid.NewGuid();

            return Id;
        }
    }
}