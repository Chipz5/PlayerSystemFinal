using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour, IObserver
{
    public Text guiText;
    public void OnNotify(int id)
    {
        if (id == 2)
        {
            StartCoroutine(ShowMessage("New Achievement Unlocked: Kick It!", 2));
        }
        else if (id == 3)
        {
            StartCoroutine(ShowMessage("New Achievement Unlocked: Combo!", 2));
        }
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        guiText.text = message;
        guiText.enabled = true;
        yield return new WaitForSeconds(delay);
        guiText.enabled = false;
    }
}
