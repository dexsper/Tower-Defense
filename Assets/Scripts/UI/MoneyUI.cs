using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void Awake()
    {
        Economic.OnMoneyChanged += UpdateText;
    }

    private void UpdateText(Team team, int money)
    {
        if (team == Team.Blue)
            moneyText.text = money.ToString();
    }
}
