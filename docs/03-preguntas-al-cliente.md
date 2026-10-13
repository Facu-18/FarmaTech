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

## Preguntas complementarias (validación del alcance implementado)

Preguntas realizadas para cerrar los puntos que quedaron abiertos y confirmar el comportamiento del sistema.

**P:** ¿Cada empleada tendría su propio usuario para entrar al sistema, o usan todas el mismo?

> **R:** No, mejor que cada una tenga el suyo. Así sé quién hizo cada venta. Algo simple, un usuario y un número cortito, porque en el mostrador no hay tiempo para contraseñas largas.

**P:** ¿Las empleadas pueden cargar productos nuevos o cambiar precios, o eso lo hacés solo vos?

> **R:** Eso lo hago yo. Las chicas venden, buscan y anotan lo que falta, pero los precios y los productos nuevos los manejo yo, porque si no después no sé por qué un precio cambió.

**P:** ¿Vos también atendés el mostrador? ¿Necesitás poder registrar ventas igual que las chicas?

> **R:** Sí, claro, cuando hay mucha gente atiendo yo también. Tengo que poder hacer todo lo que hacen ellas y además lo mío.

**P:** Cuando una empleada busca un producto, ¿cómo lo busca normalmente?

> **R:** Por el nombre casi siempre, a veces ponen una parte nomás, "ibupro" y ya sale. Y si tienen la caja en la mano, por el código que viene impreso.

**P:** ¿Un cliente suele llevar varios productos en la misma compra?

> **R:** Sí, muchísimo. Viene alguien con la receta y lleva tres o cuatro cosas. Tiene que ser una sola venta con todo junto y que el sistema me diga el total.

**P:** ¿El precio lo escribe la empleada en el momento o lo toma el sistema?

> **R:** No, que lo tome el sistema. Justamente lo que quiero es que no se equivoquen. Yo actualizo los precios y ellas cobran el que está cargado, sin tocar nada.

**P:** ¿Hace falta registrar el medio de pago, descuentos u obras sociales en la venta?

> **R:** Por ahora no. El cobro lo hacemos en la caja como siempre y las obras sociales tienen su propio trámite. Con que quede registrado qué se vendió, cuánto y a qué precio, me alcanza.

**P:** Si un producto está en el sistema pero ese día no lo tienen en el mostrador, ¿qué hace la empleada?

> **R:** Lo anota en la nota de pedido, con cuánto pidió el cliente, y sigue atendiendo. Eso es solo cuando no lo tenemos físicamente, eh; si está en la estantería, se vende y listo.

**P:** ¿Y si el cliente pide algo que ni siquiera está cargado en el sistema?

> **R:** Ahí me avisan a mí y yo lo doy de alta. No quiero que las chicas carguen productos sueltos porque después tengo el mismo remedio cargado tres veces con nombres distintos.

**P:** Al anotar un faltante, ¿conviene indicar a qué droguería pedirlo?

> **R:** Si saben, sí, que lo pongan, porque hay cosas que siempre pido a la misma. Pero si no saben, que lo anoten igual, yo después decido a quién se lo pido.

**P:** ¿Qué estado tiene un faltante una vez anotado?

> **R:** Queda pendiente hasta que yo lo pido a la droguería esa noche. Lo importante es ver todo lo pendiente junto.

**P:** ¿Un mismo medicamento con distintas presentaciones es un solo producto o varios?

> **R:** Son distintos, el ibuprofeno de 400 y el de 600 tienen precio distinto y código distinto. Cada caja como viene es un producto.

**P:** Cuando un producto se deja de vender, ¿lo borramos del sistema?

> **R:** No, borrarlo no, porque quiero que las ventas viejas sigan apareciendo. Que quede como que ya no se vende, y si algún día vuelve, lo activo de nuevo.

**P:** ¿Dos productos pueden tener el mismo código?

> **R:** No, el código es de cada producto. Si me equivoco y cargo uno repetido, que el sistema me avise.

## Conclusiones

Esta segunda entrevista confirma y ajusta los siguientes puntos del sistema FarmaTech:

- La farmacia opera con reposición diaria: todo lo vendido se repone cada noche mediante pedido a las droguerías. No existe un stock acumulado en depósito.
- La empleada detecta la ausencia de un producto al momento de buscarlo. No se requieren alertas automáticas de stock mínimo.
- Cuando un producto no se encuentra, la empleada lo agrega a una nota de pedido dentro del mismo sistema, que queda disponible para que Alicia la consulte al cierre del día.
- El sistema NO consulta catálogos de droguerías ni genera pedidos automáticos. Alicia se contacta manualmente con cada droguería (por WhatsApp o página web según corresponda) usando la nota de pedido como referencia.
- Cada empleada ingresa con su propio usuario y PIN. Hay dos roles: **Empleada** (vende, consulta y anota faltantes) y **Encargada** (además da de alta, modifica y da de baja productos y actualiza precios).
- Una venta puede incluir varios productos. El precio y el total los calcula el sistema con el precio vigente; la empleada no los ingresa. No se registran medio de pago, descuentos ni obras sociales.
- La búsqueda es por nombre (parcial) o por código. Cada presentación es un producto distinto con código único.
- La nota de pedido se usa solo cuando el producto existe en el catálogo pero no está físicamente en el mostrador. Lleva cantidad, droguería sugerida opcional y queda en estado **Pendiente**.
- La baja de un producto es lógica (pasa a **Inactivo**) para conservar el historial de ventas; puede reactivarse.

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

## Estado de las preguntas pendientes

1. **Estado del producto — Resuelta.** Indica si está activo en el catálogo (Activo/Inactivo); el sistema no controla stock.
2. **Estado de la nota — Resuelta.** Un faltante se registra como Pendiente hasta que la encargada lo pide.
3. **Productos que no están en el catálogo — Resuelta.** Los da de alta solo la encargada.
4. **Presentaciones — Resuelta.** Cada presentación es un producto distinto, con código y precio propios.
5. **Venta con varios productos y cobro — Resuelta.** Una venta tiene varios productos; no se registran medio de pago ni descuentos.

**Abiertas para una próxima iteración** (no afectan lo implementado):

- ¿Qué estado toma un faltante una vez pedido (por ejemplo, Pedido) y quién lo marca?
- Si el mismo producto se anota varias veces en el día, ¿se suman las cantidades en una sola línea?
- ¿Se vende alguna vez por fracción (por ejemplo, blíster suelto)?
