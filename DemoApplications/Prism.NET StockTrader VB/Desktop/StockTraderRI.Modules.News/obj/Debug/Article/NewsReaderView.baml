   M S B A M L   `   `   `  ����  StockTraderRI.Modules.News7    1StockTraderRI.Modules.News.Article.NewsReaderView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6J   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6�   -   5          >�Name$  
newsReader��5   �   $
��Auto��6�   $
��Auto��6�   $��0,0,0,0q�6�    � 	Resources 5       
  FadeIn g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value��%�   &	        �� 5      .��5      U� $ 00:00:00X�6#   $ 
newsReader��68   $ (UIElement.Opacity)�6[   .\�5      �� $ 00:00:00��6   $ 0Q�6.   �� 5      $ 00:00:00.5000000��6   $ 1Q�66   5      5      5	       � Triggers 5
      :� 5       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� #��[  6   5      .��5      �� $��1q�6
   	 �� Padding$	 5q�6   ���	#FFFFFFFF._�5      � 
 � RowDefinitions
 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      �� $��0��6   $��5q�6   $
��Auto��6'   $��11(�65   $
��Bold"�6C   
�����F� �� TextAlignment$
 Left=�6l   $
��Wrap=�6�   $
��Left=�6�   $
��Auto��6�   ��6�   �� *NewsArticle.PublishedDate+6�   �� 5      $��1��6   $
��Auto��6   $��5q�6*   $��18(�65   $��Normal"�6C   
�����F�$
 Left=�6n   $
��Wrap=�6�   ��6�   ��  �� Path$ NewsArticle.Title�6�   �� 5      $��2��6   
������ ���	#FFFFFFFF$��0,0,0,0q�6K   $��12(�6e   $��Normal$�6s   $��Normal"�6�   ���	#FF000000 J� Opacity$ 1Q�6�    �� TextAlignment$
 Left=�6�    �� TextWrapping$
 Wrap=�6   �� 
LineHeight$
 Auto��6  $
��Left=�67  $	��Top=�6R    PopupTextBoxStyle#��[ 6j   �� 
IsReadOnly .  J� OpacityMask 6�   Z� ��6�   �� *NewsArticle.Body+6�  5      5      5      