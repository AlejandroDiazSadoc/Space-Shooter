using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _yPosition = 7.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _canFire = -1f;

    private Player _player = null;

    private Animator _enemyAnimator = null;

    private AudioSource _audioSource;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();


        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _enemyAnimator = gameObject.GetComponent<Animator>();

        if (_enemyAnimator == null)
        {
            Debug.LogError("Animator is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Time.time > _canFire)
        {
            _canFire = Time.time + Random.Range(3f, 7f);
            GameObject enemylaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemylaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].SetEnemyLaser();
            }
            
        }

    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-9.55f, 9.55f);
            transform.position = new Vector3(randomX, _yPosition, transform.position.z);
        }
    }


    /**
     * Trigger Enter just for 3D
     * 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }else if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
    }
    **/

    /** Trigger Enter 2d **/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            //gameObject.GetComponent<Rigidbody2D>().Sleep();
            _audioSource.Play();
            Destroy(this.gameObject,2.3f);
           
            if (_player != null)
            {
                _player.addScore(10);

            }

        }
        else if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            //gameObject.GetComponent<Rigidbody2D>().Sleep();
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.3f);
            
        }
    }



}
