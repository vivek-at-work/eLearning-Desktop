   M S B A M L   `   `   `  ����U  QConeixement.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Wclr-namespace:Coneixement.Infrastructure.Convertors;assembly=Coneixement.Infrastructure%Coneixement.Infrastructure.Convertors  5      R NConeixement.Examination, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Coneixement.ExaminationConeixement.Examination 5      5   /Coneixement.Examination.Views.ReviewUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      oConeixementWclr-namespace:Coneixement.Infrastructure.Convertors;assembly=Coneixement.Infrastructure     5      7local%clr-namespace:Coneixement.Examination     5        � 	Resources  5      B   <Coneixement.Infrastructure.Convertors.EncryptedDataConvertor   EncryptedDataConvertor  correctanswer �Name �� Path 
 option  finalanswer  RadioButtonItemStyle �� 
TargetType �� Property �� Value �� 
TargetType J� IsHitTestVisible �� 
IsSelected	 d� 	IsChecked  HorizontalItemsPanel
 $� Orientation4  .Coneixement.Examination.AnswerTemplateSelector  AnswerTemplateSelector   FinalAnswerT   CorrectAnswerT   OptionT  withoutsolution �� Orientation �� 	Converter �� SelectedValue �� Padding  withsolution 9�Name4  .Coneixement.Examination.ReviewTemplateSelector 	 reviewTemplateSelector   WithoutSolutionTemplate   WithSolutionTemplate �� 
Background �� Bullet �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	      &	 �     &	 �    &	 �    &	 �    &	     7  7  7  &	 5    7   7  7  7  &	 �    7   7   7  7  7  &		 �
    7  7  '	�       5   
   6-   �� 5   
   .R�5      � $ 	container��6   $��32��5      
��� � �$��32��5      .+�5      �� $��Center=�5      $��Center=�5      ��5      �� $ Value�5      5      5      �� 5   
   .R�5      � $ 	container��6   $��32��5      
�������$��32��5      .+�5      �� $��Center=�5      $��Center=�5      ��5      �� $ Value�5      5      5       �� 5!   
   .R�5"      � $ 	container��6   $��32��5#      
��� ���$��32��5%      .+�5&      �� $��Center=�5'      $��Center=�5(      ��5&      �� $ Value�5(      5)      5*      �� 5+   
   " ��5,      .�5-      ��  � ��$ 0,0,5,0q�5.      �� 5/       � �� 50      �� 51      " ��6&   .R�52      �� $��Center=�6!   $��0q�53   !   
������ ._�55      	� |�.   .  #	 z 57   *   .��58   "   �� 59      5:      5;      5=      5>      �� 5?   
   .R�5@      $� $
 
Horizontal=�6%   5A        5B   
   8   8  8  5E   '   �� 5F   
   .R�5G      �� $ Vertical=�6   
���   �$��Stretch=�5I      .+�5J      �� $��Stretch=�6   $��Stretch=�5K      
�������.+�5M      �� $��Stretch=�6   $
��Fill=�5N      ��5O      �� *	ImagePath+8   �� 5P      $ 
Horizontal=�6!   .+�5Q      �� $��5q�6"   $
��Left=�5R   "   $��0q�5S   "   8�� 8�� 8�� ��5T   "   �� *	Answers+ 5V   "   �� *UsersAnswer+5X   "   �� 5Y      $��Center=�6$   $��10q�5Z   $   $��15(�5[   $   $
��Bold"�5\   $   $��Right=�5^   $   ��5]   $   �� $ Remarks�5^   $   5_      5`      �� 5a      
���   �$ 2q�5b      5c      5d      �� 5e   
   .R�5f      �� $ Vertical=�6   
���   �$��Stretch=�5h      .+�5i      9� $ solutionimageexpander��6   ��5j      �� 5k      .R�5l      �� .��5m   "   �� $ 
Horizontal=�6-   $��Stretch=�5n   -   .+�5o   &   �� $��Solution��60   $��Right=�5p   0   
�������5q   0   5r   #   5s      5t      ��5v      �� 5w      $��Stretch=�6    $
��Fill=�5x       ��5y       �� *SolutionImage+8   5{      �� 5|      $��Stretch=�6   $��Stretch=�5}      
�������.+�5      �� $��Stretch=�6   $
��Fill=�5�      ��5�      �� *	ImagePath+8  �� 5�      $ 
Horizontal=�6!   .+�5�      �� $��5q�6"   $
��Left=�5�   "   $��0q�5�   "   8�� 8�� 8�� ��5�   "   �� *	Answers+ 5�   "   �� *UsersAnswer+5�   "   �� 5�      $��Center=�6$   $��10q�5�   $   $��15(�5�   $   $
��Bold"�5�   $   $��Right=�5�   $   ��5�   $   �� $ Remarks�5�   $   5�      5�      �� 5�      
���   �$ 2q�5�      5�      5�        5�   
   8   8  5�   '   �� 5�   
   " 	�6   .�5�      ��  � ��
 ��  �5�      �� 5�       � �� 5�      �� 5�      " 	�6&   .R�5�      �� $ 1q�6!   
