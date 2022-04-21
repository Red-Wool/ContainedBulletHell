using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Boss Bullet Data", fileName = "New Boss Bullet Data")]
public class BossBulletData : ScriptableObject
{
    [SerializeField] private EnemyBulletSession[] session; public EnemyBulletSession[] Session { get { return session; } }
}

[CreateAssetMenu(menuName = "Data/Boss Move Data", fileName = "New Boss Move Data")]
public class BossMoveData : ScriptableObject
{
    [SerializeField] private EnemyBulletSession[] session; public EnemyBulletSession[] Session { get { return session; } }
}