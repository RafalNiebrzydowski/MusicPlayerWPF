   M S B A M L   `   `   `  ����  OdtwarzaczMuzyczny*    $OdtwarzaczMuzyczny.AddPlaylistWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  DodawaniePlaylist��5   	   $	��600��6#   $	��600��60    � 	Resources 5         PlaylistTemplate � RowDefinitions � ColumnDefinitions �� Path��%�  &	        �� 5      .R�5      �� $��2q�6   ���	#FF50565C��6A   Z� ._�5      �  5	      �� 5
      $
��auto �6$   �� 5      $
��auto �6$    5      �� 5      $��* �6'   �� 5      $	��150 �6'   .+�5      �� $��	TitleSong��6   $��5q�6-   ���	#FFC50000$��16(�6k   $��Center=�6y   ��68   �� $ Title�6y   �� 5      $��DurationSong��6   $��5q�60   $��Right=�6;   $��1��6W   $��14(�6�   $��Center=�6�   ��6g   �� $ Duration�6�   �� 5      $��Artist��6   $��5q�6*   $��1��65   $��14(�6_   $��Center=�6n   ��6B   �� $ Artist�6n   5      5      5      5      .��5      �  5   
   �� 5      $
��auto �6   �� 5      �� 5      $
��auto �6    5   
   �� 5      $	��100 �6   �� 5       .+�5"   
   �� $��Center=�6   $��Center=�6+   .��6H   Podaj nazwę:6V   �� 5#   
   -   5        $��nazwa��5#      $��1��6   $��30��6/   6>   �� 5$   
   -   5        $��playlistListBox��5$   J   $��1��6   $��2��6   ���	#FFEDECF3$��Stretch=�6a    �� SelectionMode$ Multiple=�6�   #��[  6�    5%       �� Color��%�   (��       5&   "   * SystemColors.HighlightBrushKey+)(�� '     5)   "   *1/SystemColors.InactiveSelectionHighlightBrushKey+)�� 5&      $ 	LightGray��5'      �� 5)      $ 	LightGray��5*      5+      5-      �� 5.   
   -   5        $
��Save��5.      $��1��6   $��2��6-   $��15q�6:   $��50��6F   .��6d   Zapisz6l   �� 5/   
   -   5        $��Close��5/      $��2��6   $��15q�6,   .��6M   Anuluj6U   50      51      