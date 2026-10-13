# Preguntas al cliente (tercera entrevista)

**Materia:** Modelado de Sistemas
**Proyecto:** FarmaTech - Evaluación 2 Modelado
**Cliente:** Farmacia Ternavasio (farmacia de barrio)
**Dirección:** Parque Liceo 2da sección, Av. Alfonsina Storni 835
**Entrevistada:** Alicia Ternavasio, propietaria

## Resumen de las entrevistas anteriores

En la primera entrevista surgió que la farmacia no lleva un control formal del stock. Esta entrevista tuvo como objetivo entender en profundidad cómo es el flujo real de mercadería, definir hasta dónde llega el alcance del sistema en relación a las droguerías y confirmar quién puede hacer cada tarea dentro del sistema.

## Preguntas y respuestas

**P:** En la charla anterior me comentaste que no llevan control de stock. ¿Cómo funciona entonces su flujo de producto o mercadería en el día a día?

> **R:** Claro, nosotras no tenemos un depósito con stock acumulado. Básicamente todo lo que vendemos en el día lo pedimos esa misma noche a la droguería. Al día siguiente llega y así seguimos. Siempre hay producto porque lo pedimos todos los días.

**P:** ¿O sea que si una empleada busca un producto y no lo tiene disponible en ese momento, no es porque se agotó el stock sino porque todavía no llegó el pedido del día?

> **R:** Claro, exactamente. Si no está, no está, y la chica lo anota como faltante para que yo lo pida esa noche. Pero no es que llevamos una cuenta de cuánto queda de cada cosa.

**P:** ¿Entonces le gustaría ver esa lista de faltantes al final del día en el sistema para hacer el pedido?

> **R:** Sí, me haría las cosas más fácil entrar al sistema y ver todo junto, lo que se vendió y lo que no hay, para pasárselo a la droguería sin tener que juntar los cuadernos de cada chica.

**P:** El tema de las droguerías, ¿cómo lo manejas? ¿Haces los pedidos por páginas web o por WhatsApp?

> **R:** Con una droguería trato por WhatsApp con el vendedor que es conocido, y con la otra por lo general pido por alguna diferencia de precio o por algún producto que la otra droguería no tenga, entro a su página web.

**P:** ¿Cada empleada tendría su propio usuario para entrar al sistema, o usan todas el mismo?

> **R:** No, mejor que cada una tenga el suyo. Así sé quién hizo cada venta. Algo simple, un usuario y un número cortito, porque en el mostrador no hay tiempo para contraseñas largas.

**P:** ¿Las empleadas pueden cargar productos nuevos o cambiar precios, o eso lo hacés solo vos?

> **R:** Eso lo hago yo. Las chicas venden, buscan y anotan lo que falta, pero los precios y los productos nuevos los manejo yo, porque si no después no sé por qué un precio cambió.

**P:** ¿Vos también atendés el mostrador? ¿Necesitás poder registrar ventas igual que las chicas?

> **R:** Sí, claro, cuando hay mucha gente atiendo yo también. Tengo que poder hacer todo lo que hacen ellas y además lo mío.

**P:** Cuando una empleada busca un producto, ¿cómo lo busca normalmente?

> **R:** Por el nombre casi siempre, a veces ponen una parte nomás, "ibupro" y ya sale. Y si tienen la caja en la mano, por el código que viene impreso.

**P:** Si un producto está en el sistema pero ese día no lo tienen en el mostrador, ¿qué hace la empleada?

> **R:** Lo anota en la nota de pedido, con cuánto pidió el cliente, y sigue atendiendo. Eso es solo cuando no lo tenemos físicamente, eh; si está en la estantería, se vende y listo.

**P:** ¿Y si el cliente pide algo que ni siquiera está cargado en el sistema?

> **R:** Ahí me avisan a mí y yo lo doy de alta. No quiero que las chicas carguen productos sueltos porque después tengo el mismo remedio cargado tres veces con nombres distintos.

