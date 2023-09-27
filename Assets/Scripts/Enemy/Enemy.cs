using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private List<ItemSO> _loot = new();

    [SerializeField] private GameObject _lootPrefab;
     
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        //LoadEnemyHealth();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.transform.TryGetComponent<Bullet>(out var bullet))
        {
            Destroy(bullet.gameObject);
            
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
                DropItem();
            }

            _currentHealth -= bullet.Damage;
            _healthBar.SetHealthValue(_currentHealth, _maxHealth);

            //SaveEnemyHealth();
        }
    }

    private void DropItem()
    {
        var droppedItem = _loot[Random.Range(0, _loot.Count)];
        if (droppedItem != null)
        {
            var item = Instantiate(_lootPrefab, gameObject.transform.position, Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = droppedItem.ItemImage;
            item.GetComponent<Item>().InventoryItem = droppedItem;
        }
    }

    private void SaveEnemyHealth()
    {
        HealthData enemyHealthData = new HealthData(_currentHealth, _maxHealth);
        SaveSystem.Save("EnemyHealth_" + gameObject.name, enemyHealthData);
    }

    private void LoadEnemyHealth()
    {
        HealthData enemyHealthData = SaveSystem.Load<HealthData>("EnemyHealth_" + gameObject.name);
        _currentHealth = enemyHealthData.currentHealth;
        _maxHealth = enemyHealthData.maxHealth;
        _healthBar.SetHealthValue(_currentHealth, _maxHealth);
    }
}
