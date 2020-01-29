using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClick : MonoBehaviour
{
    public Text text;
    private void Awake()
    {
        text.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventType.ShowText, "你好", "哈哈");
        });
    }
}
