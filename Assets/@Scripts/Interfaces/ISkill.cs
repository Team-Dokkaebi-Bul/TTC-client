using UnityEngine;

public interface ISkill
{
    bool IsEnabled { get; }
    bool CanUse { get; }
    float Cooldown { get; }
    float CooldownTime { get; }
    
    void Enable(TestWarrior owner);
    void Disable();
    void SetCooldownTime(float cooldownTime);
    void Execute();
} 