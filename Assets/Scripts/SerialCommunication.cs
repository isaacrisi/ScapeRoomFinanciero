using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Threading;

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

    private Thread serialReadThread;
    private Thread serialWriteThread;
    private bool isRunning = false;
    private string receivedData;
    private string dataToSend;

    void Start()
    {
        if (GameManager.instance.comunicacionHabilitada)
        {
            mensajeErrorText.alpha = 0;
            serialPort = new SerialPort("COM5", GameManager.instance.baudRate);
            try
            {
                serialPort.Open();
                serialPort.ReadTimeout = 1000;

                // Iniciar el hilo de lectura
                isRunning = true;
                serialReadThread = new Thread(ReadFromSerialPort);
                serialReadThread.Start();

                // Iniciar el hilo de escritura
                serialWriteThread = new Thread(WriteToSerialPort);
                serialWriteThread.Start();
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

        buyButton.onClick.AddListener(() => { PrepareDataToSend(); });
        buyExpensiveButton.onClick.AddListener(BuyExpensiveLight);
        buyCheapButton.onClick.AddListener(BuyCheapLight);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckTemperatureAndChangeScene();
        }

        if (!string.IsNullOrEmpty(receivedData))
        {
            Debug.Log("Datos recibidos del microcontrolador: " + receivedData);
            receivedData = null;  // Limpiar la variable para la próxima recepción
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckLumenesAndChangeScene();
        }
    }

    void ReadFromSerialPort()
    {
        while (isRunning)
        {
            try
            {
                if (serialPort.IsOpen && serialPort.BytesToRead > 0)
                {
                    receivedData = serialPort.ReadLine();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al leer del puerto serial: " + e.Message);
            }
        }
    }

    void WriteToSerialPort()
    {
        while (isRunning)
        {
            if (!string.IsNullOrEmpty(dataToSend) && serialPort.IsOpen)
            {
                try
                {
                    serialPort.WriteLine(dataToSend);
                    Debug.Log("Datos enviados al microcontrolador: " + dataToSend);
                    dataToSend = null;  // Limpiar la variable tras enviar los datos
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error al enviar datos al puerto serial: " + e.Message);
                }
            }
            Thread.Sleep(100); // Evitar sobrecarga en el hilo
        }
    }

    void PrepareDataToSend()
    {
        float lumenes = Mathf.RoundToInt(lumenSlider.value);
        float temperatura = temperatureSlider.value;

        dataToSend = $"L={lumenes},T={temperatura:F2},P={GameManager.instance.pesos}";

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
        float cost = GameManager.instance.precioVelon;
        float lumenIncrease = GameManager.instance.aumento_L;

        if (GameManager.instance.pesos >= cost)
        {
            GameManager.instance.pesos -= cost;
            GameManager.instance.lumenes += lumenIncrease;

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
        float cost = GameManager.instance.precioCP;
        float lumenIncrease = GameManager.instance.aumento_l;

        if (GameManager.instance.pesos >= cost)
        {
            GameManager.instance.pesos -= cost;
            GameManager.instance.lumenes += lumenIncrease;

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
            SceneManager.LoadScene("Piso3");
        }
        else
        {
            Debug.Log("No tienes suficientes lúmenes para cambiar de escena.");
        }
    }

    private void OnApplicationQuit()
    {
        isRunning = false;
        if (serialReadThread != null && serialReadThread.IsAlive)
        {
            serialReadThread.Join();
        }
        if (serialWriteThread != null && serialWriteThread.IsAlive)
        {
            serialWriteThread.Join();
        }

        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
