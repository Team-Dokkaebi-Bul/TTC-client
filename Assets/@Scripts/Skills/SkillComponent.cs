using UnityEngine;
using System.Collections.Generic;

public class SkillComponent : MonoBehaviour
{
    #region Fields
    [System.Serializable]
    public class SkillKeyBinding
    {
        public BaseSkill skillPrefab;
        public BaseSkill activeSkill;
        public KeyCode keyCode;
        public float cooldownTime;
    }

    [SerializeField] private List<SkillKeyBinding> _skillBindings = new();
    private TestWarrior _owner;
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (_owner == null) return;

        // 각 스킬의 키 입력 체크
        foreach (var binding in _skillBindings)
        {
            if (binding.activeSkill != null && Input.GetKeyDown(binding.keyCode))
            {
                binding.activeSkill.Execute();
            }
        }
    }
    #endregion

    #region Public Methods
    public void EnableSkills(TestWarrior owner)
    {
        _owner = owner;

        foreach (var binding in _skillBindings)
        {
            if (binding.skillPrefab != null)
            {
                // 스킬 프리팹을 인스턴스화
                binding.activeSkill = Instantiate(binding.skillPrefab, transform);

                // 쿨다운 시간 설정
                if (binding.cooldownTime > 0)
                {
                    binding.activeSkill.SetCooldownTime(binding.cooldownTime);
                }

                binding.activeSkill.Enable(owner);
            }
        }

        Debug.Log($"[{gameObject.name}] 스킬 활성화 완료");
    }

    public void DisableSkills()
    {
        foreach (var binding in _skillBindings)
        {
            if (binding.activeSkill != null)
            {
                binding.activeSkill.Disable();
                Destroy(binding.activeSkill.gameObject);
                binding.activeSkill = null;
            }
        }

        _owner = null;
        Debug.Log($"[{gameObject.name}] 스킬 비활성화 완료");
    }
    #endregion
}