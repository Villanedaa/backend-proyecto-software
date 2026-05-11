' =========================================================
' IMPORTACION DEL NAMESPACE DE MODELOS
' =========================================================
Imports SistemaHorarios.Modelos

' =========================================================
' NAMESPACE DEL MODULO DE NEGOCIO DE DOCENTES
' =========================================================
Namespace SistemaHorarios.Logica.Negocio.Docentes

    ' =====================================================
    ' CLASE VALIDADORDOCENTE
    ' =====================================================
    ' Contiene todas las validaciones relacionadas
    ' con los datos del docente.
    '
    ' Es utilizada por GestorDocente para validar
    ' antes de ejecutar cualquier operación CRUD.
    ' =====================================================
    Public Class ValidadorDocente

        ' =====================================================
        ' METODO: VALIDAR CAMPOS DEL DOCENTE
        ' =====================================================
        ' Valida todos los campos del objeto Docente.
        ' Se usa en Crear y Actualizar.
        '
        ' PARAMETROS:
        ' docente -> Objeto Docente a validar.
        '
        ' LANZA EXCEPCION si algún campo es inválido.
        ' =====================================================
        Public Sub ValidarCamposDocente(docente As Docente)

            ' =============================================
            ' VALIDAR QUE EL OBJETO NO SEA NULO
            ' =============================================
            If docente Is Nothing Then
                Throw New Exception(
                    "La información del docente es obligatoria.")
            End If

            ' =============================================
            ' VALIDAR IDENTIFICACION
            ' =============================================
            Me.ValidarIdentificacion(docente.Identificacion)

            ' =============================================
            ' VALIDAR NOMBRE
            ' =============================================
            Me.ValidarNombre(docente.Nombre)

            ' =============================================
            ' VALIDAR CORREO ELECTRONICO
            ' =============================================
            Me.ValidarCorreoElectronico(docente.CorreoElectronico)

        End Sub

        ' =====================================================
        ' METODO: VALIDAR IDENTIFICACION
        ' =====================================================
        ' Valida que la identificación cumpla con las
        ' reglas de negocio.
        '
        ' PARAMETROS:
        ' identificacion -> Identificación a validar.
        '
        ' VALIDACIONES:
        ' - No puede estar vacía.
        ' - Solo puede contener números.
        ' - Debe tener entre 6 y 15 dígitos.
        ' =====================================================
        Public Sub ValidarIdentificacion(identificacion As String)

            ' =============================================
            ' VALIDAR QUE NO ESTE VACIA
            ' =============================================
            If String.IsNullOrWhiteSpace(identificacion) Then
                Throw New Exception(
                    "La identificación es obligatoria.")
            End If

            ' =============================================
            ' VALIDAR QUE SOLO CONTENGA NUMEROS
            ' =============================================
            If Not identificacion.All(
                Function(c) Char.IsDigit(c)) Then

                Throw New Exception(
                    "La identificación solo puede contener números.")
            End If

            ' =============================================
            ' VALIDAR LONGITUD
            ' Entre 6 y 15 dígitos
            ' =============================================
            If identificacion.Length < 6 OrElse
               identificacion.Length > 15 Then

                Throw New Exception(
                    "La identificación debe tener entre 6 y 15 dígitos.")
            End If

        End Sub

        ' =====================================================
        ' METODO: VALIDAR NOMBRE
        ' =====================================================
        ' Valida que el nombre cumpla con las
        ' reglas de negocio.
        '
        ' PARAMETROS:
        ' nombre -> Nombre a validar.
        '
        ' VALIDACIONES:
        ' - No puede estar vacío.
        ' - Solo puede contener letras y espacios.
        ' - Debe tener entre 3 y 100 caracteres.
        ' =====================================================
        Public Sub ValidarNombre(nombre As String)

            ' =============================================
            ' VALIDAR QUE NO ESTE VACIO
            ' =============================================
            If String.IsNullOrWhiteSpace(nombre) Then
                Throw New Exception(
                    "El nombre es obligatorio.")
            End If

            ' =============================================
            ' VALIDAR QUE SOLO CONTENGA
            ' LETRAS Y ESPACIOS
            ' =============================================
            If Not nombre.All(
                Function(c) Char.IsLetter(c) OrElse
                            Char.IsWhiteSpace(c)) Then

                Throw New Exception(
                    "El nombre solo puede contener letras y espacios.")
            End If

            ' =============================================
            ' VALIDAR LONGITUD
            ' Entre 3 y 100 caracteres
            ' =============================================
            If nombre.Trim().Length < 3 OrElse
               nombre.Trim().Length > 100 Then

                Throw New Exception(
                    "El nombre debe tener entre 3 y 100 caracteres.")
            End If

        End Sub

        ' =====================================================
        ' METODO: VALIDAR CORREO ELECTRONICO
        ' =====================================================
        ' Valida que el correo electrónico cumpla con
        ' las reglas de negocio.
        '
        ' PARAMETROS:
        ' correo -> Correo electrónico a validar.
        '
        ' VALIDACIONES:
        ' - No puede estar vacío.
        ' - Debe contener @.
        ' - Debe tener un punto después del @.
        ' - No puede contener espacios.
        ' - Máximo 150 caracteres.
        ' =====================================================
        Public Sub ValidarCorreoElectronico(correo As String)

            ' =============================================
            ' VALIDAR QUE NO ESTE VACIO
            ' =============================================
            If String.IsNullOrWhiteSpace(correo) Then
                Throw New Exception(
                    "El correo electrónico es obligatorio.")
            End If

            ' =============================================
            ' VALIDAR QUE NO CONTENGA ESPACIOS
            ' =============================================
            If correo.Contains(" ") Then
                Throw New Exception(
                    "El correo electrónico no puede contener espacios.")
            End If

            ' =============================================
            ' VALIDAR QUE CONTENGA @
            ' =============================================
            If Not correo.Contains("@") Then
                Throw New Exception(
                    "El correo electrónico debe contener '@'.")
            End If

            ' =============================================
            ' VALIDAR FORMATO: usuario@dominio.com
            ' Separar por @ y verificar que el dominio
            ' contenga al menos un punto
            ' =============================================
            Dim partes = correo.Split("@"c)

            If partes.Length <> 2 OrElse
               Not partes(1).Contains(".") Then

                Throw New Exception(
                    "El correo electrónico no tiene un formato válido." &
                    " Ejemplo: usuario@dominio.com")
            End If

            ' =============================================
            ' VALIDAR QUE EL DOMINIO NO EMPIECE
            ' NI TERMINE CON UN PUNTO
            ' =============================================
            If partes(1).StartsWith(".") OrElse
               partes(1).EndsWith(".") Then

                Throw New Exception(
                    "El dominio del correo electrónico no es válido.")
            End If

            ' =============================================
            ' VALIDAR LONGITUD MAXIMA
            ' Máximo 150 caracteres
            ' =============================================
            If correo.Length > 150 Then
                Throw New Exception(
                    "El correo electrónico no puede superar" &
                    " los 150 caracteres.")
            End If

        End Sub

    End Class

End Namespace