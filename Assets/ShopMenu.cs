using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GestureRecognizer;

public class ShopMenu : MonoBehaviour {

    // Use this for initialization

    GestureRecognizer.GestureBehaviour gesturecolor;

    public Sprite[] unlocks;
    private int Amount;
    public GameObject scoreBoard;
    public GameObject MainMenu;
    public Button[] buttox;
    public Image[] Locks;
    LevelController level;
    Color purple = new Color(0.255f, 0.036f, 0.255f);
    int score;
    int itemCost = 5;
    void Start () {
     
        level = FindObjectOfType<LevelController>();
        score = level.score;

        
        gesturecolor = FindObjectOfType<GestureRecognizer.GestureBehaviour>();
        if(PlayerPrefs.HasKey("RedLine"))
        {
            buttox[0].image.sprite = unlocks[0];
            Locks[0].enabled = false;
        }
        if (PlayerPrefs.HasKey("GreenLine"))
        {
            buttox[1].image.sprite = unlocks[1];
            Locks[1].enabled = false;
        }
        if (PlayerPrefs.HasKey("YellowLine"))
        {
            buttox[2].image.sprite = unlocks[2];
            Locks[2].enabled = false;
        }
        if (PlayerPrefs.HasKey("ChristmasLine"))
        {
            buttox[3].image.sprite = unlocks[3];
            Locks[3].enabled = false;
        }
        if (PlayerPrefs.HasKey("YingTangLine"))
        {
            buttox[4].image.sprite = unlocks[4];
            Locks[4].enabled = false;
        }
        if (PlayerPrefs.HasKey("MagentaLine"))
        {
            buttox[5].image.sprite = unlocks[5];
            Locks[5].enabled = false;
        }
        if (PlayerPrefs.HasKey("WhiteBlueLine"))
        {
            buttox[6].image.sprite = unlocks[6];
            Locks[6].enabled = false;
        }
        if (PlayerPrefs.HasKey("MagentaBlueLine"))
        {
            buttox[7].image.sprite = unlocks[7];
            Locks[7].enabled = false;
        }
        if (PlayerPrefs.HasKey("FireFlameLine"))
        {
            buttox[8].image.sprite = unlocks[8];
            Locks[8].enabled = false;
        }
        if (PlayerPrefs.HasKey("BlackCyanLine"))
        {
            buttox[9].image.sprite = unlocks[9];
            Locks[9].enabled = false;
        }
        if (PlayerPrefs.HasKey("YellowCyanLine"))
        {
            buttox[10].image.sprite = unlocks[10];
            Locks[10].enabled = false;
        }
        if (PlayerPrefs.HasKey("FinalLine"))
        {
            buttox[11].image.sprite = unlocks[11];
            Locks[11].enabled = false;
        }
    }
	// Update is called once per frame
	void Update () {
	}
    public void RedLine()
    {
        if (score >= 50)
        {
            Debug.Log("IT WORKS");
            PlayerPrefs.SetInt("RedLine",0);
        }
        if (PlayerPrefs.HasKey("RedLine"))
        {
            Debug.Log("BOUGHT REDLINE");
            buttox[0].image.sprite = unlocks[0];
            Locks[0].enabled = false;
            gesturecolor.startColor = Color.red;
            gesturecolor.endColor = Color.white;
        }
    }
    public void GreenLine()
    {
        if (score >= 100)
        {

            PlayerPrefs.SetInt("GreenLine", 0);
        }
        if (PlayerPrefs.HasKey("GreenLine"))
        {
            buttox[1].image.sprite = unlocks[1];
            Locks[1].enabled = false;
            gesturecolor.startColor = Color.green;
            gesturecolor.endColor = Color.white;
        }
    }
    public void YellowLine()
    {
        if (score >= 150)
        {
            PlayerPrefs.SetInt("YellowLine", 0);
        }
        if (PlayerPrefs.HasKey("YellowLine"))
        {
            buttox[2].image.sprite = unlocks[2];
            Locks[2].enabled = false;
            gesturecolor.startColor = Color.yellow;
            gesturecolor.endColor = Color.white;
        }
    }

