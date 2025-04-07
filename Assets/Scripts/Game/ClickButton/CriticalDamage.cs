using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.ClickButton
{
    public class CriticalDamage : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        [SerializeField] private ParticleSystemWrapper _particleSystem;

        [SerializeField] private Vector2 posX;
        [SerializeField] private Vector2 posY;

        public event UnityAction OnClick;

        public void Initialize(Sprite sprite)
        {
            _image.sprite = sprite;
            OnButton();
            OffButton();
        }

        public void SubscribeOnClick(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void UnsubscribeOnClick(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }
        public void OnButton()
        {
            gameObject.SetActive(true);
            float randX = Random.Range(posX.x, posX.y);
            float randY = Random.Range(posY.x, posY.y);

            Vector2 pos = new Vector2(randX, randY);
            transform.localPosition = pos;
            _particleSystem.PlayAtPosition(pos);
        }
        public void OffButton()
        {
            gameObject.SetActive(false);
        }
    }
}