using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SimpleOpenCloser : MonoBehaviour {

    [SerializeField] CanvasGroup myCG;

    private void Start()
    {
        if (myCG == null)
        {
            myCG = GetComponent<CanvasGroup>();
        }
    }

    public void Open()
    {
        LeanTween.alphaCanvas(myCG, 1, 0.3f).setOnComplete(() => myCG.interactable = true);
    }

    public void Close()
    {
        LeanTween.alphaCanvas(myCG, 0, 0.3f).setOnComplete(() => myCG.interactable = false);

    }

}
