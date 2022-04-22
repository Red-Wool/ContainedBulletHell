using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[System.Serializable]
public class EnemyBulletPattern
{
    [Header("Bullets")]
    public float[] bulletAngles;

    [Header("Data"), Space(10)]
    public float speed;

    //This varible is to store which object to pull from in BossShoot
    public int key;
}

/*[System.Serializable]
public class EnemyBulletData
{
    
    public float angle;
    
}*/

[System.Serializable]
public class EnemyBulletSequence
{
    public EnemyBulletPattern pattern;

    [Space(10)]
    public int loops;
    public float rate;
    public float wait;

    [Space(10), Header("Bullet Config")]
    public bool aimAtPlayer;
    public float angleOffset;
    public float loopAngle;
    public float loopSpeed;
}

[System.Serializable]
public class EnemyBulletSession
{
    [Header("Bullets")]
    public EnemyBulletSequence[] sequence;

    [Space(10), Header("Next Possible Session")]
    public int[] nextSessions;

    [Space(10), Header("Time to next Session")]
    public float coolDown;

    [Space(10), Header("Bullet Spawn Positions")]
    public Vector2[] spawnPos;
}

[System.Serializable]
public class EnemyMovePattern
{
    public Vector2[] pos;
    public float time;
    public Ease easeStyle;
}

[System.Serializable]
public class EnemyMoveSequence
{
    public EnemyMovePattern pattern;

    [Space(10)]
    public Vector2 randomDisplace;
    public float wait;
}

[System.Serializable]
public class EnemyMoveSession
{
    [Header("Move")]
    public EnemyMoveSequence[] sequence;

    [Space(10), Header("Next Possible Session")]
    public int[] nextSessions;

    [Space(10), Header("Time to next Session")]
    public float coolDown;
}