using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUI : MonoBehaviour
{
    [SerializeField] Text text;
    // Start is called before the first frame update
    public void DisplayText()
    {
        text.gameObject.SetActive(true);
    }

    public void HideText()
    {
        text.gameObject.SetActive(false);
    }
}
