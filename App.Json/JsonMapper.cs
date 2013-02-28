using System.Linq;

namespace App.Json
{
    internal struct PropertyMetadata
    {
        public System.Reflection.MemberInfo Info;
        public bool IsField;
        public System.Type Type;
    }

    internal struct ArrayMetadata
    {
        private System.Type _elementType;


        public System.Type ElementType
        {
            get { return _elementType ?? typeof (JsonData); }

            set { _elementType = value; }
        }

        public bool IsArray { get; set; }

        public bool IsList { get; set; }
    }

    internal struct ObjectMetadata
    {
        private System.Type _elementType;


        public System.Type ElementType
        {
            get { return _elementType ?? typeof (JsonData); }

            set { _elementType = value; }
        }

        public bool IsDictionary { get; set; }

        public System.Collections.Generic.IDictionary<string, PropertyMetadata> Properties { get; set; }
    }

    internal delegate void ExporterFunc(object obj, JsonWriter writer);

    public delegate void ExporterFunc<in T>(T obj, JsonWriter writer);

    internal delegate object ImporterFunc(object input);

    public delegate TValue ImporterFunc<in TJson, out TValue>(TJson input);

    public delegate IJsonWrapper WrapperFactory();

    public class JsonMapper
    {
        #region Fields

        private static readonly int MaxNestingDepth;

        private static readonly System.IFormatProvider DatetimeFormat;

        private static readonly System.Collections.Generic.IDictionary<System.Type, ExporterFunc> BaseExportersTable;
        private static readonly System.Collections.Generic.IDictionary<System.Type, ExporterFunc> CustomExportersTable;

        private static readonly System.Collections.Generic.IDictionary<System.Type,
            System.Collections.Generic.IDictionary<System.Type, ImporterFunc>> BaseImportersTable;

        private static readonly System.Collections.Generic.IDictionary<System.Type,
            System.Collections.Generic.IDictionary<System.Type, ImporterFunc>> CustomImportersTable;

        private static readonly System.Collections.Generic.IDictionary<System.Type, ArrayMetadata> ArrayMetadata;
        private static readonly object ArrayMetadataLock = new System.Object();

        private static readonly System.Collections.Generic.IDictionary<System.Type,
            System.Collections.Generic.IDictionary<System.Type, System.Reflection.MethodInfo>> ConvOps;

        private static readonly object ConvOpsLock = new System.Object();

        private static readonly System.Collections.Generic.IDictionary<System.Type, ObjectMetadata> ObjectMetadata;
        private static readonly object ObjectMetadataLock = new System.Object();

        private static readonly System.Collections.Generic.IDictionary<System.Type,
            System.Collections.Generic.IList<PropertyMetadata>> TypeProperties;

        private static readonly object TypePropertiesLock = new System.Object();

        private static readonly JsonWriter StaticWriter;
        private static readonly object StaticWriterLock = new System.Object();

        #endregion

        #region Constructors

        static JsonMapper()
        {
            MaxNestingDepth = 100;

            ArrayMetadata = new System.Collections.Generic.Dictionary<System.Type, ArrayMetadata>();
            ConvOps =
                new System.Collections.Generic.Dictionary
                    <System.Type, System.Collections.Generic.IDictionary<System.Type, System.Reflection.MethodInfo>>();
            ObjectMetadata = new System.Collections.Generic.Dictionary<System.Type, ObjectMetadata>();
            TypeProperties = new System.Collections.Generic.Dictionary<System.Type,
                System.Collections.Generic.IList<PropertyMetadata>>();

            StaticWriter = new JsonWriter();

            DatetimeFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;

            BaseExportersTable = new System.Collections.Generic.Dictionary<System.Type, ExporterFunc>();
            CustomExportersTable = new System.Collections.Generic.Dictionary<System.Type, ExporterFunc>();

            BaseImportersTable = new System.Collections.Generic.Dictionary<System.Type,
                System.Collections.Generic.IDictionary<System.Type, ImporterFunc>>();
            CustomImportersTable = new System.Collections.Generic.Dictionary<System.Type,
                System.Collections.Generic.IDictionary<System.Type, ImporterFunc>>();

            RegisterBaseExporters();
            RegisterBaseImporters();
        }

