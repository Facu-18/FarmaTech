# FarmaTech - Entregables de Modelado de Sistemas

Cliente: Farmacia Ternavasio (Parque Liceo 2da sección, Av. Alfonsina Storni 835). Entrevistada: Alicia Ternavasio, propietaria.

## Índice

| Archivo | Contenido |
|---|---|
| [01-der.md](01-der.md) | Diagrama Entidad-Relación final (7 tablas), relaciones y correspondencia con la implementación. |
| [02-diagrama-casos-de-uso.md](02-diagrama-casos-de-uso.md) | Diagrama de casos de uso, tabla de actores y objetivos, elementos eliminados. |
| [03-preguntas-al-cliente.md](03-preguntas-al-cliente.md) | Documento de entrevista: resumen, preguntas y respuestas, conclusiones, visión y alcance, preguntas pendientes. |
| [04-caso-de-uso-registrar-venta.md](04-caso-de-uso-registrar-venta.md) | Caso de uso "Registrar Venta" corregido. |
| [05-diagrama-secuencia-registrar-venta.md](05-diagrama-secuencia-registrar-venta.md) | Diagrama de secuencia del sistema (análisis) y de diseño (endpoints reales). |

Los diagramas usan Mermaid (se visualizan en GitHub y VS Code) y pueden exportarse a PDF.

## Cambios respecto a la versión anterior

- **Producto ausente del mostrador vs. ausente del catálogo.** El flujo alternativo "El producto no existe en el catálogo -> se agrega a la nota de pedido" contradecía el DER final: `nota_pedido` solo referencia un producto mediante `id_producto` y no tiene campo de texto libre, por lo que un producto fuera del catálogo no puede registrarse. Además, la entrevista habla de productos **no disponibles** ("si no está, no está"), no de productos inexistentes. Ahora hay dos flujos distintos:
  - No disponible en el mostrador: se agrega a la nota de pedido desde la misma pantalla de búsqueda (un paso adicional).
  - No existe en el catálogo: el sistema informa que no fue encontrado y la empleada avisa a la encargada para que lo dé de alta en "Gestionar productos (ABM)".
- La venta puede contener varios productos (DER: venta 1 a N detalle_venta), por lo que el flujo principal repite los pasos de búsqueda y cantidad por cada producto.
- La búsqueda es por nombre (parcial) o código.
- Se agrega la precondición de inicio de sesión con usuario y PIN, y los roles Empleada y Encargada del DER.
- Se incorpora el diagrama de secuencia de diseño con los endpoints reales de la API.
