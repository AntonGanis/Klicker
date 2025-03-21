using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Animator _animator;

        public event UnityAction<float> OnDamaged;
        public event UnityAction OnDead;

        private float _health;

        public void Initialize(EnemyData enemyData)
        {
            _health = enemyData.Health;
            _image.sprite = enemyData.Sprite;
        }

        public void DoDamage(float damage)
        {
            if (damage >= _health)
            {
                _health = 0;

                OnDamaged?.Invoke(damage);
                OnDead?.Invoke();
                return;
            }
            _animator.SetTrigger("Damage");
            _health -= damage;
            OnDamaged?.Invoke(damage);
        }

        public float GetHealth()
        {
            return _health;
        }
    }
}