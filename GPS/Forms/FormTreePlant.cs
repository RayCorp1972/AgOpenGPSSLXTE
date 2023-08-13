using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Text;

namespace AgOpenGPS
{
    public partial class FormTreePlant : Form
    {
        private readonly FormGPS mf = null;
        private double lastDist;
        private bool wasRed, isRunning;
        private int trees;
        
        public FormTreePlant(Form callingForm)
        {
            mf = callingForm as FormGPS;

            //winform initialization
            InitializeComponent();
            isRunning = mf.Tree.isPlanting;
            nudRadius.Value = (decimal)mf.Tree.treeRadi;

            //this.Text = gStr.gsTreePlantControl;

            ////Label
            //label12.Text = gStr.gsSpacing;
            //label1.Text = gStr.gsStep;
            //label3.Text = gStr.gsTrees;

            ////Button
            //btnZeroDistance.Text = gStr.gsBegin;
            //btnStop.Text = gStr.gsDone;

            //nudTreeSpacing.Controls[0].Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

           
            //if (mf.manualBtnState != AgOpenGPS.FormGPS.btnStates.Off)
            //{
            //    mf.btnManualOffOn.PerformClick();
            //}
            //Properties.Settings.Default.setDistance_TreeSpacing = mf.vehicle.treeSpacing;
            //Properties.Settings.Default.Save();
            //mf.vehicle.treeSpacing = 0;
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            isRunning = mf.Tree.isPlanting;
            
            if (isRunning)
            {
                lblDistanceTree.Text = ((UInt16)mf.treeSpacingCounter * 0.01).ToString();
                if (lastDist > mf.treeSpacingCounter)
                {
                    //lblSpacing.Text = mf.vehicle.treeSpacing.ToString();
                    wasRed = !wasRed;                    
                    if (wasRed) btnZeroDistance.BackColor = Color.DarkSeaGreen;
                    
                    else btnZeroDistance.BackColor = Color.LightGreen;
                   
                }
                btnZeroDistance.Text = "Stop";
                
            }
            else
            {
                btnZeroDistance.Text = "Start";
               
            }
            if(mf.Tree.isSound)
            {
                button1.BackColor = Color.DarkGreen;
                this.BackColor = Color.Black;
            }
            else
            {
                button1.BackColor = Color.Orange;
                
            }
            if (mf.treeTrigger == 1) pictureBox1.Image = Properties.Resources.SwitchOn;
            if (mf.treeTrigger == 1) this.BackColor = Color.Red;
            else pictureBox1.Image = Properties.Resources.SwitchOff;
            lblStepDistance.Text = (mf.distanceCurrentStepFix * 100).ToString("N1");
            lblSpeed.Text = mf.pn.speed.ToString("N1");
            lblTrees.Text = mf.Tree.ptList.Count.ToString();
            lastDist = mf.treeSpacingCounter;
            
            this.lblTrees.TextChanged += new System.EventHandler(this.lblTrees_TextChanged);



        }
        

        private void lblTrees_TextChanged(object sender, EventArgs e)
        {
           this.BackColor = Color.Red;
        }
        private void btnZeroDistance_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                lastDist = 0;
                mf.treeSpacingCounter = 0;
                
                mf.distanceCurrentStepFix = 0;
                lblDistanceTree.Text = ((UInt16)mf.treeSpacingCounter).ToString();
                lblStepDistance.Text = (mf.distanceCurrentStepFix * 100).ToString("N1");
                btnZeroDistance.BackColor = Color.OrangeRed;
                
                mf.Tree.isPlanting = false;
                // mf.vehicle.treeSpacing = 0;
            }
            else
            {
                lastDist = 0;
                trees = 0;
                mf.treeSpacingCounter = 0;
               //if (mf.manualBtnState == AgOpenGPS.FormGPS.btnStates.Off)
                //{
                //    mf.btnManualOffOn.PerformClick();
                //}

                mf.distanceCurrentStepFix = 0;
                lblDistanceTree.Text = ((UInt16)mf.treeSpacingCounter).ToString();
                lblStepDistance.Text = (mf.distanceCurrentStepFix * 100).ToString("N1");
                btnZeroDistance.BackColor = Color.LightGreen;
                
                mf.Tree.isPlanting = true;

                //mf.vehicle.treeSpacing = Properties.Settings.Default.setDistance_TreeSpacing;
            }


