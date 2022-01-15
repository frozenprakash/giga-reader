//Created by Chandraprakash [2017-07]

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace GigaReader
{
    ////Created by Chandraprakash [2017-07-19 | 10:35 PM]
    //enum ExecutionType : byte
    //{
    //    pbLineCount,
    //    pbFileCount
    //}

    //Created by Chandraprakash [2017-08-29 | 04:53 PM]
    public class returnTotalLines
    {
        public int LineCount { get; set; }

        public string[] arrFirst1000Lines { get; set; }

        public string[] arrLast1000Lines { get; set; }

        public string[] arrCustomLines { get; set; }
    }

    class Splitter
    {
        #region Members

        private StreamReader sr;
        private StreamWriter sw;

        private ArrayList alFileNames;

        #endregion

        #region Constructors

        public Splitter()
        {
            alFileNames = new ArrayList();
        }

        #endregion

        #region Methods

        //Created by Chandraprakash [2017-05-19]
        //Modified by Chandraprakash [2017-07-17 | 03:43 PM]
        public object TotalLines(string strFileName,
                                int StartRange,

                                StatusStrip ss1,
                                StatusStrip ss2,
                                ToolStripProgressBar ss1Pb1,
                                ToolStripLabel ss2Lbl1,

                                bool rdoCustom,
                                string strStart,
                                string strEnd
                                )
        {
            int LineCount = 0,
                start = 0,
                end = 0;
            string tempStr = "";
            bool isCustomPass = false;


            #region Validation - Read Custom Lines

            if (rdoCustom == true)
            {
                if (strStart == "")
                {
                    MessageBox.Show("Please enter 'From' Value in Textbox as it cannot be empty",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return null;
                }

                if (strEnd == "")
                {
                    MessageBox.Show("Please enter 'To' Value in Textbox as it cannot be empty",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return null;
                }

                if (int.TryParse(strStart.Replace(",", ""), out start) == false)
                {
                    MessageBox.Show("'From' Value should only be a number",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return null;
                }

                if (int.TryParse(strEnd.Replace(",", ""), out end) == false)
                {
                    MessageBox.Show("'To' Value should only be a number",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return null;
                }

                if (start < 1)
                {
                    MessageBox.Show("'From' Value cannot be less than 1",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return null;
                }

                if (start > end)
                {
                    MessageBox.Show("'From' Value cannot be greater than 'To' Value",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return null;
                }

                isCustomPass = true;

                //if ((strStart != "") &&
                //    (strEnd != "") &&
                //    (int.TryParse(strStart, out start) == true) &&
                //    (int.TryParse(strEnd, out end) == true) &&
                //    (start >= 1) &&
                //    //(start != end) &&
                //    (end > start))
                //{
                //    isCustomPass = true;
                //}
            }

            #endregion Validation - Read Custom Lines

            ArrayList alFirst1000Lines = new ArrayList();
            ArrayList alLast1000Lines = new ArrayList();
            ArrayList alCustomLines = new ArrayList();

            returnTotalLines objReturn = new returnTotalLines();

            try
            {
                bool isCounted = false;

                #region ProgressBar Initialization

                if (ss1Pb1 != null)
                {
                    ss1.Invoke(new Action(() =>
                    {
                        ss1Pb1.Style = ProgressBarStyle.Marquee;
                        ss1Pb1.MarqueeAnimationSpeed = 10;
                    }
                    ));
                }

                #endregion

                if (!File.Exists(strFileName))
                {
                    return -1;
                }

                sr = File.OpenText(strFileName);
                LineCount = 0;

                DateTime dtPrevious = DateTime.Now;
                while (sr.Peek() != -1)
                {
                    //Modified by Chandraprakash [2017-08-29 | 04:30 PM]
                    LineCount++;
                    tempStr = sr.ReadLine();

                    //Load first and last 1000 or [x] Lines to array
                    if ((LineCount >= 1) &&
                        (LineCount <= StartRange))
                    {
                        alFirst1000Lines.Add(tempStr);
                        alLast1000Lines.Add(tempStr);
                    }
                    else if (LineCount > StartRange)
                    {
                        alLast1000Lines.RemoveAt(0);
                        alLast1000Lines.Add(tempStr);
                    }

                    if (isCustomPass == true)
                    {
                        if ((LineCount >= start) &&
                            (LineCount <= end))
                        {
                            alCustomLines.Add(tempStr);
                        }
                    }

                    #region ProgressBar and Label UI Update

                    if (ss2Lbl1 != null)
                    {
                        if ((DateTime.Now - dtPrevious).TotalMilliseconds > 50)
                        {
                            ss2.BeginInvoke(new Action(() =>
                                ss2Lbl1.Text = "Counting Lines:   " + LineCount.ToString("N0", CultureInfo.InvariantCulture)
                            ));

                            dtPrevious = DateTime.Now;
                        }
                    }

                    #endregion
                }
                isCounted = true;

                sr.Close();
                sr.Dispose();

                objReturn.LineCount = LineCount;

                objReturn.arrFirst1000Lines = alFirst1000Lines.Cast<string>().ToArray();
                objReturn.arrLast1000Lines = alLast1000Lines.Cast<string>().ToArray();
                objReturn.arrCustomLines = alCustomLines.Cast<string>().ToArray();
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
                if (ss1Pb1 != null)
                {
                    ss1.Invoke(new Action(() =>
                    {
                        ss1Pb1.Style = ProgressBarStyle.Blocks;
                        ss1Pb1.Value = 0;
                    }));
                }
                if (ss2Lbl1 != null)
                {
                    ss2.Invoke(new Action(() =>
                        ss2Lbl1.Text = ""
                    ));
                }
            }
            return objReturn;
        }

        //Created by Chandraprakash [2017-08-21 | 06:41 PM]
        public bool SaveFile(string strInFile,
                                string strOutFile,
                                string strData,

                                int TotalLineCount,
                                int start,
                                int end,

                                StatusStrip ss1,
                                StatusStrip ss2,
                                ToolStripProgressBar ss1Pb1,
                                ToolStripLabel ss2Lbl1,

                                byte pReadType)
        {
            bool isSaveFile = false;

            try
            {
                string lineRead = "";
                int cntLine = 0;
                DateTime dtPrevious = DateTime.Now;
                sr = File.OpenText(strInFile);
                sw = File.CreateText(strOutFile);

                #region Status UI Initialization

                ss1.Invoke(new Action(() =>
                        {
                            ss1Pb1.Style = ProgressBarStyle.Blocks;

                            ss1Pb1.Minimum = 0;
                            ss1Pb1.Maximum = TotalLineCount;
                            ss1Pb1.Value = 0;
                        }
                        ));

                ss2.Invoke(new Action(() =>
                {
                    ss2Lbl1.Text = "";
                }
                ));

                #endregion Status UI Initialization

                cntLine = 1;

                if ((TotalLineCount <= end) &&
                    pReadType != (byte)ReadType.CustomLines)
                {
                    sw.Write(strData);
                }
                else if (TotalLineCount >= end)
                {
                    while (true)
                    {
                        if (sr.Peek() == -1)
                        {
                            break;
                        }

                        #region Status UI Update

                        if (Convert.ToInt32((DateTime.Now - dtPrevious).TotalMilliseconds) > 50)
                        {
                            ss1.BeginInvoke(new Action(() =>
                                ss1Pb1.Value = cntLine
                            ));

                            ss2.BeginInvoke(new Action(() =>
                                ss2Lbl1.Text = $@"Writing to File:   {cntLine.ToString("N0", CultureInfo.InvariantCulture)} / {TotalLineCount.ToString("N0", CultureInfo.InvariantCulture)}"
                            ));

                            dtPrevious = DateTime.Now;
                        }

                        #endregion Status UI Update

                        //To skip the lines from Start to End in Source File [Will be replaced by the text in StrData (RTB1)]
                        if (cntLine == start)
                        {
                            if (pReadType == (byte)ReadType.First1000Lines)
                            {
                                sw.WriteLine(strData);
                            }
                            else if (pReadType == (byte)ReadType.Last1000Lines)
                            {
                                sw.Write(strData);
                            }
                            else if (pReadType == (byte)ReadType.CustomLines)
                            {
                                if (end < TotalLineCount)
                                {
                                    sw.WriteLine(strData);
                                }
                                else if (end == TotalLineCount)
                                {
                                    sw.Write(strData);
                                }
                            }
                        }
                        if ((cntLine >= start) && (cntLine <= end))
                        {
                            lineRead = sr.ReadLine();
                            cntLine++;
                        }
                        else
                        {
                            if (cntLine < TotalLineCount)
                            {
                                lineRead = sr.ReadLine();
                                cntLine++;
                                sw.WriteLine(lineRead);
                            }
                            else if (cntLine == TotalLineCount)
                            {
                                lineRead = sr.ReadLine();
                                cntLine++;
                                sw.Write(lineRead);
                            }
                        }
                    }
                }

                sr.Close();
                sw.Close();

                sr.Dispose();
                sw.Dispose();

                isSaveFile = true;
            }

            catch (InvalidOperationException ex)
            {
                try
                {
                    sr.Close();
                    sw.Close();

                    sr.Dispose();
                    sw.Dispose();

                    //if (Directory.Exists(strDestFolder))
                    //{
                    //    Directory.Delete(strDestFolder, true);
                    //}
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                ss1.Invoke(new Action(() =>
                    ss1Pb1.Value = 0
                ));

                ss2.Invoke(new Action(() =>
                    ss2Lbl1.Text = ""
                ));
            }

            return isSaveFile;
        }

        //Created by Chandraprakash [2017-08-11 | 07:01 PM]
        public string[] ReadLines(string strFilePath,
                                    int startLine,
                                    int endLine,

                                    StatusStrip ss1,
                                    StatusStrip ss2,
                                    ToolStripProgressBar ss1Pb1,
                                    ToolStripLabel ss2Lbl1)
        {
            string[] LineArr = new string[2]; //General Initialization

            try
            {
                #region Status UI Initialization - Phase1

                ss1.Invoke(new Action(() =>
                {
                    ss1Pb1.Style = ProgressBarStyle.Blocks;

                    ss1Pb1.Minimum = 1;
                    ss1Pb1.Maximum = startLine;
                    ss1Pb1.Value = 1;
                }
                ));

                ss2.Invoke(new Action(() =>
                {
                    ss2Lbl1.Text = "";
                }
                ));

                #endregion Status UI Initialization

                sr = File.OpenText(strFilePath);
                string lineRead;
                int cnt1;
                DateTime dtPrevious = DateTime.Now;
                int i1 = 0;

                //Array to capture lines between StartLine and EndLine
                //if ((endLine - startLine) == 0)
                //{
                //    LineArr = new string[2];
                //}

                if ((endLine - startLine) >= 0)
                {
                    LineArr = new string[endLine - startLine + 1]; // +1 = to include start and end number
                }

                //if (startLine == 1)
                //{
                //    //lineRead = sr.ReadLine(); //To read first line
                //}

                if (startLine >= 1)
                {
                    for (int i = 1; i < startLine; i++)
                    {
                        lineRead = sr.ReadLine(); //To skip lines until the pointer reaches to start line

                        #region Status UI Update - Skipping [1 to StartLine]

                        if (Convert.ToInt32((DateTime.Now - dtPrevious).TotalMilliseconds) > 50)
                        {
                            i1 = i;
                            ss1.BeginInvoke(new Action(() =>
                            {
                                ss1Pb1.Value = i1;
                            }
                            ));

                            i1 = i;
                            ss2.BeginInvoke(new Action(() =>
                            {
                                ss2Lbl1.Text = $@"Skipping until StartLine:   {i1.ToString("N0", CultureInfo.InvariantCulture)} / {startLine.ToString("N0", CultureInfo.InvariantCulture)}";
                            }
                            ));

                            dtPrevious = DateTime.Now;
                        }

                        #endregion Status UI Update
                    }
                }

                #region ProgressBar Initialization - Phase2

                ss1.Invoke(new Action(() =>
                        {
                            ss1Pb1.Minimum = startLine;
                            ss1Pb1.Maximum = endLine;
                            ss1Pb1.Value = startLine;
                        }
                        ));

                #endregion ProgressBar Initialization - Phase2

                //Modified by Chandraprakash [2017-09-07 | 07:12 PM]
                cnt1 = 0; //Array starting index
                for (int i = startLine; i <= endLine; i++)
                {
                    //Safety validation to check EOF if any unforeseen termination happens while reading between StartLine and EndLine
                    if (sr.Peek() == -1)
                    {
                        #region UI Update - Final

                        i1 = i;
                        ss1.Invoke(new Action(() =>
                            ss1Pb1.Value = i1
                        ));

                        ss2.Invoke(new Action(() =>
                            ss2Lbl1.Text = $@"Reading Lines:   {i1.ToString("N0", CultureInfo.InvariantCulture)} / {endLine.ToString("N0", CultureInfo.InvariantCulture)}"
                        ));

                        #endregion UI Update - Final

                        break;
                    }

                    LineArr[cnt1++] = sr.ReadLine();

                    #region Status UI Update - [StartLine to EndLine]

                    if (Convert.ToInt32((DateTime.Now - dtPrevious).TotalMilliseconds) > 50)
                    {
                        i1 = i;
                        ss1.BeginInvoke(new Action(() =>
                        {
                            ss1Pb1.Value = i1;
                        }
                        ));

                        ss2.BeginInvoke(new Action(() =>
                        {
                            ss2Lbl1.Text = $@"Reading Lines:   {i1.ToString("N0", CultureInfo.InvariantCulture)} / {endLine.ToString("N0", CultureInfo.InvariantCulture)}";
                        }
                        ));

                        dtPrevious = DateTime.Now;
                    }

                    #endregion Status UI Update
                }

                sr.Close();
                sr.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ss1.Invoke(new Action(() =>
                {
                    ss1Pb1.Minimum = 0;
                    ss1Pb1.Value = 0;
                }
                ));

                ss2.Invoke(new Action(() =>
                    ss2Lbl1.Text = "Read successfully"
                ));
            }
            return LineArr;
        }

        //Created by Chandraprakash [2017-07-18 | 11:51 PM]
        //New method with advanced optional parameters for live UI Updates while splitting
        public bool Split(string strFilePath,
                            string strDestFolder,
                            int SplitClaimCount,

                            // This value will be assigned by enum named [ExecutionType]
                            //  0 - Show File Count and Line Count  [ProgressBar = LineCount]
                            //  1 - Show File Count only            [ProgressBar = FileCount]
                            byte executionType = 0,

                            int TotalFileCount = -1,
                            int TotalClaimCount = -1,

                            StatusStrip ss1 = null,
                            StatusStrip ss2 = null,

                            ToolStripProgressBar ss1Pb1 = null,
                            ToolStripLabel ss2Lbl1 = null
                        )
        {
            bool isSplit = false;

            try
            {
                FileInfo info = new FileInfo(strFilePath);
                sr = File.OpenText(strFilePath);

                string dir = strDestFolder;
                string FileName = Path.GetFileNameWithoutExtension(strFilePath);
                string extension = Path.GetExtension(strFilePath);

                string lineRead;
                int fileCount = 1;
                string postfix;

                #region ProgressBar Initialization
                if (ss1Pb1 != null)
                {
                    ss1.BeginInvoke(new Action(() =>
                    {
                        if (executionType == (byte)ExecutionType.pbLineCount)
                        {
                            ss1Pb1.Minimum = 0;
                            ss1Pb1.Maximum = SplitClaimCount;
                        }
                        else if (executionType == (byte)ExecutionType.pbFileCount)
                        {
                            ss1Pb1.Minimum = 0;
                            ss1Pb1.Maximum = TotalFileCount;
                        }
                    }));
                }
                #endregion

                DateTime dtPrevious = DateTime.Now;

                while (true)
                {
                    if (sr.Peek() == -1)
                    {
                        break;
                    }

                    postfix = ('_' + fileCount.ToString());
                    string newPath = Path.Combine(dir, FileName + postfix + extension);

                    sw = File.CreateText(newPath);
                    System.Diagnostics.Debug.WriteLine(newPath);

                    for (int i = 0; i < SplitClaimCount; i++)
                    {
                        if (sr.Peek() == -1)
                        {
                            #region Final UI Update at 100% for n'th FileCount
                            if (ss2Lbl1 != null)
                            {
                                ss2.Invoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString() +
                                                        "     Line Count: " + (i + 1).ToString("N0", CultureInfo.InvariantCulture) + " / " + SplitClaimCount.ToString("N0", CultureInfo.InvariantCulture);
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString();
                                    }
                                }));
                            }
                            if (ss1Pb1 != null)
                            {
                                ss1.Invoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss1Pb1.Value = SplitClaimCount;
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss1Pb1.Value = fileCount;
                                    }
                                }
                                ));
                            }
                            #endregion

                            break;
                        }
                        lineRead = sr.ReadLine();
                        sw.WriteLine(lineRead);

                        if (ss2Lbl1 != null) //Will skip the DateTime calculation alltogether if this is null
                        {
                            #region UI General Update from 1 to 99%
                            if ((DateTime.Now - dtPrevious).TotalMilliseconds > 50)
                            {
                                ss2.BeginInvoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss2Lbl1.Text = $"Splitting file: {fileCount} / {TotalFileCount}" +
                                                        $"     Line Count: {(i + 1).ToString("N0", CultureInfo.InvariantCulture)} / {SplitClaimCount.ToString("N0", CultureInfo.InvariantCulture)}";
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss2Lbl1.Text = $"Splitting file: {fileCount} / {TotalFileCount}";
                                    }
                                }));

                                if (ss1Pb1 != null)
                                {
                                    ss1.BeginInvoke(new Action(() =>
                                    {
                                        if (executionType == (byte)ExecutionType.pbLineCount)
                                        {
                                            ss1Pb1.Value = i + 1;
                                        }
                                        else if (executionType == (byte)ExecutionType.pbFileCount)
                                        {
                                            ss1Pb1.Value = fileCount;
                                        }
                                    }));
                                }

                                dtPrevious = DateTime.Now;
                            }
                            #endregion

                            #region UI UPdate at 100% for FileCount 1 to (n-1)
                            //Will work for FileCount 1 to (n-1),
                            //as nth end loop wont always end at SplitClaimCount but before that itself mostly, and it'll be handled at the EOF above
                            if (i == SplitClaimCount - 1)
                            {
                                ss2.Invoke(new Action(() =>
                                {
                                    if (executionType == (byte)ExecutionType.pbLineCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString() +
                                                    "     Line Count: " + (i + 1).ToString("N0", CultureInfo.InvariantCulture) + " / " + SplitClaimCount.ToString("N0", CultureInfo.InvariantCulture);
                                    }
                                    else if (executionType == (byte)ExecutionType.pbFileCount)
                                    {
                                        ss2Lbl1.Text = "Splitting file: " + fileCount.ToString() + " / " + TotalFileCount.ToString();
                                    }
                                }));

                                if (ss1Pb1 != null)
                                {
                                    ss1.Invoke(new Action(() =>
                                    {
                                        if (executionType == (byte)ExecutionType.pbLineCount)
                                        {
                                            ss1Pb1.Value = i + 1;
                                        }
                                        else if (executionType == (byte)ExecutionType.pbLineCount)
                                        {
                                            ss1Pb1.Value = fileCount;
                                        }
                                    }));
                                }
                            }
                            #endregion
                        }


                    }
                    sw.Close();
                    fileCount++;
                }

                sr.Close();
                sr.Dispose();

                isSplit = true;
            }
            catch (InvalidOperationException ex)
            {
                try
                {
                    sr.Close();
                    sw.Close();

                    sr.Dispose();
                    sw.Dispose();

                    if (Directory.Exists(strDestFolder))
                    {
                        Directory.Delete(strDestFolder, true);
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (ss1 != null)
                {
                    ss1.Invoke(new Action(() =>
                        ss1Pb1.Value = 0
                    ));
                }

                if (ss2 != null)
                {
                    ss2.Invoke(new Action(() =>
                        ss2Lbl1.Text = ""
                    ));
                }
            }

            return isSplit;
        }

        #region commented
        //			public int split(string full_path, long lineCount)
        //			{
        //
        //			
        //				if (!File.Exists(full_path))
        //					return -1;
        //				FileInfo info = new FileInfo(full_path);
        //				rdr = File.OpenText(full_path);
        //			
        //				// separate the path, filename, and extension
        //				string dir = info.Directory.ToString();
        //				string name = info.Name;
        //			
        //				string[] nameArr  = name.Split('.');
        //				string extension = nameArr[nameArr.Length - 1];
        //			
        //				int lastDotIndex = name.LastIndexOf('.');
        //				string shortfilename = name.Remove(lastDotIndex, name.Length - lastDotIndex);
        //		
        //
        //				string lineRead;
        //				int fileCount = 0 ;
        //				string postfix;
        //
        //				do
        //				{
        //					if (rdr.Peek() == -1)
        //						break;
        //				
        //					postfix = (fileCount.ToString());
        //					wtr = File.CreateText(dir + '\\' + shortfilename + postfix + '.' + extension);
        //					System.Diagnostics.Debug.WriteLine(dir + shortfilename + postfix + '.' + extension);
        //					for (int i = 0; i < lineCount; i++)
        //					{
        //						if (rdr.Peek() == -1)
        //						{
        //							wtr.Close();
        //							break;
        //						}
        //						lineRead = rdr.ReadLine();
        //						wtr.WriteLine(lineRead);
        //					}
        //					wtr.Close();
        //					fileCount ++;
        //				} while(true);
        //
        //				rdr.Close();
        //				return fileCount;
        //			}
        #endregion

        #endregion

        #region Accessors

        public Array Filenames
        {
            get
            {
                Array arrNames = alFileNames.ToArray(Type.GetType("System.String"));
                return arrNames;
            }
        }

        #endregion

    }
}