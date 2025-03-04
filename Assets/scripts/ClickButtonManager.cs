using UnityEngine;
using UnityEngine.UI;

public class ClickButtonManager : ScriptableObject
{
    [SerializeField] ClickButton _clickButton;
    [SerializeField] ClickButtonConfig _clickButtonConfig;
    public Event OnClicked _onClicked;

    public void Inicialize()
    {
        _clickButton.Initialize(_clickButtonConfig);

    }
}
