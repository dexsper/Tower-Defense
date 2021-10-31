using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private Team team;

    private void Awake()
    {
        Economic.OnMoneyChanged += UpdateText;
    }

    private void UpdateText(Team team, int money)
    {
        if (this.team == team)
            moneyText.text = money.ToString();
    }
}
