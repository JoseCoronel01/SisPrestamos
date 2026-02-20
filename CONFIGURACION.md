# Configuración de SisPrestamos

## Estructura de carpetas esperada
```
SisPrestamos/
├── Model/                 # Entidades de datos
├── Service/               # Servicios de negocio y acceso a datos
├── Pages/                 # Páginas Blazor (.razor)
├── Components/            # Componentes compartidos
├── wwwroot/              # Archivos estáticos
│   ├── css/              # Estilos
│   ├── js/               # JavaScript
│   ├── index.html        # Punto de entrada HTML
│   ├── manifest.json     # Configuración PWA
│   └── service-worker.js # Service Worker
└── Program.cs            # Configuración de la aplicación
```

## Variables de Configuración de localStorage

La aplicación utiliza las siguientes claves en localStorage:

```javascript
// Clientes
localStorage['clientes'] = JSON.stringify([...])

// Líneas de Crédito
localStorage['lineas'] = JSON.stringify([...])

// Créditos
localStorage['creditos'] = JSON.stringify([...])

// Pagos
localStorage['pagos'] = JSON.stringify([...])
```

## Métodos de Interop con JavaScript

El archivo `LocalStorageService.cs` utiliza JSInterop para acceder a localStorage:

```csharp
// Obtener un valor
var valor = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

// Establecer un valor
await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);

// Eliminar un valor
await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);

// Limpiar todo
await _jsRuntime.InvokeVoidAsync("localStorage.clear");
```

## Inyección de Dependencias

Todos los servicios están registrados en `Program.cs` como Scoped:

```csharp
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<LineaService>();
builder.Services.AddScoped<CreditoService>();
builder.Services.AddScoped<PagoService>();
```

## Rutas de la Aplicación

- `/` - Dashboard principal
- `/clientes` - Gestión de clientes
- `/lineas` - Gestión de líneas de crédito
- `/creditos` - Gestión de créditos
- `/pagos` - Gestión de pagos

## Convenciones de Naming

- **Modelos**: PascalCase singular (Cliente, Linea, Credito, Pago)
- **Servicios**: PascalCase + "Service" (ClienteService, LineaService, etc.)
- **Propiedades privadas**: camelCase con prefijo "Lista" para colecciones
- **Métodos**: PascalCase (CargarDatos, GuardarCliente, etc.)
- **Interfaces**: PascalCase con prefijo "I" (ILocalStorageService)

## Estilos Bootstrap

La aplicación utiliza Bootstrap 5.3 con los siguientes esquemas de color:

- **Primary**: #007bff (Azul)
- **Success**: #28a745 (Verde)
- **Info**: #17a2b8 (Cian)
- **Warning**: #ffc107 (Amarillo)
- **Danger**: #dc3545 (Rojo)

## Validaciones

- Los formularios validan antes de guardar
- Se solicita confirmación antes de eliminar registros
- Se manejan excepciones en operaciones de localStorage
- Se validan IDs y referencias antes de operaciones

## Performance

- Carga de datos asíncrona
- Caching de datos en memoria
- Minimización de re-renders
- Uso eficiente de localStorage
- Service Worker para caché de assets

## Debugging

Para debuggear:
1. Abrir DevTools (F12)
2. Ir a la pestaña "Application" o "Storage"
3. Expandir "Local Storage"
4. Ver los datos almacenados en las claves de la app

## Notas Importantes

- **Datos Locales**: Todos los datos se almacenan en el navegador, no en un servidor
- **Privacidad**: Los datos no se envían a servidores externos
- **Backup**: Se recomienda exportar datos regularmente
- **Límite**: localStorage tiene un límite típico de 5-10MB por dominio
- **Persistencia**: Los datos persisten incluso después de cerrar el navegador

## Requisitos del Sistema

- .NET 7.0 SDK o superior
- Navegador con soporte para:
  - WebAssembly
  - Service Workers
  - localStorage API
  - Fetch API

## Compatibilidad de Navegadores

✅ Chrome 51+
✅ Firefox 52+
✅ Safari 15.4+
✅ Edge 15+
✅ Opera 38+

## Buenas Prácticas

1. Siempre usar async/await para operaciones de localStorage
2. Validar datos antes de guardar
3. Usar try-catch en ServiceWorker para manejo de errores
4. Mantener datos pequeños y normalizados
5. Limpiar datos obsoletos regularmente
