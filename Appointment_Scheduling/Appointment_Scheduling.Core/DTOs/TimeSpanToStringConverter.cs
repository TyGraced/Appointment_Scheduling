using System.Text.Json;
using System.Text.Json.Serialization;

public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string time = reader.GetString()!;
        return TimeSpan.ParseExact(time, @"hh\:mm", null);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(@"hh\:mm"));
    }
}
