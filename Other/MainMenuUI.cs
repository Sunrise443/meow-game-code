using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    //Button variables
    private Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uIDocument = GetComponent<UIDocument>();
        playButton = uIDocument.rootVisualElement.Q<Button>("PlayButton");
    }

    // Update is called once per frame
    void Update()
    {
        playButton.RegisterCallback<MouseUpEvent>((evt) => SceneManager.LoadScene("first try"));
    }
}
