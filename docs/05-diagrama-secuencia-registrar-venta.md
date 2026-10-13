# 05 - Diagramas de secuencia: Registrar Venta

## 1. Diagrama de secuencia del sistema (nivel de análisis)

Muestra la interacción entre la Empleada y el sistema tratado como una caja negra (`:Sistema`).

```mermaid
sequenceDiagram
    actor E as Empleada
    participant S as :Sistema

    E->>S: CrearNuevaVenta()
    loop por cada producto
        E->>S: BuscarProducto(texto: nombre o código)
        S-->>E: lista de coincidencias con descripción y precio vigente
        alt producto disponible
            E->>S: AgregarProducto(idProducto, cantidad)
            S-->>E: subtotal
        else producto no disponible
            E->>S: AgregarANotaPedido(idProducto, cantidad)
            S-->>E: confirmación
        else producto no existe en el catálogo
            S-->>E: mensaje "producto no encontrado"
        end
    end
    alt la empleada confirma
        E->>S: FinalizarVenta()
        S-->>E: confirmación y total cobrado
    else la empleada cancela
        E->>S: CancelarVenta()
        S-->>E: descarta los datos
    end
```

**Explicación.** La empleada inicia la venta y, por cada producto, busca por nombre parcial o código. Según el resultado: lo agrega a la venta (disponible), lo anota en la nota de pedido (no disponible) o recibe el aviso de que no existe en el catálogo. Al terminar, confirma la venta y recibe el total, o la cancela y no se registra nada.

**Cambios respecto a la versión anterior** (`CrearNuevaVenta()`, `BuscarArticulo(id)`, `ConfirmarCantidad(cantidad)`, `FinalizarVenta()`):

- `BuscarArticulo(id)` pasa a `BuscarProducto(texto)`: búsqueda por nombre parcial o código, con lista de coincidencias.
- `ConfirmarCantidad(cantidad)` pasa a `AgregarProducto(idProducto, cantidad)`, que devuelve el subtotal.
- Se agrega un `loop` porque una venta puede tener varios productos.
- Se agregan `AgregarANotaPedido` (producto no disponible) y el mensaje de producto no encontrado (no existe en el catálogo).
- Se agrega `CancelarVenta()`.

## 2. Diagrama de secuencia de diseño

Usa los endpoints reales de la API. Capas: cliente Blazor WASM (`FarmaTech.Cliente`), `HttpService` (`FarmaTech.Servicios`), controladores (`FarmaTech.Server`), repositorios (`FarmaTech.Repository`) y `AppDbContext` sobre SQL Server (`FarmaTech.BD`).

```mermaid
sequenceDiagram
    actor E as Empleada
    participant P as Pantalla Venta (Blazor)
    participant H as HttpService
    participant PC as ProductoController
    participant VC as VentaController
    participant NC as NotaPedidoController
    participant R as Repositorio
    participant DB as Base de datos

    loop por cada producto
        E->>P: ingresa nombre o código
        P->>H: buscar(texto)
        H->>PC: GET api/producto/buscar?texto=
        PC->>R: buscar productos por nombre parcial o código
        R->>DB: SELECT productos coincidentes
        DB-->>R: productos
        R-->>PC: productos
        PC-->>H: lista de productos con precio vigente
        H-->>P: lista de productos
        P-->>E: muestra coincidencias

        alt producto disponible
            E->>P: selecciona producto y cantidad
            P-->>E: agrega ítem y muestra subtotal en pantalla
        else producto no disponible
            E->>P: agregar a nota de pedido (idProducto, cantidad)
            P->>H: registrarFaltante(idProducto, cantidad, idDrogueria opcional)
            H->>NC: POST api/nota-pedido/faltante (Bearer JWT)
            NC->>NC: toma la empleada del JWT
            NC->>R: crear nota con estado Pendiente
            R->>DB: INSERT nota_pedido
            DB-->>R: ok
            R-->>NC: nota creada
            NC-->>H: confirmación
            H-->>P: confirmación
            P-->>E: nota de pedido registrada
        else producto no existe
            H-->>P: lista vacía (sin resultados)
            P-->>E: mensaje producto no encontrado
        end
    end

    alt la empleada confirma
        E->>P: confirmar venta
        P->>H: registrarVenta(items: idProducto, cantidad)
        H->>VC: POST api/venta/registrar (Bearer JWT)
        VC->>VC: toma la empleada del JWT y fecha = ahora
        VC->>R: registrar venta con items
        R->>DB: lee precio vigente de cada producto
        DB-->>R: precios
        R->>R: calcula subtotales y total
        R->>DB: INSERT venta y detalle_venta en una sola transacción
        DB-->>R: commit
        R-->>VC: venta registrada
        VC-->>H: IdVenta, Fecha, Total, detalles
        H-->>P: venta registrada
        P-->>E: confirmación y total cobrado
    else la empleada cancela
        E->>P: cancelar
        P-->>E: descarta la venta en curso (sin llamadas al servidor)
    end
```

**Explicación.**

- La búsqueda usa `GET api/producto/buscar?texto=` (parcial por nombre o código). También existe `GET api/producto/codigo/{codigo}` para búsqueda exacta por código, que sigue el mismo recorrido.
- Los ítems se acumulan en la pantalla; no se persiste nada hasta confirmar.
- El cliente envía solo `IdProducto` y `Cantidad`. El servidor toma el precio vigente de la base de datos, calcula subtotales y total, obtiene la empleada del JWT, usa la fecha actual y guarda venta y detalles en una única transacción. Responde `IdVenta`, `Fecha`, `Total` y los detalles.
- `POST api/nota-pedido/faltante` (cuerpo: `IdProducto`, `Cantidad`, `IdDrogueria` opcional) crea la nota con estado `Pendiente` y la empleada tomada del JWT. Ambos POST requieren autorización.
- Cancelar no genera llamadas al servidor porque la venta aún no fue persistida.

**Diferencias con el diagrama de análisis.** El de análisis oculta las capas; el de diseño agrega la entrega del precio desde la base de datos (nunca desde el cliente) y la transacción única, y deja explícito que "producto no existe" es un resultado vacío de la búsqueda.
