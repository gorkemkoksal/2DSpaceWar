using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    Text myHealth;
    Player player;
    void Start()
    {
        myHealth = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        myHealth.text = player.GetHealth().ToString();
    }
}
