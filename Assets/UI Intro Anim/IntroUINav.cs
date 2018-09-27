using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroUINav : MonoBehaviour {

    //public GameObject skipButton;
    public GameObject loadingIcon;
    private Animator animator;
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private GameObject dontHaveCardPanel;

    // Use this for initialization
    /*void Start () {
        if (PlayerPrefs.HasKey("PLAYED"))
        {
            //skipButton.SetActive(true);
        }
        animator = GetComponent<Animator>();
	}*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            /*if (quitPanel.activeSelf)
            {
                QuitGame();
                return;
            }
            else if (dontHaveCardPanel.activeSelf)
            {
                DontHaveCard();
            }*/
            if (dontHaveCardPanel.activeSelf)
            {
                DontHaveCard();
            } 
            else if (quitPanel.activeSelf == false)
            {
                quitPanel.SetActive(true);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    /*public void PanelOneToTwo()
    {
        animator.SetTrigger("Panel02");
    }

    public void PanelTwoToThree()
    {
        animator.SetTrigger("Panel03");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }*/

    public void StartGame()
    {
        loadingIcon.SetActive(true);
        PlayerPrefs.SetInt("PLAYED", 1);
        StartCoroutine(LoadNextSceneAsync());
    }

    /*public void QuitToOne()
    {
        animator.SetTrigger("Panel01");
    }*/

    public void DontHaveCard()
    {
        dontHaveCardPanel.SetActive(!dontHaveCardPanel.activeSelf);
    }

    public void DontQuit()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
    }

    public void QuitGame()
    {
        if (quitPanel.activeSelf)
        {
            Application.Quit();
        }
        else
        {
            quitPanel.SetActive(true);
        }
    }

    public void GetACard()
    {
        Application.OpenURL("https://www.arfunstuff.com");
    }

    IEnumerator LoadNextSceneAsync()
    {

        yield return new WaitForSeconds(1);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
