//using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using SceneManagement;
using UnityEngine;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;

        private SaveSystem _saveSystem;
        //private AudioManager _audioManager;
        private SceneLoader _sceneLoader;

        private const string COMMON_OBJECT_TAG = "CommonObject";

        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            //_audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;

            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);

            _locationManager.Initialize(progress, StartLevel);

            //_audioManager.PlayClip(AudioNames.BackgroundMetaMusic);
        }

        private void StartLevel(int location, int level)
        {
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}