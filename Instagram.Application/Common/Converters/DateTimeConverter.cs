using System.Text.Json;
using System.Text.Json.Serialization;

namespace Instagram.Application.Common.Converters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String &&
            DateTime.TryParseExact(
                reader.GetString(),
                DateTimeFormat,
                null,
                System.Globalization.DateTimeStyles.AssumeUniversal,
                out DateTime result))
        {
            return result;
        }
        return DateTime.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateTimeFormat));
    }
}
