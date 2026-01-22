# Ejecución de aplicativo
1. Tener soporte para net core 8 y sqlite en el equipo donde se va a ejecutar la aplicacion
2. Crear la base de datos dentro de sqlite, para lo cual es necesario ejecutar el archivo "creatablas.sql" y posteriormente "bbdd.sql"
3. Ejecutar la aplicación, para lo cual se debe correr en línea de comandos "dotnet run".
4. Cuando finalice el paso anterior, en el navegador consumir la url: http://localhost:5088/swagger/index.html desde donde se pueden encontrar todos los endpoints para consumir.

# Endpoints
Dentro del swagger se encuentran los siguientes endpoints:

GET Api/siniestros/Buscar: Endpoint que se consume mediante get que permite consultar los siniestros registrados en la base de datos.  Se puede filtrar por alguno o varios de los siguientes items: departamentoid, fechaInicio, fechaFin, Pagina y tamañoPagina.  

POST Api/Siniestros/Registrar: Endpoint que se consume mediante post que permite registrar un siniestro.  La información a enviar es la siguiente: 
{
  "fechaHora": "YYY-MM-DDTHH:MM:SS", //fecha y hora del siniestro
  "ciudadId": N, //Id de la ciudad registrada en la base de datos
  "tipoSiniestroId": N, //Id del tipo de siniestro registrado en la base de datos
  "vehiculosInvolucrados": N, //cantidad de vehículos involucrados en el siniestro
  "numeroVictimas": N, //Cantidad de víctimas del siniestro
  "descripcion": "texto" //Cadena de texto opcional para observaciones del siniestro
}

GET API/Siniestro/detalle/{Id} Endpoint que se consume mediante get que permite consultar un siniestro en específico. Para esto, es necesario reemplazar {id} por el número del siniestro a consultar.


# Arquitectura
El desarrollo está separado en 3 capas siguiendo principios de clean code, las cuales son:
Aplicacion
Dominio
Arquitectura

Y se tomaron las siguientes decisiones de arquitefctura:
Arquitectura limpia con CQRS
Separación Command-Query
SQLite como base de Datos
Patrón Repository
Mediator con MediatR
Paginación Offset-Based

Esto permite que la aplicación sea escalable, con un bajo acoplamiento y manteniendo una separación clara entre los flujos de negocio, aplicación y la definición de la arquitectura.  Adicionalmente, no depende de un sistema operativo en específico ni de una base de datos, ya que al tener este bajo acoplamiento permite hacer estos ajustes sin contratiempo.

Se decidió utilizar sQLite como base de datos por ser una base ligera y portable, lo cual se adapta a los requerimientos del presente proyecto.
