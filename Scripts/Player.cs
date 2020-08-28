using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 6.5f;

    [SerializeField]
    private float _speedMultiplier = 2f;

    [SerializeField]
    private GameObject _laserPrefab = null;

    [SerializeField]
    private float _cooldownFire = 0.2f;

    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private int _lives= 3;



    private SpawnManager _spawnManager = null;

    
    private bool _isTripleShootActive = false;

    [SerializeField]
    private GameObject _tripleShoot = null;

    private bool _isSpeedActive = false;

    private bool _isShieldActive = false;


    [SerializeField]
    private GameObject _playerShield = null;

    private UIManager _canvas;

    private int _score ;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position(0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _canvas = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }else if(_canvas == null)
        {
            Debug.LogError("The Canvas is NULL.");
        }
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
        _playerShield.SetActive(false);

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        /** transform.Translate(Vector3.right * 5 * Time.deltaTime);  5 meters per second in real life( Time.deltatime ). **/
        /**
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
          
        is equal to

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        **/
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        // player bounds
        /**
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y <= -3.96f)
        {
            transform.position = new Vector3(transform.position.x, -3.96f, transform.position.z);
        }
        **/
        // Equals to y player bounds.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.96f, 0f),transform.position.z);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(10.3f, transform.position.y, transform.position.z);
        }
    }

    void Shooting()
    {
        _canFire = Time.time + _cooldownFire;

        if (_isTripleShootActive == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 1.25f, 0f), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShoot, transform.position, Quaternion.identity);
        }

        _audioSource.Play();


    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _playerShield.SetActive(false);
            _isShieldActive = false;
            return;
        }
        _lives--;
        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }else if( _lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        _canvas.UpdateLives(_lives);
        
        if(_lives < 1)
        { 
           _spawnManager.OnPlayerDeath();
           
            Destroy(this.gameObject);
        }
    }

    public void enableTripleShot()
    {
        _isTripleShootActive = true;

        StartCoroutine(shutdownTripleShot());
    }

    IEnumerator shutdownTripleShot()
    {
            yield return new WaitForSeconds(5f);
            _isTripleShootActive = false;
    }

    public void enableSpeed()
    {
        if (_isSpeedActive == false)
        {
            _isSpeedActive = true;

            _speed *= _speedMultiplier;
            StartCoroutine(shutdownSpeed());
        }
    }

    IEnumerator shutdownSpeed()
    {
        yield return new WaitForSeconds(3.5f);
        _speed /= _speedMultiplier;
        _isSpeedActive = false;

    }

    public void enableShield()
    {
        _isShieldActive = true;
        _playerShield.SetActive(true);
    }

    public void addScore(int points)
    {
        _score += points;
        _canvas.UpdateScoreText(_score);
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shooting();
        }

    }
}
