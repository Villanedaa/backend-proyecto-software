' =========================================================
' IMPORTACION DE NAMESPACES NECESARIOS
' =========================================================
Imports SistemaHorarios.Logica.Negocio.Docentes
Imports SistemaHorarios.Modelos

' =========================================================
' MODULO DE PRUEBAS DE DOCENTE
' =========================================================
' Prueba todas las operaciones CRUD del gestor de docentes,
' incluyendo casos exitosos y casos de error
' para verificar que las validaciones funcionan.
' =========================================================
Module PruebaDocente

    Public Sub Ejecutar()

        Console.WriteLine("=========================================")
        Console.WriteLine(" INICIO DE PRUEBAS - MODULO DOCENTE")
        Console.WriteLine("=========================================")
        Console.WriteLine()

        ' =====================================================
        ' BLOQUE 1: PRUEBAS DE VALIDACION AL CREAR
        ' =====================================================
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBAS DE VALIDACION - CREAR DOCENTE")
        Console.WriteLine("-----------------------------------------")

        ' =============================================
        ' PRUEBA 1: IDENTIFICACION VACIA
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 1: Identificacion vacia")
        ProbarCrear("", "Sebastian Villareal", "sebastian@gmail.com")

        ' =============================================
        ' PRUEBA 2: IDENTIFICACION CON LETRAS
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 2: Identificacion con letras")
        ProbarCrear("ABC123", "Sebastian Villareal", "sebastian@gmail.com")

        ' =============================================
        ' PRUEBA 3: IDENTIFICACION MUY CORTA
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 3: Identificacion muy corta (menos de 6 digitos)")
        ProbarCrear("123", "Sebastian Villareal", "sebastian@gmail.com")

        ' =============================================
        ' PRUEBA 4: NOMBRE VACIO
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 4: Nombre vacio")
        ProbarCrear("123456", "", "sebastian@gmail.com")

        ' =============================================
        ' PRUEBA 5: NOMBRE CON NUMEROS
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 5: Nombre con numeros")
        ProbarCrear("123456", "Sebastian123", "sebastian@gmail.com")

        ' =============================================
        ' PRUEBA 6: NOMBRE MUY CORTO
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 6: Nombre muy corto (menos de 3 caracteres)")
        ProbarCrear("123456", "Se", "sebastian@gmail.com")

        ' =============================================
        ' PRUEBA 7: CORREO SIN ARROBA
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 7: Correo sin @")
        ProbarCrear("123456", "Sebastian Villareal", "sebastiangmail.com")

        ' =============================================
        ' PRUEBA 8: CORREO SIN DOMINIO
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 8: Correo sin dominio (sin punto despues del @)")
        ProbarCrear("123456", "Sebastian Villareal", "sebastian@gmailcom")

        ' =============================================
        ' PRUEBA 9: CORREO CON ESPACIOS
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 9: Correo con espacios")
        ProbarCrear("123456", "Sebastian Villareal", "sebastian @gmail.com")

        ' =============================================
        ' PRUEBA 10: CORREO VACIO
        ' =============================================
        Console.WriteLine()
        Console.WriteLine("PRUEBA 10: Correo vacio")
        ProbarCrear("123456", "Sebastian Villareal", "")

        ' =====================================================
        ' BLOQUE 2: PRUEBA EXITOSA - CREAR DOCENTE
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA EXITOSA - CREAR DOCENTE")
        Console.WriteLine("-----------------------------------------")

        Dim gestor As New GestorDocente()

        Try

            ' =============================================
            ' CREAR DOCENTE VALIDO
            ' =============================================
            Dim docente As New Docente()
            docente.Identificacion = "123456"
            docente.Nombre = "Sebastian Villareal"
            docente.CorreoElectronico = "sebastian@gmail.com"

            gestor.CrearDocente(docente)
            Console.WriteLine("OK -> Docente creado correctamente.")

        Catch ex As Exception
            Console.WriteLine("ERROR -> " & ex.Message)
        End Try

        ' =====================================================
        ' BLOQUE 3: PRUEBA - IDENTIFICACION DUPLICADA
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA - IDENTIFICACION DUPLICADA")
        Console.WriteLine("-----------------------------------------")

        Try

            ' =============================================
            ' INTENTAR CREAR DOCENTE CON LA MISMA
            ' IDENTIFICACION
            ' =============================================
            Dim docenteDuplicado As New Docente()
            docenteDuplicado.Identificacion = "123456"
            docenteDuplicado.Nombre = "Otro Docente"
            docenteDuplicado.CorreoElectronico = "otro@gmail.com"

            gestor.CrearDocente(docenteDuplicado)
            Console.WriteLine("OK -> Docente creado correctamente.")

        Catch ex As Exception
            Console.WriteLine("OK -> Validacion correcta: " & ex.Message)
        End Try

        ' =====================================================
        ' BLOQUE 4: PRUEBA - CORREO DUPLICADO
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA - CORREO DUPLICADO")
        Console.WriteLine("-----------------------------------------")

        Try

            ' =============================================
            ' INTENTAR CREAR DOCENTE CON EL MISMO CORREO
            ' =============================================
            Dim docenteCorreoDuplicado As New Docente()
            docenteCorreoDuplicado.Identificacion = "654321"
            docenteCorreoDuplicado.Nombre = "Otro Docente"
            docenteCorreoDuplicado.CorreoElectronico = "sebastian@gmail.com"

            gestor.CrearDocente(docenteCorreoDuplicado)
            Console.WriteLine("OK -> Docente creado correctamente.")

        Catch ex As Exception
            Console.WriteLine("OK -> Validacion correcta: " & ex.Message)
        End Try

        ' =====================================================
        ' BLOQUE 5: PRUEBA EXITOSA - CONSULTAR DOCENTE
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA EXITOSA - CONSULTAR DOCENTE")
        Console.WriteLine("-----------------------------------------")

        Try

            ' =============================================
            ' CONSULTAR DOCENTE EXISTENTE
            ' =============================================
            Dim docenteConsultado As Docente =
                gestor.ObtenerDocentePorIdentificacion("123456")

            Console.WriteLine("OK -> Docente encontrado.")
            Console.WriteLine("      Nombre : " & docenteConsultado.Nombre)
            Console.WriteLine("      Correo : " & docenteConsultado.CorreoElectronico)
            Console.WriteLine("      Estado : " & docenteConsultado.Estado.ToString())

        Catch ex As Exception
            Console.WriteLine("ERROR -> " & ex.Message)
        End Try

        ' =====================================================
        ' BLOQUE 6: PRUEBA - CONSULTAR DOCENTE INEXISTENTE
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA - CONSULTAR DOCENTE INEXISTENTE")
        Console.WriteLine("-----------------------------------------")

        Try

            ' =============================================
            ' CONSULTAR CON IDENTIFICACION QUE NO EXISTE
            ' =============================================
            Dim docenteInexistente As Docente =
                gestor.ObtenerDocentePorIdentificacion("999999")

            Console.WriteLine("OK -> Docente encontrado.")

        Catch ex As Exception
            Console.WriteLine("OK -> Validacion correcta: " & ex.Message)
        End Try

        ' =====================================================
        ' BLOQUE 7: PRUEBA EXITOSA - ACTUALIZAR DOCENTE
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA EXITOSA - ACTUALIZAR DOCENTE")
        Console.WriteLine("-----------------------------------------")

        Try

            ' =============================================
            ' ACTUALIZAR DOCENTE EXISTENTE
            ' =============================================
            Dim docenteActualizado As New Docente()
            docenteActualizado.Identificacion = "123456"
            docenteActualizado.Nombre = "Sebastian Actualizado"
            docenteActualizado.CorreoElectronico = "actualizado@gmail.com"
            docenteActualizado.Estado = True

            gestor.ActualizarDocente(docenteActualizado)
            Console.WriteLine("OK -> Docente actualizado correctamente.")
            Console.WriteLine("      Nuevo nombre  : " & docenteActualizado.Nombre)
            Console.WriteLine("      Nuevo correo  : " & docenteActualizado.CorreoElectronico)

        Catch ex As Exception
            Console.WriteLine("ERROR -> " & ex.Message)
        End Try

        ' =====================================================
        ' BLOQUE 8: PRUEBA EXITOSA - ELIMINAR DOCENTE
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA EXITOSA - ELIMINAR DOCENTE")
        Console.WriteLine("-----------------------------------------")

        Try

            ' =============================================
            ' ELIMINAR DOCENTE EXISTENTE
            ' =============================================
            gestor.EliminarDocente("123456")
            Console.WriteLine("OK -> Docente eliminado correctamente.")

        Catch ex As Exception
            Console.WriteLine("ERROR -> " & ex.Message)
        End Try

        ' =====================================================
        ' BLOQUE 9: PRUEBA - ELIMINAR DOCENTE YA ELIMINADO
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("-----------------------------------------")
        Console.WriteLine(" PRUEBA - ELIMINAR DOCENTE YA ELIMINADO")
        Console.WriteLine("-----------------------------------------")

        Try

            ' =============================================
            ' INTENTAR ELIMINAR UN DOCENTE
            ' QUE YA FUE ELIMINADO
            ' =============================================
            gestor.EliminarDocente("123456")
            Console.WriteLine("OK -> Docente eliminado correctamente.")

        Catch ex As Exception
            Console.WriteLine("OK -> Validacion correcta: " & ex.Message)
        End Try

        ' =====================================================
        ' FIN DE PRUEBAS
        ' =====================================================
        Console.WriteLine()
        Console.WriteLine("=========================================")
        Console.WriteLine(" FIN DE PRUEBAS - MODULO DOCENTE")
        Console.WriteLine("=========================================")

    End Sub

    ' =====================================================
    ' METODO PRIVADO: PROBAR CREAR
    ' =====================================================
    ' Intenta crear un docente con los datos recibidos
    ' y muestra si la validacion fue correcta o no.
    '
    ' PARAMETROS:
    ' identificacion -> Identificación a probar.
    ' nombre         -> Nombre a probar.
    ' correo         -> Correo a probar.
    ' =====================================================
    Private Sub ProbarCrear(
        identificacion As String,
        nombre As String,
        correo As String)

        Try

            ' =============================================
            ' CREAR INSTANCIA DEL GESTOR Y DEL DOCENTE
            ' =============================================
            Dim gestor As New GestorDocente()
            Dim docente As New Docente()

            ' =============================================
            ' ASIGNAR LOS DATOS A PROBAR
            ' =============================================
            docente.Identificacion = identificacion
            docente.Nombre = nombre
            docente.CorreoElectronico = correo

            ' =============================================
            ' INTENTAR CREAR EL DOCENTE
            ' =============================================
            gestor.CrearDocente(docente)

            ' =============================================
            ' SI NO LANZA EXCEPCION, LA PRUEBA FALLO
            ' porque se esperaba un error de validacion
            ' =============================================
            Console.WriteLine("FALLO -> Se esperaba un error de validacion.")

        Catch ex As Exception

            ' =============================================
            ' SI LANZA EXCEPCION, LA VALIDACION
            ' FUNCIONO CORRECTAMENTE
            ' =============================================
            Console.WriteLine("OK    -> Validacion correcta: " & ex.Message)

        End Try

    End Sub

End Module