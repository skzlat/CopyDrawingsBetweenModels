   M S B A M L   `   `   `  ????  CopyDrawingsBetweenModels*    $CopyDrawingsBetweenModels.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     ? Title$I  CКопирование чертежей между моделями??5   	   $	??500??5   	   $
??1000??6   $	??250??5	   	   $	??700??6    ? 	Resources 5       ?? 
TargetType ?? Property ?? Value ?? RelativeSource ?? Path?? 5   
   %w   '??       ?? 5      " ??6   .?5      ??  ?@?ToolTip 6,   ??  ?? *Self+$
 Text?5      5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??* ?6   ?? 5      $??30 ?6   ?? 5      $??30 ?6    ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6   .+?5!   
   ? $??0??6   $??0??6    5"      ?? 5#      $	??110 ?6#   ?? 5$      $??* ?6#   ?? 5%      $??30 ?6#   .+?5'      ?? $??0??6   $!??Отчёт исходный??6(   ?? 5(      -   5        	 ??Name$	 TB_SourceReport??5(   &   $??1??6   $%??DrawingCopyFromModelToModel.xml??6?   ?? 5)      -   5        $??2??5)      $	??...??6%   63   5*      ? 5+   
   $??1??6   $??0??6    5,      ?? 5-      $	??110 ?6#   ?? 5.      $??* ?6#   ?? 5/      $??30 ?6#   .+?51      ?? $??0??6   $%??Чертежи исходные??6(   ?? 52      -   5        $	 TB_SourceDrawings??52   &   $??1??6   $B??<Проверь путь после выбора отчёта??6A   ?? 53      -   5        $??2??53      $	??...??6&   64   54      ? 58   
   $??0??6   $??1??6    59      ?? 5:      $	??110 ?6#   ?? 5;      $??* ?6#   ?? 5<      $??30 ?6#   .+?5>      ?? $??0??6   $??Отчёт целевой??6(   ?? 5?      -   5        $	 TB_DestinationReport??5?   &   $??1??6   $%??DrawingCopyFromModelToModel.xml??6D   ?? 5@      -   5        $??2??5@      $	??...??6%   63   5A      ? 5B   
   $??1??6   $??1??6    5C      ?? 5D      $	??110 ?6#   ?? 5E      $??* ?6#   ?? 5F      $??30 ?6#   .+?5H      ?? $??0??6   $#??Чертежи целевые??6(   ?? 5I      -   5        $	 TB_DestinationDrawings??5I   &   $??1??6   $B??<Проверь путь после выбора отчёта??6F   ?? 5J      -   5        $??2??5J      $	??...??6%   63   5K      &   System.Windows.Controls.DataGrid  5M   
   -	   5        
  Name$
 	DG_Source??5M   =    J? 	AllowDrop .    SelectionMode$ Extended=?6$   $??2??6P   $??0??6]      AutoGenerateColumns .     
IsReadOnly .    RowHeaderWidth$ 0Q?6?      Columns 5N      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Метка??6B      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ?? *
NameBase+6Q     5P      $ Файл чертежа??6F   $ 2* 6\    6%   ?? *DrwFilename+6\   5R      ?? 5S   
   $??3??6   $??0??6"    ?? Orientation$ 
Horizontal=?62   $??Right=?6K   $??Bottom=?6g   $??26??6?   $??84??6?   .+?5T      ?? $??Чертежей:??6   ?? 5U      $
??Bold"?6P   ??6   ??  ?? ElementName$ 	DG_Source??$ Items.Count?6P   5V        5X   
   -
   5        $
 DG_Dest??5X   =    . $ Extended=?6$   $??2??6N   $??1??6[    .   . $ 0Q?6?    5Y        5Z      $ 
Метка??6B   $ 1* 6Q    6%   ?? *
NameBase+6Q     5[      $ Файл чертежа??6F   $ 2* 6\    6%   ?? *DrwFilename+6\   5]      ?? 5^   
   $??3??6   $??1??6"   $ 
Horizontal=?62   $??Right=?6K   $??Bottom=?6g   $??26??6?   $??84??6?   .+?5_      ?? $??Чертежей:??6   ?? 5`      $
??Bold"?6N   ??6   ?? $ DG_Dest??$ Items.Count?6N   5a      ?? 5c   
   -   5        $??3??5c      $??2??6   $??Скопировать??62   $??0,30,0,0q?6]   $??2??6o   5d      5e      