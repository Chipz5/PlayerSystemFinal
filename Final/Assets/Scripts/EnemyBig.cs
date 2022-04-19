using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBig : MonoBehaviour
{
    public int lives = 10;

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
        if (other.tag == "Punch" || other.tag == "Kick" || other.tag == "Combo")
        {
            new WaitForSeconds(1f);
            if (lives > 1)
                lives--;
            else
            {
                Destroy(this.gameObject);
            }

        }

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Punch" || collision.collider.tag == "Kick" || collision.collider.tag == "Combo")
        {
            new WaitForSeconds(1f);
            if (lives > 1)
                lives--;
            else
            {
                Destroy(this.gameObject);
            }

        }

        

    }
}
