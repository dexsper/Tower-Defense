using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private TextMeshProUGUI priceText;

    public void Init(string price, Sprite sprite, UnityAction action)
    {
        button.image.sprite = sprite;
        button.onClick.AddListener(action);
        priceText.text = price;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
