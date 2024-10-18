### **Proceso de Construcción del Proyecto: Escape Room de Educación Financiera**

El proyecto comenzó con la idea de utilizar la narrativa de *One Piece* y su famosa prisión, Impel Down, como base para una aplicación de educación financiera en modalidad de Escape Room. Inicialmente, la narrativa se centraba en la simulación de la fuga de Luffy de Impel Down, pero rápidamente evolucionó hacia un contexto más local, adaptando la historia a la cárcel del Buen Pastor.

La esencia del proyecto sigue siendo un Escape Room, donde los jugadores deben resolver diversos retos relacionados con la gestión financiera para lograr escapar de la cárcel. Los elementos del entorno físico, así como las variables psicológicas y ambientales, juegan un rol fundamental en el aprendizaje, creando una experiencia interactiva y educativa.

### **Implementación Técnica**

El proyecto utiliza un enfoque dual con un sistema compuesto por dos partes: una aplicación que corre en un PC y un microcontrolador Raspberry Pi Pico. Ambas plataformas se comunican para gestionar las variables clave de la experiencia: luz (lúmenes), temperatura y la cantidad de pesos colombianos disponibles para gastar.

#### **Parte de Unity (PC)**

En la implementación de la aplicación en Unity, la interfaz gráfica permite al usuario interactuar con los elementos clave. Los sliders de luz y temperatura se actualizan automáticamente cada vez que el jugador realiza una acción de compra. También, un contador de pesos muestra la cantidad disponible, que disminuye con cada compra realizada.

A lo largo del proceso, se configuró la lógica de los sliders para que comenzaran con valores iniciales: los lúmenes arrancan en 20 y aumentan automáticamente cuando se hace una compra, mientras que la temperatura sigue un comportamiento similar, estos valores iniciales luego se implementó un método que permitiera personalizarlos desde la escena de’ configuración. La cantidad de pesos, por otro lado, empieza con un valor predefinido también personalizable y se reduce a medida que el usuario avanza en el juego.

#### **Parte del Raspberry Pi Pico (Microcontrolador)**

En paralelo, el microcontrolador Raspberry Pi Pico gestiona el comportamiento de las variables desde su lado. El LED conectado al Raspberry Pi parpadea a una frecuencia de 1 Hz, indicando que el programa está corriendo correctamente. A su vez, el microcontrolador se encarga de recibir comandos del PC y enviar de vuelta el estado de las tres variables en un solo mensaje, cumpliendo con el requerimiento de eficiencia en la comunicación entre dispositivos.

El código fue diseñado para que el Raspberry Pi recibiera las instrucciones desde Unity mediante una única instrucción y reportara el estado de las tres variables al PC en un solo paquete de datos. También se incluyó la funcionalidad de cambiar la velocidad de actualización de las variables y de verificar que el sistema no se bloquea gracias al parpadeo constante del LED.

### **Resultados**

A lo largo del desarrollo, no solo se ajustaron los aspectos técnicos del código y la lógica de comunicación entre PC y microcontrolador, sino que se profundizó en cómo conectar las variables, generando una experiencia envolvente y significativa para los jugadores. La narrativa original de Impel Down evolucionó para adaptarse al contexto local de la cárcel del Buen Pastor, añadiendo una dimensión culturalmente relevante y más atractiva para el público objetivo.

El resultado es una plataforma que no solo reta a los usuarios a resolver acertijos y gestionar recursos, sino que lo hace dentro de una historia envolvente, donde la lógica de escape de una cárcel ficticia se entrelaza con los principios de educación financiera. Cada decisión de compra, cada ajuste de temperatura y luz, tiene implicaciones directas en el éxito o fracaso de la experiencia, lo que convierte este proyecto en un reto educativo dinámico y práctico.

Muy importante resaltar que en este proyecto hubo un apoyo grande de la inteligencia artificial Chat GPT, aca adjunto un resumen que él mismo hizo sobre cómo fue su ayuda y la implementación de esta en el proyecto.

“Al final del desarrollo de este proyecto, fue fundamental el proceso de colaboración para lograr que la narrativa, la implementación técnica y la experiencia del usuario convergieran de manera efectiva. A través de los distintos pasos, la interacción se centró en varios aspectos clave:

### **Prompts y Solicitudes:**

* **Narrativa**: Comenzamos con la idea de utilizar la fuga de Luffy de Impel Down como base de la historia, pero adaptamos la narrativa para que encajara en un contexto más local y significativo, ambientando la experiencia en la cárcel del Buen Pastor.  
* **Desarrollo técnico**: La implementación incluyó instrucciones sobre cómo configurar correctamente el microcontrolador Raspberry Pi Pico y Unity para que se comunicaran. Se trabajó en el manejo de variables como lúmenes, temperatura y pesos colombianos, de modo que el PC y el microcontrolador intercambiaran datos en una única instrucción.  
* **Interacción**: A lo largo del desarrollo, te ayudé a solucionar errores de comunicación, ajustar los valores iniciales de los sliders, configurar correctamente los botones en Unity, y hacer que las variables del microcontrolador cambiaran en tiempo real.  
* **Depuración y prueba**: Juntos enfrentamos varios errores, como la falta de recepción de datos en Unity y la ausencia de cambios en las variables, lo que nos llevó a ajustar el flujo de comunicación. También reconfiguramos la funcionalidad del LED para asegurar que parpadeara a 1Hz mientras el programa estuviera en ejecución.

La combinación de estas indicaciones y ajustes técnicos logró que el proyecto funcionara en ambas plataformas, con una narrativa coherente y elementos interactivos que permiten el aprendizaje financiero a través de una experiencia inmersiva y educativa.“

Para poder realizar los requerimientos del trabajo final de la unidad tres el proyecto se modificó para poder enviar y recibir datos en punto flotante, se realizó igualmente en ascii porque no se especificaba en qué forma se debían hacer los pasos de informacion. 

Se actualizaron todas las variables y la forma en la que se leian; para esto en el micro se crearon los contadores para las tres variables, y al final una de las variables se cambió por el valor de la temperatura real que entregaba el micro. 

