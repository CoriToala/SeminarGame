using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private Image[] tabImages;
    [SerializeField] private GameObject[] pages;
    int character;

    public void ActivateTab(int tabNo)
    {
        character = tabNo;
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }

        pages[character].SetActive(true);
        tabImages[character].color = Color.white;
    }

    void Start()
    {
        ActivateTab(0);
    }

    public int GetPage()
    {
        return character;
    }
}
