const int ledPin = 25;  // Pin del LED integrado en el Raspberry Pi Pico
unsigned long previousMillis = 0;
const long interval = 250;
float currentLumenes = 50.0;    // Valor actual de lúmenes
float currentTemperature = 22.0; // Valor actual de temperatura
float pesos = 100000.0;          // Valor actual de pesos
bool lumenesHabilitados = true;
bool temperaturaHabilitada = true;
float deltaLumenes = 5.0;        // Delta de cambio de lúmenes
float deltaTemperatura = 0.5;    // Delta de cambio de temperatura
float deltaPesos = -100.0;       // Delta de cambio de pesos (descuento por compra)

// Función para parpadear el LED
void blinkLED()
{
    unsigned long currentMillis = millis();

    if (currentMillis - previousMillis >= interval)
    {
        previousMillis = currentMillis;
        int ledState = digitalRead(ledPin);
        digitalWrite(ledPin, !ledState);
    }
}

void setup()
{
    Serial.begin(9600);
    pinMode(ledPin, OUTPUT);
}

void loop()
{
    blinkLED();  // Parpadeo del LED

    // Si el PC envía un byte de solicitud, responder con la información de las variables
    if (Serial.available() > 0)
    {
        char request = Serial.read();  // Leer el BYTE de solicitud
        if (request == 'A')  // Usaremos el BYTE 'A' como solicitud
        {
            // Enviar los valores de todas las variables
            Serial.print("Lumenes:");
            Serial.print(currentLumenes);
            Serial.print(", Habilitado:");
            Serial.print(lumenesHabilitados ? "1" : "0");
            Serial.print(", Delta:");
            Serial.println(deltaLumenes);

            Serial.print("Temperatura:");
            Serial.print(currentTemperature);
            Serial.print(", Habilitada:");
            Serial.print(temperaturaHabilitada ? "1" : "0");
            Serial.print(", Delta:");
            Serial.println(deltaTemperatura);

            Serial.print("Pesos:");
            Serial.print(pesos);
            Serial.print(", Habilitada:");
            Serial.print((pesos >= 0) ? "1" : "0");
            Serial.print(", Delta:");
            Serial.println(deltaPesos);
        }
    }
}
