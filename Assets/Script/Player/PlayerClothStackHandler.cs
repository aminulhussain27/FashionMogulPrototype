using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerClothStackHandler : MonoBehaviour
{
    [SerializeField] private Transform clothStack;
    [SerializeField] private GameObject clothPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int clothServed;
    private GameObject serveCloth = null;

    private readonly List<GameObject> currentClothStack = new();
    void Awake()
    {
        ClothStand.OnTimerCompleted += CreateCloth;
        UpdateScore();
    }

    private void CreateCloth(Color color)
    {
        GameObject cloth = Instantiate(clothPrefab, clothStack);
        cloth.transform.position = clothStack.position;
        cloth.GetComponent<Renderer>().material.color = color;
        cloth.transform.SetAsLastSibling();
        cloth.SetActive(true);
        currentClothStack.Add(cloth);
        ArrangeCollectedCloths();

    }

    private void ArrangeCollectedCloths()
    {
        foreach (var item in currentClothStack)
        {
            item.transform.position = clothStack.position + new Vector3(0, item.transform.GetSiblingIndex() * 1.1f, 0);
        }
    }

    //--Serve



    private bool GetClothForServe(Color color)
    {
        bool isClothAvailable = false;
        for (int i = currentClothStack.Count - 1; i >= 0; i--)
        {
            if (currentClothStack[i].GetComponent<Renderer>().material.color == color)
            {
                serveCloth = currentClothStack[i];
                isClothAvailable = true;
                break;
            }
        }
        return isClothAvailable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            if (GetClothForServe(other.gameObject.GetComponent<CustomerView>().requiredClothColor))
               if( other.gameObject.GetComponent<CustomerView>().TryServed())
                {
                    currentClothStack.Remove(serveCloth);
                    Destroy(serveCloth);
                    ArrangeCollectedCloths();

                    clothServed++;
                    UpdateScore();
                }
        }
    }

    private void UpdateScore()
    {
        scoreText.text = clothServed.ToString();
    }
}
