using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EX.DTO.Model
{
    public class AuthorNameMustBeDifferent : ValidationAttribute
    {
        public AuthorNameMustBeDifferent()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var author = (AuthorDto)validationContext.ObjectInstance;
            if (author.Name == author.MainCategory)
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(author) });
            }
            return ValidationResult.Success;
        }
    }
}
