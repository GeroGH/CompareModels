using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tekla.Structures.Model;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace CompareTwoModels
{
    public partial class CompareTwoModels : Form
    {
        private DataTable DataTable;
        private readonly List<Beam> BeamsList = new List<Beam>();

        public CompareTwoModels()
        {
            this.InitializeComponent();

            this.DataTable = new DataTable();
            this.DataTable.TableName = "ModelData";
            this.DataTable.Columns.Add("COID", typeof(string));
            this.DataTable.Columns.Add("Profile", typeof(string));
            this.DataTable.Columns.Add("AssemblyNumber", typeof(string));
            this.DataTable.Columns.Add("StartX", typeof(string));
            this.DataTable.Columns.Add("StartY", typeof(string));
            this.DataTable.Columns.Add("StartZ", typeof(string));
            this.DataTable.Columns.Add("EndX", typeof(string));
            this.DataTable.Columns.Add("EndY", typeof(string));
            this.DataTable.Columns.Add("EndZ", typeof(string));
        }

        private void Export_Click(object sender, System.EventArgs e)
        {
            this.CollectBeamsFromTheModel();

            foreach (var beam in this.BeamsList)
            {
                ExtractDataFromBeam(beam, out var Profile, out var AssemblyNumber, out var StartX, out var StartY, out var StartZ, out var EndX, out var EndY, out var EndZ, out var COID);
                this.DataTable.Rows.Add(COID, Profile, AssemblyNumber, StartX, StartY, StartZ, EndX, EndY, EndZ);
            }

            this.DataTable = this.DataTable.DefaultView.ToTable(true);
            this.DataTable.DefaultView.Sort = "COID";

            this.DataGrid.DataSource = this.DataTable;
            this.DataGrid.RowHeadersVisible = false;
            this.DataGrid.AllowUserToResizeRows = true;
            this.DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.ReadOnly = true;
            this.DataGrid.ColumnHeadersVisible = true;

            var ds = new DataSet();
            ds.DataSetName = "DataSet";
            ds.Tables.Add(this.DataTable);

            ds.WriteXml(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataFile.xml"));
        }

        private void Import_Click(object sender, EventArgs e)
        {
            var ds = new DataSet();
            ds.ReadXml(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataFile.xml"));

            this.DataTable = ds.Tables["ModelData"];
            this.DataTable = this.DataTable.DefaultView.ToTable(true);
            this.DataTable.DefaultView.Sort = "COID";
            this.DataGrid.DataSource = this.DataTable;
            this.DataGrid.RowHeadersVisible = false;
            this.DataGrid.AllowUserToResizeRows = true;
            this.DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.ReadOnly = true;
            this.DataGrid.ColumnHeadersVisible = true;
            this.DataGrid.DefaultCellStyle.BackColor = Color.Beige;
        }

        private void Compare_Click(object sender, EventArgs e)
        {
            this.CollectBeamsFromTheModel();

            foreach (var beam in this.BeamsList)
            {
                ExtractDataFromBeam(beam, out var beamProfile, out var beamAssemblyNumber, out var beamStartX, out var beamStartY, out var beamStartZ, out var beamEndX, out var beamEndY, out var beamEndZ, out var beamCOID);

                var rows = this.DataGrid.Rows;

                foreach (DataGridViewRow row in rows)
                {
                    var rowCOID = row.Cells[0].Value.ToString();
                    var rowProfile = row.Cells[1].Value.ToString();
                    var rowAssemblyNumber = row.Cells[2].Value.ToString();
                    var rowStartX = row.Cells[3].Value.ToString();
                    var rowStartY = row.Cells[4].Value.ToString();
                    var rowStartZ = row.Cells[5].Value.ToString();
                    var rowEndX = row.Cells[6].Value.ToString();
                    var rowEndY = row.Cells[7].Value.ToString();
                    var rowEndZ = row.Cells[8].Value.ToString();

                    if (rowCOID.Equals(beamCOID))
                    {
                        row.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    }
                }
            }

            this.CollectBeamsFromTheModel();
        }

        private void CollectBeamsFromTheModel()
        {
            this.BeamsList.Clear();

            var mos = new ModelObjectSelector();
            var moe = mos.GetSelectedObjects();

            while (moe.MoveNext())
            {
                var beam = moe.Current as Beam;
                if (beam == null)
                {
                    continue;
                }

                var Type = string.Empty;
                beam.GetReportProperty("PROFILE_TYPE", ref Type);
                if (Type == "B")
                {
                    continue;
                }

                this.BeamsList.Add(beam);
            }
        }

        private static void ExtractDataFromBeam(Beam beam, out string Profile, out string AssemblyNumber, out double StartX, out double StartY, out double StartZ, out double EndX, out double EndY, out double EndZ, out string COID)
        {
            Profile = beam.Profile.ProfileString;

            AssemblyNumber = string.Empty;
            beam.GetReportProperty("ASSEMBLY_POS", ref AssemblyNumber);

            StartX = Math.Round(beam.StartPoint.X);
            StartY = Math.Round(beam.StartPoint.Y);
            StartZ = Math.Round(beam.StartPoint.Z);

            EndX = Math.Round(beam.EndPoint.X);
            EndY = Math.Round(beam.EndPoint.Y);
            EndZ = Math.Round(beam.EndPoint.Z);

            COID = $"{Profile},{AssemblyNumber},{StartX},{StartY},{StartZ},{EndX},{EndY},{EndZ}";
        }
    }
}
