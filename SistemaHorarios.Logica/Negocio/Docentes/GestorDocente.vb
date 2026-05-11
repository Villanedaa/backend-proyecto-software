' =========================================================
' IMPORTACION DEL NAMESPACE DE MODELOS
' =========================================================
Imports SistemaHorarios.Modelos

' =========================================================
' NAMESPACE DEL MODULO DE NEGOCIO DE DOCENTES
' =========================================================
Namespace SistemaHorarios.Logica.Negocio.Docentes

    ' =====================================================
    ' CLASE GESTORDOCENTE
    ' =====================================================
    ' Contiene la lógica CRUD de los docentes.
    ' Delega todas las validaciones a ValidadorDocente.
    ' =====================================================
    Public Class GestorDocente

        ' =================================================
        ' LISTA TEMPORAL DE DOCENTES
        ' =================================================
        ' Simula una base de datos mientras se desarrolla
        ' el backend.
        ' =================================================
        Private Shared ListaDocentes As New List(Of Docente)

        ' =================================================
        ' INSTANCIA DEL VALIDADOR
        ' =================================================
        ' Se utiliza para validar los datos del docente
        ' antes de ejecutar cualquier operación CRUD.
        ' =================================================
        Private ReadOnly Validador As New ValidadorDocente

        ' =====================================================
        ' METODO: CREAR DOCENTE
        ' =====================================================
        ' Registra un nuevo docente en la lista.
        '
        ' PARAMETROS:
        ' docente -> Objeto Docente con la información
        '            del nuevo docente.
        '
        ' RETORNA:
        ' True -> Si el docente fue creado correctamente.
        ' =====================================================
        Public Function CrearDocente(docente As Docente) As Boolean

            ' =============================================
            ' VALIDAR TODOS LOS CAMPOS DEL DOCENTE
            ' Delega la validación a ValidadorDocente
            ' =============================================
            Validador.ValidarCamposDocente(docente)

            ' =============================================
            ' VALIDAR QUE LA IDENTIFICACION
            ' NO ESTE DUPLICADA
            ' =============================================
            Dim docenteExistente = ListaDocentes.FirstOrDefault(
                Function(d) d.Identificacion =
                    docente.Identificacion)

            If docenteExistente IsNot Nothing Then
                Throw New Exception(
                    "Ya existe un docente con esa identificación.")
            End If

            ' =============================================
            ' VALIDAR QUE EL CORREO NO ESTE DUPLICADO
            ' =============================================
            Dim correoExistente = ListaDocentes.FirstOrDefault(
                Function(d) d.CorreoElectronico.ToLower() =
                    docente.CorreoElectronico.ToLower())

            If correoExistente IsNot Nothing Then
                Throw New Exception(
                    "Ya existe un docente con ese correo electrónico.")
            End If

            ' =============================================
            ' GENERAR ID AUTOMATICO
            ' =============================================
            docente.Id = ListaDocentes.Count + 1

            ' =============================================
            ' ASIGNAR ESTADO ACTIVO POR DEFECTO
            ' =============================================
            docente.Estado = True

            ' =============================================
            ' NORMALIZAR NOMBRE
            ' Elimina espacios al inicio y al final
            ' =============================================
            docente.Nombre = docente.Nombre.Trim()

            ' =============================================
            ' NORMALIZAR CORREO A MINUSCULAS
            ' =============================================
            docente.CorreoElectronico =
                docente.CorreoElectronico.ToLower().Trim()

            ' =============================================
            ' AGREGAR DOCENTE A LA LISTA
            ' =============================================
            ListaDocentes.Add(docente)

            ' =============================================
            ' RETORNAR OPERACION EXITOSA
            ' =============================================
            Return True

        End Function

        ' =====================================================
        ' METODO: ACTUALIZAR DOCENTE
        ' =====================================================
        ' Modifica la información de un docente existente.
        '
        ' PARAMETROS:
        ' docenteActualizado -> Objeto con la nueva
        '                       información del docente.
        '
        ' RETORNA:
        ' True -> Si la actualización fue exitosa.
        ' =====================================================
        Public Function ActualizarDocente(
            docenteActualizado As Docente) As Boolean

            ' =============================================
            ' VALIDAR TODOS LOS CAMPOS DEL DOCENTE
            ' Delega la validación a ValidadorDocente
            ' =============================================
            Validador.ValidarCamposDocente(docenteActualizado)

            ' =============================================
            ' BUSCAR EL DOCENTE POR IDENTIFICACION
            ' =============================================
            Dim docente = ListaDocentes.FirstOrDefault(
                Function(d) d.Identificacion =
                    docenteActualizado.Identificacion)

            ' =============================================
            ' VALIDAR QUE EL DOCENTE EXISTA
            ' =============================================
            If docente Is Nothing Then
                Throw New Exception(
                    "Docente no encontrado.")
            End If

            ' =============================================
            ' VALIDAR QUE EL NUEVO CORREO NO ESTE
            ' EN USO POR OTRO DOCENTE DIFERENTE
            ' =============================================
            Dim correoEnUso = ListaDocentes.FirstOrDefault(
                Function(d) d.CorreoElectronico.ToLower() =
                    docenteActualizado.CorreoElectronico.ToLower() AndAlso
                    d.Identificacion <> docenteActualizado.Identificacion)

            If correoEnUso IsNot Nothing Then
                Throw New Exception(
                    "El correo electrónico ya está en uso por otro docente.")
            End If

            ' =============================================
            ' ACTUALIZAR NOMBRE (normalizado)
            ' =============================================
            docente.Nombre = docenteActualizado.Nombre.Trim()

            ' =============================================
            ' ACTUALIZAR CORREO (normalizado a minúsculas)
            ' =============================================
            docente.CorreoElectronico =
                docenteActualizado.CorreoElectronico.ToLower().Trim()

            ' =============================================
            ' ACTUALIZAR ESTADO
            ' =============================================
            docente.Estado = docenteActualizado.Estado

            ' =============================================
            ' RETORNAR OPERACION EXITOSA
            ' =============================================
            Return True

        End Function

        ' =====================================================
        ' METODO: ELIMINAR DOCENTE
        ' =====================================================
        ' Realiza una eliminación lógica del docente.
        ' NO elimina el registro físicamente,
        ' solo cambia Estado a False.
        '
        ' PARAMETROS:
        ' identificacion -> Identificación del docente.
        '
        ' RETORNA:
        ' True -> Si la eliminación fue exitosa.
        ' =====================================================
        Public Function EliminarDocente(
            identificacion As String) As Boolean

            ' =============================================
            ' VALIDAR LA IDENTIFICACION
            ' Delega la validación a ValidadorDocente
            ' =============================================
            Validador.ValidarIdentificacion(identificacion)

            ' =============================================
            ' BUSCAR DOCENTE POR IDENTIFICACION
            ' =============================================
            Dim docente = ListaDocentes.FirstOrDefault(
                Function(d) d.Identificacion = identificacion)

            ' =============================================
            ' VALIDAR QUE EL DOCENTE EXISTA
            ' =============================================
            If docente Is Nothing Then
                Throw New Exception(
                    "Docente no encontrado.")
            End If

            ' =============================================
            ' VALIDAR SI YA ESTA ELIMINADO
            ' =============================================
            If docente.Estado = False Then
                Throw New Exception(
                    "El docente ya fue eliminado.")
            End If

            ' =============================================
            ' ELIMINACION LOGICA
            ' Cambia el estado a False en lugar de
            ' borrar el registro físicamente
            ' =============================================
            docente.Estado = False

            ' =============================================
            ' RETORNAR OPERACION EXITOSA
            ' =============================================
            Return True

        End Function

        ' =====================================================
        ' METODO: OBTENER DOCENTE POR IDENTIFICACION
        ' =====================================================
        ' Busca y retorna un docente por su identificación.
        '
        ' PARAMETROS:
        ' identificacion -> Identificación del docente.
        '
        ' RETORNA:
        ' Objeto tipo Docente.
        ' =====================================================
        Public Function ObtenerDocentePorIdentificacion(
            identificacion As String) As Docente

            ' =============================================
            ' VALIDAR LA IDENTIFICACION
            ' Delega la validación a ValidadorDocente
            ' =============================================
            Validador.ValidarIdentificacion(identificacion)

            ' =============================================
            ' BUSCAR DOCENTE
            ' =============================================
            Dim docente = ListaDocentes.FirstOrDefault(
                Function(d) d.Identificacion = identificacion)

            ' =============================================
            ' VALIDAR QUE EL DOCENTE EXISTA
            ' =============================================
            If docente Is Nothing Then
                Throw New Exception(
                    "Docente no encontrado.")
            End If

            ' =============================================
            ' RETORNAR DOCENTE ENCONTRADO
            ' =============================================
            Return docente

        End Function

        ' =====================================================
        ' METODO: OBTENER TODOS LOS DOCENTES
        ' =====================================================
        ' Retorna la lista completa de docentes registrados.
        '
        ' RETORNA:
        ' List(Of Docente)
        ' =====================================================
        Public Function ObtenerTodosLosDocentes() _
            As List(Of Docente)

            ' =============================================
            ' RETORNAR LISTA COMPLETA
            ' =============================================
            Return ListaDocentes

        End Function

    End Class

End Namespace