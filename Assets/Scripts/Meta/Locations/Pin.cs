using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
    public class Pin : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private Sprite _spriteCurrent;
        [SerializeField] private Sprite _spritePassed;
        [SerializeField] private Sprite _spriteClose;

        public void Initialize(int levelMumber, PinType pinType, UnityAction clickCallback)
        {
            _text.text = $"Óð. {levelMumber}";
            _image.sprite = pinType switch
            {
                PinType.Current => _spriteCurrent,
                PinType.Close => _spriteClose,
                PinType.Passed => _spritePassed,
            };
            _button.onClick.AddListener(()=>clickCallback?.Invoke());
        }

    }
}


