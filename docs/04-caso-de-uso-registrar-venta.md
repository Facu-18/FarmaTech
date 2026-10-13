# 04 - Caso de uso: Registrar Venta (versión corregida)

| Campo | Descripción |
|---|---|
| **Caso de uso** | Registrar Venta |
| **Actor principal** | Empleada de mostrador |
| **Frecuencia** | Múltiples veces por día, en cada turno de atención |
| **Precondiciones** | La empleada inició sesión con su usuario y PIN. La farmacia opera con reposición diaria: todo lo vendido se repone cada noche vía pedido a las droguerías. |
| **Garantías de éxito** | La venta queda registrada con el precio vigente en el sistema. Si un producto no está disponible, se agrega a la nota de pedido para que Alicia gestione la reposición. |

## Personal involucrado e intereses

- **Empleada de mostrador:** registrar la venta de forma rápida y cobrar el precio correcto y actualizado. Si el producto no está disponible, agregarlo a la nota de pedido sin fricciones.
- **Alicia (encargada / propietaria):** que las ventas queden registradas con el precio vigente para evitar errores de cobro. Que al cierre del día tenga disponible la nota de pedido consolidada para contactar a las droguerías sin depender de cuadernos manuales.

## Escenario principal de éxito (flujo básico)

Una venta puede contener varios productos (en el DER, una venta tiene muchos detalles), por lo que los pasos 1 a 3 se repiten por cada producto antes de confirmar.

1. La empleada busca el producto en el sistema por nombre (búsqueda parcial) o por código.
2. El sistema muestra los datos de los productos que coinciden: descripción y precio vigente.
3. La empleada selecciona el producto y confirma la cantidad a vender. El sistema agrega el ítem a la venta y muestra su subtotal.
4. Si hay más productos, se vuelve al paso 1.
5. La empleada confirma la venta.
6. El sistema registra la venta con todos sus ítems, con la fecha actual y la empleada autenticada.
7. El sistema muestra la confirmación de la operación con el precio total cobrado.

## Escenarios alternativos (extensiones)

**1. El producto no está disponible en el mostrador** (paso 3):
Existe en el catálogo, pero no se tiene en ese momento. La empleada lo agrega a la nota de pedido desde la misma pantalla de búsqueda, con un paso adicional, indicando la cantidad. La nota referencia el producto del catálogo y, opcionalmente, una droguería sugerida. Queda disponible para que Alicia la consulte al cierre del día. La empleada continúa con el siguiente producto.

**2. El producto no existe en el catálogo** (paso 2):
El sistema informa que el producto no fue encontrado. La empleada avisa a la encargada para que lo registre mediante "Gestionar productos (ABM)". No puede agregarse a la nota de pedido hasta que exista en el catálogo. La empleada continúa con el siguiente producto.

**3. La empleada cancela la operación antes de confirmar** (en cualquier momento antes del paso 5):
El sistema descarta los datos ingresados y vuelve a la pantalla principal sin registrar ningún movimiento.

## Requisitos especiales

- El precio mostrado debe ser siempre el vigente en el sistema, sin requerir que la empleada lo consulte manualmente.
- La búsqueda de productos debe ser rápida, tolerando búsqueda parcial por nombre.
- El agregado a la nota de pedido debe ser accesible desde la misma pantalla de búsqueda, con un solo paso adicional.

## Fuera del alcance

- El sistema no consulta catálogos ni precios de las droguerías.
- El sistema no envía pedidos automáticos a las droguerías. Alicia gestiona la comunicación con cada droguería manualmente (WhatsApp o página web) usando la nota de pedido como referencia.

## Cambios respecto a la versión anterior

| Antes | Ahora | Motivo |
|---|---|---|
| Alternativo 1: "El producto no existe en el catálogo" se agrega a la nota de pedido | Alternativo 1: "No está disponible en el mostrador" se agrega a la nota. Alternativo 2 (nuevo): "No existe en el catálogo" se informa a la encargada para su alta | `nota_pedido` solo referencia un producto por `id_producto` y no tiene campo libre; la entrevista habla de productos no disponibles ("si no está, no está") |
| Búsqueda por nombre o código, un solo producto | Igual criterio de búsqueda, con repetición de los pasos por cada producto | Una venta tiene muchos detalles (DER) |
| Precondición: sistema iniciado | La empleada inició sesión con usuario y PIN | Roles Empleada y Encargada definidos en el DER |
| Cancelación (alternativo 2) | Cancelación (alternativo 3), sin cambios | - |
