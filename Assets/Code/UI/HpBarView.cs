using Code.Parameters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    /// <summary>
    /// Визуальное представление хп-бара игрока
    /// </summary>
    public sealed class HpBarView : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private Image hpBarImage;

        [SerializeField]
        private TMP_Text hpBarText;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            canvas.worldCamera = _camera;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(_camera.transform.position * -1);
        }

        public void SetValue(IParameter<float> hp, IParameter<float> maxHp)
        {
            hpBarText.text = hp.Value.ToString("F");
            hpBarImage.fillAmount = hp.Value / maxHp.Value;
        }
    }
}