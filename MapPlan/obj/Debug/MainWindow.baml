   M S B A M L   `   `   `  ����F  BMapsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:MapsManager;assembly=MapsManagerMapsManager  5   	   I EScriptsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ScriptsManager;assembly=ScriptsManagerScriptsManager 5   	    MapPlan   MapPlan.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gXceed.Wpf.AvalonDock.Themes.Metro, Version=2.0.2000.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4^ ZXceed.Wpf.AvalonDock, Version=2.0.2000.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4?
avalonDock,http://schemas.xceed.com/wpf/xaml/avalondock   5   	   Bmapsmanager.clr-namespace:MapsManager;assembly=MapsManager   5   	   Kscriptsmanager4clr-namespace:ScriptsManager;assembly=ScriptsManager   5   	     � Title$  MapPlan��5   	   $	��350��6   $	��525��6&    � WindowState$ 	Maximized=�62    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5	   
   -   5           Command$	 New��5	      6'     5
   
   -   5        $
 Open��5
      6(     5   
   -   5        $
 Save��5      6(   .��5      �  � RowDefinitions 5   
   �� 5      $��24 �6   6*   �� 5      $��24 �6   6*   �� 5      $��* �6   6)   �� 5      $��24 �6   6*   .+�5   
   o�  o� 
IsMainMenu . $��0��6"   .@�5      m� $��_File��6   .@�5      m� -   5        $
��_New��5      $��20��6+    m� Icon 5      �� 5      $��/Icons/NewDocumentHS.png��6    $��16��6B   $��16��6M   5      m� 5      $��_Open��6   $��20��6*    5      �� 5      $��/Icons/openfolderHS.png��6    $��16��6A   $��16��6L   5       �� 5!      $
��Left=�6   $	��100��67   m� 5"      $��_Save��6   $��20��6*    5#      �� 5$      $��/Icons/saveHS.png��6    $��16��6;   $��16��6F   5&      m� 5'      $��_SaveAs��6   $��20��6,   6:   �� 5(      $
��Left=�6   $	��100��67   m� 5)      $��_Close��6   $��20��6+   69   m� 5*      $��_Exit��6   $��20��6*   68   5+      m� 5,      $��_View��6   .@�5-      m� -   5        $��Scripts��5-       m� IsCheckable .  m� 	IsChecked .  6i   m� 5.      -   5        $
��Maps��5.       . 6Q   m� 5/      -   5        $��	Customers��5/       . 6[   m� 50      -   5        $��Nodes��50       . 6S   m� 51      -	   5        $��Links��51       . 6S   m� 52      -
   5        $��Services��52       . 6Y   m� 53      -   5        $��Connections��53       . 6_   m� 54      -   5        $��	StartPage��54       . 6[   55      m� 56      $��_Help��6   6(   57      `� 59   
   $��1��6   .�5:      c� 	 `� IsLocked	 . .@�5;      �� $	��New��6   
 � ToolTip$
 New Project��6'   .��5<      �� $��/Icons/NewDocumentHS.png��6   $��16��6>   $��16��6I   5=      �� 5>      $
��Open��6   $
 Open Project��6(   .��5?      �� $��/Icons/openfolderHS.png��6   $��16��6=   $��16��6H   5@      �� 5A      $
��Save��6   $
 Save Project��6(   .��5B      �� $��/Icons/saveHS.png��6   $��16��67   $��16��6B   5C      �� 5D      $
 SaveAll Project��6   .��5E      �� $��/Icons/SaveAllHS.png��6   $��16��6:   $��16��6E   5F      5G      5H      )  #Xceed.Wpf.AvalonDock.DockingManager  5I   
   -   5          Name$ dockingmanager��5I   $   $��2��6<   
�������   Layout. 5J      ,  &Xceed.Wpf.AvalonDock.Layout.LayoutRoot     	RootPanel. 5K      -  'Xceed.Wpf.AvalonDock.Layout.LayoutPanel     Orientation$ 
Horizontal=�6)   -   5        m_LayoutPanel��5K   B   5L      5{      �� 5|   
   $��3��6   6$   5}      5~      