Imports SistemaHorarios.Logica.Negocio.Docentes
Imports SistemaHorarios.Modelos.Modelos

Module PruebaDocente

    Public Sub Ejecutar()

        Try

            ' =========================================
            ' CREAR INSTANCIA DEL GESTOR
            ' =========================================
            Dim gestor As New GestorDocente()

            ' =========================================
            ' CREAR DOCENTE
            ' =========================================
            Dim docente As New Docente()

            docente.Identificacion = "123456"
            docente.Nombre = "Sebastian Villaneda"
            docente.CorreoElectronico = "sebastian@gmail.com"

            gestor.CrearDocente(docente)

            Console.WriteLine(
                "Docente creado correctamente.")

            ' =========================================
            ' CONSULTAR DOCENTE
            ' =========================================
            Dim docenteConsultado As Docente =
                gestor.ObtenerDocentePorIdentificacion(
                    "123456")

            Console.WriteLine(
                "Nombre: " &
                docenteConsultado.Nombre)

            Console.WriteLine(
                "Correo: " &
                docenteConsultado.CorreoElectronico)

            ' =========================================
            ' ACTUALIZAR DOCENTE
            ' =========================================
            docenteConsultado.Nombre =
                "Sebastian Actualizado"

            docenteConsultado.CorreoElectronico =
                "nuevo@gmail.com"

            gestor.ActualizarDocente(
                docenteConsultado)

            Console.WriteLine(
                "Docente actualizado correctamente.")

            ' =========================================
            ' ELIMINAR DOCENTE
            ' =========================================
            gestor.EliminarDocente("123456")

            Console.WriteLine(
                "Docente eliminado correctamente.")

        Catch ex As Exception

            Console.WriteLine(
                "ERROR: " & ex.Message)

        End Try

    End Sub

End Module