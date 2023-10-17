using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    private AudioSource[] audioSources; // Array untuk menyimpan komponen-komponen AudioSource

    void Start()
    {
        // Mencari semua game objek dalam scene
        GameObject[] semuaGameObjek = GameObject.FindObjectsOfType<GameObject>();

        // Menginisialisasi array audioSources
        audioSources = new AudioSource[semuaGameObjek.Length];

        int index = 0; // Digunakan untuk melacak indeks dalam array audioSources

        foreach (GameObject objek in semuaGameObjek)
        {
            // Mencari komponen AudioSource dalam setiap game objek
            AudioSource audioSource = objek.GetComponent<AudioSource>();

            // Jika komponen AudioSource ditemukan
            if (audioSource != null)
            {
                // Menyimpan komponen AudioSource dalam array
                audioSources[index] = audioSource;
                index++;
            }
        }

        // Sekarang, audioSources berisi semua komponen AudioSource yang ditemukan dalam scene
        Debug.Log("Jumlah AudioSource yang ditemukan: " + index);
    }
}
