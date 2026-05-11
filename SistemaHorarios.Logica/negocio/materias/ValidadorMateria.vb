Imports SistemaHorarios.Modelos

Public Class ValidadorMateria

    ' Valida los datos principales de una materia y devuelve los errores encontrados.
    Public Function Validar(materia As Materia) As List(Of String)

        Dim errores As New List(Of String)()

        If materia Is Nothing Then
            errores.Add("La materia no puede estar vacía.")
            Return errores
        End If

        ValidarCodigo(materia.Codigo, errores)
        ValidarNombre(materia.Nombre, errores)
        ValidarCreditos(materia.Creditos, errores)
        ValidarIntensidadHoraria(materia.IntensidadHorariaSemanal, errores)
        ValidarSemestreSugerido(materia.SemestreSugerido, errores)
        ValidarCantidadGrupos(materia.CantidadGrupos, errores)
        ValidarPlanAcademico(materia.IdPlanAcademico, errores)
        ValidarEstado(materia.Estado, errores)

        Return errores

    End Function

    ' Valida que el código de la materia sea obligatorio.
    Private Sub ValidarCodigo(codigo As String, errores As List(Of String))
        AgregarErrorSi(String.IsNullOrWhiteSpace(codigo), errores, "El código de la materia es obligatorio.")
    End Sub

    ' Valida que el nombre de la materia sea obligatorio.
    Private Sub ValidarNombre(nombre As String, errores As List(Of String))
        AgregarErrorSi(String.IsNullOrWhiteSpace(nombre), errores, "El nombre de la materia es obligatorio.")
    End Sub

    ' Valida que los créditos sean mayores a cero.
    Private Sub ValidarCreditos(creditos As Integer, errores As List(Of String))
        AgregarErrorSi(creditos <= 0, errores, "El número de créditos debe ser mayor a cero.")
    End Sub

    ' Valida que la intensidad horaria semanal sea mayor a cero.
    Private Sub ValidarIntensidadHoraria(intensidadHoraria As Integer, errores As List(Of String))
        AgregarErrorSi(intensidadHoraria <= 0, errores, "La intensidad horaria semanal debe ser mayor a cero.")
    End Sub

    ' Valida que el semestre sugerido sea mayor a cero.
    Private Sub ValidarSemestreSugerido(semestreSugerido As Integer, errores As List(Of String))
        AgregarErrorSi(semestreSugerido <= 0, errores, "El semestre sugerido debe ser mayor a cero.")
    End Sub

    ' Valida que la cantidad de grupos no sea negativa.
    Private Sub ValidarCantidadGrupos(cantidadGrupos As Integer, errores As List(Of String))
        AgregarErrorSi(cantidadGrupos < 0, errores, "La cantidad de grupos no puede ser negativa.")
    End Sub

    ' Valida que la materia esté asociada a un plan académico.
    Private Sub ValidarPlanAcademico(idPlanAcademico As Integer, errores As List(Of String))
        AgregarErrorSi(idPlanAcademico <= 0, errores, "La materia debe estar asociada a un plan académico.")
    End Sub

    ' Valida que el estado de la materia exista dentro de los estados permitidos.
    Private Sub ValidarEstado(estado As EstadoMateria, errores As List(Of String))
        AgregarErrorSi(Not [Enum].IsDefined(GetType(EstadoMateria), estado), errores, "El estado de la materia no es válido.")
    End Sub

    ' Verifica si ya existe una materia con el mismo código.
    Public Function ExisteCodigoDuplicado(materias As List(Of Materia), codigo As String) As Boolean

        If materias Is Nothing OrElse materias.Count = 0 Then
            Return False
        End If

        If String.IsNullOrWhiteSpace(codigo) Then
            Return False
        End If

        For Each materia As Materia In materias
            If String.Equals(materia.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next

        Return False

    End Function

    ' Agrega un mensaje de error cuando una condición no se cumple.
    Private Sub AgregarErrorSi(condicion As Boolean, errores As List(Of String), mensaje As String)

        If condicion Then
            errores.Add(mensaje)
        End If

    End Sub

End Class
