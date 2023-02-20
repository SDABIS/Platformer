using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUI : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image flag;

    [SerializeField] private string originalText;
    [SerializeField] private string failureText;
    [SerializeField] private string successText;
    [SerializeField] private int errorDuration;

    void Start()
    {
        text.text = originalText;
    }

    // Start is called before the first frame update
    public void DisplayText()
    {
        text.gameObject.SetActive(true);
    }

    public void HideText()
    {
        text.gameObject.SetActive(false);
        //ResetText();
    }

    public void ShowBuyFailure()
    {
        text.text = failureText;
        Invoke("ResetText", errorDuration);
    }

    public void ShowBuySuccess()
    {
        text.text = successText;
        flag.gameObject.SetActive(true);
    }

    void ResetText()
    {
        text.text = originalText;
    }
}
