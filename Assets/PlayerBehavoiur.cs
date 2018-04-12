using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavoiur : MonoBehaviour
{
    private float JumpHeight = 35;
    private bool left = true, MoveCooolDown = false;
    [SerializeField]
    internal Transform Gameboard;
    [SerializeField]
    GameObject PlayerOne;
    [SerializeField]
    GameObject PlayerTwo;
    internal static int Life = 3;
    [SerializeField]
    GameObject life;


    // Update is called once per frame
    void Update()
    {
        var playeroneveloy = PlayerOne.GetComponent<Rigidbody2D>().velocity.y;
        var playertwoveloy = PlayerTwo.GetComponent<Rigidbody2D>().velocity.y;
        life.GetComponent<Text>().text = "Life : " + Life;
        if (Input.GetKey(KeyCode.UpArrow) && !MoveCooolDown && PlayerOne.transform.position.y <= PlayerTwo.transform.position.y)
        {

            PlayerOne.GetComponent<Rigidbody2D>().velocity += Vector2.up * JumpHeight;
            StartCoroutine(PhysicsIgnore(PlayerOne.GetComponent<Collider2D>(), GetBar(PlayerOne, true).GetComponent<Collider2D>(), true));
            MoveCooolDown = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !MoveCooolDown && PlayerOne.transform.position.y > PlayerTwo.transform.position.y)
        {

            PlayerTwo.GetComponent<Rigidbody2D>().velocity += Vector2.up * JumpHeight;
            StartCoroutine(PhysicsIgnore(PlayerTwo.GetComponent<Collider2D>(), GetBar(PlayerTwo, true).GetComponent<Collider2D>(), true));
            MoveCooolDown = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !MoveCooolDown && PlayerOne.transform.position.y >= PlayerTwo.transform.position.y)
        {
            PlayerOne.GetComponent<Rigidbody2D>().velocity -= Vector2.up * JumpHeight/4;
            StartCoroutine(PhysicsIgnore(PlayerOne.GetComponent<Collider2D>(), GetBar(PlayerOne, false).GetComponent<Collider2D>(), true));
            MoveCooolDown = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !MoveCooolDown && PlayerOne.transform.position.y < PlayerTwo.transform.position.y)
        {
            PlayerTwo.GetComponent<Rigidbody2D>().velocity -= Vector2.up * JumpHeight/4;
            StartCoroutine(PhysicsIgnore(PlayerTwo.GetComponent<Collider2D>(), GetBar(PlayerTwo, false).GetComponent<Collider2D>(), true));
            MoveCooolDown = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerOne.GetComponent<Rigidbody2D>().velocity = new Vector2((Vector2.right * JumpHeight * 10 * Time.deltaTime).x, playeroneveloy);
            PlayerTwo.GetComponent<Rigidbody2D>().velocity = new Vector2((Vector2.right * JumpHeight * 10 * Time.deltaTime).x, playertwoveloy);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerOne.GetComponent<Rigidbody2D>().velocity = new Vector2((Vector2.left * JumpHeight * 10 * Time.deltaTime).x, playeroneveloy);
            PlayerTwo.GetComponent<Rigidbody2D>().velocity = new Vector2((Vector2.left * JumpHeight * 10 * Time.deltaTime).x, playertwoveloy);
        }
        else if (PlayerOne.GetComponent<Rigidbody2D>().velocity.x <= 1 && PlayerOne.GetComponent<Rigidbody2D>().velocity.x >= -1)
        {
            PlayerOne.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playeroneveloy);
            PlayerTwo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playertwoveloy);
        }
        if (Mathf.Abs(Vector2.Distance(new Vector2(PlayerTwo.transform.position.x, 0), new Vector2(PlayerOne.transform.position.x, 0))) < 0.75f)
        {
            PlayerOne.GetComponent<Rigidbody2D>().velocity -= (Vector2)PlayerOne.transform.right;
            PlayerTwo.GetComponent<Rigidbody2D>().velocity += (Vector2)PlayerOne.transform.right;
        }
        else if (Mathf.Abs(Vector2.Distance(new Vector2(PlayerTwo.transform.position.x, 0), new Vector2(PlayerOne.transform.position.x, 0))) > 0.75f)
        {
            PlayerOne.GetComponent<Rigidbody2D>().velocity += (Vector2)PlayerOne.transform.right;
            PlayerTwo.GetComponent<Rigidbody2D>().velocity -= (Vector2)PlayerOne.transform.right;
        }
    }

    private GameObject GetBar(GameObject player, bool up)
    {
        GameObject bAR = Gameboard.GetChild(5).gameObject;
        float dist = Mathf.Infinity;
        var dir = Vector3.zero;
        if (up)
        {
            dir = player.transform.up;
        }
        else
        {
            dir = -player.transform.up;
        }
        foreach (RaycastHit2D item in Physics2D.RaycastAll(player.transform.position, dir*200))
        {
            if (item.transform.name.Contains("Bar"))
            {
                if (left) // left player
                {
                    if (item.transform.position.y > player.transform.position.y && up) //direction to scan is upwards, so bars are above the player
                    {
                        if (Mathf.Abs(Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y))) < dist)
                        {
                            dist = Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y));
                            bAR = item.transform.gameObject;
                        }
                    }
                    else if (item.transform.position.y < player.transform.position.y && !up)
                    {
                        if (Mathf.Abs(Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y))) < dist)
                        {
                            dist = Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y));
                            bAR = item.transform.gameObject;
                        }
                    }
                }
                else //right player
                {
                    if (item.transform.position.y > player.transform.position.y && up)
                    {
                        if (Mathf.Abs(Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y))) < dist)
                        {
                            dist = Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y));
                            bAR = item.transform.gameObject;
                        }
                    }
                    else if (item.transform.position.y < player.transform.position.y && !up)
                    {
                        if (Mathf.Abs(Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y))) < dist)
                        {
                            dist = Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, item.transform.position.y));
                            bAR = item.transform.gameObject;
                        }
                    }
                }
            }
        }
        return bAR;
    }
    private IEnumerator PhysicsIgnore(Collider2D col, Collider2D colt, bool ignore)
    {
        if (!col.gameObject.transform.name.Contains("Top") && !colt.gameObject.transform.name.Contains("Top") && !col.gameObject.transform.name.Contains("Bot") && !colt.gameObject.transform.name.Contains("Bot"))
        {
            Physics2D.IgnoreCollision(col, colt, ignore);
            yield return new WaitForSeconds(0.12f);
            Physics2D.IgnoreCollision(col, colt, !ignore);
            yield return new WaitForSeconds(0.0575f);
            MoveCooolDown = false;
        }
        else if (col.gameObject.transform.name.Contains("Top") || colt.gameObject.transform.name.Contains("Top") || col.gameObject.transform.name.Contains("Bot") || colt.gameObject.transform.name.Contains("Bot"))
        {
            yield return new WaitForSeconds(0.1775f);
            MoveCooolDown = false;
        }
    }
}