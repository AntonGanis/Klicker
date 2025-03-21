using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Meta.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private List<Pin> _pins;

        public void Initialize(UnityAction<int> levelStartCallback)
        {
            int currentLevel = 3;
            for (int i = 0; i < _pins.Count; i++)
            {
                int level = i + 1;
                var pinType = currentLevel > level
                    ? PinType.Passed 
                    :  currentLevel == level
                       ? PinType.Current 
                       : PinType.Close;
                _pins[i].Initialize(level, pinType, ()=> levelStartCallback?.Invoke(level));
            }
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}


