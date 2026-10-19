
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [Header("-------------scene------------")]
    public GameObject BGMenu;
    public GameObject Menu;
    public GameObject BGGamePlay;
    public GameObject UIGamePlay;
    public GameObject GamePlay;
    public GameObject LevelMenu;
    [Header("-------------popup------------")]
    public GameObject HTP;
    public GameObject Shop;
    public GameObject Win;
    public GameObject Lost;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Observer.AddObserver("WhenGameWin", WhenGameWin);
        Observer.AddObserver("WhenGameLost", WhenGameLost);
    }
    void WhenGameWin(object[] data)
    {
        Win.SetActive(true);
        PlayerController.instance.canMove = false;
    }
    void WhenGameLost(object[] data)
    {
        Lost.SetActive(true);
        PlayerController.instance.canMove = false;
    }
}
