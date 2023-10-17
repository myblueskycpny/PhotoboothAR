using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceChecker : MonoBehaviour
{
    public GameObject pilihan1;
    public GameObject pilihan2;
    private Button confirmbutton;
    // Start is called before the first frame update
    void Start()
    {
        confirmbutton = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pilihan1.activeInHierarchy == false && pilihan2.activeInHierarchy == false)
        {
            Debug.Log("Pilih Terlebih Dahulu!");
            confirmbutton.interactable = false;
        }
        else
        {
            confirmbutton.interactable = true;
        }
    }

    public void confirm() { 
        
    }
}
