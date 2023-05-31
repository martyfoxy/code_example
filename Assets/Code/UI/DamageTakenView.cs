using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    /// <summary>
    /// Визуальное представление плашки полученного урона
    /// </summary>
    public sealed class DamageTakenView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        private Action _onHide;
        private Coroutine _coroutine;
        
        public void ShowDamage(float value, Action onHide)
        {
            text.text = $"-{value.ToString(CultureInfo.InvariantCulture)}";
            transform.localPosition = Vector3.zero;
            
            _onHide = onHide;

            if(_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(HideAfterSeconds());
        }

        private void Update()
        {
            transform.position += Vector3.one * 0.75f * Time.deltaTime;
        }

        private IEnumerator HideAfterSeconds()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            _onHide?.Invoke();
        }
    }
}