using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dishes : MonoBehaviour
{
    public string dishName;
    public int requiredIngredients;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null && player.heldObject != null)
            {
                // Verifica se o jogador est� segurando o n�mero correto de ingredientes
                int heldIngredients = player.heldObject.GetComponent<Player.Ingredient>().ingredientCount;

                if (heldIngredients == requiredIngredients)
                {
                    // Prato completo
                    Debug.Log("Prato completo! " + dishName);
                }
                else
                {
                    // Prato incompleto
                    Debug.Log("Prato incompleto! " + dishName);
                }

                // Remove o objeto dos ingredientes
                Destroy(player.heldObject);
                player.heldObject = null;
            }
        }
    }
}

