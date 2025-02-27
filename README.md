# Administrador de empleados en .NET Core 8 y SQL Server  💻

Este proyecto es una aplicación ASP.NET Core 8 MVC que consume una API REST desarrollada en .NET Core 8 y utiliza SQL Server como base de datos. Implementa autenticación con Identity y permite la gestión de empleados.

## Tecnologías Utilizadas
- ASP.NET Core 8 (MVC y Web API)
- SQL Server
- Entity Framework Core
- Identity para autenticación
- IIS para el despliegue
- GitHub para control de versiones
- Librerias como: DataTables, JQuery y FontAwesome

## Requisitos Previos 📃
Antes de ejecutar la aplicación, asegúrese de tener instalado:
- .NET 8 SDK [Descargar](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Hosting Bundle .NET 8 [Descargar](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server y SQL Server Management Studio (SSMS) [Descargar](https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms)
- Visual Studio 2019 o superior [Descargar](https://visualstudio.microsoft.com/es/vs/)

## Instalación y Configuración 🚀
### 1. Clonar el Repositorio
```sh
git clone <URL_DEL_REPOSITORIO>
cd <NOMBRE_DEL_PROYECTO>
```

### 2. Configurar la Base de Datos
1. Abrir SQL Server Management Studio (SSMS).
2. Ejecutar el script `script.sql` ubicado en la carpeta `/Database` para crear la base de datos y la tabla `Empleado`.
3. Modificar `appsettings.json` con la cadena de conexión adecuada, ejemplo:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SERVIDOR;Database=NOMBRE_BD;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Migraciones y Datos Iniciales
Realizar la migración del IdentityDbContext para la creación de tablas para autorización de usuarios.
```sh
dotnet ef migrations add IdentityMigration --context IdentityDbContext
dotnet ef database update --context IdentityDbContext
```

### 4. Ejecutar la API
```sh
cd ApiProyecto
dotnet run
```
⚠️ Importante: La API requiere autenticación. Se debe generar un usuario y autenticarse antes de realizar peticiones.

### 5. Ejecutar la Aplicación MVC
```sh
cd MvcProyecto
dotnet run
```
⚠️ Importante: Debe registrar un empleado desde la interfaz para poder acceder al sistema.

### 6. Despliegue en IIS
1. Abrir `Panel de Control > Activar o desactivar las características de Windows` y habilitar `Internet Information Services (IIS)`.
2. Publicar el proyecto en una carpeta local usando:
```sh
dotnet publish -c Release -o C:\PublicacionProyecto
```
3. Configurar IIS:
   - Abrir IIS Manager (`inetmgr`).
   - Crear un nuevo sitio en `Sites` apuntando a `C:\PublicacionProyecto`.
   - Configurar el `Application Pool` con .NET 8.
   - Asegurar que la API y el sitio MVC sean accesibles desde el navegador.

## Referencias de la API
Esta API permite gestionar la información de los empleados, incluyendo la capacidad de listar, crear, actualizar y eliminar empleados.
La API está compuesta por los siguientes EndPoints:
- #### Listar todos los empleados

```
  GET /api/empleados
```

- #### Listar empleado por ID

```
  GET /api/empleados/{id}
```

| Parametro | Tipo     | Descripción                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int`    | **Requerido**. ID del empleado    |

- ### Crear un nuevo empleado

```
  POST /api/empleados
```
| Parametro   | Tipo      | Descripción                                                        |
| :---------- | :-------- | :----------------------------------------------------------------- |
| `Nombre`	  | `string`  | **Requerido**. Nombre del empleado.                                |
| `Apellido`	| `string`  | **Requerido**. Apellido del empleado.                              |
| `Correo`	  | `string`  | **Requerido**. El correo electrónico del empleado (debe ser único).|
| `Télefono`	| `string`  | **No Requerido** Número de telefono del empleado.                  |
| `Salario`  	| `decimal` | **Requerido**. Salario del empleado. (debe ser mayor a 350)        |

Nota: 
La información se envía por medio de un DTO. Este método verifica que el correo electrónico no esté en uso. No se envía el ID ni la fecha de ingreso, ya que se obtiene automáticamente.

- ### Actualizar un empleado

```
  PUT /api/empleados/{id}
```
| Parametro   | Tipo      | Descripción                                                        |
| :---------- | :-------- | :----------------------------------------------------------------- |
| `ID`	      | `int`     | **Autoincremental**. Identificador de empleado.                    |
| `Nombre`	  | `string`  | **Requerido**. Nombre del empleado.                                |
| `Apellido`	| `string`  | **Requerido**. Apellido del empleado.                              |
| `Correo`	  | `string`  | **Requerido**. El correo electrónico del empleado (debe ser único).|
| `Télefono`	| `string`  | **No Requerido** Número de telefono del empleado.                  |
| `Salario`  	| `decimal` | **Requerido**. Salario del empleado. (debe ser mayor a 350)        |

Nota: La información se envía por medio de un DTO. No se envía la fecha de ingreso ya que no debe modificarse.

-- ### Eliminar un empleado

```
  DELETE /api/empleados/{id} 
```
| Parametro   | Tipo      | Descripción                                                        |
| :---------- | :-------- | :----------------------------------------------------------------- |
| `ID`	      | `int`     | **Autoincremental**. Identificador de empleado.                    |


## Capturas de Pantalla (Opcional)
- Capturas de Pantalla proyecto Web
<a href="https://github.com/user-attachments/assets/e5e58332-39b5-406a-8529-6092c1c0dc7b" target="_blank">
  <img src="https://github.com/user-attachments/assets/e5e58332-39b5-406a-8529-6092c1c0dc7b" alt="Imagen" width="150"/>
</a>
<a href="https://github.com/user-attachments/assets/8fa8fb1f-ed9f-4746-a28b-443c325b87eb" target="_blank">
  <img src="https://github.com/user-attachments/assets/8fa8fb1f-ed9f-4746-a28b-443c325b87eb" alt="Imagen" width="150"/>
</a>
<a href="https://github.com/user-attachments/assets/5d94dc0d-b8ca-477c-9856-ce32334df291" target="_blank">
  <img src="https://github.com/user-attachments/assets/5d94dc0d-b8ca-477c-9856-ce32334df291" alt="Imagen" width="150"/>
</a>
<a href="https://github.com/user-attachments/assets/231574fe-2dec-46b8-8b01-4599a8004099" target="_blank">
  <img src="https://github.com/user-attachments/assets/231574fe-2dec-46b8-8b01-4599a8004099" alt="Imagen" width="150"/>
</a>
<a href="https://github.com/user-attachments/assets/02986b74-9eb9-485d-ba25-47893652f28c" target="_blank">
  <img src="https://github.com/user-attachments/assets/02986b74-9eb9-485d-ba25-47893652f28c" alt="Imagen" width="150"/>
</a>
<a href="https://github.com/user-attachments/assets/40d8265e-e293-4035-b333-4ffa6ca3026c" target="_blank">
  <img src="https://github.com/user-attachments/assets/40d8265e-e293-4035-b333-4ffa6ca3026c" alt="Imagen" width="150"/>
</a>

- Captura de Pantalla API Employee
<a href="https://github.com/user-attachments/assets/484d23ce-02b3-454e-b9f7-bb28f0383af6" target="_blank">
  <img src="https://github.com/user-attachments/assets/484d23ce-02b3-454e-b9f7-bb28f0383af6" alt="Imagen" width="150"/>
</a>

## Autor
Cesar Soto
