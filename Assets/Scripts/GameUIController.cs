using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public TextMeshProUGUI gasText;    

    void Start(){
        gasText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateGas(int currentGas, int maxGas){
        gasText.text = currentGas + "/" + maxGas;
    }
}
