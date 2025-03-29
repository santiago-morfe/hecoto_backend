# 🚀 Hecoto Backend

Backend API desarrollado en .NET 8 con arquitectura por capas, contenedores Docker y control de versiones con Git.

[![.NET](https://img.shields.io/badge/.NET-8.0-%23512bd4)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-✓-blue)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

---

## 📌 Características Principales
- Arquitectura por capas (API, Application, Domain, Infrastructure).
- Inyección de dependencias (DI).
- Entity Framework Core para gestión de base de datos.
- Contenedorización con Docker.
- Pruebas unitarias con xUnit.

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

