using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;

    [SerializeField]
    public float currentHealth;

    Animator playerAnimator;
    PlayerController playerScript;

    //for traps/enemies that damage player:
    [SerializeField]
    public float damage;
    
    [SerializeField]
    public float damageRate;

    [SerializeField]
    public float pushBackForce;

    float nextDamage;

    //dealing with hit animation:
    private float hitDuration = 1.5f;
    private bool isDead = false;

    private bool debugDeathOnce;

    //HUD variables:
    public UnityEngine.UI.Slider healthSlider;

    public TextUpdater scoreTextScript;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;

        damage = 15f;
        damageRate = 0.5f;
        pushBackForce = 35f;

        playerScript = GetComponent<PlayerController>();
        playerAnimator = this.GetComponent<Animator>();

        nextDamage = 0f;

        //HUD initialization:
        healthSlider.maxValue = maxHealth;

        Debug.Log(healthSlider.maxValue);
        Debug.Log(healthSlider.minValue);

        healthSlider.value = maxHealth;

        scoreTextScript = GameObject.FindGameObjectWithTag("scorecard").GetComponent<TextUpdater>();
        scoreText = scoreTextScript.scoreText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        if (damage <= 0) return;

        StartCoroutine(HitAnimationSequence()); //do this using a trigger for hit like with boss
        currentHealth -= damage;

        Debug.Log(currentHealth);

        //adjust health slider
        healthSlider.value = currentHealth;

        Debug.Log(healthSlider.value);

        if(currentHealth <= 0)
        {
            killPlayer();
        }
        //method call for play death animation
    }

    IEnumerator HitAnimationSequence()
    {
        // Wait for the hit animation duration
        playerScript.enabled = false;
        playerAnimator.SetTrigger("Damaged");
        yield return new WaitForSeconds(hitDuration);
        playerScript.enabled = true;
    }

    public void killPlayer()
    {
        //traps, enemies, gravity can all kill you
        //disable player game object instead of destroying

        //stop other objects from trying to access PLayer object reference after its destroyed
        //crude way, better to use delegate/events
        try
        {
            isDead = true;
            playerAnimator.SetBool("isDead", true);
            Destroy(GameObject.FindWithTag("Player"));
        } catch(NullReferenceException)
        {
            if(!debugDeathOnce)
            {
                Debug.Log("Player is dead");
                debugDeathOnce = true;
            }
        }

        //!!In case of gravity destroy game object (for that, set bounds, add colliders to them and set them as isTrigger)
        //play death animation
        //show retry screen:
        ReloadCurrentScene();
        DontDestroyOnLoad(scoreText);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Trap") && nextDamage<Time.time)
        {
            Debug.Log("Trap!!!!!");
            takeDamage(damage);
            nextDamage = Time.time + damageRate;

            pushBack(transform, collision.transform);
        }

        if(collision.collider.CompareTag("left_block") && collision.collider.CompareTag("right_block"))
        {
            Debug.Log("Colliding with both blocks; should be dead now.");
            //killPlayer();
        }
    }

    void pushBack(Transform pushedPlayer, Transform trap)
    {
        Debug.Log("In pushBack function");
        Vector2 pushDirection = new Vector2(pushedPlayer.position.x - trap.position.x, pushedPlayer.position.y - trap.position.y).normalized;
        pushDirection *= pushBackForce;
        Rigidbody2D pushRB = this.gameObject.GetComponent<Rigidbody2D>();

        pushRB.velocity = Vector2.zero;
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse);
    }

    public void ReloadCurrentScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
