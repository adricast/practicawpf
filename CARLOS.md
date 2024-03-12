# Modificar Importar Guia Api, Excel

Se requiere elaborar un programa para importar guías desde diferentes orígenes de datos. Estos bien provenir de archivos de Microsoft Excel, sí como se APIs externas como por ejemplo, la del cliente PROMESA. Actualmente, ambos orígenes de importación de datos están implementados en vistas separadas, por lo cual se busca unificarlas a fin de  

## Historia de Usuario



## Plan de Ataque

Se propone a seguir los siguientes pasos al fin de abordar la situaccion presentada

- Proponer el Modelo de la Vista.

- Saber el patron de diseño que se va a Implementar en este caso el ***Patron Strategy***.

- Analizar la logica que hay detrás del codigo existente, en el que se deberá rescatar las procedimientos funcionales y eliminar codigo no funcional o duplicado.

- Se debe llenar la ***Clase ImportarGuiaLogistica*** usando el Patron Strategy con los Codigo que permitira la Importación de Guias. 

### Pantalla Modelo para Importar Guias.

Se muestra la propuesta de pantalla para Importar las Guias

<img src="https://i.imgur.com/xwvSZFb.png" title="" alt="Imagen" data-align="center">

### Uso del Patron Strategy

En este caso se tendrá la logica del ***Patron Strategy*** y presentamos una breve estructura

```vb
Public Interface IObtenerGuiasStrategy
    Function ObtenerData() As List(Of ImportarGuiaLogistica)
End Interface

```

Se escribe el código de las clases que permitirán realizar cada uno de los procesos de Importacion

```vb

Public Class ArchivoStrategy
    Implements IObtenerGuiasStrategy

    Private ReadOnly _rutaArchivo As String

    Public Sub New(rutaArchivo As String)
        _rutaArchivo = rutaArchivo
    End Sub

    Public Function ObtenerData() As List(Of ImportarGuiaLogistica) Implements IObtenerGuiasStrategy.ObtenerData
        ' Aquí implementa la lógica para obtener los datos de guías desde un archivo usando _rutaArchivo
        Dim datos As New List(Of ImportarGuiaLogistica)

        ' Por ejemplo, puedes leer los datos desde un archivo Excel usando _rutaArchivo
        ' Aquí colocarías la lógica específica para leer el archivo y convertirlo en una lista de ImportarGuiaLogistica

        Return datos
    End Function
End Class

Public Class ApiPromesaStrategy
    Implements IStrategy

    Public Sub ImportarGuiasApi(fechaInicio As Date, fechaFin As Date, estado As String) Implements IStrategy.ImportarGuiasApi
        ' Lógica específica para la importación desde la API con promesa
        Console.WriteLine("Importando guías desde API con promesa: " & estado)
    End Sub
End Class

Public Class ApiClienteFuturo
    Implements IStrategy

    Public Sub ImportarGuiasApi(fechaInicio As Date, fechaFin As Date, estado As String) Implements IStrategy.ImportarGuiasApi
        ' Lógica específica para la importación desde la API cliente futuro
        Console.WriteLine("Importando guías desde API cliente futuro: " & estado)
    End Sub
End Class
```

### Analisis del Codigo

Se realiza el debido analisis del Codigo Importar en Archivo Excel y el de Importar Api Promesa:

- Se observa que el *frmImportarGuiasExcel* no implementa MVVM, por lo cual se realiza la programación directamente desde la vista. Es por esto que se considera la opción de crear un *ViewModel* para manejar esta lógica. Dado que ahora se va unir y elaborar un único formulario para importar las Guias, se realizará un *ViewModel* general llamado ***ImportarGuiasViewModel***, mismo que implementará el ***Patrón Strategy***.

```vb
Public Class ImportarGuiasViewModel

    ''' Se obtiene del Item Sorce del Combo BOX
    Public Property Estrategias As New List(Of IObtenerGuiasStrategy)


    ''' Se obtiene desde el Item Seleccionado del Combo Box
    Public Property EstrategiaSeleccionada As IObtenerGuiasStrategy

    Public Property Datos As List(Of ImportarGuiaLogistica)
    Private Function ObtenerDatosCanExecute() As Boolean
        Return True
    End Function

     Private Sub ObtenerDatosExecute()
        Try
            Datos = EstrategiaSeleccionada.ObtenerData()
        Catch ex As Exception

        End Try
    End Sub

    Public ReadOnly Property ObtenerDatos As New RelayCommand(AddressOf ObtenerDatosExecute, AddressOf ObtenerDatosCanExecute)

    Private Sub ProcesarExceute()
        For Each d In Datos
            d.GenerarGuia()
        Next
    End Sub

    Public Sub New()
        Estrategias.Add(New ArchivoStrategy)
        Estrategias.Add(New ApiPromesaStrategy)
        Estrategias.Add(New ApiClienteFuturo)

        EstrategiaSeleccionada = Estrategias.First
    End Sub
End Class


```

