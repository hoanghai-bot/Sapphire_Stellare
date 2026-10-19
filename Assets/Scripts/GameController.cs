using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_player 
{
    Nor,Immortal
}
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject[] levelPrefabs;
    public int starPoint;
    public GameObject[] heart;
    public Joystick movementJoystisk;

    private void Awake()
    {
        instance = this;
    }
    public void ShowHeart()
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if(i < LevelController.Instance.heart)
            {
                heart[i].SetActive(true);
            }
            else
            {
                heart[i].SetActive(false);
            }
        }
    }
}
