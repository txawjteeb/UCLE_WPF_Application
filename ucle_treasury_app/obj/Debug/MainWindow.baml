   M S B A M L   `   `   `  ����Z  VUCLE Local Treasury Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ucle_treasury_appucle_treasury_app  5   	   "    ucle_treasury_app.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ucle_treasury_app     5   	     � Title$  UCLE Treasury��5   	   $��436.889��6   $��611.222��60    � 
ResizeMode$ 	CanResize=�6@    � Icon$ resources/images/ucle.jpg��6X    � WindowStartupLocation$ CenterOwner=�6y   .��5	      �  J� RenderTransformOrigin$ 0.5,0.5&�6   ��5
   
   �� 5       �� ImageSource$- 'resources/images/ucle_splash_screen.png��6    j� Stretch$ Uniform=�6O    �� Opacity$
 0.05Q�6a    �� RelativeTransform 5      V� 5      .~�5      �� 	 �� ScaleY$	 1Q�6)   
 �� ScaleX$
 1Q�64   �� 5       �� AngleY$ 0Q�6(    �� AngleX$ 0Q�63   �� 5       �� Angle$ 0Q�6*   T� 5      5      5      .+�5   
   �� -   5         ��Name$ btnEmail��5      $��
Email Test��6#   $
��Left=�68   $��
57,261,0,0q�6S   $	��Top=�6g   $��75��6   $��14(�6�   �� 5   
   .+�5      c� $��0,0,-0.333,0q�6   .@�5      o� .@�5      m� -   5         m�Name$ tlbFile��5      $
��File��60   $��14(�6>   .@�5      m� -   5        $ tlbAbout��5   2   $��About��6#   62   �� 5      m� 5      -   5        $ 
tlbExiting��5   1   $
��Exit��6#   6E   5      m� 5      -   5        $ tlbAdmin��5      $��Admin��61   $��14(�6@   .@�5       m� -   5        $ 
tlbNewUser��5    9   $��Add New User��6#   6M   m� 5!      -   5        $ tlbEditUser��5!   M   $"��View & Edit User Information��6#   6b   5"      m� 5#      -   5        $ 
tlbFinance��5#      $��Finance��63   $��14(�6D   .@�5$      m� -	   5        $ 
tlbTithing��5$   4   $��Tithing��6#   64   m� 5%      -
   5        $ tlbOffering��5%   >   $��Offering Donation��6#   6>   5&      �� 5'      -   5        $ btnLanguage��5'      $��Hmong��62   $��14(�6b   6r   5(      5)      5*      5,      5-      