    public void ChristmasLine()
    {
        if (score >= 200)
        {
            PlayerPrefs.SetInt("ChristmasLine", 0);
        }
        if (PlayerPrefs.HasKey("ChristmasLine"))
        {
            buttox[3].image.sprite = unlocks[3];
            Locks[3].enabled = false;
            gesturecolor.startColor = Color.green;
            gesturecolor.endColor = Color.red;
        }
    }

    public void YingYangLine()
    {
        if (score >= 250)
        {   
            PlayerPrefs.SetInt("YingTangLine", 0);
        }
        if (PlayerPrefs.HasKey("YingTangLine"))
        {
            buttox[4].image.sprite = unlocks[4];
            Locks[4].enabled = false;
            gesturecolor.startColor = Color.black;
            gesturecolor.endColor = Color.white;
        }
    }

    public void MagentaLine()
    {
        if (score >= 300)
        {
          
            PlayerPrefs.SetInt("MagentaLine", 0);
        }
        if (PlayerPrefs.HasKey("MagentaLine"))
        {
            buttox[5].image.sprite = unlocks[5];
            Locks[5].enabled = false;
            gesturecolor.startColor = Color.magenta;
            gesturecolor.endColor = Color.white;
        }
    }
    public void WhiteBlueLine()
    {
        if (score >= 350)
        {
            PlayerPrefs.SetInt("WhiteBlueLine", 0);
        }
        if (PlayerPrefs.HasKey("WhiteBlueLine"))
        {
            buttox[6].image.sprite = unlocks[6];
            Locks[6].enabled = false;
            gesturecolor.startColor = Color.white;
            gesturecolor.endColor = Color.blue;
        }
    }
    public void MagentaBlueLine()
    {
        if (score >= 400)
        {
            
            PlayerPrefs.SetInt("MagentaBlueLine", 0);
        }
        if (PlayerPrefs.HasKey("MagentaBlueLine"))
        {
            buttox[7].image.sprite = unlocks[7];
            Locks[7].enabled = false;
            gesturecolor.startColor = Color.magenta;
            gesturecolor.endColor = Color.blue;
        }
    }
    public void FireFlameLine()
    {
        if (score >= 500)
        {
            
            PlayerPrefs.SetInt("FireFlameLine", 0);
        }
        if (PlayerPrefs.HasKey("FireFlameLine"))
        {
            buttox[8].image.sprite = unlocks[8];
            Locks[8].enabled = false;
            gesturecolor.startColor = Color.red;
            gesturecolor.endColor = Color.yellow;
        }
    }

    public void BlackCyanLine()
    {
        if (score >= 600)
        {
            
            PlayerPrefs.SetInt("BlackCyanLine", 0);
        }
        if (PlayerPrefs.HasKey("BlackCyanLine"))
        {
            buttox[9].image.sprite = unlocks[9];
            Locks[9].enabled = false;
            gesturecolor.startColor = Color.gray;
            gesturecolor.endColor = Color.cyan;
        }
    }
    public void YellowCyanLine()
    {
        if (score >= 700)
        {
            
            PlayerPrefs.SetInt("YellowCyanLine", 0);
        }
        if (PlayerPrefs.HasKey("YellowCyanLine"))
        {
            buttox[10].image.sprite = unlocks[10];
            Locks[10].enabled = false;
            gesturecolor.startColor = Color.yellow;
            gesturecolor.endColor = Color.cyan;
        }
    }
    public void FinalLine()
    {
        if (score > 1000)
        {
     
            PlayerPrefs.SetInt("FinalLine", 0);
        }
        if (PlayerPrefs.HasKey("FinalLine"))
        {
            buttox[11].image.sprite = unlocks[11];
            Locks[11].enabled = false;
            gesturecolor.startColor = Color.blue;
            gesturecolor.endColor = Color.cyan;
        }
    }
    public void Exit()
    {
        gameObject.SetActive(false);
        scoreBoard.SetActive(true);

    }
    public void Exit2()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
