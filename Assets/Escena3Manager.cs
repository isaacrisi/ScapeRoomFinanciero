using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escena3Manager : MonoBehaviour
{
    public TextMeshProUGUI textoCorrupcion;
    private float precioLibertad;
    private float valorInicialPesos;

    void Start()
    {
        // Almacenar el valor inicial de los pesos (por ejemplo, al inicio del juego o cuando se entró a la prisión)
        valorInicialPesos = GameManager.instance.valorInicialPesos;

        // Calcular el 50% del valor inicial para la "libertad"
        precioLibertad = valorInicialPesos / 2;

        // Mostrar el texto con la corrupción y el precio de la libertad
        textoCorrupcion.text = $"Y como estamos en Colombia, el último paso para escapar de prisión es algo de corrupción.\n" +
                               $"El último guardia te ofrece tu libertad a cambio de que le compres una arepa con quesito, natilla y buñuelos que te cuesta {precioLibertad} pesos.\n" +
                               $"Es lo último para huir, si no gestionaste bien tus recursos presiona F para volver al último piso de la prisión.";
    }

    void Update()
    {
        // Verificar si el jugador tiene más del 50% del valor inicial
        if (GameManager.instance.pesos >= precioLibertad)
        {
            if (Input.GetKeyDown(KeyCode.Return))  // Cambiar a la escena final
            {
                Debug.Log("Has comprado tu libertad. Cambiando a la escena final.");
                SceneManager.LoadScene("Final");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))  // Retroceder al último piso
            {
                Debug.Log("No tienes suficiente dinero. Volviendo al último piso.");
                SceneManager.LoadScene("Configuracion");  // Cambia a la escena correspondiente
            }
        }
    }
}
