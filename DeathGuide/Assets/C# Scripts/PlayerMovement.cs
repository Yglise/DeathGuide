using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LighteningManager lm;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
    Animator anim;

    Vector2 movement;

    float idleTime = 0f;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Input
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0) 
        {
            anim.SetFloat("Horizontal", h);
            anim.SetFloat("Vertical", v);
        }

        if (h == 0 && v == 0)
        {
            idleTime += Time.deltaTime;
        }
        else 
        {
            idleTime = 0;
        }
        if (idleTime > 3) 
        {
            lm.CastStrike(transform.position);
            idleTime = 0;
        }
        movement = new Vector2(h, v).normalized;
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Lightening")) 
        {
            lm.GameOver();
            this.gameObject.SetActive(false);
        }
    }
}