        #endregion

        #region Private Methods

        private static void AddArrayMetadata(System.Type type)
        {
            if (ArrayMetadata.ContainsKey(type))
                return;

            var data = new ArrayMetadata {IsArray = type.IsArray};

            if (type.GetInterface("System.Collections.IList") != null)
                data.IsList = true;

            foreach (var pInfo in from p_info in type.GetProperties()
                                  where p_info.Name == "Item"
                                  let parameters = p_info.GetIndexParameters()
                                  where parameters.Length == 1
                                  where parameters[0].ParameterType == typeof (int)
                                  select p_info)
            {
                data.ElementType = pInfo.PropertyType;
            }

            lock (ArrayMetadataLock)
            {
                try
                {
                    ArrayMetadata.Add(type, data);
                }
                catch (System.ArgumentException)
                {
                }
            }
        }

        private static void AddObjectMetadata(System.Type type)
        {
            if (ObjectMetadata.ContainsKey(type))
                return;

            var data = new ObjectMetadata();

            if (type.GetInterface("System.Collections.IDictionary") != null)
                data.IsDictionary = true;

            data.Properties = new System.Collections.Generic.Dictionary<string, PropertyMetadata>();

            foreach (var pInfo in type.GetProperties())
            {
                if (pInfo.Name == "Item")
                {
                    var parameters = pInfo.GetIndexParameters();

                    if (parameters.Length != 1)
                        continue;

                    if (parameters[0].ParameterType == typeof (string))
                        data.ElementType = pInfo.PropertyType;

                    continue;
                }

                var pData = new PropertyMetadata {Info = pInfo, Type = pInfo.PropertyType};

                data.Properties.Add(pInfo.Name, pData);
            }

            foreach (var fInfo in type.GetFields())
            {
                var pData = new PropertyMetadata {Info = fInfo, IsField = true, Type = fInfo.FieldType};

                data.Properties.Add(fInfo.Name, pData);
            }

            lock (ObjectMetadataLock)
            {
                try
                {
                    ObjectMetadata.Add(type, data);
                }
                catch (System.ArgumentException)
                {
                }
            }
        }

        private static void AddTypeProperties(System.Type type)
        {
            if (TypeProperties.ContainsKey(type))
                return;

            System.Collections.Generic.IList<PropertyMetadata> props = (from pInfo in type.GetProperties()
                                                                        where pInfo.Name != "Item"
                                                                        select
                                                                            new PropertyMetadata
                                                                                {
                                                                                    Info = pInfo,
                                                                                    IsField = false
                                                                                }).ToList();

            foreach (var pData in type.GetFields().Select(fInfo => new PropertyMetadata {Info = fInfo, IsField = true}))
            {
                props.Add(pData);
            }

            lock (TypePropertiesLock)
            {
                try
                {
                    TypeProperties.Add(type, props);
                }
                catch (System.ArgumentException)
                {
                }
            }
        }

        private static System.Reflection.MethodInfo GetConvOp(System.Type t1, System.Type t2)
        {
            lock (ConvOpsLock)
            {
                if (!ConvOps.ContainsKey(t1))
                    ConvOps.Add(t1,
                                new System.Collections.Generic.Dictionary<System.Type, System.Reflection.MethodInfo>());
            }

            if (ConvOps[t1].ContainsKey(t2))
                return ConvOps[t1][t2];

            var op = t1.GetMethod(
                "op_Implicit", new[] {t2});

            lock (ConvOpsLock)
            {
                try
                {
                    ConvOps[t1].Add(t2, op);
                }
                catch (System.ArgumentException)
                {
                    return ConvOps[t1][t2];
                }
            }

            return op;
        }

