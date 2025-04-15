# 🚀 Hecoto Backend

Backend API desarrollado en .NET 8 con arquitectura por capas, contenedores Docker y control de versiones con Git.

[![.NET](https://img.shields.io/badge/.NET-8.0-%23512bd4)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-✓-blue)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

---

## 📌 Características Principales

- **Arquitectura por capas**: Separación en capas (API, Application, Domain, Infrastructure) para una mejor organización y mantenibilidad.
- **Inyección de dependencias (DI)**: Uso de servicios y repositorios inyectados para desacoplar componentes.
- **Autenticación y Autorización**: Implementación de JWT para la autenticación de usuarios.
- **Entity Framework Core**: Gestión de base de datos con soporte para PostgreSQL.
- **Contenedorización con Docker**: Configuración lista para ejecutar en contenedores Docker.
- **Pruebas unitarias**: Configuración de pruebas con xUnit y Coverlet para cobertura de código.
- **Configuración flexible**: Uso de `appsettings.json` y secretos de usuario para manejar configuraciones sensibles.

---

## 🛠️ Estructura del Proyecto

```bash
hecoto_backend/
├── API/             # Capa de Presentación (WebAPI)
├── Application/     # Lógica de negocio y servicios
├── Domain/          # Entidades e interfaces
├── Infrastructure/  # Implementaciones de repositorios y EF Core
├── Tests/           # Pruebas unitarias
├── docker-compose.yml  # Configuración de Docker
└── HecotoBackend.sln   # Solución .NET
```

---

## 📋 Prerrequisitos

Antes de comenzar, asegúrate de tener instalados los siguientes componentes:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)
- Editores recomendados: [VS Code](https://code.visualstudio.com/), [Visual Studio 2022](https://visualstudio.microsoft.com/es/vs/)

---

## 🚀 Instalación

Clona el repositorio y restaura las dependencias del proyecto:

```bash
git clone https://github.com/santiago-morfe/hecoto_backend
cd hecoto_backend

dotnet restore HecotoBackend.sln
```

---

## ⚙️ Configuración

### 1️⃣ Copiar archivo de configuración de desarrollo:
```bash
cp API/appsettings.json API/appsettings.Development.json
```
Asegúrate de configurar la conexión a la base de datos en `API/appsettings.Development.json`.

### 2️⃣ Configurar variables sensibles con Secretos de .NET:
```bash
cd API
dotnet user-secrets init
dotnet user-secrets set "JWT:Key" "clave-secreta"
```

---

## 🏃 Ejecutar la Aplicación

### 🔹 Opción 1: Usando Docker

```bash
docker-compose up --build
```

La API estará disponible en: [http://localhost:5000/swagger](http://localhost:5000/swagger)

### 🔹 Opción 2: Sin Docker

```bash
cd API
dotnet run
```

La API estará disponible en: [http://localhost:5283/swagger](http://localhost:5283/swagger)

---

## 🧪 Ejecutar Pruebas

Para ejecutar las pruebas unitarias, usa:
```bash
dotnet test
```

---

## 🐳 Despliegue con Docker

### 1️⃣ Construir la imagen:
```bash
docker build -t hecoto-backend -f API/Dockerfile .
```

### 2️⃣ Ejecutar el contenedor:
```bash
docker run -p 5000:80 hecoto-backend
```

---

## ✨ Funcionalidades Actuales

### Autenticación y Autorización
- **Inicio de sesión**: Los usuarios pueden autenticarse mediante credenciales (usuario y contraseña) para obtener un token JWT.
- **Roles de usuario**: Soporte para roles (`Admin`, `User`) definidos en la enumeración `UserRole`.

### Gestión de Usuarios
- **Registro de usuarios**: Implementación de un servicio para registrar nuevos usuarios con contraseñas encriptadas.
- **Actualización y eliminación**: Métodos para actualizar y eliminar usuarios (pendiente de implementación en controladores).

### Seguridad
- **Hashing de contraseñas**: Uso de BCrypt para almacenar contraseñas de forma segura.
- **Tokens de actualización (Refresh Tokens)**: Gestión de tokens de actualización para sesiones prolongadas.

### Base de Datos
- **PostgreSQL**: Configuración de conexión a base de datos mediante Entity Framework Core.
- **Migraciones**: Soporte para migraciones de base de datos (pendiente de implementación).

### API REST
- **Controladores**: Controlador `AuthController` para manejar la autenticación.
- **Documentación Swagger**: Generación automática de documentación para la API.

---

## 🤝 Contribuir

Si deseas contribuir a este proyecto, sigue estos pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama para tu funcionalidad:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. Realiza tus cambios y haz un commit:
   ```bash
   git commit -m "feat: Añade nueva funcionalidad"
   ```
4. Sube los cambios a tu fork:
   ```bash
   git push origin feature/nueva-funcionalidad
   ```
5. Abre un Pull Request en GitHub.

---

¡Gracias por contribuir a **Hecoto Backend**! 🎉