Adicional se implementara los codigo de los metodos procesar, cancelar imprimir

```vb

Public Class ImportarGuiasViewModel
 ''' codigo implementado

     Private Sub ProcesarExecute()
        Dim DataComposite = ProcesarComposite(Datos)
        For Each x In DataComposite.Where(Function(P) P.Seleccionado And Not P.Procesando)
            If Cancelar Then
                Exit For
            End If
            x.GenerarGuia()
        Next
     End Sub

    Private sub CancelarExcecute()
        Cancelar = True
    End Sub

    Private sub ImprimirExcecute()
        bCancelar.IsEnabled = False
        Dim DataComposite = ProcesarComposite(Datos)
        Dim idsGuiasParaImprimir = (From x In DataComposite Where x.Seleccionado And x.GuiaDB IsNot Nothing Select x.GuiaDB.id).Distinct.ToList
        If idsGuiasParaImprimir.Any Then
            Dim frm As New frmImprimirGuia(idsGuiasParaImprimir)
            frm.Show()
        Else
            cMessage.show("No hay guías cargadas en pantalla. Presione primero el botón Procesar.")
        End If
        bCancelar.IsEnabled = True

    End Sub



End Class
```



### Integrando el codigo solucion

Al momento que el usuario escoja la estrategia ArchivoStrategy se implementaria el siguiente codigo en la ***funcion ObtenerDatos***.

```vb
Public Sub ObtenerDatosExecute()
    Try
        If TypeOf EstrategiaSeleccionada Is ArchivoStrategy Then
            ' Si la estrategia seleccionada es ArchivoStrategy, obtenemos la ruta del archivo utilizando el método existente
            Dim rutaArchivo As String = RowImportarExcel.ObtenerNombreArchivoExcel()
            Dim archivoStrategy As ArchivoStrategy = DirectCast(EstrategiaSeleccionada, ArchivoStrategy)
            archivoStrategy.RutaArchivo = rutaArchivo ' Asignamos la ruta del archivo a la estrategia
            Datos = archivoStrategy.ObtenerData()
        Else
            ' Si no es ArchivoStrategy, simplemente obtenemos los datos utilizando la estrategia seleccionada
            Datos = EstrategiaSeleccionada.ObtenerData()
        End If
    Catch ex As Exception
        ' Manejar cualquier excepción que pueda ocurrir durante la obtención de datos
    End Try
End Sub

```

Se implementará el siguiente  codigo en la estrategia ***ArchivoStrategy***.

```vb
Public Class ArchivoStrategy
    Implements IStrategy

    Public Property RutaArchivo As String ' Propiedad para almacenar la ruta del archivo

    Public Function ObtenerData() As List(Of ImportarGuiaLogistica)
        Dim datos As New List(Of ImportarGuiaLogistica)()

        ' Verificamos si la ruta del archivo está vacía
        If Not String.IsNullOrEmpty(RutaArchivo) Then
            Dim idPlantilla As Integer = -1
            Using db As New PCGNDataContext
                Dim qPlantilla = (From x In db.PLANTILLA_IMPORTAR_DATOS Where x.idTipoPlantilla = PLANTILLA_IMPORTAR_DATOS.Tipos.IMPORTAR_GUÍAS_LOGÍSTICA Select x.id)
                If qPlantilla.Any Then
                    idPlantilla = qPlantilla.First
                Else
                    Throw New Exception("No se encontró una plantilla para Importar Guías Logística (probablemente el programa está desactualizado)")
                End If

                Dim Data = HelperPlantillasLiqTarjetas.CargarDataConPlantilla(Of ImportarGuiaLogistica)(idPlantilla, RutaArchivo)
                For Each x In Data
                    If Not (String.IsNullOrEmpty(x.FormaPago) Or String.IsNullOrEmpty(x.BodegaOrigen)) Then
                        datos.Add(x)
                    End If
                Next
            End Using
        End If

        Return datos
    End Function
    Private Function ProcesarComposite(ByVal DatosImportadosExcel As BindingList(Of ImportarGuiaLogistica)) As List(Of         ImportarGuiaLogistica)
        Dim Data As New List(Of ImportarGuiaLogistica)

        For Each guia In DatosImportadosExcel
            'Primero limpiar todo lo relacionado a padres y parciales
            guia.GuiaPadre = Nothing
            guia.Parciales.Clear()
        Next

        For Each guia In DatosImportadosExcel
            'Los composite se procesan estén o no seleccionados
            Dim qYaExiste = From g In Data Where g.IdentificacionRemitente = guia.IdentificacionRemitente And
                                                g.FormaPago = guia.FormaPago And
                                                g.GuiasRemision = guia.GuiasRemision And
                                                g.BodegaOrigen = guia.BodegaOrigen
            If qYaExiste.Any Then
                Dim GuiaPadre = qYaExiste.First
                guia.GuiaPadre = GuiaPadre
                GuiaPadre.Parciales.Add(guia)
            Else
                Data.Add(guia)
            End If
        Next
        Return Data
    End Function

End Class

```

