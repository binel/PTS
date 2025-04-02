namespace PTS.Entity.Util;

public static class DateTimeConverter {

    public static long ToUnix(DateTime dateTime) {
        return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }

    public static DateTime FromUnix(long dateTime) {
        return DateTimeOffset.FromUnixTimeSeconds(dateTime).UtcDateTime;
    }
}