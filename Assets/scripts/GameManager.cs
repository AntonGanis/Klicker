using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ClickButton _PlayerAttak;
    [SerializeField] EnemyManager _enemyManager;
    [SerializeField] HealthBar _healthBar;
    int o;
    void Awake()
    {
        _PlayerAttak.SubscribeOnClick(ShowClick);
        _enemyManager.Inialize(_healthBar);
    }

    void ShowClick()
    {
        o++;
        Debug.Log(o);
    }


}