**P:** ¿Qué estado tiene un faltante una vez anotado?

> **R:** Queda pendiente hasta que yo lo pido a la droguería esa noche. Lo importante es ver todo lo pendiente junto.

**P:** ¿Un mismo medicamento con distintas presentaciones es un solo producto o varios?

> **R:** Son distintos, el ibuprofeno de 400 y el de 600 tienen precio distinto y código distinto. Cada caja como viene es un producto.

## Conclusiones

- La farmacia opera con reposición diaria: todo lo vendido se repone cada noche mediante pedido a las droguerías. No existe un stock acumulado en depósito.
- La empleada detecta la ausencia de un producto al momento de buscarlo. No se requieren alertas automáticas de stock mínimo.
- El sistema NO consulta catálogos de droguerías ni genera pedidos automáticos. Alicia se contacta manualmente con cada droguería (por WhatsApp o página web según corresponda) usando la nota de pedido como referencia.
- Cada empleada ingresa con su propio usuario y PIN. Hay dos roles: **Empleada** (vende, consulta y anota faltantes) y **Encargada** (además da de alta y modifica productos y actualiza precios).
- La búsqueda es por nombre (parcial) o por código. Cada presentación es un producto distinto.
- La nota de pedido se usa solo cuando el producto existe en el catálogo pero no está físicamente en el mostrador. Lleva la cantidad y queda en estado **Pendiente** hasta que Alicia lo pide.
- Un producto que no existe en el catálogo no va a la nota de pedido: la empleada avisa a la encargada para que lo dé de alta.

## Visión y alcance

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

## Casos de uso

| N° | Caso de uso | Actor | Descripción |
|---|---|---|---|
| CU1 | **Registrar Venta** (seleccionado) | Empleada de mostrador | Registra una venta con uno o varios productos al precio vigente. |
| CU2 | Agregar producto a nota de pedido | Empleada de mostrador | Anota un producto del catálogo que no está en el mostrador. Extiende a CU1. |
| CU3 | Consultar disponibilidad de producto | Empleada de mostrador | Busca el producto por nombre o código para saber si está en el catálogo. |
| CU4 | Consultar precio de producto | Empleada de mostrador | Consulta el precio vigente de un producto. |
| CU5 | Actualizar precio de producto | Encargada | Modifica el precio vigente de un producto. |
| CU6 | Registrar ingreso de mercadería | Encargada | Registra la mercadería recibida de una droguería. |
| CU7 | Gestionar productos (ABM) | Encargada | Da de alta, modifica y da de baja productos del catálogo. |
| CU8 | Consultar nota de pedido del día | Encargada | Ve los faltantes pendientes para pedirlos a las droguerías. |

La Encargada también puede realizar los casos de uso de la Empleada.

## Caso de uso seleccionado: Registrar Venta

**Actor principal:** Empleada de mostrador.
**Precondición:** la empleada inició sesión con su usuario y PIN. El producto debe estar cargado correctamente en el sistema y debe haber stock físico en la farmacia.
**Garantía de éxito:** la venta queda registrada con el precio vigente, la fecha y la empleada que la realizó.

**Flujo básico:**

1. La empleada busca el producto por nombre (parcial) o por código.
2. El sistema muestra los productos que coinciden con su precio vigente.
3. La empleada elige el producto y la cantidad; el sistema lo agrega a la venta con su subtotal.
4. Si hay más productos, se repiten los pasos 1 a 3.
5. La empleada confirma la venta y el sistema la registra y muestra el total.

**Flujos alternativos:**

- **El producto no está en el mostrador:** la empleada lo agrega a la nota de pedido con la cantidad y continúa con la venta.
- **El producto no existe en el catálogo:** el sistema informa que no se encontró y la empleada avisa a la encargada para que lo dé de alta.
- **Cancelación:** antes de confirmar, la empleada cancela y el sistema descarta la venta sin registrar nada.
