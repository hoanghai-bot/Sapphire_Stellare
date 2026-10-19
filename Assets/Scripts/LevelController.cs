using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public int totalDia;
    public int heart = 3;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameController.instance.ShowHeart();
    }

}
