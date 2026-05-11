'Representa una materia registrada en el plan academico
Public Class Materia
    ' Identificador único de la materia.
    Public Property IdMateria As Integer

    ' Código académico de la materia.
    Public Property Codigo As String

    ' Nombre de la materia.
    Public Property Nombre As String

    ' Número de créditos académicos.
    Public Property Creditos As Integer

    ' Cantidad de horas semanales asignadas a la materia.
    Public Property IntensidadHorariaSemanal As Integer

    ' Semestre recomendado para cursar la materia.
    Public Property SemestreSugerido As Integer

    ' Cantidad de grupos asociados a la materia.
    Public Property CantidadGrupos As Integer

    ' Estado actual de la materia.
    Public Property EstadoMateria As EstadoMateria

    ' Identificador del plan académico al que pertenece la materia.
    Public Property IdPlanAcademico As Integer

    ' Constructor vacío para facilitar futuras operaciones con base de datos.
    Public Sub New()
        IdMateria = 0
        Codigo = ""
        Nombre = ""
        Creditos = 0
        IntensidadHorariaSemanal = 0
        SemestreSugerido = 0
        CantidadGrupos = 0
        Estado = EstadoMateria.Activa
        IdPlanAcademico = 0
    End Sub

End Class
