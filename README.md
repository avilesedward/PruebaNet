# PruebaNet API de Registro de Productos

## Descripción
Este proyecto es una API RESTful desarrollada en .NET 8 mediante un proyecto WebApi para gestionar un sistema de registro de productos. La API permite realizar operaciones CRUD (Crear, Leer, Actualizar) sobre productos y está diseñada siguiendo principios SOLID, patrones de diseño y buenas prácticas de Clean Code. Además, se ha implementado TDD (Proyecto test) para asegurar la calidad del código.

## Requisitos
- .NET 8 SDK
- Visual Studio 2022 o superior / Visual Studio Code
- SQL Server

## Estructura del Proyecto
- **Api**: Proyecto WebApi, contiene el único controlador del proyecto ProductoController.
- **Negocio**: Biblioteca de clases que contiene la lógica de negocio.
- **Datos**: biblioteca de clases que implementa EntityFramework para el acceso a datos.
- **Modelos**: Biblioteca de clases que contiene los modelos de datos.
- **DTO**:  Biblioteca de clases que contiene los objetos de transferencia de datos.
- **Test**: Proyecto XUnit de pruebas unitarias.

## Funcionalidades
1. **Operaciones CRUD**:
   - **POST**: (Api/Producto) Crear un nuevo producto.
   - **PUT**: (Api/Producto/{Id}) Actualizar un producto existente.
   - **GET**: (Api/Producto/{Id}) Obtener un producto por su ID.

2. **Logueo de Tiempo de Respuesta**:
   - Loguea el tiempo de respuesta de cada request en un archivo de texto plano.
   - La ruta y el nombre del archivo plano se configura en el archivo  appsettings.json. en las llaves RutaLogTiempo y ArchivoLogTiempo.
   - Para el logueo se implementó la clase RequestTime.cs en el proyecto Api y se usa como un servicio Middleware.

3. **Caché de Estados del Producto**:
   - Mantiene en caché por 5 minutos un diccionario de estados del producto.
   - El tiempo de duración del caché en minutos se puede configurar en el archivo appsettings.json. en la llave TiempoMinutosCache.

4. **Persistencia de Datos**:
   - Guarda la información del producto en una base de datos Ms Sql Sever.
   - La persistencia se realizó usando el paquete EntityFrameworkCore.SqlServer.

5. **Descuento y Precio Final**:
   - Calcula el descuento y el precio final del producto basado en un servicio externo que se puede configurar en el archivo appsettings.json. en la llave urlDescuentos.

## Patrones y Arquitectura
- **Patrón Singleton**: Para la utilización de los servicios de descuento y estado.
- **Patrón Repositorio**: Para la abstracción de la capa de acceso a datos.
- **Principios SOLID**: Para asegurar un código mantenible y escalable.
- **TDD (Test-Driven Development)**: Para asegurar la calidad del código mediante pruebas unitarias.


## Configuración y Ejecución
1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/avilesedward/PruebaNet.git
   cd PruebaNet

1. **Configurar la base de datos**:
Actualiza la cadena de conexión en appsettings.json.
Ejecutar migraciones:
dotnet ef database update

Ejecutar la aplicación:
dotnet run

## Documentación de la API:
La documentación de la API está disponible en http://localhost:5011/swagger/.
