   M S B A M L   `   `   `  ����  OdtwarzaczMuzyczny-    'OdtwarzaczMuzyczny.FindAlbumImageWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  WyszukiwanieOkladki��5   	   $	��600��6%   $	��600��62   .��5      �  � RowDefinitions 5   
   �� 5	      $��30 �6   �� 5
      .+�5   
   �� -   5         ��Name$ 
BackLayout��5   P   $��0��6   ���	#FFB42020 �� Orientation$ Vertical=�69   .+�5      � -   5         �Name$ innerLayout��5      $
��Auto��6)    � ColumnDefinitions 5      �� 5      -   5         �Name$ LeftSideMenu��5   +   $��75 �6?   �� 5      -   5        $ L2eftSideMenu��5   +   $��* �6@   �� 5      -   5        $ Middle��5   +   $	��105 �69   �� 5      -   5        $ Right��5   +   $��40 �68   �� 5      -   5        $ Right2��5   '   $	��100 �65   .+�5      �� -   5         ��Name$ Todisp��5   3   $��0��6   $��5q�6(   $��Wyszukaj:  ��6C   $��0,0,0,5q�6Y   
�������6�   �� 5      -	   5         ��Name$ 
textSearch��5   =   $��1��6   $��5,5,5,10q�6+   $��20��6Q   
���   �
�������6�   �� 5      $��2��6   $��5q�6(   $��Liczba wyników: ��64   $��0,0,0,5q�6O   
�������6w   �� 5      -
   5        $ 
textResult��5   5   $��3��6   $��5q�6*   $��20��6I   $��0,0,0,5q�6V   
���   �
�������$��20��6�   6�   �� 5      -   5        	 ��Name$	 	btnSubmit��5   ;   $��4��6   $��5,5,5,10q�6)   $��Szukaj��6N   6_   5       5!      �� 5"   
   -   5        $��AlbumImageListBox��5"   8   $��1��6   ���	#FFEDECF3$��Stretch=�6z   5#      5%      