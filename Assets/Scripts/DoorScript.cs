using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public static DoorScript instance;

    public GameObject mask;
    public GameObject effect;

    private void Awake()
    {
        instance = this;
    }
    public void OpenDoor()
    {
        GetComponent<Collider2D>().enabled = true;
        mask.transform.DOScale(new Vector3(2, 2), 1f)
            .OnComplete(() => { effect.SetActive(true); });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Observer.Notify("WhenGameWin");
        }
    }
}
