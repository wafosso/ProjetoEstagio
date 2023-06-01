using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<Order> orders = new List<Order>(); // Lista de pedidos pendentes

    private void Start()
    {
        // Exemplo de cria��o de pedidos pr�-definidos
        Order order1 = new Order("Hamburger", new List<string> { "P�o", "Carne", "Alface", "Tomate" });
        Order order2 = new Order("Pizza", new List<string> { "Massa", "Queijo", "Molho de Tomate", "Pepperoni" });

        orders.Add(order1);
        orders.Add(order2);

        // Gerar um pedido aleat�rio
        GenerateRandomOrder();
    }

    private void GenerateRandomOrder()
    {
        // L�gica para gerar um pedido aleat�rio
        int randomIndex = Random.Range(0, orders.Count);
        Order randomOrder = orders[randomIndex];
        Debug.Log("Novo pedido: " + randomOrder.dishName);

        // Acessar os ingredientes do pedido
        foreach (string ingredient in randomOrder.ingredients)
        {
            Debug.Log(" - " + ingredient);
        }
    }
}

public class Order
{
    public string dishName;
    public List<string> ingredients;

    public Order(string name, List<string> ingredientList)
    {
        dishName = name;
        ingredients = ingredientList;
    }
}