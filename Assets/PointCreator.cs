using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCreator : MonoBehaviour
{
    GameObject[] Point;
    internal GameObject CurrentPoint;
    [SerializeField]
    Material mat;
    [SerializeField]
    Material matt;
    internal static int enemycount = 0;
    static GameObject[] enemies = new GameObject[10];
    static int temp = 0; 
    float waittime = 12;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            enemies[i] = GameObject.FindGameObjectsWithTag("Enemy")[i];
        }
        Point = GameObject.FindGameObjectsWithTag("Point");
        SwitchPoints();
        StartCoroutine(EnemySpawnCounter());
    }
    internal void SwitchPoints()
    {
        if (CurrentPoint != null)
        {
            CurrentPoint.GetComponent<Renderer>().material = mat;
        }
        CurrentPoint = Point[Random.Range(0, Point.Length - 1)];
        CurrentPoint.GetComponent<Renderer>().material = matt;
    }
    internal IEnumerator EnemySpawnCounter()
    {
        yield return new WaitForSeconds(waittime);
        waittime = waittime / 12 * 11;
        print(waittime);
        SpawnEnemy();
        StartCoroutine(EnemySpawnCounter());
    }
    internal static void SpawnEnemy()
    {
        temp = Random.Range(0, enemies.Length);
        if (enemies[temp].GetComponent<Enemies>().isactivated == false)
        {
            enemies[temp].GetComponent<Renderer>().material = GameObject.Find("GameBoard").GetComponent<PointCreator>().matt;
            enemies[temp].GetComponent<Enemies>().isactivated = true;
        }
        else
        {
            int j = 0;
            foreach (var item in enemies)
            {
                if (item.GetComponent<Enemies>().isactivated)
                {
                    j++;
                }
            }
            if (j <= 9)
            {
                SpawnEnemy();
            }
        }
    }
}
