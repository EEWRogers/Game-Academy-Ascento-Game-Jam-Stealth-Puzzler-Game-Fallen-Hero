using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    TextMeshProUGUI textComponent;
    [SerializeField] TextMeshProUGUI nextLineIndicator;
    [SerializeField] string[] dialogueLines;
    [SerializeField] float textSpeed;
    int index;

    void Awake() 
    {
        nextLineIndicator.gameObject.SetActive(false);
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = string.Empty;
    }

    void Start() 
    {
        StartDialogue();
    }

    void Update() 
    {
        if (textComponent.text == dialogueLines[index])
        {
            nextLineIndicator.gameObject.SetActive(true);
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(DisplayDialogue());
    }

    IEnumerator DisplayDialogue()
    {
        //Type each character 1 by 1

        foreach (char character in dialogueLines[index].ToCharArray())
        {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        nextLineIndicator.gameObject.SetActive(false);

        if (index < dialogueLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(DisplayDialogue());
        }

        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SkipLine()
    {
        if (textComponent.text == dialogueLines[index])
        {
            nextLineIndicator.gameObject.SetActive(true);
            NextLine();
        }

        else
        {
            StopAllCoroutines();
            nextLineIndicator.gameObject.SetActive(true);
            textComponent.text = dialogueLines[index];
        }
    }


}
