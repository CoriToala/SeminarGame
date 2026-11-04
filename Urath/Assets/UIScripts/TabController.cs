using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private List<Image> tabImages = new List<Image>();
    [SerializeField] private Image tabImagePrefab;
    public List<GameObject> pages = new List<GameObject>();
    int character;
    
    public void updateTabs()
    {
        //print("Called Update Tabs");
        for(int i = 0; i < pages.Count; i++){
            if(pages.Count > tabImages.Count){
                //more pages then are tabs
                tabImages.RemoveAt(tabImages.Count - 1);
            }
            if(pages.Count < tabImages.Count){
                tabImages.Add(tabImagePrefab);
                //more tabs then pages
            }
        }
        ActivateTab(0);
    }

    public void ActivateTab(int tabNo)
    {
        //print("Started at 0");
        character = tabNo;
        for(int i = 0; i < pages.Count; i++)
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
