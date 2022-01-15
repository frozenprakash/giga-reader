using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

//Form Created by Chandraprakash [2017-06-19]
namespace GigaReader
{
    //Created by Chandraprakash [2017-07-19 | 10:11 PM]
    enum ExecutionType : byte
    {
        pbLineCount,
        pbFileCount
    }

    //Created by Chandraprakash [2017-08-30 | 06:15 PM]
    enum ReadType : byte
    {
        First1000Lines,
        Last1000Lines,
        CustomLines
    }

    public partial class frmCountandSplit : Form
    {

        #region Class Variables

        StreamReader sr;
        StreamWriter sw;

        int StartRange;

        string strFirst1000Lines = "",
                strLast1000Lines = "",
                strCustomLines = "";

        string strRTB1 = ""; //RTB Text with Header
        string strRTB1WoH = ""; //RTB Text without Header
        string strRTB1WoT = ""; //RTB Text without Trailer

        #endregion

        public frmCountandSplit()
        {
            InitializeComponent();
        }

        //Created by Chandraprakash [2017-08-23 | 02:34 PM]
        enum RTBProcessType : byte
        {
            ArrayWithoutFlag, //Normal Array, Normal count
            ArrayWithFlag //Array[0] - True/False, Count = +1
        }

        void clearRadioButtonChecks()
        {
            rdoReadFirst1000Lines.Checked = false;
            rdoReadLast1000Lines.Checked = false;
            rdoReadCustomLines.Checked = false;
        }

        async private void CountandSplit_Load(object sender, EventArgs e)
        {
            StartRange = 1000;

            ss1Pb1.Width = ss1.Width - (ss1Lbl1.Width + ss1Lbl2.Width);

            lblTotalLineCountValue.Text = "";
            ss2Lbl1.Text = "";

            chkModifyData.Checked = false;

            //Added by Chandraprakash [2017-08-11 | 04:24 PM]
            //Will enable or disable UI corresponding to checked status of radiobutton
            //rdoReadFirst1000Lines.Checked = false;
            //rdoReadLast1000Lines.Checked = false;

            //rdoReadCustomLines.Checked = true;
            lblFromRCL.Enabled = rdoReadCustomLines.Checked;
            lblToRCL.Enabled = rdoReadCustomLines.Checked;
            txtFromRCL.Enabled = rdoReadCustomLines.Checked;
            txtToRCL.Enabled = rdoReadCustomLines.Checked;

            txtFromRCL.Text = "1";
            txtToRCL.Text = "15";
        }

        private void btn_SplitClaims_Click(object sender, EventArgs e)
        {
            //FN_SplitClaims();
        }

        //Count Claims
        private void btnSourceFile_Click(object sender, EventArgs e)
        {
            FN_SourceFile();
        }

        //Added by Chandraprakash [2017-06-21]
        //Modified by Chandraprkash [2017-06-21]
        //Select Source File and Count Claims
        async void FN_SourceFile()
        {
            bool isModify = chkModifyData.Checked;
            try
            {
                chkModifyData.Checked = false;
                chkModifyData.Enabled = false;

                lblTotalLineCountValue.Text = "";
                ss2Lbl1.Text = "";
                bool isPassed = false;

                rtb1.Clear();

                if (ofdSourceFile.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(ofdSourceFile.FileName))
                    {
                        txtSourceFile.Text = ofdSourceFile.FileName;
                        ButtonsStatusDuringProcess(false);
                    }
                    else
                    {
                        MessageBox.Show("Selected file cannot be found !");
                        return;
                    }

                    ss2Lbl1.Text = "Total Line Count Calculation in Progress...";
                    isPassed = await Task.Run(() => CountClaims(txtSourceFile.Text.Trim(),
                                                                ss1,
                                                                ss2,
                                                                ss1Pb1,
                                                                ss2Lbl1,
                                                                StartRange)
                                            );
                    if (isPassed == false)
                    {
                        //MessageBox.Show("Error occurred while counting claims",
                        //                "Error",
                        //                MessageBoxButtons.OK,
                        //                MessageBoxIcon.Exclamation);
                        return;
                    }

                    //txtDestinationFolder.Text = Path.GetDirectoryName(ofdSourceFile.FileName);
                    txtDestinationFile.Text = Path.GetDirectoryName(ofdSourceFile.FileName) + "\\" +
                                                Path.GetFileNameWithoutExtension(ofdSourceFile.FileName) + "_Output" +
                                                Path.GetExtension(ofdSourceFile.FileName);

                    ss2Lbl1.Text = "Completed Calculating Total Line Count";
                }
                else
                {
                    // If user pressed cancel in Open File Dialog do nothing and exit
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ButtonsStatusDuringProcess(true);

                rdoReadCustomLines.Checked = true;

                chkModifyData.Enabled = true;
                chkModifyData.Checked = isModify;

                rtb1.Focus();
            }
        }

