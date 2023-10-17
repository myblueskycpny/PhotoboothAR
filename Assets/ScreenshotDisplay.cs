using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotDisplay : MonoBehaviour
{
    public RawImage screenshotRawImage; // Referensi ke UI RawImage
    private string screenshotFolder; // Folder tempat tangkapan layar disimpan

    public static string screenshotFilename; // Nama file tangkapan layar yang akan diisi

    private bool screenshotAvailable = false; // Apakah hasil screenshot sudah tersedia

    private void Awake()
    {
        screenshotFolder = $"{Application.persistentDataPath}";
    }

    private void Start()
    {
        // Tunggu hingga hasil screenshot tersedia
        StartCoroutine(WaitForScreenshot());
    }

    private IEnumerator WaitForScreenshot()
    {
        // Tunggu hingga hasil screenshot tersedia
        while (!screenshotAvailable)
        {
            yield return null;
        }

        // Setelah hasil screenshot tersedia, baca dan tampilkan gambar
        string screenshotPath = screenshotFilename;
        if (File.Exists(screenshotPath))
        {
            byte[] fileData = File.ReadAllBytes(screenshotPath);
            Texture2D screenshotTexture = new Texture2D(2, 2);
            screenshotTexture.LoadImage(fileData);

            // Tampilkan gambar di RawImage
            screenshotRawImage.texture = screenshotTexture;
        }
        else
        {
            Debug.LogWarning("File tangkapan layar tidak ditemukan: " + screenshotPath);
        }
    }

    // Fungsi ini dipanggil dari luar (misalnya, dari skrip ScreenshotCapture) untuk memberitahu bahwa hasil screenshot telah tersedia
    public void SetScreenshotAvailable(string filename)
    {
        screenshotAvailable = true;
        screenshotFilename = filename;
    }
}
