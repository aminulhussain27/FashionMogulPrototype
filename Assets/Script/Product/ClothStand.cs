using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClothStand : MonoBehaviour
{
    public static Action<Color> OnTimerCompleted;
        
    public Image slowDownFillImg;
    public float MaxTime = 3;
    public StandColor standColor;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player enter");
        }
    }



    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player exit");
        }
    }
}
public enum StandColor
{
    Green,
    Red
}
