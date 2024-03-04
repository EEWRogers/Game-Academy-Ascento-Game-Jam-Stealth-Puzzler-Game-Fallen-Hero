using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue Line", order = 0)]
public class DialogueLines : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string[] dialogue;
    public string[] Dialogue { get { return dialogue; } }
}
