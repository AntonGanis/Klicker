using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private Transform _enemy;
        private Animator _animator;

        public event UnityAction<float> OnDamaged;
        public event UnityAction OnDead;

        private float _health;
        private bool _isDying = false;

        private PoolMono<ParticleSystemWrapper> _pools;

        public void Initialize(float health, Transform enemy, Vector3 pos, Vector3 scale, 
            ParticleSystemWrapper PrefabBlood, Transform container)
        {
            _pools = new PoolMono<ParticleSystemWrapper>(PrefabBlood, 5, container);
            _pools.autoExpand = true;
            _health = health;

            NewEnemy(enemy, pos, scale);
        }
        private void NewEnemy(Transform enemy, Vector3 pos, Vector3 scale)
        {
            _enemy = enemy;
            _animator = _enemy.GetComponent<Animator>();
            _animator.Rebind();
            _animator.Update(0f);
            _enemy.transform.parent = null;
            _enemy.transform.position = pos;
            _enemy.transform.localScale = scale;
        }
        public void DoDamage(float damage)
        {
            if (_isDying) return;

            if (damage >= _health)
            {
                _health = 0;
                _pools.OfAllElement();
                OnDamaged?.Invoke(damage);
                Animator animatot = _animator;//new
                GameObject enemy = _enemy.gameObject;
                _animator = null;
                _enemy = null;
                StartCoroutine(DieCoroutine(animatot, enemy));

                return;
            }

            var effect = _pools.GetFreeElement();
            effect.PlayAtPosition();
            _health -= damage;

            OnDamaged?.Invoke(damage);
        }
        private IEnumerator DieCoroutine(Animator animator, GameObject enemy)
        {
            _isDying = true;

            animator.SetTrigger("die");

            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
            enemy.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            OnDead?.Invoke();
            _pools.Clear();
            _isDying = false;
        }
        public float GetHealth()
        {
            return _health;
        }
    }
}