   M S B A M L   `   `   `  ���� Z  VUCLE Local Treasury Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ucle_treasury_appucle_treasury_app  !    ucle_treasury_app.ViewUsers   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ucle_treasury_app       � Title$  Edit User Information��$��498.331��$��930.449�� � 
ResizeMode$ NoResize=�.��� .+�]� ��. .+�c� $	��Top=�.@�o� .@�m� $��	View User��.@�m� $(��"Just filling this menu bar for now���� $��Stretch=�$��25��$	��Top=�$��Bottom=�.@��� .���� -    ��Name$ lblStatusPosition��$��Ready...��&   System.Windows.Controls.DataGrid  -     Name$ dgUsers��   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . $��0,0,0,-1q�$
��Left=�$	��341��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Name��
   MaxWidth$	
 163Q�   Width$	 163 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	strName+   HeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value$ Center=�  $	 Last Updated��$	
 139Q�$	 139  �� *	dtAdded+ �� "  .���  � ��$ Center=�   ElementStyle �� " ��.���  � ��$ Center=�4  .System.Windows.Controls.DataGridTemplateColumn  $
	 Edit��   CellTemplate �� .R��� -   ���� *lngID+.��Edit�� $��2q�
�������$��31,28,31.333,114q�$
��Left=�$	��203��$	��Top=�$	��180��._��� -    ��Name$ imgUser��$��Center=�$#��resources\images\no_image.png��$	��198��$��Center=�$	��175���� -    ��Name$ lblFirstName��$��lblFirstName��$��22(�$
��Bold"�$	��Top=�$��
-20,28,0,0q�$	��318��$��40���� -   $ lblLastName��$��lblLastName��$��22(�$
��Bold"�$	��Top=�$��-318,57,0,0q�$	��318��$��40���� -   $ lblPhoneNumber��$��lblPhoneNumber��$��14(�$	��Top=�$��-318,126,0,0q�$	��317��$��30���� -   $ lblEmail��$��lblEmail��$��14(�$	��Top=�$��-318,154,0,0q�$	��317��$��30���� -	   $ 
lblAddress��$��
lblAddress��$��18(�$��Italic$�$	��Top=�$��-665,230,0,0q�$	��410��$��35���� -
   $ lblCityStateZip��$��lblCityStateZip��$��18(�$��Italic$�$	��Top=�$��-665,256,0,0q�$	��410��$��35���� -   $ 
lblTithing��$��
lblTithing��$��22(�$��Center=�$	��Top=�$��-565,340,0,0q�$	��270��$��40��