        private static object ReadValue(System.Type instType, JsonReader reader)
        {
            reader.Read();

            if (reader.Token == JsonToken.ArrayEnd)
                return null;

            if (reader.Token == JsonToken.Null)
            {
                if (!instType.IsClass)
                    throw new JsonException(System.String.Format(
                        "Can't assign null to an instance of type {0}",
                        instType));

                return null;
            }

            if (reader.Token == JsonToken.Double ||
                reader.Token == JsonToken.Int ||
                reader.Token == JsonToken.Long ||
                reader.Token == JsonToken.String ||
                reader.Token == JsonToken.Boolean)
            {
                var jsonType = reader.Value.GetType();

                if (instType.IsAssignableFrom(jsonType))
                    return reader.Value;

                // If there's a custom importer that fits, use it
                if (CustomImportersTable.ContainsKey(jsonType) &&
                    CustomImportersTable[jsonType].ContainsKey(
                        instType))
                {
                    var importer =
                        CustomImportersTable[jsonType][instType];

                    return importer(reader.Value);
                }

                // Maybe there's a base importer that works
                if (BaseImportersTable.ContainsKey(jsonType) &&
                    BaseImportersTable[jsonType].ContainsKey(
                        instType))
                {
                    var importer =
                        BaseImportersTable[jsonType][instType];

                    return importer(reader.Value);
                }

                // Maybe it's an enum
                if (instType.IsEnum)
                    return System.Enum.ToObject(instType, reader.Value);

                // Try using an implicit conversion operator
                var convOp = GetConvOp(instType, jsonType);

                if (convOp != null)
                    return convOp.Invoke(null,
                                         new[] {reader.Value});

                // No luck
                throw new JsonException(System.String.Format(
                    "Can't assign value '{0}' (type {1}) to type {2}",
                    reader.Value, jsonType, instType));
            }

            object instance = null;

            switch (reader.Token)
            {
                case JsonToken.ArrayStart:
                    {
                        AddArrayMetadata(instType);
                        var tData = ArrayMetadata[instType];

                        if (!tData.IsArray && !tData.IsList)
                            throw new JsonException(System.String.Format(
                                "Type {0} can't act as an array",
                                instType));

                        System.Collections.IList list;
                        System.Type elemType;

                        if (!tData.IsArray)
                        {
                            list = (System.Collections.IList) System.Activator.CreateInstance(instType);
                            elemType = tData.ElementType;
                        }
                        else
                        {
                            list = new System.Collections.ArrayList();
                            elemType = instType.GetElementType();
                        }

                        while (true)
                        {
                            var item = ReadValue(elemType, reader);
                            if (reader.Token == JsonToken.ArrayEnd)
                            {
                                list.Add(item);
                                list.RemoveAt(list.Count - 1);
                                break;
                            }

                            list.Add(item);
                        }

                        if (tData.IsArray)
                        {
                            var n = list.Count;
                            instance = System.Array.CreateInstance(elemType, n);

                            for (var i = 0; i < n; i++)
                                ((System.Array) instance).SetValue(list[i], i);
                        }
                        else
                            instance = list;
                    }
                    break;
                case JsonToken.ObjectStart:
                    {
                        AddObjectMetadata(instType);
                        var tData = ObjectMetadata[instType];

                        instance = System.Activator.CreateInstance(instType);

                        while (true)
                        {
                            reader.Read();

                            if (reader.Token == JsonToken.ObjectEnd)
                                break;

                            var property = (string) reader.Value;

                            if (tData.Properties.ContainsKey(property))
                            {
                                var propData =
                                    tData.Properties[property];

                                if (propData.IsField)
                                {
                                    ((System.Reflection.FieldInfo) propData.Info).SetValue(
                                        instance, ReadValue(propData.Type, reader));
                                }
                                else
                                {
                                    var pInfo =
                                        (System.Reflection.PropertyInfo) propData.Info;

                                    if (pInfo.CanWrite)
                                        pInfo.SetValue(
                                            instance,
                                            ReadValue(propData.Type, reader),
                                            null);
                                    else
                                        ReadValue(propData.Type, reader);
                                }
                            }
                            else
                            {
                                if (!tData.IsDictionary)
                                    throw new JsonException(System.String.Format(
                                        "The type {0} doesn't have the " +
                                        "property '{1}'", instType, property));

                                ((System.Collections.IDictionary) instance).Add(
                                    property, ReadValue(
                                        tData.ElementType, reader));
                            }
                        }
                    }
                    break;
            }

