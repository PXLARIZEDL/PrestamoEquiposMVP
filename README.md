# Sistema de Préstamo de Equipos

Aplicación de escritorio en **C# / WinForms / .NET Framework 4.7.2** que permite
registrar equipos y usuarios, prestar equipos, registrar devoluciones y consultar
el historial. El objetivo es demostrar el patrón **MVP (Model-View-Presenter)** y
los **principios SOLID** con un código sencillo y fácil de explicar.

No usa ninguna librería externa ni paquete NuGet: solo el framework base. Los datos
se guardan **en memoria** durante la ejecución (con datos de prueba precargados).

---

## Cómo abrir y ejecutar

1. Abrir `PrestamoEquipos.sln` con **Visual Studio 2019 o 2022**.
2. Presionar **F5** (o el botón Iniciar).

No hay que instalar nada más ni configurar cadenas de conexión.

---

## Arquitectura por capas

```
  Formularios (WinForms)        <- solo dibujan y disparan eventos (la Vista)
        | implementan
        v
  Vistas (interfaces IxxxView)  <- contrato entre Vista y Presenter
        |
        v
  Presentadores (Presenters)    <- la logica de cada pantalla
        |
        v
  Servicios (reglas de negocio) <- ej: "un equipo no se presta dos veces"
        |
        v
  Repositorios (almacenamiento) <- guardan y consultan datos (en memoria)
        |
        v
  Modelos (Usuario, Equipo, Prestamo)
```

El **ContenedorDependencias** (Composition Root) es el único lugar donde se crean
las clases concretas y se conectan con sus interfaces. Todo lo demás depende de
abstracciones.

---

## El patrón MVP explicado

- **Model**: los modelos (`Usuario`, `Equipo`, `Prestamo`), los repositorios y el
  servicio. Es "el qué".
- **View**: cada formulario implementa una interfaz (`IEquipoView`, `IUsuarioView`,
  `IPrestamoView`). El formulario **no tiene lógica**: solo expone propiedades,
  dispara eventos al hacer clic, y muestra lo que el presenter le pasa.
- **Presenter**: recibe la Vista como interfaz, escucha sus eventos, decide qué
  hacer y le devuelve los datos a mostrar. **Nunca conoce el formulario concreto**,
  solo la interfaz. Por eso se pudo probar la lógica con "vistas falsas" sin abrir
  ventanas.

Flujo de un préstamo:
`Usuario hace clic -> el Form dispara el evento -> el Presenter llama al Servicio ->
el Servicio aplica la regla -> el Presenter refresca la Vista`.

---

## Dónde se cumple cada principio SOLID

| Principio | Dónde | Cómo |
|---|---|---|
| **S** - Responsabilidad única | `PrestamoService`, cada Presenter, cada repositorio | El servicio solo tiene reglas de negocio; el presenter solo coordina la pantalla; el repositorio solo almacena; el formulario solo dibuja. |
| **O** - Abierto/cerrado | `IRepositorio<T>` y `RepositorioEnMemoria<T>` | Para pasar de memoria a SQL se crea `EquipoRepositorioSql : IEquipoRepositorio` **sin modificar** presenters ni servicio. |
| **L** - Sustitución de Liskov | `RepositorioEnMemoria<T>` y sus hijos | Cualquier implementación de `IEquipoRepositorio` puede reemplazar a otra sin romper el sistema. |
| **I** - Segregación de interfaces | `IEquipoView`, `IUsuarioView`, `IPrestamoView`, `IPrestamoRepositorio` | Interfaces pequeñas y específicas. La vista de equipos no arrastra métodos de préstamos. |
| **D** - Inversión de dependencias | Presenters y `PrestamoService` | Dependen de **interfaces**, no de clases concretas. Las concretas se inyectan por el constructor desde el `ContenedorDependencias`. |

---

## Estructura de carpetas

- `Modelos/` - entidades del dominio.
- `Repositorios/` - interfaces + implementación en memoria.
- `Servicios/` - reglas de negocio (`PrestamoService`, `Resultado`).
- `Vistas/` - interfaces de las vistas (contrato del MVP).
- `Presentadores/` - la lógica de cada pantalla.
- `Formularios/` - los formularios WinForms (View).
- `Infraestructura/` - `ContenedorDependencias` (composition root).

---

## Posibles preguntas del profesor (y respuesta corta)

**¿Por qué no usa base de datos?**
Es un MVP y el requisito era demostrar arquitectura y SOLID, no persistencia.
Gracias al patrón repositorio, agregar SQL Server solo implica crear una clase que
implemente `IEquipoRepositorio`/etc. y cambiar una línea en el
`ContenedorDependencias`. El resto del código no se toca (OCP + DIP).

**¿Qué es MVP y en qué se diferencia de poner todo en el formulario?**
En MVP el formulario no decide nada: solo muestra y avisa. La lógica vive en el
Presenter, que trabaja contra una interfaz de la vista. Permite probar la lógica sin
la interfaz gráfica y cambiar la UI sin tocar la lógica.

**¿Dónde está la regla de negocio principal?**
En `PrestamoService.RegistrarPrestamo`: antes de crear el préstamo consulta si el
equipo ya tiene un préstamo activo; si lo tiene, devuelve `Resultado.Fallo`.

**¿Por qué inyectar por el constructor?**
Para que cada clase reciba sus dependencias ya listas y dependa de la interfaz, no
de la implementación.

**¿Por qué `Resultado` en vez de excepciones?**
Un préstamo rechazado por regla de negocio no es un error del programa, es un caso
esperado. `Resultado` lo comunica sin usar excepciones para el flujo normal.
