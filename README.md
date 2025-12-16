# 🧰 Zettium Servicios
**Sistema web para la gestión integral de servicios técnicos**: presupuestos, obras, visitas, catálogo de ítems y clientes, pensado para rubros como **gas, electricidad, refrigeración, energía solar, plomería** y más.

> Enfoque: **eficiencia operativa**, **trazabilidad**, y **experiencia de uso** (flujo rápido, filtros en vivo, PDF claros para el cliente).

---

## 🚀 Qué resuelve
Zettium centraliza en una sola herramienta:
- **Clientes** y su historial de trabajos
- **Presupuestos** con cálculo automático (ítems + mano de obra)
- **Obras** con seguimiento y estados
- **Visitas técnicas** (agenda, diagnóstico, estados)
- **Catálogo** de materiales/servicios con actualización masiva de precios

---

## ✨ Features principales

### 👤 Clientes
- ABM completo (alta/edición/baja lógica)
- Búsqueda en vivo por **nombre/apellido** y **localidad**
- **Papelera**: restauración y eliminación definitiva

### 🧾 Catálogo de Ítems (materiales/servicios)
- ABM de ítems con: **precio**, **unidad/medida**, **descripción**, **fabricante**, **marca**, **fecha de actualización**
- Filtros dinámicos:
  - Por **nombre / descripción**
  - Por **fabricante**
  - Por **marca**
- **Actualización masiva de precios** por marca (o todas) con porcentaje ±
- Papelera con restauración/eliminación definitiva

### 💰 Presupuestos
- Creación/edición con:
  - Cliente asociado
  - Rubro (gas/refrigeración/electricidad/etc.)
  - Opción de pago
  - Materiales incluidos (sí/no)
  - Mano de obra, tiempo estimado, validez, observaciones
- Selector de ítems con filtros (nombre/marca/fabricante) y recálculo automático de:
  - Subtotales
  - Total de ítems
  - Total final (mano de obra + ítems)
- **“+ Nuevo Ítem”** dentro del presupuesto (sin cortar el flujo)
- **PDF** de presupuesto (logo, cliente, tabla de ítems, totales)
- Estados visuales (Pendiente / Aceptado)
- Papelera + validación para impedir eliminar presupuestos vinculados a obras

### 🏗️ Obras
- Alta de obra desde un presupuesto aceptado
- Vista de presupuestos del cliente ordenados (más nuevos → más viejos) con rubro, total y fecha
- Campos:
  - Estado: Iniciada / En Proceso / Finalizada
  - Fecha de inicio
  - Seguimiento de materiales (quién compra / entregados)
  - Comentarios y notas
- **Calendario visual** en Inicio: obras activas por día con accesos rápidos a cliente/presupuesto/materiales

### 🗓️ Visitas técnicas
- ABM de visitas: mantenimiento/reparación/relevamiento/instalación
- Selección de cliente + fecha/hora con picker
- Datos: dirección, equipo, tipo, costo estimado, diagnóstico/observaciones
- Estados: Pendiente / Completada / Cancelada / Reprogramada
- Filtros por cliente, tipo y estado
- Papelera para recuperación

### 🗑️ Papelera (recuperación de datos)
Módulo transversal para:
- Clientes
- Ítems
- Presupuestos

Cada entidad incluye:
- Baja lógica → papelera
- Restauración
- Eliminación definitiva

### 📊 Reportes (orientados a negocio)
- Listados de presupuestos (aceptados / rechazados / pendientes)
- Estado de obras
- Resúmenes claros (prioridad: **decisiones rápidas**, no gráficos complejos)

---

## 🏗️ Arquitectura y diseño
El proyecto está desarrollado con **separación por capas** y principios de **CLEAN Architecture**.

**Proyectos / Capas**
- `Zetta.BD` → Entidades + EF Core (contexto) + repositorios
- `Zetta.Server` → API REST (.NET 8): controladores, validaciones, mapeos DTO
- `Zetta.Shared` → DTOs y contratos compartidos
- `Zetta.Client` → Frontend **Blazor WebAssembly** (páginas, componentes, servicios HTTP)

**Patrones y prácticas**
- Repositorio (genérico + específicos)
- DTOs + AutoMapper para desacople
- Validaciones con Data Annotations + lógica en servicios/controladores
- Código preparado para evolución (multiusuario, autenticación, etc.)

---

## 🧪 Flujo de uso (resumen)
### De cero a una obra en ejecución
1. Crear cliente  
2. Cargar ítems de catálogo  
3. Crear presupuesto (ítems + mano de obra + validez)  
4. Generar PDF y compartir  
5. Al aceptar, crear obra vinculada  
6. Seguimiento desde Obras + calendario

### Post-obra
- Agendar visitas técnicas
- Registrar diagnóstico, costo y estado
- Consultar historial del cliente (presupuestos/obras/visitas)

---

## 👥 Autores
- **Andrés Zanetta**
- **Leonardo Contreras**

Contacto:
- Leonardo Contreras: `leo8292014@gmail.com`
- Andrés Zanetta: `andresnicolaszanetta@gmail.com`

---

## 📄 Documentación (a solicitud)
El proyecto cuenta con:
- **Manual de Desarrollo**: arquitectura, decisiones técnicas, patrones, estructura por capas, entrevistas y requerimientos.
- **Manual de Usuario**: paso a paso con capturas (clientes, ítems, presupuestos, obras, visitas, papelera y filtros).

---
