using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTV : MonoBehaviour
{
    bool isHealingZone = false;
    public int amount = 1;
    private void OnTriggerStay2D(Collider2D other)
    {
        CatController controller = other.GetComponent<CatController>();

        if (controller != null)
        {
            controller.ChangeHealth(-(amount), isHealingZone);
        }
    }
}
