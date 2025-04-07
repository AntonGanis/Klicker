using Game.ClickButton;
using Game.HealthBar;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private HealthBar _manaBar;
    private float _maxHealth;
    private float _health;

    private float _maxMana;
    private float _mana;

    public event UnityAction<bool> OnLevelPassed;

    public void Initialize(float health, float mana)
    {
        _health = health;
        _maxHealth = mana;
        _mana = mana;
        _maxMana = mana;

        InitHpBarAndManaBar(_maxHealth, _maxMana);
    }

    public void DoDamage(float damage)
    {
        if (damage >= _health)
        {
            _health = 0;
            OnLevelPassed?.Invoke(true);
            return;
        }
        _health -= damage;
        _healthBar.DecreaseValue(damage);
    }

    private void InitHpBarAndManaBar(float health, float mana)
    {
        _healthBar.Show();
        _healthBar.SetMaxValue(health);

        _manaBar.Show();
        _manaBar.SetMaxValue(health);
    }
}
