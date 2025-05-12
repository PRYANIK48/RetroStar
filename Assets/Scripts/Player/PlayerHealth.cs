using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool IsDead { get; private set; }

    [SerializeField] private Sprite fullHealthImage, emptyHealthImage;
    [SerializeField] private int timeBetweenHealthRegeneration = 10;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Transform HealthContainer;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    const string HEALTH_CONTAINER_TEXT = "Health Container";
    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake() {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        HealthContainer = GameObject.Find(HEALTH_CONTAINER_TEXT).transform;
        IsDead = false;
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy) {
            TakeDamage(1, other.transform);
        }
    }

    public void HealPlayer() {
        if (currentHealth < maxHealth) {
            currentHealth += 1;
        }
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount, Transform hitTransform) {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        UpdateHealthUI();
        StartCoroutine(DamageRecoveryRoutine());
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath() {
        if (currentHealth <= 0 && !IsDead) {
            IsDead = true;
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }
    private IEnumerator RefreshHealthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenHealthRegeneration);
            HealPlayer();
        }
    }
    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    private void UpdateHealthUI()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i <= currentHealth - 1)
            {
                HealthContainer.GetChild(i).GetComponent<Image>().sprite = fullHealthImage;
            }
            else
            {
                HealthContainer.GetChild(i).GetComponent<Image>().sprite = emptyHealthImage;
            }
        }

        if (currentHealth < maxHealth)
        {
            StartCoroutine(RefreshHealthRoutine());
        }
    }
}
