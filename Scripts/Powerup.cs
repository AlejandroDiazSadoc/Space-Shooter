using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

  

    [SerializeField] // 0 = Triple Shot, 1 = Speed, 2 = Shield
    private int _powerupID;

    [SerializeField]
    private AudioClip _clip;
    


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0: 
                        player.enableTripleShot();
                        break;
                    case 1: 
                        player.enableSpeed();
                        break;
                    case 2:
                        player.enableShield();
                        break;
                    default:
                        Debug.Log("Default Value of Powerup");
                        break;
                }
                
                    
            }
            Destroy(this.gameObject);

        }
    }

}
