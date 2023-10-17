using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SenderScript : MonoBehaviour
{
    public TMP_InputField emailInput; // Input Field untuk alamat email
    public ScreenshotDisplay screenshotDisplay; // Referensi ke objek ScreenshotDisplay

    private string screenshotFolder;

    private void Awake()
    {
        screenshotFolder = $"{Application.persistentDataPath}";
    }

    public void SendCapture()
    {
        if (screenshotDisplay != null)
        {
            string screenshotFilename = ScreenshotDisplay.screenshotFilename;
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string username = "faizhmilanisty78@gmail.com";
            string password = "rjry rija tjyw bukz";
            string recipient = emailInput.text; // Mengambil alamat email dari Input Field
            string subject = "Screenshot from Unity";
            string body = "Check out this screenshot!";
            string screenshotPath = screenshotFilename;
            EmailSender.SendEmailWithScreenshot(smtpServer, smtpPort, username, password, recipient, subject, body, screenshotPath);
        }
        else
        {
            Debug.LogError("ScreenshotDisplay object not found!");
        }
    }
}
