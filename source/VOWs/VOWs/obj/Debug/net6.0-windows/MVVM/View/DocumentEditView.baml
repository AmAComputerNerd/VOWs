   M S B A M L   `   `   `  ����?  ;VOWs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:VOWs.MVVM.ViewVOWs.MVVM.View  5      ?$clr-namespace:VOWs.AttachedBehaviourVOWs.AttachedBehaviour  5      )clr-namespace:VOWs.CustomVOWs.Custom  5      9!clr-namespace:VOWs.MVVM.ViewModelVOWs.MVVM.ViewModel  5	      %    VOWs.MVVM.View.DocumentEditView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:VOWs.MVVM.View     5      2b$clr-namespace:VOWs.AttachedBehaviour     5      ,customclr-namespace:VOWs.Custom     5      7	viewmodel!clr-namespace:VOWs.MVVM.ViewModel     5	      .��5      �    
ThemedGrid#��[  6   .+�5   
   ]� .+�5      �� $	��Top=�6   $��30��5        
MenuBorder#��[ 5      ._�5      ��   �� Orientation$  
Horizontal=�6   .+�5      d� $��0,0,2,0q�6#     MenuButton_VOWs#��[ 5   #    d� 	IsChecked 5   #   �� *VOWs_Selected+��5      �� 5      ��6$   �� *Logo.ImageSource+5      	� 5!      $
��Home��6"     MenuButton_Category#��[ 5#   "    5"   "   �� *Tabs_HomeSelected+5#   "   	� 5$      $
��Text��6"   #��[ 5&   "    5%   "   �� *Tabs_TextSelected+5&   "   	� 5'      $��Media��6"   #��[ 5)   "    5(   "   �� *Tabs_MediaSelected+5)   "   	� 5*      $
��Page��6"   #��[ 5,   "    5+   "   �� *Tabs_PageSelected+5,   "   	� 5-      $
��View��6"   #��[ 5/   "    5.   "   �� *Tabs_ViewSelected+5/   "   	� 52      $��Version Control��6"   #��[ 55   "    53   "   �� *Tabs_VersionControlSelected+y�54   "   �� *Tabs_VersionControl+55   "   	� 56      $��Image��6"   $��10,0,0,0q�57   "   #��[ 5:   "    58   "   �� *Tabs_ImageSelected+y�59   "   �� *
Tabs_Image+5:   "   	� 5;      $��Video��6"   $��10,0,0,0q�5<   "   #��[ 5?   "    5=   "   �� *Tabs_VideoSelected+y�5>   "   �� *
Tabs_Video+5?   "   5@      5A      �� 5D      $	��Top=�6   $��90��5F      #��[ 5G      y�5E      �� *	Content+._�5H      � .+�5K      �� $  
Horizontal=�6!   y�5L   !   �� *Content_Home+ �� FallbackValue$ Visible��.+�5M         VOWs.Custom.MenuSection     Title$
 Font��6-      Content. 5N      A�  A� Rows$ 3��6*   $��1��5O   *   .+�5P   "      VOWs.Custom.MenuComboBox     Source$, &/Resources/Images/menu_font-family.png��66      
Descriptor$ Family��5Q   6    �� SelectedValuePath$ Content��5R   c   1   +VOWs.AttachedBehaviour.InputBindingsManager+	   $UpdatePropertySourceWhenEnterPressed	 �@��SelectedValue
 �� 
IsEditable$
 False��5T   6    �� 
IsReadOnly$
 True��6I    �� SelectedValue 5R   6   �� *FontSelectedFamily+@�5U   &   �� 5V   *   $��Arial��67   $��Arial)�5W   7   �� 5X   *   $��Calibri��67   $��Calibri)�5Y   7    �� 
IsSelected$
 True��5Z   7   �� 5[   *   $��Consolas��67   $��Consolas)�5\   7   5^   #     5_   "   $, &/Resources/Images/menu_font-family.png��66   $
 Size��5`   6   $ Content��5a      	 �@��SelectedValue$

 True��5c   6   $ False��6H    �� IsTextSearchEnabled$ False��5d   6    5a   6   �� *FontSelectedSize+ �� UpdateSourceTrigger$ Explicit=�@�5e   &   �� 5f   *   $��8��67   �� 5g   *   $��9��67   �� 5h   *   $��10��67   �� 5i   *   $��11��67   $
 True��5j   7   �� 5k   *   $��12��67   �� 5l   *   $��14��67   �� 5m   *   $��16��67   �� 5n   *   $��18��67   �� 5o   *   $��20��67   �� 5p   *   $��22��67   �� 5q   *   $��24��67   �� 5r   *   $��26��67   �� 5s   *   $��28��67   �� 5t   *   $��36��67   �� 5u   *   $��48��67   �� 5v   *   $��72��67   5x   #      VOWs.Custom.MenuTextBox  5y   "      Source$' !/Resources/Images/menu_colour.png��65      
