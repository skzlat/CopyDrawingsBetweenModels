using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WF = System.Windows.Forms;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CopyDrawingsBetweenModels
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DG_Source.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(SourceDataGrid_PreviewMouseLeftButtonDown);
            DG_Source.Drop += new DragEventHandler(SourceDataGrid_Drop);

            DG_Dest.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DestDataGrid_PreviewMouseLeftButtonDown);
            DG_Dest.Drop += new DragEventHandler(DestDataGrid_Drop);
        }

        int rowIndex = -1;
        public delegate Point GetPosition(IInputElement element);

        void SourceDataGrid_Drop(object sender, DragEventArgs e)
        {
            if (rowIndex < 0) return;
            int index = this.GetCurrentRowIndex(e.GetPosition, DG_Source);
            if (index < 0) return;
            if (index == rowIndex) return;
            if (index == DG_Source.Items.Count - 1)
            {
                MessageBox.Show("This row-index cannot be drop");
                return;
            }
            Drawing changedProduct = sourceList[rowIndex];
            sourceList.RemoveAt(rowIndex);
            sourceList.Insert(index, changedProduct);
            DG_Source.ItemsSource = sourceList;
            DG_Source.Items.Refresh();
        }
        void DestDataGrid_Drop(object sender, DragEventArgs e)
        {
            if (rowIndex < 0) return;
            int index = this.GetCurrentRowIndex(e.GetPosition, DG_Dest);
            if (index < 0) return;
            if (index == rowIndex) return;
            if (index == DG_Dest.Items.Count - 1)
            {
                MessageBox.Show("This row-index cannot be drop");
                return;
            }
            Drawing changedProduct = destList[rowIndex];
            destList.RemoveAt(rowIndex);
            destList.Insert(index, changedProduct);
            DG_Dest.ItemsSource = destList;
            DG_Dest.Items.Refresh();
        }
        void SourceDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rowIndex = GetCurrentRowIndex(e.GetPosition, DG_Source);
            if (rowIndex < 0) return;
            DG_Source.SelectedIndex = rowIndex;
            if (!(DG_Source.Items[rowIndex] is Drawing selectedEmp)) return;
            DragDropEffects dragdropeffects = DragDropEffects.Move;
            if (DragDrop.DoDragDrop(DG_Source, selectedEmp, dragdropeffects) != DragDropEffects.None)
                DG_Source.SelectedItem = selectedEmp;
        }
        void DestDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rowIndex = GetCurrentRowIndex(e.GetPosition, DG_Dest);
            if (rowIndex < 0) return;
            DG_Dest.SelectedIndex = rowIndex;
            if (!(DG_Dest.Items[rowIndex] is Drawing selectedEmp)) return;
            DragDropEffects dragdropeffects = DragDropEffects.Move;
            if (DragDrop.DoDragDrop(DG_Dest, selectedEmp, dragdropeffects) != DragDropEffects.None)
                DG_Dest.SelectedItem = selectedEmp;
        }
        private int GetCurrentRowIndex(GetPosition pos, DataGrid dataGrid)
        {
            int curIndex = -1;
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                DataGridRow itm = GetRowItem(i, dataGrid);
                if (GetMouseTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }
        private bool GetMouseTargetRow(Visual theTarget, GetPosition position)
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point point = position((IInputElement)theTarget);
            return rect.Contains(point);
        }
        private DataGridRow GetRowItem(int index, DataGrid dataGrid)
        {
            if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return null;
            return dataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        }
        public class Drawing
        {
            public string NameBase { get; set; }
            public string DrwFilename { get; set; }
        }

        List<Drawing> sourceList;
        List<Drawing> destList;

        public string SourceReportPath { get; set; }

        private void SelectSourceReport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Xml файлы (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                TB_SourceReport.Text = openFileDialog.FileName;
                DirectoryInfo SourceDrawDI = Directory.GetParent(openFileDialog.FileName);
                SourceDrawDI = Directory.GetParent(SourceDrawDI.FullName);
                TB_SourceDrawings.Text = System.IO.Path.Combine(SourceDrawDI.FullName, "drawings");

                DG_Source.ItemsSource = null;
                DataSet SourceDS = Functions.ReportDataSet(openFileDialog.FileName);

                DataTable sourceDT = SourceDS.Tables["Drawing"];
                sourceList = new List<Drawing>();
                if (sourceDT != null)
                {
                    for (int i = 0; i < sourceDT.Rows.Count; i++)
                    {
                        var row = sourceDT.Rows[i];
                        Drawing drawing = new Drawing()
                        {
                            NameBase = row[sourceDT.Columns["NameBase"]].ToString(),
                            DrwFilename = row[sourceDT.Columns["DrwFilename"]].ToString()
                        };
                        sourceList.Add(drawing);
                    }
                    DG_Source.ItemsSource = sourceList;
                }
            }
        }
        private void SelectDestinationReport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Xml файлы (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                TB_DestinationReport.Text = openFileDialog.FileName;
                DirectoryInfo DestDrawDI = Directory.GetParent(openFileDialog.FileName);
                DestDrawDI = Directory.GetParent(DestDrawDI.FullName);
                TB_DestinationDrawings.Text = Path.Combine(DestDrawDI.FullName, "drawings");

                DG_Dest.ItemsSource = null;
                DataSet DestDS = Functions.ReportDataSet(openFileDialog.FileName);

                DataTable destDT = DestDS.Tables["Drawing"];
                destList = new List<Drawing>();
                if (destDT != null)
                {
                    for (int i = 0; i < destDT.Rows.Count; i++)
                    {
                        var row = destDT.Rows[i];
                        Drawing drawing = new Drawing()
                        {
                            NameBase = row[destDT.Columns["NameBase"]].ToString(),
                            DrwFilename = row[destDT.Columns["DrwFilename"]].ToString()
                        };
                        destList.Add(drawing);
                    }
                    DG_Dest.ItemsSource = destList;
                }
            }
        }

        private void SelectSourceDrawings_Click(object sender, RoutedEventArgs e)
        {
            WF.FolderBrowserDialog openDirectory = new WF.FolderBrowserDialog();
            openDirectory.ShowDialog();
            TB_SourceDrawings.Text = openDirectory.SelectedPath;
        }

        private void SelectDestinationDrawings_Click(object sender, RoutedEventArgs e)
        {
            WF.FolderBrowserDialog openDirectory = new WF.FolderBrowserDialog();
            openDirectory.ShowDialog();
            TB_DestinationDrawings.Text = openDirectory.SelectedPath;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < sourceList.Count; i++)
                {
                    Drawing drawing = sourceList[i];
                    File.Copy(Path.Combine(TB_SourceDrawings.Text, drawing.DrwFilename),
                        Path.Combine(TB_DestinationDrawings.Text, drawing.DrwFilename), true);
                    string destMetadataFile = Path.Combine(TB_DestinationDrawings.Text, drawing.DrwFilename + ".metadata");
                    File.Copy(Path.Combine(TB_SourceDrawings.Text, drawing.DrwFilename + ".metadata"), destMetadataFile, true);

                    // Берём метаданные из чертежа в целевой модели
                    FileInfo metaDestFI = new FileInfo(Path.Combine(TB_DestinationDrawings.Text, destList[i].DrwFilename));
                    string metaDestFile = metaDestFI.FullName + ".metadata";
                    var lines = File.ReadLines(metaDestFile).ToList();
                    string DrawingGuid = lines[lines.IndexOf("<DrawingGuid>") + 1];
                    string MainObjectGuid = lines[lines.IndexOf("<MainObjectGuid>") + 1];
                    string Mark = lines[lines.IndexOf("<Mark>") + 1];

                    // Получаем метаданные в скопированном файле
                    var destLines = File.ReadLines(destMetadataFile).ToList();
                    string destDrawingGuid = destLines[destLines.IndexOf("<DrawingGuid>") + 1];
                    string destMainObjectGuid = destLines[destLines.IndexOf("<MainObjectGuid>") + 1];
                    string destMark = destLines[destLines.IndexOf("<Mark>") + 1];

                    // Заменяем данные в скопированном файле метаданных
                    string text = File.ReadAllText(destMetadataFile);
                    text = text.Replace(destDrawingGuid, DrawingGuid);
                    text = text.Replace(destMainObjectGuid, MainObjectGuid);
                    text = text.Replace(destMark, Mark);
                    File.WriteAllText(destMetadataFile, text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
