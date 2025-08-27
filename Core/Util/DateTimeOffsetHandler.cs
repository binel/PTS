using System;
using System.Data;
using System.Globalization;
using Dapper;

public sealed class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    // Serialize as round-trip ISO 8601 (has the 'T' and offset)
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        => parameter.Value = value.ToString("O"); // 2025-08-26T21:43:29.439118-05:00

    public override DateTimeOffset Parse(object value)
    {
        if (value is DateTimeOffset dto) return dto;
        if (value is DateTime dt) return new DateTimeOffset(dt);

        var s = Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;

        // Try robust parse that accepts both "2025-08-26 21:43:29.439118-05:00"
        // and "2025-08-26T21:43:29.439118-05:00"
        if (DateTimeOffset.TryParse(s, CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.RoundtripKind,
                out var parsed))
        {
            return parsed;
        }

        // Fallback: explicit patterns (space or 'T')
        string[] formats =
        {
            "yyyy-MM-dd HH:mm:ss.FFFFFFFK",
            "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK"
        };
        return DateTimeOffset.ParseExact(s, formats, CultureInfo.InvariantCulture,
                                         DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.RoundtripKind);
    }
}