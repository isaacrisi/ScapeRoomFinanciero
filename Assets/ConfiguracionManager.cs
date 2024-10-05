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

    public static int precioRL = 50;
    public static int precioCP= 30;
    public static int precioVelon = 20;

    public static int aumentoLyT = 5;
    public static int aumento_L = 3;
    public static int aumento_l = 10;


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
        int lumenes = int.Parse(lumenesInput.text);
        float temperatura = float.Parse(temperaturaInput.text);
        int pesos = int.Parse(pesosInput.text);
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
        precioRL = int.Parse(inputPrecioRL.text);
        precioCP = int.Parse(inputPrecioCP.text);
        precioVelon = int.Parse(inputPrecioVelon.text);

        aumentoLyT = int.Parse(inputAumentoLyT.text);
        aumento_L = int.Parse(inputAumento_L.text);
        aumento_l = int.Parse(inputAumento_l.text);


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
