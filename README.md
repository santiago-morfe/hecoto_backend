# 🚀 Hecoto Backend

Backend API desarrollado en .NET 8 con arquitectura por capas, Docker y Git.

[![.NET](https://img.shields.io/badge/.NET-8.0-%23512bd4)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-✓-blue)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

## 📌 Características Principales
- Arquitectura por capas (API, Application, Domain, Infrastructure)
- Inyección de Dependencias (DI)
- Entity Framework Core
- Dockerizado para desarrollo y despliegue
- Pruebas unitarias con xUnit

## 🛠️ Estructura del Proyecto
hecoto_backend/
├── API/ # Capa de Presentación (WebAPI)
├── Application/ # Lógica de negocio y servicios
├── Domain/ # Entidades e interfaces
├── Infrastructure/ # Implementaciones de repositorios y EF Core
├── Tests/ # Pruebas unitarias
├── docker-compose.yml # Configuración de Docker
└── HecotoBackend.sln # Solución .NET

Copy

## 📋 Prerrequisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)
- Editor recomendado: [VS Code](https://code.visualstudio.com/)

## 🚀 Instalación
```bash
# Clonar repositorio
git clone https://github.com/tu-usuario/hecoto_backend.git
cd hecoto_backend

# Restaurar dependencias
dotnet restore HecotoBackend.sln
⚙️ Configuración
Copiar archivo de configuración de desarrollo:

bash
Copy
cp API/appsettings.json API/appsettings.Development.json
Configurar conexión a BD en API/appsettings.Development.json.

Variables sensibles (usar Secretos de .NET):

bash
Copy
cd API
dotnet user-secrets init
dotnet user-secrets set "JWT:Key" "clave-secreta"
🏃 Ejecutar la Aplicación
Opción 1: Con Docker
bash
Copy
# Construir y levantar contenedores
docker-compose up --build

# API disponible en: http://localhost:5000/swagger
Opción 2: Sin Docker
bash
Copy
cd API
dotnet run

# API disponible en: http://localhost:5283/swagger
🧪 Ejecutar Pruebas
bash
Copy
dotnet test
🐳 Despliegue con Docker
bash
Copy
# Construir imagen
docker build -t hecoto-backend -f API/Dockerfile .

# Ejecutar contenedor
docker run -p 5000:80 hecoto-backend
🤝 Contribuir
Haz un fork del proyecto

Crea tu rama: git checkout -b feature/nueva-funcionalidad

Haz commit: git commit -m "feat: Añade funcionalidad"

Sube cambios: git push origin tu-rama

Abre un Pull Request