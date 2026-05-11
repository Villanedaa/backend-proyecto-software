Imports Negocio.Docentes
Imports SistemaHorarios.Logica.Negocio.Docentes
Imports SistemaHorarios.Modelos
Imports SistemaHorarios.Modelos.Modelos

Module PruebaDocente

    Public Sub Ejecutar()

        Try

            Dim gestor As New GestorDocente()

            Dim docente As New Docente()

            docente.Identificacion = "123456"
            docente.Nombre = "Sebastian Villaneda"
            docente.CorreoElectronico = "sebastian@gmail.com"

            gestor.CrearDocente(docente)

            Console.WriteLine("Docente creado correctamente.")

            Dim docenteConsultado As Docente =
                gestor.ObtenerDocentePorIdentificacion("123456")

            Console.WriteLine("Nombre: " & docenteConsultado.Nombre)
            Console.WriteLine("Correo: " & docenteConsultado.CorreoElectronico)

            docenteConsultado.Nombre = "Sebastian Actualizado"
            docenteConsultado.CorreoElectronico = "nuevo@gmail.com"

            gestor.ActualizarDocente(docenteConsultado)

            Console.WriteLine("Docente actualizado correctamente.")

            gestor.EliminarDocente("123456")

            Console.WriteLine("Docente eliminado correctamente.")

        Catch ex As Exception

            Console.WriteLine("ERROR: " & ex.Message)

        End Try

    End Sub

End Module