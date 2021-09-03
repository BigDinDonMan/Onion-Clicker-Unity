
[System.Serializable]
public class SerializableTimeStamp
{
    public int hour;
    public int minute;
    public int second;
    public int millisecond;

    public int day;
    public int month;
    public int year;

    public SerializableTimeStamp(System.DateTime timestamp) {
        (hour, minute, second, millisecond) = (timestamp.Hour, timestamp.Minute, timestamp.Second, timestamp.Millisecond);
        (day, month, year) = (timestamp.Day, timestamp.Month, timestamp.Year);
    }

    public static implicit operator SerializableTimeStamp(System.DateTime stamp) => new SerializableTimeStamp(stamp);

    public static implicit operator System.DateTime(SerializableTimeStamp stamp) {
        return new System.DateTime(stamp.year, stamp.month, stamp.day, stamp.hour, stamp.minute, stamp.second, stamp.millisecond);
    }
}
