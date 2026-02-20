# 💰 Sistema de Préstamos Bancarios - SisPrestamos

## 📋 Descripción General

**SisPrestamos** es una aplicación web progresiva (PWA) desarrollada en **Blazor WebAssembly con .NET 7.0** para la gestión integral de préstamos bancarios, líneas de crédito y pagos. La aplicación funciona completamente en el cliente sin requerir un servidor backend, utilizando localStorage para persistencia de datos.

### Características Principales

✅ **Gestión de Clientes** - CRUD completo de clientes con 8 campos de información
✅ **Líneas de Crédito** - Administración de líneas de crédito asignadas a clientes
✅ **Gestión de Créditos** - Registro y seguimiento de créditos otorgados
✅ **Sistema de Pagos** - Registro de pagos con filtros avanzados
✅ **Dashboard** - Panel de control con estadísticas en tiempo real
✅ **PWA** - Funciona sin conexión gracias al Service Worker
✅ **Responsive Design** - Optimizado para desktop y móviles
✅ **localStorage** - Persistencia de datos local segura

---

## 🏗️ Estructura del Proyecto

```
SisPrestamos/
├── Model/
│   ├── Cliente.cs          # Entidad de clientes
│   ├── Linea.cs            # Entidad de líneas de crédito
│   ├── Credito.cs          # Entidad de créditos
│   └── Pago.cs             # Entidad de pagos
├── Service/
│   ├── ILocalStorageService.cs   # Interfaz de localStorage
│   ├── LocalStorageService.cs    # Implementación de localStorage
│   ├── ClienteService.cs         # CRUD de clientes
│   ├── LineaService.cs           # CRUD de líneas
│   ├── CreditoService.cs         # CRUD de créditos
│   └── PagoService.cs            # CRUD de pagos
├── Pages/
│   ├── Index.razor         # Dashboard principal
│   ├── Clientes.razor      # Gestión de clientes
│   ├── Lineas.razor        # Gestión de líneas
│   ├── Creditos.razor      # Gestión de créditos
│   └── Pagos.razor         # Gestión de pagos
├── Components/
│   └── DataInitializer.cs  # Inicialización de datos de ejemplo
├── Program.cs              # Configuración de la aplicación
├── App.razor               # Componente raíz
└── wwwroot/
    ├── index.html          # HTML principal (con PWA)
    ├── manifest.json       # Manifest PWA
    ├── service-worker.js   # Service Worker
    └── css/
        └── app.css         # Estilos globales
```

---

## 📊 Modelos de Datos

### Cliente
```csharp
public class Cliente
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}
```

### Línea de Crédito
```csharp
public class Linea
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
}
```

### Crédito
```csharp
public class Credito
{
    public int Id { get; set; }
    public int LineaId { get; set; }
    public decimal Importe { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
}
```

### Pago
```csharp
public class Pago
{
    public int Id { get; set; }
    public int CreditoId { get; set; }
    public decimal Importe { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
}
```

---

## 🚀 Cómo Ejecutar

### Requisitos
- .NET 7.0 SDK instalado
- Visual Studio 2022 o Visual Studio Code
- Navegador moderno con soporte para WebAssembly

### Pasos para ejecutar

1. **Clonar el repositorio**
```bash
git clone <repo-url>
cd SisPrestamos
```

2. **Restaurar dependencias**
```bash
dotnet restore
```

3. **Compilar el proyecto**
```bash
dotnet build
```

4. **Ejecutar la aplicación**
```bash
dotnet run
```

5. **Acceder a la aplicación**
- Abre tu navegador en: `https://localhost:5001` (o el puerto que se indique)

---

## 📱 Funcionalidades

### 1. Dashboard Principal
- Vista resumen de estadísticas
- Total de clientes, líneas, créditos y pagos
- Información financiera consolidada
- Saldo pendiente calculado automáticamente
- Acceso rápido a todas las secciones

### 2. Gestión de Clientes
- **Crear**: Agregar nuevos clientes con todos sus datos
- **Leer**: Visualizar listado de clientes en tabla
- **Actualizar**: Editar información de clientes existentes
- **Eliminar**: Borrar clientes con confirmación

