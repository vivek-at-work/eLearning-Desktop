   M S B A M L   `   `   `  ����  Coneixement.SplashScreen+    %Coneixement.SplashScreen.SplashWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  splash��5   	    � WindowStyle$
 None=�5   	   $	��325��5   	   $	��500��6    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5	   	    � ShowInTaskbar .   � Topmost .  � 	Resources 5         showStoryBoard �� TargetProperty �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame	 p� KeyTime
 p� Value  HideStoryBoard �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   Color   Opacity   
BlurRadius��%D  &	        &	     '��     '��     �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5        $	 0:0:0��6*   $
 0,0,0,0q�6:     5      $	 0:0:2��6*   $
 0,0,0,35q�6:     5      $	 0:0:5��6*   $
 0,0,0,35q�6:   5      5      �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5        $	 0:0:0��6*   $
 0,0,0,35q�6:     5      $	 0:0:2��6*   $
 0,0,0,45q�6:     5      $	 0:0:5��6*   $
 0,0,0,55q�6:   5      5      �� 5   
   " ��6   .�5      ��  �@��Padding$ 10q�6)    5      P� 5       �@J�IsMouseOver . . �5      ��  �@J�Effect 5         5!      $ 3Q�6/   $
 #fff��6?   $ 1Q�6L   $ 20Q�6X   5#      5$      5&      �� 5'   
   " ��6   .�5(      ��  � ��$ 5q�6(    5)      P� 5*       �@J�IsMouseOver . . �5+      ��  �@J�Effect 5,        5-      $ 3Q�6/   $
 #fff��6?   $ 1Q�6L   $ 20Q�6X   5/      50      52      53       � Triggers 54      :� 55   
    :� RoutedEvent$ Window.Loaded��6   .[�56      �� .��57      �� $ layoutParent��6   .��58      U�  g� 	BeginTime$ 0:0:0X�64    g� Duration$ 0:00:05D�6F   $ layoutParent��6Y   $ Opacity�6~   .\�59      9  3System.Windows.Media.Animation.EasingDoubleKeyFrame   O� KeyTime$ 0:0:0��6/    O� Value$	 0.2Q�6?     5:      $ 0:0:1��6/   $	 0.5Q�6?     5;      $ 0:0:2��6/   $	 0.8Q�6?     5<      $ 0:0:3��6/   $	 0.9Q�6?     5=      $ 0:0:5��6/   $ 1Q�6?   5>      5?      5@      5A      .��5C      � -   5        $��layoutParent��5C       � UseLayoutRounding . ��5D   
   �� 5E       �� ImageSource$! ../Resources/background.jpg��6    � RowDefinitions 5G   
   �� 5H      $	��10* �6   �� 5I      $	��95* �6   �� 5J      $	��40* �6   �� 5K      $	��30* �6   �� 5L      $	��10* �6   .+�5N   
   � $��Bottom=�6   $��2��6*   $��50,0,0,0q�67   $��Stretch=�6I   
������  5O        loadingmessage��%�   &	       �� 5P      " ��6    5Q      P� 5R       �@J�IsMouseOver . . �5S      ��  �@J�Effect 5T   "     5U   &   $ 3Q�67   $ #424242��6G   $ 1Q�6W   $ 20Q�6c   5W      5X      5Z      5[       J� OpacityMask 5\      �� 5]       �� EndPoint$ 0.5,1&�6&     �� 
StartPoint$  0.5,0&�67   .��5^      � ! � Color$! Black��6#   " � Offset$" 83Q�61   � 5_      $! Bisque��6#   $" 78Q�62   � 5`      $! 
Aquamarine��6#   $" .1Q�66   5a      .+�5c      �� -   5        # ��Name$# 
txtLoading��5c      #��[ 6,   $ �� TextAlignment$
$ Left=�6T   $��14(�6i   ���#000$
��Wrap=�6�   $��Bottom=�6�   $
��Left=�6�   % J� Effect% 5d        5e      $ 5Q�6'   $
 #fff��67   $	 1.5Q�6D   $ 40Q�6R   5g      5h      �� 5i   
   $��1��6   .+�5j      �� $$ Center=�6   $��Stretch=�6/   $
��Wrap=�6N   $��Center=�6�   
���   �$��32(�6�   $��Black"�6�   ��6b   �� & �� Path$& ApplicationName�% 5k        5l      $ 2Q�5m      '   	Direction$	' 120Q�5n      $
 0.55Q�5o      $	 0.2Q�5p      5r      �� 5s      
���   �$��Center=�6+   $$ Center=�6r   $
��Bold"�6�   $
��Wrap=�6�   $	��Top=�6�   $	��134��6�   ��6H   �� $& ApplicationVersion�6�   �� 5t      $��64��6   $��64��6   $��Center=�6+   $��../Resources/ikon.jpg��6H   z�5u      V� 5v      .~�5w      �� �� 5x      �� 5y      T� 5z      5{      5}      5~      �� 5   
   
������ $��3��6*   $	��500��67   $��Center=�6C   ._�5�      �� ( �� Orientation$( Vertical=�6   .+�5�      �� 
������ $	��321��65   
�������$��SemiBold"�6T    . $$ Center=�6�   $
��Wrap=�6�   $��Center=�6�   $��Center=�6�   $d��^You are using trial version of iCrack Application. Contact Ikon Technology to purchage Licence��6  y�6�   �� $& IsTrialVersion�6  �� 5�      
������ $��Right=�65   $��SemiBold"�6Q   $$ Right=�6g   $��Center=�6   ��6�   �� $& ApplicationPublisherName�5�      5�      5�      5�      