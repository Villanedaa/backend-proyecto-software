Imports SistemaHorarios.Modelos

Public Class GestorPrerequisito

    Private ReadOnly prerequisitos As List(Of Prerequisito)
    Private ReadOnly validadorPrerequisito As ValidadorPrerequisito

    ' Inicializa el gestor con una lista temporal en memoria.
    Public Sub New()
        prerequisitos = New List(Of Prerequisito)()
        validadorPrerequisito = New ValidadorPrerequisito()
    End Sub

    ' Agrega un prerrequisito a una materia.
    Public Function AgregarPrerequisito(prerequisito As Prerequisito, gestorMateria As GestorMateria) As List(Of String)

        Dim errores As List(Of String) = validadorPrerequisito.Validar(prerequisito)

        If prerequisito Is Nothing Then
            Return errores
        End If

        If gestorMateria Is Nothing Then
            errores.Add("No se puede validar la materia porque no existe un gestor de materias.")
            Return errores
        End If

        If gestorMateria.ObtenerMateriaPorId(prerequisito.IdMateria) Is Nothing Then
            errores.Add("La materia principal no existe.")
        End If

        If gestorMateria.ObtenerMateriaPorId(prerequisito.IdMateriaPrerequisito) Is Nothing Then
            errores.Add("La materia prerrequisito no existe.")
        End If

        If ExistePrerequisito(prerequisito.IdMateria, prerequisito.IdMateriaPrerequisito) Then
            errores.Add("El prerrequisito ya está registrado para esta materia.")
        End If

        If ExisteRelacionCircularDirecta(prerequisito.IdMateria, prerequisito.IdMateriaPrerequisito) Then
            errores.Add("No se puede crear una relación circular entre materias.")
        End If

        If errores.Count > 0 Then
            Return errores
        End If

        prerequisito.IdPrerequisito = GenerarNuevoId()
        prerequisito.Activo = True
        prerequisitos.Add(prerequisito)

        Return errores

    End Function

    ' Elimina lógicamente un prerrequisito.
    Public Function EliminarPrerequisito(idPrerequisito As Integer) As List(Of String)

        Dim errores As New List(Of String)()

        If idPrerequisito <= 0 Then
            errores.Add("El identificador del prerrequisito no es válido.")
            Return errores
        End If

        Dim prerequisito As Prerequisito = ObtenerPrerequisitoPorId(idPrerequisito)

        If prerequisito Is Nothing Then
            errores.Add("El prerrequisito que desea eliminar no existe.")
            Return errores
        End If

        prerequisito.Activo = False

        Return errores

    End Function

    ' Obtiene un prerrequisito por su identificador.
    Public Function ObtenerPrerequisitoPorId(idPrerequisito As Integer) As Prerequisito

        If idPrerequisito <= 0 Then
            Return Nothing
        End If

        For Each prerequisito As Prerequisito In prerequisitos
            If prerequisito.IdPrerequisito = idPrerequisito Then
                Return prerequisito
            End If
        Next

        Return Nothing

    End Function

    ' Lista los prerrequisitos asociados a una materia.
    Public Function ListarPrerequisitosPorMateria(idMateria As Integer) As List(Of Prerequisito)

        Dim resultado As New List(Of Prerequisito)()

        If idMateria <= 0 Then
            Return resultado
        End If

        For Each prerequisito As Prerequisito In prerequisitos
            If prerequisito.IdMateria = idMateria AndAlso prerequisito.Activo Then
                resultado.Add(prerequisito)
            End If
        Next

        Return resultado

    End Function

    ' Retorna una copia de todos los prerrequisitos registrados.
    Public Function ListarPrerequisitos() As List(Of Prerequisito)
        Return New List(Of Prerequisito)(prerequisitos)
    End Function

    ' Verifica si ya existe un prerrequisito activo entre dos materias.
    Public Function ExistePrerequisito(idMateria As Integer, idMateriaPrerequisito As Integer) As Boolean

        For Each prerequisito As Prerequisito In prerequisitos

            If prerequisito.IdMateria = idMateria AndAlso
               prerequisito.IdMateriaPrerequisito = idMateriaPrerequisito AndAlso
               prerequisito.Activo Then

                Return True
            End If

        Next

        Return False

    End Function

    ' Valida si existe una relación circular directa entre dos materias.
    Private Function ExisteRelacionCircularDirecta(idMateria As Integer, idMateriaPrerequisito As Integer) As Boolean

        For Each prerequisito As Prerequisito In prerequisitos

            If prerequisito.IdMateria = idMateriaPrerequisito AndAlso
               prerequisito.IdMateriaPrerequisito = idMateria AndAlso
               prerequisito.Activo Then

                Return True
            End If

        Next

        Return False

    End Function

    ' Genera un identificador temporal para pruebas.
    Private Function GenerarNuevoId() As Integer

        Dim mayorId As Integer = 0

        For Each prerequisito As Prerequisito In prerequisitos
            If prerequisito.IdPrerequisito > mayorId Then
                mayorId = prerequisito.IdPrerequisito
            End If
        Next

        Return mayorId + 1

    End Function

End Class