   M S B A M L   `   `   `  ����  
TestWpfApp%    TestWpfApp.Software3DLeakWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  Software3DLeakWindow��5      $	��300��6"   $	��300��6/    � 	Resources 5         
leakyModel g� Content �� Color � Material u� Brush !� Visual � Geometry k� 	Positions	 k� TextureCoordinates��%1  &	        g� 5       5      h� 5   
   .��5	      �� $ white��6   � 5
       5      u� 5       5      !� 5       5      �� 5      $��!��6   5      5       5      k� 5       �   	 �   6=   5      5      5      5      .��5      � .+�5       '� -   5        $��theViewport��5       }�. 
 '� Camera
 5"      =� 5#      -   5        
Camera�� � Position$ 0,.5,21�5$       � LookDirection$ 0,0,-15�5%       � UpDirection$ 0,1,05�5&       =� FieldOfView$ 90Q�5'      .x�5+      g�  5,      h� 5-   
   .��5.      �� $ white��6   � 5/       50      u� 51       52      �� 53       �� Color$	 Red��6&   55       57      k� 58       �   	 �   6=   5:      5;      5=       � Triggers 5?      :� 5@       :� RoutedEvent$ Viewport3D.Loaded��6   .[�5A   
   �� .��5B      �� .��5C      W�  �� 
TargetName$ Camera��6    �� TargetProperty$ FieldOfView�5D        W� From$
 90.0X�5E       	 W� To$
 80.0X�5F        g� Duration$ 0:0:2D�5G        g� AutoReverse .  g� RepeatBehavior$ Forever��5I       5K      5L      5M   	   5O      5P      5Q      