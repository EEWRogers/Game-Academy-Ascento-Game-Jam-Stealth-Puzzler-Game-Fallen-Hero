using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] DialogueLines dialogueLines;
    [SerializeField] TextMeshProUGUI nextLineIndicator;
    [SerializeField] float textSpeed;
    TextMeshProUGUI textComponent;
    PlayerController player;
    string[] textLines;
    int index;

    void Awake() 
    {
        nextLineIndicator.gameObject.SetActive(false);
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = string.Empty;
        textLines = dialogueLines.Dialogue;
        player = FindObjectOfType<PlayerController>();
    }

    void OnEnable() 
    {
        player.LockPlayerMovement();
    }

    void OnDisable() 
    {
        player.UnlockPlayerMovement();
    }

    void Start() 
    {
        StartDialogue();
    }

    void Update() 
    {
        if (textComponent.text == textLines[index])
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

        foreach (char character in textLines[index].ToCharArray())
        {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        nextLineIndicator.gameObject.SetActive(false);

        if (index < textLines.Length - 1)
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
        if (textComponent.text == textLines[index])
        {
            nextLineIndicator.gameObject.SetActive(true);
            NextLine();
        }

        else
        {
            StopAllCoroutines();
            nextLineIndicator.gameObject.SetActive(true);
            textComponent.text = textLines[index];
        }
    }


}
