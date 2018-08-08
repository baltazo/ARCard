//-----------------------------------------------------------------------
// <copyright file="AugmentedImageExampleController.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.AugmentedImage
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Controller for AugmentedImage example.
    /// </summary>
    public class MainController : MonoBehaviour
    {
        /// <summary>
        /// A prefab for visualizing an AugmentedImage.
        /// </summary>
        public AugmentedImageVisualizer AugmentedImageVisualizerPrefab;
        public AnimalVisualizer[] animalVisualizerPrefabs;

        /// <summary>
        /// The overlay containing the fit to scan user guide.
        /// </summary>
        public GameObject FitToScanOverlay;
        public ScoreBoardController scoreBoardController;
        public GameObject pausePanel;
        public GameObject loadingAnim;

        public GameObject tempCamera;
        private GameObject arcoreDevice;

        public GameObject debugText; // Remove once the debug phase is done

        private FoodController foodController;
        private bool gamePaused = false;
        private Dictionary<int, AnimalVisualizer> m_Visualizers
            = new Dictionary<int, AnimalVisualizer>();
        private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();
        private bool hasSpawned = false;

        private void Awake()
        {
            hasSpawned = false;
            gamePaused = false;
            Time.timeScale = 1f;
        }

        private void Start()
        {
            QuitOnConnectionError();
            foodController = GetComponent<FoodController>();
            arcoreDevice = GameObject.FindGameObjectWithTag("Player");
            tempCamera.SetActive(false);
        }

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                PauseGame();
            }

            // Prevent the screen from dimming unless the game is paused
            if(gamePaused)
            {
                int gamePausedSleepTimeout = 15;
                Screen.sleepTimeout = gamePausedSleepTimeout;
                if (Input.GetKey(KeyCode.Escape))
                {
                    Application.Quit();
                }
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            Debug.Log("Session Status " + Session.Status);
            // Check that motion tracking is tracking.
            if (Session.Status != SessionStatus.Tracking || hasSpawned)
            {
                return;
            }

            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

            // Create visualizers and anchors for updated augmented images that are tracking and do not previously
            // have a visualizer. Remove visualizers for stopped images.
            foreach (var image in m_TempAugmentedImages)
            {
                AnimalVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);

                if(image.TrackingState == TrackingState.Paused)
                {
                    debugText.GetComponent<Text>().text = "Tracking Paused";
                    debugText.SetActive(true);
                }
                else if(image.TrackingState == TrackingState.Stopped)
                {
                    debugText.GetComponent<Text>().text = "Tracking Stopped";
                    debugText.SetActive(true);
                }
                else
                {
                    debugText.SetActive(false);
                }                                                       

                if (image.TrackingState == TrackingState.Tracking && visualizer == null)
                {
                    // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                    Anchor anchor = image.CreateAnchor(image.CenterPose);
                    visualizer = (AnimalVisualizer)Instantiate(animalVisualizerPrefabs[image.DatabaseIndex], anchor.transform.position, Quaternion.identity); //, anchor.transform
                    visualizer.Image = image;
                    m_Visualizers.Add(image.DatabaseIndex, visualizer);
                    hasSpawned = true;
                    scoreBoardController.SetAnchor(anchor);
                    foodController.Setup(GameObject.Find("Plane"), anchor, image.DatabaseIndex);
                }
                /* else if (reset) //image.TrackingState == TrackingState.Stopped && visualizer != null
                 {
                     m_Visualizers.Remove(image.DatabaseIndex);
                     GameObject.Destroy(visualizer.gameObject);
                 }*/



            }

            // Show the fit-to-scan overlay if there are no images that are Tracking.
            foreach (var visualizer in m_Visualizers.Values)
            {
                if (visualizer.Image.TrackingState == TrackingState.Tracking)
                {
                    FitToScanOverlay.SetActive(false);
                    return;
                }
            }

            FitToScanOverlay.SetActive(true);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                /*Debug.Log("Is Paused");
                m_Visualizers.Clear();
                GameObject.Destroy(GameObject.Find("Anchor"));*/
                PauseGame();
            }
        }

        void QuitOnConnectionError()
        {
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                StartCoroutine(CodelabUtils.ToastAndExit("Camera permission is needed to run this application", 5));
            }
            else if (Session.Status.IsError())
            {
                // This covers a variety of errors.  See reference for details
                // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
                StartCoroutine(CodelabUtils.ToastAndExit("ARCore encountered a problem connecting. Please restart the app.", 5));
            }
        }

        public void PauseGame()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;
        }

        public void ResumeGame()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
        }

        public void ResetGame()
        {
           foreach(Transform child in pausePanel.transform)
            {
                child.gameObject.SetActive(false);
            }
            pausePanel.GetComponent<VerticalLayoutGroup>().enabled = false;
            loadingAnim.SetActive(true);
            Time.timeScale = 1f;
            m_Visualizers.Clear();
            GameObject.Destroy(GameObject.Find("Anchor"));
            GameObject.Destroy(GameObject.FindGameObjectWithTag("Animal"));
            m_TempAugmentedImages.Clear();
            tempCamera.SetActive(true);
            Destroy(arcoreDevice);
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        IEnumerator ResetScene()
        {
            yield return new WaitForSeconds(1);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}

