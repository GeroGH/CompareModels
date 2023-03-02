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
            var delta = 50d;

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
                    var rowStartX = Convert.ToDouble(row.Cells[3].Value.ToString());
                    var rowStartY = Convert.ToDouble(row.Cells[4].Value.ToString());
                    var rowStartZ = Convert.ToDouble(row.Cells[5].Value.ToString());
                    var rowEndX = Convert.ToDouble(row.Cells[6].Value.ToString());
                    var rowEndY = Convert.ToDouble(row.Cells[7].Value.ToString());
                    var rowEndZ = Convert.ToDouble(row.Cells[8].Value.ToString());

                    if (beamCOID.Equals(rowCOID))
                    {
                        row.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                        continue;
                    }

                    var startCoorExact = (beamStartX == rowStartX)
                                       & (beamStartY == rowStartY)
                                       & (beamStartZ == rowStartZ);

                    var endCoorExact = (beamEndX == rowEndX)
                                     & (beamEndY == rowEndY)
                                     & (beamEndZ == rowEndZ);

                    var coorExact = startCoorExact & endCoorExact;

                    if (coorExact)
                    {
                        if (beamProfile != rowProfile)
                        {
                            row.Cells[1].Value = beamProfile;
                            row.Cells[1].Style.BackColor = Color.DarkCyan;
                        }

                        if (beamAssemblyNumber != rowAssemblyNumber)
                        {
                            row.Cells[2].Value = beamAssemblyNumber;
                            row.Cells[2].Style.BackColor = Color.DarkMagenta;
                        }
                        continue;
                    }

                    var startCoorClose = (rowStartX - delta < beamStartX && beamStartX < rowStartX + delta)
                                       & (rowStartY - delta < beamStartY && beamStartY < rowStartY + delta)
                                       & (rowStartZ - delta < beamStartZ && beamStartZ < rowStartZ + delta);

                    var endCoorClose = (rowEndX - delta < beamEndX && beamEndX < rowEndX + delta)
                                     & (rowEndY - delta < beamEndY && beamEndY < rowEndY + delta)
                                     & (rowEndZ - delta < beamEndZ && beamEndZ < rowEndZ + delta);

                    var coorClose = startCoorClose & endCoorClose;

                    if (coorClose)
                    {
                        if (beamStartX != rowStartX)
                        {
                            row.Cells[3].Value = beamStartX;
                            row.Cells[3].Style.BackColor = Color.YellowGreen;
                        }
                        if (beamStartY != rowStartY)
                        {
                            row.Cells[4].Value = beamStartY;
                            row.Cells[4].Style.BackColor = Color.YellowGreen;
                        }
                        if (beamStartZ != rowStartZ)
                        {
                            row.Cells[5].Value = beamStartZ;
                            row.Cells[5].Style.BackColor = Color.YellowGreen;
                        }

                        if (beamEndX != rowEndX)
                        {
                            row.Cells[6].Value = beamEndX;
                            row.Cells[6].Style.BackColor = Color.YellowGreen;
                        }
                        if (beamEndY != rowEndY)
                        {
                            row.Cells[7].Value = beamEndY;
                            row.Cells[7].Style.BackColor = Color.YellowGreen;
                        }
                        if (beamEndZ != rowEndZ)
                        {
                            row.Cells[8].Value = beamEndZ;
                            row.Cells[8].Style.BackColor = Color.YellowGreen;
                        }

                        if (beamProfile != rowProfile)
                        {
                            row.Cells[1].Value = beamProfile;
                            row.Cells[1].Style.BackColor = Color.DarkCyan;
                        }

                        if (beamAssemblyNumber != rowAssemblyNumber)
                        {
                            row.Cells[2].Value = beamAssemblyNumber;
                            row.Cells[2].Style.BackColor = Color.DarkMagenta;
                        }

                        continue;
                    }
                }
            }
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