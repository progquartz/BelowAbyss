using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject TitleSceneComponents;
    [SerializeField]
    private GameObject StageSelectSceneComponents;

    private bool isTitleScene = true;

    public void EnableStageSelectScene()
    {
        isTitleScene = false;
        TitleSceneComponents.SetActive(false);
        StageSelectSceneComponents.SetActive(true);
    }

    public void EnableTitleScene()
    {
        isTitleScene = true;
        TitleSceneComponents.SetActive(true);
        StageSelectSceneComponents.SetActive(false);
    }

}
