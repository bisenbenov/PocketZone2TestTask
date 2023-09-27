using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _quantityText;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _itemButton;
    [SerializeField] private InventorySO _inventory;
    
    private bool _empty = true;
    
    public event EventHandler<UIInventoryItem> OnItemClicked;

    private void OnEnable()
    {
        _itemButton.onClick.RemoveAllListeners();
        _itemButton.onClick.AddListener(()=>ShowDeleteButton());
    }

    private void OnDisable()
    {
        _itemButton.onClick.RemoveListener(() => ShowDeleteButton());
    }

    private void Awake()
    {
        ResetData();
    }

    public void ShowDeleteButton()
    {
        if (_deleteButton.gameObject.activeInHierarchy)
        {
            _deleteButton.gameObject.SetActive(false);
            _deleteButton.onClick.RemoveListener(Select);
        }
        else
        {
            _deleteButton.gameObject.SetActive(true);
            _deleteButton.onClick.AddListener(Select);
        }
    }

    private void Select()
    {
        OnItemClicked?.Invoke(this, this);
    }

    public void ResetData()
    {
        _itemImage.gameObject.SetActive(false);
        _empty = true;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = sprite;

        if (quantity > 1)
        {
            _quantityText.text = quantity + "";
        }
        else
        {
            _quantityText.text = "";
        }

        _empty = false;
    }
}
