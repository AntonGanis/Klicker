using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform _contener;
    [SerializeField] EnemyConfig _enemyConfig;

    EnemyData _currentEnemyData;
    Enemy _currentEnemy;
    HealthBar _healthBar;
    public void Inialize(HealthBar healthBar)
    {
        _healthBar = healthBar;
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        _currentEnemyData = _enemyConfig.Enemies[0];
        _currentEnemy = Instantiate(_enemyConfig.prefab, _contener);
        _currentEnemy.Inizialializator(_currentEnemyData);

        _healthBar.SeTMaxValue(_currentEnemyData.Health);
        _currentEnemy.OnDamage += _healthBar.DecreaseValue;
    }
    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.DoDamage(damage);
    }

    public void SubcriteDamageEnemy(UnityAction<float> callback)
    {
        if(_currentEnemy != null)
        {
            _currentEnemy.OnDamage += callback;
        }
    }
}
