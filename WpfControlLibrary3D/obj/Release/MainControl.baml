   M S B A M L   `   `   `  ���� N  JWpfControlLibrary3D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WpfControlLibrary3DWpfControlLibrary3D  %    WpfControlLibrary3D.MainControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:WpfControlLibrary3D     .��� -     �Name$  mainGrid�� � ColumnDefinitions �� �� $	��200 �.+�'� -    '�Name$ main3D��}�.   J� IsHitTestVisible .   '� Camera =� -   	cameraPer�� � Position$ 300,200,1001� � LookDirection$ -300,-200,-1005� � UpDirection$ 0,0,15� �� 	Transform Y� .��� 	 �� ScaleX$	 1Q�
 �� ScaleY$
 1Q� �� ScaleZ$ 1Q���  �� Rotation ��  �� Axis$ 1,0,05� �� Angle ��  �� ElementName$
 sliX�� �� Path$ Value���  �� $ 0,1,05� �� $
 sliY��$ Value���  �� $ 0,0,15� �� $
 sliZ��$ Value�.x�g�  g� Content h� -   
modelGroup��.����  �� Color$ White��� -   head�� � Geometry k�  k� 	PositionsH �   �������� ���@KL ���� ���@KL @KL  �������@KL  ��� k� TriangleIndices �        j� 	Transform S�  S� OffsetX$ -1Q� S� OffsetY$ -1Q� S� OffsetZ$ -1Q� � Material u�  u� Brush �#f6416c � BackMaterial u�  �#ffde7d�� $��1��.+��� $��移动相机：���� -    ��Name$ viewX�� � Maximum$
 1000Q� � Minimum$ -1000Q�  � Value$	  300Q�! � SmallChange$! 1Q�" J� SnapsToDevicePixels" . �� -   $ viewY��$
 1000Q�$ -1000Q�$	  200Q�$! 1Q�" . �� -   $ viewZ��$
 1000Q�$ -1000Q�$	  100Q�$! 1Q�" . �� $��旋转模型：���� -	   $
 sliX��$	 360Q�$
 -360Q�$	  180Q�$! 1Q�" . �� -
   $
 sliY��$	 360Q�$
 -360Q�$  0Q�$! 1Q�" . �� -   $
 sliZ��$	 360Q�$
 -360Q�$  90Q�$! 1Q�" . �� -   # ��Name$# btnReset��$��加载预设角度��