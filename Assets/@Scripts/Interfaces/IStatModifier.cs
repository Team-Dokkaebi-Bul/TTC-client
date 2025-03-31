public interface IStatModifier
{
    string TargetStatName { get; }

    ModifierType Type { get; }

    float Value { get; }

    IStatComponent Target { get; }

    bool IsActive { get; }

    void Apply();

    void Remove();
}