            return instance;
        }

        private static IJsonWrapper ReadValue(WrapperFactory factory,
                                              JsonReader reader)
        {
            reader.Read();

            if (reader.Token == JsonToken.ArrayEnd ||
                reader.Token == JsonToken.Null)
                return null;

            var instance = factory();

            if (reader.Token == JsonToken.String)
            {
                instance.SetString((string) reader.Value);
                return instance;
            }

            if (reader.Token == JsonToken.Double)
            {
                instance.SetDouble((double) reader.Value);
                return instance;
            }

            if (reader.Token == JsonToken.Int)
            {
                instance.SetInt((int) reader.Value);
                return instance;
            }

            if (reader.Token == JsonToken.Long)
            {
                instance.SetLong((long) reader.Value);
                return instance;
            }

            if (reader.Token == JsonToken.Boolean)
            {
                instance.SetBoolean((bool) reader.Value);
                return instance;
            }

            switch (reader.Token)
            {
                case JsonToken.ArrayStart:
                    instance.SetJsonType(JsonType.Array);
                    while (true)
                    {
                        var item = ReadValue(factory, reader);
                        if (reader.Token == JsonToken.ArrayEnd && item == null)
                            break;

                        instance.Add(item);
                    }
                    break;
                case JsonToken.ObjectStart:
                    instance.SetJsonType(JsonType.Object);
                    while (true)
                    {
                        reader.Read();

                        if (reader.Token == JsonToken.ObjectEnd)
                            break;

                        var property = (string) reader.Value;

                        instance[property] = ReadValue(
                            factory, reader);
                    }
                    break;
            }

            return instance;
        }

        private static void RegisterBaseExporters()
        {
            BaseExportersTable[typeof (byte)] =
                (obj, writer) => writer.Write(System.Convert.ToInt32((byte) obj));

            BaseExportersTable[typeof (char)] =
                (obj, writer) => writer.Write(System.Convert.ToString((char) obj));

            BaseExportersTable[typeof (System.DateTime)] =
                (obj, writer) => writer.Write(System.Convert.ToString((System.DateTime) obj,
                                                                      DatetimeFormat));

            BaseExportersTable[typeof (decimal)] =
                (obj, writer) => writer.Write((decimal) obj);

            BaseExportersTable[typeof (sbyte)] =
                (obj, writer) => writer.Write(System.Convert.ToInt32((sbyte) obj));

            BaseExportersTable[typeof (short)] =
                (obj, writer) => writer.Write(System.Convert.ToInt32((short) obj));

            BaseExportersTable[typeof (ushort)] =
                (obj, writer) => writer.Write(System.Convert.ToInt32((ushort) obj));

            BaseExportersTable[typeof (uint)] =
                (obj, writer) => writer.Write(System.Convert.ToUInt64((uint) obj));

            BaseExportersTable[typeof (ulong)] =
                (obj, writer) => writer.Write((ulong) obj);
        }

