using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Image _img;

    public event UnityAction<float> OnDamage;
    public event UnityAction OnDead;

    float _health;
    public void Inizialializator(EnemyData enemyData)
    {
        _health = enemyData.Health;
        _img.sprite = enemyData.sprite;
    }
    public void DoDamage(float damage)
    {
        if(_health>= 0)
        {
            _health = 0;
            OnDamage?.Invoke(_health);
            OnDead?.Invoke();
            return;
        }
        _health -= damage;
        OnDamage?.Invoke(damage);
    }
    public float GetHealth()
    {
        return _health;
    }
}
