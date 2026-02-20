# 🚀 Guía de Despliegue - SisPrestamos

## Despliegue en Diferentes Plataformas

### 1. Despliegue Local (Desarrollo)

```bash
# Clonar el repositorio
git clone <repo-url>
cd SisPrestamos

# Restaurar paquetes
dotnet restore

# Ejecutar la aplicación
dotnet run

# La aplicación estará disponible en:
# https://localhost:5001
```

---

### 2. Despliegue en Azure Static Web Apps

#### Requisitos
- Cuenta de Azure
- Azure CLI instalado
- Git

#### Pasos

1. **Publicar la aplicación**
```bash
dotnet publish -c Release -o bin/Release/net7.0/publish/wwwroot
```

2. **Crear recurso en Azure Portal**
   - Ir a "Static Web Apps"
   - Click en "Create"
   - Seleccionar repositorio GitHub
   - Configurar build settings:
     - Build preset: Blazor
     - App location: `bin/Release/net7.0/publish/wwwroot`
     - Build location: (dejar vacío para no compilar en Azure)

3. **Configurar GitHub Actions**
   - Azure creará automáticamente un workflow
   - Commit cambios para trigger automático
   - Verificar deployment en Azure Portal

#### URL de Ejemplo
```
https://<nombre-app>.azurestaticapps.net
```

---

### 3. Despliegue en GitHub Pages

#### Requisitos
- Repositorio en GitHub
- Actions habilitadas

#### Pasos

1. **Crear workflow**
   Crear archivo `.github/workflows/deploy.yml`:

```yaml
name: Deploy to GitHub Pages

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 7.0.x
    
    - name: Publish
      run: dotnet publish -c Release -o publish
    
    - name: Deploy to GitHub Pages
      uses: peaceiris/actions-gh-pages@v3
      with:
        github_token: ${{ secrets.GITHUB_TOKEN }}
        publish_dir: ./publish/wwwroot
        base_path: /SisPrestamos
```

2. **Configurar Pages**
   - Ir a Settings → Pages
   - Source: Deploy from a branch
   - Branch: gh-pages / root

3. **URL de Ejemplo**
```
https://username.github.io/SisPrestamos
```

---

### 4. Despliegue en Firebase Hosting

#### Requisitos
- Cuenta Google/Firebase
- Firebase CLI instalado

#### Pasos

1. **Inicializar Firebase**
```bash
firebase init hosting
```

2. **Publicar aplicación**
```bash
dotnet publish -c Release -o bin/Release/net7.0/publish/wwwroot
```

3. **Copiar archivos**
```bash
cp -r bin/Release/net7.0/publish/wwwroot/* public/
```

4. **Desplegar**
```bash
firebase deploy
```

#### URL de Ejemplo
```
https://<project-id>.firebaseapp.com
```

---

### 5. Despliegue en Vercel

#### Requisitos
- Cuenta Vercel
- GitHub conectado

#### Pasos

1. **Crear configuración Vercel** (`vercel.json`):
```json
{
  "buildCommand": "dotnet publish -c Release -o out",
  "outputDirectory": "out/wwwroot",
  "framework": "blazor"
}
```

2. **Conectar repositorio**
   - Ir a vercel.com
   - Import Project
   - Seleccionar repositorio GitHub
   - Vercel detectará Blazor automáticamente

3. **Desplegar**
   - Push a main branch
   - Vercel deployará automáticamente

#### URL de Ejemplo
```
https://sisprestamos.vercel.app
```

---

### 6. Despliegue con Docker

#### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out/wwwroot .
EXPOSE 80
CMD ["dotnet", "serve"]
```

#### Build y run
```bash
# Build imagen
docker build -t sisprestamos:latest .

