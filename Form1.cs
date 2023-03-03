using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Model;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace CompareTwoModels
{
    public partial class CompareTwoModels : Form
    {
        private int Delta = 100;
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

            var up = new UserPropertyItem
            {
                Name = "Status",
                Level = UserPropertyLevelEnum.LEVEL_MODEL,
                FieldType = UserPropertyFieldTypeEnum.FIELDTYPE_TEXT,
                Type = PropertyTypeEnum.TYPE_STRING,
                Visibility = UserPropertyVisibilityEnum.VISIBILITY_NORMAL,
                Unique = true,
                AffectsNumbering = false
            };
            up.Insert();
            up.SetLabel("Status");
            up.AddToObjectType(CatalogObjectTypeEnum.STEEL_BEAM);
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

            this.DataGridView.DataSource = this.DataTable;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.AllowUserToResizeRows = true;
            this.DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.ReadOnly = true;
            this.DataGridView.ColumnHeadersVisible = true;
            this.DataGridView.DefaultCellStyle.BackColor = Color.Yellow;

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
            this.DataGridView.DataSource = this.DataTable;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.AllowUserToResizeRows = true;
            this.DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.ReadOnly = true;
            this.DataGridView.ColumnHeadersVisible = true;
            this.DataGridView.DefaultCellStyle.BackColor = Color.White;
        }

        private void Compare_Click(object sender, EventArgs e)
        {
            foreach (var beam in this.BeamsList)
            {
                beam.SetUserProperty("Status", "");
            }

            if (!this.DataGridView.Columns.Contains("Change"))
            {
                this.DataGridView.Columns.Add("Change", "Change");
            }

            this.CollectBeamsFromTheModel();

            var rows = this.DataGridView.Rows;

            foreach (var beam in this.BeamsList)
            {
                var status = string.Empty;

                ExtractDataFromBeam(beam, out var beamProfile, out var beamAssemblyNumber, out var beamStartX, out var beamStartY, out var beamStartZ, out var beamEndX, out var beamEndY, out var beamEndZ, out var beamCOID);

                foreach (DataGridViewRow row in rows)
                {
                    var rowCOID = row.Cells["COID"].Value.ToString();
                    var rowProfile = row.Cells["Profile"].Value.ToString();
                    var rowAssemblyNumber = row.Cells["AssemblyNumber"].Value.ToString();
                    var rowStartX = Convert.ToDouble(row.Cells["StartX"].Value.ToString());
                    var rowStartY = Convert.ToDouble(row.Cells["StartY"].Value.ToString());
                    var rowStartZ = Convert.ToDouble(row.Cells["StartZ"].Value.ToString());
                    var rowEndX = Convert.ToDouble(row.Cells["EndX"].Value.ToString());
                    var rowEndY = Convert.ToDouble(row.Cells["EndY"].Value.ToString());
                    var rowEndZ = Convert.ToDouble(row.Cells["EndZ"].Value.ToString());

                    if (beamCOID.Equals(rowCOID))
                    {
                        row.DefaultCellStyle.BackColor = Color.LimeGreen;
                        status += "No Change ";
                        row.Cells["Change"].Value = status;
                        beam.SetUserProperty("Status", status);
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
                            status += "Profile ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["Profile"].Value = beamProfile;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["Profile"].Style.BackColor = Color.Magenta;
                        }

                        if (beamAssemblyNumber != rowAssemblyNumber)
                        {
                            status += "Assembly ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["AssemblyNumber"].Value = beamAssemblyNumber;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["AssemblyNumber"].Style.BackColor = Color.Magenta;
                        }
                        continue;
                    }

                    var startCoorClose = (rowStartX - this.Delta < beamStartX && beamStartX < rowStartX + this.Delta)
                                       & (rowStartY - this.Delta < beamStartY && beamStartY < rowStartY + this.Delta)
                                       & (rowStartZ - this.Delta < beamStartZ && beamStartZ < rowStartZ + this.Delta);

                    var endCoorClose = (rowEndX - this.Delta < beamEndX && beamEndX < rowEndX + this.Delta)
                                     & (rowEndY - this.Delta < beamEndY && beamEndY < rowEndY + this.Delta)
                                     & (rowEndZ - this.Delta < beamEndZ && beamEndZ < rowEndZ + this.Delta);

                    var coorClose = startCoorClose & endCoorClose;

                    if (coorClose)
                    {
                        if (beamProfile != rowProfile)
                        {
                            status += "Profile ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["Profile"].Value = beamProfile;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["Profile"].Style.BackColor = Color.Magenta;
                        }

                        if (beamAssemblyNumber != rowAssemblyNumber)
                        {
                            status += "Assembly ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["AssemblyNumber"].Value = beamAssemblyNumber;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["AssemblyNumber"].Style.BackColor = Color.Magenta;
                        }

                        if (beamStartX != rowStartX)
                        {
                            status += "StartX ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["StartX"].Value = beamStartX;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["StartX"].Style.BackColor = Color.Magenta;
                        }
                        if (beamStartY != rowStartY)
                        {
                            status += "StartY ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["StartY"].Value = beamStartY;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["StartY"].Style.BackColor = Color.Magenta;
                        }
                        if (beamStartZ != rowStartZ)
                        {
                            status += "StartZ ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["StartZ"].Value = beamStartZ;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["StartZ"].Style.BackColor = Color.Magenta;
                        }

                        if (beamEndX != rowEndX)
                        {
                            status += "EndX ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["EndX"].Value = beamEndX;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["EndX"].Style.BackColor = Color.Magenta;
                        }
                        if (beamEndY != rowEndY)
                        {
                            status += "EndY ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["EndY"].Value = beamEndY;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["EndY"].Style.BackColor = Color.Magenta;
                        }
                        if (beamEndZ != rowEndZ)
                        {
                            status += "EndZ ";
                            row.Cells["Change"].Value = status;
                            beam.SetUserProperty("Status", status);

                            row.Cells["EndZ"].Value = beamEndZ;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.Cells["EndZ"].Style.BackColor = Color.Magenta;
                        }

                        continue;
                    }
                }
            }

            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells["Change"].Value is null)
                {
                    row.Cells["Change"].Value += "Beam Deleted";
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }

            foreach (var beam in this.BeamsList)
            {
                var status = string.Empty;
                beam.GetReportProperty("Status", ref status);
                if (status == "")
                {
                    ExtractDataFromBeam(beam, out var Profile, out var AssemblyNumber, out var StartX, out var StartY, out var StartZ, out var EndX, out var EndY, out var EndZ, out var COID);
                    this.DataTable.Rows.Add(COID, Profile, AssemblyNumber, StartX, StartY, StartZ, EndX, EndY, EndZ);
                }
            }

            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells["Change"].Value is null)
                {
                    row.Cells["Change"].Value += "Beam Added";
                    row.DefaultCellStyle.BackColor = Color.DodgerBlue;
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

        private void Delta_TextChanged(object sender, EventArgs e)
        {
            this.DeltaBox.Text = this.DeltaBox.Text;
            this.DeltaBox.ForeColor = Color.Firebrick;
            this.Delta = int.Parse(this.DeltaBox.Text);
        }
    }
}