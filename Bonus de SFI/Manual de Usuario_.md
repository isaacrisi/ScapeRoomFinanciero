## **Manual de Usuario:**

### **Introducción**

Este manual te guiará a través del uso de la aplicación de Escape Room de educación financiera, ambientada en la cárcel del Buen Pastor. El objetivo del juego es gestionar adecuadamente los recursos disponibles (luz, temperatura, y pesos colombianos) para avanzar en la historia y escapar de la cárcel. La interfaz del usuario está en el PC, y la interacción con el entorno físico es controlada por un microcontrolador Raspberry Pi Pico.

### **Requerimientos del Sistema**

1. **PC** con la aplicación de Unity instalada.  
2. **Raspberry Pi Pico** configurado y conectado al PC a través del puerto serial.  
3. **Software**:  
   * Unity 2021 o superior.  
   * IDE de Arduino para programar el Raspberry Pi Pico.  
   * Puerto de comunicación serial.

### **Instalación y Configuración**

1. **Conexión del Raspberry Pi Pico al PC**:  
   * Conecta el Raspberry Pi Pico al PC a través del puerto USB y asegúrate de que esté configurado en el puerto correcto.  
   * Abre el IDE de Arduino y carga el código del microcontrolador que permite la comunicación con Unity. Este código gestiona las variables de lúmenes, temperatura y pesos.  
   * Una vez el código esté cargado en el microcontrolador, el LED comenzará a parpadear a 0.5Hz, lo que indica que el programa está corriendo sin bloqueos.  
2. **Configuración de la Aplicación en Unity**:  
   * Abre la aplicación en Unity y verifica que la interfaz gráfica muestra los sliders de luz y temperatura, además de un contador de pesos colombianos.  
   * Los sliders no son modificables por el usuario directamente; estos cambian automáticamente según las acciones dentro del juego.

### **Interfaz de Usuario**

La interfaz de la aplicación incluye los siguientes elementos clave:

1. **Sliders de luz y temperatura**:  
   * **Luz (en lúmenes)**: Este slider comienza con un valor inicial de 20 y aumenta automáticamente cuando se realiza una compra.  
   * **Temperatura**: Funciona de manera similar al slider de luz, pero representa la temperatura ambiental en la celda. Su valor inicial es dado por el sensor de temperatura del microcontrolador.

2. **Contador de dinero**:  
   * Este contador muestra la cantidad de dinero disponible al inicio del juego. Los pesos disminuyen cada vez que realizas una compra.  
3. **Botón de Compra**:  
   * Al presionar el botón de compra, el sistema actualizará los valores de los sliders de luz y temperatura, y el contador de pesos disminuirá según el costo de la compra.  
   * La comunicación con el Raspberry Pi Pico se realiza automáticamente, enviando la actualización de las variables en un solo mensaje.

### **Cómo Jugar**

1. **Iniciar el Juego**:  
   * Al iniciar el juego, las variables iniciales de luz y temperatura estarán en sus valores predeterminados.  
   * El objetivo es gestionar adecuadamente los recursos (luz y temperatura) mientras te aseguras de no quedarte sin pesos colombianos.  
2. **Realizar una Compra**:  
   * Cada vez que realices una compra, los valores de luz y temperatura aumentarán según lo establecido, y el contador de pesos disminuirá. Debes gestionar estos recursos de manera efectiva para poder avanzar en el juego.  
3. **Verificación del Estado**:  
   * El LED del Raspberry Pi Pico parpadeará constantemente a 0.5Hz. Si deja de parpadear, significa que algo ha fallado en la comunicación o en el funcionamiento del sistema.  
4. **Finalización del Juego**:  
   * El juego termina cuando has escapado de la cárcel del Buen Pastor o cuando te quedas sin recursos financieros para seguir realizando compras.

### **Solución de Problemas**

* **El LED no parpadea**: Si el LED del Raspberry Pi Pico no está parpadeando, verifica las conexiones y asegúrate de que el código ha sido cargado correctamente en el microcontrolador.  
* **Los sliders no cambian**: Asegúrate de que el puerto COM5 está configurado correctamente y que el código en el Raspberry Pi Pico está recibiendo las actualizaciones de las variables.  
* **Error en la comunicación serial**: Si no se está enviando o recibiendo datos correctamente, verifica que la comunicación serial está funcionando a 115200 baudios en el IDE de Arduino.

### **Conclusión**

Este manual te proporciona los pasos necesarios para jugar y controlar la aplicación de Escape Room de educación financiera, garantizando que la narrativa y los conceptos financieros se entrelazan con la jugabilidad. Al gestionar los recursos de manera efectiva, puedes escapar de la cárcel del Buen Pastor, representando un aprendizaje significativo en la toma de decisiones financieras.

