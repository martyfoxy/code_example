using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    /// <summary>
    /// Панель управления одного из игроков
    /// </summary>
    public sealed class PlayerPanelView : MonoBehaviour
    {
        [SerializeField]
        private Button attackButton;
        
        [SerializeField]
        private Transform statsPanel;

        public Transform StatsPanel => statsPanel;

        public void SetAttackHandler(Action handler)
        {
            attackButton.onClick.RemoveAllListeners();
            attackButton.onClick.AddListener(handler.Invoke);
        }
    }
}
