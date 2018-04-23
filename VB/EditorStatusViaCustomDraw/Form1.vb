Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Namespace EditorStatusViaCustomDraw
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			dataTable1.Rows.Add("some value")
			dataTable1.Rows.Add(String.Empty)
			dataTable1.Rows.Add(DBNull.Value)
			dataTable1.Rows.Add("not empty")
		End Sub

		Private Sub gridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gridView1.CustomDrawCell
			If e.Column.Name = "colCheckIndicator" Then
				Dim text As String
				Dim isChecked As Boolean
				Dim grid As GridView = TryCast(sender, GridView)
				If grid.IsEditing AndAlso e.RowHandle = grid.FocusedRowHandle AndAlso grid.FocusedColumn.FieldName = "Column1" Then
					text = grid.ActiveEditor.Text
				Else
					text = grid.GetRowCellDisplayText(e.RowHandle, grid.Columns("Column1"))
				End If
				isChecked = (text IsNot Nothing AndAlso text <> String.Empty)
				Dim cell As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo = CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)
				Dim info As DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo = CType(cell.ViewInfo, DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo)
				info.EditValue = isChecked
			End If
		End Sub

		Private Sub repositoryItemTextEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles repositoryItemTextEdit1.EditValueChanged
			Dim grid As GridView = gridView1
			grid.InvalidateRowCell(grid.FocusedRowHandle, grid.Columns.ColumnByName("colCheckIndicator"))
		End Sub
	End Class
End Namespace