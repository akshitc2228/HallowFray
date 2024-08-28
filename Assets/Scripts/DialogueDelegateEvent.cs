using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDelegateEvent : MonoBehaviour
{
    public delegate void BossInSight(bool inSight);

    public static event BossInSight OnBossInSight;

    //for ending the conversation:
    public delegate void ConversationComplete();
    public static event ConversationComplete OnConversationComplete;

    private static DialogueDelegateEvent instance;

    public static DialogueDelegateEvent Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueDelegateEvent>();
            }
            return instance;
        }
    }

    public void SetBossInSight()
    {
        OnBossInSight?.Invoke(true);
    }

    public void EndConversation()
    {
        OnConversationComplete?.Invoke();
    }
}
