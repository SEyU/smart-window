<h1>Simulación con Unity 3D</h1>
La última parte de este proyecto consiste en una simulación en un entorno virtual con Unity.
Para ello, el proyecto se centra en cuatro aspectos:

 - Comunicación con el bróker MQTT y obtención de datos
 - Actualización de la interfaz con los datos obtenidos
 - Ajuste del toldo y las ventanas
 - Ajustes del viento y de la luz para simular las condiciones recibidas por los sensores
 
<h2>Cliente MQTT<h2>
El [cliente MQTT](https://m2mqtt4unity.codeplex.com/) se integra directamente dentro de Unity. Una vez que se establece la comunicación, este se suscribe a todos los temas definidos y empieza a convertir y publicar eventos al resto de módulos.
Cada uno de estos módulos está escuchando a diferentes eventos y actualiza su estado conforme va recibiendo información.

<h2>Actualización de la interfaz<h2>
Este módulo simplemente actualiza la interfaz con los datos recibidos del sensor. Dichos datos son tratados y se les aplica un formato para mejorar la visualización.

<h2>Ajuste del toldo y la ventana<h2>
El ajuste del toldo se hace en base a un porcentaje recibido del sensor. Con este valor se desplaza el toldo para simular la recogida y el despliegue del mismo.
La ventana aplica una rotación en base al porcentaje de apertura que le marca el sensor.

<h2>Ajuste del viento y de la luz<h2>
Estos módulos proporcionan una simulación de las condiciones en las que se encuentran los sensores. 
Con la información recibida, ajustan la fuerza del viento, la luminosidad ambiental y la proporcionada por dos puntos de luz artificial dentro y fuera de la casa.
