using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthBar _healthBar;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        //LoadPlayerHealth();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.transform.TryGetComponent<PoisonPalm>(out var enemy))
        {
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }

            _currentHealth -= enemy.Damage;
            _healthBar.SetHealthValue(_currentHealth, _maxHealth);

            //SavePlayerHealth();
        }
    }

    private void SavePlayerHealth()
    {
        HealthData playerHealthData = new HealthData(_currentHealth, _maxHealth);
        SaveSystem.Save("PlayerHealth", playerHealthData);
    }

    private void LoadPlayerHealth()
    {
        HealthData playerHealthData = SaveSystem.Load<HealthData>("PlayerHealth");
        _currentHealth = playerHealthData.currentHealth;
        _maxHealth = playerHealthData.maxHealth;
        _healthBar.SetHealthValue(_currentHealth, _maxHealth);
    }
}
