using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int lives = 5;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Punch")
        {
            new WaitForSeconds(1f);
            if (lives > 1)
                lives--;
            else
            {
                gameManager.newAbility();
                Destroy(this.gameObject);
            }

        }

        if (other.tag == "Kick")
        {
            new WaitForSeconds(1f);
            if (lives > 2)
                lives -= 2;
            else
            {
                gameManager.newAbility();
                Destroy(this.gameObject);
            }

        }

        if (other.tag == "Combo")
        {
            new WaitForSeconds(1f);
            Destroy(this.gameObject, 1f);


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Punch")
        {
            new WaitForSeconds(1f);
            if (lives > 1)
                lives--;
            else
            {
                gameManager.newAbility();
                Destroy(this.gameObject);
            }
                
        }

        if (collision.collider.tag == "Kick")
        {
            new WaitForSeconds(1f);
            if (lives > 2)
                lives -= 2;
            else
            {
                gameManager.newAbility();
                Destroy(this.gameObject);
            }

        }

        if (collision.collider.tag == "Combo")
        {
            new WaitForSeconds(1f);
            Destroy(this.gameObject, 1f);
       
            

        }

    }
}
