using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private float invincibilityTimer;
    [SerializeField]
    private SpriteRenderer characterHeadSprite;

    [HideInInspector]
    public int currentHealth;

    private bool isInvincible = false;

    private void Awake() 
    {
        currentHealth = maxHealth;    

        // Green Color
        characterHeadSprite.color = new Color(100 / 255f, 217 / 255f, 112 / 255f);
    }

    public void TakeDamage()
    {
        if (!isInvincible)
        {
            currentHealth--;

            // Call Invincible caroutine
            StartCoroutine("invincibleTimer");

            if (currentHealth == 3)
                // Green Color
                characterHeadSprite.color = new Color(100 / 255f, 217 / 255f, 112 / 255f);
            else if (currentHealth == 2)
                // Orange Color
                characterHeadSprite.color = new Color(229 / 255f, 173 / 255f, 84 / 255f);
            else if (currentHealth == 1)
                // Red Color
                characterHeadSprite.color = new Color(217 / 255f, 99 / 255f, 105 / 255f);
            else if (currentHealth <= 0)
            {
                // White Color
                characterHeadSprite.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                // Function to handle player death
            }
        }
    }

    IEnumerator invincibleTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTimer);
        isInvincible = false;
    }
}
