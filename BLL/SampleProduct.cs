using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BLL
{
    public partial class SampleProductClass
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("name_translations")]
        public Translations NameTranslations { get; set; }

        [JsonProperty("display_name_translations")]
        public Translations DisplayNameTranslations { get; set; }

        [JsonProperty("ingredients_translations")]
        public Translations IngredientsTranslations { get; set; }

        [JsonProperty("origin_translations")]
        public Translations OriginTranslations { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("unit")]
        public PortionUnitEnum Unit { get; set; }

        [JsonProperty("portion_quantity")]
        public long PortionQuantity { get; set; }

        [JsonProperty("portion_unit")]
        public PortionUnitEnum PortionUnit { get; set; }

        [JsonProperty("alcohol_by_volume")]
        public long AlcoholByVolume { get; set; }

        [JsonProperty("nutrients")]
        public Dictionary<string, Nutrient> Nutrients { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public partial class Translations
    {
        [JsonProperty("de", NullValueHandling = NullValueHandling.Ignore)]
        public string De { get; set; }

        [JsonProperty("fr", NullValueHandling = NullValueHandling.Ignore)]
        public string Fr { get; set; }

        [JsonProperty("it", NullValueHandling = NullValueHandling.Ignore)]
        public string It { get; set; }

        [JsonProperty("en", NullValueHandling = NullValueHandling.Ignore)]
        public string En { get; set; }
    }

    public partial class Nutrient
    {
        [JsonProperty("name_translations")]
        public Translations NameTranslations { get; set; }

        [JsonProperty("unit")]
        public NutrientUnit Unit { get; set; }

        [JsonProperty("per_hundred")]
        public double PerHundred { get; set; }

        [JsonProperty("per_portion")]
        public double? PerPortion { get; set; }

        [JsonProperty("per_day")]
        public long? PerDay { get; set; }
    }

    public enum Country { Ch };

    public enum NutrientUnit { G, KCal, KJ, Mg };

    public enum PortionUnitEnum { G, ML };

    public enum Status { Complete, Rescan };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CountryConverter.Singleton,
                NutrientUnitConverter.Singleton,
                PortionUnitEnumConverter.Singleton,
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CountryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Country) || t == typeof(Country?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "CH")
            {
                return Country.Ch;
            }
            throw new Exception("Cannot unmarshal type Country");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Country)untypedValue;
            if (value == Country.Ch)
            {
                serializer.Serialize(writer, "CH");
                return;
            }
            throw new Exception("Cannot marshal type Country");
        }

        public static readonly CountryConverter Singleton = new CountryConverter();
    }

    internal class NutrientUnitConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(NutrientUnit) || t == typeof(NutrientUnit?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "g":
                    return NutrientUnit.G;
                case "kCal":
                    return NutrientUnit.KCal;
                case "kJ":
                    return NutrientUnit.KJ;
                case "mg":
                    return NutrientUnit.Mg;
            }
            throw new Exception("Cannot unmarshal type NutrientUnit");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (NutrientUnit)untypedValue;
            switch (value)
            {
                case NutrientUnit.G:
                    serializer.Serialize(writer, "g");
                    return;
                case NutrientUnit.KCal:
                    serializer.Serialize(writer, "kCal");
                    return;
                case NutrientUnit.KJ:
                    serializer.Serialize(writer, "kJ");
                    return;
                case NutrientUnit.Mg:
                    serializer.Serialize(writer, "mg");
                    return;
            }
            throw new Exception("Cannot marshal type NutrientUnit");
        }

        public static readonly NutrientUnitConverter Singleton = new NutrientUnitConverter();
    }

    internal class PortionUnitEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PortionUnitEnum) || t == typeof(PortionUnitEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "g":
                    return PortionUnitEnum.G;
                case "mL":
                    return PortionUnitEnum.ML;
            }
            throw new Exception("Cannot unmarshal type PortionUnitEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PortionUnitEnum)untypedValue;
            switch (value)
            {
                case PortionUnitEnum.G:
                    serializer.Serialize(writer, "g");
                    return;
                case PortionUnitEnum.ML:
                    serializer.Serialize(writer, "mL");
                    return;
            }
            throw new Exception("Cannot marshal type PortionUnitEnum");
        }

        public static readonly PortionUnitEnumConverter Singleton = new PortionUnitEnumConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "complete":
                    return Status.Complete;
                case "rescan":
                    return Status.Rescan;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.Complete:
                    serializer.Serialize(writer, "complete");
                    return;
                case Status.Rescan:
                    serializer.Serialize(writer, "rescan");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
