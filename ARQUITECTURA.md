/**
 * ============================================================================
 * ARQUITECTURA DE SisPrestamos - SISTEMA DE PRÉSTAMOS BANCARIOS
 * ============================================================================
 * 
 * Framework: Blazor WebAssembly .NET 7.0
 * Database: localStorage (JSON)
 * Server: No server (Client-side only)
 * 
 * ============================================================================
 */

// ============================================================================
// 1. CAPA DE PRESENTACIÓN (Pages)
// ============================================================================

Dashboard (Index.razor)
├── Muestra estadísticas en tiempo real
├── Total Clientes
├── Total Líneas de Crédito
├── Total Créditos Otorgados
├── Total Pagos Registrados
└── Saldo Pendiente

Gestión de Clientes (Clientes.razor)
├── CREATE: Formulario para agregar cliente
├── READ: Tabla con listado de clientes
├── UPDATE: Editar información del cliente
├── DELETE: Eliminar cliente con confirmación
└── Campos: id, name, phone, email, address, city, state, country

Gestión de Líneas (Lineas.razor)
├── CREATE: Asignar línea de crédito a cliente
├── READ: Tabla con listado de líneas
├── UPDATE: Modificar monto de línea
├── DELETE: Eliminar línea
└── Relación: Cliente → Línea

Gestión de Créditos (Creditos.razor)
├── CREATE: Otorgar crédito basado en línea
├── READ: Tabla con listado de créditos
├── UPDATE: Editar importe de crédito
├── DELETE: Eliminar crédito
└── Relación: Línea → Crédito

Gestión de Pagos (Pagos.razor)
├── CREATE: Registrar pago contra crédito
├── READ: Tabla con listado de pagos
├── UPDATE: Editar pago
├── DELETE: Eliminar pago
├── Filtros: Por cliente, por fecha
├── Resumen: Total pagos, cantidad
└── Relación: Crédito → Pago

// ============================================================================
// 2. CAPA DE LÓGICA DE NEGOCIO (Services)
// ============================================================================

ClienteService
├── GetAllAsync()          → Obtener todos los clientes
├── GetByIdAsync(id)       → Obtener cliente por ID
├── AddAsync(cliente)      → Crear nuevo cliente
├── UpdateAsync(cliente)   → Actualizar cliente
└── DeleteAsync(id)        → Eliminar cliente

LineaService
├── GetAllAsync()          → Obtener todas las líneas
├── GetByClienteIdAsync(id) → Obtener líneas de un cliente
├── GetByIdAsync(id)       → Obtener línea por ID
├── AddAsync(linea)        → Crear nueva línea
├── UpdateAsync(linea)     → Actualizar línea
└── DeleteAsync(id)        → Eliminar línea

CreditoService
├── GetAllAsync()          → Obtener todos los créditos
├── GetByLineaIdAsync(id)  → Obtener créditos de una línea
├── GetByIdAsync(id)       → Obtener crédito por ID
├── AddAsync(credito)      → Crear nuevo crédito
├── UpdateAsync(credito)   → Actualizar crédito
└── DeleteAsync(id)        → Eliminar crédito

PagoService
├── GetAllAsync()          → Obtener todos los pagos
├── GetByCreditoIdAsync(id)→ Obtener pagos de un crédito
├── GetByIdAsync(id)       → Obtener pago por ID
├── AddAsync(pago)         → Registrar nuevo pago
├── UpdateAsync(pago)      → Actualizar pago
└── DeleteAsync(id)        → Eliminar pago

// ============================================================================
// 3. CAPA DE ACCESO A DATOS (localStorage)
// ============================================================================

LocalStorageService
├── Implementa: ILocalStorageService
├── GetItemAsync(key)      → Obtener valor de localStorage
├── SetItemAsync(key, val) → Guardar en localStorage
├── RemoveItemAsync(key)   → Eliminar de localStorage
└── ClearAsync()           → Limpiar todo localStorage

Almacenamiento:
└── localStorage
    ├── clientes    → JSON array de Cliente
    ├── lineas      → JSON array de Linea
    ├── creditos    → JSON array de Credito
    └── pagos       → JSON array de Pago

// ============================================================================
// 4. MODELOS DE DATOS (Model)
// ============================================================================

Cliente
├── id: int                → Identificador único
├── name: string           → Nombre completo
├── phone: string          → Teléfono
├── email: string          → Correo electrónico
├── address: string        → Dirección
├── city: string           → Ciudad
├── state: string          → Estado/Provincia
└── country: string        → País

Linea
├── id: int                → Identificador único
├── clienteId: int         → FK a Cliente
├── monto: decimal         → Monto de línea
├── fecha: DateTime        → Fecha de creación
└── hora: TimeSpan         → Hora de creación

