using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Resistance
{
    [Header("Сопротивления")]
    [Range(-1f, 1f)]
    public float physicalResistance;
    [Range(-1f, 1f)]
    public float fireResistance;
    [Range(-1f, 1f)]
    public float electricResistance;
    [Range(-1f, 1f)]
    public float coldResistance;
}

[System.Serializable]
public class HP
{
    [Header("Здоровье")]
    public int health;
    public int healthMax;
    public Slider slider1;
}

public class Health : MonoBehaviour
{
    public Resistance resistance;
    public HP hp;

    void Start()
    {
        hp.slider1.value = hp.health;
        if(hp.healthMax == 0)
        {
            hp.healthMax = hp.health;
        }
        hp.slider1.maxValue = hp.healthMax;
    }

    public void TakeDamage(int amount, DamageType type)
    {
        float effectiveDamage = amount;

        switch (type)
        {
            case DamageType.Physical:
                effectiveDamage *= (1 - resistance.physicalResistance);
                break;

            case DamageType.Fire:
                effectiveDamage *= (1 - resistance.fireResistance);
                break;

            case DamageType.Electric:
                effectiveDamage *= (1 - resistance.electricResistance);
                break;

            case DamageType.Cold:
                effectiveDamage *= (1 - resistance.coldResistance);
                break;
        }

        hp.health -= Mathf.RoundToInt(effectiveDamage);
        hp.health = Mathf.Max(hp.health, 0);
        hp.slider1.value = hp.health;
        if (hp.health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        if (transform.tag != "Player")
        {
            gameObject.SetActive(false);
        }
        else
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            Application.LoadLevel(sceneNumber);
        }
    }
}
