using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<Order> orders = new List<Order>(); // Lista de pedidos pendentes

    public TextMeshProUGUI orderText;
    public GameObject[] orderPrefab;
    private TimeManager timeManager;
    private float nextSpawnTime = 0f;
    public float spawnInterval = 15f;
    public GameObject medics;
    private int orderGenerated = 0;
    public GameObject point1, point2;
    private void Start()
    {
        // Exemplo de criação de pedidos pré-definidos
        Order order1 = new Order("Hamburger", new List<string> { "Pão", "Carne", "Alface", "Tomate" });
        Order order2 = new Order("Pizza", new List<string> { "Massa", "Queijo", "Molho de Tomate", "Pepperoni" });

        orders.Add(order1);
        orders.Add(order2);

        timeManager = gameObject.GetComponent<TimeManager>();

        StartCoroutine(SpawnOrders());

        // Gerar um pedido aleatório
        //GenerateRandomOrder();
    }

    /*private void GenerateRandomOrder()
    {
        int randomIndex = Random.Range(0, orders.Count);
        Order randomOrder = orders[randomIndex];
        Debug.Log("Novo pedido: " + randomOrder.dishName);

        // Acessar os ingredientes do pedido
        foreach (string ingredient in randomOrder.ingredients)
        {
            Debug.Log(" - " + ingredient);
        }
    }*/
    public void Update()
    {
        medics.transform.position = Vector3.MoveTowards(point1.transform.position, point2.transform.position, 10f);
    }
    public void SpawnMedic()
    {
       
            Instantiate(medics, point1.transform.position, Quaternion.identity);
        
    }
    IEnumerator SpawnOrders()
    {

        float x = -7.9f;
        float y = 4.1f;

        while (x <= 3.5f)
        {

            Vector2 spawnPosition = new Vector2(x, y);
            Instantiate(orderPrefab[Random.Range(0, orderPrefab.Length)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(5f);
            SpawnMedic();
            orderGenerated++;
            x += 1.9f;


            yield return null;
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
}