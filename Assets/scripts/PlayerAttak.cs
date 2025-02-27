using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttak : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] RawImage _img;

    public void Initialize()
    {
        //партиклы
    }
    public void SubscribeOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
    public void UnsubscribeOnClick(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
}
