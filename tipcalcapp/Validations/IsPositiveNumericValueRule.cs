namespace tipcalcapp.Validations
{
    public class IsPositiveNumericValueRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var strVal = value as string;

            if (decimal.TryParse(strVal, out decimal decVal))
            {
                return (decVal > 0) ? true : false;
            }
            else
            {
                return false;
            }
            
        }
    }
}