        //Created by Chandraprakash [2017-06-07]
        //Modified by Chandraprakash [2017-06-20]
        bool CountClaims(string strFileName,

                            StatusStrip ss1,
                            StatusStrip ss2,
                            ToolStripProgressBar ss1Pb1,
                            ToolStripLabel ss2Lbl1,

                            int StartRange)
        {
            bool isPassed = false;

            try
            {
                //Validations
                if (txtSourceFile.Text.Trim() == "")
                {
                    MessageBox.Show("Please Select a Source File !");
                    return false;
                }

                Splitter objSplitter = new Splitter();
                returnTotalLines objreturnTotalLines = (returnTotalLines)objSplitter.TotalLines(txtSourceFile.Text.Trim(),
                                                                                                StartRange,

                                                                                                //Optional Parameters
                                                                                                ss1,
                                                                                                ss2,
                                                                                                ss1Pb1,
                                                                                                ss2Lbl1,

                                                                                                rdoReadCustomLines.Checked,
                                                                                                txtFromRCL.Text.Replace(",", ""),
                                                                                                txtToRCL.Text.Replace(",", "")
                                                                                                );

                //Execution will enter inside this block and break if validation fails inside TotalLines Function above
                if (objreturnTotalLines == null)
                {
                    return false;
                }

                //capturing the return values into variables
                int lineCount = objreturnTotalLines.LineCount;
                strFirst1000Lines = string.Join("\n", objreturnTotalLines.arrFirst1000Lines);
                strLast1000Lines = string.Join("\n", objreturnTotalLines.arrLast1000Lines);
                strCustomLines = string.Join("\n", objreturnTotalLines.arrCustomLines);

                if (rdoReadFirst1000Lines.Checked == true)
                {
                    rtb1.Invoke(new Action(() =>
                        rtb1.Text = strFirst1000Lines
                    ));
                }

                if (rdoReadLast1000Lines.Checked == true)
                {
                    rtb1.Invoke(new Action(() =>
                        rtb1.Text = strLast1000Lines
                    ));
                }

                if (rdoReadCustomLines.Checked == true)
                {
                    rtb1.Invoke(new Action(() =>
                        rtb1.Text = strCustomLines
                    ));
                }

                //Loading RTB variables from Array
                //RTBProcess(ArrFirst1000Lines, (byte)RTBProcessType.ArrayWithoutFlag);
                //First1000Lines = strRTB1;

                //RTBProcess(arrLast1000Lines, (byte)RTBProcessType.ArrayWithoutFlag);
                //strLast1000Lines = strRTB1;

                if (lineCount == -1)
                {
                    MessageBox.Show("Error while reading the Source File !!");
                    return false;
                }
                else if (lineCount != -1)
                {
                    lblTotalClaimCount.Invoke(new Action(() =>
                        lblTotalLineCountValue.Text = lineCount.ToString("N0", CultureInfo.InvariantCulture)
                    ));
                }
                isPassed = true;
            }
            catch (InvalidOperationException ex)
            {
                //While Counting Claims, 
                //If the Form is closed while the BeginInvoke or Invoke are running,
                //execution will come here and exit without throwing any error.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rtb1.Invoke(new Action(() =>
                    rtb1.Focus()
                ));
            }
            return isPassed;
        }

        public string CreateDestinationDirectory(string fullFileName)
        {
            DateTime dt = DateTime.Now;
            string str_dt = "[" +
                            dt.ToString("yyyy") + "-" +
                            dt.ToString("MM") + "-" +
                            dt.ToString("dd") + "] [" +
                            dt.ToString("hh") + "h " +
                            dt.ToString("mm") + "m " +
                            dt.ToString("ss") + "s " +
                            dt.ToString("tt") +
                            "]";

            string dir = Path.Combine(Path.GetDirectoryName(fullFileName),
                                        Path.GetFileNameWithoutExtension(fullFileName) + " " +
                                        str_dt
                                    );

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return dir;
        }

        void ButtonsStatusDuringProcess(bool b)
        {
            btnSourceFile.Enabled = b;
            btnSaveFile.Enabled = b;

            txtSourceFile.Enabled = b;
            txtDestinationFile.Enabled = b;

            rdoReadFirst1000Lines.Enabled = b;
            rdoReadLast1000Lines.Enabled = b;
            rdoReadCustomLines.Enabled = b;

            btnRemoveHeader.Enabled = b;
            btnRemoveTrailer.Enabled = b;

            btnClearText.Enabled = b;

            lblDestinationFile.Enabled = b;
            lblTotalClaimCount.Enabled = b;

            lblFromRCL.Enabled = b;
            lblToRCL.Enabled = b;

            txtFromRCL.Enabled = b;
            txtToRCL.Enabled = b;
        }

        //Created by Chandraprakash [2017-09-07 | 06:34 PM]
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Application is developed by Chandraprakash." + "\n\n" +
                                "Email" + "\t" + ": info@frozenincorporation.com" + "\n" +
                                "Mobile" + "\t" + ": +91 9444 7770 37" + "\n"
                            );
        }

        //Created by Chandraprakash [2017-09-07 | 06:34 PM]
        private void mnuApplication_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\"GigaReader\"");
        }

        //Added by Chandraprakash [2017-08-11 | 04:23 PM]
        private void rdoReadCustomLines_CheckedChanged(object sender, EventArgs e)
        {
            FN_ReadCustomLines();

            rtb1.Invoke(new Action(() =>
                rtb1.Focus()
            ));
        }

        private void rdoReadCustomLines_Click(object sender, EventArgs e)
        {
            FN_ReadCustomLines();

            rtb1.Invoke(new Action(() =>
                rtb1.Focus()
            ));
        }

        async void FN_ReadCustomLines()
        {
            int start = 0,
                end = 0,
                total = 0;
            try
            {
                //if (rdoReadCustomLines.Checked == true)
                //{
                //    btnRemoveHeader.Enabled = false;
                //    btnRemoveTrailer.Enabled = false;
                //}

                //Will enable or disable textbox corresponding to checked status of radiobutton
                lblFromRCL.Enabled = rdoReadCustomLines.Checked;
                lblToRCL.Enabled = rdoReadCustomLines.Checked;
                txtFromRCL.Enabled = rdoReadCustomLines.Checked;
                txtToRCL.Enabled = rdoReadCustomLines.Checked;

                #region Validations - ReadCustomLines

                //Unreachable code - kept for additional validation
                if (rdoReadCustomLines.Checked == false)
                {
                    return;
                }

                if (lblTotalLineCountValue.Text == "")
                {
                    MessageBox.Show("Cannot process while TotalLineCount is empty, Please select 'Source File' button and load input file again",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                //Unreachable code - kept for additional validation
                if (int.TryParse(lblTotalLineCountValue.Text.Replace(",", ""), out total) == false)
                {
                    return;
                }

                if (txtFromRCL.Text == "")
                {
                    MessageBox.Show("Please enter 'From' Value in Textbox as it cannot be empty",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtToRCL.Text == "")
                {
                    MessageBox.Show("Please enter 'To' Value in Textbox as it cannot be empty",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                if (int.TryParse(txtFromRCL.Text.Replace(",", ""), out start) == false)
                {
                    MessageBox.Show("'From' Value should only be a number",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                if (int.TryParse(txtToRCL.Text.Replace(",", ""), out end) == false)
                {
                    MessageBox.Show("'To' Value should only be a number",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                if (start > total)
                {
                    MessageBox.Show("'From' Value cannot be greater than TotalLineCount",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                if (end > total)
                {
                    MessageBox.Show("'To' Value cannot be greater than TotalLineCount",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                if (start < 1)
                {
                    MessageBox.Show("'From' Value cannot be less than 1",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                //if (start == end)
                //{
                //    return;
                //}

                if (end < start)
                {
                    MessageBox.Show("'To' Value cannot be less than 'From' Value",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                #endregion Validations - ReadCustomLines

                strRTB1 = "";
                strRTB1WoH = "";
                strRTB1WoT = "";

                Splitter objSplitter = new Splitter();
                string[] lineArr = await Task.Run(() => objSplitter.ReadLines(txtSourceFile.Text,
                                                                                start,
                                                                                end,

                                                                                ss1,
                                                                                ss2,
                                                                                ss1Pb1,
                                                                                ss2Lbl1)
                                                    );

                //Added by Chandraprakash [2017-09-07 | 07:44 PM]
                await Task.Run(() =>
                {
                    strCustomLines = string.Join("\n", lineArr);

                    ss2.Invoke(new Action(() =>
                        ss2Lbl1.Text = "Loading Lines into RichTextBox..."
                    ));

                    rtb1.Invoke(new Action(() =>
                        rtb1.Text = strCustomLines
                    ));

                    ss2.Invoke(new Action(() =>
                        ss2Lbl1.Text = "Loaded RichTextBox successfully"
                    ));
                }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Clear1()
        {
            rtb1.Clear();

            chkModifyData.Checked = false;
            clearRadioButtonChecks();

            //btnRemoveHeader.Enabled = false;
            //btnRemoveTrailer.Enabled = false;
        }

        private void btnClearText_Click(object sender, EventArgs e)
        {
            Clear1();
        }

        //Created by Chandraprakash [2017-08-23 | 05:24 PM]
        void FN_RemoveHeader()
        {
            try
            {
                bool isRTBReadOnly = rtb1.ReadOnly;

                if (rtb1.Lines.Length == 0)
                {
                    return;
                }

                rtb1.SelectionStart = rtb1.GetFirstCharIndexFromLine(0);
                rtb1.SelectionLength = rtb1.Lines[0].Length + 1;

                rtb1.ReadOnly = false;
                rtb1.SelectedText = "";
                rtb1.ReadOnly = isRTBReadOnly;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rtb1.Focus();
            }
        }

        //Created by Chandraprakash [2017-08-23 | 05:24 PM]
        void FN_RemoveTrailer()
        {
            try
            {
                bool isRTBReadOnly = rtb1.ReadOnly; //Added by Chandraprakash [2017-09-08 | 06:58 PM]

                if (rtb1.Lines.Length == 0)
                {
                    return;
                };

                //rtb1.SelectionStart = rtb1.GetFirstCharIndexFromLine(rtb1.Lines.Length - 1);
                //rtb1.SelectionLength = rtb1.Lines[rtb1.Lines.Length - 1].Length + 1;

                if (rtb1.Lines.Length == 1)
                {
                    rtb1.SelectionStart = 0;
                }
                else if (rtb1.Lines.Length > 1)
                {
                    rtb1.SelectionStart = rtb1.Text.LastIndexOf("\n");
                }

                rtb1.SelectionLength = rtb1.Lines[rtb1.Lines.Length - 1].Length + 1;

                rtb1.ReadOnly = false;
                rtb1.SelectedText = "";
                rtb1.ReadOnly = isRTBReadOnly;

                if (rtb1.Lines.Length > 0)
                {
                    rtb1.SelectionStart = rtb1.GetFirstCharIndexFromLine(rtb1.Lines.Length - 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rtb1.Focus();
            }
        }

        //Created by Chandraprakash [2017-08-23 | 05:25 PM]
        private void btnRemoveHeader_Click(object sender, EventArgs e)
        {
            FN_RemoveHeader();
        }

        //Created by Chandraprakash [2017-08-23 | 05:25 PM]
        private void btnRemoveTrailer_Click(object sender, EventArgs e)
        {
            FN_RemoveTrailer();
        }

        //Created by Chandraprakash [2017-08-11 | 07:06 PM]
        private void rdoReadFirst10Lines_CheckedChanged(object sender, EventArgs e)
        {
            FN_ReadFirst1000Lines();
        }

        //Created by Chandraprakash [2017-08-23 | 04:58 PM]
        private void rdoReadFirst10Lines_Click(object sender, EventArgs e)
        {
            FN_ReadFirst1000Lines();
        }

        //Created by Chandraprakash [2017-08-23 | 05:02 PM]
        void FN_ReadFirst1000Lines()
        {
            try
            {
                strRTB1 = "";
                strRTB1WoH = "";

                if (rdoReadFirst1000Lines.Checked == true)
                {
                    if (txtSourceFile.Text != "" &&
                        lblTotalLineCountValue.Text != "")
                    {
                        rtb1.Text = strFirst1000Lines;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                rtb1.Focus();
            }
        }

        //Created by Chandraprakash [2017-08-11 | 07:06 PM]
        private void rdoReadLast10Lines_CheckedChanged(object sender, EventArgs e)
        {
            FN_ReadLast1000Lines();
        }

        //Created by Chandraprakash [2017-08-23 | 05:01 PM]
        private void rdoReadLast10Lines_Click(object sender, EventArgs e)
        {
            FN_ReadLast1000Lines();
        }

        //Created by Chandraprakash [2017-08-23 | 05:01 PM]
        void FN_ReadLast1000Lines()
        {
            try
            {
                strRTB1 = "";
                strRTB1WoT = "";

                if (rdoReadLast1000Lines.Checked == true)
                {
                    if (txtSourceFile.Text != "" &&
                        lblTotalLineCountValue.Text != "")
                    {
                        rtb1.Text = strLast1000Lines;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rtb1.Focus();
            }
        }

        //Created by Chandraprakash [2017-08-23 | 01:30 PM]
        void RTBProcess(string[] strArr,
                        byte pRTBProcessType)
        {
            strRTB1 = "";
            strRTB1WoH = "";
            strRTB1WoT = "";

            int start = 0,
                end = 0;

            if (pRTBProcessType == (byte)RTBProcessType.ArrayWithFlag)
            {
                start = 1;
                end = strArr.Count() - 1;
            }
            else if (pRTBProcessType == (byte)RTBProcessType.ArrayWithoutFlag)
            {
                start = 0;
                end = strArr.Count() - 1;
            }

            if ((start == 0) && (end == 0))
            {
                strRTB1 = "";
                strRTB1WoH = "";
                strRTB1WoT = "";
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    //RTB Process - Contains all element
                    if (i != end)
                    {
                        strRTB1 += strArr[i] + "\n";
                    }
                    else if (i == end) //Last element will not have trailing new line
                    {
                        strRTB1 += strArr[i];
                    }

                    //RTBWoH Process - Contains all element except first
                    if ((i != start) && (i != end))
                    {
                        strRTB1WoH += strArr[i] + "\n";
                    }
                    else if (i == end) //Last element will not have trailing new line
                    {
                        strRTB1WoH += strArr[i];
                    }

                    //RTBWoT Process - Contains all element except last
                    if ((i != (end - 1)) && (i != end))
                    {
                        strRTB1WoT += strArr[i] + "\n";
                    }
                    else if (i == (end - 1)) //Last element will not have trailing new line
                    {
                        strRTB1WoT += strArr[i];
                    }
                }
            }

        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            FN_SaveFile();
        }

        //Created by Chandraprakash [2017-08-30 | 06:51 PM]
        async void FN_SaveFile()
        {
            #region General Validations

            if (txtSourceFile.Text == "")
            {
                MessageBox.Show("Cannot save file without SourceFile Name," +
                                "please select SourceFile Button and choose a first select the source file",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            if (txtDestinationFile.Text == "")
            {
                MessageBox.Show("Cannot save file without DestinationFile Name,\n" +
                                "please select SourceFile Button and choose a source file first so that destination file will generated auomatically",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            if (lblTotalLineCountValue.Text == "")
            {
                MessageBox.Show("Cannot process while TotalLineCount is empty, Please select 'Source File' button and load input file again to calculate TotalLinecount",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            #endregion General Validations

            //If output file already exists, it'll overwrite or abort depending on the user input
            if (File.Exists(txtDestinationFile.Text))
            {
                //Added by Chandraprakash [2017-08-30 | 06:51 PM]
                if (MessageBox.Show($"File with same name as your output file [{Path.GetFileName(txtDestinationFile.Text)}] already exists." + "\n\n" +
                                    "Press OK to overwrite the existing file (or) Cancel to Abort saving.",
                                    "File Name Conflict",
                                    MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    File.Delete(txtDestinationFile.Text);
                }
                else
                {
                    return;
                }
            }

            bool isModify = chkModifyData.Checked;
            chkModifyData.Checked = false;
            rtb1.ReadOnly = true;

            ButtonsStatusDuringProcess(false);

            try
            {
                string rtbTemp = rtb1.Text;
                Splitter objSplitter = new Splitter();

                if (rdoReadFirst1000Lines.Checked == true)
                {
                    bool isSaveFile = await Task.Run(() => objSplitter.SaveFile(txtSourceFile.Text,
                                                                                txtDestinationFile.Text,
                                                                                rtbTemp,

                                                                                Convert.ToInt32(lblTotalLineCountValue.Text.Replace(",", "")),
                                                                                1,
                                                                                StartRange,

                                                                                ss1,
                                                                                ss2,
                                                                                ss1Pb1,
                                                                                ss2Lbl1,
                                                                                (byte)ReadType.First1000Lines)
                                                    );
                    if (isSaveFile == true)
                    {
                        ss2Lbl1.Text = "Saved Successfully";
                    }
                    else if (isSaveFile == false)
                    {
                        ss2Lbl1.Text = "";
                    }
                }
                else if (rdoReadLast1000Lines.Checked == true)
                {
                    bool isSaveFile = await Task.Run(() => objSplitter.SaveFile(txtSourceFile.Text,
                                                                                txtDestinationFile.Text,
                                                                                rtbTemp,

                                                                                Convert.ToInt32(lblTotalLineCountValue.Text.Replace(",", "")),
                                                                                Convert.ToInt32(lblTotalLineCountValue.Text.Replace(",", "")) - StartRange + 1,
                                                                                Convert.ToInt32(lblTotalLineCountValue.Text.Replace(",", "")),

                                                                                ss1,
                                                                                ss2,
                                                                                ss1Pb1,
                                                                                ss2Lbl1,
                                                                                (byte)ReadType.Last1000Lines)
                                                    );
                    if (isSaveFile == true)
                    {
                        ss2Lbl1.Text = "Saved Successfully";
                    }
                    else if (isSaveFile == false)
                    {
                        ss2Lbl1.Text = "";
                    }
                }
                else if (rdoReadCustomLines.Checked == true)
                {
                    #region Validations

                    if (txtFromRCL.Text == "")
                    {
                        MessageBox.Show("Please enter 'From' Value in Textbox as it cannot be empty",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (txtToRCL.Text == "")
                    {
                        MessageBox.Show("Please enter 'To' Value in Textbox as it cannot be empty",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }

                    int tStart;
                    if (int.TryParse(txtFromRCL.Text.Replace(",", ""), out tStart) == false)
                    {
                        MessageBox.Show("'From' Value should only be a number",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }

                    int tEnd;
                    if (int.TryParse(txtToRCL.Text.Replace(",", ""), out tEnd) == false)
                    {
                        return;
                    }

                    if (tStart < 1)
                    {
                        return;
                    }

                    if (tEnd == tStart)
                    {
                        return;
                    }

                    if (tEnd < tStart)
                    {
                        return;
                    }

                    #endregion Validations

                    bool isSaveFile = await Task.Run(() => objSplitter.SaveFile(txtSourceFile.Text,
                                                                                txtDestinationFile.Text,
                                                                                rtbTemp,

                                                                                Convert.ToInt32(lblTotalLineCountValue.Text.Replace(",", "")),
                                                                                tStart,
                                                                                tEnd,

                                                                                ss1,
                                                                                ss2,
                                                                                ss1Pb1,
                                                                                ss2Lbl1,
                                                                                (byte)ReadType.CustomLines)
                                                    );
                    if (isSaveFile == true)
                    {
                        ss2Lbl1.Text = "Saved Successfully";
                    }
                    else if (isSaveFile == false)
                    {
                        ss2Lbl1.Text = "";
                    }
                }

                Clear1();

                MessageBox.Show("Saved successfully !!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ButtonsStatusDuringProcess(true);
                chkModifyData.Checked = isModify;
            }
        }

        private void chkModifyData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkModifyData.Checked == true)
                {
                    rtb1.ReadOnly = false;
                }
                else if (chkModifyData.Checked == false)
                {
                    rtb1.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rtb1.Focus();
            }
        }

        //Created by Chandraprakash [2017-09-07 | 04:30 PM]
        private void txtFromRCL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FN_ReadCustomLines();
            }
        }

        //Created by Chandraprakash [2017-09-07 | 04:30 PM]
        private void txtToRCL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FN_ReadCustomLines();
            }
        }

        //Created by Chandraprakash [2017-08-31 | 03:14 PM]
        private void txtFromRCL_Enter(object sender, EventArgs e)
        {
            txtFromRCL.SelectAll();
        }

        //Created by Chandraprakash [2017-08-31 | 03:14 PM]
        private void txtToRCL_Enter(object sender, EventArgs e)
        {
            txtToRCL.SelectAll();
        }
    }

    //Added by Chandraprakash [2017-07-14 | 05:16 PM]
    //This class is used to change the color of the progress bar
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}