���   �._�5�      �� 
 ����  5�   "   �� 5�   &   $ 3q�6-   
�������._�5�   *   � $ 	container��6/   $��32��5�   /   
��� � �$��32��5�   /   .+�5�   .   �� $��Center=�6?   $��Center=�5�   ?   5�   +   5�   '   5�      5�       5�      P� 5�       �@d�	IsChecked$ falseY�5�   &   . �5�   "   �� $ 	container��6)    � ��
 �  ��5�   )   5�      5�      5�      5�      5�      .��5�      � -   5        $ SuggestionGrid��5�      ��5�   
   �� 5�       �� ImageSource$! ../Resources/background.jpg��6    � RowDefinitions 5�   
   �� 5�      $��* �6   6)   �� 5�      $��* �6   6)    � ColumnDefinitions 5�   
   �� 5�      $��* �6   .+�5�   
   �� -   5         ��Name$ ReviewListView��5�      $��0��5�      $��0��5�       �� AlternationCount$ 2��5�      $  �� IsSynchronizedWithCurrentItem$  FalseY�5�      ��5�      �� *ActualWidth+! �� ElementName$! helperField-100����5�      �� *	Reviews+" �� View" 5�      �� 5�      # �� AllowsColumnReorder# . .P�5�      �� .��5�      �� .��5�      �� $ 
Horizontal=�6)   $��Stretch=�5�   )   .+�5�   "   �� $��Center=�6,   $ �� TextAlignment$$ Center=�5�   ,   ��5�   ,   �� $ QuestionPaper.Title�5�      5�      % �� CellTemplate% 5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   $��Stretch=�5�   -   .+�5�   &   �� $ Vertical=�61   $��Stretch=�5�   1   .+�5�   *   �� $��14(�64   $��Stretch=�5�   4   ��5�   4   �� $ Title�5�   '   5�   #   5�      5�      �� 5�      .��5�      �� .��5�      �� $ 
Horizontal=�6)   $��Stretch=�5�   )   .+�5�   "   �� 6/   5�      5�      % 5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   $��Stretch=�5�   -   .+�5�   &   �� $ Vertical=�61   $��Stretch=�5�   1   .+�5�   *   �� $��14(�64   $��Stretch=�5�   4   ��5�   4   �� $ Comment�5�   '   5�   #   5�      5�      5�      5      � 5  
   -   5        $ ExaminationGrid��5     $��1��5     $��0��5       5     ��%    6    5     �� 5     $��1* �6    �� 5     $	��60* �6    �� 5     $	��30* �6    �� 5	     $��1* �6     5     �� 5     $��* �6#   .+�5     �� -   5        $ QuestionListView��5     
�������$��1��5     $��0��5     $ 2��5     $  FalseY�5     ��5     �� *QuestionPaper.Questions+" 5     �� 5     # . .P�5     �� & �� CellTemplateSelector#& [	 6)   .��5     �� .��5  "   �� $��Center=�5  -   ��5  -   �� $! helperField��$ ActualWidth�.+�5  &   �� $��Center=�61   $ 
Horizontal=�5  1   .+�5  *   �� -   5        ' ��Name$' currentquestion��5  4   $$ Right=�5  4   $��Right=�5   4   ��5  4   �� $# QuestionPaper.CurrentQuestion�5   4   �� 5!  *   -   5        $' questionsep��5!  4   $��/��5"  4   $$ Right=�5#  4   $��Right=�5$  4   �� 5%  *   -   5        $' totalquestion��5%  4   $$ Right=�5'  4   $��Right=�5(  4   ��5&  4   �� $" QuestionPaper.TotalQuestions�5(  4   5)  '   5*  #   5+     5,     5-     5/     ]� 50     $��2��6   ��.  .+�52     �� -   5        ( ��Name$
( Prev��52     $��Previous��54     $��Stretch=�55     $��50��56     $��
50,25,25,0q�58     $
��Left=�59     �� 5:     -	   5        $
( Next��5:     $
��Next��5<     $��Stretch=�5=     $��50��5>     $��
0,25,50,25q�5?     $��Right=�5@     5A     5B     � 5C  
   $y�Hidden=�6    5D     �� 5E     $��* �6#   .+�5H     � -
   5        $ helperField��5I     $��1��5H     5I     5J     5K     5L     