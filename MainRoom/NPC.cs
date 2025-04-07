using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string[] characterName;
    public string[] characterPhrase;
    internal int dialogCount = 0;
    public Texture[] characterEmotions;
    public bool isItemObject = false;
    internal bool hasInteracted = false;
    public bool isShownClose = false;
}
