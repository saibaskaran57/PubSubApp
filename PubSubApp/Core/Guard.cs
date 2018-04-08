namespace Core
{
    using System;

    public static class Guard
    {
        public static void RequiresNotNull([ValidatedNotNull]object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void RequiresNotEmpty([ValidatedNotNull]string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw (value == null) 
                    ? new ArgumentNullException(parameterName) 
                    : new ArgumentException("String was empty or whitespace", parameterName);
            }
        }

        public static void RequiresPredicate(bool predicate, string parameterName, string message = "Parameter didn't satisfy requirements")
        {
            if (!predicate)
            {
                throw new ArgumentException(message, parameterName);
            }
        }
    }
}
