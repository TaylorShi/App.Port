using System.Linq;

namespace App.Json
{
    public class JsonData : IJsonWrapper, System.IEquatable<JsonData>
    {
        #region Fields

        private System.Collections.Generic.IList<JsonData> _instArray;
        private bool _instBoolean;
        private double _instDouble;
        private int _instInt;
        private long _instLong;
        private System.Collections.Generic.IDictionary<string, JsonData> _instObject;
        private string _instString;
        private string _json;
        private JsonType _type;

        // Used to implement the IOrderedDictionary interface
        private System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<string, JsonData>> object_list;

        #endregion

        #region Properties

        public int Count
        {
            get { return EnsureCollection().Count; }
        }

        public bool IsArray
        {
            get { return _type == JsonType.Array; }
        }

        public bool IsBoolean
        {
            get { return _type == JsonType.Boolean; }
        }

        public bool IsDouble
        {
            get { return _type == JsonType.Double; }
        }

        public bool IsInt
        {
            get { return _type == JsonType.Int; }
        }

        public bool IsLong
        {
            get { return _type == JsonType.Long; }
        }

        public bool IsObject
        {
            get { return _type == JsonType.Object; }
        }

        public bool IsString
        {
            get { return _type == JsonType.String; }
        }

        public System.Collections.Generic.IDictionary<System.String, JsonData> Inst_Object
        {
            get { return _type == JsonType.Object ? _instObject : null; }
        }

        #endregion

        #region ICollection Properties

        int System.Collections.ICollection.Count
        {
            get { return Count; }
        }

        bool System.Collections.ICollection.IsSynchronized
        {
            get { return EnsureCollection().IsSynchronized; }
        }

        object System.Collections.ICollection.SyncRoot
        {
            get { return EnsureCollection().SyncRoot; }
        }

        #endregion

        #region IDictionary Properties

        bool System.Collections.IDictionary.IsFixedSize
        {
            get { return EnsureDictionary().IsFixedSize; }
        }

        bool System.Collections.IDictionary.IsReadOnly
        {
            get { return EnsureDictionary().IsReadOnly; }
        }

        System.Collections.ICollection System.Collections.IDictionary.Keys
        {
            get
            {
                EnsureDictionary();
                System.Collections.Generic.IList<string> keys = object_list.Select(entry => entry.Key).ToList();

                return (System.Collections.ICollection) keys;
            }
        }

        System.Collections.ICollection System.Collections.IDictionary.Values
        {
            get
            {
                EnsureDictionary();
                System.Collections.Generic.IList<JsonData> values = object_list.Select(entry => entry.Value).ToList();

// ReSharper disable SuspiciousTypeConversion.Global
                return (System.Collections.ICollection) values;
// ReSharper restore SuspiciousTypeConversion.Global
            }
        }

        #endregion

        #region IJsonWrapper Properties

        bool IJsonWrapper.IsArray
        {
            get { return IsArray; }
        }

        bool IJsonWrapper.IsBoolean
        {
            get { return IsBoolean; }
        }

        bool IJsonWrapper.IsDouble
        {
            get { return IsDouble; }
        }

        bool IJsonWrapper.IsInt
        {
            get { return IsInt; }
        }

        bool IJsonWrapper.IsLong
        {
            get { return IsLong; }
        }

        bool IJsonWrapper.IsObject
        {
            get { return IsObject; }
        }

        bool IJsonWrapper.IsString
        {
            get { return IsString; }
        }

        #endregion

        #region IList Properties

        bool System.Collections.IList.IsFixedSize
        {
            get { return EnsureList().IsFixedSize; }
        }

        bool System.Collections.IList.IsReadOnly
        {
            get { return EnsureList().IsReadOnly; }
        }

        #endregion

        #region IDictionary Indexer

        object System.Collections.IDictionary.this[object key]
        {
            get { return EnsureDictionary()[key]; }

            set
            {
                if (!(key is System.String))
                    throw new System.ArgumentException(
                        "The key has to be a string");

                var data = ToJsonData(value);

                this[(string) key] = data;
            }
        }

        #endregion

        #region IOrderedDictionary Indexer

        object System.Collections.Specialized.IOrderedDictionary.this[int idx]
        {
            get
            {
                EnsureDictionary();
                return object_list[idx].Value;
            }

