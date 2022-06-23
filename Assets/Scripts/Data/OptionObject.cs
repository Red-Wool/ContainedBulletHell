using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Option", fileName = "New Options")]
public class OptionObject : ScriptableObject
{
    

    public float musicPercent;
    public float sfxPercent;

    public ControlList controls;


}
