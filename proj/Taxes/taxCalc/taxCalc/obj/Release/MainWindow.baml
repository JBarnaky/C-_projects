   M S B A M L   `   `   `  ���� B  >taxCalc, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:taxCalctaxCalc      taxCalc.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:taxCalc       � Title$*  $Учет уплаты налогов��$	��350��$	��650�� � WindowStartupLocation$ CenterScreen=� � Icon$ pict/check(1).png�� � 
ResizeMode$ CanMinimize=�
������� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ ApplicationCommands.New�� J� InputBindings ��  �� Command$ ApplicationCommands.New�� �� Gesture$ Ctrl+1��.��� 	 � RowDefinitions	 �� $��0* ��� .+��� $��30��$��Bottom=�$��2��.@��� 
 � UseLayoutRounding
 . .���� -    ��Name$ tTool��o� $��35��$	��Top=�$��2��$��	130,0,0,0q�.@�m� $��Справочники��$��35��$��92��.@�m� -   $��Плательщики��$
��Left=�$��30��$	��195�� � ToolTip$ Плательщики��m� -   $'��!Виды деятельности��$
��Left=�$��30��$	��195��$' !Виды деятельности��m� -   $��Инспекции��$
��Left=�$��30��$	��195��$ Инспекции��m� -   $��Налоги��$
��Left=�$��30��$	��195��$ Налоги��m� $��Данные��$��33��$��62��.@�m� -   $��Учет налогов��$
��Left=�$��30��$	��190��$ Учет налогов�� m� Icon �� $��pict/check(2).png��m� -   $��График выплат��$
��Left=�$��30��$	��190��$ График выплат�� �� $��pict/tax.png���� $
��Left=�$��12��$	��180�� J� RenderTransformOrigin$ 0.5,0.5&�m� -	   $��
Выход��$
��Left=�$��30��$	��190��$ 
Выход�� �� $��pict/exit.png��m� -
   $��Справка��$��33��$��62��$ О программе��m� -   $��О программе��$��33��$��92��$ О программе��c� $��35��$��	0,0,514,0q�$	��Top=�$��2��.@��� -   $��btNalog��$ Учет налогов��.���� $��pict/check(2).png���� -   $��btGraf��$ График выплат��.���� $��pict/tax.png���� �� -   $��btExit��$' !Завершение работы��4  .System.Windows.Automation.AutomationProperties   HelpText$' !Завершение работы��   AutomationId ��  �� ElementName$ tTool�� �� Mode$ OneWay=�.���� $��pict/exit.png���� $
��Left=�$	��247��$��	10,40,0,0q�$	��Top=�$	��624��$
��Fill=�$��pict/nalog11.jpg��$��2��