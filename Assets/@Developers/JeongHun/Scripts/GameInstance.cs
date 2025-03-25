using Eu4ng.Manager.Data;

namespace Eu4ng.Framework.Platformer
{
    public class GameInstanceData : ISavable
    {
        
    }
        
    /// <summary>
    /// 게임 실행부터 종료 시까지 유지되는 단 하나의 전역적인 객체로 게임 세션 전체에서 유지되는 데이터와 로직을 관리하는 클래스
    /// </summary>
    public class GameInstance : DataManagerClient<GameInstance, GameInstanceData>
    {
        /* MonoSingleton */
        
        protected override void OnInitialize() {}
    }
}
