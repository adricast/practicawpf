   M S B A M L   `   `   `  ����D  @PRACTICA2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:PRACTICA2	PRACTICA2  5   	       PRACTICA2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:PRACTICA2     5   	     � Title$  
MainWindow��5   	   $	��460��6   $	��800��6)    � DataContext 5	      %   PRACTICA2.importarGuiaViewModel  5
   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��100 �6   5      �� 5      $	��120 �6   5      �� 5      $��80 �6   5      �� 5      $	��150 �6   5      �� 5      $��80 �6   5      �� 5      $	��150 �6   5       �� 5!      $	��120 �6   5#      �� 5$      $��* �6   5&       � RowDefinitions 5(   
   �� 5)      $��40 �6   5+      �� 5,      $	��340 �6   5.      �� 5/      $��40 �6   51      �� 54      $
��280* �6   .+�5:   
   �� -   5         ��Name$ 	cmbOpcion��5:      $��0��6&   $��0��66   $��4q�6C   ��6N   �� *Estrategias+ �� SelectedItem 6r   �� *EstrategiaSeleccionada+�� 5;   
   -   5         ��Name$ btnImportar��5;      $��1��6&   $��0��66   $��4q�6C   $��Importar��6N   ��6a   �� *ExportarCommand+�� 5<   
   -   5         ��Name$ datePickerContainer��5<      $��2��62   $��4��6B   $��0��6V    �� Orientation$ 
Horizontal=�6c   y�6|   �� *PanelUnoVisible+.+�5=      �� $��Fecha Inicio��6   $��4q�6+   $��Stretch=�66   (  "System.Windows.Controls.DatePicker  5>      -   5        	  Name$	 dtpFechaInicio��5>      $��4q�61   $��Stretch=�6<   �� 5?      $��	Fecha Fin��6   $��4q�6(   $��Stretch=�63     5@      -   5        $	 dtpFechaFin��5@      $��4q�6.   $��Stretch=�69   5A      �� 5B   
   -   5        $ examinarContainer��5B      $��2��60   $��4��6@   $��0��6T   $ 
Horizontal=�6a   y�6z   �� *PanelDosVisible+.+�5C      �� -   5        $ btnExaminar��5C      $��4q�6*   $	��140��65   $��Examinar��6A   6V   5D      &   System.Windows.Controls.DataGrid  5E   
   -   5        
  Name$
 dtgDatos��5E      $��8��6%   $��1��69   $��4q�6F   �� 5F   
   -	   5        $ btnImprimir��5F      $��0��6&   $��2��66   $��4q�6C   $��Imprimir��6N   �� 5G   
   -
   5        $ btnProcesar��5G      $��5��6&   $��2��66   $��4q�6C   $��Procesar��6N   �� 5H   
   -   5        $ btnCancelar��5H      $��6��6&   $��2��66   $��4q�6C   $��Cancelar��6N   5M      5N      