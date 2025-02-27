using UnityEngine;

[System.Flags]
public enum DamageType
{
    None = 0,
    Physical = 1 << 0,
    Fire = 1 << 1,
    Cold = 1 << 2,
    Electric = 1 << 3
}
