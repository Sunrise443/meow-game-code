using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCandy : MonoBehaviour
{
    bool isHealingZone = false;
    public int amount = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        CatController controller = other.GetComponent<CatController>();

        if (controller != null && (controller.health < controller.maxHealth))
        {
            controller.ChangeHealth(amount, isHealingZone);
            Destroy(gameObject);
        }    
    }
}
