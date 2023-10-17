using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImagesTrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] PrefabsToSpawn;

    private ARTrackedImageManager _arTrackedImageManager;

    private Dictionary<string, GameObject> _arObjects;

    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
        
        foreach(GameObject prefab in PrefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newARObject.name = prefab.name;
            newARObject.gameObject.SetActive(false);
            _arObjects.Add(newARObject.name, newARObject);
        } 
    }

    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if(trackedImage.trackingState is UnityEngine.XR.ARSubsystems.TrackingState.Limited or UnityEngine.XR.ARSubsystems.TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }

        if(PrefabsToSpawn != null)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
            _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        }
    }
    
}
