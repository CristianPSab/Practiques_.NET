   M S B A M L   `   `   `  ����J  FAcademy.App.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Academy.App.WPF.ViewsAcademy.App.WPF.Views  5      (    "Academy.App.WPF.Views.SubjectsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Academy.App.WPF.Views     5      .��5	      � ���	#FFE5E5E5  � RowDefinitions  5   
   �� 5      $	��150 �6   �� 5      $��* �6   .+�5   
   � $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   $��	363,0,0,0q�6M   $	��437��6`   .+�5      �� $��30��6   $��Guardar asignatura��6%   $	��124��6h   ��6B   �� *AddSubjectCommand+6h   �� 5      $��30��6   $��Editar asignatura��6%   $	��124��6h   ��6A   �� *EditSubjectCommand+6h   �� 5      $��30��6   $��Eliminar asignatura��6%   $	��124��6i   ��6C   �� *DelSubjectCommand+6i   5      �� 5      $��Nombre Asignatura��6   $
��Left=�60   $��	10,25,0,0q�6K   $	��Top=�6^   $	��118��6v   �� 5      $��Profesor��6   $
��Left=�6'   $��	10,65,0,0q�6B   $	��Top=�6U   $	��118��6m   �� 5      -   5         ��Name$ tbNombreAsignatura��5      $
��Left=�62   $��23��6M   $��
155,25,0,0q�6Y    �� TextWrapping$
 Wrap=�6m   $	��Top=�6�   $	��160��6�    J� RenderTransformOrigin$ 0.406,-0.783&�6�   ��6�   �� *NombreAsignatura+6�   �� 5      -   5        $ 
tbProfesor��5      $
��Left=�6+   $��23��6F   $��
155,65,0,0q�6R   $
 Wrap=�6f   $	��Top=�6�   $	��160��6�   $ 0.406,-0.783&�6�   ��6z   �� *
Profesor+6�   5      � 5   
   $��1��6     5      �� 5      $��* �6    �� 5      $��40 �6    .+�5"      � $��0��6    � ColumnDefinitions 5#      �� 5$      $
��111* �6'   .+�5&      &   System.Windows.Controls.DataGrid  $��5,5,5,5q�6d   $��2��6u   ��6   �� *
Subjects+ �� SelectedItem 6<   �� *CurrentSubject+6u   5'      � 5)      $��1��6   .+�5*      �� $��Obtener asignaturas��6   $	��100��6^   $��5q�6j   $��Right=�6u   ��67   �� *GetSubjectsCommand+6u   5+      5-      50      51      