using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitToScan : MonoBehaviour {

    public GameObject fitToScanVertical;
    public GameObject fitToScanHorizontal;

    private bool isPortrait = true;
    public bool fitToScanEnabled;

	// Update is called once per frame
	void Update () {
        if (fitToScanEnabled)
        {
            if (Screen.orientation == ScreenOrientation.Landscape && isPortrait)
            {
                fitToScanHorizontal.SetActive(true);
                fitToScanVertical.SetActive(false);
                isPortrait = false;
            }
            else if (Screen.orientation == ScreenOrientation.Portrait && !isPortrait)
            {
                fitToScanHorizontal.SetActive(false);
                fitToScanVertical.SetActive(true);
                isPortrait = true;
            }
        }
		
	}

    public void EnableFitToScan()
    {
        Debug.Log("Enabling Fit To Scan");
            if (Screen.orientation == ScreenOrientation.Landscape)
            {
                fitToScanHorizontal.SetActive(true);
                fitToScanVertical.SetActive(false);
                isPortrait = false;
            }
            else if (Screen.orientation == ScreenOrientation.Portrait)
            {
                fitToScanHorizontal.SetActive(false);
                fitToScanVertical.SetActive(true);
                isPortrait = true;
            }
        fitToScanEnabled = true;
    }

    public void DisableFitToScan()
    {
        fitToScanHorizontal.SetActive(false);
        fitToScanVertical.SetActive(false);
        fitToScanEnabled = false;

    }

}
