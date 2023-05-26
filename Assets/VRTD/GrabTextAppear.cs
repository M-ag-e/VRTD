using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrabTextAppear : MonoBehaviour
{
    public GameObject textObject;
    public void ActivateText()
    {
        textObject.SetActive(true);
    }
    public void DeactivateText()
    {
        textObject.SetActive(false);
    }
}
