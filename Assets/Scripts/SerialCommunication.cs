using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class SerialCommunication : MonoBehaviour
{
    private SerialPort serialPort;
    public TextMeshProUGUI pesosText;
    public Slider temperatureSlider;
    public Slider lumenSlider;
    public Button buyButton;
    public Button buyExpensiveButton;    // Botón para la compra cara
    public Button buyCheapButton;        // Botón para la compra barata
    public TextMeshProUGUI mensajeErrorText;

    void Start()
    {

        if (GameManager.instance.comunicacionHabilitada)
        {
            mensajeErrorText.alpha = 0;
            // Usar el baud rate seleccionado
            serialPort = new SerialPort("COM5", GameManager.instance.baudRate);
            try
            {
                serialPort.Open();
                serialPort.ReadTimeout = 1000;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al abrir el puerto serial: " + e.Message);
            }
        }

        // Cargar los valores desde el GameManager
        lumenSlider.value = GameManager.instance.lumenes;
        temperatureSlider.value = GameManager.instance.temperatura;
        pesosText.text = "Pesos: " + GameManager.instance.pesos;

        buyButton.onClick.AddListener(SendValuesToMicrocontroller);
        buyExpensiveButton.onClick.AddListener(BuyExpensiveLight);
        buyCheapButton.onClick.AddListener(BuyCheapLight);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckTemperatureAndChangeScene();
        }
        
        
         if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckLumenesAndChangeScene();
        }
        // Verificar si hay datos recibidos del microcontrolador
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            string receivedData = serialPort.ReadLine();
            ProcessReceivedData(receivedData);  // Procesar los datos recibidos
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendRequestToMicrocontroller();  // Enviar la solicitud de datos al presionar Enter
        }
    }

    void SendValuesToMicrocontroller()
    {
        if (serialPort.IsOpen)
        {
            float lumenes = lumenSlider.value;
            float temperatura = temperatureSlider.value;

            string dataToSend = $"L={lumenes},T={temperatura:F2},P={GameManager.instance.pesos}";
            serialPort.WriteLine(dataToSend);
            Debug.Log("Datos enviados al microcontrolador: " + dataToSend);

            if (GameManager.instance.pesos >= GameManager.instance.precioRL && GameManager.instance.pesosHabilitados == true)
            {
                GameManager.instance.pesos -= GameManager.instance.precioRL;
                if (GameManager.instance.luzHabilitada == true)
                {
                    lumenSlider.value += GameManager.instance.aumentoLyT;
                }
                else
                {
                    mensajeErrorText.alpha = 100;
                    mensajeErrorText.text = "Luz no habilitada";
                }
                if (GameManager.instance.temperaturaHabilitada == true)
                {
                    temperatureSlider.value += GameManager.instance.aumentoLyT;
                }
                else
                {
                    mensajeErrorText.alpha = 100;
                    mensajeErrorText.text = "Temperatura no habilitada";
                }               
                pesosText.text = "Pesos: " + GameManager.instance.pesos;
            }
            else
            {
                mensajeErrorText.alpha = 100;
                mensajeErrorText.text = "No hay suficiente dinero para realizar la compra. o no está habilitada";
            }
        }
    }

    void CheckTemperatureAndChangeScene()
    {
        float currentTemperature = temperatureSlider.value;

        if (currentTemperature >= 26f)
        {
            Debug.Log("Temperatura suficiente. Cambiando a la escena Piso4.");
            SceneManager.LoadScene("Piso4Lore");
        }
        else if (GameManager.instance.pesosHabilitados == false)
        {
            SceneManager.LoadScene("Piso4Lore");
        }
        else
        {
            Debug.Log("La temperatura es demasiado baja para cambiar de escena.");
        }
    }

    void BuyExpensiveLight()
    {
        float cost = GameManager.instance.precioVelon;  // Precio de la luz cara
        float lumenIncrease = GameManager.instance.aumento_L;  // Aumento significativo de lúmenes

        if (GameManager.instance.pesos >= cost)
        {
            // Reducir el dinero y aumentar los lúmenes
            GameManager.instance.pesos -= cost;
            GameManager.instance.lumenes += lumenIncrease;

            // Actualizar la UI
            pesosText.text = "Pesos: " + GameManager.instance.pesos;
            lumenSlider.value = GameManager.instance.lumenes;

            Debug.Log("Luz cara comprada. Lúmenes: " + GameManager.instance.lumenes + ", Pesos: " + GameManager.instance.pesos);
        }
        else
        {
            Debug.LogWarning("No tienes suficientes pesos para comprar el velón.");
        }
    }
    void BuyCheapLight()
    {
        float cost = GameManager.instance.precioCP;  // Precio de la luz barata
        float lumenIncrease = GameManager.instance.aumento_l;  // Aumento menor de lúmenes

        if (GameManager.instance.pesos >= cost)
        {
            // Reducir el dinero y aumentar los lúmenes
            GameManager.instance.pesos -= cost;
            GameManager.instance.lumenes += lumenIncrease;

            // Actualizar la UI
            pesosText.text = "Pesos: " + GameManager.instance.pesos;
            lumenSlider.value = GameManager.instance.lumenes;

            Debug.Log("Luz barata comprada. Lúmenes: " + GameManager.instance.lumenes + ", Pesos: " + GameManager.instance.pesos);
        }
        else
        {
            Debug.LogWarning("No tienes suficientes pesos para comprar las chispitas mariposa.");
        }
    }
    void CheckLumenesAndChangeScene()
    {
        if (GameManager.instance.lumenes >= 100)
        {
            Debug.Log("Lúmenes suficientes. Cambiando a la escena Piso3.");
            SceneManager.LoadScene("Piso3");  // Cambiar a la escena Piso3
        }
        else
        {
            Debug.Log("No tienes suficientes lúmenes para cambiar de escena.");
        }
    }
    void SendRequestToMicrocontroller()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Write("A");  // Enviar el BYTE 'A' para solicitar la información de las variables
            Debug.Log("Solicitud enviada al microcontrolador.");
        }
    }


    void ProcessReceivedData(string data)
    {
        // Procesar los datos recibidos del microcontrolador
        string[] variables = data.Split(',');

        foreach (string variable in variables)
        {
            Debug.Log("Dato recibido: " + variable);
            // Aquí puedes actualizar tus sliders o texto dependiendo de los valores recibidos
            if (variable.StartsWith("Lumenes:"))
            {
                string[] lumenesData = variable.Split(':');
                float lumenesValue = float.Parse(lumenesData[1]);
                lumenSlider.value = lumenesValue;  // Actualizar el slider de lúmenes
            }
            else if (variable.StartsWith("Temperatura:"))
            {
                string[] tempData = variable.Split(':');
                float temperatureValue = float.Parse(tempData[1]);
                temperatureSlider.value = temperatureValue;  // Actualizar el slider de temperatura
            }
            else if (variable.StartsWith("Pesos:"))
            {
                string[] pesosData = variable.Split(':');
                float pesosValue = float.Parse(pesosData[1]);
                pesosText.text = "Pesos: " + pesosValue;  // Actualizar el texto de pesos
            }
        }
    }


    private void OnApplicationQuit()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
