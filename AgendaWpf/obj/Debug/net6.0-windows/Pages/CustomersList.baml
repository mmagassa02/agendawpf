   M S B A M L   `   `   `  ����D  @AgendaWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AgendaWpf.PagesAgendaWpf.Pages  5      #    AgendaWpf.Pages.CustomersList   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AgendaWpf.Pages     5        N� Title$  Customers List��5	       N� 
Background
 �����.��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   6*   �� 5      $��5* �6   6*   .+�5   
   �� $��0��6   $��Customers list��6!   $��25(�67   $
��Bold"�6E    �� TextAlignment$ center=�6W   6p   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��2* �6#   60   �� 5      $��1* �6#   60   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��customerlist��5   '   $��0��6      AutoGenerateColumns$ False��6;      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	FirstName��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� 	 �� Path$	 	Firstname�
 �� Mode$
 TwoWay=�  5      $ LastName��6)    6;   �� $	 Lastname�$
 TwoWay=�  5      $ Email��6)    68   �� $
	 Mail�$
 TwoWay=�  5      $ Phone��6)    68   �� $	 PhoneNumber�$
 TwoWay=�  5      $ Budget��6)    69   �� $	 Budget�$
 TwoWay=�4  .System.Windows.Controls.DataGridTemplateColumn  5       $
 Edit��6-      CellTemplate 5!      �� 5"      .R�5#   "   �� -   5        $
��Edit��5#   )   6N   5$      5&        5'      $ Delete��6-    5(      �� 5)      .R�5*   "   �� -   5        $��Delete��5*   )   6S   5+      5-      5/      �� 50      -   5        $��EditMenu��50   )   $��1��6   $y�	Collapsed=�69   .+�51      �� $��
IdCustomer��6B   $y�Hidden=�6W    �� Target 6   ��  �� ElementName$ 
customerId��6m   �� 52      -   5        $��
customerId��52      $	��200��6,   $y�Hidden=�68   6N   �� 53      $��	Firstname��6I    6   �� $ customerFirstname��6_   �� 54      -   5        $��customerFirstname��54      $	��200��63   6A   �� 55      $��Lastname��6H    6   �� $ customerLastname��6]   �� 56      -   5        $��customerLastname��56      $	��200��62   6@   �� 57      $��Email��6E    6   �� $ customerEmail��6W   �� 58      -   5        $��customerEmail��58      $	��200��6/   6=   �� 59      $��Phone��6E    6   �� $ customerPhone��6W   �� 5:      -	   5        $��customerPhone��5:      $	��200��6/   6=   �� 5;      $��Budget��6F    6   �� $ customerBudget��6Y   �� 5<      -
   5        $��customerBudget��5<      $	��200��60   6>   �� 5=      -   5        $
��Edit��5=      $	��200��6(   $��0 5 0 0q�64   6\   �� 5>      $��Cancel��6   $	��200��6*   $��0 5 0 0q�66   5?      5@      5B      5C      