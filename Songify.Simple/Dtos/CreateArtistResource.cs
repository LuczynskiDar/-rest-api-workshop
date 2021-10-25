using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Songify.Simple.Dtos
{
    public class CreateArtistResource:IValidatableObject
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        public string Origin { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        
        // Custom validation, but data annotations are checked first
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Origin.Contains("XYZ"))
            {
                yield return new ValidationResult("Origin shouldn't contain XYZ",
                    new[]
                    {
                        nameof(Origin)
                    });
            }
        }
    }
}