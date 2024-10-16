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
        // Almacenar el valor inicial de los pesos (por ejemplo, al inicio del juego o cuando se entr� a la prisi�n)
        valorInicialPesos = GameManager.instance.valorInicialPesos;

        // Calcular el 50% del valor inicial para la "libertad"
        precioLibertad = valorInicialPesos / 2;

        // Mostrar el texto con la corrupci�n y el precio de la libertad
        textoCorrupcion.text = $"Y como estamos en Colombia, el �ltimo paso para escapar de prisi�n es algo de corrupci�n.\n" +
                               $"El �ltimo guardia te ofrece tu libertad a cambio de que le compres una arepa con quesito, natilla y bu�uelos que te cuesta {precioLibertad} pesos.\n" +
                               $"Es lo �ltimo para huir, si no gestionaste bien tus recursos presiona F para volver al �ltimo piso de la prisi�n.";
    }

    void Update()
    {
        // Verificar si el jugador tiene m�s del 50% del valor inicial
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
            if (Input.GetKeyDown(KeyCode.F))  // Retroceder al �ltimo piso
            {
                Debug.Log("No tienes suficiente dinero. Volviendo al �ltimo piso.");
                SceneManager.LoadScene("Configuracion");  // Cambia a la escena correspondiente
            }
        }
    }
}
