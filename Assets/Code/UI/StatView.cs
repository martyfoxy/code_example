using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class StatView : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TMP_Text text;

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }

        public void SetLabel(string label)
        {
            text.text = label;
        }
    }
}