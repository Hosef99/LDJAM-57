using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UndergroundUI : MonoBehaviour{
    public TextMeshProUGUI staminaText;

    public void UpdateStamina(int stamina){
        staminaText.text = stamina.ToString();
    }
}
