using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class MainCanvasView : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerPanelView> controlPanels;

        [SerializeField]
        private Button defaultReloadBtn;
        
        [SerializeField]
        private Button reloadWithBuffsBtn;

        public List<PlayerPanelView> ControlPanels => controlPanels;
        public Button DefaultReloadBtn => defaultReloadBtn;
        public Button ReloadWithBuffsBtn => reloadWithBuffsBtn;
    }
}
