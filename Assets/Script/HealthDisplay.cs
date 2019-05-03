using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    TextMeshProUGUI healthText;
    //GameSession gameSession;
    [SerializeField] Player player;
    


    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        //player = FindObjectOfType<Player>();
    }

   

    // Update is called once per frame
    void Update()
    {

        healthText.text = player.GetHealth().ToString();
        Debug.Log(player.GetHealth().ToString());
    }
}
