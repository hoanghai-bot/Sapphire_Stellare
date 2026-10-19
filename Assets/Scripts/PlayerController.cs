using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Joystick joystick;
    private Vector2 movementDirection;

    private float cameraHeight;
    private float cameraWidth;
    private float minX, maxX, minY, maxY;
    private string lastAnimationState = "";

    public State_player state_Player;
    public float speed = 5;
    public bool canMove = true;



    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        joystick = GameController.instance.movementJoystisk;

        Camera camera = Camera.main;
        cameraHeight = 2f * camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;

        minX = -cameraWidth / 2f;
        maxX = cameraWidth / 2f;
        minY = -cameraHeight / 2f;
        maxY = cameraHeight / 2f;
    }
    void Update()
    {
        if (canMove)
        {
            if (joystick.Direction != Vector2.zero)
            {
                movementDirection = joystick.Direction.normalized;

                Vector3 move = (Vector3)joystick.Direction * Time.deltaTime * speed;
                transform.position += move;

                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minX, maxX),
                    Mathf.Clamp(transform.position.y, minY, maxY),
                    transform.position.z  
                );

                if (Mathf.Abs(joystick.Direction.x) > Mathf.Abs(joystick.Direction.y))
                {
                    if (joystick.Direction.x > 0 && lastAnimationState != "playerRight")
                    {
                        animator.Play("playerRight");
                        lastAnimationState = "playerRight";
                    }
                    else if (joystick.Direction.x < 0 && lastAnimationState != "playerLeft")
                    {
                        animator.Play("playerLeft");
                        lastAnimationState = "playerLeft";
                    }
                }
                else if (Mathf.Abs(joystick.Direction.y) > Mathf.Abs(joystick.Direction.x))
                {
                    if (joystick.Direction.y > 0 && lastAnimationState != "playerUp")
                    {
                        animator.Play("playerUp");
                        lastAnimationState = "playerUp";
                    }
                    else if (joystick.Direction.y < 0 && lastAnimationState != "playerDown")
                    {
                        animator.Play("playerDown");
                        lastAnimationState = "playerDown";
                    }
                }
            }
        }

        if (joystick.Direction == Vector2.zero)
        {
            rb.velocity = Vector2.zero;  
            if (lastAnimationState != "playerIdle")
            {
                animator.Play("playerIdle");
                lastAnimationState = "playerIdle";
            }
        }
    }
    public void Attacked()
    {
        Debug.Log("bi tan cong");
        if(state_Player == State_player.Nor)
        {
            state_Player = State_player.Immortal;
            HeartLoss();
            DOTween.Sequence()
                .Append(sprite.DOColor(new Color(1,1,1,.5f),.5f))
                .Append(sprite.DOColor(new Color(1, 1, 1, 1f), .5f))
                .Append(sprite.DOColor(new Color(1, 1, 1, .5f), .5f))
                .Append(sprite.DOColor(new Color(1, 1, 1, 1f), .5f))
                .AppendCallback(() => { state_Player = State_player.Nor; })
                .Play();
        }
    }
    public void HeartLoss()
    {
        LevelController.Instance.heart--;
        GameController.instance.ShowHeart();
        if(LevelController.Instance.heart == 0) 
        {
            Observer.Notify("WhenGameLost");
        }
    }
}