            isRunning = mf.Tree.isPlanting;
        }

        private void NudTreeSpacing_ValueChanged(object sender, EventArgs e)
        {
            mf.vehicle.treeSpacing = (double)nudTreeSpacing.Value;
        }

        private void NudTreeSpacing_Enter(object sender, EventArgs e)
        {
           // mf.KeypadToNUD((NumericUpDown)sender);
            btnStop.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mf.Tree.isSound = !mf.Tree.isSound;
            button1.BackColor = Color.Green;
        }

        private void nudRadius_ValueChanged(object sender, EventArgs e)
        {
            mf.Tree.treeRadi = (int)nudRadius.Value;

        }

        private void button2_Click(object sender, EventArgs e)
        
            {
                mf.Tree.ptList?.Clear();
            }
        

        private void FormTreePlant_Load(object sender, EventArgs e)
        {
            // Tree points ----------------------------------------------------------------------------
            //start to read the file
            //string line;
            //string fileAndDirectory = "";
            //fileAndDirectory = mf.fieldsDirectory + mf.currentFieldDirectory + "\\Tree.txt";
            //if (!File.Exists(fileAndDirectory))
            //{
            //    var form = new FormTimedMessage(4000, "Missing Tree File", "But Field is Loaded");
            //    form.Show();
            //    //return;
            //}

            ///*
            //    May-14-17  -->  7:42:47 PM
            //    Points in Patch followed by easting, heading, northing, altitude
            //    $ContourDir
            //    cdert_May14
            //    $Offsets
            //    533631,5927279,12
            //    19
            //    2.866,2.575,-4.07,0             
            // */
            //else
            //{
                
            //    using (StreamReader reader = new StreamReader(fileAndDirectory))
            //    {
            //        try
            //        {
            //            //read the lines and skip them
            //            line = reader.ReadLine();

            //            line = reader.ReadLine();
            //            line = reader.ReadLine();
            //            line = reader.ReadLine();
            //            line = reader.ReadLine();
            //            line = reader.ReadLine();
            //            line = reader.ReadLine();


            //            while (!reader.EndOfStream)
            //            {
            //                //read how many vertices in the file

            //                int verts = int.Parse(line);



            //                for (int v = 0; v < verts; v++)
            //                {
            //                    line = reader.ReadLine();

            //                    string[] words = line.Split(',');

            //                    double east = double.Parse(words[0], CultureInfo.InvariantCulture);
            //                    double nort = double.Parse(words[2], CultureInfo.InvariantCulture);

            //                   mf.Tree.AddPoint(east, nort);
            //                    mf.Tree.ptList[mf.Tree.ptList.Count - 1].index = mf.Tree.ptList.Count - 1;
            //                    mf.Tree.ptList[mf.Tree.ptList.Count - 1].comment = words[5];
            //                    if (words.Length > 6)
            //                    {
            //                        mf.Tree.ptList[mf.Tree.ptList.Count - 1].datePlanted = words[6];
            //                        if (words[6] != "")
            //                        {
            //                            mf.Tree.ptList[mf.Tree.ptList.Count - 1].isPlanted = true;
            //                        }
            //                    }
            //                    mf.Tree.ptList[mf.Tree.ptList.Count - 1].heading = double.Parse(words[1], CultureInfo.InvariantCulture);
            //                    //vec3 treept = new vec3(Tree.ptList[Tree.ptList.Count - 1].easting, Tree.ptList[Tree.ptList.Count - 1].northing, Tree.ptList[Tree.ptList.Count - 1].heading);
            //                    //AddBoundaryAroundTree(treept, 4);


            //                }
            //            }
            //            //CalculateMinMax();
            //            //turn.BuildTurnLines();
            //            //gf.BuildGeoFenceLines();
            //            //mazeGrid.BuildMazeGridArray();

            //        }
            //        catch (Exception)
            //        {
            //            mf.WriteErrorLog("Loading Tree file" + e.ToString());

            //            var form = new FormTimedMessage(4000, "Tree File is Corrupt", "But Field is Loaded");
            //            form.Show();

            //        }
             


            

            nudTreeSpacing.Value = (decimal)mf.vehicle.treeSpacing;
            lastDist = 0;
            mf.treeSpacingCounter = 0;
            trees = 0;
            isRunning = false;
            btnZeroDistance.Text = "Start";
            btnZeroDistance.BackColor = Color.OrangeRed;
            
        }
    }
}
