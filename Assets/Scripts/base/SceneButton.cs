using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public static SceneButton instance;

    [Header("-----------------Level-----------------")]
    public Button selectLevel;
    public Button exitLevel;
    public GameObject spaceButtonLevel;
    public GameObject buttonOn;
    public int amountOfLevel;
    public bool irregular;
    public Transform[] posButtons;
    public bool lockLevel;
    public GameObject buttonOff;

    public bool haveStar;
    [Header("-----------------Play------------------")]
    public Button Play;
    [Header("-----------------UIGamePlay------------")]
    public Button home;
    public Button rePlay;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        AddPlay();
        AddLevel();
        AddUIGameplay();
    }

    //------------------------play--------------------------
    void AddPlay()
    {
        if (Play != null) Play.onClick.AddListener(ClickPlay);
    }
    void ClickPlay()
    {
        ActGamePlay(true);
        if (GetData.instance.levelPlaying >= GameController.instance.levelPrefabs.Length)
        {
            PlayLevelRandom();
        }
        else
        {
            ClickPlayLevelPlaying();
        }

    }

    //-------------------------selectLevel-----------------------------
    void AddLevel()
    {
        if (selectLevel != null) selectLevel.onClick.AddListener(ClickLevel);
        if (exitLevel != null) exitLevel.onClick.AddListener(ExitLevel);

    }
    void ClickLevel()
    {
        SceneController.instance.LevelMenu.SetActive(true);
        SelectLevel();
    }
    void ExitLevel()
    {
        SceneController.instance.LevelMenu.SetActive(false);
    }
    void SelectLevel()
    {
        foreach (Transform trf in spaceButtonLevel.transform)
        {
            Destroy(trf.gameObject);
        }
        if (irregular && posButtons != null)
        {
            for (int i = 0; i < posButtons.Length; i++)
            {
                int temp = i;
                ButtonSetting(temp, i, posButtons[temp].position);
                posButtons[temp].gameObject.SetActive(false);
            }
        }
        else
        {
            if (GameController.instance.levelPrefabs.Length > amountOfLevel)
            {
                for (int i = 0; i < GameController.instance.levelPrefabs.Length; i++)
                {
                    int temp = i;
                    ButtonSetting(temp, i);
                }
            }
            else
            {
                for (int i = 0; i < amountOfLevel; i++)
                {
                    int temp = i;
                    ButtonSetting(temp, i);
                }
            }
        }
    }
    void ButtonSetting(int temp, int id, Vector3 pos = default)
    {
        if (!lockLevel)
        {
            var clone = Instantiate(buttonOn, pos, Quaternion.identity, spaceButtonLevel.transform);
            clone.GetComponent<Button>().onClick.AddListener(() => { ClickSelectLevel(temp); });
            if (clone.GetComponentInChildren<TextMeshProUGUI>() != null)
            {
                var text = clone.GetComponentInChildren<TextMeshProUGUI>();
                text.text = (temp + 1).ToString();
            }
            StarSetting(clone, id);
        }
        else
        {
            if (GetData.instance.levelPlayed >= temp)
            {
                var clone = Instantiate(buttonOn, pos, Quaternion.identity, spaceButtonLevel.transform);
                clone.GetComponent<Button>().onClick.AddListener(() => { ClickSelectLevel(temp); });
                if (clone.GetComponentInChildren<TextMeshProUGUI>() != null)
                {
                    var text = clone.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = (temp + 1).ToString();
                }
                StarSetting(clone, id);
            }
            else
            {
                var clone = Instantiate(buttonOff, pos, Quaternion.identity, spaceButtonLevel.transform);
                if (clone.GetComponentInChildren<TextMeshProUGUI>() != null)
                {
                    var text = clone.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = (temp + 1).ToString();
                }
            }
        }
    }
    void StarSetting(GameObject clone, int id)
    {
        if (haveStar)
        {
            GetData.instance.GetArray();
            List<GameObject> list = new List<GameObject>();
            foreach (Transform child in clone.transform)
            {
                if (child.CompareTag("Respawn"))
                {
                    list.Add(child.gameObject);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (i < GetData.instance.starPoints[id])
                {
                    list[i].GetComponent<Image>().enabled = true;
                }
                else
                {
                    list[i].GetComponent<Image>().enabled = false;
                }
            }
        }
    }
    public void ClickSelectLevel(int nb)
    {
        ActGamePlay(true);
        DelInGamePlay();
        int count = GameController.instance.levelPrefabs.Length;
        if (nb >= count)
        {
            var lvplay = GetData.instance.levelPlaying = Random.Range(0, count);
            Instantiate(GameController.instance.levelPrefabs[lvplay], SceneController.instance.GamePlay.transform);
        }
        else
        {
            GetData.instance.levelPlaying = nb;
            Instantiate(GameController.instance.levelPrefabs[nb], SceneController.instance.GamePlay.transform);
        }
    }
    //--------------------UIGamPlay---------------------
    void AddUIGameplay()
    {
        if (home != null) home.onClick.AddListener(ClickHome);
        if (rePlay != null) rePlay.onClick.AddListener(ClickPlayLevelPlaying);
    }
    public void ClickHome()
    {
        DelInGamePlay();
        ActGamePlay(false);
    }
    //--------------------scene--------------------
    public void ClickPlayLevelPlaying()
    {
        DelInGamePlay();
        Instantiate(GameController.instance.levelPrefabs[GetData.instance.levelPlaying],
            SceneController.instance.GamePlay.transform);
    }
    public void PlayLevelRandom()
    {
        DelInGamePlay();
        Instantiate(GameController.instance.levelPrefabs[Random.Range(0, GameController.instance.levelPrefabs.Length)],
            SceneController.instance.GamePlay.transform);
    }
    public void ActGamePlay(bool temp)
    {
        DelInGamePlay();
        SceneController.instance.BGGamePlay.SetActive(temp);
        SceneController.instance.UIGamePlay.SetActive(temp);
        SceneController.instance.Menu.SetActive(!temp);
        if (SceneController.instance.GamePlay != null) SceneController.instance.GamePlay.SetActive(temp);
        if (SceneController.instance.LevelMenu != null) SceneController.instance.LevelMenu.SetActive(false);
    }
    public void DelInGamePlay()
    {
        foreach (Transform trf in SceneController.instance.GamePlay.transform)
        {
            Destroy(trf.gameObject);
        }
    }
}
