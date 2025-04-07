using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabNPC : MonoBehaviour
{
    internal int dialogCount = 0;
    public string[] characterName;
    public string[] characterPhrase;
    public Texture[] characterEmotions;
    public bool isSecTalkative = false;

    public bool isTalkative = false;

    //Moth Dialog
    internal string[] Mnames = { "Cat", "Michael" };
    internal string[] Mphrases =
    {
        "!!?",
        "Who are you? You seem familiar.",
        "Good evening... if there are evenings here.",
        "I'm working here and saving your poor litle sister, while you're having fun up there.",
        "What a great brother you are :)"
    };
    internal string[][] Mchoices =
    {
        new string[]
        {
            "Huh? I'm not a bad brother!",
            "I don't think I know what you're talking about...",
            "Well... Yeah. I guess you're right. I'm afwul. It was my fault right?"
        }
    };

    //Dog Dialog
    internal string[] Dnames = { "Cat", "Daiji" };
    internal string[] Dphrases =
    {
        "Oh hi, Diaji! Something weird is going on here.",
        "Glad I finally found someone trustworthy!",
        "Soo..?",
        "Hmm... I think I should tell you the truth.",
        "Yeah I would like to know what really is going on",
        "Your sister and Michael have been trying to create a top-secret [] for government.",
        "That's why he had to lie that Lily was kidnapped.",
        "SHE WAS KIDNAPPED?!",
        "Oh, I see. They didn't even bother to tell you. Alright, so...",
        "She wasn't kidnapped just by -someone-. She was kidnapped by Michael.",
        "I think I could tell she had no choice anyways.",
        "You forgot all that, and now you're trapped in your head"
    };
    internal string[][] Dchoices =
    {
        new string[]
        {
            "Can I get out of here somehow?",
            "Oh.. I can't think of something to do?",
            "So you won't be real when I wake up? I wanna stay with you."
        }
    };
}
