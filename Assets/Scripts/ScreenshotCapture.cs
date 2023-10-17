using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ScreenshotCapture : MonoBehaviour
{
    private string screenshotFolder;
    public GameObject model;
    public RawImage rawimage;
    public GameObject model2;
    public RawImage rawimage2;
    private Animator anim;
    private Animator anim2;
    public float moveDuration = 3f; // Durasi pergerakan (dalam detik)

    private Vector2 initialPosition; // Posisi awal gambar
    private Vector2 targetPosition; // Posisi tujuan gambar
    private Vector2 initialPosition2; // Posisi awal gambar
    private Vector2 targetPosition2; // Posisi tujuan gambar
    private float elapsedTime = 0f;

    public static string screenshotresult;
    public ScreenshotDisplay screenshotDisplay;

    public TMP_Text countdownText;

    public GameObject rawres;
    public GameObject borderres;
    public GameObject emailbutton;

    private void Awake()
    {
        screenshotFolder = $"{Application.persistentDataPath}";
    }
    private void Start()
    {
        anim = model.GetComponent<Animator>();
        anim2 = model2.GetComponent<Animator>();

        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }

        initialPosition = rawimage.rectTransform.anchoredPosition;
        targetPosition = new Vector2(690, 402);
        initialPosition2 = rawimage2.rectTransform.anchoredPosition;
        targetPosition2 = new Vector2(100, 858);
    }

    // Fungsi untuk mengambil tangkapan layar
    public void CaptureScreenshot()
    {
        StartCoroutine(CaptureScreenshotWithDelay());
    }

    private IEnumerator CaptureScreenshotWithDelay()
    {
        while (elapsedTime < moveDuration)
        {
            // Menggerakkan gambar perlahan menuju target
            rawimage.rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            rawimage2.rectTransform.anchoredPosition = Vector2.Lerp(initialPosition2, targetPosition2, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Pastikan gambar berada di posisi target secara tepat
        rawimage.rectTransform.anchoredPosition = targetPosition;
        rawimage2.rectTransform.anchoredPosition = targetPosition2;

        if (rawimage.rectTransform.anchoredPosition == targetPosition)
        {
            model.transform.Rotate(Vector3.up, -90f);
            anim.SetBool("isPose", true);
            anim.SetBool("isWalk", false);
        }
        if (rawimage2.rectTransform.anchoredPosition == targetPosition2)
        {
            model2.transform.Rotate(Vector3.up, 90f);
            anim2.SetBool("isPose", true);
            anim2.SetBool("isWalk", false);
        }
        // Menunggu 3 detik
        float waitTime = 3f;
        while (waitTime > 0)
        {
            // Menghitung waktu jeda mundur
            countdownText.text = "" + Mathf.Ceil(waitTime);
            waitTime -= Time.deltaTime;
            yield return null;
        }

        // Reset teks countdown
        countdownText.text = "";
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        string screenshotFilename = Path.Combine(screenshotFolder, "Screenshot_" + timestamp + ".png");
        ScreenCapture.CaptureScreenshot(screenshotFilename);
        screenshotDisplay.SetScreenshotAvailable(screenshotFilename);
        screenshotresult = screenshotFilename;
        Debug.Log("Screenshot tersimpan di: " + screenshotFilename);

        yield return new WaitForSeconds(1);

        model.transform.Rotate(Vector3.up, -90f);
        anim.SetBool("isPose", false);
        anim.SetBool("isWalk", true);

        model2.transform.Rotate(Vector3.up, 90f);
        anim2.SetBool("isPose", false);
        anim2.SetBool("isWalk", true);

        // Mengembalikan rawimage ke posisi awal
        float resetDuration = 1.0f; // Durasi pergerakan kembali (dalam detik)
        float resetElapsedTime = 0f;
        while (resetElapsedTime < resetDuration)
        {
            rawimage.rectTransform.anchoredPosition = Vector2.Lerp(targetPosition, initialPosition, resetElapsedTime / resetDuration);
            rawimage2.rectTransform.anchoredPosition = Vector2.Lerp(targetPosition2, initialPosition2, resetElapsedTime / resetDuration);
            resetElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pastikan gambar berada di posisi awal secara tepat
        rawimage.rectTransform.anchoredPosition = initialPosition;
        rawimage2.rectTransform.anchoredPosition = initialPosition2;

        rawres.SetActive(true);
        borderres.SetActive(true);
        emailbutton.SetActive(true);
    }
}