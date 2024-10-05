using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Variables persistentes entre escenas
    public int pesos = 100000;
    public int lumenes = 20;
    public float temperatura = 20.0f;

    // Variables de configuración
    public bool comunicacionHabilitada = true;
    public int baudRate = 9600;
    public int baudRateIndex = 0;  // Índice del dropdown de baud rate

    public int valorInicialPesos = 100000;

    public int precioRL = 50;
    public int precioCP = 30;
    public int precioVelon = 20;

    public int aumentoLyT = 5;
    public int aumento_L = 3;
    public int aumento_l = 10;

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
