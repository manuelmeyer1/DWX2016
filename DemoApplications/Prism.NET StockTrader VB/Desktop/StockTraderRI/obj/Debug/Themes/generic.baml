   M S B A M L   `   `   `  ����H  DStockTraderRI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:StockTraderRI.ControlsStockTraderRI.Controls  5   W   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TabControlNormalBorderBrush  �� Color/    )StockTraderRI.Controls.AnimatedTabControl �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels-  'System.Windows.Input.KeyboardNavigation   TabNavigation � RowDefinitions �� CornerRadius	 ��Name
 K� IsItemsHost   TabIndex '�Name '� Camera � Position � LookDirection =� FieldOfView g� Content �� Color t� 	Direction � Geometry k� TriangleIndices k� TextureCoordinates k� 	Positions � Material u� Brush j� Stretch !� Visual �� ElementName � BackMaterial �� RelativeTransform �� ScaleX  �� CenterX! j� 	Transform" �� Rotation# �� Axis$ �� Angle% ��Name'   !StockTraderRI.Controls.RoundedBox& ��Name' �� Triggers( :� RoutedEvent) �� 
TargetName* �� TargetProperty+ R� KeyTime  System.Windows.Visibility,   Visible- R� Value.   Hidden	/ W� To0 g� Duration1 g� 	BeginTime	2 6� To3 6� From4 g� AutoReverse5 g� DecelerationRatio6 W� From7 g� AccelerationRatio�� 6   D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    1Controls$clr-namespace:StockTraderRI.Controls  6W   :x,http://schemas.microsoft.com/winfx/2006/xaml     6�   %�  &	        '   !     7   �� 5      $  #8C8E94��67   �� 5      "   6	   .�5      ��  � ��# � ��6!   �� 5       � ��$ 4,4,4,4q�6   �� 5       � ��$ 1q�6&   �� 5       � ��8   6"   �� 5	       � �� �#F9F9F96!   �� 5
       � ��$ Stretch=�61   �� 5       � ��$ Stretch=�6/   �� 5       � �� 5      �� 5      "   6   .R�5      � }�.  . $ Local=�6;    5      �� 5   	   $
��Auto �6   �� 5   	   $��* �6   .+�5      �� $��30��6   $��
10,0,10,10q�6   $
��Left=�6/   $	��Top=�6J   $
��Auto��6b   $��1��6o   $ 12,12,12,12��6|   #��z��6�   $��2,2,2,2q�6�   ���	#FFFFFFFF._�5   	   �� $	 HeaderPanel��6   $��Center=�6'   $��Center=�6D   
 . $��0��6r   $��0��6�   $ 1��6�   5   	   � 5      $��1��6   $��0,40,0,0q�6   $��Stretch=�6,   $��Stretch=�6J   .+�5   	   '� $
 vp3D��6   $y�Hidden=�6"   $
��Auto��66   $
��Auto��6C    5   
   =� 5      
camera��$ 0,0,0.51�6-   $ 0,0,-15�6@   $ 90Q�6W   x�5   
   g� 5        5!      h� 5"      .��5#      t� $
 #444��6   $ 0,0,-15�6,   �� 5$      $
 #BBB��6   5%      5'      g� 5(       5)      � 5*       5+      k� 5,       �      �   5 �   �^���^�� � �^�� �  � �^�� � 6a    5.      u� 5/       50      !� 51      $ Uniform=�6E    6   �� $ BorderIn��6E   53       55      u� 56       57      !� 58      $ Uniform=�6E    6   �� $ BorderIn�� 59      �� 5:      $ -1Q�6"   $	  0.5Q�6.   5<      5>      ! 5@      �� 5A      " 5B      �� 5C      
rotate��$# 0,3,05�65   $$ 0Q�6B   5E      5G      5I      5K   
   �� 5L   	   $% 	BorderOut��6   $��Stretch=�6#   ._�5M   
   �� $% BorderIn��6   $��Stretch=�6#   ���	#00000000._�5N      � .+�5O        $��10q�6    �� 5P      $& PART_SelectedContentHost��6   $��SelectedContent��6?   $��20q�6_   5Q      5R      5S   
   5T   	   5U      ' 5W      :� 5X      $3( -Controls:AnimatedTabControl.SelectionChanging��6   .[�5Y   	   �� .��5Z   
   �� .��5[      T� $
) vp3D��6)   $* 
Visibility�6F   ..�5\      i� $+ 0:0:0��6#   #- Z, 63   i� 5]      $+ 0:0:1.1��6#   #- Z. 65   5^      W� 5_      $/ 0X�6   $0 0:0:0.05D�6"   $) 	BorderOut��66   $* Opacity�6X   W� 5`      $1 0:0:1.05X�6   $0 0:0:0.05D�60   $/ 1X�6D   $) 	BorderOut��6K   $* Opacity�6m   6� 5a      $2 0,0,1.1X�6   $3 0,0,0.5X�6)   $1 0:0:0.05X�68   $0 0:0:0.5D�6M   4 . $	5 0.3Q�6s   $) camera��6�   $"* (PerspectiveCamera.Position)�6�   W� 5b      $6 0X�6   $	/ 180X�6$   $	7 0.3Q�6-   $	5 0.3Q�6E   $1 0:0:0.05X�6]   $0 0:0:1D�6r   $) rotate��6�   $* Angle�6�   5c      5d   
   5e   	   5g      5k      5l      5n      