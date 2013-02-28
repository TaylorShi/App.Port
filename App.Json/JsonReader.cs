namespace App.Json
{
    public enum JsonToken
    {
        None,

        ObjectStart,
        PropertyName,
        ObjectEnd,

        ArrayStart,
        ArrayEnd,

        Int,
        Long,
        Double,

        String,

        Boolean,
        Null
    }

    public class JsonReader
    {
        #region Fields

        private static System.Collections.Generic.IDictionary<int, System.Collections.Generic.IDictionary<int, int[]>>
            _parseTable;

        private readonly System.Collections.Generic.Stack<int> _automatonStack;
        private int _currentInput;
        private int _currentSymbol;
        private readonly Lexer _lexer;
        private bool _parserInString;
        private bool _parserReturn;
        private bool _readStarted;
        private System.IO.TextReader _reader;
        private readonly bool _readerIsOwned;
        private JsonToken _token;

        #endregion

        #region Public Properties

        public bool AllowComments
        {
            get { return _lexer.AllowComments; }
            set { _lexer.AllowComments = value; }
        }

        public bool AllowSingleQuotedStrings
        {
            get { return _lexer.AllowSingleQuotedStrings; }
            set { _lexer.AllowSingleQuotedStrings = value; }
        }

        public bool EndOfInput { get; private set; }

        public bool EndOfJson { get; private set; }

        public JsonToken Token
        {
            get { return _token; }
        }

        public object Value { get; private set; }

        #endregion

        #region Constructors

        static JsonReader()
        {
            PopulateParseTable();
        }

        public JsonReader(string jsonText) :
            this(new System.IO.StringReader(jsonText), true)
        {
        }

        public JsonReader(System.IO.TextReader reader) :
            this(reader, false)
        {
        }

        private JsonReader(System.IO.TextReader reader, bool owned)
        {
            if (reader == null)
                throw new System.ArgumentNullException("reader");

            _parserInString = false;
            _parserReturn = false;

            _readStarted = false;
            _automatonStack = new System.Collections.Generic.Stack<int>();
            _automatonStack.Push((int) ParserToken.End);
            _automatonStack.Push((int) ParserToken.Text);

            _lexer = new Lexer(reader);

            EndOfInput = false;
            EndOfJson = false;

            _reader = reader;
            _readerIsOwned = owned;
        }

        #endregion

        #region Static Methods

        private static void PopulateParseTable()
        {
            _parseTable =
                new System.Collections.Generic.Dictionary<int, System.Collections.Generic.IDictionary<int, int[]>>();

            TableAddRow(ParserToken.Array);
            TableAddCol(ParserToken.Array, '[',
                        '[',
                        (int) ParserToken.ArrayPrime);

            TableAddRow(ParserToken.ArrayPrime);
            TableAddCol(ParserToken.ArrayPrime, '"',
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest,
                        ']');
            TableAddCol(ParserToken.ArrayPrime, '[',
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest,
                        ']');
            TableAddCol(ParserToken.ArrayPrime, ']',
                        ']');
            TableAddCol(ParserToken.ArrayPrime, '{',
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest,
                        ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.Number,
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest,
                        ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.True,
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest,
                        ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.False,
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest,
                        ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.Null,
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest,
                        ']');

            TableAddRow(ParserToken.Object);
            TableAddCol(ParserToken.Object, '{',
                        '{',
                        (int) ParserToken.ObjectPrime);

            TableAddRow(ParserToken.ObjectPrime);
            TableAddCol(ParserToken.ObjectPrime, '"',
                        (int) ParserToken.Pair,
                        (int) ParserToken.PairRest,
                        '}');
            TableAddCol(ParserToken.ObjectPrime, '}',
                        '}');

            TableAddRow(ParserToken.Pair);
            TableAddCol(ParserToken.Pair, '"',
                        (int) ParserToken.String,
                        ':',
                        (int) ParserToken.Value);

            TableAddRow(ParserToken.PairRest);
            TableAddCol(ParserToken.PairRest, ',',
                        ',',
                        (int) ParserToken.Pair,
                        (int) ParserToken.PairRest);
            TableAddCol(ParserToken.PairRest, '}',
                        (int) ParserToken.Epsilon);

            TableAddRow(ParserToken.String);
            TableAddCol(ParserToken.String, '"',
                        '"',
                        (int) ParserToken.CharSeq,
                        '"');

            TableAddRow(ParserToken.Text);
            TableAddCol(ParserToken.Text, '[',
                        (int) ParserToken.Array);
            TableAddCol(ParserToken.Text, '{',
                        (int) ParserToken.Object);

            TableAddRow(ParserToken.Value);
            TableAddCol(ParserToken.Value, '"',
                        (int) ParserToken.String);
            TableAddCol(ParserToken.Value, '[',
                        (int) ParserToken.Array);
            TableAddCol(ParserToken.Value, '{',
                        (int) ParserToken.Object);
            TableAddCol(ParserToken.Value, (int) ParserToken.Number,
                        (int) ParserToken.Number);
            TableAddCol(ParserToken.Value, (int) ParserToken.True,
                        (int) ParserToken.True);
            TableAddCol(ParserToken.Value, (int) ParserToken.False,
                        (int) ParserToken.False);
            TableAddCol(ParserToken.Value, (int) ParserToken.Null,
                        (int) ParserToken.Null);

            TableAddRow(ParserToken.ValueRest);
            TableAddCol(ParserToken.ValueRest, ',',
                        ',',
                        (int) ParserToken.Value,
                        (int) ParserToken.ValueRest);
            TableAddCol(ParserToken.ValueRest, ']',
                        (int) ParserToken.Epsilon);
        }

        private static void TableAddCol(ParserToken row, int col,
                                        params int[] symbols)
        {
            _parseTable[(int) row].Add(col, symbols);
        }

        private static void TableAddRow(ParserToken rule)
        {
            _parseTable.Add((int) rule, new System.Collections.Generic.Dictionary<int, int[]>());
        }

        #endregion

        #region Private Methods

        private void ProcessNumber(string number)
        {
            if (number.IndexOf('.') != -1 ||
                number.IndexOf('e') != -1 ||
                number.IndexOf('E') != -1)
            {
                double nDouble;
                if (System.Double.TryParse(number, out nDouble))
                {
                    _token = JsonToken.Double;
                    Value = nDouble;

                    return;
                }
            }

            int nInt32;
            if (System.Int32.TryParse(number, out nInt32))
            {
                _token = JsonToken.Int;
                Value = nInt32;

                return;
            }

            long nInt64;
            if (System.Int64.TryParse(number, out nInt64))
            {
                _token = JsonToken.Long;
                Value = nInt64;

                return;
            }

            // Shouldn't happen, but just in case, return something
            _token = JsonToken.Int;
            Value = 0;
        }

        private void ProcessSymbol()
        {
            switch (_currentSymbol)
            {
                case '[':
                    _token = JsonToken.ArrayStart;
                    _parserReturn = true;
                    break;
                case ']':
                    _token = JsonToken.ArrayEnd;
                    _parserReturn = true;
                    break;
                case '{':
                    _token = JsonToken.ObjectStart;
                    _parserReturn = true;
                    break;
                case '}':
                    _token = JsonToken.ObjectEnd;
                    _parserReturn = true;
                    break;
                case '"':
                    if (_parserInString)
                    {
                        _parserInString = false;

                        _parserReturn = true;
                    }
                    else
                    {
                        if (_token == JsonToken.None)
                            _token = JsonToken.String;

                        _parserInString = true;
                    }
                    break;
                case (int) ParserToken.CharSeq:
                    Value = _lexer.StringValue;
                    break;
                case (int) ParserToken.False:
                    _token = JsonToken.Boolean;
                    Value = false;
                    _parserReturn = true;
                    break;
                case (int) ParserToken.Null:
                    _token = JsonToken.Null;
                    _parserReturn = true;
                    break;
                case (int) ParserToken.Number:
                    ProcessNumber(_lexer.StringValue);
                    _parserReturn = true;
                    break;
                case (int) ParserToken.Pair:
                    _token = JsonToken.PropertyName;
                    break;
                case (int) ParserToken.True:
                    _token = JsonToken.Boolean;
                    Value = true;
                    _parserReturn = true;
                    break;
            }
        }

        private bool ReadToken()
        {
            if (EndOfInput)
                return false;

            _lexer.NextToken();

            if (_lexer.EndOfInput)
            {
                Close();

                return false;
            }

            _currentInput = _lexer.Token;

            return true;
        }

        #endregion

        public void Close()
        {
            if (EndOfInput)
                return;

            EndOfInput = true;
            EndOfJson = true;

            if (_readerIsOwned)
                _reader.Close();

            _reader = null;
        }

        public bool Read()
        {
            if (EndOfInput)
                return false;

            if (EndOfJson)
            {
                EndOfJson = false;
                _automatonStack.Clear();
                _automatonStack.Push((int) ParserToken.End);
                _automatonStack.Push((int) ParserToken.Text);
            }

            _parserInString = false;
            _parserReturn = false;

            _token = JsonToken.None;
            Value = null;

            if (!_readStarted)
            {
                _readStarted = true;

                if (!ReadToken())
                    return false;
            }


            while (true)
            {
                if (_parserReturn)
                {
                    if (_automatonStack.Peek() == (int) ParserToken.End)
                        EndOfJson = true;

                    return true;
                }

                _currentSymbol = _automatonStack.Pop();

                ProcessSymbol();

                if (_currentSymbol == _currentInput)
                {
                    if (!ReadToken())
                    {
                        if (_automatonStack.Peek() != (int) ParserToken.End)
                            throw new JsonException(
                                "Input doesn't evaluate to proper JSON text");

                        return _parserReturn;
                    }

                    continue;
                }

                int[] entrySymbols;
                try
                {
                    entrySymbols =
                        _parseTable[_currentSymbol][_currentInput];
                }
                catch (System.Collections.Generic.KeyNotFoundException e)
                {
                    throw new JsonException((ParserToken) _currentInput, e);
                }

                if (entrySymbols[0] == (int) ParserToken.Epsilon)
                    continue;

                for (var i = entrySymbols.Length - 1; i >= 0; i--)
                    _automatonStack.Push(entrySymbols[i]);
            }
        }
    }
}