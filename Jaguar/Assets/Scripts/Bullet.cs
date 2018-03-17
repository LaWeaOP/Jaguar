using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float power = 5;
    bool cargando = true;
    float cos, sin, ang;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player_Controler.sharedInstance.canMove = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        if (cargando)
        {
            power += 0.05f;
            if (Player_Controler.sharedInstance.sr.flipX)
            {
                gameObject.transform.position = Player_Controler.sharedInstance.kunaiIz.transform.position;
            }
            else
            {
                gameObject.transform.position = Player_Controler.sharedInstance.kunaiDe.transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0) || power > 10 || (!Input.GetMouseButton(0) && cargando))
        {
            if (cargando)
            {
                ang = Player_Controler.sharedInstance.ang;
                cos = Mathf.Cos(ang * Mathf.PI / 180);
                sin = Mathf.Sin(ang * Mathf.PI / 180);
                Disparo(new Vector2(cos * power, sin * power));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Jaguar")
        {
            transform.SetParent(collision.transform);
            collision.gameObject.GetComponent<HpScript>().Damage(Mathf.RoundToInt(power));
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(rb);
        }
        else if (collision.gameObject.tag == "Player")
        {

        }
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(rb);
        }

        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void Disparo(Vector2 vel)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().velocity = vel;
        Debug.Log("Angulo: " + ang + "\tCos: " + cos + "\tSin: " + sin);
        cargando = false;
        Player_Controler.sharedInstance.canMove = true;
    }
}


