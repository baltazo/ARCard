﻿//-----------------------------------------------------------------------
// <copyright file="AugmentedImageVisualizer.cs" company="Google">
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
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using GoogleARCoreInternal;
    using UnityEngine;

    /// <summary>
    /// Uses 4 frame corner objects to visualize an AugmentedImage.
    /// </summary>
    public class AnimalVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;

        public GameObject animalPrefab;

        private bool hasSpawned = false;

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        /// 

        

        public void Update()
        {
            if (!hasSpawned)
            {
               /* if (Image == null || Image.TrackingState != TrackingState.Tracking)
                {
                    animalPrefab.SetActive(false);
                    return;
                }*/
                SpawnAnimal();
                
            }
            

        }

        private void SpawnAnimal()
        {
            animalPrefab.SetActive(true);
            hasSpawned = true;
        }
    }
}