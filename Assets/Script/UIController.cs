using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] public GameObject FadeSquare;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public IEnumerator Fade(bool fade = true, float fadeSpeed = 1f)
    {
        Color color = FadeSquare.GetComponent<Image>().color;
        float fadeState;
        if (fade)
        {
            color = new Color(color.r, color.g, color.b, 0);
            FadeSquare.GetComponent<Image>().color = color;
            while (FadeSquare.GetComponent<Image>().color.a < 1)
            {
                fadeState = color.a + (fadeSpeed * Time.deltaTime);

                color = new Color(color.r, color.g, color.b, fadeState);
                FadeSquare.GetComponent<Image>().color = color;
                yield return null;
            }
        }
        else
        {
            color = new Color(color.r, color.g, color.b, 1);
            FadeSquare.GetComponent<Image>().color = color;
            while (FadeSquare.GetComponent<Image>().color.a > 0)
            {
                fadeState = color.a - (fadeSpeed * Time.deltaTime);

                color = new Color(color.r, color.g, color.b, fadeState);
                FadeSquare.GetComponent<Image>().color = color;
                yield return null;
            }
        }
    }
}
