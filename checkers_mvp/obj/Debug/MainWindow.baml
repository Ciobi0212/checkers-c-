   M S B A M L   `   `   `  ����G  Ccheckers_mvp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:checkers_mvpcheckers_mvp  5   	   7 clr-namespace:checkers_mvp.Viewscheckers_mvp.Views  5   	       checkers_mvp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:checkers_mvp     5   	   1view clr-namespace:checkers_mvp.Views     5   	     � Title$  Checkers��5	   	   $	��900��6   $	��990��6'    � 	Resources 5       ��Name   SlideOutFromRight W�Name �� 
TargetName �� TargetProperty W� From	 W� To g� Duration  SlideInFromRight��%�  &	        &	 ;    �� 5   
   $ storyBoardShrink��6   .��5      W� $ 
menuShrink��6   $ 	MenuStack��62   $ Width�6T   $	 170X�6v   $ 60X�6�   $ 0:0:0.5D�6�   W� 5      $ displayFrameLarger��6   $ displayFrame��6:   $ Width�6_   $	 800X�6�   $	 920X�6�   $ 0:0:0.5D�6�   6�   5      �� 5   
   $ storyBoardLarge��6   .��5      W� $ 
menuExpand��6   $ 	MenuStack��62   $ Width�6T   $ 50X�6v   $	 170X�6�   $ 0:0:0.2D�6�   W� 5      $ displayFrameSmaller��6   $ displayFrame��6;   $ Width�6`   $	 920X�6�   $	 800X�6�   $ 0:0:0.2D�6�   6�   5      5      .��5      ]� ��. .+�5   
   �� 
���  ��$��0 0 4 0q�6$   ._�5      �� -   5        	 ��Name$	 	MenuStack��5      $
��Left=�6,   $	��170��6B   
���   �.+�5      �� -   5        
 ��Name$
 btnMenu��5      $��30��6J   $��30��6U   $
��Left=�6a   $	��Top=�6|   
���   �$��
10 10 0 50q�6�   ��6*   �� *MenuCommand+.��5      �� $"��C:\Users\cioba\Desktop\a.png��6   6D   5      �� 5      $��
0 30 10 10q�69   $��35��6M   $��20(�6Y   
���   �
���   �
�������$
��Left=�6�   ��6   �� *HomeCommand+.��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5       �� $��Images/home.png��6    $��50��69   $��30��6D   6R   �� 5!      $
��Home��6$   62   5"      5#      �� 5%      #��[  6<   $��
0 30 10 10q�6~   $��35��6�   $��20(�6�   
���   �
���   �
�������$
��Left=�6�   ��6   �� *NewGameCommand+.��5&      �� $ 
Horizontal=�6!   .+�5'      �� $��Images/newgame.png��6    $��50��6<   $��30��6G   6U   �� 5(      $��New game��6$   66   5)      5*      �� 5.      #��[  6=   $��35��6�   $��20(�6�   $��
0 30 10 10q�6�   
���   �
���   �
�������$
��Left=�6�   ��6   �� *LoadGameCommand+.��5/      �� $ 
Horizontal=�6!   .+�50      �� $��Images/loadgame.png��6    $��50��6=   $��30��6H   6V   �� 51      $��	Load game��6$   67   52      53      �� 55      $��35��6=   $��20(�6I   $��
0 30 10 10q�6W   
���   �
���   �
�������$
��Left=�6�   ��6   �� *SaveGameCommand+.��56      �� $ 
Horizontal=�6!   .+�57      �� $��Images/savegame.png��6    $��50��6=   $��30��6H   6V   �� 58      $��	Save game��6$   67   59      5:      d� 5=      $��35��6>   $��20(�6J   $��
0 30 10 10q�6X   
���   �
���   �
�������$
��Left=�6�   ��6   �� *
AMJCommand+.��5>      �� $ 
Horizontal=�6!   .+�5@      �� $��/Images/jump.png��6    $��50��6:   $��30��6E   6S   �� 5A      $��MultJump��6$   66   5C      5D      �� 5F      $��35��6:   $��20(�6F   $��
0 30 10 10q�6T   
���   �
���   �
�������$
��Left=�6�   ��6   �� *StatsCommand+.��5G      �� $ 
Horizontal=�6!   .+�5H      �� $��Images/stats.png��6    $��50��6:   $��30��6E   6S   �� 5I      $��Stats��6$   63   5J      5K      5L      5M       � 5O   
   -   5          �Name$ displayFrame��5O      $��Right=�6?   
�������  � NavigationUIVisibility$ Hidden=�6i   ��6&   �� *Page+6�   5P      5R      