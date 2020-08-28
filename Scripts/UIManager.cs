using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _LivesImg;

    [SerializeField]
    private Text _GameOver;

    [SerializeField]
    private Text _ReplayGame;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _GameOver.gameObject.SetActive(false);
        _ReplayGame.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL.");
        }

        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText(int number)
    {
        _scoreText.text = "Score: " + number;
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }

    }

    void GameOverSequence()
    {
        StartCoroutine(FlickrGameOver());
        _ReplayGame.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator FlickrGameOver()
    {
        while (true)
        {
            _GameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _GameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }



}
