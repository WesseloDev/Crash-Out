using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenGui : MonoBehaviour
{
    public Text header;
    public Text paragraph;
    public GameObject panel;

    public void ShowScreen()
    {
        panel.SetActive(true);
        CursorManager.EnableCursor();
    }

    public void UpdateText()
    {
        header.text = "You lose!";
        paragraph.text = "So close... try again?";
    }
}