# Run contenedor
docker run -p 80:80 sisprestamos:latest
```

---

### 7. Despliegue en IIS (Windows Server)

#### Requisitos
- Windows Server con IIS
- .NET 7 Runtime

#### Pasos

1. **Publicar aplicación**
```bash
dotnet publish -c Release -o bin/Release/net7.0/publish/wwwroot
```

2. **Copiar archivos a servidor**
```bash
# Copiar carpeta \wwwroot a C:\inetpub\wwwroot\sisprestamos
```

3. **Configurar IIS**
   - Abrir IIS Manager
   - Crear nuevo Application Pool (.NET v4.0, Integrated)
   - Crear nuevo Website o Virtual Directory
   - Asignar Application Pool
   - Configurar permisos NTFS

4. **Acceder**
```
http://servidor/sisprestamos
```

---

### 8. Despliegue en Netlify

#### Pasos

1. **Conectar repositorio**
   - Ir a netlify.com
   - New site from Git
   - Seleccionar repositorio

2. **Configurar build**
   - Build command: `dotnet publish -c Release -o publish`
   - Publish directory: `publish/wwwroot`

3. **Deploy**
   - Netlify deployará automáticamente

#### URL de Ejemplo
```
https://sisprestamos.netlify.app
```

---

## Configuraciones Pre-Despliegue

### Optimización de Performance

1. **Minificación**
```bash
# CSS
dotnet tool install -g MinifyCSS

# JavaScript
npm install -g terser
```

2. **Configurar caché**
   En `web.config` o equivalent:
```xml
<caching>
  <staticContent>
    <clientCache cacheControlMode="UseMaxAge" cacheControlMaxAge="30.00:00:00" />
  </staticContent>
</caching>
```

3. **Habilitar compresión**
   - GZIP/Brotli en servidor

### Seguridad

1. **Headers de seguridad**
```html
<!-- index.html -->
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<meta name="theme-color" content="#007bff" />
```

2. **CORS** (si necesitas API externa)
```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
```

3. **HTTPS Requerido**
   - Siempre usar HTTPS en producción
   - Obtener certificado SSL/TLS

### Monitoreo

1. **Application Insights** (Azure)
```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

2. **Logging**
```csharp
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);
```

---

## Checklist Pre-Despliegue

- [ ] Compilación exitosa (`dotnet build`)
- [ ] Sin errores de compilación
- [ ] Tests pasando
- [ ] Datos de ejemplo funcionan
- [ ] Funcionalidad offline probada
- [ ] Service Worker registrado correctamente
- [ ] manifest.json válido
- [ ] HTTPS habilitado
- [ ] Performance optimizado
- [ ] Documentación actualizada
- [ ] Variables de entorno configuradas
- [ ] Backup de datos implementado

---

## Rollback en Caso de Error

### Azure Static Web Apps
```bash
az staticwebapp environment list
az staticwebapp deploy --name <app-name> --environment-name staging
```

### GitHub Pages
```bash
git revert <commit-hash>
git push origin main
```

### Docker
```bash
docker pull sisprestamos:previous-tag
docker run -p 80:80 sisprestamos:previous-tag
```

---

## Variables de Entorno

Ejemplo para diferentes ambientes:

```csharp
// Development
appsettings.Development.json
// Staging
appsettings.Staging.json
// Production
appsettings.Production.json
```

---

## Soporte y Troubleshooting

### Error: "Service Worker failed to register"
- Verificar que `service-worker.js` existe
- Comprobar permisos de archivos
- Verificar console del navegador (F12)

### Error: "localStorage no disponible"
- Modo privado del navegador bloquea localStorage
- Informar al usuario
- Usar sessionStorage como fallback

### Performance lenta
- Reducir tamaño de la aplicación
- Habilitar compresión en servidor
- Usar CDN para assets estáticos
- Optimizar imágenes

---

## Contacto y Soporte

Para problemas de despliegue:
1. Revisar logs del servidor
2. Verificar networking y firewall
3. Contactar soporte de la plataforma
4. Crear issue en GitHub

---

**Última actualización**: 2024
**Versión**: 1.0.0
