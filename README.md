# Sistema de Préstamo de Equipos (WinForms, MVP)

Aplicación de escritorio para llevar el control de préstamos de equipos: registrar
equipos y usuarios, prestar un equipo a una persona, registrar su devolución y
consultar el historial. La regla que sostiene todo lo demás es que un mismo equipo
no puede estar prestado a dos personas a la vez.

## Contexto

Es un trabajo de la asignatura Ingeniería del Conocimiento (cuatrimestre 7). El
objetivo no era entregar un producto listo para producción, sino mostrar una
separación de capas limpia y los principios SOLID sobre un caso pequeño y fácil de
seguir.

Existe una versión gemela del mismo sistema resuelta con WPF y el patrón MVVM, para
comparar cómo cambia solo la capa de presentación cuando el dominio se mantiene
igual: https://github.com/PXLARIZEDL/PrestamoEquiposMVVM

## Tecnologías

- C#
- .NET Framework 4.7.2
- WinForms

No depende de paquetes NuGet ni de librerías externas. Solo el framework base.

## Cómo ejecutarlo

Requisitos: Visual Studio 2022 con la carga de trabajo de escritorio .NET y
.NET Framework 4.7.2 instalado.

1. Abrir `PrestamoEquipos.sln`.
2. Ejecutar con F5.

No hay cadenas de conexión ni configuración previa. Al arrancar, el
`ContenedorDependencias` precarga tres equipos y tres usuarios de ejemplo para poder
probar de inmediato.

## Funcionalidad

- Registrar equipos (nombre y código).
- Registrar usuarios (nombre y matrícula).
- Prestar un equipo a un usuario.
- Registrar la devolución de un préstamo activo.
- Ver los préstamos activos y el historial completo con su estado.

La regla de negocio principal vive en `PrestamoService.RegistrarPrestamo`: antes de
crear el préstamo consulta si el equipo ya tiene uno activo y, si lo tiene, no lo
registra y devuelve un mensaje explicando a quién está prestado. La devolución
(`RegistrarDevolucion`) marca la fecha de devolución y vuelve a dejar el equipo
disponible.

## Arquitectura

El proyecto está dividido en capas y la dependencia siempre apunta hacia abajo, y
siempre a través de interfaces. Un formulario no conoce un repositorio concreto; lo
alcanza pasando por su presentador, el servicio y la interfaz del repositorio.

```
  Formularios (WinForms)         solo dibujan y disparan eventos
        | implementan IEquipoView / IUsuarioView / IPrestamoView
        v
  Presentadores                  la lógica de cada pantalla
        |
        v
  Servicios (PrestamoService)    reglas de negocio
        |
        v
  Repositorios                   almacenamiento (en memoria)
        |
        v
  Modelos (Equipo, Usuario, Prestamo)
```

Las flechas cruzan siempre por una interfaz: los formularios implementan
`IEquipoView`, `IUsuarioView` e `IPrestamoView`; los presentadores y el servicio
reciben `IEquipoRepositorio`, `IUsuarioRepositorio`, `IPrestamoRepositorio` e
`IPrestamoService`. Ninguna capa instancia a otra directamente. Eso lo hace un solo
lugar, el `ContenedorDependencias`.

## Estructura del proyecto

```
PrestamoEquipos/
  Modelos/           Entidades del dominio: Equipo, Usuario, Prestamo, IEntidad.
  Repositorios/      Interfaces de almacenamiento y su implementación en memoria.
  Servicios/         PrestamoService (reglas), IPrestamoService, Resultado.
  Vistas/            Interfaces IEquipoView, IUsuarioView, IPrestamoView (contrato del MVP).
  Presentadores/     EquipoPresenter, UsuarioPresenter, PrestamoPresenter.
  Formularios/       Los formularios WinForms; cada uno implementa su interfaz de vista.
  Infraestructura/   ContenedorDependencias (composition root).
  Program.cs         Punto de entrada.
```

## Decisiones de diseño

