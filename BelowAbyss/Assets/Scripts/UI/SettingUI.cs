using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private Image musicButtonOnOff;
    [SerializeField]
    private Image soundButtonOnOff;

    [SerializeField]
    private Sprite[] onOffButtonSprites = new Sprite[2];

    bool isMusicButtonOn = true;
    bool isSoundButtonOn = true;

    public void MusicButtonOnOff()
    {
        isMusicButtonOn = !isMusicButtonOn;
        if(isMusicButtonOn)
        {
            // 켜기
            musicButtonOnOff.sprite = onOffButtonSprites[1];
            BGMSource.instance.SetVolume(0.17f);
        }
        else
        {
            // 끄기
            musicButtonOnOff.sprite = onOffButtonSprites[0];
            BGMSource.instance.SetVolume(0.0f);
        }
    }

    public void SoundButtonOnOff()
    {
        isSoundButtonOn = !isSoundButtonOn;
        if(isSoundButtonOn)
        {
            // 켜기
            soundButtonOnOff.sprite = onOffButtonSprites[1];
            AudioListener.pause = false;
        }
        else
        {
            // 끄기
            soundButtonOnOff.sprite = onOffButtonSprites[0];
            AudioListener.pause = true;
            
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void AbandonButton()
    {
        MapManager.Instance.OnGameOver();
        GameManager.instance.isFirstGame = false;
        SceneLoader.SceneLoad("TitleScene");
    }


}