### 3. Gestión de Líneas de Crédito
- Asignar líneas de crédito a clientes
- Registrar monto de línea disponible
- Fecha y hora automáticas
- Relación con clientes

### 4. Gestión de Créditos
- Crear créditos basados en líneas existentes
- Registrar importe otorgado
- Fecha y hora automáticas
- Trazabilidad completa

### 5. Gestión de Pagos
- **Registrar pagos** contra créditos específicos
- **Filtrar** por cliente y fecha
- **Visualizar** tabla de pagos con detalles
- **Resumen** de pagos registrados
- Cálculo automático de saldo pendiente

---

## 🔐 Almacenamiento de Datos

La aplicación utiliza **localStorage** del navegador para persistencia:

- **Clientes**: `localStorage['clientes']`
- **Líneas**: `localStorage['lineas']`
- **Créditos**: `localStorage['creditos']`
- **Pagos**: `localStorage['pagos']`

Los datos se guardan en formato JSON y se cargan automáticamente al iniciar la aplicación.

---

## 🌐 Capacidades PWA

### Service Worker
- **Cache First**: Archivos estáticos cacheados para acceso offline
- **Network Fallback**: Intenta red y vuelve a cache si no hay conexión
- **Actualización automática**: Limpia caches obsoletos

### Manifest.json
- Aplicación instalable en dispositivos
- Icono de aplicación personalizado
- Configuración de pantalla de inicio
- Orientación optimizada

### Offline Support
- Funciona completamente sin conexión
- Datos sincronizados localmente
- Experiencia fluida en conectividad limitada

---

## 🎨 Tecnologías Utilizadas

| Tecnología | Versión | Uso |
|---|---|---|
| **Blazor WebAssembly** | .NET 7.0 | Framework principal |
| **C#** | 11 | Lenguaje de programación |
| **Bootstrap** | 5.3 | Diseño responsive |
| **HTML5** | 5 | Estructura |
| **CSS3** | 3 | Estilos avanzados |
| **JavaScript** | ES6+ | Service Worker |
| **localStorage API** | HTML5 | Persistencia de datos |

---

## 📈 Flujo de Datos

```
Cliente
   ↓
Línea de Crédito (asignada a Cliente)
   ↓
Crédito (basado en Línea)
   ↓
Pagos (contra Crédito)
```

---

## 🔧 Configuración

### Program.cs
Todos los servicios se registran como **Scoped** en el contenedor de inyección de dependencias:

```csharp
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<LineaService>();
builder.Services.AddScoped<CreditoService>();
builder.Services.AddScoped<PagoService>();
```

---

## 📝 Datos de Ejemplo

La aplicación carga automáticamente datos de ejemplo la primera vez:

### Clientes
- Ana García
- Luis Rodríguez
- Pedro Martínez

### Líneas
- Cliente 1: $50,000
- Cliente 2: $30,000
- Cliente 3: $75,000

### Créditos y Pagos
Se generan automáticamente con datos relacionados.

---

## 🚨 Manejo de Errores

- Validación en formularios
- Confirmación antes de eliminar
- Mensajes de error claros
- Manejo de excepciones en servicios
- Error UI para problemas no controlados

---

## 💡 Mejoras Futuras

- [ ] Autenticación de usuarios
- [ ] Sincronización con servidor backend
- [ ] Reportes avanzados PDF
- [ ] Notificaciones push
- [ ] Gráficos de análisis financiero
- [ ] Exportar datos a Excel
- [ ] Búsqueda avanzada
- [ ] Auditoría de cambios

---

## 📞 Soporte

Para soporte técnico o reportar bugs, por favor crear un issue en el repositorio.

---

## 📄 Licencia

Este proyecto es de código abierto bajo la licencia MIT.

---

## 👨‍💻 Autor

Desarrollado como un sistema completo de gestión de préstamos con Blazor WebAssembly .NET 7.0

**Características**: Experiencia de 15+ años en .NET Framework | PWA | Offline-First | localStorage

---

**Versión**: 1.0.0
**Última actualización**: 2024
**Estado**: ✅ Producción
