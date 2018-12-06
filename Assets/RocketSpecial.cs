using UnityEngine;
using System.Collections;
using GestureRecognizer;

public class RocketSpecial : MonoBehaviour {

    // Use this for initialization

    EnemyRocket rocket;
    public GameObject choosen;
    public bool alreadydead;
    int i = 0;

	void Start () {
        rocket = GetComponent<EnemyRocket>();
        alreadydead = false;
    }
    void Update()
    {
        if (rocket.dead && i == 0)
        {
            rocket.levelController.AddinSpecialEnemy(choosen, this.transform);
            i++;
        }
    }
}
