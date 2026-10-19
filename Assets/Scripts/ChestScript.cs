using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public static ChestScript instance;

    
    public Sprite open;
    public GameObject twinkle;
    public GameObject effect;
    public GameObject light;

    private bool canOpen;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        effect.transform.position = transform.position;
    }
    public void CalculateDia()
    {
        LevelController.Instance.totalDia--;
        if (LevelController.Instance.totalDia == 0) 
        {
            light.SetActive(true);
            canOpen = true;
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canOpen)
        {
            canOpen = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = open;
            twinkle.SetActive(true);
            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() => { effect.SetActive(true); })
                .Append(effect.transform.DOMove(DoorScript.instance.transform.position, 1f))
                .AppendCallback(() =>
                {
                    effect.SetActive(false);
                    DoorScript.instance.OpenDoor();
                }).Play();
        }
    }
}
