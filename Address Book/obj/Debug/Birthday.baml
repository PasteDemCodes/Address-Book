   M S B A M L   `   `   `  ����G  CAddress Book, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Address_BookAddress_Book  5   	       Address_Book.Birthday   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Address_Book     5   	     � Title$  Birthday��5   	   $��307.992��6   $��789.574��6+    � 
ResizeMode$ NoResize=�6;    � WindowStyle$
 None=�6Q    � Icon$ icon_logo.png��6d   .��5	      � ���	#FF191919$��
0,0,-184,0q�6"   $
��Left=�66   $��792.074��6Q   .+�5
   
   � -   5         �Name$ 	Grid_body��5
      $��
0,77.5,0,0q�6"   ���	#FF282828$
��Left=�6s   $��790.574��6�   .+�5      �� -   5         ��Name$ Label_Events��5      $��Today's Birthdays:��6*   $��8.104,-39.334,0,0q�6G   $	��Top=�6b   $��18(�6z   $��Center=�6�   $��Center=�6�   $
��Left=�6�   
�������6  �� 5      -   5        $ Label_Birthdays��5      $�� ��6-   $��66.985,71.94,0,0q�68   $	��Top=�6R   $��16(�6j   $��Center=�6x   $��Center=�6�   $
��Left=�6�   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dataGrid_Birthday��5      $��0,0.606,0,10.528q�6J   $��14(�6d      AutoGenerateColumns .  $��2q�6�      CanUserAddRows .  	   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 * 6�   
 �� SelectedIndex$
 0��6�   ��62   ��    ColumnHeaderStyle 5      �� 5       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5      ��  �� Property � �� �� Value$
 Bold"�68   �� 5       � ��$ Stretch=�6@   �� 5       � ��$ Center=�6G   5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width$ 40 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *ID+6L     5      $
 Name��6)   $	 100 6P    67   �� *Name+6P     5      $ Surname��6)   $	 100 6V    6:   �� *	Surname+6V     5      $ Phonenumber��6)   $	 110 6^    6>   �� *Phonenumber+6^     5      $ Email��6)    68   �� *Email+  5      $ Address��6)    6:   �� *	Address+  5      $ Birthday��6)    6;   �� *
Birthday+5      5       � 5"   
   -   5        $ 	Grid_logo��5"      $��0,0,0,276.658q�6"   ���	#FF282828$
��Left=�6P   $��790.574��6k   .+�5#      �� -   5         ��Name$ 
Image_logo��5#      $
��Left=�6(   $��33.971��6C   $	��Top=�6S   $��34.827��6k   $
��Fill=�6z   $��icon_logo.png��6�    J� OpacityMask 5$      �� 5%       �� ImageSource$ icon_logo.png��6!   5'      �� 5(      -   5         ��Name$ Button_exit��5(      $��X��6*   $
��Left=�66   $��755.065,4.83,0,0q�6Q   $	��Top=�6k   $��25.509��6�   $��23.971��6�   ���	#FF191919$��14(�6�   ��6�   Z� ��6�   Z� 6  �� 5)      -   5        $ 
Label_logo��5)      $��Birthday��6(   $
��Left=�6;   $��10,0,0,0q�6V   $	��Top=�6h   $��31.971��6�   $��114.242��6�   $��16(�6�   $��Center=�6�   $��Center=�6�   
�������6�   5+      5-      5.      