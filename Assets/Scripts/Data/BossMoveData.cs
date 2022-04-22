using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Boss Move Data", fileName = "New Boss Move Data")]
public class BossMoveData : ScriptableObject
{
    [SerializeField] private EnemyMoveSession[] session; public EnemyMoveSession[] Session { get { return session; } }
}
