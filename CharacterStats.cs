using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
	public HealthBarScript healthBar;
	public bool OpenDieScene= false;
	public int maxHealth = 100;
	public int currentHealth;
	 int checkhealth;
	public int damage = 10;
	void Update()
	{
		healthBar.SetHealth(currentHealth);
		if (currentHealth < maxHealth && currentHealth > 0 &&(!hpReg))
		{
			
			StartCoroutine(RegainHealthOverTime());
		}

	}

    void Awake()
	{
		currentHealth = maxHealth;
	}

   
    public void TakeDamage(int damage)
	{
		damage = Mathf.Clamp(damage, 0, int.MaxValue);
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die() { 
		if (gameObject.tag == "Enemy")
        {
			Destroy(gameObject);
		} else Debug.Log(transform.name + " died."); 
		if (gameObject.name == "Player")
        {
			LoadScene();
		}

	}
	public void HPplus()
	{
		currentHealth +=10;
	}

	public void LoadScene()
    {
    	SceneManager.LoadScene("Die");
    }
	private bool hpReg;
	private IEnumerator RegainHealthOverTime()
	{
		hpReg = true;
		while (currentHealth < 100)
		{
			HPplus();
			yield return new WaitForSeconds(1.5f);
		}
		hpReg = false;
	}
}

