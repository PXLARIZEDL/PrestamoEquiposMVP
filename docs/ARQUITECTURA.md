# Arquitectura (WinForms, MVP)

Este documento entra en el detalle técnico que el README resume: qué hace cada capa,
qué promete cada interfaz, qué clase la cumple y cómo se recorre el sistema de punta a
punta cuando alguien registra un préstamo.

## Capas y flujo de dependencias

Hay cinco capas y todas dependen hacia abajo:

```
Formularios  ->  Presentadores  ->  Servicios  ->  Repositorios  ->  Modelos
```

La dependencia nunca es hacia una clase concreta. El formulario implementa una
interfaz de vista; el presentador conoce esa interfaz y una interfaz de repositorio;
el servicio conoce una interfaz de repositorio. Las clases concretas se unen en un
solo punto, `ContenedorDependencias`, que actúa como composition root.

El modelo `IEntidad` es la pieza que permite que el repositorio genérico funcione sin
conocer el tipo concreto: obliga a que toda entidad tenga un `Id`, y con eso
`RepositorioEnMemoria<T>` puede asignar identificadores y buscar por Id sirviendo por
igual a `Equipo`, `Usuario` y `Prestamo`.

## Catálogo de interfaces

| Interfaz | Qué promete | Quién la implementa |
|---|---|---|
| `IEntidad` | Toda entidad expone un `Id` de tipo `int`. | `Equipo`, `Usuario`, `Prestamo` |
| `IRepositorio<T>` | Almacenamiento genérico: `Agregar`, `ObtenerTodos`, `ObtenerPorId`. | `RepositorioEnMemoria<T>` |
| `IEquipoRepositorio` | Especializa `IRepositorio<Equipo>` sin añadir nada; da un nombre propio para inyectar. | `EquipoRepositorioEnMemoria` |
| `IUsuarioRepositorio` | Especializa `IRepositorio<Usuario>`. | `UsuarioRepositorioEnMemoria` |
| `IPrestamoRepositorio` | Añade a `IRepositorio<Prestamo>` lo que su cliente necesita: `ObtenerActivos` y `ObtenerPrestamoActivoPorEquipo`. | `PrestamoRepositorioEnMemoria` |
| `IPrestamoService` | Reglas de préstamo: `RegistrarPrestamo`, `RegistrarDevolucion`, `ObtenerPrestamosActivos`, `ObtenerHistorial`. | `PrestamoService` |
| `IEquipoView` | Contrato de la pantalla de equipos: propiedades de entrada, evento `GuardarEquipo` y métodos para mostrar datos y mensajes. | `FrmEquipos` |
| `IUsuarioView` | Contrato de la pantalla de usuarios. | `FrmUsuarios` |
| `IPrestamoView` | Contrato de la pantalla de préstamos: selección de equipo, usuario y préstamo, eventos `RegistrarPrestamo` y `RegistrarDevolucion`, y métodos de carga. | `FrmPrestamos` |

## Catálogo de clases

Modelos:

- `Equipo`: nombre, código y disponibilidad. Su `ToString` devuelve el nombre, que es
  lo que se ve en los combos de la pantalla de préstamos.
- `Usuario`: nombre y matrícula. También devuelve el nombre en `ToString`.
- `Prestamo`: enlaza un `Equipo` con un `Usuario`, guarda `FechaPrestamo` y una
  `FechaDevolucion` opcional. La propiedad calculada `EstaDevuelto` es cierta cuando
  hay fecha de devolución; es la que usa el servicio para decidir si un préstamo sigue
  activo.

Repositorios:

- `RepositorioEnMemoria<T>`: clase base abstracta que guarda una `List<T>` y un
  contador `_siguienteId`. Reúne la lógica común de `Agregar`, `ObtenerTodos` y
  `ObtenerPorId` para no repetirla en cada repositorio. `ObtenerTodos` devuelve una
  copia con `ToList` para que nadie modifique la lista interna por fuera.
- `EquipoRepositorioEnMemoria` y `UsuarioRepositorioEnMemoria`: heredan de la base y no
  añaden nada; existen para tener un tipo concreto que instanciar.
- `PrestamoRepositorioEnMemoria`: además de heredar la base, implementa
  `ObtenerPrestamoActivoPorEquipo` (busca el préstamo no devuelto de un equipo) y
  `ObtenerActivos` (todos los no devueltos).

Servicios:

- `PrestamoService`: concentra las reglas. Recibe un `IPrestamoRepositorio` por el
  constructor. Es la única clase que sabe qué significa "no se puede prestar dos veces".
- `Resultado`: objeto de retorno de las operaciones de negocio. Constructor privado y
  dos fábricas, `Ok(mensaje)` y `Fallo(mensaje)`, para no usar excepciones en el flujo
  normal.

Presentación:

- `EquipoPresenter`, `UsuarioPresenter`: reciben la vista y el repositorio, se suscriben
  al evento de guardar, validan la entrada mínima, agregan al repositorio y refrescan la
  grilla.
