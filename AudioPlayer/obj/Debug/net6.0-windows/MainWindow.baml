   M S B A M L   `   `   `  ����  AudioPlayer    AudioPlayer.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Audio Player��5   	   $	��450��6   $	��800��6+   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $	��Top=�6K   $��10q�6c   .+�5      �� -   5         ��Name$ OpenFolderButton��5      $��Выбрать папку��6/   $��5q�6f   �� 5      -   5        $ 
playButton��5      $��Играть��6)   6:   �� 5      -   5        $ pauseButton��5      $��
Пауза��6*   6:   �� 5      -   5        $ PreviousButton��5      $#��Предыдущий трек��6-   $��5q�6d   �� 5      -   5        $ 
NextButton��5      $!��Следующий трек��6)   $��5q�6[   �� 5      -   5        $ RepeatButton��5      $"��Вкл/выкл повтор��6+   $��5q�6`   �� 5      -   5        $ ShuffleButton��5      $7��1Вкл/выкл случайный порядок��6,   $��5q�6m   �� 5      -   5        $ HistoryButton��5      $��История��6,   $��5q�6Z   5      �� 5   
   -	   5         ��Name$ volumeSlider��5       � Minimum$ 0Q�6'    � Maximum$	 100Q�63    � Value$ 50Q�6A   $��10,279,535,53q�6u   $��1��6�   �� 5   
   -
   5        $ positionSlider��5      $��10,322,10,14q�6T   $��1��6j   �� 5   
   -   5         ��Name$ durationTextBlock��5      $��00:00 / 00:00��6/   $��Center=�6D   $	��Top=�6a   $��	0,301,0,0q�6y   $��1��6�   r� 5   
   -   5        	 r�Name$	 mediaElement��5      $��1��6R   5      5      