Descriptor$ Colour��5z   5   	 � ����5{   5   �� *FontSelectedForeground+5|   5   5}      5~        5      $
 Page��6-   . 5�      A� $ 3��6*   $��1��5�   *   .+�5�   "   �� $%��/Resources/Images/menu_page.png��6)   
 � Tag$ New Page��5�   )     
MenuButton#��[ 5�   )   ��5�   )   �� *Pages_CreatePageCommand+�� 5�   "   $%��/Resources/Images/menu_page.png��6)   $ Remove Page��5�   )   #��[ 5�   )   ��5�   )   �� *Pages_RemovePageCommand+�� 5�   "   $%��/Resources/Images/menu_page.png��6)   $ Clear Pages��5�   )   #��[ 5�   )   ��5�   )   �� *Pages_ClearPagesCommand+5�      5�      5�      �� 5�      $  
Horizontal=�6!   y�5�   !   �� *Content_Text+$ 	Collapsed��5�      �� 5�      $  
Horizontal=�6!   y�5�   !   �� *Content_Media+$ 	Collapsed��5�      �� 5�      $  
Horizontal=�6!   y�5�   !   �� *Content_Page+$ 	Collapsed��.+�5�        $	 New��6-   . 5�      �� $  
Horizontal=�6)   $��1��5�   )   .+�5�   "   �� $%��/Resources/Images/menu_page.png��6)   $ New Page��5�   )     MenuButton2#��[ 5�   )   ��5�   )   �� *Pages_CreatePageCommand+�� 5�   "   $%��/Resources/Images/menu_page.png��6)   $ 	New Slide��5�   )   #��[ 5�   )   ��5�   )   �� *Pages_CreateSlideCommand+5�      5�        5�      . 5�      A� $ 2��6*   $��1��5�   *   .+�5�   "   �� $%��/Resources/Images/menu_page.png��6)   $  Remove Latest Page / Slide��5�   )   #��[ 5�   )   ��5�   )   �� *Pages_RemovePageCommand+�� 5�   "   $%��/Resources/Images/menu_page.png��6)   $ Clear All Pages / Slides��5�   )   #��[ 5�   )   ��5�   )   �� *Pages_ClearPagesCommand+5�      5�      5�      �� 5�      $  
Horizontal=�6!   y�5�   !   �� *Content_View+$ 	Collapsed��5�      �� 5�      $  
Horizontal=�6!   y�5�   !   �� *Content_VersionControl+$ 	Collapsed��5�      �� 5�      $  
Horizontal=�6!   y�5�   !   �� *Content_Image+$ 	Collapsed��5�      �� 5�      $  
Horizontal=�6!   y�5�   !   �� *Content_Video+$ 	Collapsed��5�      5�      5�      �� 5�      $��0,20,0,0q�5�      $��Stretch=�5�      ��5�      �� *PageVM+5�      5�      �� 5�   
   $
��Left=�6   $��75��5�        ThemedBorder#��[ 5�      y�5�      �� *	SideBar+$ 	Collapsed��._�5�      A� $ 7��6   .+�5�      d� $
��Back��6     ThemedToggleButton#��[ 5�       5�      �� *VOWs_Selected+5�      �� 5�      $
��Home��6     ThemedButton#��[ 5�      ��5�      �� *SideMenu_HomeCommand+�� 5�      $
��Info��6   #��[ 5�      ��5�      �� *SideMenu_InfoCommand+�� 5�      $
��Save��6   #��[ 5�      ��5�      �� *SideMenu_SaveCommand+�� 5�      $��Print��6   #��[ 5�      ��5�      �� *SideMenu_PrintCommand+�� 5�      $��Export��6   #��[ 5�      ��5�      �� *SideMenu_ExportCommand+�� 5�      $
��Dir.��6   #��[ 5�      ��5�      �� *SideMenu_DirCommand+5�      5�      5�      5      