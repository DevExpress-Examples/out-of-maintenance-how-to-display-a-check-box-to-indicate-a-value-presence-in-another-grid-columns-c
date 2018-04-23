using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace EditorStatusViaCustomDraw {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            dataTable1.Rows.Add("some value");
            dataTable1.Rows.Add(string.Empty);
            dataTable1.Rows.Add(DBNull.Value);
            dataTable1.Rows.Add("not empty");
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
            if(e.Column.Name == "colCheckIndicator") {
                string text;
                bool isChecked;
                GridView grid = sender as GridView;
                if(grid.IsEditing && e.RowHandle == grid.FocusedRowHandle && grid.FocusedColumn.FieldName == "Column1") {
                    text = grid.ActiveEditor.Text;
                }
                else
                    text = grid.GetRowCellDisplayText(e.RowHandle, grid.Columns["Column1"]);
                isChecked = (text != null && text != string.Empty);
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo cell = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)e.Cell;
                DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info = (DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo)cell.ViewInfo;
                info.EditValue = isChecked;
            }
        }

        private void repositoryItemTextEdit1_EditValueChanged(object sender, EventArgs e) {
            GridView grid = gridView1;
            grid.InvalidateRowCell(grid.FocusedRowHandle, grid.Columns.ColumnByName("colCheckIndicator"));
        }
    }
}