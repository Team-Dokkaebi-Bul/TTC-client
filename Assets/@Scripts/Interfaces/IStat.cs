public interface IStat
{
    string StatName { get; }
    string Description { get; }
    float DefaultValue { get; }
    float MinValue { get; }
    float MaxValue { get; }
}