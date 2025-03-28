using System;
using Eu4ng.Manager.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Eu4ng.Framework.Platformer
{
    /// <summary>
    /// 씬 로드 후 PlayerStart 컴포넌트가 부착된 오브젝트들을 찾아 해당 위치에서 스폰합니다.
    /// PlayerStart 컴포넌트가 여러 개인 경우 랜덤 위치(PlayerStart)에 스폰됩니다.
    /// </summary>
    public class PlayerSpawnManager : MonoSingleton<PlayerSpawnManager>
    {
        /* Fields */
        
        [SerializeField] GameObject m_PlayerPrefab;

        GameObject m_PlayerPrefabInstance;
        
        public event Action PlayerSpawned;
        
        /* Properties */
        
        public GameObject Player => m_PlayerPrefabInstance;
        
        /* MonoSingleton */

        protected override void OnInitialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SpawnPlayer();
        }

        protected virtual void SpawnPlayer()
        {
            // 유효성 검사
            if (m_PlayerPrefab == null) return;
            
            // 씬 오브젝트들 중 PlayerStart 컴포넌트를 지닌 오브젝트 목록 가져오기
            var playerStarts = FindObjectsByType<PlayerStart>(FindObjectsSortMode.None);
            if (playerStarts.Length <= 0) return;
            
            // 플레이어 스폰 위치 계산
            int playerStartIndex = playerStarts.Length == 1 ? 0 : Random.Range(0, playerStarts.Length);
            Vector3 playerSpawnPosition = playerStarts[playerStartIndex].transform.position;
            
            // 플레이어 스폰
            m_PlayerPrefabInstance = Instantiate(m_PlayerPrefab, playerSpawnPosition, Quaternion.identity);
            
            // 플레이어 스폰 이벤트 호출
            PlayerSpawned?.Invoke();
        }
    }
}
