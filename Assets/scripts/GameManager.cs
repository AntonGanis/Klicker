using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerAttak _PlayerAttak;
    int o;
    void Awake()
    {
        _PlayerAttak.SubscribeOnClick(ShowClick);
    }

    void ShowClick()
    {
        o++;
        Debug.Log(o);
    }
}
