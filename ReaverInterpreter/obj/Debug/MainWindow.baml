   M S B A M L   `   `   `  ����L  HReaverInterpreter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ReaverInterpreterReaverInterpreter  5   	   "    ReaverInterpreter.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ReaverInterpreter     5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	     � Title$  Reaver��5	   	   $	��450��6   $	��800��6%    � AllowsTransparency . 
������  � WindowStartupLocation$ CenterScreen=�6d    � WindowStyle$
 None=�6�    � Icon$ 	/logo.png��6�   .��5
      � -   5        .+�5   
   �� 
���   �$��1q�6%   $	��330��69   $��140,0,140,0q�6F   $��Center=�6[   ���	#FF191919._�5      � .+�5      -  'MahApps.Metro.Controls.MetroProgressBar  -   5          Name$ ProgressBar��5   '   $��14��6<   $��
24,84,24,0q�6H   $	��Top=�6\   ���	#FF32B1DC6t   �� 5      $��Reaver��6   $��Center=�6)   $��0,10,0,0q�6F   $	��Top=�6X   
�������$��47��6�   $	��106��6�   $��30(�6�   $��Segoe UI)�6�   �� 5      -   5         ��Name$ DownloadProgress��5      $��0%��62   $��430,78,20,0q�6?   $	��Top=�6T   
�������$��30��6   $��12(�6�   $��Microsoft YaHei)�6�   $��Right=�6�   �� 5      -   5        $ Process��5      $-��'No file currently loaded for processing��6)   $��Center=�6[   $��	0,117,0,0q�6x   $	��Top=�6�   
�������$��31��6�   $	��478��6�   $��16(�6�   $��Gadugi)�6�   $��Center=�6�   �� 5      -   5        $��210,245,210,49q�5      ���	#FF32B1DC
�������$��Upload��6[    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%^   '��       �� 5      " ��6    .�5      �� 	 �@��CornerRadius$
 5��6=   5      5      5      �� 5      -   5        $ Steps��5      $-��'No steps found in file currently loaded��6'   $��Center=�6Y   $��Center=�6v   
�������$��31��6�   $	��478��6�   $��16(�6�   $��Gadugi)�6�   $��Center=�6�   �� 5      -   5        $ 
FileLoaded��5      $+��%File loaded: No file loaded currently��6,   $��Center=�6\   $	��Top=�6y   
�������$��30��6�   $	��478��6�   $��16(�6�   $��Gadugi)�6�   $��Center=�6�   $��	0,180,0,0q�6  �� 5      -   5        $ 	InfoLabel��5      $4��.Downloads: 0     File Moves: 0     Commands: 0��6+   $��Center=�6d   $	��Top=�6�   
�������$��30��6�   $	��478��6�   $��16(�6�   $��Gadugi)�6�   $��Center=�6�   $��	0,210,0,0q�6
  �� 5      -	   5        $	����5      $
��Left=�6,   $��	483,0,0,0q�6G   $	��Top=�6Z   
�������$��34��6�   $��34��6�   $��25(�6�   $��Segoe MDL2 Assets)�6�   ���	#FF191919
������ 6�   �� 5      $
��Left=�6   $��34��63   $	��Top=�6?   $��36��6W   $��	/logo.png��6b   5      5      5       5!      