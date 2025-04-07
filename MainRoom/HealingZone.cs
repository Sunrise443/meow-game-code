using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    bool isHealingZone = true;
    public int amount = 1;
    private void OnTriggerStay2D(Collider2D other)
    {
        CatController controller = other.GetComponent<CatController>();

        if (controller != null && (controller.health < controller.maxHealth))
        {
            controller.ChangeHealth(amount, isHealingZone);
        }
    }
}
