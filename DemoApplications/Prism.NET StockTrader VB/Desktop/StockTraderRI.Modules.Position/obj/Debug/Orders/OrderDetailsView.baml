   M S B A M L   `   `   `  ����W  SStockTraderRI.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullqPclr-namespace:StockTraderRI.Infrastructure;assembly=StockTraderRI.InfrastructureStockTraderRI.Infrastructure  5   �   �[clr-namespace:StockTraderRI.Infrastructure.Converters;assembly=StockTraderRI.Infrastructure'StockTraderRI.Infrastructure.Converters  6�   " StockTraderRI.Modules.Position<   6StockTraderRI.Modules.Position.Orders.OrderDetailsView   6   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6P   `infPclr-namespace:StockTraderRI.Infrastructure;assembly=StockTraderRI.Infrastructure     6�   uinfConverters[clr-namespace:StockTraderRI.Infrastructure.Converters;assembly=StockTraderRI.Infrastructure     6�   -   5          >�Name$  UserControl��5   �   � 	Resources 5         ErrorTemplate<   6StockTraderRI.Infrastructure.Converters.ErrorConverter  errorConverter  textBoxInError �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� RelativeSource	 �� Path
 �� 	ConverterD   >StockTraderRI.Infrastructure.Converters.EnumToBooleanConverter  enumToBooleanConverterM   GStockTraderRI.Infrastructure.Converters.StringToNullableNumberConverter # stringToNullableNumberConverter��%�  &	        &	 Z     &	 m     7  &	 *    &	 =    �� 5      .R�5      �� 
���  ��$��2q�6   ._�5      �� 5      5        5	      6!   �� 5
      " ��6!    5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip 6!   ��  �� *Self+$	 (Validation.Errors)�8
   5      5        5      6)     5      62   5      .��5      � -   5         �Name$ 
LayoutRoot��5      $��Stretch=�6   $��Stretch=�6:   $��0,0,0,0q�6V    � RowDefinitions 5      �� 5      $��35 �6   �� 5      $��35 �6    � ColumnDefinitions 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $��* �6   .+�5#      �� $��0��6   $��2��6    �� Orientation$ 
Horizontal=�60   $��Center=�6I   $��Center=�6d   .+�5$      	� -   5        $��BuyRadio��5$      4  .System.Windows.Automation.AutomationProperties   AutomationId$ BuyRadioButton��6!   $��0,0,15,0q�6�   $
��Left=�6  $��Center=�6#   d� 	IsChecked 6T   �� $	 TransactionType� �� Mode$ TwoWay=�#
 [ 2   ,StockTraderRI.Infrastructure.TransactionType
   Buy �� ConverterParameter# Z .��6>  Buy6C  	� 5%      -   5        $��	SellRadio��5%      $ SellRadioButton��6"   $��15,0,0,0q�6�   $
��Left=�6  $��Center=�6&   6V   �� $	 TransactionType�$ TwoWay=�#
 [    Sell# Z .��6A  Sell6G  5&      �� 5(      $��0��6   $��2��6   $��Shares:��6+   ���	#FF444444$��Center=�6Q   $
��Auto��6l   $
��Auto��6y   $��Right=�6�   $��1��6�   $��5,0,0,0q�6�   �� 5)      -   5        $��SharesTextBox��5)   [   $��0��6   $��3��6   $ SharesTextBox��6)    =� ErrorTemplate# [  6D  $��Stretch=�6~  $	��120��6�  $
��Auto��6�  $��Center=�6�  $��5,0,0,0q�6�  ��6p   �� $	 Shares� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� NotifyOnValidationError .  �� ValidatesOnDataErrors . #
 [ 6�  �� 5+      $��0��6   $��4��6   $��Price Limit:��6+   ���	#FF444444$��Right=�6V   $��Center=�6r   $
��Auto��6�   $
��Auto��6�   $��5,0,0,0q�6�   �� 5,      -   5        $��StopLimitPriceTextBox��5,      $ PriceLimitTextBox��6)   # [  6;  $��0��6u  $��5��6�  $
��Left=�6�  $	��120��6�  $
��Auto��6�  $��Center=�6�  $��5,0,0,0q�6�  ��6_   �� $	 StopLimitPrice�$ PropertyChanged=� .  .  . #
 [ 6�  �� 5.      $��1��6   $��0��6   $��Right=�6+   $
��Auto��6G   $
��Auto��6T   $��Order Type:��6b   ���	#FF444444$��Center=�6�   $��5,0,0,0q�6�   �� 5/      -   5         ��Name$ OrderTypeCombo��5/      $ OrderTypeComboBox��6%   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6[    �� SelectedValuePath$ Value��6�   $��1��6�   $��1��6	  $	��120��6  $
��Auto��6%  $��Center=�63  $��Stretch=�6N  $��5,0,0,0q�6l   �� SelectedValue 6�   �� $	 	OrderType�$ TwoWay=���6�   �� *AvailableOrderTypes+6l  �� 51      $��1��6   $��2��6   $��Term:��6+   ���	#FF444444$��Right=�6O   $��Center=�6k   $
��Auto��6�   $
��Auto��6�   $��5,0,0,0q�6�   �� 52      -   5        $��TimeInForceCombo��52      $ TermComboBox��6%   $
 TrueY�6V   $ Value��6�   $��1��6�   $��3��6  $��Stretch=�6  $	��120��61  $
��Auto��6=  $��Center=�6K  $��5,0,0,0q�6f   6{   �� *TimeInForce+$ TwoWay=���6�   �� *AvailableTimesInForce+6f  53      54      