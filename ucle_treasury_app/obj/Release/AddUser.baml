   M S B A M L   `   `   `  ���� Z  VUCLE Local Treasury Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ucle_treasury_appucle_treasury_app      ucle_treasury_app.AddUser   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ucle_treasury_app       � Title$  Add New User��$��407.205��$��569.793�� � 
ResizeMode$ NoResize=� � 	Resources    DefaultFadeOut W� From	 W� To �� 
TargetName �� TargetProperty g� Duration g�Name  DefaultFadeIn  FadeOut��%8  &	        &	 Y     &	 �     �� -   .��W� $ 1X�$ 0X�$ imgUserPhoto��$ (Image.Opacity)�$ 0:0:0.5D��� $ DefaultFadeIn��.��W� $ 0X�$ 1X�$ imgUserPhoto��$ (Image.Opacity)�$ 0:0:0.5D��� -   .��W� $ 1X�$ 0X�$ imgUserPhoto��$ (Image.Opacity)�$ 0:0:0.5D�.��� .+�� $
��Left=�$	��379��$	��Top=�$	��565��$��-1,0,0,-0.5q�	 � ColumnDefinitions	 �� $
��101* ��� $
��177* ��� $
��247* �
 � RowDefinitions
 �� $	��56* ��� $	��89* ��� $	��65* ��� $	��46* ��� $	��34* �.+��� -    ��Name$ lblTitle��$��Add New User��$��Center=�$��24(�$
��Bold"�$
��Left=�$��21,5,0,0q�$	��Top=�$	��290��$��3���� -   $ lblFirstName��$��First Name:��$��Right=�$��17,47,22.5,0q�$	��Top=�$��26���� -    ��Name$ tbFirstName��$��Center=�$
��Left=�$��26��$��	87,47,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��186�� �� 	MaxLength$ 30��$��2���� -   $ lblLastName��$��
Last Name:��$��Right=�$��15,0,22.5,0q�$	��Top=�$��26��$��1���� -   $ 
tbLastName��$��Center=�$
��Left=�$��26��$��87,0,0,0q�$
 Wrap=�$	��Top=�$	��186��$ 30��$��2��$��1���� -   $ lblTopPhone��$��(ie. 5591234567)��$��Italic$�$
��Bold"�$��10(�$
��Left=�$��	91,31,0,0q�$��1��$	��Top=�$��2��$	��182��$��23���� -	   $ lblPhone��$��Phone:��$
��Left=�$��	40,54,0,0q�$��1��$	��Top=��� -
   $ tbPhone��$��Center=�$ 10��$
��Left=�$��26��$��	87,54,0,0q�$��1��$
 Wrap=�$	��Top=�$	��186��$��2���� -   $ 
lblCarrier��$��Carrier:��$
��Left=�$��	38,80,0,0q�$��1��$	��Top=��� -    ��Name$ 
cboCarrier��$
��Left=�$��	87,80,0,0q�$��1��$	��Top=�$	��186��$��26��$��2���� -   $ lblEmail��$��Email:��$��Right=�$
��Left=�$��
46,0.5,0,0q�$��2��$	��Top=��� -   $ tbEmail��$��Center=�$
��Left=�$��26��$��
87,0.5,0,0q�$��2��$ 30��$
 Wrap=�$	��Top=�$	��186��$��2���� -   $ 
lblAddress��$��Address:��$
��Left=�$��32,45.5,0,0q�$��2��$	��Top=��� -   $ 	tbAddress��$��Center=�$��3��$
��Left=�$ 44��$��27��$��87,44.5,0,0q�$��2��$
 Wrap=�$	��Top=�$	��265���� -   $ lblCity��$��City:��$��Right=�$
��Left=�$��31,76.5,0,0q�$��2��$	��Top=�$��56��$��2���� -   $ tbCity��$��Center=�$
��Left=�$ 21��$��26��$��87,76.5,0,0q�$��2��$
 Wrap=�$	��Top=�$	��134��$��2��$��2���� -   $ lblState��$��State:��$��Right=�$
��Left=�$��125.5,76.5,0,0q�$��2��$	��Top=�$��1��$��2���� -   $ cboState��$
��Left=�$��164.5,76.5,0,0q�$��2��$	��Top=�$��79��$��26��$��1��$��2��$��2���� -   $ 
lblZipCode��$��	Zip Code:��$��Right=�$
��Left=�$��26,23.5,0,0q�$��3��$	��Top=��� -   $ 	tbZipCode��$��Center=�$
��Left=�$��26��$��87,23.5,0,0q�$ 5��$��3��$
 Wrap=�$	��Top=�$	��111��$��2���� -   $��imgBackground��$��31,12,31,2.5q�$#��resources\images\no_image.png��$��2��$��2��$��Stretch=�$	��203��$��Center=�$	��175���� $��2q�
�������$��31.667,10,31.333,0q�$��2��$��2��$��Stretch=�$	��203��$��Center=�$	��179��._��� -   $��imgUserPhoto��$��-2q�$��2��$��2��$��Stretch=�$	��203��$��Center=�$	��175���� -    ��Name$ 	btnUpload��$��
Load Image��$��22��$��Center=�$��32,0,145,52.5q�$��Bottom=�$��89��$��2��$��2���� -   $ 	btnRemove��$��Remove Image��$��2��$
��Left=�$��137,10.5,0,0q�$��2��$	��Top=�$��98��$��22���� -   $ btnSave��$��Create New User��$��2��$
��Left=�$��31,59.5,0,0q�$��3��$	��Top=�$	��112��$��24��$��2���� -   $ btnClose��$
��Exit��$��2��$
��Left=�$��24��$��159,59.5,0,0q�$��3��$	��Top=�$��76��$��2��