using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Sprite start;
    public Sprite change;

    public bool canAttack;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if(canAttack)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
    private void FixedUpdate()
    {
        if (canAttack)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 2.5f)
            {
                sprite.sprite = change;
            }
            else
            {
                sprite.sprite = start;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (canAttack)
        {
            PlayerController.instance.Attacked();
        }
    }
}