            set
            {
                EnsureDictionary();
                var data = ToJsonData(value);

                var oldEntry = object_list[idx];

                _instObject[oldEntry.Key] = data;

                var entry =
                    new System.Collections.Generic.KeyValuePair<string, JsonData>(oldEntry.Key, data);

                object_list[idx] = entry;
            }
        }

        #endregion

        #region IList Indexer

        object System.Collections.IList.this[int index]
        {
            get { return EnsureList()[index]; }

            set
            {
                EnsureList();
                var data = ToJsonData(value);

                this[index] = data;
            }
        }

        #endregion

        #region Public Indexers

        public JsonData this[string propName]
        {
            get
            {
                EnsureDictionary();
                return _instObject[propName];
            }

            set
            {
                EnsureDictionary();

                var entry =
                    new System.Collections.Generic.KeyValuePair<string, JsonData>(propName, value);

                if (_instObject.ContainsKey(propName))
                {
                    for (var i = 0; i < object_list.Count; i++)
                    {
                        if (object_list[i].Key != propName) continue;
                        object_list[i] = entry;
                        break;
                    }
                }
                else
                    object_list.Add(entry);

                _instObject[propName] = value;

                _json = null;
            }
        }

        public JsonData this[int index]
        {
            get
            {
                EnsureCollection();

                return _type == JsonType.Array ? _instArray[index] : object_list[index].Value;
            }

            set
            {
                EnsureCollection();

                if (_type == JsonType.Array)
                    _instArray[index] = value;
                else
                {
                    var entry = object_list[index];
                    var newEntry =
                        new System.Collections.Generic.KeyValuePair<string, JsonData>(entry.Key, value);

                    object_list[index] = newEntry;
                    _instObject[entry.Key] = value;
                }

                _json = null;
            }
        }

        #endregion

        #region Constructors

        public JsonData()
        {
        }

        public JsonData(bool boolean)
        {
            _type = JsonType.Boolean;
            _instBoolean = boolean;
        }

        public JsonData(double number)
        {
            _type = JsonType.Double;
            _instDouble = number;
        }

        public JsonData(int number)
        {
            _type = JsonType.Int;
            _instInt = number;
        }

        public JsonData(long number)
        {
            _type = JsonType.Long;
            _instLong = number;
        }

        public JsonData(object obj)
        {
            if (obj is System.Boolean)
            {
                _type = JsonType.Boolean;
                _instBoolean = (bool) obj;
                return;
            }

            if (obj is System.Double)
            {
                _type = JsonType.Double;
                _instDouble = (double) obj;
                return;
            }

            if (obj is System.Int32)
            {
                _type = JsonType.Int;
                _instInt = (int) obj;
                return;
            }

            if (obj is System.Int64)
            {
                _type = JsonType.Long;
                _instLong = (long) obj;
                return;
            }

            var s = obj as string;
            if (s != null)
            {
                _type = JsonType.String;
                _instString = s;
                return;
            }

            throw new System.ArgumentException(
                "Unable to wrap the given object with JsonData");
        }

        public JsonData(string str)
        {
            _type = JsonType.String;
            _instString = str;
        }

        #endregion

        #region Implicit Conversions

        public static implicit operator JsonData(System.Boolean data)
        {
            return new JsonData(data);
        }

        public static implicit operator JsonData(System.Double data)
        {
            return new JsonData(data);
        }

        public static implicit operator JsonData(System.Int32 data)
        {
            return new JsonData(data);
        }

        public static implicit operator JsonData(System.Int64 data)
        {
            return new JsonData(data);
        }

        public static implicit operator JsonData(System.String data)
        {
            return new JsonData(data);
        }

        #endregion

        #region Explicit Conversions

        public static explicit operator System.Boolean(JsonData data)
        {
            if (data._type != JsonType.Boolean)
                throw new System.InvalidCastException(
                    "Instance of JsonData doesn't hold a double");

            return data._instBoolean;
        }

        public static explicit operator System.Double(JsonData data)
        {
            if (data._type != JsonType.Double && data._type != JsonType.Int && data._type != JsonType.Long)
                throw new System.InvalidCastException(
                    "Instance of JsonData doesn't hold a double");

            return data._instDouble;
        }

        public static explicit operator System.Int32(JsonData data)
        {
            if (data._type != JsonType.Int)
                throw new System.InvalidCastException(
                    "Instance of JsonData doesn't hold an int");

            return data._instInt;
        }

