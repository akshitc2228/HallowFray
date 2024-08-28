using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public Animator bossAnimator;

    public int maxHealth = 100;
    int currentHealth;

    public UnityEngine.UI.Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        //play hit animation:
        bossAnimator.SetTrigger("Hurt");
        healthSlider.value = currentHealth;

        if(currentHealth <= 0)
        {
            enemyDead();
        }
    }

    void enemyDead()
    {
        bossAnimator.SetBool("isDead", true);
        Debug.Log("Boss dead");
        //dont diable enemy and box colliders if you want phases
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x,0);
        SceneManager.LoadScene("Epilogue");
    }
}
