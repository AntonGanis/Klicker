using UnityEngine;
using UnityEngine.Events;

namespace Game.ClickButton {
    public class ClickButtonManager : MonoBehaviour {
        [SerializeField] private ClickButton _clickButton;
        [SerializeField] private CriticalDamage _clickButtonCritical;
        [SerializeField] private ClickButtonConfig _buttonConfig;

        public event UnityAction<bool> OnClicked;
        [SerializeField] private int _chanceCrit;
        bool CriticalCombo;
        int punh;
        public void Initialize()
        {
            _clickButton.Initialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
            _clickButtonCritical.Initialize(_buttonConfig.CriticalSprite);

            _clickButton.SubscribeOnClick(() => OnClicked?.Invoke(false));
            _clickButtonCritical.SubscribeOnClick(() => OnClicked?.Invoke(true));

            _clickButton.SubscribeOnClick(ChanceCritical);
            _clickButtonCritical.SubscribeOnClick(NextCritical);
        }
        private void ChanceCritical()
        {
            if (CriticalCombo == false)
            {
                int chance = Random.Range(0, 100);
                if (chance < _chanceCrit)
                {
                    _clickButtonCritical.OnButton();
                    CriticalCombo = true;
                    punh = 0;
                }
            }
            else
            {
                if (punh > 3)
                {
                    _clickButtonCritical.OffButton();
                    CriticalCombo = false;
                }
                punh++;
            }
        }
        private void NextCritical()
        {
            _clickButtonCritical.OnButton();
        }
    }
}
