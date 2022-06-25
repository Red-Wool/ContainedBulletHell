using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Control List", menuName = "Controls/ControlList")]
public class ControlList : ScriptableObject
{
    [SerializeField] private DefaultControl[] controls;

    private DefaultControl Find(string id)
    {
        for (int i = 0; i < controls.Length; i++)
        {
            if (controls[i].name == id)
            {
                return controls[i];
            }
        }

        Debug.LogError("Nonvalid Control! " + id);
        return controls[0];
    }

    public KeyCode GetControl(string id)
    {
        return Find(id).key;
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
    }

    public void ResetControl(string id)
    {
        for (int i = 0; i < controls.Length; i++)
        {
            if (controls[i].name == id)
            {
                controls[i].key = controls[i].defaultKey;
                return;
            }
        }
    }
}

[System.Serializable]
public struct DefaultControl
{
    public string name;
    public KeyCode key;
    public KeyCode defaultKey;
}