using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private float timer1;
    // Start is called before the first frame update
    void Start()
    {
        timer1 = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer1 -= Time.deltaTime;
        if (timer1 < 0)
        {
            SceneManager.LoadScene("Thanks");
        }
    }
}
