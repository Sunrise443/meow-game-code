using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    //HealthBar
    private VisualElement m_HealthBar;

    //Dialog window
    public float displayTime = 4.0f;
    private VisualElement m_NPCDialogue;
    private VisualElement ShownClose;
    private VisualElement ShownClose1;
    private int countShow = 0;
    private float m_TimerDisplay;

    //Items
    private VisualElement key;
    internal bool hasKeyDeletion = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Healthbar
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_HealthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        //Items
        key = uiDocument.rootVisualElement.Q<VisualElement>("Key");
        key.style.display = DisplayStyle.None;

        //Dialog window
        m_NPCDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        ShownClose = uiDocument.rootVisualElement.Q<VisualElement>("ShownClose");
        ShownClose1 = uiDocument.rootVisualElement.Q<VisualElement>("ShownClose1");
        m_NPCDialogue.style.display = DisplayStyle.None;
        ShownClose.style.display = DisplayStyle.None;
        ShownClose1.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NPCDialogue.style.display = DisplayStyle.None;
                ShownClose.style.display = DisplayStyle.None;
                ShownClose1.style.display = DisplayStyle.None;
            }
        }

        if (hasKeyDeletion)
        {
            key.style.display = DisplayStyle.None;
        }
    }

    public void DisplayDialog(NPC character, CatController controller)
    {
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        VisualElement charPicture = m_NPCDialogue.Q<VisualElement>("Picture");
        VisualElement shownClose = m_NPCDialogue.Q<VisualElement>("ShownClose");

        if (character.characterName.Length == 1)
        {
            textCharacter.text = character.characterName[0];
        } else if (character.characterName.Length > 0)
        {
            textCharacter.text = character.characterName[character.dialogCount];
        } else
        {
            textCharacter.text = "...";
        }

        if (character.characterPhrase.Length > 0)
        {
            textPhrase.text = character.characterPhrase[character.dialogCount];
        }

        if (character.characterEmotions.Length > 0)
        {
            charPicture.style.backgroundImage = (StyleBackground)character.characterEmotions[character.dialogCount];
            charPicture.style.height = character.characterEmotions[character.dialogCount].height/10*8;
            charPicture.style.width = character.characterEmotions[character.dialogCount].width/10*8;
        }

        character.dialogCount++;
        if (character.dialogCount >= character.characterPhrase.Length)
        {
            character.dialogCount = character.characterPhrase.Length-1;
        }

        if (!controller.hasKey)
        {
            key.style.display = DisplayStyle.None;
        }

        if (character.isShownClose)
        {
            Debug.Log(countShow);
            if (countShow==2)
            {
                ShownClose.style.display = DisplayStyle.Flex;
                countShow = -1;
            }
            else if (countShow!=-1)
            {
                countShow += 1;
                ShownClose1.style.display = DisplayStyle.Flex;
            }
        }


        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;

        if (character.isItemObject && character.dialogCount<=1 && !character.hasInteracted)
        {
            Debug.Log(character.dialogCount);
            controller.hasKey = true;
            key.style.display = DisplayStyle.Flex;
        }
        
        character.hasInteracted = true;
    }

    public void SetHealthValue(float percentage)
    {
        m_HealthBar.style.width = Length.Percent(100 * percentage);
    }
}