        public static explicit operator System.Int64(JsonData data)
        {
            if (data._type != JsonType.Long && data._type != JsonType.Int)
                throw new System.InvalidCastException(
                    "Instance of JsonData doesn't hold an int");

            return data._instLong;
        }

        public static explicit operator System.String(JsonData data)
        {
            if (data._type != JsonType.String)
                throw new System.InvalidCastException(
                    "Instance of JsonData doesn't hold a string");

            return data._instString;
        }

        #endregion

        #region ICollection Methods

        void System.Collections.ICollection.CopyTo(System.Array array, int index)
        {
            EnsureCollection().CopyTo(array, index);
        }

        #endregion

        #region IDictionary Methods

        void System.Collections.IDictionary.Add(object key, object value)
        {
            var data = ToJsonData(value);

            EnsureDictionary().Add(key, data);

            var entry =
                new System.Collections.Generic.KeyValuePair<string, JsonData>((string) key, data);
            object_list.Add(entry);

            _json = null;
        }

        void System.Collections.IDictionary.Clear()
        {
            EnsureDictionary().Clear();
            object_list.Clear();
            _json = null;
        }

        bool System.Collections.IDictionary.Contains(object key)
        {
            return EnsureDictionary().Contains(key);
        }

        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
        {
            return ((System.Collections.Specialized.IOrderedDictionary) this).GetEnumerator();
        }

        void System.Collections.IDictionary.Remove(object key)
        {
            EnsureDictionary().Remove(key);

            for (var i = 0; i < object_list.Count; i++)
            {
                if (object_list[i].Key != (string) key) continue;
                object_list.RemoveAt(i);
                break;
            }

            _json = null;
        }

        #endregion

        #region IEnumerable Methods

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return EnsureCollection().GetEnumerator();
        }

        #endregion

        #region IJsonWrapper Methods

        bool IJsonWrapper.GetBoolean()
        {
            if (_type != JsonType.Boolean)
                throw new System.InvalidOperationException(
                    "JsonData instance doesn't hold a boolean");

            return _instBoolean;
        }

        double IJsonWrapper.GetDouble()
        {
            if (_type != JsonType.Double)
                throw new System.InvalidOperationException(
                    "JsonData instance doesn't hold a double");

            return _instDouble;
        }

        int IJsonWrapper.GetInt()
        {
            if (_type != JsonType.Int)
                throw new System.InvalidOperationException(
                    "JsonData instance doesn't hold an int");

            return _instInt;
        }

        long IJsonWrapper.GetLong()
        {
            if (_type != JsonType.Long)
                throw new System.InvalidOperationException(
                    "JsonData instance doesn't hold a long");

            return _instLong;
        }

        string IJsonWrapper.GetString()
        {
            if (_type != JsonType.String)
                throw new System.InvalidOperationException(
                    "JsonData instance doesn't hold a string");

            return _instString;
        }

        void IJsonWrapper.SetBoolean(bool val)
        {
            _type = JsonType.Boolean;
            _instBoolean = val;
            _json = null;
        }

        void IJsonWrapper.SetDouble(double val)
        {
            _type = JsonType.Double;
            _instDouble = val;
            _json = null;
        }

        void IJsonWrapper.SetInt(int val)
        {
            _type = JsonType.Int;
            /************
             * CYF 2010Äê7ÔÂ21ÈÕ
            *************/
            _instDouble = _instLong = _instInt = val;
            _json = null;
        }

        void IJsonWrapper.SetLong(long val)
        {
            _type = JsonType.Long;
            /************
             * CYF 2010Äê7ÔÂ21ÈÕ
            *************/
            _instDouble = _instLong = val;
            _json = null;
        }

        void IJsonWrapper.SetString(string val)
        {
            _type = JsonType.String;
            _instString = val;
            _json = null;
        }

        string IJsonWrapper.ToJson()
        {
            return ToJson();
        }

        void IJsonWrapper.ToJson(JsonWriter writer)
        {
            ToJson(writer);
        }

        #endregion

        #region IList Methods

        int System.Collections.IList.Add(object value)
        {
            return Add(value);
        }

        void System.Collections.IList.Clear()
        {
            EnsureList().Clear();
            _json = null;
        }

        bool System.Collections.IList.Contains(object value)
        {
            return EnsureList().Contains(value);
        }

