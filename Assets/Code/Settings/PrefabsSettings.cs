using Code.Players;
using Code.UI;
using UnityEngine;

namespace Code.Settings
{
    /// <summary>
    /// Хранит ссылки на префабы
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabsSettings", menuName = "Prefabs Settings")]
    public sealed class PrefabsSettings : ScriptableObject
    {
        [SerializeField]
        private PlayerView playerPrefab;
        
        [SerializeField]
        private StatView statPrefab;
        
        [SerializeField]
        private DamageTakenView damageBarPrefab;

        public PlayerView PlayerPrefab => playerPrefab;
        public StatView StatPrefab => statPrefab;
        public DamageTakenView DamageBarPrefab => damageBarPrefab;
    }
}