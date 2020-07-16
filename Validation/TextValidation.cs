using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GamingReviews.Validation
{
    class TextValidation:ValidationRule
    {
        public string Type { get; set; }

        // TODO validation things
        // -if another article with the name exists
        // ...

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (String.IsNullOrWhiteSpace(value as string))
                return new ValidationResult(false, "Field cant be empty");
            else
            switch (Type)
            {
                case "Header":
                    if ((value as string).Length > 20)
                        return new ValidationResult(false, "Header cant be longer than 20 cahracters");
                    break;
                case "Content":
                    if ((value as string).Length > 200)
                        return new ValidationResult(false, "Content cant be longer than 200 cahracters");
                    break;
            }
            return new ValidationResult(true, null);
        }
    }
}