Credito
├── id: int                → Identificador único
├── lineaId: int           → FK a Linea
├── importe: decimal       → Importe del crédito
├── fecha: DateTime        → Fecha de creación
└── hora: TimeSpan         → Hora de creación

Pago
├── id: int                → Identificador único
├── creditoId: int         → FK a Credito
├── importe: decimal       → Importe pagado
├── fecha: DateTime        → Fecha del pago
└── hora: TimeSpan         → Hora del pago

// ============================================================================
// 5. COMPONENTES Y UTILIDADES
// ============================================================================

DataInitializer (Components)
└── InitializeDefaultDataAsync()
    └── Carga datos de ejemplo la primera ejecución

MainLayout (Shared)
├── Navbar con navegación
├── Links a todas las secciones
├── Footer con información
└── Body content (página actual)

// ============================================================================
// 6. CAPAS PWA Y OFFLINE
// ============================================================================

Service Worker (service-worker.js)
├── Cache First Strategy
├── Network Fallback
├── Actualización de caché
└── Offline support

Manifest (manifest.json)
├── Metadatos de aplicación
├── Iconos para instalación
├── Configuración de pantalla
└── Tema de color

// ============================================================================
// 7. FLUJO DE DATOS
// ============================================================================

User Action (UI)
    ↓
Event Handler (Razor Component)
    ↓
Service Method (CRUD)
    ↓
LocalStorageService (Get/Set)
    ↓
Browser localStorage (JSON)
    ↓
Component State Update
    ↓
Re-render (UI)

// ============================================================================
// 8. INYECCIÓN DE DEPENDENCIAS
// ============================================================================

Program.cs Registry:

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>()
builder.Services.AddScoped<ClienteService>()
builder.Services.AddScoped<LineaService>()
builder.Services.AddScoped<CreditoService>()
builder.Services.AddScoped<PagoService>()

Ciclo de vida: Scoped = por solicitud HTTP (equivalente a sesión de usuario)

// ============================================================================
// 9. RELACIONES ENTRE ENTIDADES
// ============================================================================

Cliente (1) ──────→ (N) Linea
            asigna línea

Linea (1) ──────────→ (N) Credito
          otorga crédito

Credito (1) ────────→ (N) Pago
            registra pago

// ============================================================================
// 10. CARACTERÍSTICAS Y FUNCIONALIDADES
// ============================================================================

✅ CRUD Completo (Create, Read, Update, Delete)
✅ Validación de formularios
✅ Confirmación antes de eliminar
✅ Filtros avanzados (por cliente, fecha)
✅ Dashboard con estadísticas
✅ Almacenamiento local (localStorage)
✅ Funciona sin conexión (PWA)
✅ Responsive design (Mobile & Desktop)
✅ Interfaz moderna con Bootstrap 5.3
✅ Inicialización automática de datos
✅ Cálculos automáticos (saldo pendiente)
✅ Fecha y hora automáticas

// ============================================================================
// 11. STACK TECNOLÓGICO
// ============================================================================

Frontend:
├── Blazor WebAssembly
├── C# 11
├── HTML5
├── CSS3
├── Bootstrap 5.3
└── JavaScript ES6+

Almacenamiento:
├── localStorage API
├── JSON serialization
└── Client-side persistence

PWA:
├── Service Worker
├── Web App Manifest
├── Cache First Strategy
└── Offline Support

Herramientas:
├── .NET 7.0 SDK
├── Visual Studio / VS Code
└── Git

// ============================================================================
// 12. LIMITACIONES Y CONSIDERACIONES
// ============================================================================

- localStorage limitado (~5-10MB por dominio)
- Datos no sincronizados entre pestañas
- Sin respaldo automático en servidor
- Depende del navegador del cliente
- Modo privado puede bloquear localStorage
- No hay encriptación nativa en localStorage

// ============================================================================
// 13. MEJORAS FUTURAS RECOMENDADAS
// ============================================================================

1. Agregar Backend con API REST
2. Implementar autenticación
3. Agregar base de datos (SQL Server/PostgreSQL)
4. Sincronización cloud
5. Generación de reportes PDF
6. Gráficos de análisis
7. Exportación a Excel
8. Notificaciones push
9. Búsqueda avanzada
10. Auditoría de cambios

// ============================================================================
// 14. RUTAS DE LA APLICACIÓN
// ============================================================================

GET /              → Dashboard
GET /clientes      → Gestión de Clientes
GET /lineas        → Gestión de Líneas
GET /creditos      → Gestión de Créditos
GET /pagos         → Gestión de Pagos

// ============================================================================
// 15. MÉTODOS HTTP SIMULADOS (localStorage)
// ============================================================================

GET:    localStorage.getItem(key)
POST:   localStorage.setItem(key, value)
PUT:    localStorage.setItem(key, value)
DELETE: localStorage.removeItem(key)

// ============================================================================
// FIN DE DOCUMENTACIÓN DE ARQUITECTURA
// ============================================================================
 */
