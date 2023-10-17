using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTrackingController : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    
    public GameObject[] objectsToShow;

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateObject(trackedImage);
        }
    }

    private void UpdateObject(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        GameObject objToShow = GetObjectToShow(imageName);

        if (objToShow)
        {
            if (!spawnedObjects.ContainsKey(imageName))
            {
                GameObject spawnedObj = Instantiate(objToShow, trackedImage.transform.position, trackedImage.transform.rotation);
                spawnedObjects.Add(imageName, spawnedObj);
            }
            else
            {
                spawnedObjects[imageName].transform.position = trackedImage.transform.position;
                spawnedObjects[imageName].transform.rotation = trackedImage.transform.rotation;
            }
        }
    }

    private GameObject GetObjectToShow(string imageName)
    {
        foreach (var obj in objectsToShow)
        {
            if (obj.name == imageName)
            {
                return obj;
            }
        }
        return null;
    }
}