Los datos están en memoria y no en una base de datos. Para el alcance del trabajo,
persistir en SQL no aportaba nada al objetivo, que era la arquitectura. La parte que
importa es que esa decisión quedó aislada: todo el sistema habla contra
`IEquipoRepositorio`, `IUsuarioRepositorio` e `IPrestamoRepositorio`, nunca contra la
clase concreta. Migrar a SQL sería escribir una clase nueva, por ejemplo
`EquipoRepositorioSql : IEquipoRepositorio`, y cambiar una sola línea en el
`ContenedorDependencias` para instanciarla en lugar de `EquipoRepositorioEnMemoria`.
Ni los presentadores, ni el servicio, ni los formularios se enterarían.

Todo depende de interfaces y no de clases concretas por esa misma razón. Cuando el
`PrestamoService` recibe un `IPrestamoRepositorio` por el constructor, no le importa
si detrás hay una lista en memoria o una tabla; solo le importa el contrato. Eso hace
que la lógica sea comprobable con una implementación falsa del repositorio sin abrir
ninguna ventana, y que cambiar una implementación no obligue a tocar a quien la usa.

El `ContenedorDependencias` existe para tener un único punto donde se arma todo. Es el
composition root: el único lugar de la aplicación donde aparece la palabra `new`
seguida de una clase concreta de repositorio o servicio. `Program.cs` lo crea una vez
y ese mismo contenedor se comparte con todos los formularios, de modo que los datos
viven durante toda la sesión en lugar de reiniciarse cada vez que se abre una ventana.

En la capa de presentación opté por el patrón MVP. El formulario no toma decisiones:
expone propiedades (por ejemplo `NombreEquipo`), dispara eventos cuando el usuario
pulsa un botón y muestra lo que el presentador le entrega. El presentador recibe la
vista como interfaz (`IEquipoView`), se suscribe a sus eventos en el constructor y es
quien decide qué hacer. Por eso un formulario puede cambiar por completo sin tocar la
lógica: mientras cumpla la interfaz, el presentador sigue funcionando igual.

Las operaciones de negocio devuelven un `Resultado` en vez de lanzar excepciones. Un
préstamo rechazado porque el equipo ya está prestado no es un fallo del programa, es un
caso normal y esperado. `Resultado.Ok` y `Resultado.Fallo` llevan un booleano y un
mensaje, y el presentador solo tiene que mostrarlo y refrescar la vista si hubo éxito.

## Principios SOLID

- S (responsabilidad única): `PrestamoService` solo contiene reglas de negocio; cada
  presentador solo coordina su pantalla; cada repositorio solo almacena; cada
  formulario solo dibuja.
- O (abierto/cerrado): pasar de memoria a SQL es crear una clase nueva que implemente
  `IEquipoRepositorio` (u otra interfaz de repositorio), sin modificar `PrestamoService`
  ni los presentadores.
- L (sustitución de Liskov): `EquipoRepositorioEnMemoria` hereda de
  `RepositorioEnMemoria<Equipo>` y cualquier otra implementación de la interfaz puede
  ocupar su lugar sin romper a quien la consume.
- I (segregación de interfaces): las vistas están separadas (`IEquipoView`,
  `IUsuarioView`, `IPrestamoView`) y `IPrestamoRepositorio` añade solo lo que su cliente
  necesita (`ObtenerActivos`, `ObtenerPrestamoActivoPorEquipo`), sin cargar a los demás
  repositorios con esos métodos.
- D (inversión de dependencias): los presentadores y `PrestamoService` reciben
  interfaces por el constructor; las clases concretas se crean únicamente en
  `ContenedorDependencias`.

## Limitaciones conocidas

- No hay persistencia. Los datos se guardan en memoria y se pierden al cerrar la
  aplicación.
- No hay autenticación ni control de acceso de usuarios.
- No incluye pruebas automatizadas. La arquitectura las permite (la lógica está
  desacoplada de la UI), pero no se agregaron en esta entrega.
- Es un proyecto académico, no un producto de producción.

## Autor

Ricardo Pimentel.
