using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamonScript : MonoBehaviour
{

    private Tween animIdle;
    private void Start()
    {
        animIdle = transform.DOMoveY(transform.position.y + .5f, .5f).SetLoops(-1,LoopType.Yoyo);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            animIdle.Kill();
            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y + 2f, .5f))
                .AppendInterval(.5f)
                .Append(transform.DOMove(ChestScript.instance.transform.position, .5f))
                .AppendCallback(() =>
                {
                    ChestScript.instance.CalculateDia();
                    Destroy(gameObject);
                })
                .Play();
        }
    }
}
