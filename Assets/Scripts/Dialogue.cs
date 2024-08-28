using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool startedConvo = false;

    private GameObject mainCamera;
    private AudioSource bossMusic;

    private void OnEnable()
    {
        DialogueDelegateEvent.OnBossInSight += BeginConversation;
        BeginConversation(false);
    }

    private void OnDisable()
    {
        DialogueDelegateEvent.OnBossInSight -= BeginConversation;
    }

    void Start()
    {
        textComponent.text = string.Empty;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        bossMusic = mainCamera.GetComponent<AudioSource>();
    }

    private void BeginConversation(bool inSight)
    {
        if (inSight && !startedConvo)
        {
            Time.timeScale = 0f;
            StartDialogue();
            startedConvo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                textComponent.text = lines[index];
                StopAllCoroutines();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueDelegateEvent.Instance.EndConversation();
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            bossMusic.Play();
        }
    }
}

