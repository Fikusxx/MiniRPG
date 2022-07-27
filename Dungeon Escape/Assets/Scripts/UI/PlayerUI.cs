using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region Player
    [Space(5)]
    [Header("Player")]
    #endregion
    [SerializeField] private GameObject player;
    private HealthSystem healthSystem;

    #region Visual Data
    [Space(10)]
    [Header("Visual Data")]
    #endregion
    [SerializeField] private Image healthImage;


    private void Awake()
    {
        healthSystem = player.GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        healthSystem.OnHealthChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        healthSystem.OnHealthChange -= UpdateHealthBar;
    }

    /// <summary>
    /// Update Health Bar on taking damage
    /// </summary>
    private void UpdateHealthBar(int currentHealth)
    {
        float fillRatio = (float)currentHealth / (float)healthSystem.MaxHealth;
        healthImage.fillAmount = fillRatio;
    }
}