        int System.Collections.IList.IndexOf(object value)
        {
            return EnsureList().IndexOf(value);
        }

        void System.Collections.IList.Insert(int index, object value)
        {
            EnsureList().Insert(index, value);
            _json = null;
        }

        void System.Collections.IList.Remove(object value)
        {
            EnsureList().Remove(value);
            _json = null;
        }

        void System.Collections.IList.RemoveAt(int index)
        {
            EnsureList().RemoveAt(index);
            _json = null;
        }

        #endregion

        #region IOrderedDictionary Methods

        System.Collections.IDictionaryEnumerator System.Collections.Specialized.IOrderedDictionary.GetEnumerator()
        {
            EnsureDictionary();

            return new OrderedDictionaryEnumerator(
                object_list.GetEnumerator());
        }

        void System.Collections.Specialized.IOrderedDictionary.Insert(int idx, object key, object value)
        {
            var property = (string) key;
            var data = ToJsonData(value);

            this[property] = data;

            var entry =
                new System.Collections.Generic.KeyValuePair<string, JsonData>(property, data);

            object_list.Insert(idx, entry);
        }

        void System.Collections.Specialized.IOrderedDictionary.RemoveAt(int idx)
        {
            EnsureDictionary();

            _instObject.Remove(object_list[idx].Key);
            object_list.RemoveAt(idx);
        }

        #endregion

        #region Private Methods

        private System.Collections.ICollection EnsureCollection()
        {
            if (_type == JsonType.Array)
// ReSharper disable SuspiciousTypeConversion.Global
                return (System.Collections.ICollection) _instArray;
// ReSharper restore SuspiciousTypeConversion.Global

            if (_type == JsonType.Object)
// ReSharper disable SuspiciousTypeConversion.Global
                return (System.Collections.ICollection) _instObject;
// ReSharper restore SuspiciousTypeConversion.Global

            throw new System.InvalidOperationException(
                "The JsonData instance has to be initialized first");
        }

        private System.Collections.IDictionary EnsureDictionary()
        {
            if (_type == JsonType.Object)
// ReSharper disable SuspiciousTypeConversion.Global
                return (System.Collections.IDictionary) _instObject;
// ReSharper restore SuspiciousTypeConversion.Global

            if (_type != JsonType.None)
                throw new System.InvalidOperationException(
                    "Instance of JsonData is not a dictionary");

            _type = JsonType.Object;
            _instObject = new System.Collections.Generic.Dictionary<string, JsonData>();
            object_list =
                new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, JsonData>>();

// ReSharper disable SuspiciousTypeConversion.Global
            return (System.Collections.IDictionary) _instObject;
// ReSharper restore SuspiciousTypeConversion.Global
        }

        private System.Collections.IList EnsureList()
        {
            if (_type == JsonType.Array)
// ReSharper disable SuspiciousTypeConversion.Global
                return (System.Collections.IList) _instArray;
// ReSharper restore SuspiciousTypeConversion.Global

            if (_type != JsonType.None)
                throw new System.InvalidOperationException(
                    "Instance of JsonData is not a list");

            _type = JsonType.Array;
            _instArray = new System.Collections.Generic.List<JsonData>();

// ReSharper disable SuspiciousTypeConversion.Global
            return (System.Collections.IList) _instArray;
// ReSharper restore SuspiciousTypeConversion.Global
        }

        private JsonData ToJsonData(object obj)
        {
            if (obj == null)
                return null;

            var data = obj as JsonData;
            return data ?? new JsonData(obj);
        }

        private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
        {
            if (obj.IsString)
            {
                writer.Write(obj.GetString());
                return;
            }

            if (obj.IsBoolean)
            {
                writer.Write(obj.GetBoolean());
                return;
            }

            if (obj.IsDouble)
            {
                writer.Write(obj.GetDouble());
                return;
            }

            if (obj.IsInt)
            {
                writer.Write(obj.GetInt());
                return;
            }

            if (obj.IsLong)
            {
                writer.Write(obj.GetLong());
                return;
            }

            if (obj.IsArray)
            {
                writer.WriteArrayStart();
                foreach (var elem in (System.Collections.IList) obj)
                    WriteJson((JsonData) elem, writer);
                writer.WriteArrayEnd();

                return;
            }

            if (!obj.IsObject) return;
            writer.WriteObjectStart();

            foreach (System.Collections.DictionaryEntry entry in ((System.Collections.IDictionary) obj))
            {
                writer.WritePropertyName((string) entry.Key);
                WriteJson((JsonData) entry.Value, writer);
            }
            writer.WriteObjectEnd();
        }

