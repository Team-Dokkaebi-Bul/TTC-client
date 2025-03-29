public interface ISkill
{
    void Attack(IStatComponent stat); // 스킬 모션 실행 + 공격력 계산 + 몬스터에게 넘겨주기
}
