using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleFloor : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowHole()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CatController controller = other.GetComponent<CatController>();

        if (controller != null)
        {
            SceneManager.LoadScene("second try");
            gameObject.SetActive(false);
        }
    }
}
