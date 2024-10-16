using TMPro;  // Para TextMeshPro
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfiguracionManager : MonoBehaviour
{
    public TMP_InputField lumenesInput;
    public TMP_InputField temperaturaInput;
    public TMP_InputField pesosInput;
    public Toggle habilitarComunicacionToggle;
    public TMP_Dropdown baudRateDropdown;
    public Button confirmButton;

    // Referencias a los elementos de UI
    public Toggle toggleLuz;
    public Toggle toggleTemperatura;
    public Toggle togglePesos;

    public TMP_InputField inputPrecioRL;
    public TMP_InputField inputPrecioCP;
    public TMP_InputField inputPrecioVelon;

    public TMP_InputField inputAumentoLyT;
    public TMP_InputField inputAumento_L;
    public TMP_InputField inputAumento_l;

    // Variables para almacenar la configuración
    public static bool luzHabilitada = true;
    public static bool temperaturaHabilitada = true;
    public static bool pesosHabilitados = true;

    public static float precioRL = 20.5f;
    public static float precioCP= 20.2f;
    public static float precioVelon = 40.2f;

    public static float aumentoLyT = 5.2f;
    public static float aumento_L = 3.3f;
    public static float aumento_l = 10.1f;


    void Start()
    {
        // Configurar el botón de confirmación
        confirmButton.onClick.AddListener(ConfirmarConfiguracion);

        // Opciones de baud rates
        baudRateDropdown.options.Clear();
        baudRateDropdown.options.Add(new TMP_Dropdown.OptionData("9600"));
        baudRateDropdown.options.Add(new TMP_Dropdown.OptionData("14400"));
        baudRateDropdown.options.Add(new TMP_Dropdown.OptionData("19200"));
        baudRateDropdown.options.Add(new TMP_Dropdown.OptionData("38400"));
        baudRateDropdown.options.Add(new TMP_Dropdown.OptionData("57600"));
        baudRateDropdown.options.Add(new TMP_Dropdown.OptionData("115200"));

        // Valores iniciales opcionales
        lumenesInput.text = GameManager.instance.lumenes.ToString();
        temperaturaInput.text = GameManager.instance.temperatura.ToString();
        pesosInput.text = GameManager.instance.pesos.ToString();
        habilitarComunicacionToggle.isOn = GameManager.instance.comunicacionHabilitada;
        baudRateDropdown.value = GameManager.instance.baudRateIndex;
    }

    void ConfirmarConfiguracion()
    {
        // Obtener valores de los Input Fields
        float lumenes = float.Parse(lumenesInput.text);
        float temperatura = float.Parse(temperaturaInput.text);
        float pesos = float.Parse(pesosInput.text);
        bool habilitarComunicacion = habilitarComunicacionToggle.isOn;
        int baudRate = int.Parse(baudRateDropdown.options[baudRateDropdown.value].text);

        // Guardar la habilitación de cada variable
        bool luzHabilitada = toggleLuz.isOn;
        bool temperaturaHabilitada = toggleTemperatura.isOn;
        bool pesosHabilitados = togglePesos.isOn;

        GameManager.instance.luzHabilitada= luzHabilitada;
        GameManager.instance.temperaturaHabilitada = temperaturaHabilitada;
        GameManager.instance.pesosHabilitados = pesosHabilitados;

        // Guardar los precios y aumentos ingresados por el usuario
        precioRL = float.Parse(inputPrecioRL.text);
        precioCP = float.Parse(inputPrecioCP.text);
        precioVelon = float.Parse(inputPrecioVelon.text);

        aumentoLyT = float.Parse(inputAumentoLyT.text);
        aumento_L = float.Parse(inputAumento_L.text);
        aumento_l = float.Parse(inputAumento_l.text);


        // Actualizar los valores en el GameManager
        GameManager.instance.lumenes = lumenes;
        GameManager.instance.temperatura = temperatura;
        GameManager.instance.pesos = pesos;
        GameManager.instance.valorInicialPesos = pesos;
        GameManager.instance.comunicacionHabilitada = habilitarComunicacion;
        GameManager.instance.baudRate = baudRate;
        GameManager.instance.baudRateIndex = baudRateDropdown.value;

        GameManager.instance.precioRL = precioRL;
        GameManager.instance.precioCP = precioCP;
        GameManager.instance.precioVelon = precioVelon;
        GameManager.instance.aumentoLyT = aumentoLyT;
        GameManager.instance.aumento_L = aumento_L;
        GameManager.instance.aumento_l = aumento_l;

        // Cambiar a la siguiente escena (por ejemplo, "Piso4" o cualquier otra)
        SceneManager.LoadScene("Intro");
    }
}
