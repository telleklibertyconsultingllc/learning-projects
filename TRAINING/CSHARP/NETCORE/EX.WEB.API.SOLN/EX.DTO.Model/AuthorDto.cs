using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EX.DTO.Model
{
    [AuthorNameMustBeDifferent(ErrorMessage = "Author and Main Category are the same")]
    public class AuthorDto /// : IValidatableObject
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }
        public int Age { get; set; }
        public string MainCategory { get; set; }
        public string Gender { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Name == Description)
        //    {
        //        yield return new ValidationResult(
        //            "The provided description should be different from the title",
        //            new[] { "AuthorDto" });
        //    }
        //}
    }
}
