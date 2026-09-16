# Consultar Preaprobación de Crédito

Aplicación de arquitectura en capas (SOLID) que evalúa si una solicitud de
crédito es aprobada o rechazada, según ingresos, egresos, monto y plazo
solicitados, consultando el puntaje del cliente en la Central de Riesgo.

## Arquitectura

| Proyecto | 
|---|---|
| `Modelo` | DTOs `SolicitudCredito` y `ResultadoCredito` |
| `Negocio` | Reglas de negocio (plazo, balanza, relación crédito/balanza, tramos de puntaje) y el orquestador `EvaluadorCredito`. Define el puerto `IConsultaCentralRiesgo` |
| `AccesoDatos` | Implementa `IConsultaCentralRiesgo` contra SQL Server / LocalDB |
| `DatosPruebas` | Implementa `IConsultaCentralRiesgo` en memoria, para pruebas |
| `Presentacion` | Formulario WinForms para ingresar los datos y ver el resultado en un alert |
| `PruebasUnitarias` | Pruebas xUnit de cada regla y del `EvaluadorCredito` completo |

`Negocio` no depende de `AccesoDatos` ni de `Presentacion`; ambos dependen de
`Negocio` (Inversión de Dependencias). Eso permite reemplazar la fuente de
datos (SQL Server ↔ memoria) sin tocar la lógica de negocio.

## Requisitos

- .NET SDK 8.0+
- SQL Server LocalDB (para ejecutar `Presentacion`; no es necesario para
  correr las pruebas, que usan `DatosPruebas`)

## Clonar y ejecutar

```bash
git clone https://github.com/ErikSantiago-G/Parcial-1-Arquitectura-Soft.git
cd Parcial-1-Arquitectura-Soft

# Restaurar dependencias
dotnet restore

# Correr las pruebas unitarias (no requiere base de datos)
dotnet test PruebasUnitarias

# Compilar y ejecutar la aplicación de escritorio (requiere LocalDB)
dotnet run --project Presentacion
```

## Base de datos

La tabla que consulta `AccesoDatos.ConsultaCentralRiesgo`:

```sql
CREATE DATABASE BD_CentralRiesgo;
GO
USE BD_CentralRiesgo;
GO
CREATE TABLE CentralRiesgo (
    TipoDoc VARCHAR(5)    NOT NULL,
    NroDoc  VARCHAR(20)   NOT NULL,
    Puntaje INT           NOT NULL,
    PRIMARY KEY (TipoDoc, NroDoc)
);
GO
INSERT INTO CentralRiesgo VALUES ('CC', '12234587', 563);
```
