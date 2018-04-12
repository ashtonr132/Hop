using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    private bool DirRight = true;
    [SerializeField]
    GameObject PlayerOne;
    [SerializeField]
    GameObject PlayerTwo;
    [SerializeField]
    Material mat;
    [SerializeField]
    Material matt;
    Vector2 spawnpoint;
    [SerializeField]
    GameObject Score;
    internal bool isactivated = false;
    [SerializeField]
    GameObject Gameboard;

    // Use this for initialization
    void Start()
    {
        Physics2D.IgnoreCollision(PlayerOne.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(PlayerTwo.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
        spawnpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Gameboard.transform.position) > Gameboard.GetComponent<Renderer>().bounds.extents.x * 2.125f)
        {
            isactivated = false;
            PointCreator.SpawnEnemy();
        }
        if (isactivated == true)
        {
            var temp = GetComponent<Rigidbody2D>().velocity.y;
            temp = Mathf.Clamp(temp, -Mathf.Infinity, 5);
            if (DirRight && transform.position.x > 4.5f)
            {
                DirRight = false;
            }
            else if (!DirRight && transform.position.x < -4.5f)
            {
                DirRight = true;
            }
            if (DirRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, temp);
                transform.Rotate(Vector3.forward * -1f);

            }
            else if (!DirRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, temp);
                transform.Rotate(Vector3.forward * 1f);
            }
        }
        else
        {
            transform.position = spawnpoint;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject == PlayerOne || collision.gameObject == PlayerTwo) && isactivated)
        {
            if (collision.gameObject.transform.position.y > transform.position.y && collision.gameObject.transform.position.x < transform.position.x + GetComponent<Renderer>().bounds.extents.x && collision.gameObject.transform.position.x > transform.position.x - GetComponent<Renderer>().bounds.extents.x)
            {
                isactivated = false;
                GetComponent<Renderer>().material = mat;
                transform.rotation = Quaternion.identity;
                PointGet.score++;
                Score.GetComponent<Text>().text = "Points : " + PointGet.score.ToString();
            }
            else
            {
                PlayerBehavoiur.Life--;
                isactivated = false;
                GetComponent<Renderer>().material = mat;
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
