using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour  {

    public Text scoreText;
    public float scoreAmount;
    public float pointIncreased;


    // Start is called before the first frame update
    void Start()
    {
        scoreAmount =  0f;
        pointIncreased = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + (int)scoreAmount;
        scoreAmount += pointIncreased * Time.deltaTime;
    }
}
