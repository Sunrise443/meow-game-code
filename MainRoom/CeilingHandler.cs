using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CeilingHandler : MonoBehaviour
{
    private VisualElement key;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CatController controller = other.GetComponent<CatController>();

        if (controller != null && (controller.hasKey == true))
        {
            Destroy(gameObject);
            controller.hasKey = false;
            UIHandler.instance.hasKeyDeletion = true;
        }
    }
}
