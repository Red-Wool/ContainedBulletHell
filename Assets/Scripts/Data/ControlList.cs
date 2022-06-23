using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Control List", menuName = "Controls/ControlList")]
public class ControlList : ScriptableObject
{
    [SerializeField] private DefaultControl[] controls;

    public KeyCode GetControl(string id)
    {
        for (int i = 0; i < controls.Length; i++)
        {
            if (controls[i].name == id)
            {
                return controls[i].key;
            }
        }

        Debug.LogError("Tried to get Nonvalid KeyCode! " + id);
        return KeyCode.Alpha0;
    }

    public void SetControl(string id, KeyCode newKey)
    {
        for (int i = 0; i < controls.Length; i++)
        {
            if (controls[i].name == id)
            {
                controls[i].key = newKey;
                return;
            }
        }
        Debug.LogError("Tried to set Nonvalid KeyCode! " + id);
    }
}

[System.Serializable]
public struct DefaultControl
{
    public string name;
    public KeyCode key;
}