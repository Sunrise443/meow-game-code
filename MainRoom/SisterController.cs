using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
    public float speed = 1.0f;
    public float timeChange = 3.0f;
    public int amount = 1;

    int vertical;
    float timer;
    int direction = 1;
    bool isHealingZone = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = timeChange;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Random.Range(0, 2);
        timer -= Time.deltaTime;
        if (timer<0)
        {
            direction = -direction;
            timer = timeChange;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical==0)
        {
            position.y += speed * Time.deltaTime * direction;
        }
        else
        {
            position.x += speed * Time.deltaTime * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CatController controller = other.GetComponent<CatController>();

        if (controller != null)
        {
            controller.ChangeHealth(-(amount), isHealingZone);
        }
    }
}
