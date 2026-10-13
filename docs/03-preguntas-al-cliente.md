# 03 - Preguntas al cliente (segunda entrevista)

**Materia:** Modelado de Sistemas
**Proyecto:** FarmaTech - Evaluación 2 Modelado
**Cliente:** Farmacia Ternavasio (farmacia de barrio)
**Dirección:** Parque Liceo 2da sección, Av. Alfonsina Storni 835
**Entrevistada:** Alicia Ternavasio, propietaria

## Resumen de la primera entrevista

En la primera entrevista surgió que la farmacia no lleva un control formal del stock. La segunda charla tuvo como objetivo entender en profundidad cómo es el flujo real de mercadería y definir hasta dónde llega el alcance del sistema en relación a las droguerías.

## Preguntas y respuestas (segunda entrevista)

**P:** En la charla anterior me comentaste que no llevan control de stock. ¿Cómo funciona entonces su flujo de producto o mercadería en el día a día?

> **R:** Claro, nosotras no tenemos un depósito con stock acumulado. Básicamente todo lo que vendemos en el día lo pedimos esa misma noche a la droguería. Al día siguiente llega y así seguimos. Siempre hay producto porque lo pedimos todos los días.

**P:** ¿O sea que si una empleada busca un producto y no lo tiene disponible en ese momento, no es porque se agotó el stock sino porque todavía no llegó el pedido del día?

> **R:** Claro, exactamente. Si no está, no está, y la chica lo anota como faltante para que yo lo pida esa noche. Pero no es que llevamos una cuenta de cuánto queda de cada cosa.

**P:** ¿Entonces le gustaría ver esa lista de faltantes al final del día en el sistema para hacer el pedido?

> **R:** Sí, me haría las cosas más fácil entrar al sistema y ver todo junto, lo que se vendió y lo que no hay, para pasárselo a la droguería sin tener que juntar los cuadernos de cada chica.

**P:** El tema de las droguerías, ¿cómo lo manejas? ¿Haces los pedidos por páginas web o por WhatsApp?

> **R:** Con una droguería trato por WhatsApp con el vendedor que es conocido, y con la otra por lo general pido por alguna diferencia de precio o por algún producto que la otra droguería no tenga, entro a su página web.

## Conclusiones

Esta segunda entrevista confirma y ajusta los siguientes puntos del sistema FarmaTech:

- La farmacia opera con reposición diaria: todo lo vendido se repone cada noche mediante pedido a las droguerías. No existe un stock acumulado en depósito.
- La empleada detecta la ausencia de un producto al momento de buscarlo. No se requieren alertas automáticas de stock mínimo.
- Cuando un producto no se encuentra, la empleada lo agrega a una nota de pedido dentro del mismo sistema, que queda disponible para que Alicia la consulte al cierre del día.
- El sistema NO consulta catálogos de droguerías ni genera pedidos automáticos. Alicia se contacta manualmente con cada droguería (por WhatsApp o página web según corresponda) usando la nota de pedido como referencia.

**Precisión posterior.** "Cuando un producto no se encuentra" se interpreta como producto **no disponible** en el mostrador (existe en el catálogo pero no se tiene ese día). Un producto que no existe en el catálogo no puede ir a la nota de pedido, porque en el DER final `nota_pedido` solo referencia un producto del catálogo (`id_producto`). Ver [04-caso-de-uso-registrar-venta.md](04-caso-de-uso-registrar-venta.md).

## Visión y alcance corregidos

**Descripción general.** FarmaTech es un sistema de gestión diseñado para farmacias de barrio que buscan modernizar su operación diaria. Permite registrar las ventas del día, gestionar y consultar precios desde un lugar centralizado, y llevar una nota de pedido con los productos faltantes para que la encargada pueda coordinar la reposición con las droguerías proveedoras, reemplazando el uso de cuadernos y planillas manuales por una herramienta digital accesible para todo el personal del mostrador.

**Dentro del sistema (lo que abarca):**

- Registro de ventas del día
- Consulta de disponibilidad y precios de productos
- Alta, baja y modificación de productos del catálogo interno
- Registro de ingresos de mercadería
- Registro y consulta de nota de pedido con los productos faltantes detectados durante el día

**Fuera del sistema (lo que NO abarca):**

- Gestión de stock mínimo o alertas automáticas de inventario
- Consulta de catálogos o precios de las droguerías
- Generación o envío automático de pedidos a las droguerías (Alicia los gestiona manualmente vía WhatsApp o página web según corresponda)
- Gestión de clientes o historial de compras por persona
- Gestión contable o facturación electrónica
- Compras online

## Preguntas pendientes para la próxima entrevista

Propuestas a partir de vacíos detectados en la documentación. **No fueron formuladas ni respondidas todavía.**

1. **Estado del producto y de la nota.** El DER define `producto.estado` y `nota_pedido.estado`, pero ninguna fuente indica sus valores. ¿Qué significa el estado de un producto (por ejemplo, disponible o no disponible)? ¿Lo marca la empleada o cambia con el ingreso de mercadería? ¿Qué estados tiene una nota de pedido (por ejemplo, pendiente o pedida)?
2. **Productos que no están en el catálogo.** Cuando un cliente pide algo que el sistema no conoce, ¿quién lo da de alta y cuándo? ¿Alcanza con que la encargada lo cargue al día siguiente, o debería poder hacerlo la empleada en el momento?
3. **Nota de pedido diaria.** ¿La nota es una lista por día? ¿Quién la da por cerrada o por pedida, y qué pasa con lo que quedó sin pedir? Si el mismo producto se anota varias veces en el día, ¿se suman las cantidades?
4. **Unidades y presentaciones.** ¿Se vende y se pide por unidad, caja o fracción (por ejemplo, blíster)? ¿Un mismo medicamento con distintas presentaciones es un producto o varios?
5. **Venta con varios productos y cobro.** El DER permite varios detalles por venta. ¿Es habitual que un cliente lleve varios productos en una misma compra? ¿Debe registrarse el medio de pago o descuentos (por ejemplo, obras sociales)? Hoy ningún dato del DER lo contempla.
