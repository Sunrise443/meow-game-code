using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LabUIHandler : MonoBehaviour
{
    public static LabUIHandler instance { get; private set; }

    //Variables related to dialog window
    private VisualElement m_NPCDialogue;
    private Button m_Button1;
    private Button m_Button2;
    private Button m_Button3;
    private bool cDialogEnded = false;
    private bool dDialogEnded = false;
    private float m_TimerDisplay;
    public float displayTime = 4.0f;

    //Variables related to endings
    int end1 = 0;
    int end2 = 0;
    int[] options = { 1, 2 };
    int randomEnding;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        //Dialog window
        m_NPCDialogue = uiDocument.rootVisualElement.Q<VisualElement>("DialogueWindow");
        m_NPCDialogue.style.display = DisplayStyle.None;

        //Choice buttons
        m_Button1 = uiDocument.rootVisualElement.Q<Button>("Choice1");
        m_Button2 = uiDocument.rootVisualElement.Q<Button>("Choice2");
        m_Button3 = uiDocument.rootVisualElement.Q<Button>("Choice3");

        m_Button1.style.visibility = Visibility.Hidden;
        m_Button2.style.visibility = Visibility.Hidden;
        m_Button3.style.visibility = Visibility.Hidden;

        //Endings
        int randomIndex = Random.Range(0, options.Length);
        randomEnding = options[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NPCDialogue.style.display = DisplayStyle.None; 
                if (dDialogEnded)
                {
                    if (end1 > end2)
                    {
                        SceneManager.LoadScene("Ending1");
                    }
                    else if (end1 < end2)
                    {
                        SceneManager.LoadScene("Ending2");
                    }
                    else
                    {
                        if (randomEnding == 1)
                        {
                            SceneManager.LoadScene("Ending1");
                        }
                        else
                        {
                            SceneManager.LoadScene("Ending2");
                        }
                    }

                }
            }
        }
        if (cDialogEnded)
        {
            Time.timeScale = 1;
        }
    }

    public void DisplayDialog(LabNPC character,LabCatController controller)
    {
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        VisualElement charPicture = m_NPCDialogue.Q<VisualElement>("Picture");
        VisualElement DialogWondow = m_NPCDialogue.Q<VisualElement>("DialogueBackground");


        if (!character.isTalkative)
        {
            if (character.characterName.Length == 1)
            {
                textCharacter.text = character.characterName[0];
            }
            else if (character.characterName.Length > 0)
            {
                textCharacter.text = character.characterName[character.dialogCount];
            }
            else
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
                charPicture.style.height = character.characterEmotions[character.dialogCount].height / 10 * 8;
                charPicture.style.width = character.characterEmotions[character.dialogCount].width / 10 * 8;
            }

            character.dialogCount++;
            if (character.dialogCount >= character.characterPhrase.Length)
            {
                character.dialogCount = character.characterPhrase.Length - 1;
            }

            m_NPCDialogue.style.display = DisplayStyle.Flex;
            m_TimerDisplay = displayTime;
        } else
        {

            //Michael dialog
            if (end1 == 0 && end2 == 0 && !character.isSecTalkative)
            {
                Time.timeScale = 0;

                m_Button1.RegisterCallback<MouseUpEvent>((evt) => Choice1Made(character));
                m_Button2.RegisterCallback<MouseUpEvent>((evt) => Choice2Made(character));
                m_Button3.RegisterCallback<MouseUpEvent>(evt => Choice3Made(character));

                if (character.dialogCount < 4)
                {
                    if (character.dialogCount == 1 || character.dialogCount == 0)
                    {
                        textCharacter.text = character.Mnames[0];
                        charPicture.style.backgroundImage = (StyleBackground)character.characterEmotions[0];
                        charPicture.style.height = character.characterEmotions[0].height / 10 * 8;
                        charPicture.style.width = character.characterEmotions[0].width / 10 * 8;
                    }
                    else
                    {
                        textCharacter.text = character.Mnames[1];
                        charPicture.style.backgroundImage = (StyleBackground)character.characterEmotions[1];
                        charPicture.style.height = character.characterEmotions[1].height / 10 * 8;
                        charPicture.style.width = character.characterEmotions[1].width / 10 * 8;
                    }
                    textPhrase.text = character.Mphrases[character.dialogCount];
                    m_NPCDialogue.style.display = DisplayStyle.Flex;
                    character.dialogCount++;
                }
                else if (character.dialogCount == 4)
                {
                    textCharacter.text = character.Mnames[1];
                    textPhrase.text = character.Mphrases[4];
                    charPicture.style.backgroundImage = (StyleBackground)character.characterEmotions[1];
                    charPicture.style.height = character.characterEmotions[1].height / 10 * 8;
                    charPicture.style.width = character.characterEmotions[1].width / 10 * 8;
                    m_Button1.text = character.Mchoices[0][0];
                    m_Button2.text = character.Mchoices[0][1];
                    m_Button3.text = character.Mchoices[0][2];
                    m_Button1.style.visibility = Visibility.Visible;
                    m_Button2.style.visibility = Visibility.Visible;
                    m_Button3.style.visibility = Visibility.Visible;
                    m_NPCDialogue.style.display = DisplayStyle.Flex;
                }

                m_TimerDisplay = 4.0f;
            } else if (cDialogEnded && character.isSecTalkative)
            //Daiji dialog
            {
                Time.timeScale = 0;
                Debug.Log(character.dialogCount);

                m_Button1.RegisterCallback<MouseUpEvent>((evt) => Choice21Made(character));
                m_Button2.RegisterCallback<MouseUpEvent>((evt) => Choice22Made(character));
                m_Button3.RegisterCallback<MouseUpEvent>(evt => Choice23Made(character));

                if (character.dialogCount < 11)
                {
                    if (character.dialogCount>=3 && character.dialogCount<=6 || character.dialogCount>=8 && character.dialogCount<=11)
                    {
                        textCharacter.text = character.Dnames[1];
                        charPicture.style.backgroundImage = (StyleBackground)character.characterEmotions[1];
                        charPicture.style.height = character.characterEmotions[1].height / 10 * 8;
                        charPicture.style.width = character.characterEmotions[1].width / 10 * 8;
                    }
                    else
                    {
                        textCharacter.text = character.Dnames[0];
                        charPicture.style.backgroundImage = (StyleBackground)character.characterEmotions[0];
                        charPicture.style.height = character.characterEmotions[0].height / 10 * 8;
                        charPicture.style.width = character.characterEmotions[0].width / 10 * 8;
                    }
                    textPhrase.text = character.Dphrases[character.dialogCount];
                    m_NPCDialogue.style.display = DisplayStyle.Flex;
                    character.dialogCount++;
                }
                else if (character.dialogCount == 11)
                {
                    textCharacter.text = character.Dnames[1];
                    charPicture.style.backgroundImage = (StyleBackground)character.characterEmotions[1];
                    charPicture.style.height = character.characterEmotions[1].height / 10 * 8;
                    charPicture.style.width = character.characterEmotions[1].width / 10 * 8;
                    textPhrase.text = character.Dphrases[11];
                    m_Button1.text = character.Dchoices[0][0];
                    m_Button2.text = character.Dchoices[0][1];
                    m_Button3.text = character.Dchoices[0][2];
                    m_Button1.style.visibility = Visibility.Visible;
                    m_Button2.style.visibility = Visibility.Visible;
                    m_Button3.style.visibility = Visibility.Visible;
                    m_NPCDialogue.style.display = DisplayStyle.Flex;
                }
            }
        }
    }

    //Michael dialog functions
    public void Choice1Made (LabNPC character) {
        Debug.Log("Button1 clicked.");
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        textCharacter.text = character.Mnames[1];
        textPhrase.text = "Whatever you consider to be truth.";
        m_Button1.style.visibility = Visibility.Hidden;
        m_Button2.style.visibility = Visibility.Hidden;
        m_Button3.style.visibility = Visibility.Hidden;
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        cDialogEnded = true;
        end1++;
    }
    public void Choice2Made(LabNPC character)
    {
        Debug.Log("Button2 clicked.");
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        textCharacter.text = character.Mnames[1];
        textPhrase.text = "You have something to learn then.";
        m_Button1.style.visibility = Visibility.Hidden;
        m_Button2.style.visibility = Visibility.Hidden;
        m_Button3.style.visibility = Visibility.Hidden;
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        cDialogEnded = true;
        end2++;
    }
    public void Choice3Made(LabNPC character)
    {
        Debug.Log("Button3 clicked.");
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        textCharacter.text = character.Mnames[1];
        textPhrase.text = "Very well.";
        m_Button1.style.visibility = Visibility.Hidden;
        m_Button2.style.visibility = Visibility.Hidden;
        m_Button3.style.visibility = Visibility.Hidden;
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        cDialogEnded = true;
        end2++;
    }

    //Daiji dialog functions
    public void Choice21Made(LabNPC character)
    {
        Debug.Log("Button1 clicked.");
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        textCharacter.text = character.Dnames[1];
        textPhrase.text = "Yes. You can. Glad you're brave enough to do that.";
        m_Button1.style.visibility = Visibility.Hidden;
        m_Button2.style.visibility = Visibility.Hidden;
        m_Button3.style.visibility = Visibility.Hidden;
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        dDialogEnded = true;
        end1++;
    }
    public void Choice22Made(LabNPC character)
    {
        Debug.Log("Button2 clicked.");
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        textCharacter.text = character.Dnames[1];
        textPhrase.text = "Yeah. As always.";
        m_Button1.style.visibility = Visibility.Hidden;
        m_Button2.style.visibility = Visibility.Hidden;
        m_Button3.style.visibility = Visibility.Hidden;
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        dDialogEnded = true;
        end2++;
    }
    public void Choice23Made(LabNPC character)
    {
        Debug.Log("Button3 clicked.");
        Label textCharacter = m_NPCDialogue.Q<Label>("Character");
        Label textPhrase = m_NPCDialogue.Q<Label>("Phrase");
        textCharacter.text = character.Dnames[1];
        textPhrase.text = "... Okay.";
        m_Button1.style.visibility = Visibility.Hidden;
        m_Button2.style.visibility = Visibility.Hidden;
        m_Button3.style.visibility = Visibility.Hidden;
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        dDialogEnded = true;
        end2++;
    }
}                                                             
