Imports SistemaHorarios.Modelos

Public Class ValidadorPrerequisito

    ' Valida los datos básicos de una relación de prerrequisito.
    Public Function Validar(prerequisito As Prerrequisito) As List(Of String)

        Dim errores As New List(Of String)()

        If prerequisito Is Nothing Then
            errores.Add("El prerrequisito no puede estar vacío.")
            Return errores
        End If

        ValidarIdMateria(prerequisito.IdMateria, errores)
        ValidarIdMateriaPrerequisito(prerequisito.IdMateriaPrerequisito, errores)
        ValidarMateriaDiferente(prerequisito.IdMateria, prerequisito.IdMateriaPrerequisito, errores)

        Return errores

    End Function

    ' Valida que la materia principal tenga un identificador válido.
    Private Sub ValidarIdMateria(idMateria As Integer, errores As List(Of String))
        AgregarErrorSi(idMateria <= 0, errores, "La materia principal no es válida.")
    End Sub

    ' Valida que la materia prerrequisito tenga un identificador válido.
    Private Sub ValidarIdMateriaPrerequisito(idMateriaPrerequisito As Integer, errores As List(Of String))
        AgregarErrorSi(idMateriaPrerequisito <= 0, errores, "La materia prerrequisito no es válida.")
    End Sub

    ' Valida que una materia no sea prerrequisito de sí misma.
    Private Sub ValidarMateriaDiferente(idMateria As Integer, idMateriaPrerequisito As Integer, errores As List(Of String))
        AgregarErrorSi(idMateria = idMateriaPrerequisito, errores, "Una materia no puede ser prerrequisito de sí misma.")
    End Sub

    ' Agrega un error cuando se cumple una condición inválida.
    Private Sub AgregarErrorSi(condicion As Boolean, errores As List(Of String), mensaje As String)

        If condicion Then
            errores.Add(mensaje)
        End If

    End Sub

End Class