   M S B A M L   `   `   `  ����  OdtwarzaczMuzyczny+    %OdtwarzaczMuzyczny.EditPlaylistWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  EdytowaniePlaylisty��5   	   $	��650��6%   $	��650��62    � 	Resources 5         PlaylistTemplate � RowDefinitions � ColumnDefinitions �� Path��%�  &	        �� 5   
   .R�5      �� $��2q�6   ���	#FF50565C��6A   Z� ._�5      �  5	      �� 5
      $
��auto �6(   �� 5      $
��auto �6(    5      �� 5      $��* �6+   �� 5      $	��150 �6+   .+�5      �� $��	TitleSong��6    $��5q�61   ���	#FFC50000$��16(�6o   $��Center=�6}   ��6<   �� $ Title�6}   �� 5      $��DurationSong��6    $��5q�64   $��Right=�6?   $��1��6[   $��14(�6�   $��Center=�6�   ��6k   �� $ Duration�6�   �� 5      $��Artist��6    $��5q�6.   $��1��69   $��14(�6c   $��Center=�6r   ��6F   �� $ Artist�6r   5      5      5      5      .��5      �  5   
   �� 5      $
��auto �6   �� 5      �� 5      $
��auto �6    5   
   �� 5       $	��150 �6   �� 5!      .+�5#   
   �� $��Center=�6   $��Center=�6+   .��6H   Podaj nową nazwę:6[   �� 5$   
   -   5        $
��Name��5$      $��1��6   $��30��6.   6=   �� 5%   
   -   5        $��playlistListBox��5%   4   $��1��6   $��2��6   $��Stretch=�6K    �� SelectionMode$ Multiple=�6p   #��[  6�    5&       �� Color��%�   (��       5'   "   * SystemColors.HighlightBrushKey+)(�� '     5*   "   *1/SystemColors.InactiveSelectionHighlightBrushKey+)�� 5'      $ 	LightGray��5(      �� 5*      $ 	LightGray��5+      5,      5.      �� 5/   
   -   5        $��
EditButton��5/      $��1��6#   $��2��63   $��15q�6@   $��50��6L   .��6k   Edytuj6s   �� 50   
   -   5        $��CloseButton��50      $��2��6%   $��15q�62   .��6T   Anuluj6\   51      52      