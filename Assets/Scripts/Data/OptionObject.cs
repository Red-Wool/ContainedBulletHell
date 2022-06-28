using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Option", fileName = "New Options")]
public class OptionObject : ScriptableObject
{
    public float musicPercent;
    public float sfxPercent;

    public bool toggleShoot;
    public bool toggleSlowMove;

    public ControlList controls;


}
