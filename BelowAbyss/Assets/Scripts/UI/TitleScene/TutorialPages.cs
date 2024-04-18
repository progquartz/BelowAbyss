using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPages : MonoBehaviour
{
    public bool isTutorialEnabled = false;
    public int tutorialIndex = 0;
    public int tutorialPageCount = 0;

    [SerializeField]
    private List<GameObject> tutorialPages;
    public Transform tutorialParent;

    [SerializeField]
    private GameObject leftButton;
    [SerializeField]
    private GameObject rightButton;
    [SerializeField]
    private GameObject finishButton;

    public void EnableTutorial()
    {
        tutorialIndex = 0;
        isTutorialEnabled = true;
    }

    public void DisableTutorial()
    {

        isTutorialEnabled = false;
    }

    public void PressLeftButton()
    {
        tutorialIndex--;
        if (tutorialIndex < 0)
        {
            tutorialIndex = 0;
        }
        
    }

    public void PressRightButton()
    {
        tutorialIndex++;
        if (tutorialIndex >= tutorialPageCount)
        {
            tutorialIndex--;
        }
    }

    private void Start()
    {
        tutorialPageCount = tutorialParent.childCount;
        for(int i = 0; i < tutorialPageCount; i++)
        {
            tutorialPages.Add(tutorialParent.GetChild(i).gameObject);
        }
    }
    void Update()
    {
        if(isTutorialEnabled)
        {
            if (tutorialIndex == 0)
            {
                leftButton.SetActive(false);
            }
            else
            {
                leftButton.SetActive(true);
            }
            if (tutorialIndex >= tutorialPageCount - 1)
            {
                rightButton.SetActive(false);
                finishButton.SetActive(true);
            }
            else
            {
                rightButton.SetActive(true);
                finishButton.SetActive(false);
            }
            for (int i = 0; i < tutorialPageCount; i++)
            {
                if (i == tutorialIndex)
                {
                    tutorialPages[i].SetActive(true);
                }
                else
                {
                    tutorialPages[i].SetActive(false);
                }
            }
        }
        else
        {
            leftButton.SetActive(false);
            rightButton.SetActive(false);
            finishButton.SetActive(false);
            for (int i = 0; i < tutorialPageCount; i++)
            {
                tutorialPages[i].SetActive(false);
            }
        }
        
    }
}
