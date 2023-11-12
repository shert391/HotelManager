namespace DataContract;

public class Interval<T> where T : struct
{
    public T Min { get; set; }
    public T Max { get; set; }
}
