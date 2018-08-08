using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using TMPro;

public class ScoreBoardController : MonoBehaviour {

    public Camera firstPersonCamera;
    public float xOffset = -1f;
    public float yOffset = 2f;
    public float zOffset = -2f;
    private bool scoreboardAppeared;
    private int totalScore = 0;
    private TextMeshPro textMeshPro;

	// Use this for initialization
	void Start () {
        firstPersonCamera = Camera.main;
        textMeshPro = GetComponentInChildren<TextMeshPro>();
		foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (scoreboardAppeared)
        {
            transform.LookAt(firstPersonCamera.transform);
        }
	}

    public void SetAnchor(Anchor anchor)
    {
        transform.position = new Vector3(anchor.transform.position.x + xOffset, anchor.transform.position.y + yOffset, anchor.transform.position.z +zOffset);
        transform.SetParent(anchor.transform);
        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
        scoreboardAppeared = true;
    }

    public void SetScore(int score)
    {
        totalScore += score;
        textMeshPro.text = "Score: " + totalScore;
    }
}
