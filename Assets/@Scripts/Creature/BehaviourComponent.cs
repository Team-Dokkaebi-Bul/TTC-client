using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creature
{
    /// <summary>
    /// 생성한 크리처의 행동을 실행하는 클래스입니다.
    /// IBehaviour 클래스들을 등록하고, 실행하고, 변경할 수 있습니다.
    /// </summary>
    public class BehaviourComponenet : MonoBehaviour
    {
        #region Fields
        private ECreatureStatus _currentStatus;
        private Dictionary<ECreatureStatus, CreatureBehave> _behaviours = new Dictionary<ECreatureStatus, CreatureBehave>();
        #endregion

        #region Methods
        public void AddBehaviour(ECreatureStatus status, CreatureBehave behaviour)
        {
            if (!_behaviours.ContainsKey(status))
            {
                _behaviours.Add(status, behaviour);
            }
        }

        public void InitBehaviour(ECreatureStatus status)
        {
            if (_behaviours.ContainsKey(status))
            {
                _currentStatus = status;
            }
        }

        public ECreatureStatus GetStatus()
        {
            return _currentStatus;
        }

        public void ChangeStatus(ECreatureStatus status)
        {
            if (_currentStatus == status)
            {
                return;
            }
            _currentStatus = status;
            _behaviours[_currentStatus]();
        }

        public void Behave()
        {
            _behaviours[_currentStatus]();
        }
        #endregion
    }

    /// <summary>
    /// 크리처의 상태를 표현하기 위한 Enum입니다.
    /// </summary>
    #region Creature Status
    public enum ECreatureStatus
    {
        Start, Idle, Air, Jump, Attack, Stop, Charming, Damaged, Dead
    }
    #endregion

    /// <summary>
    /// 크리처의 상태에 맞는 행동을 실행하기 위한 코루틴 델리게이트 입니다.
    /// </summary>
    #region Delegate
    public delegate void CreatureBehave();
    #endregion
}
