using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.AddListener<string, string>(EventType.ShowText, Show);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<string, string>(EventType.ShowText, Show);
    }
    public void Show(string str, string str1)
    {
        gameObject.SetActive(true);
        GetComponent<Text>().text = str + str1;
    }
}
