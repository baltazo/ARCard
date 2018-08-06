using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroUINav : MonoBehaviour {

    public GameObject skipButton;
    public GameObject loadingIcon;
    private Animator animator;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("PLAYED"))
        {
            skipButton.SetActive(true);
        }
        animator = GetComponent<Animator>();
	}

    public void PanelOneToTwo()
    {
        animator.SetBool("Panel2", true);
        animator.SetBool("Panel1", false);
    }

    public void PanelTwoToThree()
    {
        animator.SetBool("Panel3", true);
        animator.SetBool("Panel2", false);
    }

    public void FadeOut()
    {
        animator.SetBool("FadeOut", true);
        animator.SetBool("Panel3", false);
    }

    public void StartGame()
    {
        loadingIcon.SetActive(true);
        PlayerPrefs.SetInt("PLAYED", 1);
        StartCoroutine(LoadNextSceneAsync());

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
