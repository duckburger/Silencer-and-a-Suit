using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerprintDoor : MonoBehaviour {

    [SerializeField] SpriteRenderer mySpriteRenderer;

    bool isOpen = false;

    public void ToggleDoor()
    {
        if (isOpen)
        {
            LeanTween.cancel(this.gameObject);
            Close();
        }
        else
        {
            LeanTween.cancel(this.gameObject);
            Open();
        }
    }

    void Open()
    {
        LeanTween.moveLocalY(this.gameObject, this.transform.localPosition.y + mySpriteRenderer.sprite.rect.height / 32, 0.3f);
        LeanTween.alpha(this.gameObject, 0, 0.3f);
        isOpen = true;
    }

    void Close()
    {
        LeanTween.moveLocalY(this.gameObject, this.transform.localPosition.y - mySpriteRenderer.sprite.rect.height / 32, 0.3f);
        LeanTween.alpha(this.gameObject, 1, 0.3f);
        isOpen = false;
    }

}
