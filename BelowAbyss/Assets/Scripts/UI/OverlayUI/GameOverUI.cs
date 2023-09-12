using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverObject;
    private float gameoverImageFadeInTime = 0.5f;

    public void OnGameOver()
    {
        GameOverSetup();
    }

    private void GameOverSetup()
    {
        gameOverObject.SetActive(true);
        StartCoroutine(GameOverImageFadeIn());
    }

    IEnumerator GameOverImageFadeIn()
    {
        for (float alpha = 0.0f; alpha < 0.95f; alpha += (0.95f * Time.deltaTime / gameoverImageFadeInTime))
        {
            gameOverObject.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    public void OnClickRestartButton()
    {
        SceneLoader.SceneLoad("TitleScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.isFirstGame)
        {
            GameManager.instance.gameOverUI = GameObject.Find("GameOverUIHolder").GetComponent<GameOverUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
