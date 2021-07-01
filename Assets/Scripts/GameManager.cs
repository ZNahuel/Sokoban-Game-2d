using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool readyForInput;
    public Player player;

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();

        if (input.sqrMagnitude > 0.5)
        {
            if (readyForInput)
            {
                readyForInput = false;
                player.Move(input);
            }
        }
        else
        {
            readyForInput = true;
        }
    }
}