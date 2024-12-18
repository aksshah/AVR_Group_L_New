using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ConnectorManager : MonoBehaviour
{

    static bool image1Tracked = false;
    static bool image2Tracked = false;
    static bool image3Tracked = false;


ARTrackedImageManager imageManager;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        imageManager = (ARTrackedImageManager)FindObjectOfType(typeof(ARTrackedImageManager));

        imageManager.trackedImagesChanged += ManageScenes;
    }

    private void ManageScenes(ARTrackedImagesChangedEventArgs detected)
    {
        foreach( var image in   detected.added){
                //image...
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