        #endregion

        public int Add(object value)
        {
            var data = ToJsonData(value);

            _json = null;

            return EnsureList().Add(data);
        }

        public void Clear()
        {
            if (IsObject)
            {
                ((System.Collections.IDictionary) this).Clear();
                return;
            }

            if (IsArray)
            {
                ((System.Collections.IList) this).Clear();
            }
        }

        public bool Equals(JsonData x)
        {
            if (x == null)
                return false;

            if (x._type != _type)
                return false;

            switch (_type)
            {
                case JsonType.None:
                    return true;

                case JsonType.Object:
                    return _instObject.Equals(x._instObject);

                case JsonType.Array:
                    return _instArray.Equals(x._instArray);

                case JsonType.String:
                    return _instString.Equals(x._instString);

                case JsonType.Int:
                    return _instInt.Equals(x._instInt);

                case JsonType.Long:
                    return _instLong.Equals(x._instLong);

                case JsonType.Double:
                    return _instDouble.Equals(x._instDouble);

                case JsonType.Boolean:
                    return _instBoolean.Equals(x._instBoolean);
            }

            return false;
        }

        public JsonType GetJsonType()
        {
            return _type;
        }

        public void SetJsonType(JsonType types)
        {
            if (_type == types)
                return;

            switch (types)
            {
                case JsonType.None:
                    break;

                case JsonType.Object:
                    _instObject = new System.Collections.Generic.Dictionary<string, JsonData>();
                    object_list =
                        new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, JsonData>>();
                    break;

                case JsonType.Array:
                    _instArray = new System.Collections.Generic.List<JsonData>();
                    break;

                case JsonType.String:
                    _instString = default(System.String);
                    break;

                case JsonType.Int:
                    _instInt = default(System.Int32);
                    break;

                case JsonType.Long:
                    _instLong = default(System.Int64);
                    break;

                case JsonType.Double:
                    _instDouble = default(System.Double);
                    break;

                case JsonType.Boolean:
                    _instBoolean = default(System.Boolean);
                    break;
            }

            _type = types;
        }

        public string ToJson()
        {
            if (_json != null)
                return _json;

            var sw = new System.IO.StringWriter();
            var writer = new JsonWriter(sw) {Validate = false};

            WriteJson(this, writer);
            _json = sw.ToString();

            return _json;
        }

        public void ToJson(JsonWriter writer)
        {
            var oldValidate = writer.Validate;

            writer.Validate = false;

            WriteJson(this, writer);

            writer.Validate = oldValidate;
        }

        public override string ToString()
        {
            switch (_type)
            {
                case JsonType.Array:
                    return "JsonData array";

                case JsonType.Boolean:
                    return _instBoolean.ToString();

                case JsonType.Double:
                    return _instDouble.ToString(System.Globalization.CultureInfo.InvariantCulture);

                case JsonType.Int:
                    return _instInt.ToString(System.Globalization.CultureInfo.InvariantCulture);

                case JsonType.Long:
                    return _instLong.ToString(System.Globalization.CultureInfo.InvariantCulture);

                case JsonType.Object:
                    return "JsonData object";

                case JsonType.String:
                    return _instString;
            }

            return "Uninitialized JsonData";
        }
    }


    internal class OrderedDictionaryEnumerator : System.Collections.IDictionaryEnumerator
    {
        private readonly
            System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<string, JsonData>>
            _listEnumerator;


        public object Current
        {
            get { return Entry; }
        }

        public System.Collections.DictionaryEntry Entry
        {
            get
            {
                var curr = _listEnumerator.Current;
                return new System.Collections.DictionaryEntry(curr.Key, curr.Value);
            }
        }

        public object Key
        {
            get { return _listEnumerator.Current.Key; }
        }

        public object Value
        {
            get { return _listEnumerator.Current.Value; }
        }


        public OrderedDictionaryEnumerator(
            System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<string, JsonData>> enumerator)
        {
            _listEnumerator = enumerator;
        }


        public bool MoveNext()
        {
            return _listEnumerator.MoveNext();
        }

        public void Reset()
        {
            _listEnumerator.Reset();
        }
    }
}