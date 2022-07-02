using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletModify
{
    [Header("Movement")]
    public float speedUp;

    public bool chase;

    [Space(10),Header("Angle")]
    public bool randomAngle;
    public float angleRange;
    public float angleUp;

    [Space(10), Header("Spawn")]
    public bool spawnOnDeath;
    public EnemyBulletSequence bulletOnDeath;
}
