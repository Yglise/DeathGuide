using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    Animator anim;
    [SerializeField]GameObject hitBox;
    float delay = 0.7f;
    float delayTimer = 0;
    bool play = false;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > delay)
        {
            if (!play) 
            {
                anim.SetTrigger("Strike");
                play = true;
                StartCoroutine(HitBoxIn(0.5f));
                StartCoroutine(DestroyIn(2));
            }         
        }
        else 
        {
            delayTimer += Time.deltaTime;
        }
    }

    IEnumerator DestroyIn(int time) 
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    IEnumerator HitBoxIn(float time)
    {
        yield return new WaitForSeconds(time);
        hitBox.SetActive(true);
    }

    public void SetDelay(float time) 
    {
        delay = time;
    }
}
