   M S B A M L   `   `   `  ����   Coneixement.VideoPlayer/    )Coneixement.VideoPlayer.Views.VideoPlayer   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� 
���   �  � RowDefinitions  �� $��* ��� $��60 �.+�r� -   $��videoElement��$��0�� r� LoadedBehavior$ Manual=� r� UnloadedBehavior$
 Stop=� r� ScrubbingEnabled .  r� Source ��  �� Path$ 	MediaPath��� $��1��.+��� $��Stretch=�$y�	Collapsed=� �� Orientation$ 
Horizontal=�.+��� 
�������$��5q�$��Center=�	Seek To�� -   $��timelineSlider��$��Stretch=� � SmallChange$ 1Q� � LargeChange$ 10000Q��� -   $��videoElementTime��
�������$��5q�$��Center=�##:##:##:##�� $��Center=�$	��450��$ 
Horizontal=�.+��� -   $
��test��$��..\images\record_play.png��$��5q��� -   $ ��..\images\record_pause.png��$��5q��� -   $��..\images\record_stop.png��$��5q��� 
�������$��Center=�$��5q�Volume�� -   $��volumeSlider��$��Center=�	 � Minimum$	 0Q�
 � Maximum$
 1Q� � Value$	 0.5Q�$��70�� �� IsSnapToTickEnabled .  �� TickFrequency$	 0.1Q��� 
�������$��5,5,30,0q�$��Center=�����  �� ElementName$ volumeSlider��$ Value��� 
�������$��5q�$��Center=�Speed�� -   $��speedRatioSlider��$��Center=�$ 1Q�$��70��$ .5Q� . $
 4Q�$	 0.5Q�$	 0.5Q��� 
�������$��5q�$��Center=����� $ speedRatioSlider��$ Value