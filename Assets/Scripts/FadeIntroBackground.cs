using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIntroBackground : MonoBehaviour
{
    private SpriteRenderer background;

    public float fadeDuration = 2.5f;
    public float delayBeforeFade = 1.0f;

    private Color targetColor;
    private Color initialColor;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<SpriteRenderer>();

        if( background != null )
        {
            initialColor = background.color;
            targetColor = new Color(160f / 255f, 160f / 255f, 160f / 255f, initialColor.a);
            StartCoroutine(StartFade());
        }
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(delayBeforeFade);
        StartCoroutine(FadeBackground());
    }

    private IEnumerator FadeBackground()
    {
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float lerpValue = timer / fadeDuration;
            background.color = Color.Lerp(initialColor, targetColor, lerpValue);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Load the next scene (Level_1).
            SceneManager.LoadScene("Level_1");
        }
    }
}