        private static void RegisterBaseImporters()
        {
            ImporterFunc importer = input => System.Convert.ToByte((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (byte), importer);

            importer = input => System.Convert.ToUInt64((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (ulong), importer);

            importer = input => System.Convert.ToSByte((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (sbyte), importer);

            importer = input => System.Convert.ToInt16((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (short), importer);

            importer = input => System.Convert.ToUInt16((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (ushort), importer);

            importer = input => System.Convert.ToUInt32((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (uint), importer);

            importer = input => System.Convert.ToSingle((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (float), importer);

            importer = input => System.Convert.ToDouble((int) input);
            RegisterImporter(BaseImportersTable, typeof (int),
                             typeof (double), importer);

            importer = input => System.Convert.ToDecimal((double) input);
            RegisterImporter(BaseImportersTable, typeof (double),
                             typeof (decimal), importer);


            importer = input => System.Convert.ToUInt32((long) input);
            RegisterImporter(BaseImportersTable, typeof (long),
                             typeof (uint), importer);

            importer = input => System.Convert.ToChar((string) input);
            RegisterImporter(BaseImportersTable, typeof (string),
                             typeof (char), importer);

            importer = input => System.Convert.ToDateTime((string) input, DatetimeFormat);
            RegisterImporter(BaseImportersTable, typeof (string),
                             typeof (System.DateTime), importer);
        }

        private static void RegisterImporter(
            System.Collections.Generic.IDictionary
                <System.Type, System.Collections.Generic.IDictionary<System.Type, ImporterFunc>> table,
            System.Type jsonType, System.Type valueType, ImporterFunc importer)
        {
            if (!table.ContainsKey(jsonType))
                table.Add(jsonType, new System.Collections.Generic.Dictionary<System.Type, ImporterFunc>());

            table[jsonType][valueType] = importer;
        }

        private static void WriteValue(object obj, JsonWriter writer,
                                       bool writerIsPrivate,
// ReSharper disable UnusedParameter.Local
                                       int depth)
// ReSharper restore UnusedParameter.Local
        {
            if (depth > MaxNestingDepth)
                throw new JsonException(
                    System.String.Format("Max allowed object depth reached while " +
                                         "trying to export from type {0}",
                                         obj.GetType()));

            if (obj == null)
            {
                writer.Write(null);
                return;
            }

            var wrapper = obj as IJsonWrapper;
            if (wrapper != null)
            {
                if (writerIsPrivate)
                    writer.TextWriter.Write(wrapper.ToJson());
                else
                    wrapper.ToJson(writer);

                return;
            }

            var s = obj as string;
            if (s != null)
            {
                writer.Write(s);
                return;
            }

            if (obj is System.Double)
            {
                writer.Write((double) obj);
                return;
            }

            if (obj is System.Int32)
            {
                writer.Write((int) obj);
                return;
            }

            if (obj is System.Boolean)
            {
                writer.Write((bool) obj);
                return;
            }

            if (obj is System.Int64)
            {
                writer.Write((long) obj);
                return;
            }

            var array = obj as System.Array;
            if (array != null)
            {
                writer.WriteArrayStart();

                foreach (var elem in array)
                    WriteValue(elem, writer, writerIsPrivate, depth + 1);

                writer.WriteArrayEnd();

                return;
            }

            var list = obj as System.Collections.IList;
            if (list != null)
            {
                writer.WriteArrayStart();
                foreach (var elem in list)
                    WriteValue(elem, writer, writerIsPrivate, depth + 1);
                writer.WriteArrayEnd();

                return;
            }

            var entries = obj as System.Collections.IDictionary;
            if (entries != null)
            {
                writer.WriteObjectStart();
                foreach (System.Collections.DictionaryEntry entry in entries)
                {
                    writer.WritePropertyName((string) entry.Key);
                    WriteValue(entry.Value, writer, writerIsPrivate,
                               depth + 1);
                }
                writer.WriteObjectEnd();

                return;
            }

            var objType = obj.GetType();

            // See if there's a custom exporter for the object
            if (CustomExportersTable.ContainsKey(objType))
            {
                var exporter = CustomExportersTable[objType];
                exporter(obj, writer);

                return;
            }

            // If not, maybe there's a base exporter
            if (BaseExportersTable.ContainsKey(objType))
            {
                var exporter = BaseExportersTable[objType];
                exporter(obj, writer);

                return;
            }

            // Last option, let's see if it's an enum
            if (obj is System.Enum)
            {
                var eType = System.Enum.GetUnderlyingType(objType);

                if (eType == typeof (long)
                    || eType == typeof (uint)
                    || eType == typeof (ulong))
// ReSharper disable PossibleInvalidCastException
                    writer.Write((ulong) obj);
// ReSharper restore PossibleInvalidCastException
                else
// ReSharper disable PossibleInvalidCastException
                    writer.Write((int) obj);
// ReSharper restore PossibleInvalidCastException

                return;
            }

            // Okay, so it looks like the input should be exported as an
            // object
            AddTypeProperties(objType);
            var props = TypeProperties[objType];

            writer.WriteObjectStart();
            foreach (var pData in props)
            {
                if (pData.IsField)
                {
                    writer.WritePropertyName(pData.Info.Name);
                    WriteValue(((System.Reflection.FieldInfo) pData.Info).GetValue(obj),
                               writer, writerIsPrivate, depth + 1);
                }
                else
                {
                    var pInfo = (System.Reflection.PropertyInfo) pData.Info;

                    if (pInfo.CanRead)
                    {
                        writer.WritePropertyName(pData.Info.Name);
                        WriteValue(pInfo.GetValue(obj, null),
                                   writer, writerIsPrivate, depth + 1);
                    }
                }
            }
            writer.WriteObjectEnd();
        }

        #endregion

        public static string ToJson(object obj)
        {
            lock (StaticWriterLock)
            {
                StaticWriter.Reset();

                WriteValue(obj, StaticWriter, true, 0);

                return StaticWriter.ToString();
            }
        }

        public static void ToJson(object obj, JsonWriter writer)
        {
            WriteValue(obj, writer, false, 0);
        }

        public static JsonData ToObject(JsonReader reader)
        {
            return (JsonData) ToWrapper(
                () => new JsonData(), reader);
        }

        public static JsonData ToObject(System.IO.TextReader reader)
        {
            var jsonReader = new JsonReader(reader);

            return (JsonData) ToWrapper(
                () => new JsonData(), jsonReader);
        }

        public static JsonData ToObject(string json)
        {
            return (JsonData) ToWrapper(
                () => new JsonData(), json);
        }

        public static T ToObject<T>(JsonReader reader)
        {
            return (T) ReadValue(typeof (T), reader);
        }

        public static T ToObject<T>(System.IO.TextReader reader)
        {
            var jsonReader = new JsonReader(reader);

            return (T) ReadValue(typeof (T), jsonReader);
        }

        public static T ToObject<T>(string json)
        {
            var reader = new JsonReader(json);

            return (T) ReadValue(typeof (T), reader);
        }

        public static IJsonWrapper ToWrapper(WrapperFactory factory,
                                             JsonReader reader)
        {
            return ReadValue(factory, reader);
        }

        public static IJsonWrapper ToWrapper(WrapperFactory factory,
                                             string json)
        {
            var reader = new JsonReader(json);

            return ReadValue(factory, reader);
        }

        public static void RegisterExporter<T>(ExporterFunc<T> exporter)
        {
            ExporterFunc exporterWrapper =
                (obj, writer) => exporter((T) obj, writer);

            CustomExportersTable[typeof (T)] = exporterWrapper;
        }

        public static void RegisterImporter<TJson, TValue>(
            ImporterFunc<TJson, TValue> importer)
        {
            ImporterFunc importerWrapper =
                input => importer((TJson) input);

            RegisterImporter(CustomImportersTable, typeof (TJson),
                             typeof (TValue), importerWrapper);
        }

        public static void UnregisterExporters()
        {
            CustomExportersTable.Clear();
        }

        public static void UnregisterImporters()
        {
            CustomImportersTable.Clear();
        }
    }
}