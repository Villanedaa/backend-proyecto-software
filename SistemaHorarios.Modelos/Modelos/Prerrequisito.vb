Public Class Prerrequisito

    ' Identificador único del registro de prerrequisito.
    Public Property IdPrerequisito As Integer

    ' Identificador de la materia que necesita cumplir un prerrequisito.
    Public Property IdMateria As Integer

    ' Identificador de la materia que debe aprobarse previamente.
    Public Property IdMateriaPrerequisito As Integer

    ' Indica si la relación de prerrequisito está activa.
    Public Property Activo As Boolean

    ' Constructor vacío para facilitar futuras operaciones con base de datos.
    Public Sub New()
        IdPrerequisito = 0
        IdMateria = 0
        IdMateriaPrerequisito = 0
        Activo = True
    End Sub
End Class
