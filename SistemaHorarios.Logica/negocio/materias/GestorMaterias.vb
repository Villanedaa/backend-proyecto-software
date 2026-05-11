Imports SistemaHorarios.Modelos

Public Class GestorMateria

    Private ReadOnly materias As List(Of Materia)
    Private ReadOnly validadorMateria As ValidadorMateria

    ' Inicializa el gestor con una lista temporal en memoria.
    Public Sub New()
        materias = New List(Of Materia)()
        validadorMateria = New ValidadorMateria()
    End Sub

    ' Crea una nueva materia en memoria.
    Public Function CrearMateria(materia As Materia) As List(Of String)

        Dim errores As List(Of String) = validadorMateria.Validar(materia)

        If materia Is Nothing Then
            Return errores
        End If

        If ExisteCodigoDuplicado(materia.Codigo, 0) Then
            errores.Add("Ya existe una materia registrada con el mismo código.")
        End If

        If errores.Count > 0 Then
            Return errores
        End If

        materia.IdMateria = GenerarNuevoId()
        materias.Add(materia)

        Return errores

    End Function

    ' Modifica una materia existente en memoria.
    Public Function ModificarMateria(materiaModificada As Materia) As List(Of String)

        Dim errores As List(Of String) = validadorMateria.Validar(materiaModificada)

        If materiaModificada Is Nothing Then
            Return errores
        End If

        Dim materiaActual As Materia = ObtenerMateriaPorId(materiaModificada.IdMateria)

        If materiaActual Is Nothing Then
            errores.Add("La materia que desea modificar no existe.")
            Return errores
        End If

        If ExisteCodigoDuplicado(materiaModificada.Codigo, materiaModificada.IdMateria) Then
            errores.Add("Ya existe otra materia registrada con el mismo código.")
        End If

        If errores.Count > 0 Then
            Return errores
        End If

        materiaActual.Codigo = materiaModificada.Codigo
        materiaActual.Nombre = materiaModificada.Nombre
        materiaActual.Creditos = materiaModificada.Creditos
        materiaActual.IntensidadHorariaSemanal = materiaModificada.IntensidadHorariaSemanal
        materiaActual.SemestreSugerido = materiaModificada.SemestreSugerido
        materiaActual.CantidadGrupos = materiaModificada.CantidadGrupos
        materiaActual.Estado = materiaModificada.Estado
        materiaActual.IdPlanAcademico = materiaModificada.IdPlanAcademico

        Return errores

    End Function

    ' Elimina una materia si existe y no está siendo usada.
    Public Function EliminarMateria(idMateria As Integer, materiaEnUso As Boolean) As List(Of String)

        Dim errores As New List(Of String)()

        If idMateria <= 0 Then
            errores.Add("El identificador de la materia no es válido.")
            Return errores
        End If

        Dim materia As Materia = ObtenerMateriaPorId(idMateria)

        If materia Is Nothing Then
            errores.Add("La materia que desea eliminar no existe.")
            Return errores
        End If

        If materiaEnUso Then
            errores.Add("No se puede eliminar la materia porque está siendo usada en grupos, horarios o reglas activas.")
            Return errores
        End If

        materias.Remove(materia)

        Return errores

    End Function

    ' Obtiene una materia por su identificador.
    Public Function ObtenerMateriaPorId(idMateria As Integer) As Materia

        If idMateria <= 0 Then
            Return Nothing
        End If

        For Each materia As Materia In materias
            If materia.IdMateria = idMateria Then
                Return materia
            End If
        Next

        Return Nothing

    End Function

    ' Obtiene una materia por su código.
    Public Function ObtenerMateriaPorCodigo(codigo As String) As Materia

        If String.IsNullOrWhiteSpace(codigo) Then
            Return Nothing
        End If

        For Each materia As Materia In materias
            If String.Equals(materia.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase) Then
                Return materia
            End If
        Next

        Return Nothing

    End Function

    ' Retorna una copia de las materias registradas.
    Public Function ListarMaterias() As List(Of Materia)
        Return New List(Of Materia)(materias)
    End Function

    ' Verifica si ya existe otra materia con el mismo código.
    Private Function ExisteCodigoDuplicado(codigo As String, idMateriaExcluir As Integer) As Boolean

        If String.IsNullOrWhiteSpace(codigo) Then
            Return False
        End If

        For Each materia As Materia In materias
            If materia.IdMateria <> idMateriaExcluir AndAlso
               String.Equals(materia.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase) Then

                Return True
            End If
        Next

        Return False

    End Function

    ' Genera un identificador temporal para pruebas.
    Private Function GenerarNuevoId() As Integer

        Dim mayorId As Integer = 0

        For Each materia As Materia In materias
            If materia.IdMateria > mayorId Then
                mayorId = materia.IdMateria
            End If
        Next

        Return mayorId + 1

    End Function

End Class