using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointGet : MonoBehaviour {
    [SerializeField]
    GameObject Score;
    [SerializeField]
    GameObject Gameboard;
    internal static int score = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Gameboard.GetComponent<PointCreator>().CurrentPoint)
        {
            Gameboard.GetComponent<PointCreator>().SwitchPoints();
            score++;
            Score.GetComponent<Text>().text = "Points : " + score.ToString();
        }
    }
}