- `PrestamoPresenter`: recibe la vista, el servicio y los dos repositorios de apoyo.
  Delega las reglas al servicio y solo se encarga de refrescar la vista con lo que el
  servicio devuelve.
- `FrmPrincipal`: menú que abre cada formulario como diálogo pasándole lo que necesita
  del contenedor.
- `FrmEquipos`, `FrmUsuarios`, `FrmPrestamos`: implementan su interfaz de vista. En el
  constructor traducen el clic de un botón a un evento de la interfaz y crean su
  presentador.
- `ContenedorDependencias`: composition root. Crea los repositorios y el servicio,
  precarga datos de prueba y expone todo por propiedades de solo lectura.

## Flujo completo: registrar un préstamo

Este es el recorrido paso a paso cuando el usuario selecciona un equipo y un usuario y
pulsa "Registrar Préstamo" en `FrmPrestamos`:

1. El usuario pulsa el botón. En el constructor de `FrmPrestamos` ese clic está
   conectado así:
   `btnPrestar.Click += (s, e) => RegistrarPrestamo?.Invoke(this, EventArgs.Empty);`
   El formulario no ejecuta lógica; solo dispara el evento `RegistrarPrestamo` de su
   interfaz `IPrestamoView`.

2. `PrestamoPresenter` está suscrito a ese evento desde su constructor
   (`_vista.RegistrarPrestamo += OnRegistrarPrestamo;`), así que se ejecuta
   `OnRegistrarPrestamo`.

3. El presentador lee la selección directamente de la vista, por interfaz, y llama al
   servicio:

   ```csharp
   Resultado resultado = _servicio.RegistrarPrestamo(
       _vista.EquipoSeleccionado, _vista.UsuarioSeleccionado);
   ```

   `EquipoSeleccionado` y `UsuarioSeleccionado` son propiedades que el formulario
   resuelve desde sus combos (`cboEquipo.SelectedItem as Equipo`).

4. `PrestamoService.RegistrarPrestamo` aplica las reglas. Valida que haya equipo y
   usuario, y luego pregunta al repositorio si el equipo ya tiene un préstamo activo:

   ```csharp
   Prestamo activo = _prestamos.ObtenerPrestamoActivoPorEquipo(equipo.Id);
   if (activo != null)
       return Resultado.Fallo("El equipo '" + equipo.Nombre +
           "' ya esta prestado a " + activo.Usuario.Nombre + ".");
   ```

   Si el equipo está libre, crea el `Prestamo` con `FechaPrestamo = DateTime.Now` y
   `FechaDevolucion = null`, lo agrega al repositorio, marca `equipo.Disponible = false`
   y devuelve `Resultado.Ok`.

5. `PrestamoRepositorioEnMemoria.Agregar` (heredado de `RepositorioEnMemoria<T>`) asigna
   el `Id` autoincremental y guarda el préstamo en la lista.

6. De vuelta en el presentador, se muestra el mensaje y, si hubo éxito, se refresca todo:

   ```csharp
   _vista.MostrarMensaje(resultado.Mensaje);
   if (resultado.Exito)
       Refrescar();
   ```

7. `Refrescar` vuelve a cargar equipos, usuarios, activos e historial en la vista. El
   formulario recibe las listas y las pinta: en `MostrarActivos` guarda cada `Prestamo`
   real en el `Tag` de su fila de la grilla, para poder recuperarlo cuando el usuario
   seleccione uno y pulse devolver.

8. El usuario ve el `MessageBox` con el mensaje del `Resultado`.

La devolución sigue el mismo recorrido con el evento `RegistrarDevolucion`:
`PrestamoSeleccionado` sale del `Tag` de la fila seleccionada, el servicio marca la
fecha de devolución y deja el equipo disponible otra vez.

## Cómo extender el sistema

Agregar persistencia SQL. El punto de extensión es la interfaz de repositorio. Se crea
una implementación nueva, por ejemplo:

```csharp
public class EquipoRepositorioSql : IEquipoRepositorio
{
    // Agregar, ObtenerTodos, ObtenerPorId contra la base de datos.
}
```

Y en `ContenedorDependencias` se cambia una línea:

```csharp
Equipos = new EquipoRepositorioSql(/* conexión */);
```

Nada más se toca. Los presentadores y el servicio siguen recibiendo `IEquipoRepositorio`
y no notan la diferencia. Ese es el objetivo de que todo dependa de la interfaz.

Agregar una entidad nueva (por ejemplo, "Sala"). Se crea el modelo `Sala : IEntidad`,
su interfaz `ISalaRepositorio : IRepositorio<Sala>`, la implementación
`SalaRepositorioEnMemoria : RepositorioEnMemoria<Sala>, ISalaRepositorio`, y se expone
en `ContenedorDependencias`. Para la pantalla se sigue el mismo molde: una interfaz de
vista `ISalaView`, un `SalaPresenter` y un `FrmSalas` que implemente la interfaz. La
base genérica ya aporta el `Agregar`, `ObtenerTodos` y `ObtenerPorId`.
