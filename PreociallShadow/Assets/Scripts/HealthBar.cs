using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    float currHealth;
    float maxHealth = 1f;
    Slider bar;

    private void Awake()
    {
        bar = GetComponent<Slider>();
        currHealth = maxHealth;
    }

    public void getDamage(float damage)
    {
        currHealth -= damage;
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        bar.value = currHealth;
    }


}
