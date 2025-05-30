using System.Collections.Generic;
using System.ComponentModel;
using Game.Configs.EnemyConfigs;
using Game.Configs.LevelConfigs;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemiesConfig _enemiesConfig;

        private Enemy _currentEnemyMonoBehaviour;
        private HealthBar.HealthBar _healthBar;
        private Timer.Timer _timer;
        private LevelData _levelData;
        private int _currentEnemyIndex;

        public event UnityAction<bool> OnLevelPassed;

        private Transform container;
        public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer, Transform container)
        {
            _timer = timer;
            _healthBar = healthBar;
            this.container = container;
        }

        public void StartLevel(LevelData levelData)
        {
            _levelData = levelData;
            _currentEnemyIndex = -1;

            if (_currentEnemyMonoBehaviour == null)
            {
                _currentEnemyMonoBehaviour = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
                _currentEnemyMonoBehaviour.OnDead += SpawnEnemy;
                _currentEnemyMonoBehaviour.OnDamaged += _healthBar.DecreaseValue;
            }

            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            _currentEnemyIndex++;
            _timer.Stop();

            if (_currentEnemyIndex >= _levelData.Enemies.Count)
            {
                OnLevelPassed?.Invoke(true);
                _timer.Stop();
                return;
            }

            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];

            _timer.SetActive(currentEnemy.IsBoss);
            if (currentEnemy.IsBoss)
            {
                _timer.SetValue(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
            }
            var currentEnemyViewData = _enemiesConfig.GetEnemy(currentEnemy.Id);
            InitHpBar(currentEnemy.Hp, currentEnemyViewData.Name);
            Transform prefab = Instantiate(currentEnemyViewData.Prefab);
            _currentEnemyMonoBehaviour.Initialize(currentEnemy.Hp, prefab, 
                currentEnemyViewData.Position, currentEnemyViewData.Scale, 
                currentEnemyViewData.PrefabBlood, container);
        }

        private void InitHpBar(float health, string name)
        {
            _healthBar.Show();
            _healthBar.SetMaxValue(health);
            _healthBar.SetName(name);
        }

        public void DamageCurrentEnemy(float damage)
        {
            _currentEnemyMonoBehaviour.DoDamage(damage);
        }
    }
}