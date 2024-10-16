using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Variables persistentes entre escenas
    public float pesos = 100000;
    public float lumenes = 20;
    public float temperatura = 20.0f;

    // Variables de configuración
    public bool comunicacionHabilitada = true;
    public int baudRate = 9600;
    public int baudRateIndex = 0;  // Índice del dropdown de baud rate

    public float valorInicialPesos = 1000.40f;

    public float precioRL = 55.4f;
    public float precioCP = 30.2f;
    public float precioVelon = 20.3f;

    public float aumentoLyT = 1.1f;
    public float aumento_L = 3.2f;
    public float aumento_l = 10.1f;

    public bool luzHabilitada = true;
    public bool temperaturaHabilitada = true;
    public bool pesosHabilitados = true;

    void Awake()
    {
        // Verificar si ya existe una instancia del GameManager
        if (instance == null)
        {
            instance = this;  // Asignar esta instancia a la variable estática
            DontDestroyOnLoad(gameObject);  // Evitar que se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject);  // Destruir este objeto si ya existe una instancia
        }
    }

    // Método para obtener los valores actuales para enviar de vuelta al microcontrolador
    public string GetValuesForRaspberry()
    {
        return $"L={lumenes},T={temperatura:F2},P={pesos}";
    }
}
