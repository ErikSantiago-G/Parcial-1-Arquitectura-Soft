# Parcial 1 — Arquitectura de Software

## Sistema de Preaprobación de Crédito

Aplicación desarrollada en **C# y .NET** que permite evaluar una solicitud de crédito utilizando información financiera del solicitante y su puntaje registrado en una Central de Riesgo.

El proyecto fue desarrollado aplicando conceptos de **arquitectura en capas, principios SOLID, inversión de dependencias y pruebas unitarias**.

---

## Objetivo

Construir una aplicación capaz de determinar si una solicitud de crédito es **aprobada o rechazada**, teniendo en cuenta:

* Tipo y número de documento.
* Ingresos totales.
* Egresos totales.
* Monto solicitado.
* Plazo solicitado.
* Puntaje obtenido de la Central de Riesgo.

La aplicación separa las responsabilidades en diferentes capas para facilitar el mantenimiento, las pruebas y el reemplazo de componentes.

---

# Arquitectura del proyecto

La solución está dividida en cinco proyectos:

```text
ParcialCentralRiesgo
│
├── CapaNegocio
├── CapaAccesoDatos
├── DatosSimulados
├── CapaPresentacion
└── PruebasUnitarias
```

### Diagrama de dependencias

```text
                 ┌─────────────────────┐
                 │  CapaPresentacion   │
                 │    Windows Forms    │
                 └──────────┬──────────┘
                            │
                            ▼
                 ┌─────────────────────┐
                 │    CapaNegocio     │
                 │                     │
                 │ Reglas de negocio   │
                 │ Interfaces          │
                 │ Modelos             │
                 └──────────┬──────────┘
                            │
                   IAccesoCentralRiesgo
                            │
                 ┌──────────┴──────────┐
                 │                     │
                 ▼                     ▼
       ┌──────────────────┐   ┌──────────────────┐
       │ CapaAccesoDatos  │   │ DatosSimulados   │
       │                  │   │                  │
       │ SQL Server       │   │ Memoria          │
       └──────────────────┘   └──────────────────┘

                 ┌──────────────────┐
                 │ PruebasUnitarias │
                 │                  │
                 │ Usa Datos        │
                 │ Simulados        │
                 └──────────────────┘
```

La capa de negocio no depende directamente de SQL Server ni de la capa de presentación. La comunicación con la Central de Riesgo se realiza mediante la interfaz `IAccesoCentralRiesgo`.

Esto permite cambiar la fuente de datos sin modificar la lógica principal del negocio.

---

# Proyectos

## 1. CapaNegocio

Contiene la lógica principal de la aplicación.

### Interfaces

```text
Interfaces/
├── IAccesoCentralRiesgo.cs
└── IServicioCredito.cs
```

`IAccesoCentralRiesgo` define el contrato que debe cumplir cualquier componente encargado de consultar el puntaje de la Central de Riesgo.

`IServicioCredito` define el contrato del servicio encargado de evaluar una solicitud de crédito.

### Modelos

```text
Modelos/
├── SolicitudCredito.cs
└── ResultadoCredito.cs
```

`SolicitudCredito` representa los datos ingresados por el usuario.

`ResultadoCredito` representa el resultado generado después de aplicar las reglas de negocio.

### Servicios

```text
Servicios/
└── ServicioCredito.cs
```

`ServicioCredito` contiene las reglas necesarias para determinar si una solicitud cumple las condiciones para ser aprobada.

---

## 2. CapaAccesoDatos

Contiene la implementación encargada de consultar la Central de Riesgo utilizando **SQL Server / LocalDB**.

La clase de acceso a datos implementa:

```csharp
IAccesoCentralRiesgo
```

De esta manera, la capa de negocio no necesita conocer detalles relacionados con:

* `SqlConnection`
* `SqlCommand`
* Consultas SQL
* Conexiones a la base de datos

La responsabilidad de estas operaciones queda aislada en esta capa.

---

## 3. DatosSimulados

Contiene una implementación de `IAccesoCentralRiesgo` que trabaja con datos almacenados en memoria.

Esta capa se utiliza principalmente para las **pruebas unitarias**, evitando que las pruebas dependan de una base de datos real.

Ejemplo:

```text
CC-123456 → 850
CC-111111 → 750
CC-222222 → 650
CC-333333 → 450
CC-444444 → 300
```

Esto permite controlar fácilmente el puntaje utilizado durante cada prueba.

---

## 4. CapaPresentacion

Aplicación desarrollada con **Windows Forms**.

Permite al usuario ingresar los datos necesarios para realizar la evaluación:

* Tipo de documento.
* Número de documento.
* Ingresos totales.
* Egresos totales.
* Monto solicitado.
* Plazo solicitado.

Después de realizar la evaluación, la aplicación muestra mediante un `MessageBox` si el crédito fue aprobado o rechazado.

La presentación no contiene las reglas de negocio. Su responsabilidad principal es recibir los datos del usuario, llamar al servicio correspondiente y mostrar el resultado.

---

## 5. PruebasUnitarias

Contiene las pruebas unitarias de la lógica de negocio.

