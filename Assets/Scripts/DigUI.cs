using UnityEngine;
using UnityEngine.UI;

public class DigUI : MonoBehaviour{
    public Text digText;

    public void UpdateDigText(int attemptsLeft){
        digText.text = "Attempts Left: " + attemptsLeft;
    }
}
