using Game.ClickButton;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfig;
using Game.Enemies;
using Game.Skills;
//using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEditor;
using UnityEngine;
using Progress = Global.SaveSystem.SavableObjects.Progress;
using UnityEngine.UI;

namespace Game {
    public class GameManager : EntryPoint{
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private Image _backgraund;
        [SerializeField] private SkillsConfig _skillsConfig;

        [SerializeField] private Timer.Timer _timer;

        //private AudioManager _audioManager;

        private SkillSystem _skillSystem;
        private EndLevelSystem _endLevelSystem;
        private SceneLoader _sceneLoader;

        private const string COMMON_OBJECT_TAG = "CommonObject";


        private GameEnterParams _gameEnterParams;
        private SaveSystem _saveSystem;


        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            //_audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;

            if (enterParams is not GameEnterParams gameEnterParams)
            {
                Debug.LogError("troubles with enter params into game");
                return;
            }

            _gameEnterParams = gameEnterParams;

            _clickButtonManager.Initialize();
            _enemyManager.Initialize(_healthBar, _timer, transform);
            _endLevelWindow.Initialize();

            var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillSystem = new(openedSkills, _skillsConfig, _enemyManager);
            _endLevelSystem = new(_endLevelWindow, _saveSystem, _gameEnterParams, _levelsConfig);

            _clickButtonManager.OnClicked += (isCritical) =>
            {
                _skillSystem.InvokeTrigger(SkillTrigger.OnDamage);  // Активируем триггер скилла
                GetCalculateManager(isCritical);                    // Вызываем расчет урона с учетом крита
            };
            _endLevelWindow.OnRestartClicked += RestartLevel;
            _enemyManager.OnLevelPassed += _endLevelSystem.LevelPassed;
            _playerHealth.OnLevelPassed += _endLevelSystem.LevelPassed;

            StartLevel();
        }
        private void GetCalculateManager(bool isCritical)
        {
            var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);

            if (isCritical)
            {
                _enemyManager.DamageCurrentEnemy(5f);
            }
            else
            {
                _enemyManager.DamageCurrentEnemy(1f);
            }
        }

        private void StartLevel()
        {
            var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            var location = _gameEnterParams.Location;
            var level = _gameEnterParams.Level;
            if (location > maxLocationAndLevel.x ||
               (location == maxLocationAndLevel.x && level > maxLocationAndLevel.y))
            {
                location = maxLocationAndLevel.x;
                level = maxLocationAndLevel.y;
            }
            var levelData = _levelsConfig.GetLevel(location, level);

            _enemyManager.StartLevel(levelData);
        }
        private void RestartLevel()
        {
            _sceneLoader.LoadGameplayScene(_gameEnterParams);
        }
        private void GoToMeta()
        {
            _sceneLoader.LoadMetaScene();
        }
        
    }
}