ENUNCIADO DEL PROBLMEA: Se deberá dar un tratamiento para el escenario en el cual el programa no logra mapear ciertas ciudades contra la base de datos de PCGerente. 

El tratamiento será mostrarle al usuario en un ComboBox la lista de ciudades almacenadas en el programa para que el usuario pueda realizar el mapeo manualmente

1) El viewmodel necesita identificar el error

//seudo codigo para idntificar el error

```vb
PUblic Property CIudadesNoIdentificadas as new list(of String)
Public Sub Validar()
    ' Método para obtener ciudades
    CIudadesNOIdentificadas.Clear()
     Dim CiudadesDistintas = (From x in Datos Select x.Ciudad).Distinct()
    for each ciu in CiudadesDistintas
        Dim Existe=(from x in viewModelcontext.CIUDAD where x.nombre = ciu)
        if Not Existe.Any() then
	        Dim qExisteComoMapeo=(from x in viewodelcontext.CIUDAD_MAPEO where x.nombreCiudad = ciu)    
		    if not qExisteComoMapeo.Any() then
			    CiudadesNoIdentificadas.Add(ciu)
			else
				'no hacer anada, porque está ok'
			end if
		    
		End If
    Next

	if CiudadesNOIdentificadas.Any() THen
		MostrarError($"{CiudadesNOIdentificadas.Count} ciudades no se identificaron. Corregir")
	End Function
	
End Sub
```

2. La vista debe mostrar e mensaje "X ciudades no se reconoce ____Corregir____"

3. Al hacer clic en el hipervìnculo Corregir, se debe abrir una nueva VISTA donde permita mapear esos nombres no reconocidos a ciudades que sí existen en la base de datos

frmImportarGuias

```vb
Public Sub CorregirError_Click()
	
	Dim frm as new frmMapearCiudades(vm.CiudadesNoIdentificadas())
	if frm.ShowDialog() then
		vm.Validar()
	next 
End Sub
```


frmMapearCiudades
```xml
<Window>
	<BUtton>Mostrar todo</Button>
	<Grid>
		<COlumn Label="Ciudad No Identificada" Name="CiudadNoIdentificada"/> <!-- esto yo te doy -->
		<COlumn Label="Ciudad Mapear" Name="idCIudad"> <!-- esto llena el usuario -->
			<ComboSource>
				<ListaCiudades>
			</ComboSource>
		</Column>
	</Grid>
	<Button>Guardar</Button>
</Window>
```


```vb
Dim Datos as new list(of MapeoCiudad)
Public Sub New(Byval CIudadesNoIdentificadas as list(of String))
	
	for each ciu in ciudadesnoidentificadas
		Dim rg as new mapeociudad
		rg.CiudadNoIdentificada=ciu
		
		Datos.add(rg)
	next

		Datagrid.DataSource=Retorno
		'TODO: CARGAR ComboSource / ListaCIudades'
End 

PUblic Sub Guardar_CLick()
'TODO: VALIDAR QUE ESTÉN TODAS ESCOGIDAS
	using db as new pcgndatacontext
		For each x in Datos
			Dim cm as new CIUDAD_MAPEO
			cm.nombreCIudad=x.CiudadNoIdentificada
			cm.idCiudad = x.idCiudad
			db.CIUDAD_MAPEO.InsertOnSubmit(cm)
		Next 
		db.submitChanges()
	end using
	DialogResult=True
End If

Public Class MapeoCIudad
	Public Property CiudadNoIdentificada as string
	Public Property idCIudad as Integer
End Class
```

4 . Necesitamos guardar la corrección en una tabla

```SQL
CREATE TABLE CIUDAD_MAPEO(
	id int IDENTITY(1,1) NOT NULL,
	nombreCiudad varchar(200) NOT NULL, 
	idCiudad INT NOT NULL,
	CONSTRAINT PK_CIUDAD_MAPEO PRIMARY KEY (id),
	CONSTRAINT FK_CIUDAD_MAPEO_CIUDAD FOREIGN KEY CIUDAD (id)
)
```
   
