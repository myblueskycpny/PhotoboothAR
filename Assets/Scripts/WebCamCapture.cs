using UnityEngine;

public class WebCamCapture : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private Renderer rendererComponent;

    void Start()
    {
        rendererComponent = GetComponent<Renderer>();

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.LogError("No webcams found!");
            return;
        }

        string deviceName = devices[0].name; // Gunakan webcam pertama yang ditemukan
        webcamTexture = new WebCamTexture(deviceName);
        rendererComponent.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    void Update()
    {
        // Kode lain yang Anda inginkan di sini
    }
}
