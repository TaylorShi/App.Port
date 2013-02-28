namespace App.Json
{
    public class JsonException : System.ApplicationException
    {
        public JsonException()
        {
        }

        internal JsonException(ParserToken token) :
            base(System.String.Format(
                "Invalid token '{0}' in input string", token))
        {
        }

        internal JsonException(ParserToken token,
                               System.Exception innerException) :
                                   base(System.String.Format(
                                       "Invalid token '{0}' in input string", token),
                                        innerException)
        {
        }

        internal JsonException(int c) :
            base(System.String.Format(
                "Invalid character '{0}' in input string", (char) c))
        {
        }

        internal JsonException(int c, System.Exception innerException) :
            base(System.String.Format(
                "Invalid character '{0}' in input string", (char) c),
                 innerException)
        {
        }


        public JsonException(string message)
            : base(message)
        {
        }

        public JsonException(string message, System.Exception innerException) :
            base(message, innerException)
        {
        }
    }
}