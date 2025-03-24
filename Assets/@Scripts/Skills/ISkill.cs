public interface ISkill
{
    bool CanUse { get; }
    float Cooldown { get; }
    float CooldownTime { get; }
    void Execute();
    void Enable(TestWarrior owner);
    void Disable();
    void SetCooldownTime(float cooldownTime);
}