Las pruebas utilizan `DatosSimulados` en lugar de una base de datos real.

Esto permite verificar diferentes escenarios de aprobación y rechazo de manera independiente y repetible.

---

# Reglas de negocio

La evaluación de crédito sigue las siguientes reglas:

### 1. Validación del plazo

El plazo solicitado debe estar entre:

```text
1 y 72 meses
```

Si el plazo es menor a 1 o mayor a 72 meses, la solicitud es rechazada.

---

### 2. Cálculo del balance

El balance se obtiene mediante:

```text
Balance = Ingresos Totales - Egresos Totales
```

Si el balance es cero o negativo, la solicitud es rechazada.

---

### 3. Cálculo de la relación Crédito/Balance

La relación se calcula como:

```text
Relación = (Monto Solicitado / Plazo Solicitado) / Balance
```

La relación determina el puntaje mínimo necesario en la Central de Riesgo.

---

### 4. Relación igual o superior a 0.95

Si:

```text
Relación >= 0.95
```

la solicitud es rechazada.

---

### 5. Relación superior a 0.7 e inferior a 0.95

Si:

```text
0.7 < Relación < 0.95
```

se consulta la Central de Riesgo y el puntaje debe ser:

```text
>= 800
```

---

### 6. Relación superior a 0.4 y menor o igual a 0.7

Si:

```text
0.4 < Relación <= 0.7
```

el puntaje mínimo requerido es:

```text
>= 600
```

---

### 7. Relación menor o igual a 0.4

Si:

```text
Relación <= 0.4
```

el puntaje mínimo requerido es:

```text
>= 400
```

---

# Principios SOLID aplicados

## Single Responsibility Principle — SRP

Cada componente tiene una responsabilidad específica.

```text
CapaNegocio
    → Reglas de negocio

CapaAccesoDatos
    → Acceso a SQL Server

DatosSimulados
    → Datos para pruebas

CapaPresentacion
    → Interacción con el usuario

PruebasUnitarias
    → Validación automática de la lógica
```

---

## Open/Closed Principle — OCP

La aplicación permite agregar nuevas implementaciones de `IAccesoCentralRiesgo` sin modificar `ServicioCredito`.

Por ejemplo:

```text
IAccesoCentralRiesgo
       │
       ├── CapaAccesoDatos
       ├── DatosSimulados
       └── Nueva implementación
```

---

## Dependency Inversion Principle — DIP

La lógica de negocio depende de la abstracción:

```csharp
IAccesoCentralRiesgo
```

y no directamente de:

```csharp
CapaAccesoDatos
```

Esto permite utilizar tanto la implementación real de SQL Server como la implementación simulada en memoria.

---

# Tecnologías utilizadas

* **C#**
* **.NET**
* **Windows Forms**
* **SQL Server LocalDB**
* **Microsoft.Data.SqlClient**
* **xUnit / pruebas unitarias**
* **Git**
* **GitHub**

---

# Requisitos

Para ejecutar el proyecto se requiere:

* .NET SDK compatible con la solución.
* Visual Studio con soporte para C# y Windows Forms.
* SQL Server LocalDB para ejecutar la aplicación utilizando la capa de acceso a datos.
* La base de datos configurada según el script correspondiente.

Las pruebas unitarias utilizan datos simulados y no requieren conexión a SQL Server.

---

# Base de datos

La aplicación utiliza una base de datos denominada:

```text
BD_CENTRAL_RIESGO
```

Tabla:

```sql
CREATE DATABASE BD_CENTRAL_RIESGO;
GO

USE BD_CENTRAL_RIESGO;
GO

CREATE TABLE CentralRiesgo
(
    TipoDoc VARCHAR(5) NOT NULL,
    NroDoc VARCHAR(20) NOT NULL,
    Puntaje INT NOT NULL,
    PRIMARY KEY (TipoDoc, NroDoc)
);
GO
```

Ejemplo de registro:

```sql
INSERT INTO CentralRiesgo
VALUES ('CC', '123456', 850);
```

---

# Ejecución

## Restaurar dependencias

```bash
dotnet restore
```

## Compilar la solución

```bash
dotnet build
```

## Ejecutar las pruebas

```bash
dotnet test
```

## Ejecutar la aplicación

Desde Visual Studio se debe seleccionar `CapaPresentacion` como proyecto de inicio y ejecutar la aplicación.

---

# Flujo de funcionamiento

```text
Usuario
   │
   ▼
CapaPresentacion
   │
   ▼
ServicioCredito
   │
   ├── Valida plazo
   ├── Calcula balance
   ├── Calcula relación
   ├── Determina puntaje requerido
   │
   ▼
IAccesoCentralRiesgo
   │
   ├── CapaAccesoDatos → SQL Server
   │
   └── DatosSimulados → Memoria
   │
   ▼
ResultadoCredito
   │
   ▼
CapaPresentacion
   │
   ▼
Aprobado / Rechazado
```

---

# Autor

**Erik Santiago García González**

Proyecto académico — Parcial 1 de Arquitectura de Software.
