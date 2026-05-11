' =========================================================
' IMPORTACION DEL NAMESPACE DE MODELOS
' =========================================================
' Permite utilizar la clase Docente

Imports SistemaHorarios.Modelos

' =========================================================
' NAMESPACE DEL MODULO DE NEGOCIO DE DOCENTES
' =========================================================
Namespace SistemaHorarios.Logica.Negocio.Docentes

    ' =====================================================
    ' CLASE GESTORDOCENTE
    ' =====================================================
    ' Esta clase contiene toda la lógica de negocio
    ' relacionada con los docentes.
    '
    ' Funcionalidades:
    ' - Crear docentes
    ' - Actualizar docentes
    ' - Eliminar docentes
    ' - Consultar docentes
    ' =====================================================
    Public Class GestorDocente

        ' =================================================
        ' LISTA TEMPORAL DE DOCENTES
        ' =================================================
        ' Simula una base de datos mientras se desarrolla
        ' el backend.
        '
        ' Shared:
        ' La lista será compartida por todas las instancias
        ' de la clase.
        ' =================================================
        Private Shared ListaDocentes As New List(Of Docente)

        ' =====================================================
        ' METODO: CREAR DOCENTE
        ' =====================================================
        ' Este método permite registrar un nuevo docente.
        '
        ' PARAMETROS:
        ' docente -> Objeto tipo Docente con la información
        '             del nuevo docente.
        '
        ' RETORNA:
        ' True -> Si el docente fue creado correctamente.
        '
        ' VALIDACIONES:
        ' - El objeto docente no puede ser nulo.
        ' - La identificación no puede repetirse.
        ' =====================================================
        Public Function CrearDocente(
            docente As Docente) As Boolean

            ' =============================================
            ' VALIDAR QUE EL OBJETO NO SEA NULO
            ' =============================================
            If docente Is Nothing Then

                Throw New Exception(
                    "La información del docente es obligatoria.")

            End If

            ' =============================================
            ' BUSCAR SI YA EXISTE UN DOCENTE
            ' CON LA MISMA IDENTIFICACION
            ' =============================================
            Dim docenteExistente = ListaDocentes.FirstOrDefault(
                Function(d) d.Identificacion =
                    docente.Identificacion)

            ' =============================================
            ' VALIDAR IDENTIFICACION DUPLICADA
            ' =============================================
            If docenteExistente IsNot Nothing Then

                Throw New Exception(
                    "Ya existe un docente con esa identificación.")

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
        ' Permite modificar la información de un docente.
        '
        ' PARAMETROS:
        ' docenteActualizado -> Objeto con la nueva
        '                       información del docente.
        '
        ' RETORNA:
        ' True -> Si la actualización fue exitosa.
        '
        ' VALIDACIONES:
        ' - El objeto no puede ser nulo.
        ' - El docente debe existir.
        ' =====================================================
        Public Function ActualizarDocente(
            docenteActualizado As Docente) As Boolean

            ' =============================================
            ' VALIDAR QUE EL OBJETO NO SEA NULO
            ' =============================================
            If docenteActualizado Is Nothing Then

                Throw New Exception(
                    "La información del docente es obligatoria.")

            End If

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
            ' ACTUALIZAR NOMBRE
            ' =============================================
            docente.Nombre =
                docenteActualizado.Nombre

            ' =============================================
            ' ACTUALIZAR CORREO ELECTRONICO
            ' =============================================
            docente.CorreoElectronico =
                docenteActualizado.CorreoElectronico

            ' =============================================
            ' ACTUALIZAR ESTADO
            ' =============================================
            docente.Estado =
                docenteActualizado.Estado

            ' =============================================
            ' RETORNAR OPERACION EXITOSA
            ' =============================================
            Return True

        End Function

        ' =====================================================
        ' METODO: ELIMINAR DOCENTE
        ' =====================================================
        ' Realiza una eliminación lógica del docente.
        '
        ' NO elimina el registro físicamente.
        ' Solamente cambia el estado a FALSE.
        '
        ' PARAMETROS:
        ' identificacion -> Identificación del docente.
        '
        ' RETORNA:
        ' True -> Si la eliminación fue exitosa.
        '
        ' VALIDACIONES:
        ' - La identificación es obligatoria.
        ' - El docente debe existir.
        ' - El docente no debe estar eliminado.
        ' =====================================================
        Public Function EliminarDocente(
            identificacion As String) As Boolean

            ' =============================================
            ' VALIDAR IDENTIFICACION VACIA
            ' =============================================
            If String.IsNullOrWhiteSpace(
                identificacion) Then

                Throw New Exception(
                    "La identificación es obligatoria.")

            End If

            ' =============================================
            ' BUSCAR DOCENTE
            ' =============================================
            Dim docente = ListaDocentes.FirstOrDefault(
                Function(d) d.Identificacion =
                    identificacion)

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
        ' Permite buscar un docente utilizando
        ' la identificación.
        '
        ' PARAMETROS:
        ' identificacion -> Identificación del docente.
        '
        ' RETORNA:
        ' Objeto tipo Docente.
        '
        ' VALIDACIONES:
        ' - La identificación es obligatoria.
        ' - El docente debe existir.
        ' =====================================================
        Public Function ObtenerDocentePorIdentificacion(
            identificacion As String) As Docente

            ' =============================================
            ' VALIDAR IDENTIFICACION VACIA
            ' =============================================
            If String.IsNullOrWhiteSpace(
                identificacion) Then

                Throw New Exception(
                    "La identificación es obligatoria.")

            End If

            ' =============================================
            ' BUSCAR DOCENTE
            ' =============================================
            Dim docente = ListaDocentes.FirstOrDefault(
                Function(d) d.Identificacion =
                    identificacion)

            ' =============================================
            ' VALIDAR QUE EL DOCENTE EXISTA
            ' =============================================
            If docente Is Nothing Then

                Throw New Exception(
                    "Docente no encontrado.")

            End If

            ' =============================================
            ' RETORNAR DOCENTE
            ' =============================================
            Return docente

        End Function

        ' =====================================================
        ' METODO: OBTENER TODOS LOS DOCENTES
        ' =====================================================
        ' Retorna la lista completa de docentes.
        '
        ' RETORNA:
        ' List(Of Docente)
        ' =====================================================
        Public Function ObtenerTodosLosDocentes() _
            As List(Of Docente)

            Return ListaDocentes

        End Function

    End Class

End Namespace