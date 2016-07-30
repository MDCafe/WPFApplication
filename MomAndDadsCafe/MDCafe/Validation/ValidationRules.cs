using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MDCafe.Validation
{
    public class ValidateComboxRule : ValidationRule
    {
        //public object itemsColln { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            //var itemObject = value as item;

            //if (itemObject == null)
            //    return new ValidationResult(false, "Invalid Time");

            if (value is item)
                return new ValidationResult(true,null);


            return new ValidationResult(false,"Invalid item");
        }
    }
}
