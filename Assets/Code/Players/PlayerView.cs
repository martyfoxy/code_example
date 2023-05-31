using Code.Parameters;
using Code.UI;
using Code.Utils;
using UnityEngine;

namespace Code.Players
{
    /// <summary>
    /// Визуальное представление игрока
    /// </summary>
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        
        [SerializeField]
        private HpBarView hpBarView;

        public HpBarView HpBarView => hpBarView;

        private static readonly int Health = Animator.StringToHash("Health");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void ResetAnimator(IParameter<float> hpParameter)
        {
            animator.SetInteger(Health, (int) hpParameter.Value);
            animator.SetBool(Attack, false);
        }
        
        public void SetHpParameter(IParameter<float> hpParameter)
        {
            animator.SetInteger(Health, (int) hpParameter.Value);
        }

        public void PlayAttack()
        {
            animator.SetTrigger(Attack);
        }
    }
}