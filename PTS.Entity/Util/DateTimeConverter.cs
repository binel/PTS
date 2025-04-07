namespace PTS.Entity.Util;

public static class DateTimeConverter {

    public static long ToUnix(DateTime dateTime) {
        return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }

    public static DateTime FromUnix(long dateTime) {
        return DateTimeOffset.FromUnixTimeSeconds(dateTime).UtcDateTime;
    }

    public static DateTime StripToSeconds(DateTime dt) {
    return new DateTime(
            dt.Year,
            dt.Month,
            dt.Day,
            dt.Hour,
            dt.Minute,
            dt.Second,
            dt.Kind
        );
    }
}