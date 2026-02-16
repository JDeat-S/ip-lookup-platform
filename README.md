# Plataforma IP Lookup 

Aplicación Full Stack desarrollada como prueba técnica para consultar información geográfica de cualquier dirección IP y almacenar el histórico de consultas.

---

## Tecnologías utilizadas

### Backend
- .NET 8 Web API
- SQL Server
- Entity Framework Core
- Arquitectura por capas (Controller → Service → Repository)

### Frontend
- React (Vite)
- Axios
- Bootstrap 5
- Google Maps (iframe)

---

## Funcionalidades

-Consulta de IP (IPv4 / IPv6)  
-Validación en frontend y backend  
-Integración con API externa (ipwho.is)  
-Persistencia en base de datos  
-Histórico de consultas  
-Filtro dinámico por IP
-Eliminación de registros  
-Nivel de amenaza dinámico (Low / Medium / High)  
-Visualización geográfica en mapa  

---

# Arquitectura

El backend implementa una arquitectura por capas.

Esto permite separación de responsabilidades y código mantenible.

---

# ⚙ Cómo ejecutar el proyecto

# Backend

1. Configurar cadena de conexión en `appsettings.json`
2. Ejecutar:

```bash
dotnet run
```
# Frontend
1. npm install
2. npm run dev

# API externa utilizada
https://ipwho.is