using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.Utils
{
    class ModelUtils
    {
        public static void Validate(object model)
        {
            var context = new ValidationContext(model);
            Validator.ValidateObject(model, context, true);
        }
    }
}
