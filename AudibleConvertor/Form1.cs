// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.Form1
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Amib.Threading;
using CustomControls;
using CustomControls.Controls;
using CustomControls.OS;
using DirectShowLib;
using IniParser;
using Inwards;
using Ionic.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Taskbar;
using ProgressODoom;
using RichTextBoxLinks;
using SoundEditor;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Media;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using System.Xml.Serialization;
using TagLib;
using TagLib.Id3v2;
using TagLib.Mpeg;
using TagLib.Mpeg4;

namespace AudibleConvertor
{
    public class Form1 : Form
    {
        public static string appPath = Path.GetDirectoryName(AudibleConvertor.GLOBALS.ExecutablePath);
        public static string thumbnailDir = Path.GetTempPath() + "\\inAudible";
        public int threads = Environment.ProcessorCount;
        private System.Timers.Timer uiTimer = new System.Timers.Timer();
        public string inputFileName = "";
        public string outputFileName = "";
        public string m3u = "";
        public SupportLibraries supportLibs = new SupportLibraries();
        public string fdkPath = "";
        public string iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "inAudible", "config.ini");
        public string cdWavPath = "";
        public char fileDeliminator = '|';
        public BindingList<Codec> availableCodecs = Form1.GenerateCodecs(false, "aax");
        public BindingSource codecBinding = new BindingSource();
        private ToolTip toolTip1 = new ToolTip();
        private AdvancedSplitting.Chapters myChapters = new AdvancedSplitting.Chapters();
        private List<double> splitPoints = new List<double>();
        private Audible myAudible = new Audible();
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private BatchFiles myBatchFiles = new BatchFiles();
        private string opusOptions = "";
        private string oggOptions = "";
        private VirtualWAV omniWAV = new VirtualWAV();
        public string outputFileMask = "";
        private bool firstRun = true;
        private long maxAAXsize = 2000000000;
        public AdvancedOptions myAdvancedOptions = new AdvancedOptions();
        private Form1.ProcessingMode currentProcessingMode = Form1.ProcessingMode.AAX;
        private BackgroundWorker bgWorker1 = new BackgroundWorker();
        private CoverArt myCoverArt = new CoverArt(Form1.thumbnailDir);
        private const int URLMON_OPTION_USERAGENT = 268435457;
        private Thread log;
        private bool uiReadyForUpdate;
        public int numInputFiles;
        public bool multithreading;
        private DateTime startTime;
        private bool autoEncodeOnDownload;
        public Thread endingMusicThread;
        public Process musicProcess;
        public bool audibleChapters;
        private List<double> originalChapters;
        private Audible[] myAudibles;
        private bool hasCoverArt;
        private bool WAVoutput;
        private bool MP3output;
        private bool helix;
        private bool M4Boutput;
        private bool FLACoutput;
        private bool opusOutput;
        private bool oggOutput;
        private bool studioOutput;
        private bool studioProcessingMode;
        private bool m4bTranscodeMode;
        private bool cdProcessingMode;
        private bool omniMode;
        private bool flacUnpacked;
        private bool mergeMode;
        private bool mainWindowMaximized;
        private bool finishedBatch;
        private bool mazeProgress;
        private long totalSecondsProcessed;
        private bool aaxMode;
        private bool m4pMode;
        private bool aaMode;
        private bool bardMode;
        private string[] mergedWAVfiles;
        private bool aaxOnce;
        private bool noUIupdates;
        private BARD myBardFile;
        private bool reRun;
        private string[] savedInitialDirs;
        private List<string> savedWorkingDirs;
        private WebBrowser myWB;
        private BackgroundWorker bgWorkerAAPostProcessing;
        private BackgroundWorker bgwAA;
        private BackgroundWorker bgwAdjust;
        private bool safeToSave;
        private IContainer components;
        public Button btnConvert;
        private System.Windows.Forms.TextBox txtInputFile;
        private Button btnInputFile;
        private Label label1;
        private Label label2;
        private Button btnOutputFile;
        public System.Windows.Forms.TextBox txtOutputFile;
        private GroupBox grpLame;
        private CheckBox chkMultithread;
        private GroupBox grpOutputType;
        private CheckBox chkFileSplitting;
        private Label label3;
        private System.Windows.Forms.TextBox txtSplitThreshold;
        private Label label4;
        private CheckBox chkAudibleSplit;
        private GroupBox grpSplitting;
        private PictureBox pbCover;
        private CheckBox chkKeepMonolithicMP3;
        private CheckBox chkMono;
        private TrackBar tbVBRquality;
        private Label lblVBRquality;
        private RadioButton rdVBR;
        private Button btnLog;
        private CheckBox chkEmbedCover;
        private Button btnOutputOptions;
        private GroupBox grpOutputOptions;
        private CheckBox chkBookTitle;
        private CheckBox chkAuthor;
        private CheckBox chkAuthorTitle;
        private GroupBox grpITunes;
        private System.Windows.Forms.TextBox txtITunesPassSize;
        private Label label8;
        private RadioButton rdITunesManual;
        private RadioButton rdITunesDesperation;
        private RadioButton rdITunesDefault;
        private Label lblResizer;
        private CheckBox chkM4Bsplit;
        private CheckBox chkRemoveAudibleMarkers;
        private CheckBox chkAutodetectChannels;
        private Label lblSampleRate;
        private CheckBox chkVerifyAudibleSplits;
        private RadioButton rdITunesNone;
        private System.Windows.Forms.TextBox txtItunesIdleCountdown;
        private Label lblIdleCountdown;
        private Button btnVCDSettings;
        private CheckBox chkChangeFileNumbering;
        private CheckBox chkRerun;
        private CheckBox chkDoNotTag;
        private Button btnMetadata;
        private System.Windows.Forms.TextBox txtChapterThreshold;
        private CheckBox chkChapterThreshold;
        private Label label9;
        private Button btnEditChapters;
        private Button btnAdvancedOptions;
        private Button btnCDrip;
        private Button btnSplitter;
        private Label lblDownloadStatus;
        private Button btnLossless;
        private CheckBox chkSplitByDuration;
        private System.Windows.Forms.TextBox txtDurationToSplit;
        private Button btnChapterMetadata;
        private ComboBox cmbBitrate;
        private RadioButton rdCBR;
        private CheckBox chkSameAsSource;
        private CheckBox chkCUEfile;
        private CheckBox chkNormalize;
        private ProgressBarEx goldProgressBarEx1;
        private PlainBackgroundPainter goldPlainBackgroundPainter1;
        private GradientGlossPainter goldGradientGlossPainter1;
        private PlainBorderPainter goldPlainBorderPainter1;
        private PlainProgressPainter goldPlainProgressPainter1;
        private MiddleGlossPainter goldMiddleGlossPainter1;
        private RoundGlossPainter goldRoundGlossPainter1;
        private GradientGlossPainter goldGradientGlossPainter2;
        private CheckBox chkDRC;
        private RichTextBoxEx rtbLog;
        private ComboBox cmbCodec;
        private CheckBox chkAdvancedCodecs;
        private ComboBox cmbSampleRate;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openAudibleFilesToolStripMenuItem;
        private ToolStripMenuItem audibleM4BToolStripMenuItem;
        private ToolStripMenuItem mP3ProcessingToolStripMenuItem;
        private ToolStripMenuItem anyAudioFileToolStripMenuItem;
        private ToolStripMenuItem directoryToolStripMenuItem;
        private ToolStripMenuItem chapterToolsToolStripMenuItem;
        private ToolStripMenuItem cDRippingToolStripMenuItem;
        private ToolStripMenuItem miscToolsToolStripMenuItem;
        private ToolStripMenuItem beginConversionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem advancedSplitterToolStripMenuItem;
        private ToolStripMenuItem chapterEditorToolStripMenuItem;
        private ToolStripMenuItem overdriveChapterizerToolStripMenuItem;
        private ToolStripMenuItem simpleRipperToolStripMenuItem;
        private ToolStripMenuItem multiDriveRipperToolStripMenuItem;
        private ToolStripMenuItem tagEditorToolStripMenuItem;
        private ToolStripMenuItem renamingToolToolStripMenuItem;
        private ToolStripMenuItem mP3M4BJoinerToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem advancedSettingsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem allowFormResizeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem chapterMetadataToolStripMenuItem;
        private ToolStripMenuItem wAVFilesToolStripMenuItem;
        private ToolStripMenuItem splitterToolStripMenuItem;
        private ToolStripMenuItem previewToolStripMenuItem;
        private ToolStripMenuItem tempFileToolStripMenuItem;
        private ToolStripMenuItem libraryToolStripMenuItem;
        private ToolStripMenuItem mP3SPLTToolStripMenuItem;
        private ToolStripMenuItem ffmpegToolStripMenuItem;
        private ToolStripMenuItem aARipperToolStripMenuItem;
        private ToolStripMenuItem directshowFilterToolStripMenuItem;
        private ToolStripMenuItem audibleManagerToolStripMenuItem;
        private ToolStripMenuItem inAudibleNGToolStripMenuItem;
        private ToolStripMenuItem aAXRipperToolStripMenuItem;
        private ToolStripMenuItem iTunesToolStripMenuItem;
        private ToolStripMenuItem audibleManagerToolStripMenuItem1;
        private ToolStripMenuItem inAudibleNGToolStripMenuItem1;
        private ToolStripMenuItem cDRipperToolStripMenuItem;
        private ToolStripMenuItem cUEToolsToolStripMenuItem;
        private ToolStripMenuItem cDDA2WAVToolStripMenuItem;
        private ToolStripMenuItem otherToolStripMenuItem;
        private ToolStripMenuItem beepOnJobCompletionToolStripMenuItem;
        private ToolStripMenuItem createNFOToolStripMenuItem;
        private ToolStripMenuItem wizardToolStripMenuItem;
        private CheckBox chkEmbedM4BChapters;
        private ToolStripMenuItem factoryResetToolStripMenuItem;
        private Button btnBatchEdit;
        private CheckBox chkMP3chapterTags;
        private ToolStripMenuItem audioEditorToolStripMenuItem;
        private GroupBox grpDownloader;
        private Label lblDlCodec;
        private Label lblDlProductId;
        private Label lblDlTitle;
        private CheckBox chkSaveCover;
        private ToolStripMenuItem inAudibleDownloadManagerToolStripMenuItem;
        private CheckBox chkAddExension;
        private ToolStripMenuItem selectFilesToolStripMenuItem;
        private ToolStripMenuItem recursiveDirectoriesToolStripMenuItem;
        private ToolStripMenuItem scrapeAudiblecomForAddedMetadataToolStripMenuItem;
        private ToolStripMenuItem internalToolStripMenuItem;
        private ToolStripMenuItem addAppleTagsToM4BToolStripMenuItem;
        private ToolStripMenuItem removeTinyVerySmallChaptersToolStripMenuItem;
        private CheckBox chkStripUnabridged;

        Action<string> _conversionCompleteAction;
        public Form1(Action<string> conversionCompleteAction) : this() => _conversionCompleteAction = conversionCompleteAction;

        public Form1()
        {
            this.InitializeComponent();
            this.Size = new Size(478, 679);
            this.grpLame.Enabled = false;
            this.grpSplitting.Enabled = false;
            this.grpOutputType.Enabled = false;
            this.grpITunes.Enabled = false;
            this.grpOutputOptions.Enabled = false;
            int height = this.lblResizer.Location.Y + this.lblResizer.Size.Height * 3;
            int x = this.grpOutputOptions.Location.X;
            this.AutoSize = false;
            this.Size = new Size(x, height);
            CLog.LogLevel = LOG_TYPE.LOG_DEBUG;
            CLog.ConsoleWrite = true;
            CLog.LogFolder = Path.GetTempPath() + "\\inAudible\\";
            CLog.ApplicationName = "inAudible";
            this.log = new Thread(new ThreadStart(CLog.StartLogger));
            this.log.IsBackground = true;
            this.log.Start();
            CLog.WriteLine(LOG_TYPE.LOG_DEBUG, "Initializing");
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            if (!((IEnumerable<string>)new string[3]
            {
        "en-US",
        "en-GB",
        "en-CA"
            }).Contains<string>(currentCulture.Name))
            {
                this.SetText("FYI-This machine is set to " + currentCulture.DisplayName + " [" + currentCulture.Name + "] - Disabling iTunes Automation");
                this.rdITunesNone.Checked = true;
            }
            if (this.GetVCDSettingsPath() == "")
                this.btnVCDSettings.Enabled = false;
            this.PopulateCodecs();
            this.bgwAdjust = new BackgroundWorker();
            this.bgwAdjust.DoWork += new DoWorkEventHandler(this.bgwAdjust_DoWork);
            this.bgwAdjust.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwAdjust_Completed);
            this.bgwAdjust.ProgressChanged += new ProgressChangedEventHandler(this.bgwAA_ProgressChanged);
            this.bgwAdjust.WorkerReportsProgress = true;
            this.uiTimer.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
            this.uiTimer.Interval = 1000.0;
            this.uiTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.uiReadyForUpdate = true;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major != 5 || Environment.OSVersion.Version.Minor < 1) && Environment.OSVersion.Version.Major < 6)
                return false;
            using (Process currentProcess = Process.GetCurrentProcess())
            {
                bool wow64Process;
                if (!Form1.IsWow64Process(currentProcess.Handle, out wow64Process))
                    return false;
                return wow64Process;
            }
        }

        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);

        public void ChangeUserAgent(string Agent)
        {
            Form1.UrlMkSetSessionOption(268435457, Agent, Agent.Length, 0);
        }

        private void PopulateCodecs()
        {
            this.codecBinding.DataSource = (object)this.availableCodecs;
            this.cmbCodec.DataSource = this.codecBinding.DataSource;
            this.cmbCodec.DisplayMember = "Description";
            this.cmbCodec.ValueMember = "Name";
        }

        private static BindingList<Codec> GenerateCodecs(bool advancedMode, string mode = "aax")
        {
            BindingList<Codec> bindingList = new BindingList<Codec>();
            if (advancedMode || mode == "aa")
            {
                bindingList.Add(new Codec("Lossless", "lossless", true, false, true, "mp3"));
                bindingList.Add(new Codec("AAC/M4B (NeroAAC)", "nero", true, true, false, "m4b"));
            }
            else
                bindingList.Add(new Codec("Lossless", "lossless", true, false, true, "m4b"));
            if (mode == "aax")
            {
                bindingList.Add(new Codec("AAC/M4B (Fraunhofer FDK)", "fdk", false, false, false, "m4b"));
                if (advancedMode)
                {
                    bindingList.Add(new Codec("AAC/M4B (ffmpeg)", "ffmpeg_aac", false, true, false, "m4b"));
                    bindingList.Add(new Codec("Raw AAC", "raw_aac", false, true, true, "aac"));
                    bindingList.Add(new Codec("FLAC", "flac", false, true, false, "flac"));
                    bindingList.Add(new Codec("MP3 (ffmpeg)", "ffmpeg_mp3", false, true, false, "mp3"));
                    bindingList.Add(new Codec("MP3 (Helix)", "helix", false, true, false, "mp3"));
                }
            }
            bindingList.Add(new Codec("MP3 (LAME)", "lame", true, false, false, "mp3"));
            if (mode == "aax" && advancedMode)
            {
                bindingList.Add(new Codec("Ogg", "ogg", false, true, false, "ogg"));
                bindingList.Add(new Codec("Opus", "opus", false, true, false, "opus"));
            }
            bindingList.Add(new Codec("WAV", "wav", true, false, false, "wav"));
            return bindingList;
        }

        private void SetLosslessMode(string mode)
        {
            object selectedValue = this.cmbCodec.SelectedValue;
            this.availableCodecs = !(mode == "MP3") ? Form1.GenerateCodecs(this.chkAdvancedCodecs.Checked, "aax") : Form1.GenerateCodecs(this.chkAdvancedCodecs.Checked, "aa");
            this.availableCodecs[0].Description = "\"True Decrypt\" output will be " + mode;
            this.codecBinding.DataSource = (object)this.availableCodecs;
            this.cmbCodec.DataSource = this.codecBinding.DataSource;
            try
            {
                this.cmbCodec.SelectedValue = selectedValue;
            }
            catch
            {
            }
        }

        private void SetLossyMode()
        {
            for (int index = this.availableCodecs.Count - 1; index >= 0; --index)
            {
                if (this.availableCodecs[index].Lossless)
                    this.availableCodecs.RemoveAt(index);
            }
            this.codecBinding.DataSource = (object)this.availableCodecs;
            this.cmbCodec.DataSource = this.codecBinding.DataSource;
            this.btnLossless.Text = "Lossy";
            this.btnLossless.BackColor = Color.Orange;
        }

        private void FilterCodecs(bool show)
        {
            Codec availableCodec = this.availableCodecs[0];
            this.availableCodecs = Form1.GenerateCodecs(show, "aax");
            if (availableCodec.Name == "lossless")
            {
                this.availableCodecs[0] = availableCodec;
            }
            else
            {
                for (int index = this.availableCodecs.Count - 1; index >= 0; --index)
                {
                    if (this.availableCodecs[index].Lossless)
                        this.availableCodecs.RemoveAt(index);
                }
            }
            this.codecBinding.DataSource = (object)this.availableCodecs;
            this.cmbCodec.DataSource = this.codecBinding.DataSource;
        }

        private bool IsCodecLossless()
        {
            bool flag = false;
            for (int index = 0; index < this.availableCodecs.Count; ++index)
            {
                if (this.GetSelectedCodec() == this.availableCodecs[index].Name && this.availableCodecs[index].Lossless)
                    flag = true;
            }
            return flag;
        }

        private string GetSelectedCodec()
        {
            string codec = "";
            if (this.IsHandleCreated)
            {
                this.Invoke((System.Action)(() =>
               {
                   try
                   {
                       codec = this.cmbCodec.SelectedValue.ToString();
                   }
                   catch
                   {
                   }
               }));
            }
            else
            {
                try
                {
                    codec = this.cmbCodec.SelectedValue.ToString();
                    if (codec == "AudibleConvertor.Codec")
                        codec = "";
                }
                catch
                {
                }
            }
            return codec;
        }

        private string GetSelectedCodecDescription()
        {
            string codec = "";
            if (this.IsHandleCreated)
                this.Invoke((System.Action)(() => codec = ((Codec)this.cmbCodec.SelectedItem).Description));
            return codec;
        }

        private Codec GetSelectedCodecObject()
        {
            Codec codec = new Codec("tmp", "tmp", false, false, false, "tmp");
            if (this.IsHandleCreated)
                this.Invoke((System.Action)(() => codec = (Codec)this.cmbCodec.SelectedItem));
            return codec;
        }

        private int EnsureCodecExists(string selected)
        {
            bool flag = false;
            int num = -1;
            for (int index = 0; index < this.availableCodecs.Count; ++index)
            {
                if (this.availableCodecs[index].Name == selected)
                {
                    flag = true;
                    num = index;
                    break;
                }
            }
            if (!flag)
            {
                this.chkAdvancedCodecs.Checked = true;
                this.RegenerateCodecs();
                this.codecBinding.DataSource = (object)this.availableCodecs;
                this.cmbCodec.DataSource = this.codecBinding.DataSource;
            }
            return num;
        }

        private void RegenerateCodecs()
        {
            BindingList<Codec> codecs = Form1.GenerateCodecs(true, "aax");
            Codec availableCodec = this.availableCodecs[0];
            this.availableCodecs = codecs;
            this.availableCodecs[0] = availableCodec;
        }

        private void SetSelectedCodec(string value)
        {
            if (value == null)
                return;
            try
            {
                if (!this.IsHandleCreated)
                    return;
                this.Invoke((System.Action)(() => this.cmbCodec.SelectedValue = (object)value));
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.myAdvancedOptions.cylon)
                this.SOXPlay(Form1.appPath + "\\inAudible.mp3", true);
            if (this.txtInputFile.Text == "")
            {
                int num1 = (int)MessageBox.Show("You need to specify an input file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (this.chkMultithread.Checked)
                    this.multithreading = true;
                this.totalSecondsProcessed = 0L;
                this.startTime = DateTime.Now;
                if (Directory.Exists(Path.GetDirectoryName(this.txtOutputFile.Text) + "\\split\\"))
                    Directory.Delete(Path.GetDirectoryName(this.txtOutputFile.Text) + "\\split\\", true);
                switch (this.myAdvancedOptions.threadPriority)
                {
                    case "Normal":
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
                        break;
                    case "Below Normal":
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
                        break;
                    case "Low":
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
                        break;
                    default:
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
                        break;
                }
                if (this.GetSelectedCodec() == "wav")
                {
                    this.WAVoutput = true;
                    this.studioOutput = false;
                    this.MP3output = false;
                    this.M4Boutput = false;
                    this.FLACoutput = false;
                    this.opusOutput = false;
                    this.oggOutput = false;
                    this.helix = false;
                }
                if (this.GetSelectedCodec() == "lame" || this.GetSelectedCodec() == "ffmpeg_mp3")
                {
                    this.MP3output = true;
                    this.studioOutput = false;
                    this.WAVoutput = false;
                    this.M4Boutput = false;
                    this.FLACoutput = false;
                    this.opusOutput = false;
                    this.oggOutput = false;
                    this.helix = false;
                }
                if (this.GetSelectedCodec() == "helix")
                {
                    this.MP3output = true;
                    this.studioOutput = false;
                    this.WAVoutput = false;
                    this.M4Boutput = false;
                    this.FLACoutput = false;
                    this.opusOutput = false;
                    this.oggOutput = false;
                    this.helix = true;
                }
                if (this.GetSelectedCodec() == "nero" || this.GetSelectedCodec() == "fdk" || this.IsCodecLossless())
                {
                    if (!this.aaMode || this.aaMode && this.GetSelectedCodec() == "nero")
                        this.M4Boutput = true;
                    else if (this.aaMode && this.IsCodecLossless())
                        this.M4Boutput = false;
                    this.studioOutput = false;
                    this.WAVoutput = false;
                    this.MP3output = false;
                    this.FLACoutput = false;
                    this.opusOutput = false;
                    this.oggOutput = false;
                    this.helix = false;
                }
                if (this.GetSelectedCodec() == "flac")
                {
                    this.FLACoutput = true;
                    this.studioOutput = false;
                    this.MP3output = false;
                    this.WAVoutput = false;
                    this.M4Boutput = false;
                    this.opusOutput = false;
                    this.oggOutput = false;
                    this.helix = false;
                }
                if (this.GetSelectedCodec() == "opus")
                {
                    this.opusOutput = true;
                    this.opusOptions = this.getOpusArgs();
                    this.studioOutput = false;
                    this.MP3output = false;
                    this.WAVoutput = false;
                    this.M4Boutput = false;
                    this.FLACoutput = false;
                    this.oggOutput = false;
                    this.helix = false;
                }
                if (this.GetSelectedCodec() == "ogg")
                {
                    this.oggOutput = true;
                    this.oggOptions = this.getOggOptions();
                    this.studioOutput = false;
                    this.MP3output = false;
                    this.WAVoutput = false;
                    this.M4Boutput = false;
                    this.FLACoutput = false;
                    this.opusOutput = false;
                    this.helix = false;
                }
                this.m3u = "";
                string wav = "";
                string str = "";
                int num2 = 1;
                int num3 = 0;
                Audible.diskLogger(this.Text);
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                Audible.diskLogger("Locale set to " + currentCulture.DisplayName + " [" + currentCulture.Name + "]");
                Audible.diskLogger("OS = " + Environment.OSVersion.VersionString);
                Audible.diskLogger("64-bit OS = " + (object)Form1.InternalCheckIsWow64());
                Audible.diskLogger("codec = " + this.GetSelectedCodec());
                Audible.diskLogger("chkEmbedCover = " + (object)this.chkEmbedCover.Checked);
                Audible.diskLogger("bitrate = " + this.cmbBitrate.SelectedItem);
                Audible.diskLogger("chkMono = " + (object)this.chkMono.Checked);
                Audible.diskLogger("rdCBR = " + (object)this.rdCBR.Checked);
                Audible.diskLogger("rdVBR = " + (object)this.rdVBR.Checked);
                Audible.diskLogger("tbVBRquality = " + (object)this.tbVBRquality.Value);
                Audible.diskLogger("chkMultithread = " + (object)this.chkMultithread.Checked);
                Audible.diskLogger("chkKeepMonolithicMP3 = " + (object)this.chkKeepMonolithicMP3.Checked);
                Audible.diskLogger("chkDoNotTag = " + (object)this.chkDoNotTag.Checked);
                Audible.diskLogger("chkM4Bsplit = " + (object)this.chkM4Bsplit.Checked);
                Audible.diskLogger("chkFileSplitting = " + (object)this.chkFileSplitting.Checked);
                Audible.diskLogger("chkAudibleSplit = " + (object)this.chkAudibleSplit.Checked);
                Audible.diskLogger("txtSplitThreshold = " + this.txtSplitThreshold.Text);
                Audible.diskLogger("chkRemoveAudibleMarkers = " + (object)this.chkRemoveAudibleMarkers.Checked);
                Audible.diskLogger("chkVerifyAudibleSplits = " + (object)this.chkVerifyAudibleSplits.Checked);
                Audible.diskLogger("chkChapterThreshold = " + (object)this.chkChapterThreshold.Checked);
                Audible.diskLogger("txtChapterThreshold = " + this.txtChapterThreshold.Text);
                Audible.diskLogger("chkAutodetectChannels = " + (object)this.chkAutodetectChannels.Checked);
                Audible.diskLogger("chkMono = " + (object)this.chkMono.Checked);
                Audible.diskLogger("cmbSampleRate = " + this.cmbSampleRate.Text);
                Audible.diskLogger("chkAuthor = " + (object)this.chkAuthor.Checked);
                Audible.diskLogger("chkBookTitle = " + (object)this.chkBookTitle.Checked);
                Audible.diskLogger("chkAuthorTitle = " + (object)this.chkAuthorTitle.Checked);
                Audible.diskLogger("rdITunesDefault = " + (object)this.rdITunesDefault.Checked);
                Audible.diskLogger("rdITunesDesperation = " + (object)this.rdITunesDesperation.Checked);
                Audible.diskLogger("rdITunesManual = " + (object)this.rdITunesManual.Checked);
                Audible.diskLogger("txtITunesPassSize = " + this.txtITunesPassSize.Text);
                Audible.diskLogger("chkRerun = " + (object)this.chkRerun.Checked);
                Audible.diskLogger("chkDoNotTag = " + (object)this.chkDoNotTag.Checked);
                if (this.aaxMode)
                {
                    if (this.m4bTranscodeMode)
                        this.SetText("M4B mode");
                    else if (this.m4pMode)
                        this.SetText("M4P mode");
                    else
                        this.SetText("AAX mode");
                    if (this.myBatchFiles.IsBatch())
                    {
                        this.SetText("MAROON-Validating output paths (this could take a while...)");
                        this.myBatchFiles.outputPath = this.txtOutputFile.Text;
                        this.myBatchFiles.myAudibles = this.myAudibles;
                        if ((!this.myBatchFiles.AlreadyEdited || this.myBatchFiles.HasErrors) && !this.myBatchFiles.ValidateBatch(this.chkAuthor.Checked, this.chkBookTitle.Checked, this.chkAuthorTitle.Checked, this.chkAddExension.Checked, this.GetSelectedCodecObject().Extension, this.chkStripUnabridged.Checked) && (!this.mergeMode || !this.myBatchFiles.safeToMerge))
                        {
                            this.SetText("RED-Problem with batch.");
                            if (!this.ShowBadPaths(this.myBatchFiles))
                            {
                                this.SetText("RED-Paths not fixed, aborting.");
                                return;
                            }
                        }
                    }
                    this.myBatchFiles.AlreadyEdited = false;
                    this.SetEncodeMode();
                    this.bgwAA = new BackgroundWorker();
                    this.bgwAA.DoWork += new DoWorkEventHandler(this.bgwAAX_DoWork);
                    this.bgwAA.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwAAX_Completed);
                    this.bgwAA.ProgressChanged += new ProgressChangedEventHandler(this.bgwAA_ProgressChanged);
                    this.bgwAA.WorkerReportsProgress = true;
                    this.bgwAA.RunWorkerAsync((object)new DecryptAAXOptions("", wav, 0.0)
                    {
                        mp3File = str,
                        splitPosition = num3,
                        piecenum = num2,
                        lameOptions = this.getLAMEargs(),
                        lameParams = this.getLAMEparams(),
                        m4bOptions = this.getM4Boptions()
                    });
                }
                else
                {
                    this.SetText("AA mode");
                    this.SetEncodeMode();
                    this.bgwAA = new BackgroundWorker();
                    this.bgwAA.DoWork += new DoWorkEventHandler(this.bgwAA_DoWork);
                    this.bgwAA.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwAA_Completed);
                    this.bgwAA.ProgressChanged += new ProgressChangedEventHandler(this.bgwAA_ProgressChanged);
                    this.bgwAA.WorkerReportsProgress = true;
                    this.bgwAA.RunWorkerAsync((object)new DecryptAAOptions("", wav, 0.0)
                    {
                        mp3File = str,
                        splitPosition = num3,
                        piecenum = num2,
                        lameOptions = this.getLAMEargs()
                    });
                }
            }
        }

        private bool ShowBadPaths(BatchFiles myBatchFiles)
        {
            Audible[] audibles = this.myAudibles;
            FormBatchEdit formBatchEdit = new FormBatchEdit();
            formBatchEdit.myBatchFiles = myBatchFiles;
            formBatchEdit.init();
            int num = (int)formBatchEdit.ShowDialog();
            if (formBatchEdit.applied)
            {
                this.SetText("GREEN-Changes applied.");
                myBatchFiles = formBatchEdit.myBatchFiles;
                return true;
            }
            formBatchEdit.Dispose();
            return false;
        }

        private string GetOSName()
        {
            object obj = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>().Select<ManagementObject, object>((System.Func<ManagementObject, object>)(x => x.GetPropertyValue("Caption"))).FirstOrDefault<object>();
            if (obj == null)
                return "Unknown";
            return obj.ToString();
        }

        private void bgwAdjust_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void bgwAAX_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DecryptAAXOptions data = e.Argument as DecryptAAXOptions;
                Audible.diskLogger("1 - Background worker started");
                int batch = 0;
                foreach (string str1 in (IEnumerable<string>)((IEnumerable<string>)this.inputFileName.Split(this.fileDeliminator)).OrderBy<string, string>((System.Func<string, string>)(f => f)))
                {
                    if (this.omniMode && batch > 0)
                        break;
                    if (this.myBatchFiles.IsBatch() && !this.omniMode)
                    {
                        if (!this.mergeMode)
                            this.SetText("MAROON-Batch mode: " + (object)(batch + 1) + " / " + (object)this.inputFileName.Split(this.fileDeliminator).Length + "\n");
                        else
                            this.SetText("Merging " + (object)(batch + 1) + " / " + (object)this.inputFileName.Split(this.fileDeliminator).Length);
                        if (this.chkRerun.Checked)
                        {
                            this.SetText("EAA-Disabling Re-Run mode in Batch mode.");
                            Form1.SetControlPropertyThreadSafe((Control)this.chkRerun, "Checked", (object)false);
                            this.reRun = false;
                        }
                    }
                    Audible.diskLogger("2 - Inside batch loop");
                    if (batch > 0 || this.finishedBatch && !this.cdProcessingMode)
                    {
                        if (this.m4bTranscodeMode)
                        {
                            this.myAudible.ffmpegPath = this.supportLibs.ffmpegPath;
                            this.myAudible.GetM4BMetaDataTagLib(str1);
                            this.myAudible.decryptedFile = str1;
                        }
                        else if (!this.studioProcessingMode)
                        {
                            if (this.myAudibles != null)
                            {
                                this.myAudible.GetCustomAAXMetaData(str1);
                                this.myAudible.title = this.myAudibles[batch].title;
                                this.myAudible.album = this.myAudibles[batch].album;
                                this.myAudible.author = this.myAudibles[batch].author;
                                this.myAudible.narrator = this.myAudibles[batch].narrator;
                                this.myAudible.year = this.myAudibles[batch].year;
                                this.myAudible.addTrackToTitle = this.myAudibles[batch].addTrackToTitle;
                                this.myAudible.SetComments(this.myAudibles[batch].GetComments());
                                this.myAudible.trackNum = this.myAudibles[batch].trackNum;
                                this.myAudible.trackTotal = this.myAudibles[batch].trackTotal;
                                if (this.myAdvancedOptions.AudibleScraping)
                                    this.myAudible.genre = this.myAudible.genre == null || this.myAudible.genre == "" ? this.GetAudibleDotComInfo(this.myAudibles[batch].id) : this.myAudibles[batch].genre;
                            }
                            else
                            {
                                this.myAudible.GetCustomAAXMetaData(str1);
                                if (this.myAdvancedOptions.AudibleScraping)
                                    this.myAudible.genre = this.GetAudibleDotComInfo(this.myAudible.id);
                            }
                            this.myAudible.totalTime = "";
                        }
                        else if (!this.omniMode)
                        {
                            try
                            {
                                InAudible inAudible = this.DeSerializeObject<InAudible>(str1);
                                this.myChapters.SetDoubleChapters(inAudible.chapters);
                                this.myAudible = inAudible.audible;
                            }
                            catch
                            {
                            }
                        }
                        if (this.chkStripUnabridged.Checked)
                            this.myAudible = this.StripUnabridged(this.myAudible);
                        bool flag = false;
                        if (!this.studioProcessingMode || this.m4bTranscodeMode)
                        {
                            flag = this.GetCoverFromAAX(str1);
                            if (!flag)
                                flag = this.GetCoverFromAudible(this.myAudible.id);
                        }
                        this.hasCoverArt = flag;
                        try
                        {
                            if (flag && this.chkEmbedCover.Checked)
                            {
                                string filename = Form1.thumbnailDir + "\\" + this.myCoverArt.GetImage(Form1.thumbnailDir);
                                Image image = (Image)new Bitmap(filename);
                                Form1.SetControlPropertyThreadSafe((Control)this.pbCover, "Image", (object)image.GetThumbnailImage(89, 80, (Image.GetThumbnailImageAbort)null, new IntPtr()));
                                image.Dispose();
                                this.myAudible.coverPath = filename;
                                Form1.SetControlPropertyThreadSafe((Control)this.pbCover, "Visible", (object)true);
                                this.hasCoverArt = true;
                                Form1.SetControlPropertyThreadSafe((Control)this.chkEmbedCover, "Checked", (object)true);
                                Form1.SetControlPropertyThreadSafe((Control)this.chkEmbedCover, "Visible", (object)true);
                            }
                            else
                                this.hasCoverArt = false;
                        }
                        catch
                        {
                            this.hasCoverArt = false;
                        }
                        string goodFileName = goodFileName = this.myAudible.GetASCIITag(this.myAudible.title);
                        this.Invoke((System.Action)(() =>
                       {
                           this.outputFileMask = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\" + goodFileName + ".mp3";
                           this.outputFileMask = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + " - " + batch.ToString("D2") + ".mp3";
                           this.outputFileName = this.outputFileMask;
                           this.myBatchFiles.outputPath = this.txtOutputFile.Text;
                       }));
                        this.ProcessOutputOptions(true, batch);
                        ++batch;
                        this.SetText("Book: " + this.myAudible.title);
                        this.SetText("Author: " + this.myAudible.author);
                        this.SetText("Narrator: " + this.myAudible.narrator);
                        this.SetText("Year: " + this.myAudible.year);
                        this.SetText("Detected " + this.myAudible.codec + " encoding.\r\n");
                        if (this.outputFileMask.Length > this.myBatchFiles.maxFileLength)
                        {
                            this.SetText("WARNING-The output file path \"" + this.outputFileMask + "\" is too long.  Windows has a maximum file/directory length of 260 characters.  Please adjust your output path.");
                            continue;
                        }
                    }
                    else
                    {
                        Audible.diskLogger("4 - Process Output Options");
                        bool forceLegacyMode = true;
                        if (!this.chkEmbedCover.Checked)
                            this.hasCoverArt = false;
                        this.myBatchFiles.outputPath = this.txtOutputFile.Text;
                        if (!this.myBatchFiles.IsBatch())
                        {
                            this.myBatchFiles.myAudibles = new Audible[1];
                            this.myBatchFiles.myAudibles[0] = this.myAudible;
                            this.myBatchFiles.oneOff = true;
                            if (!this.myBatchFiles.ValidateBatch(this.chkAuthor.Checked, this.chkBookTitle.Checked, this.chkAuthorTitle.Checked, this.chkAddExension.Checked, this.GetSelectedCodecObject().Extension, this.chkStripUnabridged.Checked))
                            {
                                this.SetText("RED-Your output path is too long.");
                                if (!this.ShowBadPaths(this.myBatchFiles))
                                {
                                    this.SetText("RED-Paths not fixed, aborting.");
                                    break;
                                }
                                forceLegacyMode = false;
                            }
                        }
                        this.ProcessOutputOptions(forceLegacyMode, batch);
                        Audible.diskLogger("5 - Options Set Correctly");
                        if (this.outputFileMask.Length > this.myBatchFiles.maxFileLength)
                        {
                            this.SetText("WARNING-The output file path \"" + this.outputFileMask + "\" is too long.  Windows has a maximum file/directory length of 260 characters.  Please adjust your output path.");
                            ++batch;
                            continue;
                        }
                        ++batch;
                    }
                    data.wavFile = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".wav";
                    data.mp3File = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".mp3";
                    if (!this.studioProcessingMode && !this.myAdvancedOptions.decrypt)
                    {
                        if (!this.myChapters.customChapters || batch > 0)
                        {
                            this.SetText("Extracting chapter markers...");
                            this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(str1));
                        }
                        if (!iTunes.checkVirtualCDinstallation())
                        {
                            this.SetText("WARNING-Virtual CD (http://www.virtualcd-online.com/) is required for AAX support and does not appear to be installed.  STOPPING.");
                            break;
                        }
                        if (!this.reRun && !iTunes.checkITunesInstallation())
                        {
                            this.SetText("WARNING-iTunes is required for AAX support and does not appear to be installed.  STOPPING.");
                            break;
                        }
                    }
                    else if (this.m4bTranscodeMode)
                    {
                        if (!this.myChapters.customChapters)
                        {
                            this.SetText("Extracting chapter markers...");
                            this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(str1));
                            Audible.diskLogger("6 - Succssfully read chapters");
                        }
                    }
                    else if (this.myAdvancedOptions.decrypt && !this.studioProcessingMode && (!this.myChapters.customChapters || batch > 1))
                    {
                        this.SetText("Extracting chapter markers...");
                        this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(str1));
                        Audible.diskLogger("6.5 - Extracted " + (object)this.myChapters.Count() + " chapters.");
                    }
                    string audibleTotalTime = this.getAudibleTotalTime(str1);
                    this.myAudible.nfo.totalTime = audibleTotalTime;
                    Audible.diskLogger("7 - Got total time: " + audibleTotalTime);
                    string[] strArray1 = audibleTotalTime.Split(':');
                    double num = double.Parse(strArray1[0], (IFormatProvider)CultureInfo.InvariantCulture) * 60.0 * 60.0 + double.Parse(strArray1[1], (IFormatProvider)CultureInfo.InvariantCulture) * 60.0 + double.Parse(strArray1[2], (IFormatProvider)CultureInfo.InvariantCulture);
                    if (this.myChapters.Count() > 2 && this.myChapters.GetChapterDouble(this.myChapters.Count() - 1) / num > 1.5)
                    {
                        this.SetText("FYI-Chapters appear to be broken.  Trying to fix...");
                        for (int pos = 0; pos < this.myChapters.Count(); ++pos)
                            this.myChapters.SetChapter(pos, this.myChapters.GetChapterDouble(pos) / 2.0);
                    }
                    if (this.m4bTranscodeMode && num - this.myChapters.GetChapterDouble(this.myChapters.Count() - 1) > 10.0)
                        this.myChapters.Add(num);
                    this.totalSecondsProcessed += (long)num;
                    TimeSpan timeSpan = TimeSpan.FromSeconds(num);
                    string str2 = Math.Floor(timeSpan.TotalHours).ToString() + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2");
                    this.SetText("Input file is " + str2 + " in " + (object)this.myChapters.Count() + " chapters");
                    List<double> doubleList = new List<double>();
                    this.myChapters.includeFileNumbers = this.myAdvancedOptions.includeChapterNumberInFilename;
                    this.myChapters.useAsTitle = this.myAdvancedOptions.useChapterAsTitle;
                    this.myChapters.useTrackAndTitleAsTitle = this.myAdvancedOptions.includeChapterAndTitleInTitleTag;
                    this.myChapters.leadingZeroes = this.LeadingZeroes();
                    Audible.diskLogger("8 - Entering encoding method...");
                    data.wavFile = this.decryptAAX(str1, data.wavFile, timeSpan.TotalSeconds, data);
                    if (data.wavFile == "aborted")
                    {
                        this.SetText("WARNING-Aborted!");
                        break;
                    }
                    if (data.wavFile == "nonWavMode")
                    {
                        this.SetText("VirtualCD is not outputting WAV files.  Please go to Settings-> Burn-> Output Format and set it to WAV.");
                        break;
                    }
                    if (this.WAVoutput || this.FLACoutput)
                        this.SetText("Completed.");
                    this.outputFileName = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + "." + this.GetSelectedCodecObject().Extension;
                    if (!this.MP3output)
                    {
                        if (this.GetSelectedCodec() == "raw_aac")
                        {
                            string source = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".mp4";
                            string str3 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".aac";
                            Form1.SafeRename(source, str3);
                            if (this.chkAudibleSplit.Checked)
                            {
                                this.SetText("Splitting...");
                                string[] strArray2 = Utilities.SplitGeneric(str3, Path.GetFileNameWithoutExtension(str3), this.myChapters, true);
                                for (int index = 0; index < strArray2.Length; ++index)
                                    Utilities.WriteTags(this.myAudible, strArray2[index], index + 1, strArray2.Length);
                            }
                            else
                                Utilities.WriteTags(this.myAudible, str3, 0, 0);
                        }
                        else if (this.M4Boutput)
                        {
                            string str3 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".mp4";
                            string str4 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".m4b";
                            if ((this.GetSelectedCodec() == "fdk" || this.GetSelectedCodec() == "lossless") && !this.chkDoNotTag.Checked)
                            {
                                this.SetText("Chapterizing and tagging...");
                                this.TagAndChapterFfmpeg(str3);
                            }
                            else if (!this.chkDoNotTag.Checked)
                            {
                                if (this.GetSelectedCodec() == "lossless" && !this.cdProcessingMode)
                                {
                                    this.SetText("Rebuilding AAC header...");
                                    this.FixAACHeader(str3);
                                }
                                this.SetText("Tagging...");
                                this.addM4Btags(this.myAudible, str3, "", "");
                                this.myChapters.UpdateDoubleChapters(this.fixChapterOffsets(this.myChapters.GetDoubleList()));
                                this.fixChapters(str3);
                                System.IO.File.Delete(str4);
                                System.IO.File.Move(str3, str4);
                            }
                            else if (this.chkDoNotTag.Checked)
                            {
                                if (this.IsCodecLossless() && !this.cdProcessingMode)
                                {
                                    this.SetText("Rebuilding AAC header...");
                                    this.FixAACHeader(str3);
                                }
                                string str5 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".aac";
                                System.IO.File.Delete(this.outputFileMask);
                            }
                            if (!this.chkDoNotTag.Checked && this.myAdvancedOptions.AppleTags)
                            {
                                Utilities utilities = new Utilities();
                                utilities.StopwatchStart();
                                this.SetText("Adding Apple tags...");
                                this.AddAppleTags(this.myAudible, str4);
                                utilities.StopwatchStop();
                                Audible.diskLogger("Apple tags added in " + utilities.StopwatchGetElapsed(""));
                            }
                            if (this.splitPoints.Count > 0 || this.chkM4Bsplit.Checked && !this.chkAudibleSplit.Checked)
                            {
                                this.myChapters.UpdateDoubleChapters(this.fixChapterOffsets(this.myChapters.GetDoubleList()));
                                if (num > (double)(this.myAdvancedOptions.iOSSplitThreshold * 60 * 60) || this.splitPoints.Count > 0)
                                    this.SplitM4BbyAudible(str4, num, false);
                                else
                                    this.SetText("File is less than " + (object)this.myAdvancedOptions.iOSSplitThreshold + " hours, not splitting.");
                            }
                            else if (this.chkAudibleSplit.Checked)
                            {
                                this.myChapters.UpdateDoubleChapters(this.fixChapterOffsets(this.myChapters.GetDoubleList()));
                                this.SplitM4BbyAudible(str4, num, true);
                            }
                        }
                    }
                    if (this.chkCUEfile.Checked)
                    {
                        string fileName = "";
                        string cueType = "";
                        if (this.FLACoutput)
                        {
                            fileName = Path.GetFileNameWithoutExtension(this.outputFileName) + ".flac";
                            cueType = "FLAC";
                        }
                        else if (this.MP3output)
                        {
                            fileName = Path.GetFileNameWithoutExtension(this.outputFileName) + ".mp3";
                            cueType = "MP3";
                        }
                        else if (this.M4Boutput)
                        {
                            fileName = Path.GetFileNameWithoutExtension(this.outputFileName) + ".m4b";
                            cueType = "MP4";
                        }
                        string[] strArray2 = str2.Split(':');
                        double totalSeconds = (double)(int.Parse(strArray2[0]) * 60 * 60 + int.Parse(strArray2[1]) * 60 + int.Parse(strArray2[2]));
                        this.myAudible.writeCUEfile(this.myAudible.getCUEfromChapters(this.myChapters, fileName, cueType, totalSeconds), Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".cue");
                    }
                    if (this.MP3output && !this.chkAudibleSplit.Checked && (this.chkMP3chapterTags.Checked && !this.chkSplitByDuration.Checked))
                    {
                        Utilities utilities = new Utilities();
                        utilities.StopwatchStart();
                        string[] strArray2 = str2.Split(':');
                        double totalSeconds = (double)(int.Parse(strArray2[0]) * 60 * 60 + int.Parse(strArray2[1]) * 60 + int.Parse(strArray2[2]));
                        TagLib.File file = TagLib.File.Create(this.outputFileName);
                        TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag)file.GetTag(TagTypes.Id3v2, false);
                        if (tag != null)
                            tag.Version = (byte)3;
                        UserTextInformationFrame informationFrame1 = new UserTextInformationFrame("UFID", StringType.UTF16);
                        string cuEfromChapters = this.myAudible.getCUEfromChapters(this.myChapters, "", "MP3", totalSeconds);
                        UserTextInformationFrame informationFrame2 = new UserTextInformationFrame("CUESHEET", StringType.UTF16);
                        informationFrame2.Text = new string[1]
                        {
              cuEfromChapters
                        };
                        tag.AddFrame((Frame)informationFrame2);
                        file.Save();
                        utilities.StopwatchStop();
                        Audible.diskLogger(utilities.StopwatchGetElapsed("chapter tag time"));
                    }
                    if (this.myAudible.coverPath != "")
                    {
                        if (this.hasCoverArt)
                        {
                            try
                            {
                                if (this.chkSaveCover.Checked)
                                    System.IO.File.Copy(this.myAudible.coverPath, Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".jpg", true);
                                this.myCoverArt.Cleanup();
                            }
                            catch
                            {
                            }
                        }
                    }
                    if (this.myAdvancedOptions.nfo)
                        this.CreateNFO();
                }
            }
            catch (Exception ex)
            {
                this.SetText("WARNING-Crash in main decryption loop: " + ex.ToString());
            }
        }

        private void AddAppleTags(Audible myAudible, string outputFile)
        {
            if (myAudible.id == "")
                return;
            TagLib.File file = TagLib.File.Create(outputFile);
            AppleTag tag = (AppleTag)file.GetTag(TagTypes.Apple, true);
            tag.SetText((ByteVector)"prID", myAudible.bookId);
            tag.SetText((ByteVector)"cpub", myAudible.publisher);
            tag.SetText((ByteVector)"CDEK", "http://www.audible.com/pd/" + myAudible.id);
            tag.Copyright = myAudible.originalTags.Copyright;
            file.Save();
        }

        private void TagAndChapterFfmpeg(string outputFile)
        {
            string str1 = "";
            List<double> doubleList = this.myChapters.GetDoubleList();
            if (doubleList[0] != 0.0)
                str1 = " -ss " + doubleList[0].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " -t " + (doubleList[doubleList.Count - 1] - 1.0).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " ";
            string str2 = "";
            if (!this.chkAudibleSplit.Checked)
                str2 = this.GenerateFfmpegChapters(this.myChapters);
            string ffmpegTags = this.GenerateFfmpegTags();
            string path = outputFile + ".ff.txt";
            string str3 = Path.GetDirectoryName(this.outputFileName) + "\\tempChaps.mp4";
            if (!this.chkEmbedM4BChapters.Checked)
                str2 = "";
            System.IO.File.WriteAllText(path, ffmpegTags + str2);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-y -i \"" + outputFile + "\" -f ffmetadata -i \"" + path + "\" -map_metadata 1  -bsf:a aac_adtstoasc -c:a copy" + str1 + " -map 0 \"" + str3 + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.outputFileMask);
            process.Start();
            process.WaitForExit();
            if (this.hasCoverArt)
                this.ReinsertCoverArt3(str3);
            bool flag = true;
            while (flag)
            {
                try
                {
                    System.IO.File.Delete(outputFile);
                    System.IO.File.Delete(this.outputFileName);
                    System.IO.File.Delete(path);
                    System.IO.File.Move(str3, this.outputFileName);
                    flag = false;
                }
                catch
                {
                    flag = true;
                }
            }
            this.AddAppleTags(this.outputFileName);
        }

        private void AddAppleTags(string file)
        {
            TagLib.File file1 = TagLib.File.Create(file, "audio/mp4", ReadStyle.Average);
            AppleTag tag = (AppleTag)file1.GetTag(TagTypes.Apple, true);
            tag.Publisher = this.myAudible.publisher;
            tag.LongDescription = this.myAudible.GetComments();
            tag.Description = this.myAudible.GetComments();
            file1.Save();
            file1.Dispose();
        }

        private string GenerateFfmpegTags()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(";FFMETADATA1\n");
            stringBuilder.Append("major_brand=aax\n");
            stringBuilder.Append("minor_version=1\n");
            stringBuilder.Append("compatible_brands=aax M4B mp42isom\n");
            stringBuilder.Append("date=" + this.myAudible.year + "\n");
            stringBuilder.Append("genre=" + this.myAudible.genre + "\n");
            stringBuilder.Append("title=" + this.myAudible.title + "\n");
            stringBuilder.Append("artist=" + this.myAudible.author + "\n");
            stringBuilder.Append("album=" + this.myAudible.album + "\n");
            if (this.myAudible.trackNum != null && this.myAudible.trackNum != "")
                stringBuilder.Append("track=" + this.myAudible.trackNum + "/" + this.myAudible.trackTotal + "\n");
            stringBuilder.Append("composer=" + this.myAudible.narrator + "\n");
            string str = this.myAudible.GetComments().Length >= 254 ? this.myAudible.GetComments().Substring(0, 254) : this.myAudible.GetComments();
            stringBuilder.Append("comment=" + str + "\n");
            stringBuilder.Append("description=" + this.myAudible.GetComments() + "\n");
            return stringBuilder.ToString();
        }

        private string GenerateFfmpegChapters(AdvancedSplitting.Chapters chapters)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < chapters.Count() - 1; ++index)
            {
                TimeSpan.FromSeconds(chapters.GetChapterDouble(index));
                stringBuilder.Append("[CHAPTER]\n");
                stringBuilder.Append("TIMEBASE=1/1000\n");
                stringBuilder.Append("START=" + (object)(chapters.GetChapterDouble(index) * 1000.0) + "\n");
                stringBuilder.Append("END=" + (object)(chapters.GetChapterDouble(index) * 1000.0) + "\n");
                string chapterTitle = this.myAudible.GetChapterTitle(chapters.GetDoubleList(), chapters.GetChapterNames(true), index);
                stringBuilder.Append("title=" + chapterTitle + "\n");
            }
            return stringBuilder.ToString();
        }

        private void ReinsertCoverArt3(string m4bName)
        {
            this.myCoverArt.GetImage(Form1.thumbnailDir);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.atomicParsleyPath;
            process.StartInfo.Arguments = "\"" + m4bName + "\" --encodingTool \"" + this.Text + "\" --artwork \"" + this.myAudible.coverPath + "\" --overWrite";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.outputFileMask);
            process.Start();
            process.WaitForExit();
        }

        private void TagSplitM4B(string m4bName, int trackNum, int trackTotal, bool coverArt)
        {
            this.myCoverArt.GetImage(Form1.thumbnailDir);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.atomicParsleyPath;
            if (coverArt)
                process.StartInfo.Arguments = "\"" + m4bName + "\" --encodingTool \"" + this.Text + "\" --artwork \"" + this.myAudible.coverPath + "\" --tracknum \"" + (object)trackNum + "/" + (object)trackTotal + "\" --overWrite";
            else
                process.StartInfo.Arguments = "\"" + m4bName + "\" --encodingTool \"" + this.Text + "\" --tracknum \"" + (object)trackNum + "/" + (object)trackTotal + "\" --overWrite";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.outputFileMask);
            process.Start();
            process.WaitForExit();
        }

        private void CreateNFO()
        {
            string str1 = "";
            string[] strArray = this.myAudible.nfo.totalTime.Split(':');
            if (int.Parse(strArray[0]) != 0)
                str1 = int.Parse(strArray[0], (IFormatProvider)CultureInfo.InvariantCulture).ToString() + " hours, ";
            string str2 = str1 + (object)int.Parse(strArray[1], (IFormatProvider)CultureInfo.InvariantCulture) + " minutes, " + (object)(int)double.Parse(strArray[2], (IFormatProvider)CultureInfo.InvariantCulture) + " seconds";
            string str3 = "No";
            if (this.myAudible.nfo.lossless)
            {
                str3 = "Yes";
                this.myAudible.nfo.targetFormat = "AAC / M4B";
                this.myAudible.nfo.targetSampleRate = this.myAudible.nfo.sourceSampleRate;
                this.myAudible.nfo.targetChannels = this.myAudible.nfo.sourceChannels;
                this.myAudible.nfo.targetBitrate = this.myAudible.nfo.sourceBitrate;
                this.myAudible.nfo.vbr = false;
            }
            string str4 = "General Information\r\n===================\r\n" + " Title:                  " + this.myAudible.title + "\r\n" + " Author:                 " + this.myAudible.author + "\r\n" + " Read By:                " + this.myAudible.narrator + "\r\n" + " Copyright:              " + this.myAudible.year + "\r\n" + " Audiobook Copyright:    " + this.myAudible.year + "\r\n";
            if (this.myAudible.genre != "")
                str4 = str4 + " Genre:                  " + this.myAudible.genre + "\r\n";
            string str5 = str4 + " Publisher:              " + this.myAudible.publisher + "\r\n" + " Duration:               " + str2 + "\r\n" + " Chapters:               " + this.myAudible.nfo.chapters + "\r\n" + "\r\n\r\n" + "Media Information\r\n=================\r\n" + " Source Format:          " + this.myAudible.nfo.sourceFormat + "\r\n" + " Source Sample Rate:     " + this.myAudible.nfo.sourceSampleRate + " Hz\r\n" + " Source Channels:        " + this.myAudible.nfo.sourceChannels + "\r\n" + " Source Bitrate:         " + this.myAudible.nfo.sourceBitrate + " kbits\r\n" + "\r\n" + " Lossless Encode:        " + str3 + "\r\n" + " Encoded Codec:          " + this.myAudible.nfo.targetFormat + "\r\n" + " Encoded Sample Rate:    " + this.myAudible.nfo.targetSampleRate + " Hz\r\n" + " Encoded Channels:       " + this.myAudible.nfo.targetChannels + "\r\n";
            string s = (!this.myAudible.nfo.vbr ? str5 + " Encoded Bitrate:        " + this.myAudible.nfo.targetBitrate + " kbits\r\n" : str5 + " Encoded Bitrate:        VBR " + this.myAudible.nfo.vbrValue + "\r\n") + "\r\n" + " Ripper:                 " + this.Text + "\r\n" + "\r\n\r\n" + "Book Description\r\n================\r\n" + this.myAudible.GetComments();
            try
            {
                System.IO.File.WriteAllBytes(Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".nfo", Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("windows-1252"), Encoding.UTF8.GetBytes(s)));
            }
            catch
            {
            }
        }

        private List<double> fixChapterOffsets(List<double> oldChapters)
        {
            List<double> doubleList = new List<double>();
            double oldChapter = oldChapters[0];
            if (oldChapter == 0.0)
                return oldChapters;
            for (int index = 0; index < oldChapters.Count; ++index)
                doubleList.Add(oldChapters[index] - oldChapter);
            return doubleList;
        }

        private void SplitM4BbyAudible(string m4bName, double totalSeconds, bool splitAllChapters)
        {
            List<double> doubleList1 = this.myChapters.GetDoubleList();
            List<double> doubleList2 = this.GetM4BSplitPoints(doubleList1, totalSeconds);
            if (this.splitPoints.Count > 0)
            {
                doubleList2 = this.splitPoints;
                doubleList2.Insert(0, 0.0);
            }
            if (splitAllChapters)
            {
                doubleList2 = new List<double>();
                for (int index = 0; index < doubleList1.Count - 1; ++index)
                    doubleList2.Add(doubleList1[index]);
            }
            string directoryName = Path.GetDirectoryName(m4bName);
            string withoutExtension = Path.GetFileNameWithoutExtension(m4bName);
            this.SetText("Splitting M4B into " + (object)doubleList2.Count + " pieces");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.WorkingDirectory = directoryName;
            string str1 = "";
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            for (int pos = 0; pos < doubleList2.Count; ++pos)
            {
                string str2 = withoutExtension + " - " + (pos + 1).ToString("D3");
                if (splitAllChapters)
                    str2 = this.myChapters.GetFinalFileName(pos, withoutExtension);
                if (doubleList2.Count != pos + 1)
                    str1 = str1 + " -c copy -map 0 -vn -t " + (doubleList2[pos + 1] - doubleList2[pos]).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " -ss " + doubleList2[pos].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " \"" + str2 + ".m4a\"";
                else
                    str1 = str1 + " -c copy -map 0 -vn -ss " + doubleList2[pos].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " \"" + str2 + ".m4a\"";
                stringList1.Add(directoryName + "\\" + str2 + ".m4a");
                if (str1.Length > 7500)
                {
                    stringList2.Add(str1);
                    str1 = "";
                }
            }
            if (stringList2.Count == 0)
            {
                process.StartInfo.Arguments = string.Format("-y -i \"{0}\" {1}", (object)m4bName, (object)str1);
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
            else
            {
                if (str1 != "")
                    stringList2.Add(str1);
                foreach (string str2 in stringList2)
                {
                    process.StartInfo.Arguments = string.Format("-y -i \"{0}\" {1}", (object)m4bName, (object)str2);
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                }
            }
            if (process.ExitCode != 0)
            {
                this.SetText("WARNING-ffmpeg failed!");
                this.SetText("EAA-" + process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            }
            else
            {
                System.IO.File.Delete(m4bName);
                if (this.myChapters.useAsTitle)
                {
                    for (int pos = 0; pos < doubleList2.Count; ++pos)
                        this.TagM4BTitle(stringList1[pos], this.myChapters.GetFormattedFileName(pos));
                }
                FileInfo[] files = new DirectoryInfo(Path.GetDirectoryName(this.outputFileMask)).GetFiles("*.m4a");
                int trackNum = 1;
                foreach (FileInfo fileInfo in files)
                {
                    string str2 = directoryName + "\\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + ".m4b";
                    try
                    {
                        System.IO.File.Delete(str2);
                    }
                    catch
                    {
                    }
                    this.TagSplitM4B(fileInfo.FullName, trackNum, files.Length, this.hasCoverArt);
                    ++trackNum;
                    System.IO.File.Move(fileInfo.FullName, str2);
                }
            }
        }

        private void ProcessOutputOptions(bool forceLegacyMode, int batch = 0)
        {
            if (!this.chkBookTitle.Checked && !this.chkAuthor.Checked && !this.chkAuthorTitle.Checked)
            {
                this.outputFileMask = this.myBatchFiles.outputFiles[batch];
                this.outputFileName = this.outputFileMask;
            }
            else
            {
                string title = this.myAudible.title;
                string author = this.myAudible.author;
                string asciiTag1 = this.myAudible.GetASCIITag(title);
                string asciiTag2 = this.myAudible.GetASCIITag(author);
                string outputPath = "";
                this.Invoke((System.Action)(() => outputPath = Path.GetDirectoryName(this.txtOutputFile.Text)));
                if (this.aaMode || forceLegacyMode && batch == 0 && (!this.myBatchFiles.IsBatch() && this.myBatchFiles.oneOff))
                {
                    if (this.chkAuthor.Checked)
                        outputPath = outputPath + "\\" + asciiTag2;
                    if (this.chkBookTitle.Checked)
                        outputPath = outputPath + "\\" + asciiTag1;
                    if (this.chkAuthorTitle.Checked)
                        outputPath = outputPath + "\\" + asciiTag2 + " - " + asciiTag1;
                    if (this.chkAddExension.Checked)
                        outputPath = outputPath + "." + this.GetSelectedCodecObject().Extension.ToUpper();
                    string fileName = Path.GetFileName(this.txtOutputFile.Text);
                    this.outputFileMask = !this.txtInputFile.Text.Contains<char>(this.fileDeliminator) ? outputPath + "\\" + fileName : outputPath + "\\" + asciiTag1 + ".mp3";
                    this.CreateFullOutputPath(this.outputFileMask);
                }
                else
                {
                    string outputFile = this.myBatchFiles.outputFiles[batch];
                    this.CreateFullOutputPath(outputFile);
                    outputPath = Path.GetDirectoryName(outputFile);
                    this.outputFileMask = outputFile;
                }
                if (this.mergeMode)
                    this.outputFileMask = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + " - part " + batch.ToString("D2") + ".mp3";
                this.outputFileName = this.outputFileMask;
            }
        }

        private void CreateFullOutputPath(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        private string GetFuturePath()
        {
            if (!this.chkBookTitle.Checked && !this.chkAuthor.Checked && !this.chkAuthorTitle.Checked)
                return "";
            string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string str2 = this.myAudible.title;
            string str3 = this.myAudible.author;
            foreach (char ch in str1)
            {
                str2 = str2.Replace(ch.ToString(), "");
                str3 = str3.Replace(ch.ToString(), "");
            }
            string str4 = Path.GetDirectoryName(this.txtOutputFile.Text);
            if (this.chkAuthor.Checked)
                str4 = str4 + "\\" + str3;
            if (this.chkBookTitle.Checked)
                str4 = str4 + "\\" + str2;
            if (this.chkAuthorTitle.Checked)
                str4 = str4 + "\\" + str3 + " - " + str2;
            string fileName = Path.GetFileName(this.txtOutputFile.Text);
            return !this.txtInputFile.Text.Contains<char>(this.fileDeliminator) ? str4 + "\\" + fileName : str4 + "\\" + str2 + ".mp3";
        }

        private void bgwAA_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DecryptAAOptions data = e.Argument as DecryptAAOptions;
                int startTrackNum = 1;
                string[] strArray1 = this.inputFileName.Split(this.fileDeliminator);
                for (int index = 0; index < strArray1.Length; ++index)
                {
                    this.ProcessOutputOptions(true, 0);
                    if (this.outputFileMask.Length > this.myBatchFiles.maxFileLength)
                    {
                        this.SetText("WARNING-The output file path is too long.  Windows has a maximum file/directory length of 260 characters.  Please adjust your output path.");
                        break;
                    }
                    if (this.numInputFiles > 1)
                    {
                        data.wavFile = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + " - " + data.piecenum.ToString("D3") + ".wav";
                        data.mp3File = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + " - " + data.piecenum.ToString("D3") + ".mp3";
                        ++data.piecenum;
                    }
                    else
                    {
                        data.wavFile = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".wav";
                        data.mp3File = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".mp3";
                    }
                    bool flag1 = false;
                    if (!this.myChapters.customChapters && this.chkAudibleSplit.Checked && this.GetSelectedCodec() == "lossless")
                        flag1 = true;
                    if (!this.myChapters.customChapters || index > 0)
                    {
                        if (this.IsXPMode() || this.myAudible.codec != "mp332")
                            this.myChapters.SetDoubleChapters(this.myAudible.getAudibleChapters(strArray1[index]));
                        else if (!flag1 && (this.chkAudibleSplit.Checked || this.M4Boutput))
                            this.myChapters.SetDoubleChapters(this.GetAAChapters(strArray1[index]));
                    }
                    string audibleTotalTime = this.getAudibleTotalTime(strArray1[index]);
                    if (this.myChapters.Count() == 0)
                        this.SetText("Input file is " + audibleTotalTime);
                    else
                        this.SetText("Input file is " + audibleTotalTime + " in " + (object)this.myChapters.Count() + " chapters");
                    string[] strArray2 = audibleTotalTime.Split(':');
                    double totalSeconds1 = double.Parse(strArray2[0], (IFormatProvider)CultureInfo.InvariantCulture) * 60.0 * 60.0 + double.Parse(strArray2[1], (IFormatProvider)CultureInfo.InvariantCulture) * 60.0 + double.Parse(strArray2[2], (IFormatProvider)CultureInfo.InvariantCulture);
                    this.totalSecondsProcessed += (long)totalSeconds1;
                    TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds1);
                    data.aaFile = strArray1[index];
                    data.totalSeconds = timeSpan.TotalSeconds;
                    string str1 = "";
                    bool flag2 = false;
                    VirtualWAV myVirtualWav = new VirtualWAV();
                    if (!System.IO.File.Exists(this.myAudible.coverPath) && this.ScrapeAAJPG(strArray1[index]))
                    {
                        this.myAudible.coverPath = Form1.thumbnailDir + "\\" + this.myCoverArt.GetImage(Form1.thumbnailDir);
                        this.hasCoverArt = true;
                    }
                    if (this.myAdvancedOptions.aaDirectShow)
                    {
                        this.SetText("Dumping PCM from filter...");
                        this.decryptAA(data.aaFile, data.wavFile, data.totalSeconds);
                        flag2 = true;
                    }
                    else if (this.myAudible.codec == "mp332")
                    {
                        if (!this.M4Boutput)
                        {
                            this.SetText("Decrypting original MP3...");
                            if (this.IsXPMode())
                            {
                                if (!this.VerifyAudibleManagerInstallation(false))
                                    break;
                                this.SetText("This is a 32bit OS, decrypting with Audible Manager...");
                                str1 = this.decryptMP3fromAA(data.aaFile, data.wavFile, data.totalSeconds, true);
                            }
                            else if (!flag1)
                                str1 = this.decryptMP3fromAA(data.aaFile, data.wavFile, data.totalSeconds, false);
                            else
                                startTrackNum = this.QuickSplitAA(data.aaFile, data.wavFile, startTrackNum);
                            myVirtualWav.advancedOptions = this.myAdvancedOptions;
                            myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                            myVirtualWav.aax2wavPath = this.supportLibs.aax2wavPath;
                            myVirtualWav.sampleRate = 22050;
                            myVirtualWav.channels = 1;
                            myVirtualWav.AACtoWAV(str1);
                            myVirtualWav.totalSeconds = data.totalSeconds;
                        }
                    }
                    else
                    {
                        this.SetText("Decrypted content is ACELP.  Converting to WAV...");
                        this.decryptAA3(data.aaFile, data.wavFile, data.totalSeconds);
                        flag2 = true;
                    }
                    this.bgwAA.ReportProgress(0);
                    if (this.chkVerifyAudibleSplits.Checked && !flag1)
                    {
                        this.SetText("Verifying split points...");
                        if (flag2)
                        {
                            for (int pos = 1; pos < this.myChapters.Count(); ++pos)
                            {
                                try
                                {
                                    double val = this.VerifyAAChapterSplit(this.myChapters.GetChapterDouble(pos), data.wavFile);
                                    this.SetText((pos + 1).ToString() + " / " + (object)(this.myChapters.Count() - 1) + " offset by " + string.Format("{0:0.0}", (object)(val - this.myChapters.GetChapterDouble(pos))) + "s");
                                    this.myChapters.SetChapter(pos, val);
                                    this.bgwAA.ReportProgress((int)((double)pos / (double)(this.myChapters.Count() - 1) * 100.0));
                                }
                                catch (Exception ex)
                                {
                                    Audible.diskLogger("WARNING-Something bad happened - " + ex.ToString());
                                }
                            }
                        }
                        else
                        {
                            for (int pos = 1; pos < this.myChapters.Count() - 1; ++pos)
                            {
                                try
                                {
                                    this.myAdvancedOptions.doNotVerifyIfSilence = false;
                                    myVirtualWav.advancedOptions = this.myAdvancedOptions;
                                    double val = this.VerifyChapterSplit(this.myChapters.GetChapterDouble(pos), myVirtualWav);
                                    this.SetText((pos + 1).ToString() + " / " + (object)(this.myChapters.Count() - 1) + " offset by " + string.Format("{0:0.0}", (object)(val - this.myChapters.GetChapterDouble(pos))) + "s");
                                    this.myChapters.SetChapter(pos, val);
                                    this.bgwAA.ReportProgress((int)((double)pos / (double)(this.myChapters.Count() - 2) * 100.0));
                                }
                                catch (Exception ex)
                                {
                                    Audible.diskLogger("WARNING-Something bad happened - " + ex.ToString());
                                }
                            }
                        }
                    }
                    if (this.chkRemoveAudibleMarkers.Checked && !flag1)
                    {
                        this.SetText("Removing \"This is Audible, etc..\"");
                        if (flag2)
                        {
                            this.TrimAudibleClipsFromAA(data);
                        }
                        else
                        {
                            myVirtualWav.soxPath = this.supportLibs.soxPath;
                            double[] numArray = new double[2];
                            this.myChapters.SetChapter(0, myVirtualWav.RemoveAudibleMarkers()[0]);
                        }
                    }
                    if (this.WAVoutput)
                    {
                        if (!flag2)
                            this.MP32WAV(str1, data.wavFile, true);
                        this.SetText("Completed.");
                        break;
                    }
                    if (!flag1 && (this.MP3output || this.GetSelectedCodec() == "lossless"))
                    {
                        if (this.chkFileSplitting.Checked || this.audibleChapters)
                        {
                            if (flag2)
                                this.soxSplit(data.wavFile, this.txtSplitThreshold.Text, data.lameOptions);
                            else
                                startTrackNum = this.splitMP3byAudible(str1, Path.GetDirectoryName(this.outputFileMask), startTrackNum);
                        }
                        else
                        {
                            if (!flag2)
                            {
                                this.SetText("Tagging...");
                                this.TaglibAndRenameMP3(str1, data.mp3File, 1);
                            }
                            else
                            {
                                this.MP32WAV(str1, data.wavFile, true);
                                this.encodeMP3(data.wavFile, data.mp3File, 1, data.lameOptions);
                            }
                            if (this.chkMP3chapterTags.Checked)
                            {
                                if (this.myChapters.Count() == 0)
                                    this.myChapters.SetDoubleChapters(this.GetAAChapters(strArray1[index]));
                                Utilities utilities = new Utilities();
                                utilities.StopwatchStart();
                                string[] strArray3 = audibleTotalTime.Split(':');
                                double totalSeconds2 = double.Parse(strArray3[0]) * 60.0 * 60.0 + double.Parse(strArray3[1]) * 60.0 + double.Parse(strArray3[2]);
                                TagLib.File file = TagLib.File.Create(data.mp3File);
                                TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag)file.GetTag(TagTypes.Id3v2, false);
                                if (tag != null)
                                    tag.Version = (byte)3;
                                UserTextInformationFrame informationFrame1 = new UserTextInformationFrame("UFID", StringType.UTF16);
                                string cuEfromChapters = this.myAudible.getCUEfromChapters(this.myChapters, "", "MP3", totalSeconds2);
                                UserTextInformationFrame informationFrame2 = new UserTextInformationFrame("CUESHEET", StringType.UTF16);
                                informationFrame2.Text = new string[1]
                                {
                  cuEfromChapters
                                };
                                tag.AddFrame((Frame)informationFrame2);
                                file.Save();
                                utilities.StopwatchStop();
                                Audible.diskLogger(utilities.StopwatchGetElapsed("chapter tag time"));
                            }
                        }
                    }
                    else if (this.M4Boutput)
                    {
                        this.SetText("Transcoding AA to AAC...");
                        data.wavFile = data.wavFile.Replace(".wav", ".m4a");
                        this.MP32FDK(data.aaFile, data.wavFile, data.totalSeconds);
                        data.splitPosition += this.myChapters.GetDoubleList().Count;
                        this.TagAndChapterFfmpeg(data.wavFile);
                        string str2 = data.wavFile.Replace(".m4a", ".m4b");
                        if (this.outputFileName != data.wavFile)
                            System.IO.File.Move(this.outputFileName, str2);
                        if (this.chkAudibleSplit.Checked)
                        {
                            this.myChapters.UpdateDoubleChapters(this.fixChapterOffsets(this.myChapters.GetDoubleList()));
                            this.SplitM4BbyAudible(str2, totalSeconds1, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.SetText("WARNING-Crash in main decryption loop - " + ex.ToString());
            }
        }

        private int GetBitrate()
        {
            string val = "";
            int bitrate = 64;
            if (this.cmbBitrate.InvokeRequired)
            {
                this.Invoke((System.Action)(() =>
               {
                   val = this.cmbBitrate.Text;
                   try
                   {
                       bitrate = int.Parse(val);
                   }
                   catch
                   {
                   }
               }));
            }
            else
            {
                try
                {
                    val = this.cmbBitrate.SelectedItem.ToString();
                }
                catch
                {
                    val = this.cmbBitrate.Text;
                }
                try
                {
                    bitrate = int.Parse(val);
                }
                catch
                {
                }
            }
            return bitrate;
        }

        private bool IsXPMode()
        {
            try
            {
                return !Form1.InternalCheckIsWow64();
            }
            catch
            {
                return false;
            }
        }

        private void MP32WAV(string tempMp3File, string wavFile, bool cleanup = false)
        {
            this.SetText("Dumping MP3 to WAV...");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = string.Format(" -y -i \"" + tempMp3File + "\" \"" + wavFile + "\"");
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            if (!cleanup)
                return;
            try
            {
                System.IO.File.Delete(tempMp3File);
            }
            catch
            {
            }
        }

        private void decryptAA2(string aaFile, string wavFile, double totalTime)
        {
            VirtualWAV myVirtualWav = new VirtualWAV();
            myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            myVirtualWav.aax2wavPath = this.supportLibs.aax2wavPath;
            myVirtualWav.sampleRate = 22050;
            myVirtualWav.channels = 1;
            Thread thread = new Thread((ThreadStart)(() => myVirtualWav.DecryptAAX(aaFile, wavFile, this.myAdvancedOptions.AudibleMangerDLLPath)));
            thread.Start();
            while (thread.IsAlive)
            {
                try
                {
                    if (System.IO.File.Exists(wavFile))
                    {
                        long length = new FileInfo(wavFile).Length;
                        Thread.Sleep(500);
                        long num = (long)((double)myVirtualWav.sampleRate * (double)myVirtualWav.channels * 2.0 * totalTime);
                        this.bgwAA.ReportProgress((int)((double)length / (double)num * 100.0));
                    }
                }
                catch
                {
                }
            }
        }

        private void decryptAA3(string aaFile, string wavFile, double totalTime)
        {
            VirtualWAV virtualWav = new VirtualWAV();
            virtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            virtualWav.aax2wavPath = this.supportLibs.aax2wavPath;
            virtualWav.sampleRate = 22050;
            virtualWav.channels = 1;
            Thread thread = new Thread((ThreadStart)(() =>
           {
               Process process = new Process();
               process.StartInfo = new ProcessStartInfo();
               process.StartInfo.FileName = this.supportLibs.ffmpegPath;
               process.StartInfo.Arguments = "-loglevel panic -y -i \"" + aaFile + "\" \"" + wavFile + "\"";
               process.StartInfo.UseShellExecute = false;
               process.StartInfo.RedirectStandardInput = true;
               process.StartInfo.CreateNoWindow = true;
               process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
               process.Start();
               process.WaitForExit();
           }));
            thread.Start();
            while (thread.IsAlive)
            {
                try
                {
                    if (System.IO.File.Exists(wavFile))
                    {
                        long length = new FileInfo(wavFile).Length;
                        Thread.Sleep(500);
                        long num = (long)((double)virtualWav.sampleRate * (double)virtualWav.channels * 2.0 * totalTime);
                        this.bgwAA.ReportProgress((int)((double)length / (double)num * 100.0));
                    }
                }
                catch
                {
                }
            }
        }

        private string decryptMP3fromAA(string aaFile, string mp3File, double totalTime, bool XPMode)
        {
            VirtualWAV myVirtualWav = new VirtualWAV();
            myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            myVirtualWav.aax2wavPath = this.supportLibs.instaripPath;
            myVirtualWav.ngPath = this.supportLibs.ngPath;
            myVirtualWav.sampleRate = 22050;
            myVirtualWav.channels = 1;
            string wFile = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".inAudible.mp3";
            Thread thread = XPMode ? new Thread((ThreadStart)(() => myVirtualWav.DecryptAAX(aaFile, wFile, this.myAdvancedOptions.AudibleMangerDLLPath))) : new Thread((ThreadStart)(() => myVirtualWav.DecryptAA(aaFile, wFile)));
            thread.Start();
            while (thread.IsAlive)
            {
                try
                {
                    if (System.IO.File.Exists(wFile))
                    {
                        long length1 = new FileInfo(wFile).Length;
                        Thread.Sleep(500);
                        long length2 = new FileInfo(aaFile).Length;
                        this.bgwAA.ReportProgress((int)((double)length1 / (double)length2 * 100.0));
                    }
                }
                catch
                {
                }
            }
            return wFile;
        }

        private void MP32FDK(string inputFile, string outputFile, double totalTime)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(outputFile);
            int num = this.GetBitrate() * 1000;
            string str = "\"" + this.supportLibs.ffmpegPath + "\" -i \"" + inputFile + "\" -f wav - | \"" + Path.GetDirectoryName(this.supportLibs.ffmpegPath) + "\\fdkaac.exe\" --ignorelength --profile 5 --bitrate " + (object)num + " -o \"" + outputFile + "\"  -";
            process.StartInfo.Arguments = string.Format("/C \"" + str + "\"");
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            while (!process.HasExited)
            {
                try
                {
                    if (System.IO.File.Exists(outputFile))
                    {
                        long length1 = new FileInfo(outputFile).Length;
                        Thread.Sleep(500);
                        long length2 = new FileInfo(inputFile).Length;
                        this.bgwAA.ReportProgress((int)((double)length1 / (double)length2 * 100.0));
                    }
                }
                catch
                {
                }
            }
        }

        private double TrimAudibleClipsFromAA(DecryptAAOptions data)
        {
            this.SetText("Removing \"This is Audible\", etc...");
            VirtualWAV virtualWav = new VirtualWAV();
            virtualWav.channels = 1;
            virtualWav.sampleRate = 22050;
            SourcePCM wav = new SourcePCM();
            wav.AddWAV(data.wavFile);
            virtualWav.AddWAV(wav);
            virtualWav.soxPath = this.supportLibs.soxPath;
            double[] numArray = virtualWav.RemoveAudibleMarkers();
            numArray[1] -= 0.5;
            for (int pos = 1; pos < this.myChapters.Count(); ++pos)
            {
                double val = this.myChapters.GetChapterDouble(pos) - numArray[0];
                this.myChapters.SetChapter(pos, val);
            }
            string sourceFileName = Path.GetDirectoryName(data.wavFile) + "\\trimmed.wav";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = string.Format(" -y -i \"" + data.wavFile + "\" -acodec copy -t " + (numArray[1] - numArray[0]).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " -ss " + numArray[0].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " \"" + sourceFileName + "\"");
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            System.IO.File.Delete(data.wavFile);
            System.IO.File.Move(sourceFileName, data.wavFile);
            return numArray[0];
        }

        private void bgwAA_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(this.startTime);
            long totalSeconds = (long)timeSpan.TotalSeconds;
            this.SetText("Completed in " + timeSpan.Hours.ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2"));
            if (this.myAudible.coverPath != "")
            {
                System.IO.File.Copy(this.myAudible.coverPath, Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".jpg", true);
                this.myCoverArt.Cleanup();
            }
            try
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            catch (Exception ex)
            {
                Audible.diskLogger("WindowsAPICodePack threw an error: " + ex.ToString());
            }
            int num = 0;
            try
            {
                num = (int)(this.totalSecondsProcessed / totalSeconds);
            }
            catch
            {
            }
            this.SetText("Speedup is " + (object)num + "x realtime.");
            this.RenameFiles();
            this.SetText("Done!");
            this.SetEncodeMode();
        }

        private void bgwAdjust_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            this.SetEncodeMode();
        }

        private void bgwAAX_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            this.finishedBatch = true;
            TimeSpan timeSpan = DateTime.Now.Subtract(this.startTime);
            long totalSeconds = (long)timeSpan.TotalSeconds;
            this.SetText("Completed in " + ((int)Math.Floor(timeSpan.TotalHours)).ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2"));
            if (timeSpan.TotalDays > 1.0)
                this.SetText("(Holy crap! That's " + (object)(int)Math.Floor(timeSpan.TotalDays) + " days!)");
            try
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            catch (Exception ex)
            {
                Audible.diskLogger("WindowsAPICodePack threw an error: " + ex.ToString());
            }
            try
            {
                int num = (int)(this.totalSecondsProcessed / totalSeconds);
                Audible.diskLogger("speedup ratio = " + (object)this.totalSecondsProcessed + " / " + (object)totalSeconds);
                this.SetText("Speedup is " + (object)num + "x realtime.");
                this.SetURL("Done: ", "Output Files", !this.myBatchFiles.IsBatch() ? Path.GetDirectoryName(this.outputFileName) : Path.GetDirectoryName(this.txtOutputFile.Text));
            }
            catch
            {
            }
            this.SetEncodeMode();
            this.DoCompletionEvent();
            if (this.myAdvancedOptions.beep && !this.myAdvancedOptions.cylon) this.SOXPlay(Form1.appPath + "\\beep.mp3", true);
            else if (myAdvancedOptions.cylon) SOXPlay(appPath + "\\inAudible-end.mp3", true);
            _conversionCompleteAction?.Invoke(outputFileName);
        }

        private void DoCompletionEvent()
        {
            if (this.myAdvancedOptions.completion == null || this.myAdvancedOptions.completion == "")
                return;
            if (this.myAdvancedOptions.completion == "hibernate")
            {
                this.SetText("Hibernating...");
                Application.SetSuspendState(PowerState.Hibernate, true, true);
            }
            else if (this.myAdvancedOptions.completion == "sleep")
            {
                this.SetText("Sleeping...");
                Application.SetSuspendState(PowerState.Suspend, true, true);
            }
            else
            {
                if (!(this.myAdvancedOptions.completion == "shutdown"))
                    return;
                this.SetText("Shutting down...");
                this.Shutdown();
            }
        }

        private void Shutdown()
        {
            ManagementClass managementClass = new ManagementClass("Win32_OperatingSystem");
            managementClass.Get();
            managementClass.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject methodParameters = managementClass.GetMethodParameters("Win32Shutdown");
            methodParameters["Flags"] = (object)"1";
            methodParameters["Reserved"] = (object)"0";
            foreach (ManagementObject instance in managementClass.GetInstances())
                instance.InvokeMethod("Win32Shutdown", methodParameters, (InvokeMethodOptions)null);
        }

        private bool LeadingZeroes()
        {
            return this.chkChangeFileNumbering.Checked && (this.chkM4Bsplit.Checked || this.chkAudibleSplit.Checked || this.chkFileSplitting.Checked);
        }

        private void RenameFiles()
        {
            if (!this.chkChangeFileNumbering.Checked || !this.chkM4Bsplit.Checked && !this.chkAudibleSplit.Checked && !this.chkFileSplitting.Checked)
                return;
            string directoryName = Path.GetDirectoryName(this.outputFileMask);
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
            int num1 = 1;
            bool flag1 = false;
            foreach (FileSystemInfo file in new DirectoryInfo(directoryName).GetFiles("*.m3u"))
                file.Delete();
            FileInfo[] files = directoryInfo.GetFiles("*.*");
            if (!this.aaxMode)
            {
                foreach (FileSystemInfo fileSystemInfo in files)
                {
                    if (fileSystemInfo.FullName.Contains("- 001 - part 001"))
                        flag1 = true;
                }
            }
            foreach (FileInfo fileInfo in files)
            {
                bool flag2 = false;
                int num2 = fileInfo.FullName.LastIndexOf(" - ");
                if (num2 > -1)
                {
                    string extension = fileInfo.Extension;
                    string path1 = fileInfo.FullName.Substring(0, num2);
                    string path2 = fileInfo.FullName.Substring(num2, fileInfo.FullName.Length - num2);
                    string str1;
                    if (this.aaxMode)
                    {
                        int result = 0;
                        if (int.TryParse(path1.Split('\\')[path1.Split('\\').Length - 1], out result))
                            continue;
                        str1 = Path.GetFileNameWithoutExtension(path2).Replace("-", "").Trim();
                    }
                    else if (flag1)
                    {
                        str1 = num1.ToString("D3");
                        int length = path1.LastIndexOf(" - ");
                        if (length > -1)
                            path1 = path1.Substring(0, length);
                        else
                            continue;
                    }
                    else
                        str1 = Path.GetFileNameWithoutExtension(path2).Replace("- part", "").Trim();
                    string fileName = Path.GetFileName(path1);
                    ++num1;
                    if (!flag2)
                    {
                        string str2 = directoryName + "\\" + str1 + " - " + fileName + extension;
                        try
                        {
                            System.IO.File.Delete(str2);
                        }
                        catch
                        {
                        }
                        System.IO.File.Move(fileInfo.FullName, str2);
                    }
                }
            }
        }

        private void SetEncodeMode()
        {
            this.btnInputFile.Enabled = !this.btnInputFile.Enabled;
            if (this.myBatchFiles.IsBatch())
                this.btnBatchEdit.Enabled = !this.btnBatchEdit.Enabled;
            this.btnCDrip.Enabled = !this.btnCDrip.Enabled;
            this.btnOutputFile.Enabled = !this.btnOutputFile.Enabled;
            try
            {
                this.grpOutputType.Enabled = !this.grpOutputType.Enabled;
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
            }
            this.grpITunes.Enabled = !this.grpITunes.Enabled;
            this.btnSplitter.Enabled = !this.btnSplitter.Enabled;
            this.btnConvert.Enabled = !this.btnConvert.Enabled;
            bool flag = false;
            if (this.GetSelectedCodec() == "lossless")
            {
                flag = true;
                this.grpLame.Enabled = !this.grpLame.Enabled;
            }
            if ((this.MP3output || this.oggOutput || (this.opusOutput || this.M4Boutput)) && !flag)
                this.grpLame.Enabled = !this.grpLame.Enabled;
            int num = this.M4Boutput ? 1 : 0;
            if (!this.FLACoutput)
                this.grpSplitting.Enabled = !this.grpSplitting.Enabled;
            this.grpOutputOptions.Enabled = !this.grpOutputOptions.Enabled;
            this.btnOutputOptions.Enabled = !this.btnOutputOptions.Enabled;
        }

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                try
                {
                    control.Invoke((Delegate)new Form1.SetControlPropertyThreadSafeDelegate(Form1.SetControlPropertyThreadSafe), (object)control, (object)propertyName, propertyValue);
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, (Binder)null, (object)control, new object[1]
                    {
            propertyValue
                    });
                }
                catch
                {
                }
            }
        }

        private M4BOptions getM4Boptions()
        {
            M4BOptions m4Boptions = new M4BOptions();
            int num1 = 1000;
            if (this.GetSelectedCodec() == "fdk" && this.myAdvancedOptions.fdk)
                num1 = 1;
            int num2 = this.GetBitrate() * num1;
            this.myAudible.nfo.targetBitrate = (num2 / num1).ToString();
            m4Boptions.bitrate = num2;
            if (this.rdVBR.Checked)
            {
                m4Boptions.vbr = true;
                m4Boptions.quality = double.Parse(this.lblVBRquality.Text);
                this.myAudible.nfo.vbrValue = this.lblVBRquality.Text;
                this.myAudible.nfo.vbr = true;
            }
            else
                this.myAudible.nfo.vbr = false;
            return m4Boptions;
        }

        private void splitWAVbyAudible(string tmpWAVfile, string outputDir)
        {
            this.SetText("Splitting WAV using Audible chapters...");
            string str1 = "split\\";
            Directory.CreateDirectory(outputDir + "\\" + str1);
            tmpWAVfile = tmpWAVfile.Replace(outputDir + "\\", "");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.WorkingDirectory = outputDir;
            string str2 = "";
            List<double> doubleList = this.myChapters.GetDoubleList();
            for (int index = 0; index < doubleList.Count; ++index)
            {
                if (doubleList.Count != index + 1)
                    str2 = str2 + " -acodec copy -t " + (doubleList[index + 1] - doubleList[index]).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " -ss " + doubleList[index].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " " + str1 + "s" + (index + 1).ToString("D2") + ".wav";
                else
                    str2 = str2 + " -acodec copy -ss " + doubleList[index].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " " + str1 + "s" + (index + 1).ToString("D2") + ".wav";
            }
            process.StartInfo.Arguments = string.Format("/C \"\"" + this.supportLibs.soxPath + "\" {0} -t raw - | \"{1}\" -y -f s16le -ac 2 -ar 44100 -i pipe:0 {2}\"", (object)tmpWAVfile, (object)this.supportLibs.ffmpegPath, (object)str2);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                this.SetText("WARNING-SOX failed!");
                this.SetText("EAA-" + process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            }
            else
                this.removeMergedWAVfiles();
        }

        private void multithreadMP3encode(string tmpWAVfile, string outputDir, bool pad, DecryptAAXOptions data)
        {
            string path = outputDir + "\\split\\";
            int length = Directory.GetFiles(path).Length;
            Thread[] threadArray = new Thread[length];
            if (pad)
            {
                this.SetText("Padding...");
                foreach (string padFile in (IEnumerable<string>)((IEnumerable<string>)Directory.GetFiles(path)).OrderBy<string, string>((System.Func<string, string>)(f => f)))
                    this.soxPad(padFile);
            }
            string lameArgs = data.lameOptions;
            this.SetText("Encoding...");
            int num1 = 1;
            int index1 = 0;
            Directory.GetFiles(path);
            foreach (string tmpWAVfile1 in (IEnumerable<string>)((IEnumerable<string>)Directory.GetFiles(path)).OrderBy<string, string>((System.Func<string, string>)(f => f)))
            {
                string mp3File = Path.GetDirectoryName(tmpWAVfile) + "\\" + Path.GetFileNameWithoutExtension(tmpWAVfile) + " - part " + num1.ToString("D3") + ".mp3";
                if (this.multithreading)
                {
                    string tFile = tmpWAVfile1;
                    string tMp3File = mp3File;
                    string sPiece = num1.ToString();
                    threadArray[index1] = new Thread((ThreadStart)(() => this.encodeMP3multi(tFile, tMp3File, Form1.appPath, false, lameArgs, int.Parse(sPiece), this.myAudible)));
                    threadArray[index1].Name = "Encoder" + (object)index1;
                    threadArray[index1].Start();
                    ++index1;
                }
                else
                {
                    this.SetText((index1 + 1).ToString() + " / " + (object)length);
                    Application.DoEvents();
                    this.encodeMP3(tmpWAVfile1, mp3File, num1 - 1, lameArgs);
                }
                this.m3u = this.m3u + Path.GetFileNameWithoutExtension(tmpWAVfile) + " - part " + num1.ToString("D3") + ".mp3\r\n";
                ++num1;
            }
            if (this.multithreading)
            {
                bool flag = true;
                int num2 = 0;
                while (flag)
                {
                    Thread.Sleep(500);
                    int num3 = 0;
                    for (int index2 = 0; index2 < length; ++index2)
                    {
                        if (!threadArray[index2].IsAlive)
                        {
                            ++num3;
                            if (num2 < num3)
                            {
                                this.SetText(num3.ToString() + " / " + (object)length);
                                this.bgwAA.ReportProgress((int)((double)num3 / (double)length * 100.0));
                                num2 = num3;
                                Application.DoEvents();
                            }
                            if (num3 == length)
                                flag = false;
                        }
                    }
                }
                Application.DoEvents();
            }
            Directory.Delete(path, true);
        }

        private void encodeAAXmp4(string[] mergedWAVfiles, string outputFile, int bitrate)
        {
            string str = "";
            foreach (string mergedWaVfile in mergedWAVfiles)
                str = str + " -if \"" + mergedWaVfile + "\"";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.neroAACpath;
            process.StartInfo.Arguments = string.Format("-ignorelength -br {0} {1} -of \"{2}\"", (object)bitrate, (object)str, (object)outputFile);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

        private void SOXPlay(string file, bool background)
        {
            if (this.musicProcess != null && this.endingMusicThread.IsAlive)
                this.musicProcess.Kill();
            this.musicProcess = new Process();
            this.musicProcess.StartInfo = new ProcessStartInfo();
            this.musicProcess.StartInfo.FileName = this.supportLibs.soxPath;
            this.musicProcess.StartInfo.Arguments = "\"" + file + "\" -d";
            this.musicProcess.StartInfo.CreateNoWindow = true;
            this.musicProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            this.musicProcess.StartInfo.UseShellExecute = false;
            this.musicProcess.Start();
            if (background)
            {
                this.endingMusicThread = new Thread((ThreadStart)(() => this.musicProcess.WaitForExit()));
                this.endingMusicThread.Start();
            }
            else
                this.musicProcess.WaitForExit();
        }

        private void splitMP3bySilence(string tmpWAVfile, string outputDir, string threshold)
        {
            this.SetText("Splitting WAV using silence detection...");
            string path = outputDir + "\\split\\";
            Directory.CreateDirectory(path);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = string.Format("/C \"\"" + this.supportLibs.soxPath + "\" {0} -t raw - | \"{1}\" -t raw -b 16 -e signed -c 2 -r 44100 - \"{2}out - .wav\" --show-progress silence 1 0 1% 1 {3} 1% : newfile : restart \"", (object)tmpWAVfile, (object)this.supportLibs.soxPath, (object)path, (object)double.Parse(threshold));
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                this.SetText("WARNING-SOX failed!");
                this.SetText("EAA-" + process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            }
            else
                this.removeMergedWAVfiles();
        }

        private void splitMP3byAudible(string outputDir)
        {
            this.SetText("Splitting MP3 by Audible Chapters...");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.WorkingDirectory = outputDir;
            string str = "";
            List<double> doubleList = this.myChapters.GetDoubleList();
            for (int index = 0; index < doubleList.Count; ++index)
            {
                if (doubleList.Count != index + 1)
                    str = str + " -acodec copy -t " + (doubleList[index + 1] - doubleList[index]).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " -ss " + doubleList[index].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " s-" + (index + 1).ToString("D3") + ".mp3";
                else
                    str = str + " -acodec copy -ss " + doubleList[index].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " s-" + (index + 1).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + ".mp3";
            }
            process.StartInfo.Arguments = string.Format("-y -i \"{0}\" {1}", (object)this.outputFileMask, (object)str);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                this.SetText("WARNING-ffmpeg failed!");
                this.SetText("EAA-" + process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            }
            else
            {
                if (!this.chkKeepMonolithicMP3.Checked)
                    System.IO.File.Delete(this.outputFileMask);
                string withoutExtension = Path.GetFileNameWithoutExtension(this.outputFileMask);
                FileInfo[] files = new DirectoryInfo(outputDir).GetFiles("s-*.mp3");
                int trackNum = 1;
                foreach (FileInfo fileInfo in files)
                {
                    this.tagAndRenameMP3(fileInfo.FullName, fileInfo.FullName.ToString().Replace("s-", withoutExtension + " - "), trackNum);
                    ++trackNum;
                }
            }
        }

        private int RenameAANGOutput(string mp3File, string outputDir, int startTrackNum)
        {
            string withoutExtension = Path.GetFileNameWithoutExtension(this.outputFileMask);
            IOrderedEnumerable<FileInfo> orderedEnumerable = ((IEnumerable<FileInfo>)new DirectoryInfo(outputDir).GetFiles("s-*.wav")).OrderBy<FileInfo, DateTime>((System.Func<FileInfo, DateTime>)(f => f.CreationTime));
            int trackNum = startTrackNum;
            if (trackNum == 1)
                trackNum = 0;
            foreach (FileInfo fileInfo in (IEnumerable<FileInfo>)orderedEnumerable)
            {
                int.Parse(fileInfo.Name.Split('.')[0].Split('-')[2]);
                this.tagAndRenameMP3(fileInfo.FullName, Path.GetDirectoryName(fileInfo.FullName) + "\\" + withoutExtension + " - " + trackNum.ToString("D3") + ".mp3", trackNum);
                ++trackNum;
            }
            return trackNum;
        }

        private int splitMP3byAudible(string mp3File, string outputDir, int startTrackNum)
        {
            this.SetText("Splitting MP3 by Audible Chapters...");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.WorkingDirectory = outputDir;
            string str = "";
            List<double> doubleList = this.myChapters.GetDoubleList();
            for (int index = 0; index < doubleList.Count; ++index)
            {
                if (doubleList.Count != index + 1)
                {
                    double num1 = Math.Truncate((doubleList[index + 1] - doubleList[index]) * 100.0) / 100.0;
                    double num2 = Math.Truncate(doubleList[index] * 100.0) / 100.0;
                    str = str + " -acodec copy -t " + num1.ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " -ss " + num2.ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " s-" + (index + 1).ToString("D3") + ".mp3";
                }
                else
                    str = str + " -acodec copy -ss " + doubleList[index].ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " s-" + (index + 1).ToString("D3") + ".mp3";
            }
            process.StartInfo.Arguments = string.Format("-y -i \"{0}\" {1}", (object)mp3File, (object)str);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                this.SetText("WARNING-ffmpeg failed!");
                this.SetText("EAA-" + process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
                return 0;
            }
            if (!this.chkKeepMonolithicMP3.Checked)
                System.IO.File.Delete(mp3File);
            string withoutExtension = Path.GetFileNameWithoutExtension(this.outputFileMask);
            IOrderedEnumerable<FileInfo> orderedEnumerable = ((IEnumerable<FileInfo>)new DirectoryInfo(outputDir).GetFiles("s-*.mp3")).OrderBy<FileInfo, string>((System.Func<FileInfo, string>)(f => f.Name));
            int trackNum = startTrackNum;
            foreach (FileInfo fileInfo in (IEnumerable<FileInfo>)orderedEnumerable)
            {
                int.Parse(fileInfo.Name.Split('.')[0].Split('-')[1]);
                this.tagAndRenameMP3(fileInfo.FullName, Path.GetDirectoryName(fileInfo.FullName) + "\\" + withoutExtension + " - " + trackNum.ToString("D3") + ".mp3", trackNum);
                ++trackNum;
            }
            return trackNum;
        }

        private void TaglibAndRenameMP3(string inputFile, string outputFile, int trackNum)
        {
            this.myCoverArt.GetImage(Form1.thumbnailDir);
            string str1 = "Narrated by " + this.myAudible.narrator + ". " + this.myAudible.GetComments();
            string str2 = this.myAudible.title;
            if (this.myAudible.addTrackToTitle)
                str2 = str2 + " - " + trackNum.ToString("D3");
            TagLib.File file = TagLib.File.Create(inputFile);
            file.Tag.Title = str2;
            file.Tag.Composers = new string[1]
            {
        this.myAudible.narrator
            };
            file.Tag.Performers = new string[1]
            {
        this.myAudible.author
            };
            file.Tag.Album = this.myAudible.album;
            try
            {
                file.Tag.Year = uint.Parse(this.myAudible.year.ToString());
            }
            catch
            {
            }
            file.Tag.Track = (uint)trackNum;
            file.Tag.Comment = str1;
            if (this.chkEmbedCover.Checked && this.hasCoverArt)
            {
                this.myCoverArt.GetImage(Form1.thumbnailDir);
                IPicture picture = (IPicture)new Picture(this.myAudible.coverPath);
                picture.Description = "Cover Art";
                file.Tag.Pictures = new IPicture[1] { picture };
            }
            file.Save();
            try
            {
                System.IO.File.Delete(outputFile);
                System.IO.File.Move(inputFile, outputFile);
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
            }
        }

        private void tagAndRenameMP3(string inputFile, string outputFile, int trackNum)
        {
            this.myCoverArt.GetImage(Form1.thumbnailDir);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            bool flag = false;
            if (inputFile == outputFile)
            {
                flag = true;
                outputFile = Path.GetDirectoryName(outputFile) + "\\" + Path.GetFileNameWithoutExtension(outputFile) + "-tmp.mp3";
            }
            string str1 = "Narrated by " + this.myAudible.narrator + ". " + this.myAudible.GetComments();
            string str2 = this.myAudible.title;
            if (this.myAudible.addTrackToTitle)
                str2 = str2 + " - " + trackNum.ToString("D3");
            if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                process.StartInfo.Arguments = string.Format("-y -i \"{0}\" -id3v2_version 3 -write_id3v1 1 -metadata title=\"{2}\" -metadata artist=\"{3}\" -metadata album=\"{4}\" -metadata date=\"{5}\" -metadata track=\"{6}\" -metadata comment=\"{7}\" -acodec copy \"{1}\"", (object)inputFile, (object)outputFile, (object)str2, (object)this.myAudible.author, (object)this.myAudible.album, (object)this.myAudible.year, (object)trackNum, (object)str1);
            else if (this.chkEmbedCover.Checked && this.hasCoverArt)
            {
                string str3 = Form1.thumbnailDir + "\\inAudibleCover.jpg";
                process.StartInfo.Arguments = string.Format("-y -i \"{0}\" -i \"{7}\" -map 0:0 -map 1:0 -c copy -id3v2_version 3 -metadata:s:v title=\"Album cover\" -metadata:s:v comment=\"Cover (Front)\" -write_id3v1 1 -metadata title=\"{2}\" -metadata artist=\"{3}\" -metadata album=\"{4}\" -metadata date=\"{5}\" -metadata track=\"{6}\" -metadata comment=\"{8}\" -acodec copy \"{1}\"", (object)inputFile, (object)outputFile, (object)str2, (object)this.myAudible.author, (object)this.myAudible.album, (object)this.myAudible.year, (object)trackNum, (object)this.myAudible.coverPath, (object)str1);
            }
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                this.SetText("WARNING-ffmpeg failed!");
                this.SetText("EAA-" + process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            }
            else
            {
                System.IO.File.Delete(inputFile);
                if (!flag)
                    return;
                System.IO.File.Move(outputFile, inputFile);
            }
        }

        private void createMonolithicMP3(string tmpWAVfile, DecryptAAXOptions data)
        {
            this.SetText("Creating monolithic MP3...");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = string.Format("/C \"\"" + this.supportLibs.soxPath + "\" {0} -t raw - | \"{1}\" - -r {2} --tt \"{4}\" --ta \"{5}\" --tc \"{6}\" --tl \"{7}\" \"{3}\" \"", (object)tmpWAVfile, (object)this.supportLibs.lamePath, (object)data.lameOptions, (object)this.outputFileMask, (object)this.myAudible.title, (object)this.myAudible.author, (object)("Narrated by: " + this.myAudible.narrator), (object)this.myAudible.title);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            Audible.diskLogger(process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                this.SetText("WARNING-LameSox failed!");
                this.SetText("EAA-" + process.StartInfo.FileName.ToString() + " " + process.StartInfo.Arguments.ToString());
            }
            else
                this.removeMergedWAVfiles();
        }

        private void removeMergedWAVfiles()
        {
            if (this.mergedWAVfiles == null || this.mergedWAVfiles.Length == 0)
                return;
            foreach (string mergedWaVfile in this.mergedWAVfiles)
                System.IO.File.Delete(mergedWaVfile);
        }

        private string decryptAAX(string file, string tmpWAVfile, double totalTime, DecryptAAXOptions data)
        {
            Audible.diskLogger("8.1 - Parsing slice size");
            double num1 = double.Parse(this.txtITunesPassSize.Text) * 60.0 * 60.0;
            double num2 = Math.Ceiling(totalTime / num1);
            double num3 = 1.0;
            string[] strArray = (string[])null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.threads = !this.chkMultithread.Checked ? 1 : Environment.ProcessorCount;
            bool flag1 = false;
            if (this.m4pMode && !this.reRun || !this.reRun && !this.studioProcessingMode && !this.myAdvancedOptions.decrypt)
            {
                flag1 = true;
                double cutStartTime = 0.0;
                while (cutStartTime < totalTime)
                {
                    iTunes iTunes = new iTunes();
                    iTunes.VCDinit();
                    string[] thisPassDirs = (string[])iTunes.initialDirs.Clone();
                    if (strArray == null)
                        strArray = iTunes.initialDirs;
                    this.SetText("Pass " + (object)num3 + " / " + (object)num2);
                    Audible.diskLogger("Start = " + (object)cutStartTime + ", End = " + (object)(cutStartTime + num1) + ", Total = " + (object)totalTime);
                    double endTime = cutStartTime + num1;
                    if (endTime > totalTime)
                        endTime = totalTime + 15.0;
                    switch (this.iTunesExternal(file, cutStartTime, endTime))
                    {
                        case -1:
                            iTunes.VerfyITunesPass(thisPassDirs, totalTime * 10.0);
                            return "aborted";
                        case -2:
                            iTunes.VerfyITunesPass(thisPassDirs, totalTime * 10.0);
                            return "nonWavMode";
                        default:
                            double passTime = num1;
                            if (cutStartTime + num1 > totalTime)
                                passTime = totalTime - (num3 - 1.0) * num1;
                            if (!iTunes.VerfyITunesPass(thisPassDirs, passTime))
                            {
                                this.SetText("Failed to verify this iTunes pass.  Retrying...");
                                cutStartTime -= num1;
                            }
                            else
                            {
                                this.bgwAA.ReportProgress((int)(num3 / num2 * 100.0));
                                ++num3;
                            }
                            cutStartTime += num1;
                            continue;
                    }
                }
            }
            else if (this.reRun)
            {
                strArray = this.savedInitialDirs;
                this.SetText("EAA-Re-run mode: skipping iTunes dump.");
            }
            stopwatch.Stop();
            TimeSpan elapsed1 = stopwatch.Elapsed;
            string str1 = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed1.Hours, (object)elapsed1.Minutes, (object)elapsed1.Seconds, (object)(elapsed1.Milliseconds / 10));
            if (flag1)
                this.SetText("iTunes processing completed in " + str1);
            Audible.diskLogger("8.2 - Done with init");
            bool flag2 = false;
            this.myAudible.nfo.lossless = false;
            if (this.IsCodecLossless() && this.M4Boutput)
            {
                flag2 = true;
                this.myAudible.nfo.lossless = true;
            }
            Audible.diskLogger("8.3 - iTunes init");
            iTunes myItunes2 = new iTunes();
            if (this.m4pMode || !this.studioProcessingMode && !this.myAdvancedOptions.decrypt)
                myItunes2.VCDinit();
            myItunes2.soxPath = this.supportLibs.soxPath;
            myItunes2.lamePath = this.supportLibs.lamePath;
            myItunes2.initialDirs = strArray;
            myItunes2.inAudibleTargetDir = Path.GetDirectoryName(tmpWAVfile);
            myItunes2.targetWAV = Path.GetFileName(tmpWAVfile);
            try
            {
                this.bgwAA.ReportProgress(0);
            }
            catch
            {
            }
            Audible.diskLogger("9 - Getting encoding options");
            EncodingOptions myEncodingOptions = this.GetEncodingOptions();
            stopwatch.Restart();
            List<string> workingDirs = (List<string>)null;
            if (this.m4pMode || !this.studioProcessingMode && !this.myAdvancedOptions.decrypt)
            {
                workingDirs = myItunes2.getWorkingDirectories();
                this.savedWorkingDirs = workingDirs;
                this.SetText("Constructing virtual WAV from " + (object)workingDirs.Count + " CD's...");
            }
            VirtualWAV myVirtualWav = new VirtualWAV();
            if (this.chkNormalize.Checked && this.cdProcessingMode && !this.omniMode)
            {
                this.SetText("Making VirtualWAV compatible with normalization...");
                this.omniMode = true;
                this.omniWAV.panicMode = false;
                this.omniWAV.supportLibs = this.supportLibs;
                this.omniWAV.ffmpegPath = this.supportLibs.ffmpegPath;
                this.omniWAV.ffprobePath = this.supportLibs.ffprobePath;
                this.omniWAV.ConstructCDWAV(this.txtInputFile.Text);
                this.omniWAV.audioFiles = new SourceAudio[this.omniWAV.GetTotalWavs().Count];
                for (int index = 0; index < this.omniWAV.GetTotalWavs().Count; ++index)
                {
                    SourceAudio sourceAudio = new SourceAudio(this.supportLibs);
                    sourceAudio.Add(this.omniWAV.GetTotalWavs()[index].fileName);
                    this.omniWAV.audioFiles[index] = sourceAudio;
                }
            }
            if (this.omniMode)
                myVirtualWav = (VirtualWAV)this.omniWAV.Clone();
            myVirtualWav.advancedOptions = this.myAdvancedOptions;
            bool flag3 = false;
            TimeSpan elapsed2;
            if (this.m4pMode || !this.studioProcessingMode && !this.myAdvancedOptions.decrypt)
            {
                if (this.m4pMode)
                    myVirtualWav.singleWavMode = true;
                Task task = (Task)Task.Factory.StartNew<bool>((System.Func<bool>)(() => myVirtualWav.ConstructVirtualWAV(workingDirs)));
                while (!task.IsCompleted)
                {
                    Thread.Sleep(1000);
                    this.bgwAA.ReportProgress(myVirtualWav.percentComplete);
                    flag3 = myVirtualWav.mergedSuccessfully;
                }
            }
            else if (!this.cdProcessingMode)
            {
                if (this.flacUnpacked)
                {
                    this.SetText("Virtualizing physical WAV...");
                    string str2 = Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(this.myAudible.decryptedFile) + ".wav";
                    this.GetSampleRateFromInput(str2, ref myVirtualWav);
                    myVirtualWav.Physical2VirtualWAV(str2);
                    flag3 = true;
                }
                else if (this.studioProcessingMode && !this.m4bTranscodeMode)
                {
                    this.SetText("Decoding...");
                    myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                    string tFile = Path.GetDirectoryName(file) + "\\" + Path.GetFileName(this.myAudible.decryptedFile);
                    this.GetSampleRateFromInput(tFile, ref myVirtualWav);
                    this.myAudible.decryptedFile = tFile;
                    flag3 = true;
                    string fileName = Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(this.myAudible.decryptedFile) + ".wav";
                    Thread thread = new Thread((ThreadStart)(() => myVirtualWav.ConstructStudioWAV(tFile)));
                    thread.Start();
                    this.UpdateProgressBar(0, "green");
                    while (thread.IsAlive)
                    {
                        try
                        {
                            long length = new FileInfo(fileName).Length;
                            Thread.Sleep(500);
                            double chapterDouble = this.myChapters.GetChapterDouble(this.myChapters.Count() - 1);
                            long num4 = (long)((double)myVirtualWav.sampleRate * (double)myVirtualWav.channels * 2.0 * chapterDouble);
                            int percentProgress = (int)((double)length / (double)num4 * 100.0);
                            Audible.diskLogger(length.ToString("N", (IFormatProvider)CultureInfo.InvariantCulture) + " / " + num4.ToString("N", (IFormatProvider)CultureInfo.InvariantCulture) + " = " + (object)percentProgress + "%");
                            this.bgwAA.ReportProgress(percentProgress);
                        }
                        catch
                        {
                        }
                    }
                }
                else if (this.m4bTranscodeMode)
                {
                    stopwatch.Restart();
                    myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                    myVirtualWav.aax2wavPath = this.supportLibs.aax2wavPath;
                    flag3 = true;
                    myVirtualWav.sampleRate = 22050;
                    myVirtualWav.channels = 2;
                    myVirtualWav.instarip = this.supportLibs.instaripPath;
                    string str2 = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".tmp.mp4";
                    this.SetText("Extracting clean AAC...");
                    this.CopyCleanMP4(file, str2);
                    if (!System.IO.File.Exists(str2))
                    {
                        this.SetText("Could not extract clean audio.  Trying to process directly...");
                        str2 = file;
                    }
                    myVirtualWav.aacMode = true;
                    myVirtualWav.M4BtoWAV(str2);
                    this.GetSampleRateFromInput(str2, ref myVirtualWav);
                    myVirtualWav.totalSeconds = totalTime;
                }
                else if (!this.m4pMode && this.myAdvancedOptions.decrypt && (!flag2 && !this.cdProcessingMode))
                {
                    stopwatch.Restart();
                    this.SetText("Decrypting AAC from AAX...");
                    string wFile = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".tmp.mp4";
                    if (!this.DecryptAacAudibleManager(myVirtualWav, file, wFile, totalTime))
                        return "aborted";
                    flag3 = true;
                    stopwatch.Stop();
                    elapsed2 = stopwatch.Elapsed;
                    this.SetText("Decrypted in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed2.Hours, (object)elapsed2.Minutes, (object)elapsed2.Seconds, (object)(elapsed2.Milliseconds / 10)));
                }
                else if (!this.m4pMode && this.myAdvancedOptions.decrypt && (flag2 && !this.cdProcessingMode))
                {
                    stopwatch.Restart();
                    Audible.diskLogger("10 - Decrypting AAC from AAX for True Decrypt");
                    this.SetText("Decrypting AAC from AAX...");
                    string wFile = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp4";
                    if (!this.DecryptAacAudibleManager(myVirtualWav, file, wFile, totalTime))
                        return "aborted";
                    Audible.diskLogger("11 - Decrypted successfully");
                    flag3 = true;
                    this.bgwAA.ReportProgress(100);
                    stopwatch.Stop();
                    elapsed2 = stopwatch.Elapsed;
                    this.SetText("Decrypted in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed2.Hours, (object)elapsed2.Minutes, (object)elapsed2.Seconds, (object)(elapsed2.Milliseconds / 10)));
                }
            }
            else
            {
                this.SetText("Constructing virtual WAV...");
                myVirtualWav.omni = this.omniMode;
                myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                myVirtualWav.ffprobePath = this.supportLibs.ffprobePath;
                myVirtualWav.supportLibs = this.supportLibs;
                if (!this.omniMode)
                    myVirtualWav.ConstructCDWAV(this.txtInputFile.Text);
                flag3 = true;
            }
            if (!flag3)
            {
                this.SetText("WARNING-Failed to merge VCD files.");
                foreach (string path in workingDirs)
                    Directory.Delete(path, true);
                return "aborted";
            }
            int num5 = myVirtualWav.GetTotalWavs().Count;
            if (this.omniMode)
                num5 = myVirtualWav.audioFiles.Length;
            this.SetText("Virtualized " + myVirtualWav.fileLength.ToString("#,##0") + " bytes, in " + (object)num5 + " files. Total time = " + myVirtualWav.GetFormattedTime());
            double[] numArray = new double[2];
            if (this.chkAutodetectChannels.Checked && !flag2)
            {
                myVirtualWav.soxPath = this.supportLibs.soxPath;
                this.SetText("Checking channels...");
                if (!myVirtualWav.IsMono())
                {
                    this.SetText("Detected STEREO");
                    myEncodingOptions.downmix = false;
                    myEncodingOptions.channels = 2;
                }
                else
                {
                    this.SetText("Detected MONO");
                    myEncodingOptions.downmix = false;
                    myEncodingOptions.channels = 1;
                }
            }
            this.myChapters.totalTime = myVirtualWav.totalSeconds;
            bool flag4 = false;
            if (this.chkAudibleSplit.Checked || this.chkFileSplitting.Checked || this.chkSplitByDuration.Checked)
                flag4 = true;
            if (this.chkSplitByDuration.Checked && !this.myChapters.customChapters)
            {
                this.SetText("Splitting by duration - finding optimal split points...");
                this.myChapters.SetDoubleChapters(this.GetSplitsByDuration(myVirtualWav, double.Parse(this.txtDurationToSplit.Text.Trim())));
            }
            if (this.chkFileSplitting.Checked)
            {
                this.SetText("Searching for silence...");
                this.myChapters.SetDoubleChapters(this.GetChaptersFromSilence(myVirtualWav, double.Parse(this.txtSplitThreshold.Text)));
                this.SetText("Created " + (object)this.myChapters.Count() + " chapters.");
            }
            if (this.chkRemoveAudibleMarkers.Checked && !this.studioOutput)
            {
                if (!this.myChapters.customChapters || this.myChapters.GetChapterDouble(0) == 0.0)
                {
                    this.SetText("Removing \"This is Audible\", etc...");
                    myVirtualWav.soxPath = this.supportLibs.soxPath;
                    numArray = myVirtualWav.RemoveAudibleMarkers();
                    this.myChapters.SetChapter(0, numArray[0]);
                }
                else
                    this.SetText("RED-Not automically removing \"This is Audible\" because chapter one (\"" + this.myChapters.GetChapter(0).description + "\") no longer begins at the start of the audio.");
            }
            if (!this.chkAudibleSplit.Checked && !this.myChapters.customChapters && this.chkRemoveAudibleMarkers.Checked)
                this.myChapters.SetChapter(0, numArray[0]);
            int num6 = (int)Math.Floor(myVirtualWav.totalSeconds);
            if (this.chkRemoveAudibleMarkers.Checked && !this.studioOutput && !this.myChapters.customChapters)
                this.myChapters.Add(numArray[1], "(End)");
            else if (!this.myChapters.customChapters && !this.m4bTranscodeMode)
                this.myChapters.SetRealEndChapter();
            else if (this.m4bTranscodeMode && (double)num6 - this.myChapters.GetChapterDouble(this.myChapters.Count() - 1) > 10.0)
                this.myChapters.SetRealEndChapter();
            if (this.myAdvancedOptions.RemoveTinyChapters)
                this.myChapters.SanityCheck();
            if (this.chkVerifyAudibleSplits.Checked && !this.myChapters.customChapters)
            {
                this.SetText("Verifying split points...");
                this.UpdateProgressBar(0, "blue");
                for (int pos = 1; pos < this.myChapters.Count() - 1; ++pos)
                {
                    try
                    {
                        double val = this.VerifyChapterSplit(this.myChapters.GetChapterDouble(pos), myVirtualWav);
                        this.SetText((pos + 1).ToString() + " / " + (object)(this.myChapters.Count() - 1) + " offset by " + string.Format("{0:0.0}", (object)(val - this.myChapters.GetChapterDouble(pos))) + "s");
                        this.myChapters.SetChapter(pos, val);
                        this.bgwAA.ReportProgress((int)((double)pos / (double)(this.myChapters.Count() - 2) * 100.0));
                    }
                    catch (Exception ex)
                    {
                        Audible.diskLogger("WARNING-Something bad happened - " + ex.ToString());
                    }
                }
            }
            if (this.chkChapterThreshold.Checked && !this.myChapters.customChapters)
            {
                this.SetText("Removing short chapters...");
                int num4 = this.myChapters.Count() + 1;
                while (num4 != this.myChapters.Count())
                {
                    num4 = this.myChapters.Count();
                    this.myChapters.SetDoubleChapters(this.RemoveShortChapters(int.Parse(this.txtChapterThreshold.Text), this.myChapters.GetDoubleList()));
                }
            }
            stopwatch.Restart();
            this.myAudible.nfo.chapters = (this.myChapters.Count() - 1).ToString();
            if (this.myAdvancedOptions.decrypt && flag2 && !this.cdProcessingMode)
                return tmpWAVfile;
            this.myAudible.newChaps = this.myChapters;
            if (this.chkNormalize.Checked)
            {
                this.SetText("Performing pre-normalization analysis...");
                this.UpdateProgressBar(0, "blue");
                Lame myLame1 = new Lame();
                myLame1.myAdvancedOptions = this.myAdvancedOptions;
                this.myChapters.GetDoubleList();
                myLame1.mySupportLibs = this.supportLibs;
                myEncodingOptions.encoder = "normalize";
                myEncodingOptions.startChap = (long)(int)this.myChapters.GetChapterDouble(0);
                myEncodingOptions.endChap = (long)(int)this.myChapters.GetLastChapter();
                myLame1.soxPath = this.supportLibs.soxPath;
                string resultsFile = this.myAdvancedOptions.GetTempPath() + "\\inaudible_norm.txt";
                myLame1.lameCLIoptions = data.lameOptions;
                Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, resultsFile, myEncodingOptions)));
                while (!task.IsCompleted)
                {
                    Thread.Sleep(1000);
                    this.bgwAA.ReportProgress(myLame1.percentComplete);
                }
                myVirtualWav.normalizeLevel = this.ParseNormalizeResults(resultsFile);
                this.SetText("Max volume was " + myVirtualWav.normalizeLevel.ToString("0.0") + " db.");
                myVirtualWav.normalizeLevel = Math.Abs(myVirtualWav.normalizeLevel);
                myVirtualWav.normalize = true;
                this.bgwAA.ReportProgress(100);
                this.bgwAA.ReportProgress(0);
            }
            if (this.chkDRC.Checked)
            {
                myVirtualWav.DRC = true;
                this.SetText("Dynamic Range Compression is active.");
            }
            this.UpdateProgressBar(0, "red");
            if (this.GetSelectedCodec() == "lame")
            {
                this.myAudible.nfo.targetChannels = myEncodingOptions.channels.ToString();
                this.myAudible.nfo.targetSampleRate = myEncodingOptions.sampleRate.ToString();
                this.myAudible.nfo.targetFormat = "LAME MP3";
                List<double> doubleList = this.myChapters.GetDoubleList();
                if (flag4 && doubleList.Count != 2)
                {
                    Audible.diskLogger("Non-monolithic encoding");
                    this.VirtualWAV2MP3(data, myVirtualWav, myItunes2, myEncodingOptions);
                }
                else
                {
                    string outputMP3 = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp3";
                    Lame myLame1 = new Lame();
                    myLame1.myAdvancedOptions = this.myAdvancedOptions;
                    int num4 = (int)doubleList[0];
                    int num7 = (int)doubleList[doubleList.Count - 1];
                    double num8 = doubleList[0];
                    double num9 = doubleList[doubleList.Count - 1];
                    myEncodingOptions.dStartChap = num8;
                    myEncodingOptions.dEndChap = num9;
                    myEncodingOptions.doubleChapters = true;
                    myEncodingOptions.audible = this.myAudible;
                    myEncodingOptions.trackNum = 1;
                    myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                    if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                        myEncodingOptions.embedCover = false;
                    else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                        myEncodingOptions.embedCover = true;
                    Audible.diskLogger("start = " + (object)num4 + " end = " + (object)num7);
                    this.SetText("Encoding with LAME...");
                    LameOptions lameParams = data.lameParams;
                    if (myEncodingOptions.channels == 1)
                        myLame1.SetMono();
                    else
                        myLame1.SetStereo();
                    if (!lameParams.vbr)
                    {
                        myLame1.bitrate = lameParams.bitrate;
                        myLame1.SetCBRMode();
                    }
                    else
                    {
                        myLame1.vbrQuality = lameParams.vbrQuality;
                        myLame1.SetVBRMode();
                    }
                    myEncodingOptions.encoder = "lame";
                    myEncodingOptions.startChap = (long)num4;
                    myEncodingOptions.endChap = (long)num7;
                    myLame1.soxPath = this.supportLibs.soxPath;
                    myLame1.lamePath = this.supportLibs.lamePath;
                    myLame1.lameCLIoptions = data.lameOptions;
                    Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputMP3, myEncodingOptions)));
                    while (!task.IsCompleted)
                    {
                        Thread.Sleep(1000);
                        this.bgwAA.ReportProgress(myLame1.percentComplete);
                    }
                }
            }
            if (this.GetSelectedCodec() == "helix")
            {
                this.myAudible.nfo.targetChannels = myEncodingOptions.channels.ToString();
                this.myAudible.nfo.targetSampleRate = myEncodingOptions.sampleRate.ToString();
                this.myAudible.nfo.targetFormat = "Helix MP3";
                this.myAudible.nfo.targetBitrate = data.lameParams.bitrate.ToString();
                this.myAudible.nfo.vbr = data.lameParams.vbr;
                this.myAudible.nfo.vbrValue = data.lameParams.vbrQuality.ToString();
                List<double> doubleList = this.myChapters.GetDoubleList();
                myEncodingOptions.chapters = this.myChapters;
                if (myEncodingOptions.sampleRate == 44100 && myEncodingOptions.channels == 1)
                {
                    this.SetText("Helix does not support mono encodes at 44.1k. Encoding in joint stereo.");
                    myEncodingOptions.channels = 2;
                }
                if (flag4 && doubleList.Count != 2)
                {
                    Audible.diskLogger("Non-monolithic encoding");
                    this.VirtualWAV2Helix(data, myVirtualWav, myItunes2, myEncodingOptions);
                }
                else
                {
                    string outputMP3 = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp3";
                    Lame myLame1 = new Lame();
                    myLame1.myAdvancedOptions = this.myAdvancedOptions;
                    int num4 = (int)doubleList[0];
                    int num7 = (int)doubleList[doubleList.Count - 1];
                    double num8 = doubleList[0];
                    double num9 = doubleList[doubleList.Count - 1];
                    myEncodingOptions.dStartChap = num8;
                    myEncodingOptions.dEndChap = num9;
                    myEncodingOptions.doubleChapters = true;
                    Audible.diskLogger("start = " + (object)num4 + " end = " + (object)num7);
                    this.SetText("Encoding with Helix...");
                    myEncodingOptions.encoder = "helix";
                    myEncodingOptions.startChap = (long)num4;
                    myEncodingOptions.endChap = (long)num7;
                    myEncodingOptions.lameOptions = data.lameParams;
                    myLame1.soxPath = this.supportLibs.soxPath;
                    myLame1.ffmpegPath = this.supportLibs.ffmpegPath;
                    myEncodingOptions.audible = this.myAudible;
                    myEncodingOptions.trackNum = 1;
                    myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                    if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                        myEncodingOptions.embedCover = false;
                    else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                        myEncodingOptions.embedCover = true;
                    myLame1.helixPath = this.supportLibs.helixPath;
                    myLame1.lameCLIoptions = data.lameOptions;
                    Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputMP3, myEncodingOptions)));
                    while (!task.IsCompleted)
                    {
                        Thread.Sleep(1000);
                        this.bgwAA.ReportProgress(myLame1.percentComplete);
                    }
                }
            }
            if (this.GetSelectedCodec().StartsWith("ffmpeg_"))
            {
                this.myAudible.nfo.targetChannels = myEncodingOptions.channels.ToString();
                this.myAudible.nfo.targetSampleRate = myEncodingOptions.sampleRate.ToString();
                this.myAudible.nfo.targetFormat = this.GetSelectedCodecDescription();
                this.myAudible.nfo.targetBitrate = data.lameParams.bitrate.ToString();
                this.myAudible.nfo.vbr = data.lameParams.vbr;
                this.myAudible.nfo.vbrValue = data.lameParams.vbrQuality.ToString();
                List<double> doubleList = this.myChapters.GetDoubleList();
                myEncodingOptions.chapters = this.myChapters;
                if (flag4 && doubleList.Count != 2)
                {
                    Audible.diskLogger("Non-monolithic encoding");
                    this.VirtualWAV2FFmpeg(data, myVirtualWav, myItunes2, myEncodingOptions);
                }
                else
                {
                    string outputMP3 = "";
                    if (this.GetSelectedCodec().Split('_')[1] == "aac")
                    {
                        outputMP3 = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp4";
                        this.M4Boutput = true;
                    }
                    else
                        outputMP3 = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp3";
                    Lame myLame1 = new Lame();
                    myLame1.myAdvancedOptions = this.myAdvancedOptions;
                    myLame1.bitrate = data.lameParams.bitrate;
                    myLame1.vbrQuality = data.lameParams.vbrQuality;
                    int num4 = (int)doubleList[0];
                    int num7 = (int)doubleList[doubleList.Count - 1];
                    double num8 = doubleList[0];
                    double num9 = doubleList[doubleList.Count - 1];
                    myEncodingOptions.dStartChap = num8;
                    myEncodingOptions.dEndChap = num9;
                    myEncodingOptions.doubleChapters = true;
                    Audible.diskLogger("start = " + (object)num4 + " end = " + (object)num7);
                    this.SetText("Encoding with " + this.GetSelectedCodecDescription() + "...");
                    myEncodingOptions.encoder = this.GetSelectedCodec();
                    myEncodingOptions.startChap = (long)num4;
                    myEncodingOptions.endChap = (long)num7;
                    myEncodingOptions.lameOptions = data.lameParams;
                    myLame1.soxPath = this.supportLibs.soxPath;
                    myLame1.ffmpegPath = this.supportLibs.ffmpegPath;
                    myEncodingOptions.audible = this.myAudible;
                    myEncodingOptions.trackNum = 1;
                    myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                    if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                        myEncodingOptions.embedCover = false;
                    else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                        myEncodingOptions.embedCover = true;
                    myLame1.lameCLIoptions = data.lameOptions;
                    Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputMP3, myEncodingOptions)));
                    while (!task.IsCompleted)
                    {
                        Thread.Sleep(1000);
                        this.bgwAA.ReportProgress(myLame1.percentComplete);
                    }
                }
            }
            else if (this.GetSelectedCodec() == "flac")
            {
                string outputFLAC = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".flac";
                Lame myLame1 = new Lame();
                myLame1.myAdvancedOptions = this.myAdvancedOptions;
                List<double> doubleList = this.myChapters.GetDoubleList();
                myLame1.ffmpegPath = this.supportLibs.ffmpegPath;
                int num4 = (int)doubleList[0];
                int num7 = (int)doubleList[doubleList.Count - 1];
                this.SetText("Encoding with FLAC...");
                myEncodingOptions.encoder = "flac";
                myEncodingOptions.startChap = (long)num4;
                myEncodingOptions.endChap = (long)num7;
                myLame1.soxPath = this.supportLibs.soxPath;
                myLame1.lameCLIoptions = data.lameOptions;
                Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputFLAC, myEncodingOptions)));
                while (!task.IsCompleted)
                {
                    Thread.Sleep(1000);
                    this.bgwAA.ReportProgress(myLame1.percentComplete);
                }
            }
            else if (this.GetSelectedCodec() == "wav")
            {
                List<double> doubleList = this.myChapters.GetDoubleList();
                if (flag4 && doubleList.Count != 2)
                {
                    this.VirtualWAV2WAVs(data, myVirtualWav, myItunes2, myEncodingOptions);
                }
                else
                {
                    string outputWAV = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".wav";
                    Lame myLame1 = new Lame();
                    myLame1.myAdvancedOptions = this.myAdvancedOptions;
                    int num4 = (int)doubleList[0];
                    int num7 = (int)doubleList[doubleList.Count - 1];
                    this.SetText("Dumping WAVs...");
                    myEncodingOptions.encoder = "wav";
                    myEncodingOptions.startChap = (long)num4;
                    myEncodingOptions.endChap = (long)num7;
                    myLame1.soxPath = this.supportLibs.soxPath;
                    myLame1.lameCLIoptions = data.lameOptions;
                    Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputWAV, myEncodingOptions)));
                    while (!task.IsCompleted)
                    {
                        Thread.Sleep(1000);
                        this.bgwAA.ReportProgress(myLame1.percentComplete);
                    }
                }
            }
            else if (this.GetSelectedCodec() == "opus")
            {
                this.myAudible.nfo.targetChannels = myEncodingOptions.channels.ToString();
                this.myAudible.nfo.targetSampleRate = myEncodingOptions.sampleRate.ToString();
                this.myAudible.nfo.targetFormat = "Opus";
                List<double> doubleList = this.myChapters.GetDoubleList();
                if (flag4 && doubleList.Count != 2)
                {
                    this.VirtualWAV2Opus(data, myVirtualWav, myItunes2, myEncodingOptions);
                }
                else
                {
                    Lame myLame1 = new Lame();
                    myLame1.myAdvancedOptions = this.myAdvancedOptions;
                    myLame1.opusPath = this.supportLibs.opusPath;
                    this.SetText("Encoding with Opus...");
                    string outputMP3 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".opus";
                    myLame1.SetOutputFile(outputMP3);
                    int num4 = (int)doubleList[0];
                    int num7 = (int)doubleList[doubleList.Count - 1];
                    double num8 = doubleList[0];
                    double num9 = doubleList[doubleList.Count - 1];
                    myEncodingOptions.dStartChap = num8;
                    myEncodingOptions.dEndChap = num9;
                    myEncodingOptions.doubleChapters = true;
                    VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                    OpusOptions opusOptions = new OpusOptions();
                    opusOptions.myVirtualWav = virtualWav;
                    opusOptions.start = (long)num4;
                    opusOptions.end = (long)num7;
                    opusOptions.fileName = outputMP3;
                    opusOptions.encodingArgs = this.opusOptions;
                    opusOptions.myAudible = this.myAudible;
                    myEncodingOptions.encoder = "opus";
                    myEncodingOptions.startChap = (long)num4;
                    myEncodingOptions.endChap = (long)num7;
                    myLame1.soxPath = this.supportLibs.soxPath;
                    myEncodingOptions.opusOptions = opusOptions;
                    myEncodingOptions.audible = this.myAudible;
                    Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputMP3, myEncodingOptions)));
                    while (!task.IsCompleted)
                    {
                        Thread.Sleep(1000);
                        this.bgwAA.ReportProgress(myLame1.percentComplete);
                    }
                }
            }
            else if (this.GetSelectedCodec() == "ogg")
            {
                this.myAudible.nfo.targetChannels = myEncodingOptions.channels.ToString();
                this.myAudible.nfo.targetSampleRate = myEncodingOptions.sampleRate.ToString();
                this.myAudible.nfo.targetFormat = "Ogg";
                List<double> doubleList = this.myChapters.GetDoubleList();
                if (flag4 && doubleList.Count != 2)
                {
                    this.VirtualWAV2Ogg(data, myVirtualWav, myItunes2, myEncodingOptions);
                }
                else
                {
                    Lame myLame1 = new Lame();
                    myLame1.myAdvancedOptions = this.myAdvancedOptions;
                    myLame1.oggPath = this.supportLibs.oggPath;
                    this.SetText("Encoding with Ogg...");
                    string outputMP3 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".ogg";
                    myLame1.SetOutputFile(outputMP3);
                    int num4 = (int)doubleList[0];
                    int num7 = (int)doubleList[doubleList.Count - 1];
                    double num8 = doubleList[0];
                    double num9 = doubleList[doubleList.Count - 1];
                    myEncodingOptions.dStartChap = num8;
                    myEncodingOptions.dEndChap = num9;
                    myEncodingOptions.doubleChapters = true;
                    VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                    OggOptions oggOptions = new OggOptions();
                    oggOptions.myVirtualWav = virtualWav;
                    oggOptions.start = (long)num4;
                    oggOptions.end = (long)num7;
                    oggOptions.fileName = outputMP3;
                    oggOptions.encodingArgs = this.oggOptions;
                    oggOptions.myAudible = this.myAudible;
                    myEncodingOptions.encoder = "ogg";
                    myEncodingOptions.startChap = (long)num4;
                    myEncodingOptions.endChap = (long)num7;
                    myLame1.soxPath = this.supportLibs.soxPath;
                    myEncodingOptions.oggOptions = oggOptions;
                    myEncodingOptions.audible = this.myAudible;
                    Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputMP3, myEncodingOptions)));
                    while (!task.IsCompleted)
                    {
                        Thread.Sleep(1000);
                        this.bgwAA.ReportProgress(myLame1.percentComplete);
                    }
                }
            }
            else if (this.GetSelectedCodec() == "fdk")
            {
                this.myAudible.nfo.targetChannels = myEncodingOptions.channels.ToString();
                this.myAudible.nfo.targetSampleRate = myEncodingOptions.sampleRate.ToString();
                this.myAudible.nfo.targetFormat = "Fraunhofer FDK AAC";
                this.myAudible.nfo.targetBitrate = myEncodingOptions.m4bOptions.bitrate.ToString();
                string outputM4B = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp4";
                Lame myLame1 = new Lame();
                myLame1.myAdvancedOptions = this.myAdvancedOptions;
                List<double> doubleList = this.myChapters.GetDoubleList();
                myLame1.ffmpegPath = this.supportLibs.ffmpegPath;
                myLame1.soxPath = this.supportLibs.soxPath;
                int num4 = (int)doubleList[0];
                int num7 = (int)doubleList[doubleList.Count - 1];
                double num8 = doubleList[0];
                double num9 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num8;
                myEncodingOptions.dEndChap = num9;
                myEncodingOptions.doubleChapters = true;
                this.SetText("Encoding with Fraunhofer FDK AAC...");
                myEncodingOptions.encoder = "fdk";
                myEncodingOptions.startChap = (long)num4;
                myEncodingOptions.endChap = (long)num7;
                myEncodingOptions.audible = this.myAudible;
                Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputM4B, myEncodingOptions)));
                while (!task.IsCompleted)
                {
                    Thread.Sleep(1000);
                    this.bgwAA.ReportProgress(myLame1.percentComplete);
                }
            }
            else if (this.GetSelectedCodec() == "nero")
            {
                this.myAudible.nfo.targetChannels = myEncodingOptions.channels.ToString();
                this.myAudible.nfo.targetSampleRate = myEncodingOptions.sampleRate.ToString();
                this.myAudible.nfo.targetFormat = "Nero AAC";
                List<double> doubleList = this.myChapters.GetDoubleList();
                string outputM4B = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp4";
                Lame myLame1 = new Lame();
                myLame1.myAdvancedOptions = this.myAdvancedOptions;
                myLame1.neroAACpath = this.supportLibs.neroAACpath;
                myLame1.soxPath = this.supportLibs.soxPath;
                int num4 = (int)doubleList[0];
                int num7 = (int)doubleList[doubleList.Count - 1];
                double num8 = doubleList[0];
                double num9 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num8;
                myEncodingOptions.dEndChap = num9;
                myEncodingOptions.doubleChapters = true;
                this.SetText("Encoding with Nero AAC...");
                myEncodingOptions.encoder = "nero";
                myEncodingOptions.startChap = (long)num4;
                myEncodingOptions.endChap = (long)num7;
                myEncodingOptions.audible = this.myAudible;
                Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, outputM4B, myEncodingOptions)));
                while (!task.IsCompleted)
                {
                    Thread.Sleep(1000);
                    this.bgwAA.ReportProgress(myLame1.percentComplete);
                }
            }
            else if (this.studioOutput)
            {
                this.SetText("Compressing decrypted output...");
                string directoryName = Path.GetDirectoryName(this.outputFileMask);
                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);
                string outputFLAC = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".flac";
                Lame myLame1 = new Lame();
                myLame1.myAdvancedOptions = this.myAdvancedOptions;
                List<double> doubleList = this.myChapters.GetDoubleList();
                myLame1.ffmpegPath = this.supportLibs.ffmpegPath;
                int startChap = (int)doubleList[0];
                int endChap = (int)doubleList[doubleList.Count - 1];
                myEncodingOptions.encoder = "flac";
                myEncodingOptions.startChap = (long)startChap;
                myEncodingOptions.endChap = (long)endChap;
                myLame1.soxPath = this.supportLibs.soxPath;
                myLame1.lameCLIoptions = data.lameOptions;
                Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.FLACencodeVirtualWav(myVirtualWav, (long)startChap, (long)endChap, outputFLAC)));
                while (!task.IsCompleted)
                {
                    Thread.Sleep(1000);
                    this.bgwAA.ReportProgress(myLame1.percentComplete);
                }
                this.myAudible.totalTime = myVirtualWav.GetFormattedTime();
                this.CreateStudioBundle(outputFLAC);
            }
            this.bgwAA.ReportProgress(100);
            stopwatch.Stop();
            elapsed2 = stopwatch.Elapsed;
            this.SetText("Encoded in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed2.Hours, (object)elapsed2.Minutes, (object)elapsed2.Seconds, (object)(elapsed2.Milliseconds / 10)));
            if (!this.chkRerun.Checked && !this.studioProcessingMode && (!this.myAdvancedOptions.decrypt || this.m4pMode))
            {
                foreach (string path in workingDirs)
                    Directory.Delete(path, true);
            }
            if (this.myAdvancedOptions.decrypt)
            {
                try
                {
                    System.IO.File.Delete(Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".wav");
                    System.IO.File.Delete(Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".tmp.mp4");
                    System.IO.File.Delete(Path.GetDirectoryName(this.outputFileMask) + "\\funny.aac");
                }
                catch
                {
                }
            }
            if (this.studioProcessingMode && !this.cdProcessingMode)
            {
                System.IO.File.Delete(Path.GetDirectoryName(this.myAudible.decryptedFile) + "\\" + Path.GetFileNameWithoutExtension(this.myAudible.decryptedFile) + ".wav");
                this.flacUnpacked = false;
            }
            if (this.chkRerun.Checked)
            {
                this.reRun = true;
                this.savedInitialDirs = strArray;
            }
            return tmpWAVfile;
        }

        private double ParseNormalizeResults(string resultsFile)
        {
            double num = 12.0;
            string text = System.IO.File.ReadAllText(resultsFile);
            Audible.diskLogger(text);
            string str1 = text;
            char[] chArray = new char[1] { '\r' };
            foreach (string str2 in str1.Split(chArray))
            {
                if (str2.Contains("max_volume:"))
                {
                    string s = str2.Split(':')[1].Trim().Split(' ')[0];
                    double result = 0.0;
                    double.TryParse(s, out result);
                    num += result;
                }
            }
            try
            {
                System.IO.File.Delete(resultsFile);
            }
            catch (Exception ex)
            {
                Audible.diskLogger("Could not clean up normilazation results: " + ex.ToString());
            }
            return num;
        }

        private List<double> GetSplitsByDuration(VirtualWAV myVirtualWav, double threshold)
        {
            List<double> doubleList = new List<double>();
            double num1 = 0.5;
            string tmpFile = Path.GetDirectoryName(this.outputFileMask) + "\\tmpchap.txt";
            EncodingOptions myEncodingOptions = this.GetEncodingOptions();
            myEncodingOptions.sampleRate = 44100;
            myEncodingOptions.channels = 1;
            Lame myLame1 = new Lame();
            myEncodingOptions.encoder = "soxsilence";
            myEncodingOptions.startChap = 0L;
            myEncodingOptions.endChap = (long)myVirtualWav.totalSeconds;
            myEncodingOptions.dStartChap = 0.0;
            myEncodingOptions.dEndChap = myVirtualWav.totalSeconds;
            myEncodingOptions.doubleChapters = true;
            myLame1.soxPath = this.supportLibs.soxPath;
            myEncodingOptions.silenceThreshold = num1;
            Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, tmpFile, myEncodingOptions)));
            while (!task.IsCompleted)
            {
                Thread.Sleep(200);
                this.bgwAA.ReportProgress(myLame1.percentComplete);
            }
            List<string> soxOutput = Form1.ParseSoxOutput(tmpFile);
            List<double> doubleSplits = new List<double>();
            foreach (string str in soxOutput)
            {
                char[] chArray = new char[1] { ':' };
                string[] strArray = str.Split(chArray);
                double num2 = double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2]);
                doubleSplits.Add(num2);
            }
            int num3 = (int)(myVirtualWav.totalSeconds / 60.0 / threshold);
            doubleList.Add(0.0);
            for (int index = 1; index <= num3; ++index)
            {
                double nearestSilence = this.GetNearestSilence((double)index * threshold * 60.0, doubleSplits);
                doubleList.Add(nearestSilence - num1 / 2.0);
            }
            return doubleList;
        }

        private List<double> GetChaptersFromSilence(VirtualWAV myVirtualWav, double silenceDuration)
        {
            string tmpFile = Path.GetDirectoryName(this.outputFileMask) + "\\tmpchap.txt";
            EncodingOptions myEncodingOptions = this.GetEncodingOptions();
            myEncodingOptions.sampleRate = 44100;
            myEncodingOptions.channels = 1;
            Lame myLame1 = new Lame();
            myEncodingOptions.encoder = "soxsilence";
            myEncodingOptions.startChap = 0L;
            myEncodingOptions.endChap = (long)myVirtualWav.totalSeconds;
            myEncodingOptions.dStartChap = 0.0;
            myEncodingOptions.dEndChap = myVirtualWav.totalSeconds;
            myEncodingOptions.doubleChapters = true;
            myLame1.soxPath = this.supportLibs.soxPath;
            myEncodingOptions.silenceThreshold = silenceDuration;
            Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, tmpFile, myEncodingOptions)));
            while (!task.IsCompleted)
            {
                Thread.Sleep(200);
                this.bgwAA.ReportProgress(myLame1.percentComplete);
            }
            List<string> soxOutput = Form1.ParseSoxOutput(tmpFile);
            List<double> doubleList = new List<double>();
            foreach (string str in soxOutput)
            {
                char[] chArray = new char[1] { ':' };
                string[] strArray = str.Split(chArray);
                double num = double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2]);
                doubleList.Add(num);
            }
            double num1 = 10.0;
            for (int index = 1; index < doubleList.Count; ++index)
            {
                double num2 = doubleList[index] - doubleList[index - 1];
                if (num1 > num2)
                {
                    if (index == 1)
                        doubleList.RemoveAt(index);
                    else
                        doubleList.RemoveAt(index - 1);
                }
            }
            return doubleList;
        }

        private double GetNearestSilence(double chap, List<double> doubleSplits)
        {
            double num = chap;
            for (int index = 0; index < doubleSplits.Count; ++index)
            {
                if (doubleSplits[index] > chap)
                    return doubleSplits[index];
            }
            return num;
        }

        private void SetMazeMode(bool mode)
        {
        }

        private bool DecryptAacAudibleManager(VirtualWAV myVirtualWav, string file, string wFile, double totalTime)
        {
            this.UpdateProgressBar(0, "green");
            this.SetMazeMode(true);
            int returnCode = 100;
            myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            myVirtualWav.aax2wavPath = this.supportLibs.aax2wavPath;
            myVirtualWav.instarip = this.supportLibs.instaripPath;
            string tempRipFile = "";
            this.GetSampleRateFromInput(file, ref myVirtualWav);
            bool flag1 = false;
            if (!this.myAdvancedOptions.ng && !this.VerifyAudibleManagerInstallation(true))
            {
                this.SetText("Enabling inAudible-NG decryption engine...");
                this.myAdvancedOptions.ng = true;
                flag1 = true;
            }
            bool flag2 = false;
            bool flag3 = false;
            if (!this.myAdvancedOptions.ng && new FileInfo(file).Length < this.maxAAXsize)
            {
                Thread thread = new Thread((ThreadStart)(() => returnCode = myVirtualWav.DecryptAAC(file, wFile, this.myAdvancedOptions.AudibleMangerDLLPath)));
                tempRipFile = wFile;
                thread.Start();
                while (thread.IsAlive || returnCode != 0)
                {
                    if (returnCode < 0)
                    {
                        try
                        {
                            this.bgwAA.ReportProgress(100);
                        }
                        catch
                        {
                        }
                        this.SetText("WARNING-Decryption failed with error " + (object)returnCode);
                        this.GetInstaripError(returnCode);
                        flag3 = true;
                        flag1 = true;
                        this.SetText("Switching to NG decryption...");
                        break;
                    }
                    try
                    {
                        Thread.Sleep(500);
                        if (System.IO.File.Exists(tempRipFile))
                        {
                            int percentProgress = (int)((double)new FileInfo(tempRipFile).Length / (double)new FileInfo(file).Length * 100.0);
                            try
                            {
                                this.bgwAA.ReportProgress(percentProgress);
                            }
                            catch
                            {
                                this.UpdateProgressBar(percentProgress, "green");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Audible.diskLogger(ex.ToString());
                    }
                }
            }
            if (flag3 || this.myAdvancedOptions.ng || new FileInfo(file).Length > this.maxAAXsize)
            {
                flag2 = true;
                string key = "";
                if (this.myAdvancedOptions.ngKeys.Length == 0 || this.myAdvancedOptions.ngKeys[0] == "")
                {
                    this.SetText("Finding your Audible key.  This could take a while, but only needs to be done once.");
                    string checksum = this.GetChecksum(file);
                    this.SetText("Checksum: " + checksum);
                    key = this.CrackAAX(checksum);
                    if (key == "<not found>")
                    {
                        this.SetText("EAA-Could not find your key. You can probably fix this problem by downloading and installing the inAuddible-NG-keys pacakge. You can likely find this package from the same place that you found inAudible.");
                        this.SetMazeMode(false);
                        return false;
                    }
                    this.SetText("Derived key - " + key);
                    this.myAdvancedOptions.ngKeys = new string[1];
                    this.myAdvancedOptions.ngKeys[0] = key;
                }
                bool flag4 = false;
                myVirtualWav.ngPath = this.supportLibs.ngPath;
                bool flag5;
                foreach (string ngKey in this.myAdvancedOptions.ngKeys)
                {
                    string tKey = ngKey;
                    Audible.diskLogger("Trying key " + tKey);
                    returnCode = 100;
                    tempRipFile = Path.GetDirectoryName(this.outputFileMask) + "\\funny.aac";
                    try
                    {
                        System.IO.File.Delete(tempRipFile);
                    }
                    catch
                    {
                    }
                    myVirtualWav.trackDumpPath = this.supportLibs.trackDumpPath;
                    Thread thread = new Thread((ThreadStart)(() => returnCode = myVirtualWav.ngDecrypt(file, tempRipFile, tKey)));
                    thread.Start();
                    while (!flag4 && (thread.IsAlive || returnCode != 0))
                    {
                        if (returnCode < 0)
                        {
                            try
                            {
                                this.bgwAA.ReportProgress(100);
                            }
                            catch
                            {
                                this.UpdateProgressBar(100, "default");
                            }
                        }
                        if (returnCode == -99)
                        {
                            flag4 = false;
                            break;
                        }
                        try
                        {
                            Thread.Sleep(500);
                            if (System.IO.File.Exists(tempRipFile))
                            {
                                int percentProgress = (int)((double)new FileInfo(tempRipFile).Length / (double)new FileInfo(file).Length * 100.0);
                                try
                                {
                                    this.bgwAA.ReportProgress(percentProgress);
                                }
                                catch
                                {
                                    this.UpdateProgressBar(percentProgress, "green");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Audible.diskLogger(ex.ToString());
                        }
                    }
                    if (returnCode == 0)
                    {
                        flag5 = true;
                        break;
                    }
                    this.SetMazeMode(false);
                }
                if (returnCode == -99)
                {
                    if (flag1)
                    {
                        this.SetText("WARNING-You are trying to rip a file that is not associated with your Audible account.  Aborting...");
                        this.myAdvancedOptions.ng = false;
                        this.SetMazeMode(false);
                        return false;
                    }
                    if (flag3)
                        this.SetText("Attempting to rip anyway...");
                    else
                        this.SetText("Your key does not work with this AAX. Trying to crack it...");
                    string checksum = this.GetChecksum(file);
                    this.SetText("Checksum: " + checksum);
                    key = this.CrackAAX(checksum);
                    this.SetText("Derived key - " + key);
                    string[] strArray = new string[this.myAdvancedOptions.ngKeys.Length + 1];
                    for (int index = 0; index < this.myAdvancedOptions.ngKeys.Length; ++index)
                        strArray[index] = this.myAdvancedOptions.ngKeys[index];
                    strArray[this.myAdvancedOptions.ngKeys.Length] = key;
                    this.myAdvancedOptions.ngKeys = strArray;
                    Audible.diskLogger("Trying key " + key);
                    tempRipFile = Path.GetDirectoryName(this.outputFileMask) + "\\funny.aac";
                    try
                    {
                        System.IO.File.Delete(tempRipFile);
                    }
                    catch
                    {
                    }
                    myVirtualWav.trackDumpPath = this.supportLibs.trackDumpPath;
                    returnCode = 100;
                    Thread thread = new Thread((ThreadStart)(() => returnCode = myVirtualWav.ngDecrypt(file, tempRipFile, key)));
                    thread.Start();
                    while (thread.IsAlive || returnCode != 0)
                    {
                        if (returnCode < 0)
                        {
                            try
                            {
                                this.bgwAA.ReportProgress(100);
                            }
                            catch
                            {
                                this.UpdateProgressBar(100, "default");
                            }
                        }
                        try
                        {
                            Thread.Sleep(500);
                            if (System.IO.File.Exists(tempRipFile))
                            {
                                int percentProgress = (int)((double)new FileInfo(tempRipFile).Length / (double)new FileInfo(file).Length * 100.0);
                                try
                                {
                                    this.bgwAA.ReportProgress(percentProgress);
                                }
                                catch
                                {
                                    this.UpdateProgressBar(percentProgress, "default");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Audible.diskLogger(ex.ToString());
                        }
                    }
                }
                if (returnCode == 0)
                    flag5 = true;
            }
            if (flag2)
                Form1.SafeRename(Path.GetDirectoryName(wFile) + "\\funny.aac", wFile);
            try
            {
                this.bgwAA.ReportProgress(100);
            }
            catch
            {
            }
            myVirtualWav.AACtoWAV(wFile);
            myVirtualWav.totalSeconds = totalTime;
            if (this.myAdvancedOptions.SHA256Checksum)
            {
                this.SetText("Calculating SHA256 checksum...");
                this.SetText(Audible.GetChecksumBuffered(wFile));
            }
            if (flag1)
                this.myAdvancedOptions.ng = false;
            this.SetMazeMode(false);
            return true;
        }

        private string CrackAAX(string checksum)
        {
            string str1 = "";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ngPath + "rcrack.exe";
            process.StartInfo.Arguments = "*.rtc -h " + checksum;
            process.StartInfo.WorkingDirectory = this.supportLibs.ngPath;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            string str2 = process.StandardOutput.ReadToEnd().Replace("\r", "");
            char[] chArray = new char[1] { '\n' };
            foreach (string str3 in str2.Split(chArray))
            {
                if (str3.Contains("hex:".ToLower()))
                {
                    string[] strArray = str3.Split(':');
                    str1 = strArray[strArray.Length - 1].Trim();
                }
            }
            return str1;
        }

        private string GetChecksum(string file)
        {
            BinaryReader binaryReader = new BinaryReader((Stream)System.IO.File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read));
            long length = binaryReader.BaseStream.Length;
            int num1 = 653;
            int num2 = 20;
            int index = 0;
            byte[] ba = new byte[20];
            binaryReader.BaseStream.Seek((long)num1, SeekOrigin.Begin);
            for (; (long)num1 < length && index < num2; ++index)
            {
                ba[index] = binaryReader.ReadByte();
                ++num1;
            }
            return Form1.ByteArrayToString(ba);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        private bool VerifyAudibleManagerInstallation(bool silence = false)
        {
            bool flag = false;
            if (System.IO.File.Exists(this.myAdvancedOptions.AudibleMangerDLLPath))
                return true;
            string path = this.myAdvancedOptions.AudibleMangerDLLPath.Replace(" (x86)", "");
            if (System.IO.File.Exists(path))
            {
                this.myAdvancedOptions.AudibleMangerDLLPath = path;
                return true;
            }
            if (!silence)
                this.SetText("WARNING-Could not locate AAXSDKWin.dll. The Audible Manager needs to be installed and you need to set the location of the AAXSDKWin.dll file under the Advanced settings in inAudible.");
            return flag;
        }

        public static void SafeRename(string source, string target)
        {
            bool flag = false;
            while (!flag)
            {
                try
                {
                    System.IO.File.Delete(target);
                    System.IO.File.Move(source, target);
                    flag = true;
                }
                catch
                {
                    Thread.Sleep(1000);
                    Audible.diskLogger("Failed to move " + source + " to " + target);
                }
            }
        }

        private void CopyCleanMP4(string file, string wFile)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.mp4boxPath;
            process.StartInfo.Arguments = "-raw 1 \"" + file + "\" -out \"" + wFile + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

        private void GetInstaripError(int returnCode)
        {
            switch (returnCode)
            {
                case -1073741515:
                    this.SetText("WARNING-You need to install the Visual C++ 2010 SP1 Redistributable - http://www.microsoft.com/en-ca/download/details.aspx?id=8328");
                    break;
                case -8:
                    this.SetText("WARNING-Error closing AAX file");
                    break;
                case -7:
                    this.SetText("WARNING-Input file was not an AAX or AA type 4");
                    break;
                case -6:
                    this.SetText("WARNING-Unable to authenticate your Audible account for this AAX. Have you authenticated Audible Manager with your Audible account and successfully played an AAX file?");
                    break;
                case -5:
                    this.SetText("WARNING-Unable to seek to beginning of AAX");
                    break;
                case -4:
                    this.SetText("WARNING-Unable to get sample rate");
                    break;
                case -3:
                    this.SetText("WARNING-Unable to get channel count");
                    break;
                case -2:
                    this.SetText("WARNING-Could not load the AAX file");
                    break;
                case -1:
                    this.SetText("WARNING-Could not load the Audible DLL");
                    break;
                default:
                    this.SetText("WARNING-Unknown error. Maybe you need to install the redistributable? http://www.microsoft.com/en-ca/download/details.aspx?id=8328");
                    break;
            }
        }

        private double GetTotalTimeFromAAC(string file)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-i \"" + file + "\" -bsf:a aac_adtstoasc -acodec copy -f null -";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            string end = process.StandardError.ReadToEnd();
            process.WaitForExit();
            string[] strArray1 = end.Split('\r');
            List<string> stringList = new List<string>();
            foreach (string str in strArray1)
            {
                if (str.Trim().StartsWith("size=N/A time="))
                    stringList.Add(str.Split(' ')[1].Split('=')[1]);
            }
            string[] strArray2 = stringList[stringList.Count - 1].Split(':');
            //strArray2[0].Trim() + ":" + strArray2[1].Trim() + ":" + strArray2[2].Trim();
            return double.Parse(strArray2[0].Trim()) * 60.0 * 60.0 + double.Parse(strArray2[1].Trim()) * 60.0 + double.Parse(strArray2[2].Trim());
        }

        private string GetTotalTimeFromAAX(string file)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffprobePath;
            process.StartInfo.Arguments = "-i \"" + file + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            string end = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return Audible.GetM4BTotalTimeffmpeg(end);
        }

        private string GetTotalTimeFfmpeg(string file)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-i \"" + file + "\" -acodec copy -f null -";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            string end = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return Audible.GetM4BTotalTimeffmpeg(end);
        }

        private int ConvertAAtoWAV(string inputFile, string outputFile)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-y -i \"" + inputFile + "\" \"" + outputFile + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        private int ConvertMP3toWAV(string inputFile, string outputFile)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.soxPath;
            process.StartInfo.Arguments = "\"" + inputFile + "\" \"" + outputFile + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        private int Convert3PGtoWAV(string inputFile, string outputFile)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.BardDecoder;
            process.StartInfo.Arguments = "-if \"" + inputFile + "\" -of \"" + outputFile + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            try
            {
                System.IO.File.Delete(inputFile);
            }
            catch (Exception ex)
            {
                Audible.diskLogger("Couldn't delete " + inputFile + ": " + ex.ToString());
            }
            return process.ExitCode;
        }

        private int QuickSplitAA(string file, string outFile, int startTrackNum)
        {
            string directoryName = Path.GetDirectoryName(outFile);
            string str = directoryName + "\\rename_me";
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
            foreach (FileSystemInfo enumerateFile in new DirectoryInfo(directoryName).EnumerateFiles("rename_me*.*"))
                enumerateFile.Delete();
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = Path.GetDirectoryName(this.supportLibs.ngPath) + "\\AA-ng.exe";
            process.StartInfo.Arguments = "-split \"" + file + "\" \"" + str + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            System.IO.File.Delete(str + ".m3u");
            FileInfo[] files = directoryInfo.GetFiles("rename_me*.*");
            int trackNum = startTrackNum;
            string withoutExtension = Path.GetFileNameWithoutExtension(this.outputFileMask);
            FileInfo[] array = ((IEnumerable<FileInfo>)files).OrderBy<FileInfo, DateTime>((System.Func<FileInfo, DateTime>)(f => f.CreationTime)).ToArray<FileInfo>();
            this.SetText("Rebuilding decrypted MP3's...");
            for (int index = 0; index < array.Length; ++index)
            {
                this.bgwAA.ReportProgress((int)((double)(index + 1) / (double)array.Length * 100.0));
                this.TaglibAndRenameMP3(this.StripXing(array[index].FullName), Path.GetDirectoryName(array[index].FullName) + "\\" + withoutExtension + " - " + trackNum.ToString("D3") + ".mp3", trackNum);
                ++trackNum;
            }
            return trackNum;
        }

        private string PackMp3File(string inputFile)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.mp3packerPath;
            process.StartInfo.Arguments = "-s -t -f --keep-ok out \"" + inputFile + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            string sourceFileName = Path.GetDirectoryName(inputFile) + "\\" + Path.GetFileNameWithoutExtension(inputFile) + "-vbr.wav";
            string destFileName = Path.GetDirectoryName(inputFile) + "\\" + Path.GetFileNameWithoutExtension(inputFile) + ".mp3";
            System.IO.File.Move(sourceFileName, destFileName);
            return destFileName;
        }

        private string StripXing(string inputFile)
        {
            string sourceFileName = Path.GetDirectoryName(inputFile) + "\\temp.mp3";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.mp3SplitPath;
            process.StartInfo.Arguments = "\"" + inputFile + "\" 0.0 EOF -n -x -d \"" + Path.GetDirectoryName(inputFile) + "\" -o temp";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            string destFileName = Path.GetDirectoryName(inputFile) + "\\" + Path.GetFileNameWithoutExtension(inputFile) + ".mp3";
            System.IO.File.Delete(inputFile);
            System.IO.File.Move(sourceFileName, destFileName);
            return destFileName;
        }

        public List<double> GetAAChapters(string file)
        {
            List<double> doubleList1 = new List<double>();
            string tempPath = this.myAdvancedOptions.GetTempPath();
            string str1 = tempPath + "\\inaudible";
            this.SetText("Decrypting chapters...");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = Path.GetDirectoryName(this.supportLibs.ngPath) + "\\AA-ng.exe";
            process.StartInfo.Arguments = "-split \"" + file + "\" \"" + str1 + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            IOrderedEnumerable<string> orderedEnumerable = ((IEnumerable<string>)Directory.GetFiles(tempPath, "inaudible*.wav")).OrderBy<string, DateTime>((System.Func<string, DateTime>)(f => new DirectoryInfo(f).CreationTime));
            string str2 = tempPath + "\\tmpWAVs";
            Directory.CreateDirectory(str2);
            int num1 = 0;
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            List<string> stringList = new List<string>();
            foreach (string str3 in (IEnumerable<string>)orderedEnumerable)
                stringList.Add(str3);
            IWorkItemResult<int>[] workItemResultArray = new IWorkItemResult<int>[stringList.Count];
            for (int index = 0; index < stringList.Count; ++index)
            {
                int num2 = num1;
                workItemResultArray[index] = smartThreadPool.QueueWorkItem<string, string, int>(new Amib.Threading.Func<string, string, int>(this.ConvertAAtoWAV), stringList[index], str2 + "\\temp-" + num2.ToString("D3") + ".wav");
                ++num1;
            }
            int num3 = 0;
            int num4 = stringList.Count - 1;
            while (num3 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num3 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num3;
                }
                if (num4 != num3 && num3 > 0)
                {
                    num4 = num3;
                    try
                    {
                        this.bgwAA.ReportProgress((int)((double)num3 / (double)workItemResultArray.Length * 100.0));
                    }
                    catch
                    {
                        this.SetText(num3.ToString() + "/" + (object)workItemResultArray.Length);
                    }
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
            List<double> doubleList2 = new VirtualWAV()
            {
                ffmpegPath = this.supportLibs.ffmpegPath,
                ffprobePath = this.supportLibs.ffprobePath
            }.ConstructCDWAV(str2);
            for (int index = doubleList2.Count<double>() + 1; index != doubleList2.Count<double>(); doubleList2 = this.RemoveShortChapters(2, doubleList2))
                index = doubleList2.Count<double>();
            foreach (FileInfo enumerateFile in new DirectoryInfo(tempPath).EnumerateFiles("inaudible*.wav"))
            {
                try
                {
                    enumerateFile.Delete();
                }
                catch
                {
                }
            }
            try
            {
                Directory.Delete(str2, true);
            }
            catch
            {
            }
            return doubleList2;
        }

        private void AAX2VirtualWav(DecryptAAXOptions data, ref VirtualWAV myVirtualWav, iTunes myItunes2, int sliceSize, string file)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            LameOptions lameParams = data.lameParams;
            IWorkItemResult<int>[] workItemResultArray = new IWorkItemResult<int>[this.threads];
            int num1 = 0;
            for (int index = 0; index < this.threads; ++index)
            {
                int num2 = num1;
                int num3 = sliceSize + 10;
                if (index + 1 == this.threads)
                    num3 = sliceSize;
                Lame lame = new Lame();
                string str = myItunes2.inAudibleTargetDir + "\\aax-" + (index + 1).ToString("D3") + ".wav";
                lame.SetOutputFile(str);
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                lame.soxPath = this.supportLibs.soxPath;
                lame.lameCLIoptions = data.lameOptions;
                AAXDecrypterOptions decrypterOptions = (AAXDecrypterOptions)new AAXDecrypterOptions(file, str, 0.0)
                {
                    DLLPath = this.myAdvancedOptions.AudibleMangerDLLPath,
                    start = num2,
                    end = num3
                }.Clone();
                workItemResultArray[index] = smartThreadPool.QueueWorkItem<AAXDecrypterOptions, int>(new Amib.Threading.Func<AAXDecrypterOptions, int>(myVirtualWav.DecryptAAXSegment), decrypterOptions);
                num1 += sliceSize;
            }
            new Stopwatch().Start();
            int num4 = 0;
            int num5 = this.threads;
            while (num4 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num4 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num4;
                }
                if (num5 != num4 && num4 > 0)
                {
                    this.SetText("Chunk " + (object)num4 + "/" + (object)workItemResultArray.Length);
                    num5 = num4;
                    this.bgwAA.ReportProgress((int)((double)num4 / (double)workItemResultArray.Length * 100.0));
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }

        private void GetSampleRateFromInput(string tFile, ref VirtualWAV myVirtualWav)
        {
            Process process1 = new Process();
            process1.StartInfo = new ProcessStartInfo();
            process1.StartInfo.FileName = this.supportLibs.ffprobePath;
            process1.StartInfo.Arguments = "-loglevel panic -show_streams -print_format flat \"" + tFile + "\"";
            process1.StartInfo.UseShellExecute = false;
            process1.StartInfo.RedirectStandardOutput = true;
            process1.StartInfo.CreateNoWindow = true;
            process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process1.Start();
            string end1 = process1.StandardOutput.ReadToEnd();
            process1.WaitForExit();
            string str1 = end1;
            char[] chArray1 = new char[1] { '\n' };
            foreach (string str2 in str1.Split(chArray1))
            {
                char[] chArray2 = new char[1] { '=' };
                string[] strArray = str2.Split(chArray2);
                if (strArray[0] == "streams.stream.0.channels")
                {
                    try
                    {
                        string s = strArray[1].Replace("\"", "").TrimEnd('\r', '\n');
                        myVirtualWav.channels = int.Parse(s);
                    }
                    catch
                    {
                    }
                }
                else if (strArray[0] == "streams.stream.0.sample_rate")
                {
                    try
                    {
                        string s = strArray[1].Replace("\"", "").TrimEnd('\r', '\n');
                        myVirtualWav.sampleRate = int.Parse(s);
                    }
                    catch
                    {
                    }
                }
                else if (strArray[0] == "streams.stream.0.bit_rate")
                {
                    try
                    {
                        string s = strArray[1].Replace("\"", "").TrimEnd('\r', '\n');
                        myVirtualWav.originalBitrate = (int)Math.Round(double.Parse(s) / 1000.0, MidpointRounding.AwayFromZero);
                    }
                    catch
                    {
                    }
                }
            }
            if (!(Path.GetExtension(tFile).ToLower() == ".aax") || !this.myAdvancedOptions.decrypt || this.myAdvancedOptions.ng)
                return;
            Process process2 = new Process();
            process2.StartInfo = new ProcessStartInfo();
            process2.StartInfo.FileName = this.supportLibs.instaripPath;
            process2.StartInfo.Arguments = "\"" + tFile + "\" \"temp.aac\" \"" + Path.GetDirectoryName(this.myAdvancedOptions.AudibleMangerDLLPath) + "\\AAXSDKWin.dll\" -keyonly";
            process2.StartInfo.UseShellExecute = false;
            process2.StartInfo.RedirectStandardInput = true;
            process2.StartInfo.RedirectStandardOutput = true;
            process2.StartInfo.RedirectStandardError = true;
            process2.StartInfo.WorkingDirectory = this.myAdvancedOptions.GetTempPath();
            process2.StartInfo.CreateNoWindow = true;
            process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process2.Start();
            string end2 = process2.StandardError.ReadToEnd();
            string end3 = process2.StandardOutput.ReadToEnd();
            process2.WaitForExit();
            end2.Split('\n');
            string str3 = end3;
            char[] chArray3 = new char[1] { '\n' };
            foreach (string str2 in str3.Split(chArray3))
            {
                if (str2.StartsWith("IV"))
                {
                    string[] strArray = str2.Split('=');
                    myVirtualWav.AAXiv = strArray[1].Trim();
                }
                if (str2.StartsWith("Key ="))
                {
                    string[] strArray = str2.Split('=');
                    myVirtualWav.AAXkey = strArray[1].Trim();
                }
            }
        }

        private void GetSampleRateFromInputOld(string tFile, ref VirtualWAV myVirtualWav)
        {
            Process process1 = new Process();
            process1.StartInfo = new ProcessStartInfo();
            process1.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process1.StartInfo.Arguments = "-i \"" + tFile + "\"";
            process1.StartInfo.UseShellExecute = false;
            process1.StartInfo.RedirectStandardInput = true;
            process1.StartInfo.RedirectStandardError = true;
            process1.StartInfo.CreateNoWindow = true;
            process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process1.Start();
            string end1 = process1.StandardError.ReadToEnd();
            process1.WaitForExit();
            string str1 = end1;
            char[] chArray1 = new char[1] { '\n' };
            foreach (string str2 in str1.Split(chArray1))
            {
                if (str2.Trim().StartsWith("Stream #0:0"))
                {
                    myVirtualWav.channels = str2.Contains("stereo") || str2.Contains(" 2 channels") ? 2 : 1;
                    string str3 = str2;
                    char[] chArray2 = new char[1] { ',' };
                    foreach (string str4 in str3.Split(chArray2))
                    {
                        if (str4.Contains(" Hz"))
                            myVirtualWav.sampleRate = int.Parse(str4.Trim().Split(' ')[0]);
                        if (str4.Contains(" kb/s"))
                            myVirtualWav.originalBitrate = int.Parse(str4.Trim().Split(' ')[0]);
                    }
                }
            }
            if (!(Path.GetExtension(tFile).ToLower() == ".aax"))
                return;
            Process process2 = new Process();
            process2.StartInfo = new ProcessStartInfo();
            process2.StartInfo.FileName = this.supportLibs.instaripPath;
            process2.StartInfo.Arguments = "\"" + tFile + "\" \"temp.aac\" \"" + Path.GetDirectoryName(this.myAdvancedOptions.AudibleMangerDLLPath) + "\\AAXSDKWin.dll\" -keyonly";
            process2.StartInfo.UseShellExecute = false;
            process2.StartInfo.RedirectStandardInput = true;
            process2.StartInfo.RedirectStandardOutput = true;
            process2.StartInfo.RedirectStandardError = true;
            process2.StartInfo.WorkingDirectory = this.myAdvancedOptions.GetTempPath();
            process2.StartInfo.CreateNoWindow = true;
            process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process2.Start();
            string end2 = process2.StandardError.ReadToEnd();
            string end3 = process2.StandardOutput.ReadToEnd();
            process2.WaitForExit();
            string str5 = end2;
            char[] chArray3 = new char[1] { '\n' };
            foreach (string str2 in str5.Split(chArray3))
            {
                if (str2.StartsWith("[^] Channels"))
                {
                    string[] strArray = str2.Split('=');
                    myVirtualWav.channels = int.Parse(strArray[1].Trim());
                }
                if (str2.StartsWith("[^] Sample rate"))
                {
                    string[] strArray = str2.Split('=');
                    myVirtualWav.sampleRate = int.Parse(strArray[1].Trim());
                }
            }
            string str6 = end3;
            char[] chArray4 = new char[1] { '\n' };
            foreach (string str2 in str6.Split(chArray4))
            {
                if (str2.StartsWith("IV"))
                {
                    string[] strArray = str2.Split('=');
                    myVirtualWav.AAXiv = strArray[1].Trim();
                }
                if (str2.StartsWith("Key ="))
                {
                    string[] strArray = str2.Split('=');
                    myVirtualWav.AAXkey = strArray[1].Trim();
                }
            }
        }

        private void CreateStudioBundle(string outputFlac)
        {
            string fileName = Path.GetDirectoryName(outputFlac) + "\\" + Path.GetFileNameWithoutExtension(outputFlac) + ".ias";
            InAudible serializableObject = new InAudible();
            serializableObject.chapters = this.originalChapters;
            this.myAudible.decryptedFile = outputFlac;
            serializableObject.audible = this.myAudible;
            serializableObject.audible.advancedOptions.codecOptions = (DataTable)null;
            this.SerializeObject<InAudible>(serializableObject, fileName);
        }

        private List<double> RemoveShortChapters(int threshold, List<double> chaps)
        {
            List<double> doubleList = new List<double>();
            for (int index = 0; index < chaps.Count; ++index)
                doubleList.Add(chaps[index]);
            for (int index = 1; index < doubleList.Count; ++index)
            {
                double num = doubleList[index] - doubleList[index - 1];
                if ((double)threshold > num)
                {
                    if (index == 1)
                        doubleList.RemoveAt(index);
                    else
                        doubleList.RemoveAt(index - 1);
                    this.SetText("Discarding chapter " + (object)(index + 1) + " (" + (object)num + " seconds)");
                }
            }
            return doubleList;
        }

        private double VerifyAAChapterSplit(double chapter, string wavFile)
        {
            double num = chapter;
            int duration = 30;
            double start = chapter - (double)(duration / 2);
            double end = start + (double)duration;
            string tmpFile = Path.GetDirectoryName(this.outputFileMask) + "\\tmpchap.txt";
            List<string> stringList = new List<string>();
            double silenceThreshold = 1.0;
            while (stringList.Count != 1 && silenceThreshold < 4.0)
            {
                this.VerifyAAChapter(wavFile, tmpFile, start, end, silenceThreshold, duration);
                silenceThreshold += 0.25;
                stringList = Form1.ParseSoxOutput(tmpFile);
                if (stringList.Count == 1)
                    break;
            }
            if (stringList.Count == 1)
            {
                string s = stringList[stringList.Count - 1];
                double totalSeconds = TimeSpan.Parse(s).TotalSeconds;
                Audible.diskLogger("Split on silence found at " + s + " - " + (object)totalSeconds);
                num = start + totalSeconds;
                Audible.diskLogger("original split = " + (object)chapter + ", star range: " + (object)start + ", detected offset: " + (object)totalSeconds + ", new = " + (object)num);
            }
            else
                Audible.diskLogger("Could not find any silence.");
            return num;
        }

        private void VerifyAAChapter(string wavFile, string tmpFile, double start, double end, double silenceThreshold, int duration)
        {
            string str1 = "cmd";
            string str2 = "/C \" \"" + this.supportLibs.soxPath + "\" \"" + wavFile + "\" -t wav - trim " + (object)start + " " + (object)duration + " | \"" + this.supportLibs.soxPath + "\" - -n --show-progress silence 1 0 1% 1 " + string.Format("{0:0.0}", (object)silenceThreshold) + " 1% : newfile : restart 2>\"" + tmpFile + "\" \"";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = str1;
            process.StartInfo.Arguments = str2;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }

        public double VerifyChapterSplit(double chapter, VirtualWAV myVirtualWav)
        {
            double num1 = chapter;
            double splitsSearchSize = (double)this.myAdvancedOptions.verifySplitsSearchSize;
            double num2 = chapter - splitsSearchSize;
            double num3 = num2 + splitsSearchSize * 2.0;
            string str = Path.GetDirectoryName(this.myAdvancedOptions.GetTempPath()) + "\\tmpchap.txt";
            EncodingOptions encodingOptions = this.GetEncodingOptions();
            encodingOptions.sampleRate = 44100;
            Lame lame = new Lame();
            encodingOptions.encoder = "detectsilence";
            encodingOptions.startChap = (long)num2 + (long)splitsSearchSize;
            encodingOptions.endChap = (long)num3;
            double num4 = num2 + splitsSearchSize;
            double num5 = num3;
            encodingOptions.dStartChap = num4;
            encodingOptions.dEndChap = num5;
            encodingOptions.doubleChapters = true;
            lame.soxPath = this.supportLibs.soxPath;
            lame.PreprocessVirtualWav(myVirtualWav, str, encodingOptions);
            if (this.myAdvancedOptions.doNotVerifyIfSilence && Form1.DetectSilence(str))
            {
                Audible.diskLogger("Split falls on silence; skipping...");
                return num1;
            }
            encodingOptions.encoder = "soxsilence";
            encodingOptions.startChap = (long)num2;
            encodingOptions.dStartChap = num2;
            List<string> stringList = new List<string>();
            encodingOptions.silenceThreshold = 4.0;
            while (stringList.Count != 1 && encodingOptions.silenceThreshold >= 1.5)
            {
                lame.PreprocessVirtualWav(myVirtualWav, str, encodingOptions);
                encodingOptions.silenceThreshold -= 0.25;
                stringList = Form1.ParseSoxOutput(str);
                if (stringList.Count == 1)
                    break;
            }
            if (stringList.Count == 1)
            {
                string s = stringList[stringList.Count - 1];
                double totalSeconds = TimeSpan.Parse(s).TotalSeconds;
                Audible.diskLogger("Split on silence found at " + s + " - " + (object)totalSeconds);
                num1 = num2 + totalSeconds - encodingOptions.silenceThreshold / 2.0;
                Audible.diskLogger("original split = " + (object)chapter + ", star range: " + (object)num2 + ", detected offset: " + (object)totalSeconds + ", new = " + (object)num1);
            }
            else
                Audible.diskLogger("Could not find any silence.");
            return num1;
        }

        public static bool DetectSilence(string tmpFile)
        {
            string str1 = "";
            int num1 = 0;
            bool flag = true;
            while (str1 == "")
            {
                try
                {
                    str1 = System.IO.File.ReadAllText(tmpFile);
                }
                catch
                {
                    Thread.Sleep(200);
                    ++num1;
                    Audible.diskLogger("deadlock detected");
                }
                if (num1 > 100)
                    break;
            }
            while (System.IO.File.Exists(tmpFile))
            {
                try
                {
                    System.IO.File.Delete(tmpFile);
                }
                catch
                {
                }
                Thread.Sleep(200);
            }
            string[] strArray1 = str1.Split('\r');
            double num2 = 0.0;
            foreach (string str2 in strArray1)
            {
                char[] chArray = new char[1] { ' ' };
                string[] strArray2 = str2.Split(chArray);
                try
                {
                    if (strArray2[0] == "\nMax")
                        num2 = double.Parse(strArray2[strArray2.Length - 1]);
                }
                catch
                {
                }
            }
            if (num2 > 0.05)
                flag = false;
            return flag;
        }

        public static List<string> ParseSoxOutput(string tmpFile)
        {
            string str1 = "";
            int num = 0;
            while (str1 == "")
            {
                try
                {
                    str1 = System.IO.File.ReadAllText(tmpFile);
                }
                catch
                {
                    Thread.Sleep(200);
                    ++num;
                    Audible.diskLogger("deadlock detected");
                }
                if (num > 100)
                    break;
            }
            while (System.IO.File.Exists(tmpFile))
            {
                try
                {
                    System.IO.File.Delete(tmpFile);
                }
                catch
                {
                }
                Thread.Sleep(200);
            }
            List<string> stringList = new List<string>();
            string[] strArray1 = str1.Split('\r');
            for (int index = 0; index < strArray1.Length; ++index)
            {
                if (index > 0 && strArray1[index - 1].StartsWith("In:") && strArray1[index] == "\n")
                {
                    string str2 = strArray1[index - 1].Split(' ')[1];
                    if (!(str2 == ""))
                    {
                        string[] strArray2 = str2.Split(':');
                        string str3 = strArray2[0].Trim() + ":" + strArray2[1].Trim() + ":" + strArray2[2].Trim();
                        stringList.Add(str3);
                    }
                }
            }
            return stringList;
        }

        private EncodingOptions GetEncodingOptions()
        {
            EncodingOptions myEncodingOptions = new EncodingOptions();
            if (this.IsHandleCreated)
                this.Invoke((System.Action)(() => myEncodingOptions.sampleRate = int.Parse(this.cmbSampleRate.Text)));
            myEncodingOptions.channels = !this.chkMono.Checked ? 2 : 1;
            if (this.chkAutodetectChannels.Checked && myEncodingOptions.channels == 1)
                myEncodingOptions.downmix = true;
            if (this.GetSelectedCodec() == "nero" || this.GetSelectedCodec() == "fdk")
                myEncodingOptions.m4bOptions = this.getM4Boptions();
            return myEncodingOptions;
        }

        private void VirtualWAV2Ogg(DecryptAAXOptions data, VirtualWAV myVirtualWav, iTunes myItunes2, EncodingOptions myEncodingOptions)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            LameOptions lameParams = data.lameParams;
            List<double> doubleList = this.myChapters.GetDoubleList();
            IWorkItemResult<int>[] workItemResultArray;
            if (!this.chkKeepMonolithicMP3.Checked)
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            else if (this.chkAudibleSplit.Checked)
            {
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count];
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                lame.oggPath = this.supportLibs.oggPath;
                string outputMP3 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".ogg";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[0];
                int num2 = (int)doubleList[doubleList.Count - 1];
                double num3 = doubleList[0];
                double num4 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                OggOptions oggOptions = new OggOptions();
                oggOptions.myVirtualWav = virtualWav;
                oggOptions.start = (long)num1;
                oggOptions.end = (long)num2;
                oggOptions.fileName = outputMP3;
                oggOptions.encodingArgs = this.oggOptions;
                oggOptions.myAudible = this.myAudible;
                myEncodingOptions.encoder = "ogg";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                myEncodingOptions.oggOptions = oggOptions;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[doubleList.Count - 1] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            else
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            for (int pos = 0; pos < doubleList.Count - 1; ++pos)
            {
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                lame.oggPath = this.supportLibs.oggPath;
                string outputMP3 = myItunes2.inAudibleTargetDir + "\\" + this.myChapters.GetFullFileName(pos, Path.GetFileNameWithoutExtension(this.outputFileName)) + ".ogg";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[pos];
                int num2 = (int)doubleList[pos + 1];
                double num3 = doubleList[pos];
                double num4 = doubleList[pos + 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                OggOptions oggOptions = new OggOptions();
                oggOptions.myVirtualWav = virtualWav;
                oggOptions.start = (long)num1;
                oggOptions.end = (long)num2;
                oggOptions.fileName = outputMP3;
                oggOptions.encodingArgs = this.oggOptions;
                oggOptions.myAudible = this.myAudible;
                oggOptions.trackNum = pos + 1;
                myEncodingOptions.encoder = "ogg";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                myEncodingOptions.oggOptions = oggOptions;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[pos] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            new Stopwatch().Start();
            this.SetText("Encoding...");
            int num5 = 0;
            int num6 = doubleList.Count - 1;
            while (num5 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num5 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num5;
                }
                if (num6 != num5 && num5 > 0)
                {
                    this.SetText("Chapter " + (object)num5 + "/" + (object)workItemResultArray.Length);
                    num6 = num5;
                    this.bgwAA.ReportProgress((int)((double)num5 / (double)workItemResultArray.Length * 100.0));
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }

        private void VirtualWAV2Opus(DecryptAAXOptions data, VirtualWAV myVirtualWav, iTunes myItunes2, EncodingOptions myEncodingOptions)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            LameOptions lameParams = data.lameParams;
            List<double> doubleList = this.myChapters.GetDoubleList();
            IWorkItemResult<int>[] workItemResultArray;
            if (!this.chkKeepMonolithicMP3.Checked)
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            else if (this.chkAudibleSplit.Checked)
            {
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count];
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                lame.opusPath = this.supportLibs.opusPath;
                string outputMP3 = Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".opus";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[0];
                int num2 = (int)doubleList[doubleList.Count - 1];
                double num3 = doubleList[0];
                double num4 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                OpusOptions opusOptions = new OpusOptions();
                opusOptions.myVirtualWav = virtualWav;
                opusOptions.start = (long)num1;
                opusOptions.end = (long)num2;
                opusOptions.fileName = outputMP3;
                opusOptions.encodingArgs = this.opusOptions;
                opusOptions.myAudible = this.myAudible;
                myEncodingOptions.encoder = "opus";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                myEncodingOptions.opusOptions = opusOptions;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[doubleList.Count - 1] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            else
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            for (int pos = 0; pos < doubleList.Count - 1; ++pos)
            {
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                lame.opusPath = this.supportLibs.opusPath;
                string outputMP3 = myItunes2.inAudibleTargetDir + "\\" + this.myChapters.GetFullFileName(pos, Path.GetFileNameWithoutExtension(this.outputFileName)) + ".opus";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[pos];
                int num2 = (int)doubleList[pos + 1];
                double num3 = doubleList[pos];
                double num4 = doubleList[pos + 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                OpusOptions opusOptions = new OpusOptions();
                opusOptions.myVirtualWav = virtualWav;
                opusOptions.start = (long)num1;
                opusOptions.end = (long)num2;
                opusOptions.fileName = outputMP3;
                opusOptions.encodingArgs = this.opusOptions;
                opusOptions.myAudible = this.myAudible;
                myEncodingOptions.encoder = "opus";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                myEncodingOptions.opusOptions = opusOptions;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[pos] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            new Stopwatch().Start();
            this.SetText("Encoding...");
            int num5 = 0;
            int num6 = doubleList.Count - 1;
            while (num5 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num5 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num5;
                }
                if (num6 != num5 && num5 > 0)
                {
                    this.SetText("Chapter " + (object)num5 + "/" + (object)workItemResultArray.Length);
                    num6 = num5;
                    this.bgwAA.ReportProgress((int)((double)num5 / (double)workItemResultArray.Length * 100.0));
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }

        private void VirtualWAV2MP3(DecryptAAXOptions data, VirtualWAV myVirtualWav, iTunes myItunes2, EncodingOptions myEncodingOptions)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            LameOptions lameParams = data.lameParams;
            List<double> doubleList = this.myChapters.GetDoubleList();
            myEncodingOptions.chapters = this.myChapters;
            IWorkItemResult<int>[] workItemResultArray;
            if (!this.chkKeepMonolithicMP3.Checked)
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            else if (this.chkAudibleSplit.Checked)
            {
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count];
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                if (lameParams.mono)
                    lame.SetMono();
                else
                    lame.SetStereo();
                if (!lameParams.vbr)
                {
                    lame.bitrate = lameParams.bitrate;
                    lame.SetCBRMode();
                }
                else
                {
                    lame.vbrQuality = lameParams.vbrQuality;
                    lame.SetVBRMode();
                }
                string outputFileName = this.outputFileName;
                lame.SetOutputFile(outputFileName);
                int num1 = (int)doubleList[0];
                int num2 = (int)doubleList[doubleList.Count - 1];
                double num3 = doubleList[0];
                double num4 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                myEncodingOptions.audible = this.myAudible;
                myEncodingOptions.trackNum = 1;
                myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                    myEncodingOptions.embedCover = false;
                else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                    myEncodingOptions.embedCover = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = "lame";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[doubleList.Count - 1] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputFileName, encodingOptions);
            }
            else
            {
                Audible.diskLogger("oops, this is bad...");
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            }
            for (int pos = 0; pos < doubleList.Count - 1; ++pos)
            {
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                if (lameParams.mono)
                    lame.SetMono();
                else
                    lame.SetStereo();
                if (!lameParams.vbr)
                {
                    lame.bitrate = lameParams.bitrate;
                    lame.SetCBRMode();
                }
                else
                {
                    lame.vbrQuality = lameParams.vbrQuality;
                    lame.SetVBRMode();
                }
                string withoutExtension = Path.GetFileNameWithoutExtension(this.outputFileMask);
                string outputMP3 = myItunes2.inAudibleTargetDir + "\\" + this.myChapters.GetFullFileName(pos, withoutExtension) + ".mp3";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[pos];
                int num2 = (int)doubleList[pos + 1];
                double num3 = doubleList[pos];
                double num4 = doubleList[pos + 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                myEncodingOptions.audible = this.myAudible;
                myEncodingOptions.trackNum = pos + 1;
                myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                    myEncodingOptions.embedCover = false;
                else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                    myEncodingOptions.embedCover = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = "lame";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[pos] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            new Stopwatch().Start();
            this.SetText("Encoding...");
            int num5 = 0;
            int num6 = doubleList.Count - 1;
            while (num5 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num5 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num5;
                }
                if (num6 != num5 && num5 > 0)
                {
                    this.SetText("Chapter " + (object)num5 + "/" + (object)workItemResultArray.Length);
                    num6 = num5;
                    this.bgwAA.ReportProgress((int)((double)num5 / (double)workItemResultArray.Length * 100.0));
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }

        private void VirtualWAV2FFmpeg(DecryptAAXOptions data, VirtualWAV myVirtualWav, iTunes myItunes2, EncodingOptions myEncodingOptions)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            LameOptions lameParams = data.lameParams;
            List<double> doubleList = this.myChapters.GetDoubleList();
            myEncodingOptions.chapters = this.myChapters;
            IWorkItemResult<int>[] workItemResultArray;
            if (!this.chkKeepMonolithicMP3.Checked)
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            else if (this.chkAudibleSplit.Checked)
            {
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count];
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                lame.ffmpegPath = this.supportLibs.ffmpegPath;
                string outputFileName = this.outputFileName;
                lame.SetOutputFile(outputFileName);
                int num1 = (int)doubleList[0];
                int num2 = (int)doubleList[doubleList.Count - 1];
                double num3 = doubleList[0];
                double num4 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                myEncodingOptions.audible = this.myAudible;
                myEncodingOptions.trackNum = 1;
                myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                    myEncodingOptions.embedCover = false;
                else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                    myEncodingOptions.embedCover = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = this.GetSelectedCodec();
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[doubleList.Count - 1] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputFileName, encodingOptions);
            }
            else
            {
                Audible.diskLogger("oops, this is bad...");
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            }
            for (int pos = 0; pos < doubleList.Count - 1; ++pos)
            {
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                if (lameParams.mono)
                    lame.SetMono();
                else
                    lame.SetStereo();
                if (!lameParams.vbr)
                {
                    lame.bitrate = lameParams.bitrate;
                    lame.SetCBRMode();
                }
                else
                {
                    lame.vbrQuality = lameParams.vbrQuality;
                    lame.SetVBRMode();
                }
                string withoutExtension = Path.GetFileNameWithoutExtension(this.outputFileMask);
                string outputMP3 = myItunes2.inAudibleTargetDir + "\\" + this.myChapters.GetFullFileName(pos, withoutExtension) + ".mp3";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[pos];
                int num2 = (int)doubleList[pos + 1];
                double num3 = doubleList[pos];
                double num4 = doubleList[pos + 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                myEncodingOptions.audible = this.myAudible;
                myEncodingOptions.trackNum = pos + 1;
                myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                    myEncodingOptions.embedCover = false;
                else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                    myEncodingOptions.embedCover = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = this.GetSelectedCodec();
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                lame.ffmpegPath = this.supportLibs.ffmpegPath;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[pos] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            new Stopwatch().Start();
            this.SetText("Encoding...");
            int num5 = 0;
            int num6 = doubleList.Count - 1;
            while (num5 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num5 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num5;
                }
                if (num6 != num5 && num5 > 0)
                {
                    this.SetText("Chapter " + (object)num5 + "/" + (object)workItemResultArray.Length);
                    num6 = num5;
                    this.bgwAA.ReportProgress((int)((double)num5 / (double)workItemResultArray.Length * 100.0));
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }

        private void VirtualWAV2WAVs(DecryptAAXOptions data, VirtualWAV myVirtualWav, iTunes myItunes2, EncodingOptions myEncodingOptions)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            LameOptions lameParams = data.lameParams;
            List<double> doubleList = this.myChapters.GetDoubleList();
            IWorkItemResult<int>[] workItemResultArray;
            if (!this.chkKeepMonolithicMP3.Checked)
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            else if (this.chkAudibleSplit.Checked)
            {
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count];
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                if (lameParams.mono)
                    lame.SetMono();
                else
                    lame.SetStereo();
                if (!lameParams.vbr)
                {
                    lame.bitrate = lameParams.bitrate;
                    lame.SetCBRMode();
                }
                else
                {
                    lame.vbrQuality = lameParams.vbrQuality;
                    lame.SetVBRMode();
                }
                string outputFileName = this.outputFileName;
                lame.SetOutputFile(outputFileName);
                int num1 = (int)doubleList[0];
                int num2 = (int)doubleList[doubleList.Count - 1];
                double num3 = doubleList[0];
                double num4 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = "wavbychapters";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[doubleList.Count - 1] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputFileName, encodingOptions);
            }
            else
            {
                Audible.diskLogger("oops, this is bad...");
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            }
            for (int index = 0; index < doubleList.Count - 1; ++index)
            {
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                if (lameParams.mono)
                    lame.SetMono();
                else
                    lame.SetStereo();
                if (!lameParams.vbr)
                {
                    lame.bitrate = lameParams.bitrate;
                    lame.SetCBRMode();
                }
                else
                {
                    lame.vbrQuality = lameParams.vbrQuality;
                    lame.SetVBRMode();
                }
                string outputMP3 = myItunes2.inAudibleTargetDir + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + " - " + (index + 1).ToString("D3") + ".wav";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[index];
                int num2 = (int)doubleList[index + 1];
                double num3 = doubleList[index];
                double num4 = doubleList[index + 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = "wavbychapters";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                lame.soxPath = this.supportLibs.soxPath;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                lame.lamePath = this.supportLibs.lamePath;
                lame.lameCLIoptions = data.lameOptions;
                workItemResultArray[index] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            new Stopwatch().Start();
            this.SetText("Dumping WAVs as chapters...");
            int num5 = 0;
            int num6 = doubleList.Count - 1;
            while (num5 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num5 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num5;
                }
                if (num6 != num5 && num5 > 0)
                {
                    this.SetText("Chapter " + (object)num5 + "/" + (object)workItemResultArray.Length);
                    num6 = num5;
                    this.bgwAA.ReportProgress((int)((double)num5 / (double)workItemResultArray.Length * 100.0));
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }

        private void VirtualWAV2Helix(DecryptAAXOptions data, VirtualWAV myVirtualWav, iTunes myItunes2, EncodingOptions myEncodingOptions)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            LameOptions lameParams = data.lameParams;
            List<double> doubleList = this.myChapters.GetDoubleList();
            IWorkItemResult<int>[] workItemResultArray;
            if (!this.chkKeepMonolithicMP3.Checked)
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            else if (this.chkAudibleSplit.Checked)
            {
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count];
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                string outputFileName = this.outputFileName;
                lame.SetOutputFile(outputFileName);
                int num1 = (int)doubleList[0];
                int num2 = (int)doubleList[doubleList.Count - 1];
                double num3 = doubleList[0];
                double num4 = doubleList[doubleList.Count - 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = "helix";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                myEncodingOptions.lameOptions = data.lameParams;
                lame.soxPath = this.supportLibs.soxPath;
                lame.helixPath = this.supportLibs.helixPath;
                lame.lameCLIoptions = data.lameOptions;
                lame.ffmpegPath = this.supportLibs.ffmpegPath;
                myEncodingOptions.audible = this.myAudible;
                myEncodingOptions.trackNum = 1;
                myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                    myEncodingOptions.embedCover = false;
                else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                    myEncodingOptions.embedCover = true;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                workItemResultArray[doubleList.Count - 1] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputFileName, encodingOptions);
            }
            else
            {
                Audible.diskLogger("oops, this is bad...");
                workItemResultArray = new IWorkItemResult<int>[doubleList.Count - 1];
            }
            for (int pos = 0; pos < doubleList.Count - 1; ++pos)
            {
                Lame lame = new Lame();
                lame.myAdvancedOptions = this.myAdvancedOptions;
                string withoutExtension = Path.GetFileNameWithoutExtension(this.outputFileMask);
                string outputMP3 = myItunes2.inAudibleTargetDir + "\\" + this.myChapters.GetFullFileName(pos, withoutExtension) + ".mp3";
                lame.SetOutputFile(outputMP3);
                int num1 = (int)doubleList[pos];
                int num2 = (int)doubleList[pos + 1];
                double num3 = doubleList[pos];
                double num4 = doubleList[pos + 1];
                myEncodingOptions.dStartChap = num3;
                myEncodingOptions.dEndChap = num4;
                myEncodingOptions.doubleChapters = true;
                VirtualWAV virtualWav = (VirtualWAV)myVirtualWav.Clone();
                myEncodingOptions.encoder = "helix";
                myEncodingOptions.startChap = (long)num1;
                myEncodingOptions.endChap = (long)num2;
                myEncodingOptions.lameOptions = data.lameParams;
                lame.soxPath = this.supportLibs.soxPath;
                lame.helixPath = this.supportLibs.helixPath;
                lame.lameCLIoptions = data.lameOptions;
                lame.ffmpegPath = this.supportLibs.ffmpegPath;
                myEncodingOptions.audible = this.myAudible;
                myEncodingOptions.trackNum = pos + 1;
                myEncodingOptions.doNotTag = this.chkDoNotTag.Checked;
                if (!this.hasCoverArt || !this.chkEmbedCover.Checked)
                    myEncodingOptions.embedCover = false;
                else if (this.chkEmbedCover.Checked && this.hasCoverArt)
                    myEncodingOptions.embedCover = true;
                EncodingOptions encodingOptions = (EncodingOptions)myEncodingOptions.Clone();
                workItemResultArray[pos] = smartThreadPool.QueueWorkItem<VirtualWAV, string, EncodingOptions, int>(new Amib.Threading.Func<VirtualWAV, string, EncodingOptions, int>(lame.PreprocessVirtualWav), virtualWav, outputMP3, encodingOptions);
            }
            new Stopwatch().Start();
            this.SetText("Encoding...");
            int num5 = 0;
            int num6 = doubleList.Count - 1;
            while (num5 != workItemResultArray.Length)
            {
                Thread.Sleep(1000);
                num5 = 0;
                for (int index = 0; index < workItemResultArray.Length; ++index)
                {
                    if (workItemResultArray[index].IsCompleted)
                        ++num5;
                }
                if (num6 != num5 && num5 > 0)
                {
                    this.SetText("Chapter " + (object)num5 + "/" + (object)workItemResultArray.Length);
                    num6 = num5;
                    this.bgwAA.ReportProgress((int)((double)num5 / (double)workItemResultArray.Length * 100.0));
                }
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }

        private int iTunesExternal(string file, double cutStartTime, double endTime)
        {
            int num = 1;
            if (this.rdITunesDefault.Checked)
                num = 1;
            else if (this.rdITunesDesperation.Checked)
                num = 2;
            else if (this.rdITunesManual.Checked)
                num = 0;
            else if (this.rdITunesNone.Checked)
                num = 3;
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.iTunesProxyPath;
            process.StartInfo.Arguments = string.Format("\"{0}\" {1} {2} {3} {4}", (object)file, (object)cutStartTime, (object)endTime, (object)num, (object)this.txtItunesIdleCountdown.Text);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        private void waitForITunes(object sender, DoWorkEventArgs e)
        {
            iTunes iTunes = e.Argument as iTunes;
            while (iTunes.checkForCompletion() != 1)
                Thread.Sleep(100);
            e.Result = (object)iTunes;
        }

        private void decryptAA(string file, string tmpWAVfile, double totalSeconds)
        {
            System.IO.File.Delete(tmpWAVfile);
            IGraphBuilder pGraph = (IGraphBuilder)new FilterGraph();
            if (Form1.BuildGraph(pGraph, file, tmpWAVfile) < 0)
            {
                this.SetText("Something went wrong trying to build the directshow decryption graph.  Aborting.");
            }
            else
            {
                double num1 = 44100.0 * totalSeconds + 44.0;
                IMediaControl mediaControl = (IMediaControl)pGraph;
                IMediaEvent mediaEvent = (IMediaEvent)pGraph;
                int hr = mediaControl.Run();
                this.SetText("Decrypting Audible file...");
                Form1.checkHR(hr, "can't run the graph");
                bool flag = false;
                long num2 = 0;
                int num3 = 0;
                while (!flag)
                {
                    long num4 = new FileInfo(tmpWAVfile).Length / new FileInfo(file).Length;
                    Thread.Sleep(500);
                    long num5 = num2;
                    num2 = new FileInfo(tmpWAVfile).Length;
                    this.bgwAA.ReportProgress((int)((double)num2 / num1 * 100.0));
                    if (num2 - num5 == 0L)
                    {
                        Thread.Sleep(1000);
                        if (num5 - new FileInfo(tmpWAVfile).Length == 0L)
                        {
                            int num6 = (int)((double)num2 / num1 * 100.0);
                            if ((double)num2 < num1 && num6 < 99)
                            {
                                ++num3;
                                if (num3 > 30)
                                {
                                    this.SetText("Failed to reach estimated filesize, but decryption buffer is empty.  Decode aborted at " + (object)num6 + "%.");
                                    mediaControl.Stop();
                                    flag = true;
                                }
                            }
                            else if (num6 >= 99)
                            {
                                Thread.Sleep(1000);
                                mediaControl.Stop();
                                flag = true;
                            }
                        }
                    }
                    EventCode lEventCode;
                    IntPtr lParam1;
                    IntPtr lParam2;
                    while (mediaEvent.GetEvent(out lEventCode, out lParam1, out lParam2, 0) == 0)
                    {
                        if (lEventCode == EventCode.Complete || lEventCode == EventCode.UserAbort)
                        {
                            Console.WriteLine("Done!");
                            flag = true;
                        }
                        else if (lEventCode == EventCode.ErrorAbort)
                        {
                            Console.WriteLine("An error occured: HRESULT={0:X}", (object)lParam1);
                            mediaControl.Stop();
                            flag = true;
                        }
                        mediaEvent.FreeEventParams(lEventCode, lParam1, lParam2);
                    }
                }
                this.bgwAA.ReportProgress(100);
                this.SetText("Decrypted " + num2.ToString("N0") + " raw bytes.");
            }
        }

        private void addM4Btags(Audible myAudible, string outputFileName, string trackNum = "", string trackTotal = "")
        {
            this.myCoverArt.GetImage(Form1.thumbnailDir);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.neroAACtagPath;
            if (trackNum == "")
            {
                trackNum = myAudible.trackNum;
                trackTotal = myAudible.trackTotal;
            }
            if (this.hasCoverArt)
                process.StartInfo.Arguments = string.Format("\"{0}\" -meta:title=\"{1}\" -meta:artist=\"{2}\" -meta:comment=\"{3}\" -meta-user:composer=\"{4}\" -add-cover:front:\"{5}\" -meta:album=\"{6}\" -meta:year=\"{7}\" -meta:track=\"{8}\" -meta:totaltracks=\"{9}\" -meta:genre=\"{10}\"", (object)outputFileName, (object)myAudible.title.Replace("\"", "\\\""), (object)myAudible.author.Replace("\"", "\\\""), (object)myAudible.GetComments().Replace("\"", "\\\""), (object)myAudible.narrator.Replace("\"", "\\\""), (object)myAudible.coverPath, (object)myAudible.album.Replace("\"", "\\\""), (object)myAudible.year, (object)trackNum, (object)trackTotal, (object)myAudible.genre.Replace("\"", "\\\""));
            else
                process.StartInfo.Arguments = string.Format("\"{0}\" -meta:title=\"{1}\" -meta:artist=\"{2}\" -meta:comment=\"{3}\" -meta-user:composer=\"{4}\" -meta:album=\"{5}\" -meta:year=\"{6}\" -meta:track=\"{7}\" -meta:totaltracks=\"{8}\" -meta:genre=\"{9}\"", (object)outputFileName, (object)myAudible.title.Replace("\"", "\\\""), (object)myAudible.author.Replace("\"", "\\\""), (object)myAudible.GetComments().Replace("\"", "\\\""), (object)myAudible.narrator.Replace("\"", "\\\""), (object)myAudible.album.Replace("\"", "\\\""), (object)myAudible.year, (object)trackNum, (object)trackTotal, (object)myAudible.genre.Replace("\"", "\\\""));
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            this.AddAppleTags(outputFileName);
        }

        private void TagM4BTitle(string outputFileName, string title)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.neroAACtagPath;
            process.StartInfo.Arguments = string.Format("\"{0}\" -meta:title=\"{1}\"", (object)outputFileName, (object)title);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

        private void addITunesTags(Audible myAudible, string outputFileName)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.atomicParsleyPath;
            string str1 = "";
            if (this.hasCoverArt)
            {
                this.myCoverArt.GetImage(Form1.thumbnailDir);
                str1 = "--artwork \"" + myAudible.coverPath + "\"";
            }
            string str2 = myAudible.GetComments().Length >= 254 ? myAudible.GetComments().Substring(0, 254) : myAudible.GetComments();
            process.StartInfo.Arguments = "\"" + outputFileName + "\" --title \"" + myAudible.title + "\" --artist \"" + myAudible.author + "\" --comment \"" + str2 + "\" --composer \"" + myAudible.narrator + "\" --year \"" + myAudible.year + "\" --album \"" + myAudible.album + "\" --overWrite " + str1 + " --genre \"" + myAudible.genre + "\" --tracknum \"" + myAudible.trackNum + "/" + myAudible.trackTotal + "\" ";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

        private void fixChapters(string outputFileName)
        {
            string str = Path.GetDirectoryName(outputFileName) + "\\" + Path.GetFileNameWithoutExtension(outputFileName) + ".chapters.txt";
            this.myAudible.createM4BchapterFile(this.myChapters.GetDoubleList(), this.myChapters.GetChapterNames(true), str);
            if (this.GetSelectedCodec() == "lossless")
            {
                int num = this.chkRemoveAudibleMarkers.Checked ? 1 : 0;
            }
            this.SetText("Embedding chapters...");
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.mp4chapsPath;
            process.StartInfo.Arguments = string.Format("-i \"{0}\"", (object)outputFileName);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            try
            {
                System.IO.File.Delete(str);
            }
            catch (Exception ex)
            {
                Audible.diskLogger("Failed to clean up temp chapter file: " + ex.ToString());
            }
        }

        internal void CreateMp4chapsFile(string filename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num1 = 1;
            foreach (double num2 in this.myChapters.GetDoubleList())
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(num2);
                stringBuilder.Append(((int)timeSpan.TotalHours).ToString() + ":" + (object)timeSpan.Minutes + ":" + (object)timeSpan.Seconds + "." + (object)timeSpan.Milliseconds + " ");
                string chapterTitle = this.myAudible.GetChapterTitle(this.myChapters.GetDoubleList(), this.myChapters.GetChapterNames(true), num1 - 1);
                stringBuilder.Append(chapterTitle + "\n");
                ++num1;
            }
            System.IO.File.WriteAllText(filename, stringBuilder.ToString());
        }

        private void importAudibleChapters(string outputFileName)
        {
            this.myAudible.createM4BchapterFile(this.myChapters.GetDoubleList(), this.myChapters.GetChapterNames(true), this.outputFileMask + ".chapters");
            this.SetText("Writing chapters...");
            string str = Path.GetDirectoryName(outputFileName) + "\\tempChaps.mp4";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.mp4boxPath;
            process.StartInfo.Arguments = string.Format("-add \"{0}\" -chap \"{1}\" \"{2}\"", (object)outputFileName, (object)(this.outputFileMask + ".chapters"), (object)str);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            Thread thread = new Thread((ThreadStart)(() => process.WaitForExit()));
            thread.Start();
            while (thread.IsAlive)
            {
                try
                {
                    if (System.IO.File.Exists(str))
                    {
                        long length1 = new FileInfo(str).Length;
                        Thread.Sleep(100);
                        long length2 = new FileInfo(outputFileName).Length;
                        this.bgwAA.ReportProgress((int)((double)length1 / (double)length2 * 100.0));
                    }
                }
                catch
                {
                }
            }
            this.bgwAA.ReportProgress(100);
            bool flag = true;
            while (flag)
            {
                try
                {
                    System.IO.File.Delete(outputFileName);
                    System.IO.File.Delete(this.outputFileMask + ".chapters");
                    System.IO.File.Move(str, outputFileName);
                    flag = false;
                }
                catch
                {
                    flag = true;
                }
            }
        }

        private void FixAACHeader(string outputFileName)
        {
            List<double> doubleList = this.myChapters.GetDoubleList();
            string str = "";
            if (doubleList[0] != 0.0)
                str = " -ss " + ((long)doubleList[0]).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " -t " + ((long)doubleList[doubleList.Count - 1] - 1L).ToString("0.000", (IFormatProvider)CultureInfo.InvariantCulture) + " ";
            string sourceFileName = Path.GetDirectoryName(outputFileName) + "\\tempChaps.mp4";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-y -i \"" + outputFileName + "\" -bsf:a aac_adtstoasc -acodec copy " + str + " \"" + sourceFileName + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            bool flag = true;
            while (flag)
            {
                try
                {
                    System.IO.File.Delete(outputFileName);
                    System.IO.File.Move(sourceFileName, outputFileName);
                    flag = false;
                }
                catch
                {
                    flag = true;
                }
            }
        }

        private void FixITunesWeirdness(string outputFileName)
        {
            string sourceFileName = Path.GetDirectoryName(outputFileName) + "\\tempChaps.mp4";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-y -i \"" + outputFileName + "\" -map 0 -c copy \"" + sourceFileName + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            bool flag = true;
            while (flag)
            {
                try
                {
                    System.IO.File.Delete(outputFileName);
                    System.IO.File.Move(sourceFileName, outputFileName);
                    flag = false;
                }
                catch
                {
                    flag = true;
                }
            }
        }

        private void encodeMP4(string splitDirectory, string outputFile, int bitrate)
        {
            Directory.GetFiles(splitDirectory);
            string str1 = "";
            foreach (string str2 in (IEnumerable<string>)((IEnumerable<string>)Directory.GetFiles(splitDirectory)).OrderBy<string, string>((System.Func<string, string>)(f => f)))
                str1 = str1 + " -if \"" + str2 + "\"";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.neroAACpath;
            process.StartInfo.Arguments = string.Format("-br {0} {1} -of \"{2}\"", (object)bitrate, (object)str1, (object)outputFile);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

        private int split4m4b(string tmpWAVfile, int splitPosition, string splitDirectory)
        {
            string str1 = Form1.appPath + "\\sox.exe";
            Directory.CreateDirectory(splitDirectory);
            int num = splitPosition;
            List<double> doubleList = this.myChapters.GetDoubleList();
            Process process = new Process();
            for (int index = 0; index < doubleList.Count; ++index)
            {
                this.bgwAA.ReportProgress((index + 1) / doubleList.Count * 100);
                ++num;
                string str2 = doubleList.Count != index + 1 ? (doubleList[index + 1] - doubleList[index]).ToString() : "";
                process.StartInfo = new ProcessStartInfo();
                process.StartInfo.FileName = str1;
                process.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" {2} {3} {4}", (object)tmpWAVfile, (object)(splitDirectory + "out" + num.ToString("D3") + ".wav"), (object)"trim", (object)doubleList[index], (object)str2);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    this.SetText("WARNING-SOX failed!");
                    this.SetText("EAA-" + tmpWAVfile + " " + splitDirectory + "out" + index.ToString("D2") + ".wav " + (object)doubleList[index] + " " + (object)doubleList[index + 1]);
                    return num;
                }
            }
            if (!this.aaxMode)
                System.IO.File.Delete(tmpWAVfile);
            return num;
        }

        private string getAudibleTotalTime(string file)
        {
            if (this.myAudible.totalTime != "")
                return this.myAudible.totalTime;
            if (!this.aaMode || !(this.myAudible.codec != "mp332"))
                return this.GetTotalTimeFromAAX(file);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.audibleChaptersPath;
            process.StartInfo.Arguments = string.Format("\"{0}\"", (object)file);
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            string end = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            string[] strArray1 = end.Split('\n');
            string s1 = "";
            bool flag = false;
            List<double> doubleList = new List<double>();
            foreach (string str in strArray1)
            {
                if (str.StartsWith("Chapter"))
                {
                    try
                    {
                        string[] strArray2 = str.Split(':');
                        string s2 = strArray2[1].Trim() + ":" + strArray2[2].Trim() + ":" + strArray2[3].Trim();
                        doubleList.Add(TimeSpan.Parse(s2).TotalSeconds);
                    }
                    catch
                    {
                        flag = true;
                    }
                }
                if (str.StartsWith("Total"))
                {
                    string[] strArray2 = str.Split(':');
                    s1 = strArray2[1].Trim() + ":" + strArray2[2].Trim() + ":" + strArray2[3].Trim();
                }
            }
            if (flag)
            {
                double num1 = (double)new FileInfo(file).Length / 485159.3093;
                int num2 = this.aaxMode ? 1 : 0;
                TimeSpan timeSpan = TimeSpan.FromMinutes(num1);
                s1 = ((int)Math.Floor(timeSpan.TotalHours)).ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2");
                Audible.diskLogger("Calculated total time = " + s1 + ", minutes = " + (object)num1);
            }
            else if (this.aaxMode)
            {
                try
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(TimeSpan.Parse(s1).TotalSeconds * 2.0);
                    s1 = ((int)Math.Floor(timeSpan.TotalHours)).ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2");
                }
                catch
                {
                    double num = (double)new FileInfo(file).Length / 485159.3093;
                    TimeSpan timeSpan = TimeSpan.FromMinutes(num);
                    s1 = ((int)Math.Floor(timeSpan.TotalHours)).ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2");
                    Audible.diskLogger("Calculated total time = " + s1 + ", minutes = " + (object)num);
                }
            }
            this.myAudible.totalTime = s1;
            return s1;
        }

        private int parseEncoding(string fileName)
        {
            int num = 0;
            string[] strArray = fileName.Split('_');
            if (strArray.Length > 1)
            {
                if (strArray[1] == "acelp16")
                    num = 3;
                if (strArray[1] == "acelp85")
                    num = 2;
                if (strArray[1] == "mp332")
                    num = 4;
            }
            return num;
        }

        private void soxPad(string padFile)
        {
            string str1 = Form1.appPath + "\\sox.exe";
            string sourceFileName = Path.GetDirectoryName(padFile) + "\\tmp-" + Path.GetFileName(padFile);
            string str2 = "\"" + padFile + "\" \"" + sourceFileName + "\" pad 0 3";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = str1;
            process.StartInfo.Arguments = string.Format("{0}", (object)str2);
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            System.IO.File.Delete(padFile);
            System.IO.File.Move(sourceFileName, padFile);
        }

        private void soxSplit(string tmpWAVfile, string threshold, string lameArgs)
        {
            string soxPath = this.supportLibs.soxPath;
            string str1 = "--show-progress silence 1 0 1% 1 " + threshold + " 1% : newfile : restart";
            this.SetText("Splitting...");
            this.bgwAA.ReportProgress(0);
            string path = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\split\\";
            Directory.CreateDirectory(path);
            string str2 = path + "out.wav";
            Process process = new Process();
            int num1 = 0;
            if (this.chkFileSplitting.Checked)
            {
                process.StartInfo = new ProcessStartInfo();
                process.StartInfo.FileName = soxPath;
                string str3 = !tmpWAVfile.Contains<char>('"') ? "\"" + tmpWAVfile + "\"" : tmpWAVfile;
                process.StartInfo.Arguments = string.Format("-V2 {0} \"{1}\" {2}", (object)str3, (object)str2, (object)str1);
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    this.SetText("WARNING-SOX failed!");
                    this.SetText("EAA-" + str1 + " " + tmpWAVfile + " " + str2);
                    return;
                }
                num1 = Directory.GetFiles(path).Length;
                this.SetText("Split into " + (object)num1 + " pieces.");
                if (!this.aaxMode)
                    System.IO.File.Delete(tmpWAVfile);
                if (num1 > 500)
                {
                    this.SetText("The chosen split time resulted in too many files.  Aborting...");
                    Directory.Delete(path, true);
                    return;
                }
            }
            else if (this.audibleChapters)
            {
                List<double> doubleList = this.myChapters.GetDoubleList();
                for (int index = 0; index < doubleList.Count; ++index)
                {
                    string str3 = doubleList.Count != index + 1 ? (doubleList[index + 1] - doubleList[index]).ToString() : "";
                    process.StartInfo = new ProcessStartInfo();
                    process.StartInfo.FileName = soxPath;
                    string str4 = !tmpWAVfile.Contains<char>('"') ? "\"" + tmpWAVfile + "\"" : tmpWAVfile;
                    process.StartInfo.Arguments = string.Format("{0} \"{1}\" {2} {3} {4}", (object)str4, (object)(path + "out" + index.ToString("D2") + ".wav"), (object)"trim", (object)doubleList[index], (object)str3);
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        this.SetText("WARNING-SOX failed!");
                        this.SetText("EAA-" + tmpWAVfile + " " + path + "out" + index.ToString("D2") + ".wav trim " + (object)doubleList[index] + " " + str3);
                    }
                }
                if (!this.aaxMode)
                    System.IO.File.Delete(tmpWAVfile);
            }
            int length = Directory.GetFiles(path).Length;
            Thread[] threadArray = new Thread[length];
            if (!this.audibleChapters)
            {
                this.SetText("Padding...");
                int num2 = 0;
                int num3 = ((IEnumerable<string>)Directory.GetFiles(path)).OrderBy<string, string>((System.Func<string, string>)(f => f)).Count<string>();
                foreach (string padFile in (IEnumerable<string>)((IEnumerable<string>)Directory.GetFiles(path)).OrderBy<string, string>((System.Func<string, string>)(f => f)))
                {
                    this.bgwAA.ReportProgress((int)((double)num2 / (double)num3 * 100.0));
                    this.soxPad(padFile);
                    ++num2;
                }
                this.bgwAA.ReportProgress(100);
            }
            if (this.multithreading)
                this.SetText("Encoding...");
            int num4 = 1;
            int index1 = 0;
            foreach (string tmpWAVfile1 in (IEnumerable<string>)((IEnumerable<string>)Directory.GetFiles(path)).OrderBy<string, string>((System.Func<string, string>)(f => f)))
            {
                if (!this.multithreading)
                    this.bgwAA.ReportProgress((int)((double)num4 / (double)num1 * 100.0));
                string mp3File = Path.GetDirectoryName(tmpWAVfile) + "\\" + Path.GetFileNameWithoutExtension(tmpWAVfile) + " - part " + num4.ToString("D3") + ".mp3";
                if (this.multithreading)
                {
                    string tFile = tmpWAVfile1;
                    string tMp3File = mp3File;
                    string sPiece = num4.ToString();
                    if (!this.helix)
                    {
                        threadArray[index1] = new Thread((ThreadStart)(() => this.encodeMP3multi(tFile, tMp3File, Form1.appPath, false, lameArgs, int.Parse(sPiece), this.myAudible)));
                    }
                    else
                    {
                        string helixArgs = "";
                        helixArgs = !this.rdVBR.Checked ? "-B" + (object)this.GetBitrate() : "-V" + this.lblVBRquality.Text;
                        threadArray[index1] = new Thread((ThreadStart)(() => this.encodeHelixMulti(tFile, tMp3File, Form1.appPath, false, helixArgs, int.Parse(sPiece), this.myAudible)));
                    }
                    threadArray[index1].Name = "Encoder" + (object)index1;
                    threadArray[index1].Start();
                    ++index1;
                }
                else
                    this.encodeMP3(tmpWAVfile1, mp3File, num4 - 1, lameArgs);
                this.m3u = this.m3u + Path.GetFileNameWithoutExtension(tmpWAVfile) + " - part " + num4.ToString("D3") + ".mp3\r\n";
                ++num4;
            }
            this.bgwAA.ReportProgress(100);
            if (this.multithreading)
            {
                bool flag = true;
                int num2 = 0;
                while (flag)
                {
                    Thread.Sleep(500);
                    int num3 = 0;
                    for (int index2 = 0; index2 < length; ++index2)
                    {
                        if (!threadArray[index2].IsAlive)
                        {
                            ++num3;
                            if (num2 < num3)
                            {
                                this.bgwAA.ReportProgress((int)((double)num3 / (double)length * 100.0));
                                num2 = num3;
                            }
                            if (num3 == length)
                                flag = false;
                        }
                    }
                }
            }
            this.bgwAA.ReportProgress(100);
            Directory.Delete(path, true);
        }

        private void encodeMP3(string tmpWAVfile, string mp3File, int pieceNum, string lameArgs)
        {
            string str1 = Form1.appPath + "\\lame.exe";
            string str2 = tmpWAVfile;
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            if (!this.helix)
            {
                this.SetText("Encoding with LAME...");
                process.StartInfo.FileName = str1;
                process.StartInfo.Arguments = string.Format("{0} --tt \"{3}\" --ta \"{4}\" --tc \"{5}\" --tn {6} --tl \"{7}\" \"{1}\" \"{2}\"", (object)lameArgs, (object)str2, (object)mp3File, (object)this.myAudible.title, (object)this.myAudible.author, (object)("Narrated by: " + this.myAudible.narrator), (object)pieceNum, (object)this.myAudible.title);
            }
            else
            {
                this.SetText("Encoding with Helix...");
                string str3 = !this.rdVBR.Checked ? "-B" + (object)this.GetBitrate() : "-V" + this.lblVBRquality.Text;
                process.StartInfo.FileName = this.supportLibs.helixPath;
                process.StartInfo.Arguments = string.Format("-U2 -X2 {0} \"{1}\" \"{2}\"", (object)str3, (object)str2, (object)mp3File);
            }
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0 && !this.helix)
            {
                this.SetText("WARNING-LAME failed!");
                this.SetText("EAA-" + lameArgs + " " + str2 + " " + mp3File);
            }
            else
            {
                this.SetText("Compressed MP3 down to " + new FileInfo(mp3File).Length.ToString("N0") + " bytes.");
                System.IO.File.Delete(tmpWAVfile);
            }
        }

        private int encodeMP3multi(string tmpWAVfile, string mp3File, string appPath, bool doNotEncode, string lameArgs, int pieceNum, Audible myAudible)
        {
            if (doNotEncode)
                return 0;
            string str1 = appPath + "\\lame.exe";
            string str2 = tmpWAVfile;
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = str1;
            process.StartInfo.Arguments = string.Format("{0} --tt \"{3}\" --ta \"{4}\" --tc \"{5}\" --tn {6} \"{1}\" \"{2}\"", (object)lameArgs, (object)str2, (object)mp3File, (object)myAudible.title, (object)myAudible.author, (object)("Narrated by: " + myAudible.narrator), (object)pieceNum);
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
                return 0;
            this.tagAndRenameMP3(mp3File, mp3File, pieceNum);
            return 1;
        }

        private int encodeHelixMulti(string tmpWAVfile, string mp3File, string appPath, bool doNotEncode, string lameArgs, int pieceNum, Audible myAudible)
        {
            if (doNotEncode)
                return 0;
            string str1 = appPath + "\\hmp3.exe";
            string str2 = tmpWAVfile;
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = str1;
            process.StartInfo.Arguments = string.Format("-U2 -X2 {0} \"{1}\" \"{2}\"", (object)lameArgs, (object)str2, (object)mp3File);
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            int exitCode = process.ExitCode;
            long length = new FileInfo(mp3File).Length;
            this.tagAndRenameMP3(mp3File, mp3File, pieceNum);
            return 1;
        }

        private static void checkHR(int hr, string msg)
        {
            if (hr >= 0)
                return;
            int num = (int)MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            Console.WriteLine(msg);
        }

        private static int BuildGraph(IGraphBuilder pGraph, string srcFile1, string dstFile)
        {
            int hr1 = ((ICaptureGraphBuilder2)new CaptureGraphBuilder2()).SetFiltergraph(pGraph);
            Form1.checkHR(hr1, "Can't set SetFiltergraph");
            if (hr1 < 0)
                return hr1;
            Guid clsid1 = new Guid("D05F33E0-3F75-11D3-A176-006008944486");
            Guid clsid2 = new Guid("3C78B8E2-6C4D-11D1-ADE2-0000F8754B99");
            IBaseFilter instance1 = (IBaseFilter)Activator.CreateInstance(System.Type.GetTypeFromCLSID(clsid1));
            int hr2 = pGraph.AddFilter(instance1, "Audible Words Codec");
            Form1.checkHR(hr2, "Can't add audible filter");
            if (hr2 < 0)
                return hr2;
            IBaseFilter instance2 = (IBaseFilter)Activator.CreateInstance(System.Type.GetTypeFromCLSID(clsid2));
            int hr3 = pGraph.AddFilter(instance2, "Audible Words Codec");
            Form1.checkHR(hr3, "Can't add WAV Dest filter");
            if (hr3 < 0)
                return hr3;
            IBaseFilter baseFilter1 = (IBaseFilter)new AsyncReader();
            int hr4 = pGraph.AddFilter(baseFilter1, "File Source (Async.)");
            Form1.checkHR(hr4, "Can't find file source filter");
            if (hr4 < 0)
                return hr4;
            IFileSourceFilter fileSourceFilter = baseFilter1 as IFileSourceFilter;
            if (fileSourceFilter == null)
                Form1.checkHR(-2147467262, "Can't get source filter");
            int hr5 = fileSourceFilter.Load(srcFile1, (AMMediaType)null);
            Form1.checkHR(hr5, "Can't load file");
            if (hr5 < 0)
                return hr5;
            IBaseFilter baseFilter2 = (IBaseFilter)new FileWriter();
            int hr6 = pGraph.AddFilter(baseFilter2, "File Writer");
            Form1.checkHR(hr6, "Can't add File Writer to graph");
            if (hr6 < 0)
                return hr6;
            int hr7 = (baseFilter2 as IFileSinkFilter).SetFileName(dstFile, (AMMediaType)null);
            Form1.checkHR(hr7, "Can't set filename");
            if (hr7 < 0)
                return hr7;
            int hr8 = pGraph.ConnectDirect(Form1.GetPin(baseFilter1, "Output"), Form1.GetPin(instance1, "Input"), (AMMediaType)null);
            Form1.checkHR(hr8, "Can't connect source file to audible codec");
            if (hr8 < 0)
                return hr8;
            int hr9 = pGraph.ConnectDirect(Form1.GetPin(instance1, "Output"), Form1.GetPin(instance2, "In"), (AMMediaType)null);
            Form1.checkHR(hr9, "Can't connect audible codect to wav");
            if (hr9 < 0)
                return hr9;
            int hr10 = pGraph.ConnectDirect(Form1.GetPin(instance2, "Out"), Form1.GetPin(baseFilter2, "in"), (AMMediaType)null);
            Form1.checkHR(hr10, "Can't wav to writer");
            if (hr10 < 0)
                return hr10;
            return hr10;
        }

        private static IPin GetPin(IBaseFilter filter, string pinname)
        {
            IEnumPins ppEnum;
            DsError.ThrowExceptionForHR(filter.EnumPins(out ppEnum));
            IntPtr pcFetched = Marshal.AllocCoTaskMem(4);
            IPin[] ppPins = new IPin[1];
            while (ppEnum.Next(1, ppPins, pcFetched) == 0)
            {
                PinInfo pInfo;
                ppPins[0].QueryPinInfo(out pInfo);
                bool flag = pInfo.name == pinname;
                DsUtils.FreePinInfo(pInfo);
                if (flag)
                    return ppPins[0];
            }
            DsError.ThrowExceptionForHR(-1);
            return (IPin)null;
        }

        private void btnInputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All supported formats|*.aa;*.aax;*.ias;*.m4b;*.m4a;*.mp4;*.aac;*.m4p;*.opf;*.ppf|Audible files (*.aa, *.aax, *.ias)|*.aa;*.aax;*.ias|M4B files (*.m4b, *.m4a, *.mp4, *.aac)|*.m4b;*.m4a;*.mp4;*.aac|NLS/BARD files (*.opf, *.ppf)|*.opf;*.ppf|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            this.bardMode = false;
            string lower = Path.GetExtension(openFileDialog.FileNames[0]).ToLower();
            if (lower == ".mp3")
            {
                this.SetText("MAROON-Switching to MP3 Processing Mode...");
                this.MP3Processing(openFileDialog.FileNames);
            }
            else if (lower == ".aa" || lower == ".aax" || (lower == ".m4b" || lower == ".m4a") || (lower == ".mp4" || lower == ".aac"))
                this.LoadAudibleFiles(openFileDialog.FileNames);
            else if (lower == ".ppf" || lower == ".opf")
            {
                this.LoadBARDFile(openFileDialog.FileNames[0]);
            }
            else
            {
                this.SetText("MAROON-Generic processing...");
                this.OmniProcess(openFileDialog.FileNames);
            }
        }

        private void LoadBARDFile(string bardMetadataFile)
        {
            this.bardMode = true;
            this.txtInputFile.Text = bardMetadataFile;
            this.ParseBARDMetadata(bardMetadataFile);
            this.btnEditChapters.Visible = true;
            string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string str2 = this.myAudible.title;
            foreach (char ch in str1)
                str2 = str2.Replace(ch.ToString(), "");
            this.outputFileMask = Path.GetDirectoryName(bardMetadataFile) + "\\" + str2 + ".mp3";
            this.txtOutputFile.Text = this.outputFileMask;
            this.saveFileDialog1.FileName = str2 + ".mp3";
            this.inputFileName = this.txtInputFile.Text;
            this.outputFileName = this.txtOutputFile.Text;
            this.myBardFile = new BARD(bardMetadataFile, Path.GetDirectoryName(this.iniPath));
            if (!this.myBardFile.HasValidUser())
            {
                FormBardAccount formBardAccount = new FormBardAccount();
                formBardAccount.txtUser.Text = this.myAdvancedOptions.BardUser;
                formBardAccount.txtPassword.Text = this.myAdvancedOptions.BardPassword;
                int num = (int)formBardAccount.ShowDialog();
                if (formBardAccount.applied)
                {
                    this.myBardFile.userName = formBardAccount.txtUser.Text;
                    this.myAdvancedOptions.BardUser = this.myBardFile.userName;
                    this.myBardFile.userPass = formBardAccount.txtPassword.Text;
                    this.myAdvancedOptions.BardPassword = this.myBardFile.userPass;
                    this.myBardFile.GetAccountKey();
                }
                else
                {
                    this.SetText("No valid account supplied; aborting...");
                    return;
                }
            }
            else
                Audible.diskLogger("Has valid account key.");
            if (!this.myBardFile.HasDecryptionKey())
            {
                if (this.myAdvancedOptions.BardUser == null || this.myAdvancedOptions.BardUser == "")
                {
                    this.LoadPrefs("Other");
                    this.myAdvancedOptions.okayToSaveSettings = true;
                }
                if (this.myAdvancedOptions.BardUser == null || this.myAdvancedOptions.BardUser == "")
                {
                    FormBardAccount formBardAccount = new FormBardAccount();
                    formBardAccount.txtUser.Text = this.myAdvancedOptions.BardUser;
                    formBardAccount.txtPassword.Text = this.myAdvancedOptions.BardPassword;
                    int num = (int)formBardAccount.ShowDialog();
                    if (formBardAccount.applied)
                    {
                        this.myBardFile.userName = formBardAccount.txtUser.Text;
                        this.myAdvancedOptions.BardUser = this.myBardFile.userName;
                        this.myBardFile.userPass = formBardAccount.txtPassword.Text;
                        this.myAdvancedOptions.BardPassword = this.myBardFile.userPass;
                    }
                }
                else
                    this.SetText("Using cached credentials for user: " + this.myAdvancedOptions.BardUser);
                this.SetText("Pulling decryption key...");
                this.myBardFile.userName = this.myAdvancedOptions.BardUser;
                this.myBardFile.userPass = this.myAdvancedOptions.BardPassword;
                this.myBardFile.GetBookKey();
            }
            else
                this.SetText("Using cached decryption key...");
            this.SetText("Decrypting...");
            this.myBardFile.Decrypt();
            this.BARDProcessing(this.myBardFile.fileList);
        }

        private void ParseBARDMetadata(string bardMetadataFile)
        {
            string withoutExtension = Path.GetFileNameWithoutExtension(bardMetadataFile);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.XmlResolver = (XmlResolver)null;
            xmlDocument.LoadXml(System.IO.File.ReadAllText(bardMetadataFile));
            foreach (XmlNode childNode1 in xmlDocument.DocumentElement.ChildNodes)
            {
                if (childNode1.Name == "metadata")
                {
                    foreach (XmlNode childNode2 in childNode1.ChildNodes)
                    {
                        if (childNode2.Name == "dc-metadata")
                        {
                            foreach (XmlNode childNode3 in childNode2.ChildNodes)
                            {
                                if (childNode3.Name == "dc:Title")
                                    this.myAudible.title = childNode3.InnerText;
                                if (childNode3.Name == "dc:Creator")
                                    this.myAudible.author = childNode3.InnerText;
                                if (childNode3.Name == "dc:Description")
                                    this.myAudible.SetComments(childNode3.InnerText);
                            }
                        }
                        if (childNode2.Name == "x-metadata")
                        {
                            foreach (XmlNode childNode3 in childNode2.ChildNodes)
                            {
                                if (childNode3.Attributes["name"].Value != null && childNode3.Attributes["name"].Value == "dtb:sourceDate")
                                    this.myAudible.year = childNode3.Attributes["content"].Value;
                                if (childNode3.Attributes["name"].Value != null && childNode3.Attributes["name"].Value == "dtb:narrator")
                                    this.myAudible.narrator = childNode3.Attributes["content"].Value;
                                if (childNode3.Attributes["name"].Value != null && childNode3.Attributes["name"].Value == "dtb:sourcePublisher")
                                    this.myAudible.publisher = childNode3.Attributes["content"].Value;
                                if (childNode3.Attributes["name"].Value != null && childNode3.Attributes["name"].Value == "nls:labelPrintTitle")
                                    this.myAudible.album = childNode3.Attributes["content"].Value;
                            }
                        }
                    }
                }
            }
            if (this.myAudible.title != null && !(this.myAudible.title == ""))
                return;
            this.myAudible.title = withoutExtension;
        }

        public void LoadAudibleFiles(string[] inputFiles)
        {
            this.rtbLog.Clear();
            this.splitPoints.Clear();
            this.cdProcessingMode = false;
            this.flacUnpacked = false;
            this.finishedBatch = false;
            this.omniMode = false;
            this.myCoverArt.Init();
            this.myAudible = new Audible();
            int num1 = 0;
            int num2 = 0;
            foreach (string inputFile in inputFiles)
            {
                if (Path.GetExtension(inputFile) == ".aa")
                    ++num1;
                if (Path.GetExtension(inputFile) == ".aax")
                    ++num2;
            }
            if (num1 > 0 && num2 > 0)
            {
                int num3 = (int)MessageBox.Show("You cannot process AA and AAX files as part of the same batch.", "Warning");
            }
            else
            {
                this.reRun = false;
                this.myChapters.customChapters = false;
                this.myChapters.Clear();
                this.myChapters.customChapterNames = false;
                Utilities utilities = new Utilities();
                utilities.StopwatchStart();
                this.noUIupdates = true;
                this.myBatchFiles.AddInputFiles(inputFiles);
                if (this.myBatchFiles.inputFiles.Length > 1 && num1 == 0)
                {
                    this.SetText("MAROON-Multiple input files selected.");
                    this.btnBatchEdit.Enabled = true;
                }
                else
                    this.btnBatchEdit.Enabled = false;
                this.addAudibleFiles(this.myBatchFiles);
                utilities.StopwatchStop();
                utilities.StopwatchGetElapsed("add files");
                this.noUIupdates = false;
                utilities.StopwatchStart();
                this.setEncodingUI();
                utilities.StopwatchStop();
                utilities.StopwatchGetElapsed("encoding UI");
                try
                {
                    string str = Path.GetDirectoryName(this.GetFuturePath()) + "\\" + Path.GetFileNameWithoutExtension(this.GetFuturePath()) + ".inaudible";
                    if (!System.IO.File.Exists(str) || !this.chkRerun.Checked)
                        return;
                    InAudible inAudible = this.DeSerializeObject<InAudible>(str);
                    this.savedInitialDirs = inAudible.savedInitialDirs;
                    this.savedWorkingDirs = inAudible.savedWorkingDirs;
                    this.reRun = true;
                    this.SetText("WARNING-Loading InAudible session.");
                }
                catch
                {
                }
            }
        }

        private void SetChapterUI()
        {
            string str = "Adjust Chapters (" + (object)this.myChapters.Count() + ")";
            this.btnEditChapters.Text = str;
            this.chapterEditorToolStripMenuItem.Enabled = true;
            this.chapterEditorToolStripMenuItem.Text = str;
            this.btnChapterMetadata.Enabled = true;
            this.chapterMetadataToolStripMenuItem.Enabled = true;
        }

        private void addAudibleFiles(BatchFiles files)
        {
            Utilities utilities = new Utilities();
            utilities.StopwatchStart();
            this.grpLame.Enabled = true;
            this.grpSplitting.Enabled = true;
            this.grpOutputType.Enabled = true;
            this.mergeMode = false;
            Array.Sort<string>(files.inputFiles);
            this.txtInputFile.Text = string.Join(this.fileDeliminator.ToString(), files.inputFiles);
            this.numInputFiles = files.inputFiles.Length;
            string lower = Path.GetExtension(files.inputFiles[0]).ToLower();
            if (lower == ".aa")
            {
                this.currentProcessingMode = Form1.ProcessingMode.AA;
                this.aaxMode = false;
                this.aaMode = true;
                this.m4pMode = false;
                this.finishedBatch = false;
                this.myAudible.getMetaData(files.inputFiles[0]);
                this.myAudible.nfo.sourceFormat = "Audible AA";
                this.GetAACoverArt(files.inputFiles[0]);
                this.SetAAoptions();
                this.studioProcessingMode = false;
                this.btnEditChapters.Visible = true;
                this.SetLosslessMode("MP3");
            }
            else if (lower == ".aax")
            {
                this.currentProcessingMode = Form1.ProcessingMode.AAX;
                this.aaxMode = true;
                this.m4pMode = false;
                this.aaMode = false;
                this.myAudible.GetM4BMetaDataTagLib(files.inputFiles[0]);
                utilities.StopwatchGetElapsed("get metadata");
                utilities.StopwatchStart();
                this.aaxOnce = true;
                this.myAudible.nfo.sourceFormat = "Audible AAX";
                if (this.myAdvancedOptions.audibleCustomerId == null || this.myAdvancedOptions.audibleCustomerId == "")
                    this.myAdvancedOptions.audibleCustomerId = this.myAudible.guid;
                this.studioProcessingMode = false;
                this.m4bTranscodeMode = false;
                this.SetLosslessMode("AAC/M4B");
                this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(files.inputFiles[0]));
                this.SetChapterUI();
                this.setAAXoptions(Form1.ProcessingMode.AAX, files.inputFiles[0]);
                if (this.chkStripUnabridged.Checked)
                    this.myAudible = this.StripUnabridged(this.myAudible);
                Audible.diskLogger("Audible Book ID = " + this.myAudible.id);
            }
            else if (lower == ".m4p")
            {
                this.currentProcessingMode = Form1.ProcessingMode.AAX;
                this.aaxMode = true;
                this.m4pMode = true;
                this.aaMode = false;
                this.myAudible.GetCustomM4PMetaData(files.inputFiles[0]);
                this.aaxOnce = true;
                this.myAudible.nfo.sourceFormat = "iTunes M4P";
                if (this.myAdvancedOptions.audibleCustomerId == null || this.myAdvancedOptions.audibleCustomerId == "")
                    this.myAdvancedOptions.audibleCustomerId = this.myAudible.guid;
                this.studioProcessingMode = false;
                this.m4bTranscodeMode = false;
                this.setAAXoptions(Form1.ProcessingMode.AAX, files.inputFiles[0]);
                this.txtITunesPassSize.Text = "1";
                this.SetLossyMode();
            }
            else if (lower == ".m4b" || lower == ".m4a" || (lower == ".mp4" || lower == ".aac"))
            {
                this.currentProcessingMode = Form1.ProcessingMode.AAX;
                this.aaxMode = true;
                this.m4pMode = false;
                this.myAudible.ffmpegPath = this.supportLibs.ffmpegPath;
                this.myAudible.GetM4BMetaDataTagLib(files.inputFiles[0]);
                this.myAudible.decryptedFile = files.inputFiles[0];
                this.aaxOnce = true;
                this.myAudible.nfo.sourceFormat = "AAC";
                this.studioProcessingMode = true;
                this.m4bTranscodeMode = true;
                this.btnEditChapters.Visible = true;
                this.myChapters = this.myAudible.GetM4BChapters(files.inputFiles[0]);
                this.SetChapterUI();
                this.setAAXoptions(Form1.ProcessingMode.AAX, files.inputFiles[0]);
                if (files.inputFiles.Length > 1)
                    this.mergeMode = MessageBox.Show("You have selected mutiple M4B files.  Would you like to treat them as one book?", "Batch or Merge", MessageBoxButtons.YesNo) == DialogResult.Yes;
                this.SetLossyMode();
                this.SetText("M4B mode active.");
            }
            else if (lower == ".ias")
            {
                this.currentProcessingMode = Form1.ProcessingMode.AAX;
                this.aaxMode = true;
                this.aaxOnce = true;
                this.studioProcessingMode = true;
                this.m4bTranscodeMode = false;
                InAudible inAudible = this.DeSerializeObject<InAudible>(files.inputFiles[0]);
                this.myChapters.SetDoubleChapters(inAudible.chapters);
                this.myAudible = inAudible.audible;
                this.btnEditChapters.Visible = true;
                this.setAAXoptions(Form1.ProcessingMode.AAX, "");
                this.SetText("WARNING-Studio mode active.");
            }
            string asciiTag = this.myAudible.GetASCIITag(this.myAudible.title);
            this.outputFileMask = this.myAdvancedOptions.outputPath == null || this.myAdvancedOptions.outputPath == "" || !Directory.Exists(this.myAdvancedOptions.outputPath) ? Path.GetDirectoryName(files.inputFiles[0]) + "\\" + asciiTag + ".mp3" : this.myAdvancedOptions.outputPath + "\\" + asciiTag + ".mp3";
            this.txtOutputFile.Text = this.outputFileMask;
            this.saveFileDialog1.FileName = asciiTag + ".mp3";
            this.inputFileName = this.txtInputFile.Text;
            this.outputFileName = this.txtOutputFile.Text;
            this.SetText("Book: " + this.myAudible.title);
            this.SetText("Author: " + this.myAudible.author);
            this.SetText("Narrator: " + this.myAudible.narrator);
            this.SetText("Year: " + this.myAudible.year);
            this.myAudible.nfo.totalTime = this.getAudibleTotalTime(files.inputFiles[0]);
            if (this.aaMode)
                this.SetText("Total Time: " + this.myAudible.nfo.totalTime);
            else
                this.SetText("Total Time: " + this.myAudible.nfo.totalTime + " in " + (object)this.myChapters.Count() + " chapters");
            this.SetText("Detected " + this.myAudible.codec + " encoding.\r\n");
            if (this.m4bTranscodeMode || this.aaxMode)
            {
                VirtualWAV myVirtualWav = new VirtualWAV();
                this.GetSampleRateFromInput(files.inputFiles[0], ref myVirtualWav);
                this.SetText("WARNING-Source is " + (object)myVirtualWav.originalBitrate + " kbits @ " + (object)myVirtualWav.sampleRate + "Hz, " + (object)myVirtualWav.channels + " channels");
                this.myAudible.nfo.sourceSampleRate = myVirtualWav.sampleRate.ToString();
                this.myAudible.nfo.sourceChannels = myVirtualWav.channels.ToString();
                this.myAudible.nfo.sourceBitrate = myVirtualWav.originalBitrate.ToString();
            }
            else
            {
                if (!this.aaMode)
                    return;
                this.SetText("WARNING-Source is " + (object)32 + " kbits @ " + (object)22050 + "Hz, " + (object)1 + " channels");
                this.myAudible.nfo.sourceSampleRate = "32";
                this.myAudible.nfo.sourceChannels = "22050";
                this.myAudible.nfo.sourceBitrate = "1";
            }
        }

        private Audible StripUnabridged(Audible ma)
        {
            if (this.myAudible.title != null)
            {
                ma.title = ma.title.Replace(" (Unabridged)", "");
                ma.album = ma.album.Replace(" (Unabridged)", "");
            }
            return ma;
        }

        private bool ScrapeAAJPG(string file)
        {
            try
            {
                string path = Form1.thumbnailDir + "\\" + this.myCoverArt.GenerateFileName();
                long length1 = new FileInfo(file).Length;
                long length2 = 100000;
                if (length1 < length2)
                    length2 = length1;
                byte[] buffer = new byte[length2];
                long offset = length1 - length2;
                using (BinaryReader binaryReader = new BinaryReader((Stream)new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    binaryReader.Read(buffer, 0, (int)length2);
                }
                byte[] bytes1 = Encoding.ASCII.GetBytes("JFIF");
                long sourceIndex = Audible.ByteSearchBHM(buffer, bytes1, 1) - 6L;
                long length3 = length2 - sourceIndex;
                byte[] bytes2 = new byte[length3];
                Array.Copy((Array)buffer, sourceIndex, (Array)bytes2, 0L, length3);
                System.IO.File.WriteAllBytes(path, bytes2);
                return true;
            }
            catch (Exception ex)
            {
                Audible.diskLogger("Failed to find cover art in AA file: " + ex.ToString());
                return false;
            }
        }

        private void GetAACoverArt(string file)
        {
            bool gotCover = false;
            Task.Factory.StartNew((System.Action)(() => gotCover = this.ScrapeAAJPG(file))).ContinueWith((System.Action<Task>)(t =>
          {
              try
              {
                  if (!gotCover)
                      return;
                  string filename = Form1.thumbnailDir + "\\" + this.myCoverArt.GetImage(Form1.thumbnailDir);
                  Image image = (Image)new Bitmap(filename);
                  this.pbCover.Image = image.GetThumbnailImage(89, 80, (Image.GetThumbnailImageAbort)null, new IntPtr());
                  image.Dispose();
                  this.myAudible.coverPath = filename;
                  this.pbCover.Visible = true;
                  this.hasCoverArt = true;
                  this.chkEmbedCover.Visible = true;
                  this.chkSaveCover.Visible = true;
              }
              catch (Exception ex)
              {
                  Audible.diskLogger("Cover art fail: " + ex.ToString());
              }
          }), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private bool GetCoverFromAudible(string audibleId)
        {
            string address1 = "http://www.audible.com/pd/" + audibleId;
            try
            {
                string str1 = new WebClient().DownloadString(address1);
                int startIndex1 = str1.IndexOf("adbl-prod-detail-main");
                if (startIndex1 < 0)
                {
                    Audible.diskLogger("This title could not be found on Audible.com");
                    return false;
                }
                string str2 = str1.Substring(startIndex1);
                int startIndex2 = str2.IndexOf("src=");
                string address2 = str2.Substring(startIndex2, 200).Split('"')[1].Replace("SL300", "SS500");
                if (!Directory.Exists(Form1.thumbnailDir))
                    Directory.CreateDirectory(Form1.thumbnailDir);
                string str3 = Form1.thumbnailDir + "\\" + this.myCoverArt.GenerateFileName();
                while (System.IO.File.Exists(str3))
                {
                    try
                    {
                        System.IO.File.Delete(str3);
                    }
                    catch
                    {
                        Thread.Sleep(100);
                    }
                }
                new WebClient().DownloadFile(address2, str3);
                Audible.diskLogger("Successfully scraped cover from Audible.com");
                return true;
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
                return false;
            }
        }

        private string GetAudibleDotComInfo(string audibleId)
        {
            if (audibleId == null || audibleId == "")
                return "";
            string address = "http://www.audible.com/pd/" + audibleId;
            try
            {
                string str1 = new WebClient().DownloadString(address);
                int startIndex = str1.IndexOf("adbl-prod-detail-cont");
                int num1 = str1.IndexOf("adbl-sample-cont");
                if (startIndex < 0)
                {
                    Audible.diskLogger("This title could not be found on Audible.com");
                    return "";
                }
                string str2 = str1.Substring(startIndex, num1 - startIndex);
                string str3 = "itemprop=\"name\">";
                int length = str2.LastIndexOf(str3);
                string str4 = str2.Substring(0, length);
                int num2 = str4.LastIndexOf(str3);
                string str5 = str4.Substring(num2 + str3.Length);
                return str5.Substring(0, str5.IndexOf("<"));
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
                Audible.diskLogger("Book URL = " + address);
                return "";
            }
        }

        private void SetAAoptions()
        {
            this.currentProcessingMode = Form1.ProcessingMode.AA;
            if (this.firstRun && this.CurrentPrefs("AA"))
            {
                this.firstRun = false;
                this.LoadPrefs("AA");
            }
            this.cmbSampleRate.Text = "22050";
            this.chkMono.Enabled = false;
            this.chkMono.Checked = true;
            this.chkAutodetectChannels.Enabled = false;
            this.btnLossless.Visible = true;
            this.chkAdvancedCodecs.Checked = false;
            this.chkAdvancedCodecs.Enabled = false;
        }

        private List<double> GetM4BSplitPoints(List<double> tmpChaps, double totalSeconds)
        {
            List<double> doubleList = new List<double>();
            double num1 = (double)(3600 * this.myAdvancedOptions.iOSSplitSize);
            double num2 = num1;
            doubleList.Add(0.0);
            for (int index = 0; index < tmpChaps.Count; ++index)
            {
                if (tmpChaps[index] > num2)
                {
                    doubleList.Add(tmpChaps[index - 1]);
                    num2 += num1;
                }
            }
            if (totalSeconds - doubleList[doubleList.Count - 1] < (double)(this.myAdvancedOptions.iOSMinSplitSize * 60 * 60))
                doubleList.RemoveAt(doubleList.Count - 1);
            return doubleList;
        }

        private bool checkAvailableSpace(string file)
        {
            string[] strArray = this.getAudibleTotalTime(file).Split(':');
            double num1 = (double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2])) * 352.0 * 1024.0 / 8.0;
            string pathRoot = Path.GetPathRoot(this.outputFileMask);
            double num2 = 0.0;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.Name.Contains(pathRoot))
                    num2 = (double)drive.AvailableFreeSpace;
            }
            bool flag = num2 >= num1;
            Audible.diskLogger("required = " + num1.ToString("N0") + ", free = " + num2.ToString("N0"));
            return flag;
        }

        private void setAAXoptions(Form1.ProcessingMode mode, string aaxFile = "")
        {
            this.btnLossless.Visible = true;
            this.chkKeepMonolithicMP3.Visible = true;
            this.chkAdvancedCodecs.Enabled = true;
            if (this.firstRun)
            {
                if (mode == Form1.ProcessingMode.AAX)
                {
                    if (this.CurrentPrefs("AAX"))
                    {
                        this.firstRun = false;
                        this.LoadPrefs("AAX");
                    }
                }
                else if (mode == Form1.ProcessingMode.AA)
                {
                    if (this.CurrentPrefs("AA"))
                    {
                        this.firstRun = false;
                        this.LoadPrefs("AA");
                    }
                }
                else if (mode == Form1.ProcessingMode.Other && this.CurrentPrefs("Other"))
                {
                    this.firstRun = false;
                    this.LoadPrefs("Other");
                }
            }
            else if (!this.aaxOnce)
            {
                this.chkAutodetectChannels.Enabled = true;
                this.SetBitrate(64);
                this.chkAudibleSplit.Checked = true;
                this.chkMultithread.Checked = false;
                this.chkVerifyAudibleSplits.Enabled = true;
                if (!this.chkAutodetectChannels.Checked)
                {
                    this.chkMono.Enabled = true;
                    this.chkMono.Checked = false;
                }
            }
            bool gotCover = false;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (this.cdProcessingMode)
                return;
            Task.Factory.StartNew((System.Action)(() =>
           {
               if (this.myAdvancedOptions.AudibleScraping && this.myAudible.id != null)
                   this.myAudible.genre = this.GetAudibleDotComInfo(this.myAudible.id);
               if (aaxFile != "")
               {
                   gotCover = this.GetCoverFromAAX(aaxFile);
                   if (gotCover)
                       return;
                   gotCover = this.GetCoverFromAudible(this.myAudible.id);
               }
               else
               {
                   if (!this.studioProcessingMode)
                       return;
                   gotCover = this.GetCoverFromAudible(this.myAudible.id);
               }
           })).ContinueWith((System.Action<Task>)(t =>
     {
               try
               {
                   if (gotCover)
                   {
                       string filename = Form1.thumbnailDir + "\\" + this.myCoverArt.GetImage(Form1.thumbnailDir);
                       Image image = (Image)new Bitmap(filename);
                       this.pbCover.Image = image.GetThumbnailImage(89, 80, (Image.GetThumbnailImageAbort)null, new IntPtr());
                       this.pbCover.SizeMode = PictureBoxSizeMode.StretchImage;
                       image.Dispose();
                       this.myAudible.coverPath = filename;
                       this.pbCover.Visible = true;
                       this.hasCoverArt = true;
                       this.chkEmbedCover.Visible = true;
                       this.chkSaveCover.Visible = true;
                       stopWatch.Stop();
                       TimeSpan elapsed = stopWatch.Elapsed;
                       Audible.diskLogger("Cover art scraped in " + string.Format("{0:00}:{1:00}:{2:00}.{3:000}", (object)elapsed.Hours, (object)elapsed.Minutes, (object)elapsed.Seconds, (object)(elapsed.Milliseconds / 10)));
                   }
                   else
                   {
                       this.bgWorker1.DoWork += new DoWorkEventHandler(this.myBackgroundWorker_DoWork);
                       this.bgWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.myBackgroundWorker_RunWorkerCompleted);
                       this.FetchResults(this.myAudible.author + " " + this.myAudible.title, true);
                   }
               }
               catch (Exception ex)
               {
                   Audible.diskLogger("Cover art fail: " + ex.ToString());
               }
           }), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SetBitrate(string bitrate)
        {
            int result = 0;
            if (int.TryParse(bitrate, out result))
                this.SetBitrate(result);
            else
                this.SetBitrate(64);
        }

        private void SetBitrate(int bitrate)
        {
            this.cmbBitrate.SelectedItem = (object)bitrate.ToString();
            this.cmbBitrate.Text = bitrate.ToString();
        }

        private bool GetCoverFromAnything(string aaxFile)
        {
            try
            {
                TagLib.File file = TagLib.File.Create(aaxFile);
                if (file.Tag.Pictures.Length > 0)
                {
                    MemoryStream memoryStream = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    if (memoryStream != null && memoryStream.Length > 4096L)
                        this.myCoverArt.CopyStream((Stream)memoryStream);
                    memoryStream.Close();
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private bool GetCoverFromAAX(string aaxFile)
        {
            try
            {
                TagLib.File file = TagLib.File.Create(aaxFile, "audio/mp4", ReadStyle.Average);
                if (file.Tag.Pictures.Length > 0)
                {
                    MemoryStream memoryStream = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    if (memoryStream != null && memoryStream.Length > 4096L)
                        this.myCoverArt.CopyStream((Stream)memoryStream);
                    memoryStream.Close();
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private bool GetCoverFromMP3(string mp3File)
        {
            string path = Form1.thumbnailDir + "\\inAudibleCover.jpg";
            TagLib.File file = (TagLib.File)new AudioFile(mp3File);
            if (file.Tag.Pictures.Length <= 0)
                return false;
            MemoryStream memoryStream = new MemoryStream(file.Tag.Pictures[0].Data.Data);
            if (memoryStream != null && memoryStream.Length > 4096L)
            {
                FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo((Stream)fileStream);
                fileStream.Close();
            }
            memoryStream.Close();
            return true;
        }

        private bool GetCoverFromAAXwithMP4Art(string aaxFile)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.mp4ArtPath;
            process.StartInfo.Arguments = "-o --extract \"" + aaxFile + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
                return false;
            string str1 = Path.GetDirectoryName(aaxFile) + "\\" + Path.GetFileNameWithoutExtension(aaxFile) + ".art[0].jpg";
            if (!System.IO.File.Exists(str1))
            {
                string str2 = Path.GetDirectoryName(aaxFile) + "\\" + Path.GetFileNameWithoutExtension(aaxFile) + ".art[0].png";
                if (!System.IO.File.Exists(str2))
                    return false;
                Image image = Image.FromFile(str2);
                image.Save(str1, ImageFormat.Jpeg);
                image.Dispose();
                System.IO.File.Delete(str2);
            }
            string str3 = Form1.thumbnailDir + "\\" + this.myCoverArt.GenerateFileName();
            try
            {
                if (this.chkEmbedCover.Checked)
                {
                    System.IO.File.Delete(str3);
                    System.IO.File.Move(str1, str3);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CurrentPrefs(string mode)
        {
            if (mode == "AA")
                this.currentProcessingMode = Form1.ProcessingMode.AA;
            else if (mode == "AAX")
                this.currentProcessingMode = Form1.ProcessingMode.AAX;
            else if (mode == "Other")
                this.currentProcessingMode = Form1.ProcessingMode.Other;
            FileIniDataParser fileIniDataParser = new FileIniDataParser();
            try
            {
                IniData iniData = fileIniDataParser.LoadFile(this.iniPath);
                string str = iniData[mode]["selected_codec"];
                if (iniData["system"]["version"] == this.Text && str != null && str.Length > 0)
                    return true;
                this.SetText("FYI-New version of inAudible detected.  You may want to verify your preferences.");
                return true;
            }
            catch
            {
                IniData parsedData;
                if (System.IO.File.Exists(this.iniPath))
                {
                    parsedData = fileIniDataParser.LoadFile(this.iniPath);
                }
                else
                {
                    parsedData = new IniData();
                    this.safeToSave = true;
                }
                parsedData.Sections.AddSection("system");
                parsedData.Sections.AddSection(mode);
                parsedData["system"].AddKey("version", this.Text);
                if (!Directory.Exists(Path.GetDirectoryName(this.iniPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(this.iniPath));
                fileIniDataParser.SaveFile(this.iniPath, parsedData);
                return false;
            }
        }

        public bool SafeBoolChecker(bool originalValue, string value)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return originalValue;
            }
        }

        public string SafeStringChecker(string originalValue, string value)
        {
            try
            {
                if (value != null)
                {
                    if (value != "")
                        return value;
                }
            }
            catch
            {
                return originalValue;
            }
            return originalValue;
        }

        public int SafeIntChecker(int originalValue, string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return originalValue;
            }
        }

        private void GetAudibleIdFromPrefs()
        {
            string index = "AAX";
            IniData iniData = new FileIniDataParser().LoadFile(this.iniPath);
            try
            {
                this.myAdvancedOptions.audibleCustomerId = iniData[index]["audible_customer_id"];
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
            }
        }

        private string GetSingleConfigSetting(string mode, string setting)
        {
            string str = "";
            try
            {
                str = new FileIniDataParser().LoadFile(this.iniPath)[mode][setting];
            }
            catch (Exception ex)
            {
                Audible.diskLogger("could not read property [" + mode + "][" + setting + "]: " + ex.ToString());
            }
            return str;
        }

        private void LoadPrefs(string mode)
        {
            IniData iniData = new FileIniDataParser().LoadFile(this.iniPath);
            try
            {
                try
                {
                    this.allowFormResizeToolStripMenuItem.Enabled = true;
                    this.chkEmbedCover.Checked = this.SafeBoolChecker(this.chkEmbedCover.Checked, iniData[mode]["embed_cover"]);
                }
                catch
                {
                    this.safeToSave = true;
                    return;
                }
                try
                {
                    this.EnsureCodecExists(iniData[mode]["selected_codec"]);
                    this.SetSelectedCodec(iniData[mode]["selected_codec"]);
                }
                catch (Exception ex)
                {
                }
                if (this.GetSelectedCodec() == "helix")
                    this.SetHelixMode();
                this.SetBitrate(iniData[mode]["bitrate_custom_options"]);
                this.rdVBR.Checked = this.SafeBoolChecker(this.rdVBR.Checked, iniData[mode]["vbr_mode"]);
                try
                {
                    this.tbVBRquality.Value = int.Parse(iniData[mode]["vbr_setting"]);
                    this.lblVBRquality.Text = this.SafeStringChecker(this.lblVBRquality.Text, iniData[mode]["vbr_setting_label"]);
                }
                catch
                {
                }
                this.chkMultithread.Checked = this.SafeBoolChecker(this.chkMultithread.Checked, iniData[mode]["multithreading"]);
                this.chkKeepMonolithicMP3.Checked = this.SafeBoolChecker(this.chkKeepMonolithicMP3.Checked, iniData[mode]["keep_monolithic"]);
                this.chkM4Bsplit.Checked = this.SafeBoolChecker(this.chkM4Bsplit.Checked, iniData[mode]["ios_splitting"]);
                this.chkEmbedM4BChapters.Checked = this.SafeBoolChecker(this.chkEmbedM4BChapters.Checked, iniData[mode]["embed_m4b_chapters"]);
                this.chkDoNotTag.Checked = this.SafeBoolChecker(this.chkDoNotTag.Checked, iniData[mode]["do_not_tag"]);
                this.chkNormalize.Checked = this.SafeBoolChecker(this.chkNormalize.Checked, iniData[mode]["normalize"]);
                this.chkDRC.Checked = this.SafeBoolChecker(this.chkDRC.Checked, iniData[mode]["drc"]);
                this.chkFileSplitting.Checked = this.SafeBoolChecker(this.chkFileSplitting.Checked, iniData[mode]["split_on_silence"]);
                this.txtSplitThreshold.Text = this.SafeStringChecker(this.txtSplitThreshold.Text, iniData[mode]["split_threshold"]);
                if (iniData[mode]["sample_rate"] != "")
                    this.cmbSampleRate.Text = iniData[mode]["sample_rate"];
                else
                    this.cmbSampleRate.Text = "44100";
                this.chkMono.Checked = this.SafeBoolChecker(this.chkMono.Checked, iniData[mode]["mono"]);
                this.chkAudibleSplit.Checked = this.SafeBoolChecker(this.chkAudibleSplit.Checked, iniData[mode]["split_on_audible"]);
                this.chkSplitByDuration.Checked = this.SafeBoolChecker(this.chkSplitByDuration.Checked, iniData[mode]["fixed_duration_splitting"]);
                this.chkVerifyAudibleSplits.Checked = this.SafeBoolChecker(this.chkVerifyAudibleSplits.Checked, iniData[mode]["verify_audible_splits"]);
                this.chkRemoveAudibleMarkers.Checked = this.SafeBoolChecker(this.chkRemoveAudibleMarkers.Checked, iniData[mode]["remove_audible_clips"]);
                this.chkAutodetectChannels.Checked = this.SafeBoolChecker(this.chkAutodetectChannels.Checked, iniData[mode]["auto_detect_stereo"]);
                this.chkChapterThreshold.Checked = this.SafeBoolChecker(this.chkChapterThreshold.Checked, iniData[mode]["chapter_threshold"]);
                this.txtChapterThreshold.Text = this.SafeStringChecker(this.txtChapterThreshold.Text, iniData[mode]["chapter_threshold_value"]);
                this.chkMP3chapterTags.Checked = this.SafeBoolChecker(this.chkMP3chapterTags.Checked, iniData[mode]["embed_mp3_chapters"]);
                this.chkSaveCover.Checked = this.SafeBoolChecker(this.chkSaveCover.Checked, iniData[mode]["copy_cover_art"]);
                this.txtDurationToSplit.Text = this.SafeStringChecker(this.txtDurationToSplit.Text, iniData[mode]["split_duration"]);
                this.chkAuthor.Checked = this.SafeBoolChecker(this.chkAuthor.Checked, iniData[mode]["output_author"]);
                this.chkBookTitle.Checked = this.SafeBoolChecker(this.chkBookTitle.Checked, iniData[mode]["output_title"]);
                this.chkAuthorTitle.Checked = this.SafeBoolChecker(this.chkAuthorTitle.Checked, iniData[mode]["output_author_title"]);
                this.chkAddExension.Checked = this.SafeBoolChecker(this.chkAddExension.Checked, iniData[mode]["output_exension"]);
                this.chkChangeFileNumbering.Checked = this.SafeBoolChecker(this.chkChangeFileNumbering.Checked, iniData[mode]["change_file_numbering"]);
                this.myAudible.addTrackToTitle = this.SafeBoolChecker(this.myAudible.addTrackToTitle, iniData[mode]["add_track_num_to_title"]);
                this.chkStripUnabridged.Checked = this.SafeBoolChecker(this.chkStripUnabridged.Checked, iniData[mode]["strip_unabridged"]);
                this.rdITunesNone.Checked = this.SafeBoolChecker(this.rdITunesNone.Checked, iniData[mode]["itunes_none"]);
                this.rdITunesDefault.Checked = this.SafeBoolChecker(this.rdITunesDefault.Checked, iniData[mode]["itunes_default"]);
                this.rdITunesDesperation.Checked = this.SafeBoolChecker(this.rdITunesDesperation.Checked, iniData[mode]["itunes_desperation"]);
                this.rdITunesManual.Checked = this.SafeBoolChecker(this.rdITunesManual.Checked, iniData[mode]["itunes_manual"]);
                this.txtITunesPassSize.Text = this.SafeStringChecker(this.txtITunesPassSize.Text, iniData[mode]["itunes_pass_size"]);
                this.txtItunesIdleCountdown.Text = this.SafeStringChecker(this.txtItunesIdleCountdown.Text, iniData[mode]["itunes_idle_countdown"]);
                this.chkRerun.Checked = this.SafeBoolChecker(this.chkRerun.Checked, iniData[mode]["itunes_rerun"]);
                this.chkCUEfile.Checked = this.SafeBoolChecker(this.chkCUEfile.Checked, iniData[mode]["create_cue"]);
                this.chkSameAsSource.Checked = this.SafeBoolChecker(this.chkSameAsSource.Checked, iniData[mode]["same_as_source"]);
                this.myAdvancedOptions.DownloadPath = this.SafeStringChecker("", iniData[mode]["download_path"]);
                try
                {
                    this.mainWindowMaximized = bool.Parse(iniData[mode]["maximized"]);
                }
                catch
                {
                }
                if (this.mainWindowMaximized && this.Size.Width < 700)
                    this.ResizeMainWindow();
                this.myAdvancedOptions.iTunesMode = this.SafeBoolChecker(this.myAdvancedOptions.iTunesMode, iniData[mode]["advanced_itunes"]);
                this.myAdvancedOptions.decrypt = this.SafeBoolChecker(this.myAdvancedOptions.decrypt, iniData[mode]["advanced_decrypt"]);
                try
                {
                    this.myAdvancedOptions.ng = bool.Parse(iniData[mode]["inaudible-ng"]);
                    this.myAdvancedOptions.ngKeys = iniData[mode]["inaudible-ng-keys"].ToString().Trim().Split(',');
                }
                catch
                {
                }
                this.myAdvancedOptions.overlapOverride = this.SafeBoolChecker(this.myAdvancedOptions.overlapOverride, iniData[mode]["advanced_overlap"]);
                if (iniData[mode]["audible_manager_path"] != "" && iniData[mode]["audible_manager_path"] != null)
                    this.myAdvancedOptions.AudibleMangerDLLPath = iniData[mode]["audible_manager_path"];
                if (iniData[mode]["ffmpeg_path"] != "" && iniData[mode]["ffmpeg_path"] != null)
                {
                    this.myAdvancedOptions.ffmpegPath = iniData[mode]["ffmpeg_path"];
                    this.myAdvancedOptions.fdk = true;
                    this.fdkPath = this.myAdvancedOptions.ffmpegPath;
                }
                this.myAdvancedOptions.outputPath = iniData[mode]["saved_output_path"];
                this.myAdvancedOptions.SetTempPath(iniData[mode]["temp_path"]);
                if (this.myAdvancedOptions.GetTempPath() == "")
                    this.myAdvancedOptions.SetTempPath(Path.GetTempPath());
                this.supportLibs.tempPath = this.myAdvancedOptions.GetTempPath();
                this.myAdvancedOptions.completion = iniData[mode]["completion"];
                this.myAdvancedOptions.BardPassword = iniData[mode]["bard_password"];
                this.myAdvancedOptions.BardUser = iniData[mode]["bard_user"];
                try
                {
                    this.myAdvancedOptions.aaDirectShow = this.SafeBoolChecker(this.myAdvancedOptions.aaDirectShow, iniData[mode]["aa_directshow"]);
                    this.myAdvancedOptions.nfo = this.SafeBoolChecker(this.myAdvancedOptions.nfo, iniData[mode]["nfo"]);
                    this.myAdvancedOptions.AudibleScraping = this.SafeBoolChecker(this.myAdvancedOptions.AudibleScraping, iniData[mode]["audible_scraping"]);
                    this.myAdvancedOptions.EncodeAfterDownload = this.SafeBoolChecker(this.myAdvancedOptions.EncodeAfterDownload, iniData[mode]["encode_after_download"]);
                    this.myAdvancedOptions.RemoveTinyChapters = this.SafeBoolChecker(this.myAdvancedOptions.RemoveTinyChapters, iniData[mode]["remove_tiny_chapters"]);
                    this.myAdvancedOptions.beep = this.SafeBoolChecker(this.myAdvancedOptions.beep, iniData[mode]["beep"]);
                    this.myAdvancedOptions.iOSSplitThreshold = this.SafeIntChecker(this.myAdvancedOptions.iOSSplitThreshold, iniData[mode]["ios_split_threshold"]);
                    this.myAdvancedOptions.iOSSplitSize = this.SafeIntChecker(this.myAdvancedOptions.iOSSplitSize, iniData[mode]["ios_split_size"]);
                    this.myAdvancedOptions.iOSMinSplitSize = this.SafeIntChecker(this.myAdvancedOptions.iOSMinSplitSize, iniData[mode]["ios_min_split_size"]);
                    this.myAdvancedOptions.SHA256Checksum = this.SafeBoolChecker(this.myAdvancedOptions.SHA256Checksum, iniData[mode]["sha256"]);
                    this.myAdvancedOptions.cylon = this.SafeBoolChecker(this.myAdvancedOptions.cylon, iniData[mode]["cylon"]);
                    this.myAdvancedOptions.legacyChapterMode = this.SafeBoolChecker(this.myAdvancedOptions.legacyChapterMode, iniData[mode]["legacy_chapter_mode"]);
                    this.myAdvancedOptions.lowQualityPreview = this.SafeBoolChecker(this.myAdvancedOptions.lowQualityPreview, iniData[mode]["low_quality_preview"]);
                    this.myAdvancedOptions.chapterEditorTempFile = this.SafeBoolChecker(this.myAdvancedOptions.chapterEditorTempFile, iniData[mode]["chapter_editor_temp_file"]);
                    this.myAdvancedOptions.newRipper = this.SafeBoolChecker(this.myAdvancedOptions.newRipper, iniData[mode]["new_cd_ripper"]);
                }
                catch
                {
                }
                this.myAdvancedOptions.threadPriority = iniData[mode]["thread_priority"];
                if (this.myAdvancedOptions.audibleCustomerId == null || this.myAdvancedOptions.audibleCustomerId == "")
                    this.myAdvancedOptions.audibleCustomerId = iniData[mode]["audible_customer_id"];
                for (int index = 0; index < this.myAdvancedOptions.codecOptions.Rows.Count; ++index)
                {
                    string str = this.myAdvancedOptions.codecOptions.Rows[index].Field<string>(0);
                    this.myAdvancedOptions.codecOptions.Rows[index][1] = (object)iniData[mode]["codec_options_" + str];
                }
                this.cdWavPath = iniData["system"]["wav_path"];
                try
                {
                    this.myAdvancedOptions.genres = iniData[mode]["genres"].Split(',');
                }
                catch
                {
                }
                this.myAdvancedOptions.renameOptions = iniData[mode]["rename_options"];
                this.myAdvancedOptions.useChapterAsTitle = this.SafeBoolChecker(this.myAdvancedOptions.useChapterAsTitle, iniData[mode]["use_chapter_as_title"]);
                this.myAdvancedOptions.includeChapterNumberInFilename = this.SafeBoolChecker(this.myAdvancedOptions.includeChapterNumberInFilename, iniData[mode]["include_chapter_number_in_filename"]);
                this.myAdvancedOptions.noTitleInFilename = this.SafeBoolChecker(this.myAdvancedOptions.noTitleInFilename, iniData[mode]["no_title_in_file_name"]);
                this.myAdvancedOptions.includeChapterAndTitleInTitleTag = this.SafeBoolChecker(this.myAdvancedOptions.includeChapterAndTitleInTitleTag, iniData[mode]["chapter_and_number_in_title_tag"]);
                this.myAdvancedOptions.AppleTags = this.SafeBoolChecker(this.myAdvancedOptions.AppleTags, iniData[mode]["apple_tags"]);
                this.myAdvancedOptions.AllowFormResize = this.SafeBoolChecker(this.myAdvancedOptions.noTitleInFilename, iniData[mode]["allow_form_resize"]);
                try
                {
                    this.myAdvancedOptions.SplitMode = (AdvancedOptions.SplitTypes)Enum.Parse(typeof(AdvancedOptions.SplitTypes), iniData[mode]["split_mode"], true);
                }
                catch
                {
                    this.myAdvancedOptions.SplitMode = AdvancedOptions.SplitTypes.ffmpeg;
                }
                if (this.myAdvancedOptions.AllowFormResize)
                {
                    try
                    {
                        string[] strArray = iniData[mode]["form_size"].Split(',');
                        this.myAdvancedOptions.SavedFormSize = new Size(int.Parse(strArray[0].Split('=')[1].Trim()), int.Parse(strArray[1].Split('=')[1].Replace("}", "")));
                        this.allowFormResizeToolStripMenuItem.Checked = this.myAdvancedOptions.AllowFormResize;
                        this.SetFormResizeMode(true, true);
                    }
                    catch
                    {
                    }
                }
                this.safeToSave = true;
                try
                {
                    if (this.myAdvancedOptions.SplitMode == AdvancedOptions.SplitTypes.ffmpeg)
                    {
                        this.ffmpegToolStripMenuItem.Checked = true;
                        this.mP3SPLTToolStripMenuItem.Checked = false;
                    }
                    else
                    {
                        this.ffmpegToolStripMenuItem.Checked = false;
                        this.mP3SPLTToolStripMenuItem.Checked = true;
                    }
                    if (this.myAdvancedOptions.newRipper)
                    {
                        this.cUEToolsToolStripMenuItem.Checked = true;
                        this.cDDA2WAVToolStripMenuItem.Checked = false;
                    }
                    else
                    {
                        this.cDDA2WAVToolStripMenuItem.Checked = true;
                        this.cUEToolsToolStripMenuItem.Checked = false;
                    }
                    this.previewToolStripMenuItem.Checked = this.myAdvancedOptions.lowQualityPreview;
                    this.tempFileToolStripMenuItem.Checked = this.myAdvancedOptions.chapterEditorTempFile;
                    if (this.myAdvancedOptions.aaDirectShow)
                        this.directshowFilterToolStripMenuItem.Checked = true;
                    else
                        this.inAudibleNGToolStripMenuItem.Checked = true;
                    this.iTunesToolStripMenuItem.Checked = this.myAdvancedOptions.iTunesMode;
                    this.audibleManagerToolStripMenuItem1.Checked = this.myAdvancedOptions.decrypt;
                    if (this.myAdvancedOptions.ng)
                    {
                        this.audibleManagerToolStripMenuItem1.Checked = false;
                        this.inAudibleNGToolStripMenuItem1.Checked = true;
                    }
                    this.beepOnJobCompletionToolStripMenuItem.Checked = this.myAdvancedOptions.cylon;
                    this.createNFOToolStripMenuItem.Checked = this.myAdvancedOptions.nfo;
                    this.scrapeAudiblecomForAddedMetadataToolStripMenuItem.Checked = this.myAdvancedOptions.AudibleScraping;
                    this.removeTinyVerySmallChaptersToolStripMenuItem.Checked = this.myAdvancedOptions.RemoveTinyChapters;
                    this.addAppleTagsToM4BToolStripMenuItem.Checked = this.myAdvancedOptions.AppleTags;
                }
                catch (Exception ex)
                {
                    Audible.diskLogger("blew up setting dropdown prefs: " + ex.ToString());
                }
                this.myAdvancedOptions.OptionsLoaded = true;
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
            }
        }

        private string cleanFileName(string p)
        {
            string[] strArray = p.Split('_');
            if (strArray.Length > 1)
                return strArray[0];
            return p;
        }

        private void btnOutputFile_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "Audio files (MP3/M4B)|*.mp3;*.mp4;*.m4b;*.m4a|All files (*.*)|*.*";
            this.saveFileDialog1.AddExtension = true;
            if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.txtOutputFile.Text = this.saveFileDialog1.FileName;
            this.outputFileName = this.txtOutputFile.Text;
            this.outputFileMask = this.outputFileName;
        }

        private void SetText(string text)
        {
            if (this.rtbLog.InvokeRequired)
                this.Invoke((Delegate)new Form1.SetTextCallback(this.SetText), (object)text);
            else
                this.debugLog(text);
        }

        private void SetURL(string leadingText, string text, string url)
        {
            if (this.rtbLog.InvokeRequired)
                this.Invoke((Delegate)new Form1.SetTextCallback(this.SetText), (object)text);
            else
                this.urlLog(leadingText, text, url);
        }

        private void urlLog(string leadingText, string text, string url)
        {
            if (text.Length == 0)
                return;
            int maxValue = int.MaxValue;
            string text1 = "[" + DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] - " + leadingText;
            if (this.rtbLog.Text.Length + text1.Length + url.Length > maxValue)
                this.rtbLog.Text = this.rtbLog.Text.Substring(text1.Length + url.Length, this.rtbLog.Text.Length - text1.Length - url.Length);
            this.rtbLog.SelectedText = text1;
            url = Utilities.URLize(url);
            this.rtbLog.InsertLink(text, url);
            this.rtbLog.SelectedText = "\r\n";
            Audible.diskLogger(text1);
            Audible.diskLogger("url=" + url);
            this.rtbLog.SelectionStart = this.rtbLog.Text.Length;
            this.rtbLog.ScrollToCaret();
        }

        private void debugLog(string text)
        {
            if (text.Length == 0)
                return;
            int maxValue = int.MaxValue;
            string text1 = "[" + DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] - " + text + "\r\n";
            if (this.rtbLog.Text.Length + text1.Length > maxValue)
                this.rtbLog.Text = this.rtbLog.Text.Substring(text1.Length, this.rtbLog.Text.Length - text1.Length);
            Color color = Color.Black;
            if (text1.Contains("FYI-") || text1.Contains("BLUE-"))
            {
                text1 = text1.Replace("FYI-", "").Replace("BLUE-", "");
                color = Color.Blue;
            }
            if (text1.Contains("WARNING-") || text1.Contains("RED-"))
            {
                text1 = text1.Replace("WARNING-", "").Replace("RED-", "");
                color = Color.Red;
            }
            if (text1.Contains("EAA-") || text1.Contains("GREEN-"))
            {
                text1 = text1.Replace("EAA-", "").Replace("GREEN-", "");
                color = Color.DarkGreen;
            }
            if (text1.Contains("MAROON-"))
            {
                text1 = text1.Replace("MAROON-", "");
                color = Color.Maroon;
            }
            this.AppendColourText((RichTextBox)this.rtbLog, color, text1);
            Audible.diskLogger(text1);
            this.rtbLog.SelectionStart = this.rtbLog.Text.Length;
            this.rtbLog.ScrollToCaret();
        }

        private void AppendColourText(RichTextBox box, Color color, string text)
        {
            int textLength1 = box.TextLength;
            box.AppendText(text);
            int textLength2 = box.TextLength;
            box.Select(textLength1, textLength2 - textLength1);
            box.SelectionColor = color;
            box.SelectionLength = 0;
        }

        private static string ToLiteral(string input)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                using (CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression((CodeExpression)new CodePrimitiveExpression((object)input), (TextWriter)stringWriter, (CodeGeneratorOptions)null);
                    return stringWriter.ToString();
                }
            }
        }

        private void chkFileSplitting_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkFileSplitting.Checked)
            {
                this.chkAudibleSplit.Checked = false;
                this.chkSplitByDuration.Checked = false;
                if (this.chkAudibleSplit.Checked)
                    return;
                this.chkKeepMonolithicMP3.Enabled = true;
            }
            else
            {
                this.chkKeepMonolithicMP3.Enabled = false;
                this.chkKeepMonolithicMP3.Checked = false;
            }
        }

        private void chkAudibleSplit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAudibleSplit.Checked)
            {
                this.chkFileSplitting.Checked = false;
                if (!this.chkFileSplitting.Checked)
                    this.chkKeepMonolithicMP3.Enabled = true;
                this.chkMP3chapterTags.Checked = false;
            }
            else
            {
                this.chkKeepMonolithicMP3.Enabled = false;
                this.chkKeepMonolithicMP3.Checked = false;
            }
            this.audibleChapters = !this.audibleChapters;
        }

        private void rdTypeMP3_CheckedChanged(object sender, EventArgs e)
        {
            this.setEncodingUI();
        }

        private void SetLosslessOptions(bool lossless)
        {
            if (lossless)
            {
                this.rdCBR.Enabled = false;
                this.rdVBR.Enabled = false;
                this.cmbSampleRate.Enabled = false;
                this.cmbBitrate.Enabled = false;
                this.chkSameAsSource.Enabled = false;
                this.chkNormalize.Enabled = false;
                this.chkDRC.Enabled = false;
                this.chkMultithread.Enabled = false;
                this.chkKeepMonolithicMP3.Enabled = false;
                this.chkMono.Enabled = false;
                this.chkAutodetectChannels.Enabled = false;
                this.lblSampleRate.Enabled = false;
                this.tbVBRquality.Enabled = false;
                this.lblVBRquality.Enabled = false;
            }
            else
            {
                this.rdCBR.Enabled = true;
                this.rdVBR.Enabled = true;
                this.cmbSampleRate.Enabled = true;
                this.cmbBitrate.Enabled = true;
                this.chkSameAsSource.Enabled = true;
                this.chkNormalize.Enabled = true;
                this.chkDRC.Enabled = true;
                this.chkMultithread.Enabled = true;
                this.chkKeepMonolithicMP3.Enabled = true;
                this.grpLame.Enabled = true;
                this.chkMono.Enabled = true;
                this.chkAutodetectChannels.Enabled = true;
                this.lblSampleRate.Enabled = true;
                this.tbVBRquality.Enabled = true;
                this.lblVBRquality.Enabled = true;
            }
        }

        private void setEncodingUI()
        {
            if (this.noUIupdates)
                return;
            this.SetExtension();
            this.grpITunes.Enabled = true;
            this.grpOutputOptions.Enabled = true;
            this.SetVBRDescription();
            if (this.IsCodecLossless())
            {
                this.SetLosslessOptions(true);
                if (this.aaxMode)
                {
                    this.chkM4Bsplit.Visible = true;
                    this.chkEmbedM4BChapters.Visible = true;
                    this.chkMP3chapterTags.Visible = false;
                }
            }
            else if (this.GetSelectedCodec() != "")
            {
                this.SetLosslessOptions(false);
                this.grpLame.Enabled = true;
                this.cmbSampleRate.Enabled = true;
                this.chkMono.Enabled = true;
                this.chkAutodetectChannels.Enabled = true;
                if (this.chkAutodetectChannels.Checked)
                    this.chkMono.Enabled = false;
            }
            if (this.GetSelectedCodec() == "nero" || this.GetSelectedCodec() == "fdk" || this.GetSelectedCodec() == "ffmpeg_aac")
            {
                this.grpLame.Enabled = true;
                this.grpSplitting.Enabled = true;
                try
                {
                    Form1.SetControlPropertyThreadSafe((Control)this.pbCover, "Visible", (object)true);
                }
                catch
                {
                }
                this.chkAudibleSplit.Visible = true;
                this.chkFileSplitting.Enabled = false;
                this.chkM4Bsplit.Visible = true;
                this.chkEmbedM4BChapters.Visible = true;
                this.chkMP3chapterTags.Visible = false;
                this.rdVBR.Enabled = true;
                this.tbVBRquality.Enabled = true;
                this.lblVBRquality.Enabled = true;
                this.tbVBRquality.Value = 3;
            }
            else if (!this.IsCodecLossless())
            {
                this.cmbSampleRate.Enabled = true;
                this.chkMono.Enabled = true;
                this.chkAutodetectChannels.Enabled = true;
                if (this.chkAutodetectChannels.Checked)
                    this.chkMono.Enabled = false;
            }
            if (this.GetSelectedCodec() == "lame" || this.GetSelectedCodec() == "ffmpeg_mp3" || this.GetSelectedCodec() == "helix")
            {
                this.grpLame.Enabled = true;
                this.grpSplitting.Enabled = true;
                try
                {
                    Form1.SetControlPropertyThreadSafe((Control)this.pbCover, "Visible", (object)true);
                }
                catch
                {
                }
                this.tbVBRquality.Value = 5;
                this.chkKeepMonolithicMP3.Text = "Keep Monolithic MP3";
                this.chkAudibleSplit.Enabled = true;
                this.chkFileSplitting.Enabled = true;
                this.chkMP3chapterTags.Visible = true;
                this.chkM4Bsplit.Visible = false;
                this.chkEmbedM4BChapters.Visible = false;
                this.rdVBR.Enabled = true;
                this.tbVBRquality.Enabled = true;
                this.lblVBRquality.Enabled = true;
                if (!this.aaxMode && !this.myAdvancedOptions.aaDirectShow)
                    this.grpLame.Enabled = false;
            }
            if (this.GetSelectedCodec() == "wav" || this.GetSelectedCodec() == "flac")
            {
                this.grpLame.Enabled = false;
                Form1.SetControlPropertyThreadSafe((Control)this.pbCover, "Visible", (object)false);
                this.chkM4Bsplit.Visible = false;
                this.chkEmbedM4BChapters.Visible = false;
                this.chkMP3chapterTags.Visible = false;
            }
            if (this.GetSelectedCodec() == "opus")
            {
                this.grpLame.Enabled = true;
                this.grpSplitting.Enabled = true;
                this.chkAudibleSplit.Enabled = true;
                this.chkFileSplitting.Enabled = true;
                this.chkM4Bsplit.Visible = false;
                this.chkEmbedM4BChapters.Visible = false;
                this.chkMP3chapterTags.Visible = false;
                this.rdVBR.Enabled = false;
                this.tbVBRquality.Enabled = false;
                this.lblVBRquality.Enabled = false;
            }
            if (this.GetSelectedCodec() == "ogg")
            {
                this.grpLame.Enabled = true;
                this.grpSplitting.Enabled = true;
                Form1.SetControlPropertyThreadSafe((Control)this.pbCover, "Visible", (object)false);
                this.tbVBRquality.Value = 3;
                this.chkKeepMonolithicMP3.Text = "Keep Monolithic Ogg";
                this.chkM4Bsplit.Visible = false;
                this.chkEmbedM4BChapters.Visible = false;
                this.chkMP3chapterTags.Visible = false;
                this.rdVBR.Enabled = true;
                this.tbVBRquality.Enabled = true;
                this.lblVBRquality.Enabled = true;
            }
            if (this.aaxMode)
            {
                if (this.IsCodecLossless())
                {
                    this.btnLossless.Text = "Lossless";
                    this.btnLossless.BackColor = Color.Lime;
                }
                else
                {
                    this.btnLossless.Text = "Lossy";
                    this.btnLossless.BackColor = Color.Orange;
                }
            }
            else if (!this.myAdvancedOptions.aaDirectShow && (this.GetSelectedCodec() == "lame" || this.GetSelectedCodec() == "lossless"))
            {
                this.SetLosslessOptions(true);
                this.btnLossless.Text = "Lossless";
                this.btnLossless.BackColor = Color.Lime;
                this.chkMP3chapterTags.Visible = true;
            }
            else
            {
                this.btnLossless.Text = "Lossy";
                this.btnLossless.BackColor = Color.Orange;
            }
            if (this.chkSameAsSource.Checked)
                this.SetUI2Source(this.txtInputFile.Text.Split(this.fileDeliminator)[0]);
            if (this.advancedSettingsToolStripMenuItem.Enabled || !this.grpOutputType.Enabled)
                return;
            this.advancedSettingsToolStripMenuItem.Enabled = true;
        }

        private void SetExtension()
        {
            Codec selectedCodec = this.GetSelectedCodecObject();
            if (!(this.txtOutputFile.Text != ""))
                return;
            this.Invoke((System.Action)(() =>
           {
               try
               {
                   string str = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\" + Path.GetFileNameWithoutExtension(this.txtOutputFile.Text) + "." + selectedCodec.Extension;
                   this.txtOutputFile.Text = str;
                   this.saveFileDialog1.FileName = str;
                   this.saveFileDialog1.DefaultExt = selectedCodec.Extension;
                   this.chkAddExension.Text = "Add \"" + selectedCodec.Extension.ToUpper() + "\" to dir";
               }
               catch
               {
                   Audible.diskLogger("This path is too long...");
               }
           }));
        }

        private void SetUI2Source(string file)
        {
            VirtualWAV myVirtualWav = new VirtualWAV();
            this.GetSampleRateFromInput(file, ref myVirtualWav);
            if (myVirtualWav.originalBitrate <= 32)
                this.cmbBitrate.Text = "32";
            else if (myVirtualWav.originalBitrate >= 60 && myVirtualWav.originalBitrate <= 70)
                this.cmbBitrate.Text = "64";
            else if (myVirtualWav.originalBitrate >= 110 && myVirtualWav.originalBitrate <= 131)
                this.cmbBitrate.Text = "128";
            else
                this.cmbBitrate.Text = myVirtualWav.originalBitrate.ToString();
            this.cmbSampleRate.Text = myVirtualWav.sampleRate.ToString();
            if (myVirtualWav.channels == 1)
            {
                this.chkMono.Checked = true;
                this.chkAutodetectChannels.Checked = false;
            }
            else
            {
                this.chkMono.Checked = false;
                this.chkAutodetectChannels.Checked = true;
            }
        }

        private void rdTypeWAV_CheckedChanged(object sender, EventArgs e)
        {
            this.setEncodingUI();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void AddComplexAudibleFiles(string[] files)
        {
            this.splitPoints.Clear();
            this.cdProcessingMode = false;
            this.flacUnpacked = false;
            this.finishedBatch = false;
            this.omniMode = false;
            this.myCoverArt.Init();
            this.myAudible = new Audible();
            this.reRun = false;
            this.myChapters.customChapters = false;
            this.myChapters.Clear();
            this.myChapters.customChapterNames = false;
            Utilities utilities = new Utilities();
            utilities.StopwatchStart();
            this.noUIupdates = true;
            this.myBatchFiles.AddInputFiles(files);
            if (this.myBatchFiles.inputFiles.Length > 1)
                this.SetText("MAROON-Multiple input files selected.");
            this.addAudibleFiles(this.myBatchFiles);
            utilities.StopwatchStop();
            utilities.StopwatchGetElapsed("add files");
            this.noUIupdates = false;
            this.setEncodingUI();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
            string lower = Path.GetExtension(data[0]).ToLower();
            if (lower == ".aax" || lower == ".aa" || (lower == ".m4b" || lower == ".mp4") || (lower == ".m4a" || lower == ".aac" || lower == ".m4p"))
            {
                this.LoadAudibleFiles(data);
            }
            else
            {
                if (!(lower == ".bmp") && !(lower == ".jpg") && !(lower == ".png"))
                    return;
                Image image = (Image)new Bitmap(data[0]);
                this.pbCover.Image = image.GetThumbnailImage(89, 80, (Image.GetThumbnailImageAbort)null, new IntPtr());
                image.Dispose();
                this.myAudible.coverPath = data[0];
                this.hasCoverArt = true;
            }
        }

        private void initPictureBox()
        {
            Bitmap bitmap = new Bitmap(this.pbCover.Size.Width, this.pbCover.Size.Height);
            RectangleF layoutRectangle = new RectangleF(0.0f, 0.0f, (float)this.pbCover.Size.Width, (float)this.pbCover.Size.Height);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.DrawString("Drop some\r\ncover art\r\nhere", new Font("Calibri", 11f), Brushes.Black, layoutRectangle);
            graphics.Flush();
            this.pbCover.Image = (Image)bitmap;
            this.pbCover.Visible = false;
        }

        private void btnAddShellExtension_Click(object sender, EventArgs e)
        {
            string.Format("\"{0}\" \"%L\"", (object)AudibleConvertor.GLOBALS.ExecutablePath);
            FileShellExtension.Unregister("aa", "Simple Context Menu");
        }

        private LameOptions getLAMEparams()
        {
            LameOptions lameOptions = new LameOptions();
            lameOptions.bitrate = this.GetBitrate();
            lameOptions.mono = this.chkMono.Checked;
            if (this.rdVBR.Checked)
            {
                lameOptions.vbrQuality = this.tbVBRquality.Value;
                lameOptions.vbr = true;
            }
            else
                lameOptions.vbr = false;
            return lameOptions;
        }

        private string getOpusArgs()
        {
            string str = "" + "--bitrate " + (object)this.GetBitrate() + " ";
            if (this.hasCoverArt)
                str = str + " --picture \"" + this.myAudible.coverPath + "\" ";
            return str;
        }

        private string getOggOptions()
        {
            string str = "" + " --bitrate " + (object)this.GetBitrate();
            if (this.rdVBR.Checked)
            {
                str = str + " -q " + (object)this.tbVBRquality.Value;
                this.myAudible.nfo.vbr = true;
                this.myAudible.nfo.vbrValue = this.tbVBRquality.Value.ToString();
            }
            else
                this.myAudible.nfo.vbr = false;
            return str;
        }

        private string getLAMEargs()
        {
            string str = "-b " + (object)this.GetBitrate();
            this.myAudible.nfo.targetBitrate = this.GetBitrate().ToString();
            if (this.rdVBR.Checked)
            {
                str = " -V" + (object)this.tbVBRquality.Value;
                this.myAudible.nfo.vbr = true;
                this.myAudible.nfo.vbrValue = this.tbVBRquality.Value.ToString();
            }
            else
                this.myAudible.nfo.vbr = false;
            return str;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            iTunes iTunes = new iTunes();
            iTunes.soxPath = this.supportLibs.soxPath;
            iTunes.lamePath = this.supportLibs.lamePath;
            string str1 = "C:\\vs\\AudibleConvertor\\test files\\AAX\\test5.mp3";
            Audible.diskLogger("Removing overlapping audio...");
            string str2 = "";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.lamePath;
            process.StartInfo.Arguments = "- -r -s 44100 -b 64 \"" + str1 + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            StreamWriter standardInput = process.StandardInput;
            string overlap = iTunes.mergeFirstDirPipe("C:\\Users\\Public\\Music\\VCD_2014_7_16 (1)", standardInput);
            str2 = iTunes.mergeMiddleDirPipe("C:\\Users\\Public\\Music\\VCD_2014_7_16 (2)", overlap, standardInput);
            standardInput.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string str1 = "C:\\vs\\AudibleConvertor\\test files\\test output\\_title_ (9).wav";
            string str2 = "C:\\vs\\AudibleConvertor\\test files\\test output\\_title_.wav";
            long length1 = new FileInfo(str2).Length;
            long length2 = new FileInfo(str1).Length;
            System.IO.File.ReadAllBytes(str2);
            System.IO.File.ReadAllBytes(str1);
            this.myAudible.findOverlap(str1, str2);
        }

        private void tbVBRquality_Scroll(object sender, EventArgs e)
        {
            if (this.GetSelectedCodec() == "lame")
            {
                this.tbVBRquality.Minimum = 0;
                this.tbVBRquality.Maximum = 9;
                this.tbVBRquality.TickFrequency = 1;
            }
            else if (this.GetSelectedCodec() == "helix")
            {
                this.tbVBRquality.Minimum = 0;
                this.tbVBRquality.Maximum = 150;
                this.tbVBRquality.TickFrequency = 5;
            }
            else if (this.GetSelectedCodec() == "fdk")
            {
                this.tbVBRquality.Minimum = 1;
                this.tbVBRquality.Maximum = 5;
                this.tbVBRquality.TickFrequency = 1;
            }
            if (this.GetSelectedCodec() == "nero")
                this.lblVBRquality.Text = ((double)this.tbVBRquality.Value / 10.0 + 0.05).ToString();
            else
                this.lblVBRquality.Text = this.tbVBRquality.Value.ToString();
            this.rdVBR.Checked = true;
        }

        private void rdVBR_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GetSelectedCodec() == "nero")
                this.lblVBRquality.Text = ((double)this.tbVBRquality.Value / 10.0 + 0.05).ToString();
            else
                this.lblVBRquality.Text = this.tbVBRquality.Value.ToString();
            this.SetVBRDescription();
        }

        private void SetVBRDescription()
        {
            if (this.GetSelectedCodec() == "fdk")
                this.toolTip1.SetToolTip((Control)this.rdVBR, "VBR quality setting from 1 - 5.\r\n1 - 20-32kbits per channel\r\n2 - 32-40kbits per channel\r\n3 - 48-56kbits per channel\r\n4 - 64-72kbits per channel\r\n5 - 96-112kbits per channel");
            else if (this.GetSelectedCodec() == "nero")
                this.toolTip1.SetToolTip((Control)this.rdVBR, "VBR quality setting from 0.05 - 0.95.\r\n0.05 - 16kbits per channel\r\n0.15 - 33kbits per channel\r\n0.25 - 66kbits per channel\r\n0.35 - 100kbits per channel\r\n0.45 - 146kbits per channel\r\n0.55 - 192kbits per channel\r\n0.65 - 238kbits per channel\r\n0.75 - 285kbits per channel\r\n0.85 - 332kbits per channel\r\n0.95 - 381kbits per channel");
            else if (this.GetSelectedCodec() == "helix")
            {
                this.toolTip1.SetToolTip((Control)this.rdVBR, "VBR quality setting from 0 - 150.");
            }
            else
            {
                if (!(this.GetSelectedCodec() == "lame"))
                    return;
                this.toolTip1.SetToolTip((Control)this.rdVBR, "VBR quality setting from 0 - 9.\r\n0 - 220–260 kbits\r\n1 - 190–250 kbits\r\n2 - 170–210 kbits\r\n3 - 150–195 kbits\r\n4 - 140–185 kbits\r\n5 - 120–150 kbits\r\n6 - 100–130 kbits\r\n7 - 80–120 kbits\r\n8 - 70–105 kbits\r\n9 - 45–85 kbits");
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            CLog.GetLogFileName();
            Process.Start(CLog.GetLogFileName());
            if (this.myAdvancedOptions.decrypt)
                return;
            string str = Path.GetTempPath() + "\\inAudible_log.txt";
            if (!System.IO.File.Exists(str))
                return;
            Process.Start(str);
        }

        private void FetchResults(string searchInput, bool firstRun)
        {
            searchInput = searchInput.Split('(')[0];
            string thumbnailDir = Form1.thumbnailDir;
            if (!(searchInput != ""))
                return;
            string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string str2 = searchInput;
            foreach (char ch in str1)
                str2 = str2.Replace(ch.ToString(), "");
            searchInput = str2;
            string str3 = thumbnailDir + searchInput;
            string urlString = "https://www.google.com/search?tbm=isch&q=" + Uri.EscapeDataString(searchInput);
            try
            {
                this.myWB = new WebBrowser();
                this.myWB.Visible = false;
                this.myWB.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.myWB_DocumentCompleted);
                if (firstRun)
                    this.pbCover.Image = (Image)null;
                this.ChangeUserAgent("Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)");
                this.myWB.Navigate(urlString);
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
            }
        }

        private void myWB_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string[] array = ((IEnumerable<string>)this.myWB.DocumentText.Split(new string[1]
            {
        "&amp;"
            }, StringSplitOptions.RemoveEmptyEntries)).Where<string>((System.Func<string, bool>)(data => data.Contains("/imgres?imgurl"))).Select<string, string>((System.Func<string, string>)(data => data.Substring(3))).ToArray<string>();
            if (array.Length == 0 || this.bgWorker1.IsBusy)
                return;
            this.bgWorker1.RunWorkerAsync((object)array);
        }

        private void myBackgroundWorker_DoWork_10(object sender, DoWorkEventArgs e)
        {
            string[] strArray = e.Argument as string[];
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            string str = "";
            int num = 10;
            for (int index = 0; index < num; ++index)
            {
                string address = strArray[index].Remove(0, strArray[index].IndexOf("imgurl=") + 7);
                try
                {
                    if (!Directory.Exists(Form1.thumbnailDir))
                        Directory.CreateDirectory(Form1.thumbnailDir);
                    string fileName = Form1.thumbnailDir + "\\inAudibleCover" + str + ".jpg";
                    webClient.DownloadFile(address, fileName);
                }
                catch (Exception ex)
                {
                    Audible.diskLogger(ex.ToString());
                }
                str = index.ToString();
            }
        }

        private void myBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.goldProgressBarEx1.Value = e.ProgressPercentage;
            this.Text = e.ProgressPercentage.ToString();
        }

        private void PlayDisk2Sound(string sound)
        {
            try
            {
                ThreadPool.QueueUserWorkItem((WaitCallback)(ignoredState =>
               {
                   using (SoundPlayer soundPlayer = new SoundPlayer(Form1.appPath + "\\disk_ii." + sound + ".wav"))
                       soundPlayer.PlaySync();
               }));
            }
            catch
            {
                Audible.diskLogger("Failed to play disk sound");
            }
        }

        private void UpdateProgressBar(int value, string colour = "default")
        {
            if (value < 0)
                value = 0;
            if (value > 100)
                value = 100;
            Form1.SetControlPropertyThreadSafe((Control)this.goldProgressBarEx1, "Value", (object)value);
            if (colour == "green")
            {
                this.goldPlainProgressPainter1.Color = Color.Lime;
                this.goldPlainProgressPainter1.LeadingEdge = Color.Transparent;
                this.goldPlainBorderPainter1.Color = Color.Green;
                this.goldRoundGlossPainter1.Color = Color.DarkGreen;
            }
            else if (colour == "red")
            {
                this.goldPlainProgressPainter1.Color = Color.Red;
                this.goldPlainProgressPainter1.LeadingEdge = Color.Transparent;
                this.goldPlainBorderPainter1.Color = Color.DarkRed;
                this.goldRoundGlossPainter1.Color = Color.Maroon;
            }
            else if (colour == "blue")
            {
                this.goldPlainProgressPainter1.Color = Color.Blue;
                this.goldPlainProgressPainter1.LeadingEdge = Color.Transparent;
                this.goldPlainBorderPainter1.Color = Color.DarkBlue;
                this.goldRoundGlossPainter1.Color = Color.BlueViolet;
            }
            if (value > 50)
            {
                if (this.goldPlainProgressPainter1.Color == Color.Blue)
                    this.goldProgressBarEx1.ForeColor = Color.Yellow;
                else if (this.goldPlainProgressPainter1.Color == Color.Red)
                {
                    this.goldProgressBarEx1.ForeColor = Color.Yellow;
                }
                else
                {
                    if (!(this.goldPlainProgressPainter1.Color == Color.Lime))
                        return;
                    this.goldProgressBarEx1.ForeColor = SystemColors.ControlText;
                }
            }
            else
                this.goldProgressBarEx1.ForeColor = SystemColors.ControlText;
        }

        private void bgwAA_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int num = this.goldProgressBarEx1.Value;
            int progressPercentage = e.ProgressPercentage;
            if (progressPercentage > 100 || progressPercentage < 0)
                return;
            this.UpdateProgressBar(progressPercentage, "");
            if (progressPercentage <= num)
            {
                if (num == 0)
                {
                    if (progressPercentage != 0)
                        goto label_5;
                }
            }
            label_5:
            try
            {
                int maximumValue = 100;
                TaskbarManager instance = TaskbarManager.Instance;
                instance.SetProgressState(TaskbarProgressBarState.Normal);
                instance.SetProgressValue(e.ProgressPercentage, maximumValue);
            }
            catch
            {
            }
        }

        private void myBackgroundWorker_DoWork_DecryptAA(object sender, DoWorkEventArgs e)
        {
            DecryptAAOptions decryptAaOptions = e.Argument as DecryptAAOptions;
            this.decryptAA(decryptAaOptions.aaFile, decryptAaOptions.wavFile, decryptAaOptions.totalSeconds);
            e.Result = (object)decryptAaOptions;
        }

        private void bgWorkerAASox_DoWork(object sender, DoWorkEventArgs e)
        {
            DecryptAAOptions decryptAaOptions = e.Argument as DecryptAAOptions;
            this.soxSplit(decryptAaOptions.wavFile, this.txtSplitThreshold.Text, decryptAaOptions.lameOptions);
            e.Result = (object)decryptAaOptions;
        }

        private void myBackgroundWorker_AAprocessingComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(this.startTime);
            this.SetText("Completed in " + timeSpan.Hours.ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2"));
            if (this.myAudible.coverPath != "")
                System.IO.File.Copy(this.myAudible.coverPath, Path.GetDirectoryName(this.outputFileName) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileName) + ".jpg", true);
            this.SetText("Done!");
            this.SetEncodeMode();
        }

        private void myBackgroundWorker_DecryptAACompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.WAVoutput)
            {
                this.SetText("Completed.");
            }
            else
            {
                DecryptAAOptions result = e.Result as DecryptAAOptions;
                if (this.MP3output && !this.aaxMode)
                {
                    if (this.chkFileSplitting.Checked || this.audibleChapters)
                    {
                        this.SetText("Encoding with " + (object)this.threads + " logical cores.");
                        this.bgWorkerAAPostProcessing = new BackgroundWorker();
                        this.bgWorkerAAPostProcessing.DoWork += new DoWorkEventHandler(this.bgWorkerAASox_DoWork);
                        this.bgWorkerAAPostProcessing.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.myBackgroundWorker_AAprocessingComplete);
                        this.bgWorkerAAPostProcessing.ProgressChanged += new ProgressChangedEventHandler(this.myBackgroundWorker_ProgressChanged);
                        this.bgWorkerAAPostProcessing.WorkerReportsProgress = true;
                        this.bgWorkerAAPostProcessing.RunWorkerAsync((object)result);
                    }
                    else
                        this.encodeMP3(result.wavFile, result.mp3File, 1, result.lameOptions);
                }
                else
                {
                    if (!this.M4Boutput || this.aaxMode)
                        return;
                    string splitDirectory = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\split\\";
                    this.SetText("Preparing Chapters...");
                    result.splitPosition = this.split4m4b(result.wavFile, result.splitPosition, splitDirectory);
                }
            }
        }

        private void myBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] strArray = e.Argument as string[];
            WebClient webClient = new WebClient();
            int num = 1;
            for (int index = 0; index < num && strArray[index].Contains("imgurl"); ++index)
            {
                string address = strArray[index].Remove(0, strArray[index].IndexOf("imgurl=") + 7);
                try
                {
                    if (!Directory.Exists(Form1.thumbnailDir))
                        Directory.CreateDirectory(Form1.thumbnailDir);
                    string fileName = Form1.thumbnailDir + "\\" + this.myCoverArt.GenerateFileName();
                    webClient.DownloadFile(address, fileName);
                }
                catch (Exception ex)
                {
                    ++num;
                    if (num > 5)
                        break;
                }
            }
        }

        private void myBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                string filename = Form1.thumbnailDir + "\\" + this.myCoverArt.GetImage(Form1.thumbnailDir);
                Image image = (Image)new Bitmap(filename);
                this.pbCover.Image = image.GetThumbnailImage(89, 80, (Image.GetThumbnailImageAbort)null, new IntPtr());
                image.Dispose();
                this.myAudible.coverPath = filename;
                this.pbCover.Visible = true;
                this.hasCoverArt = true;
                this.chkEmbedCover.Checked = true;
                this.chkEmbedCover.Visible = true;
                this.chkSaveCover.Visible = true;
            }
            catch
            {
            }
        }

        private void pbCover_Click(object sender, EventArgs e)
        {
            this.bgWorker1 = new BackgroundWorker();
            this.bgWorker1.DoWork += new DoWorkEventHandler(this.myBackgroundWorker_DoWork_10);
            FormOpenFileDialog formOpenFileDialog = new FormOpenFileDialog();
            formOpenFileDialog.StartLocation = AddonWindowLocation.Right;
            formOpenFileDialog.DefaultViewMode = FolderViewMode.Thumbnails;
            formOpenFileDialog.OpenDialog.AddExtension = true;
            formOpenFileDialog.OpenDialog.Filter = "Image Files(*.jpg)|*.jpg";
            if (formOpenFileDialog.ShowDialog((IWin32Window)this) == DialogResult.OK)
            {
                string fileName = formOpenFileDialog.OpenDialog.FileName;
                this.pbCover.Image = Image.FromFile(fileName);
                this.myAudible.coverPath = fileName;
                this.hasCoverArt = true;
            }
            formOpenFileDialog.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            string[] inputFiles = new string[commandLineArgs.Length - 1];
            if (commandLineArgs.Length > 1)
            {
                List<string> stringList = new List<string>();
                for (int index = 1; index < commandLineArgs.Length; ++index)
                    stringList.Add(commandLineArgs[index]);
                stringList.Sort();
                for (int index = 0; index < stringList.Count; ++index)
                    inputFiles[index] = stringList[index];
                if (Path.GetExtension(inputFiles[0]) == ".adh" || Path.GetExtension(inputFiles[0]) == "")
                {
                    this.PerformAudibleDownload(inputFiles[0]);
                    return;
                }
                this.LoadAudibleFiles(inputFiles);
            }
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 500;
            this.toolTip1.SetToolTip((Control)this.btnSplitter, "Advanced splitting tool.");
            this.toolTip1.SetToolTip((Control)this.chkM4Bsplit, "Split M4B files > 8 hours into 6 hour segments at chapter boundaries.");
            this.toolTip1.SetToolTip((Control)this.chkEmbedM4BChapters, "Include chapter markers in M4B file.");
            this.toolTip1.SetToolTip((Control)this.pbCover, "Click to select new image.");
            this.toolTip1.SetToolTip((Control)this.rdVBR, "VBR quality setting from 1 - 9.\r\nThe higher the number, the lower the quality.");
            this.toolTip1.SetToolTip((Control)this.tbVBRquality, "VBR quality setting from 1 - 9.\r\nThe higher the number, the lower the quality.");
            this.toolTip1.SetToolTip((Control)this.chkMultithread, "Use multithreading for MP3 encoding.\r\nIgnored in Turbo mode.");
            this.toolTip1.SetToolTip((Control)this.chkKeepMonolithicMP3, "Create / keep a single MP3 file of the transcoded output. Also creates a CUE file of chapter points.\r\n\r\nIn Turbo mode, this causes an extra thread to be spawned which will increase encoding time.");
            this.toolTip1.SetToolTip((Control)this.chkDoNotTag, "Do not add MP3 tags or embed cover art.");
            this.toolTip1.SetToolTip((Control)this.chkDRC, "Use dynamic range compression to make all parts of the audio the same volume.");
            this.toolTip1.SetToolTip((Control)this.chkNormalize, "Increase the overall volume to the maximum level without clipping.");
            this.toolTip1.SetToolTip((Control)this.chkFileSplitting, "Detect silence and split accordingly");
            this.toolTip1.SetToolTip((Control)this.txtSplitThreshold, "Consecutive seconds of silence that must occur before declaring a cut point.");
            this.toolTip1.SetToolTip((Control)this.chkAudibleSplit, "Use the chapter marks from the orignal AA/AAX files for splitting.");
            this.toolTip1.SetToolTip((Control)this.chkRemoveAudibleMarkers, "Automatically remove \"This is Audible\" and \"Audible hopes you have enjoyed this program\".");
            this.toolTip1.SetToolTip((Control)this.chkChapterThreshold, "Remove any chapter markers shorter than the specified number of seconds.");
            this.toolTip1.SetToolTip((Control)this.rdITunesNone, "Do not try to automate iTunes.  You will need to manually control each iTunes pass.");
            this.toolTip1.SetToolTip((Control)this.rdITunesDefault, "Try to control iTunes by selecting the\r\nBurn Menu from the File menu.");
            this.toolTip1.SetToolTip((Control)this.rdITunesDesperation, "Try to bring up the burn menu by right-clicking on the plalist.\r\nIf that fails, try to use two different OCR methods to select it.\r\nIf that fails, try the file menu.");
            this.toolTip1.SetToolTip((Control)this.rdITunesManual, "Do not try to automate iTunes. You must right-click on the \"inAudible\" playlist and start the burn yourself.");
            this.toolTip1.SetToolTip((Control)this.txtITunesPassSize, "Set number of hours per iTunes pass.\r\n\r\nHigher numbers mean less passes, but beware;\r\niTunes will randomly crash the higher you go.");
            this.toolTip1.SetToolTip((Control)this.txtItunesIdleCountdown, "Set the number of seconds that inAudible should wait before giving up on iTunes generating any more output.");
            this.toolTip1.SetToolTip((Control)this.chkAutodetectChannels, "Analyze the audio channels to see if the file is true stereo or just double mono.");
            this.toolTip1.SetToolTip((Control)this.chkVerifyAudibleSplits, "Adjust chapter splits provided by Audible so that they fall during silence.");
            this.toolTip1.SetToolTip((Control)this.chkRerun, "Saves this session and does not delete the iTunes WAVs.\r\n\r\nThis allows you to re-process the output without re-ripping.");
            if (System.IO.File.Exists(this.iniPath))
                return;
            this.LaunchWizard(true);
            this.SetText("FYI-This appears to be the first time you have run inAudible.  If you are new to converting Audible files, you may want to try the Wizard tool in the file menu.");
        }

        private void SetDownloadWindow(bool status)
        {
            if (status)
            {
                this.grpDownloader.Location = this.grpLame.Location;
                this.grpDownloader.Size = this.grpLame.Size;
                this.grpLame.Visible = false;
                this.grpDownloader.Visible = true;
            }
            else
            {
                this.grpDownloader.Visible = false;
                this.grpLame.Visible = true;
                this.grpDownloader.Location = new Point(this.Size.Width, this.Size.Height);
            }
        }

        private void PerformAudibleDownload(string adhFile)
        {
            int num1 = this.myAdvancedOptions.OptionsLoaded ? 1 : 0;
            if (!this.myAdvancedOptions.OptionsLoaded || this.myAdvancedOptions.RevertedDownloadPath)
            {
                this.myAdvancedOptions.EncodeAfterDownload = this.SafeBoolChecker(false, this.GetSingleConfigSetting("AAX", "encode_after_download"));
                this.myAdvancedOptions.DownloadPath = this.SafeStringChecker(this.myAdvancedOptions.DownloadPath, this.GetSingleConfigSetting("AAX", "download_path"));
            }
            FormAudibleDownloader frmDownloader = new FormAudibleDownloader();
            frmDownloader.myAdvancedOptions = this.myAdvancedOptions;
            frmDownloader.checkBox1.Checked = this.myAdvancedOptions.EncodeAfterDownload;
            string str1 = System.IO.File.ReadAllText(adhFile);
            Uri uri = new Uri("http://cds.audible.com/download?" + str1);
            string str2 = HttpUtility.ParseQueryString(uri.Query).Get("title");
            string str3 = HttpUtility.ParseQueryString(uri.Query).Get("product_id");
            string str4 = HttpUtility.ParseQueryString(uri.Query).Get("awtype");
            string str5 = HttpUtility.ParseQueryString(uri.Query).Get("codec");
            frmDownloader.lblDlTitle.Text = str2;
            int num2 = (int)frmDownloader.ShowDialog();
            this.lblDlTitle.Text = "Title: " + str2;
            this.lblDlProductId.Text = "Product ID: " + str3;
            this.lblDlCodec.Text = "Codec: " + str5 + " (" + str4 + ")";
            if (!frmDownloader.applied)
                return;
            this.SetDownloadWindow(true);
            string property = str3 + " - " + str2 + "." + str4.ToLower();
            string fulldownloadPath = this.myAdvancedOptions.DownloadPath;
            string asciiTag = new Audible().GetASCIITag(property);
            if (fulldownloadPath == null || fulldownloadPath == "")
                fulldownloadPath = Form1.thumbnailDir + "\\" + asciiTag;
            else
                fulldownloadPath = fulldownloadPath + "\\" + asciiTag;
            string bookURL = "http://cds.audible.com/download?" + str1;
            Task.Factory.StartNew((System.Action)(() =>
           {
               using (new WebClient())
                   this.startDownload(bookURL, fulldownloadPath);
           })).ContinueWith((System.Action<Task>)(t =>
     {
               if (!frmDownloader.autoEncode)
                   return;
               this.autoEncodeOnDownload = true;
           }), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void txtCustomLame_Click(object sender, EventArgs e)
        {
        }

        private void rtbLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string linkText = e.LinkText;
            string fileName = Uri.UnescapeDataString(e.LinkText);
            if (fileName.Contains("#"))
                fileName = fileName.Split('#')[1];
            Process.Start(fileName);
        }

        private void btnOutputOptions_Click(object sender, EventArgs e)
        {
            this.mainWindowMaximized = !this.mainWindowMaximized;
            this.ResizeMainWindow();
        }

        private void ResizeMainWindow()
        {
            int width = this.grpOutputOptions.Location.X + this.grpOutputOptions.Size.Width + 15;
            int num = width - this.Size.Width;
            if (this.Size.Width > this.grpOutputOptions.Location.X)
            {
                num = this.grpOutputOptions.Location.X - width;
                width = this.grpOutputOptions.Location.X;
            }
            this.Size = new Size(width, this.Size.Height);
            if (this.allowFormResizeToolStripMenuItem.Checked)
                return;
            this.txtInputFile.Size = new Size(this.txtInputFile.Size.Width + num, this.txtInputFile.Size.Height);
            this.btnInputFile.Location = new Point(this.btnInputFile.Location.X + num, this.btnInputFile.Location.Y);
            this.btnBatchEdit.Location = new Point(this.btnBatchEdit.Location.X + num, this.btnBatchEdit.Location.Y);
            this.btnCDrip.Location = new Point(this.btnCDrip.Location.X + num, this.btnCDrip.Location.Y);
            this.btnSplitter.Location = new Point(this.btnSplitter.Location.X + num, this.btnSplitter.Location.Y);
            this.txtOutputFile.Size = new Size(this.txtOutputFile.Size.Width + num, this.txtOutputFile.Size.Height);
            this.btnOutputFile.Location = new Point(this.btnOutputFile.Location.X + num, this.btnOutputFile.Location.Y);
            this.btnOutputOptions.Location = new Point(this.btnOutputOptions.Location.X + num, this.btnOutputOptions.Location.Y);
            this.btnLossless.Location = new Point(this.btnLossless.Location.X + num, this.btnLossless.Location.Y);
            this.rtbLog.Size = new Size(this.rtbLog.Size.Width + num, this.rtbLog.Size.Height);
            this.goldProgressBarEx1.Size = new Size(this.goldProgressBarEx1.Size.Width + num, this.goldProgressBarEx1.Size.Height);
            this.btnLog.Location = new Point(this.btnLog.Location.X + num, this.btnLog.Location.Y);
            this.btnConvert.Location = new Point(this.btnConvert.Location.X + num / 2, this.btnConvert.Location.Y);
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            string str = "C:\\Users\\Public\\Music\\VCD_2014_9_18 (1)";
            VirtualWAV virtualWav = new VirtualWAV();
            virtualWav.soxPath = this.supportLibs.soxPath;
            virtualWav.ConstructVirtualWAV(new List<string>()
      {
        str
      });
            this.SetText("mono = " + (object)virtualWav.IsMono());
        }

        private void chkAutodetectChannels_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAutodetectChannels.Checked)
                this.chkMono.Enabled = false;
            else
                this.chkMono.Enabled = true;
        }

        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if ((object)serializableObject == null)
                return;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    new XmlSerializer(serializableObject.GetType()).Serialize((Stream)memoryStream, (object)serializableObject);
                    memoryStream.Position = 0L;
                    xmlDocument.Load((Stream)memoryStream);
                    xmlDocument.Save(fileName);
                    memoryStream.Close();
                }
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
            }
        }

        public static string SerializeObject<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (StringWriter stringWriter = new StringWriter())
            {
                try
                {
                    xmlSerializer.Serialize((TextWriter)stringWriter, (object)toSerialize);
                }
                catch
                {
                }
                return stringWriter.ToString();
            }
        }

        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return default(T);
            T obj = default(T);
            try
            {
                string empty = string.Empty;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                using (StringReader stringReader = new StringReader(xmlDocument.OuterXml))
                {
                    using (XmlReader xmlReader = (XmlReader)new XmlTextReader((TextReader)stringReader))
                    {
                        obj = (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
                        xmlReader.Close();
                    }
                    stringReader.Close();
                }
            }
            catch (Exception ex)
            {
                Audible.diskLogger(ex.ToString());
            }
            return obj;
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void AddINIvalue(ref IniData parsedData, string section, string key, string value)
        {
            if (!parsedData[section].ContainsKey(key))
                parsedData[section].AddKey(key, value);
            else
                parsedData[section][key] = value;
        }

        private static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.endingMusicThread != null && this.endingMusicThread.IsAlive)
            {
                this.musicProcess.Kill();
                this.endingMusicThread.Abort();
            }
            this.CleanupTmpMP3Wavs();
            if (this.reRun)
            {
                DialogResult dialogResult = MessageBox.Show("Re-Run mode is active.  Would you like inAudible to delete the iTunes WAV files (YES) or save and create .inaudible file (NO)?", "Cleanup iTunes WAVs?", MessageBoxButtons.YesNo);
                string str = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".inaudible";
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (string savedWorkingDir in this.savedWorkingDirs)
                    {
                        try
                        {
                            Directory.Delete(savedWorkingDir, true);
                        }
                        catch
                        {
                        }
                    }
                    try
                    {
                        System.IO.File.Delete(str);
                    }
                    catch
                    {
                    }
                }
                else
                    this.SerializeObject<InAudible>(new InAudible()
                    {
                        savedInitialDirs = this.savedInitialDirs,
                        savedWorkingDirs = this.savedWorkingDirs
                    }, str);
            }
            try
            {
                FileIniDataParser fileIniDataParser = new FileIniDataParser();
                IniData parsedData = fileIniDataParser.LoadFile(this.iniPath);
                if (!System.IO.File.Exists(this.iniPath))
                    this.safeToSave = true;
                if (!this.myAdvancedOptions.okayToSaveSettings && this.txtInputFile.Text.Length <= 0 || !this.safeToSave)
                    return;
                this.AddINIvalue(ref parsedData, "system", "version", this.Text);
                if (this.cdProcessingMode)
                    this.AddINIvalue(ref parsedData, "system", "wav_path", this.txtInputFile.Text);
                string section = this.currentProcessingMode.ToString();
                this.AddINIvalue(ref parsedData, section, "selected_codec", this.GetSelectedCodec());
                this.AddINIvalue(ref parsedData, section, "embed_cover", this.chkEmbedCover.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "bitrate_custom_options", this.GetBitrate().ToString());
                this.AddINIvalue(ref parsedData, section, "vbr_mode", this.rdVBR.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "vbr_setting", this.tbVBRquality.Value.ToString());
                this.AddINIvalue(ref parsedData, section, "vbr_setting_label", this.lblVBRquality.Text.ToString());
                this.AddINIvalue(ref parsedData, section, "create_cue", this.chkCUEfile.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "same_as_source", this.chkSameAsSource.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "normalize", this.chkNormalize.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "drc", this.chkDRC.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "multithreading", this.chkMultithread.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "keep_monolithic", this.chkKeepMonolithicMP3.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "ios_splitting", this.chkM4Bsplit.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "embed_m4b_chapters", this.chkEmbedM4BChapters.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "do_not_tag", this.chkDoNotTag.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "split_on_silence", this.chkFileSplitting.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "split_threshold", this.txtSplitThreshold.Text.ToString());
                this.AddINIvalue(ref parsedData, section, "fixed_duration_splitting", this.chkSplitByDuration.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "split_duration", this.txtDurationToSplit.Text.ToString());
                this.AddINIvalue(ref parsedData, section, "sample_rate", this.cmbSampleRate.Text.ToString());
                this.AddINIvalue(ref parsedData, section, "mono", this.chkMono.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "split_on_audible", this.chkAudibleSplit.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "verify_audible_splits", this.chkVerifyAudibleSplits.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "remove_audible_clips", this.chkRemoveAudibleMarkers.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "auto_detect_stereo", this.chkAutodetectChannels.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "chapter_threshold", this.chkChapterThreshold.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "chapter_threshold_value", this.txtChapterThreshold.Text.ToString());
                this.AddINIvalue(ref parsedData, section, "embed_mp3_chapters", this.chkMP3chapterTags.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "copy_cover_art", this.chkSaveCover.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "output_author", this.chkAuthor.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "output_title", this.chkBookTitle.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "output_author_title", this.chkAuthorTitle.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "output_exension", this.chkAddExension.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "strip_unabridged", this.chkStripUnabridged.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "change_file_numbering", this.chkChangeFileNumbering.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "add_track_num_to_title", this.myAudible.addTrackToTitle.ToString());
                this.AddINIvalue(ref parsedData, section, "itunes_none", this.rdITunesNone.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "itunes_default", this.rdITunesDefault.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "itunes_desperation", this.rdITunesDesperation.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "itunes_manual", this.rdITunesManual.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "itunes_pass_size", this.txtITunesPassSize.Text.ToString());
                this.AddINIvalue(ref parsedData, section, "itunes_idle_countdown", this.txtItunesIdleCountdown.Text.ToString());
                this.AddINIvalue(ref parsedData, section, "itunes_rerun", this.chkRerun.Checked.ToString());
                this.AddINIvalue(ref parsedData, section, "advanced_itunes", this.myAdvancedOptions.iTunesMode.ToString());
                this.AddINIvalue(ref parsedData, section, "advanced_decrypt", this.myAdvancedOptions.decrypt.ToString());
                this.AddINIvalue(ref parsedData, section, "inaudible-ng", this.myAdvancedOptions.ng.ToString());
                this.AddINIvalue(ref parsedData, section, "inaudible-ng-keys", string.Join(",", this.myAdvancedOptions.ngKeys));
                this.AddINIvalue(ref parsedData, section, "advanced_overlap", this.myAdvancedOptions.overlapOverride.ToString());
                this.AddINIvalue(ref parsedData, section, "audible_manager_path", this.myAdvancedOptions.AudibleMangerDLLPath);
                this.AddINIvalue(ref parsedData, section, "ffmpeg_path", this.myAdvancedOptions.ffmpegPath);
                this.AddINIvalue(ref parsedData, section, "fdk", this.myAdvancedOptions.fdk.ToString());
                this.AddINIvalue(ref parsedData, section, "aa_directshow", this.myAdvancedOptions.aaDirectShow.ToString());
                this.AddINIvalue(ref parsedData, section, "nfo", this.myAdvancedOptions.nfo.ToString());
                this.AddINIvalue(ref parsedData, section, "audible_scraping", this.myAdvancedOptions.AudibleScraping.ToString());
                this.AddINIvalue(ref parsedData, section, "encode_after_download", this.myAdvancedOptions.EncodeAfterDownload.ToString());
                this.AddINIvalue(ref parsedData, section, "remove_tiny_chapters", this.myAdvancedOptions.RemoveTinyChapters.ToString());
                this.AddINIvalue(ref parsedData, section, "beep", this.myAdvancedOptions.beep.ToString());
                this.AddINIvalue(ref parsedData, section, "saved_output_path", this.myAdvancedOptions.outputPath);
                this.AddINIvalue(ref parsedData, section, "temp_path", this.myAdvancedOptions.GetTempPath());
                this.AddINIvalue(ref parsedData, section, "completion", this.myAdvancedOptions.completion);
                this.AddINIvalue(ref parsedData, section, "sha256", this.myAdvancedOptions.SHA256Checksum.ToString());
                this.AddINIvalue(ref parsedData, section, "cylon", this.myAdvancedOptions.cylon.ToString());
                this.AddINIvalue(ref parsedData, section, "legacy_chapter_mode", this.myAdvancedOptions.legacyChapterMode.ToString());
                this.AddINIvalue(ref parsedData, section, "low_quality_preview", this.myAdvancedOptions.lowQualityPreview.ToString());
                this.AddINIvalue(ref parsedData, section, "chapter_editor_temp_file", this.myAdvancedOptions.chapterEditorTempFile.ToString());
                this.AddINIvalue(ref parsedData, section, "new_cd_ripper", this.myAdvancedOptions.newRipper.ToString());
                this.AddINIvalue(ref parsedData, section, "ios_split_threshold", this.myAdvancedOptions.iOSSplitThreshold.ToString());
                this.AddINIvalue(ref parsedData, section, "ios_split_size", this.myAdvancedOptions.iOSSplitSize.ToString());
                this.AddINIvalue(ref parsedData, section, "ios_min_split_size", this.myAdvancedOptions.iOSMinSplitSize.ToString());
                this.AddINIvalue(ref parsedData, section, "maximized", this.mainWindowMaximized.ToString());
                this.AddINIvalue(ref parsedData, section, "rename_options", this.myAdvancedOptions.renameOptions);
                this.AddINIvalue(ref parsedData, section, "use_chapter_as_title", this.myAdvancedOptions.useChapterAsTitle.ToString());
                this.AddINIvalue(ref parsedData, section, "include_chapter_number_in_filename", this.myAdvancedOptions.includeChapterNumberInFilename.ToString());
                this.AddINIvalue(ref parsedData, section, "no_title_in_file_name", this.myAdvancedOptions.noTitleInFilename.ToString());
                this.AddINIvalue(ref parsedData, section, "chapter_and_number_in_title_tag", this.myAdvancedOptions.includeChapterAndTitleInTitleTag.ToString());
                this.AddINIvalue(ref parsedData, section, "apple_tags", this.myAdvancedOptions.AppleTags.ToString());
                this.AddINIvalue(ref parsedData, section, "thread_priority", this.myAdvancedOptions.threadPriority);
                this.AddINIvalue(ref parsedData, section, "allow_form_resize", this.myAdvancedOptions.AllowFormResize.ToString());
                this.AddINIvalue(ref parsedData, section, "form_size", this.Size.ToString());
                this.AddINIvalue(ref parsedData, section, "split_mode", this.myAdvancedOptions.SplitMode.ToString());
                this.AddINIvalue(ref parsedData, section, "download_path", this.myAdvancedOptions.DownloadPath);
                this.AddINIvalue(ref parsedData, section, "bard_user", this.myAdvancedOptions.BardUser);
                this.AddINIvalue(ref parsedData, section, "bard_password", this.myAdvancedOptions.BardPassword);
                if (this.myAdvancedOptions.audibleCustomerId != null)
                    this.AddINIvalue(ref parsedData, section, "audible_customer_id", this.myAdvancedOptions.audibleCustomerId);
                try
                {
                    this.AddINIvalue(ref parsedData, section, "genres", string.Join(",", this.myAdvancedOptions.genres));
                }
                catch
                {
                }
                for (int index = 0; index < this.myAdvancedOptions.codecOptions.Rows.Count; ++index)
                    this.AddINIvalue(ref parsedData, section, "codec_options_" + this.myAdvancedOptions.codecOptions.Rows[index].Field<string>(0), this.myAdvancedOptions.codecOptions.Rows[index].Field<string>(1));
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(this.iniPath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(this.iniPath));
                    fileIniDataParser.SaveFile(this.iniPath, parsedData);
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                Audible.diskLogger("couldn't save settings: " + ex.ToString());
            }
        }

        private void btnVCDSettings_Click(object sender, EventArgs e)
        {
            Process.Start(this.GetVCDSettingsPath());
        }

        private string GetVCDSettingsPath()
        {
            string path = Form1.ProgramFilesx86() + "\\Virtual CD v10\\System\\vc10set.exe";
            if (System.IO.File.Exists(path))
                return path;
            return "";
        }

        private void btnMetadata_Click(object sender, EventArgs e)
        {
            FormMetaData formMetaData = new FormMetaData();
            formMetaData.aaxFiles = this.txtInputFile.Text.Split(this.fileDeliminator);
            this.myAudibles = new Audible[formMetaData.aaxFiles.Length];
            if (formMetaData.aaxFiles.Length > 1 && this.aaxMode)
            {
                this.myAudibles[0] = this.myAudible;
                this.SetText("Extracting Metadata from " + (object)formMetaData.aaxFiles.Length + " file(s)...");
                for (int index = 1; index < formMetaData.aaxFiles.Length; ++index)
                {
                    this.myAudibles[index] = new Audible();
                    this.myAudibles[index].GetCustomAAXMetaData(formMetaData.aaxFiles[index]);
                    if (this.myAdvancedOptions.AudibleScraping)
                        this.myAudibles[index].genre = this.GetAudibleDotComInfo(this.myAudibles[index].id);
                }
                formMetaData.lblPosition.Text = "1/" + (object)formMetaData.aaxFiles.Length;
            }
            else
                this.myAudibles[0] = this.myAudible;
            formMetaData.myAudibles = this.myAudibles;
            formMetaData.myAudible = this.myAudible;
            formMetaData.txtTitle.Text = this.myAudible.title;
            formMetaData.txtAlbum.Text = this.myAudible.album;
            formMetaData.txtAuthor.Text = this.myAudible.author;
            formMetaData.txtNarrator.Text = this.myAudible.narrator;
            formMetaData.txtYear.Text = this.myAudible.year;
            formMetaData.chkTrackToTitle.Checked = this.myAudible.addTrackToTitle;
            formMetaData.txtComments.Text = this.myAudible.GetComments();
            formMetaData.txtPublisher.Text = this.myAudible.publisher;
            if (this.myAudible.genre != null || this.myAudible.genre != "")
                formMetaData.cmbGenre.Text = this.myAudible.genre;
            formMetaData.FormatComments();
            if (this.myAdvancedOptions.genres != null)
            {
                formMetaData.cmbGenre.Items.Clear();
                for (int index = 0; index < this.myAdvancedOptions.genres.Length; ++index)
                    formMetaData.cmbGenre.Items.Add((object)this.myAdvancedOptions.genres[index]);
            }
            int num = (int)formMetaData.ShowDialog();
            if (!formMetaData.cancelled)
            {
                if (formMetaData.aaxFiles.Length == 1)
                {
                    this.myAudible.title = formMetaData.txtTitle.Text.Trim();
                    this.myAudible.album = formMetaData.txtAlbum.Text.Trim();
                    this.myAudible.author = formMetaData.txtAuthor.Text.Trim();
                    this.myAudible.narrator = formMetaData.txtNarrator.Text.Trim();
                    this.myAudible.year = formMetaData.txtYear.Text.Trim();
                    this.myAudible.addTrackToTitle = formMetaData.chkTrackToTitle.Checked;
                    this.myAudible.SetComments(formMetaData.txtComments.Text.Trim());
                    this.myAudible.SetComments(this.myAudible.GetComments().Replace('"', '\''));
                    this.myAudible.trackNum = formMetaData.txtTrackNum.Text;
                    this.myAudible.trackTotal = formMetaData.txtTrackTotal.Text;
                    this.myAudible.genre = formMetaData.cmbGenre.Text;
                    this.myAudible.publisher = formMetaData.txtPublisher.Text;
                    this.txtOutputFile.Text = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\" + this.myAudible.GetASCIITag(this.myAudible.title) + Path.GetExtension(this.txtOutputFile.Text);
                }
                else
                {
                    this.myAudibles = formMetaData.myAudibles;
                    this.myAudible = this.myAudibles[0];
                }
                this.myAdvancedOptions.genres = formMetaData.genres;
                if (this.cdProcessingMode && (this.chkBookTitle.Checked || this.chkAuthorTitle.Checked))
                {
                    string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                    string str2 = this.myAudible.title;
                    foreach (char ch in str1)
                        str2 = str2.Replace(ch.ToString(), "");
                    this.outputFileMask = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\" + str2 + ".mp3";
                    this.txtOutputFile.Text = this.outputFileMask;
                    this.saveFileDialog1.FileName = str2 + ".mp3";
                    this.inputFileName = this.txtInputFile.Text;
                    this.outputFileName = this.txtOutputFile.Text;
                }
            }
            formMetaData.Dispose();
        }

        private void AddTagsToComboSelect(ref ComboBox comboBox, AAXMetaData aAXMetaData)
        {
            comboBox.Items.Add((object)new ComboboxItem()
            {
                Text = aAXMetaData.nam,
                Value = (object)"nam"
            });
        }

        private void chkHelix_CheckedChanged(object sender, EventArgs e)
        {
            this.SetHelixMode();
        }

        private void SetHelixMode()
        {
            double num = 0.0;
            try
            {
                num = double.Parse(this.lblVBRquality.Text);
            }
            catch
            {
            }
            if (this.GetSelectedCodec() == "helix")
            {
                this.tbVBRquality.Minimum = 0;
                this.tbVBRquality.Maximum = 9;
                this.tbVBRquality.TickFrequency = 1;
                if (num > 9.0)
                {
                    this.tbVBRquality.Value = 5;
                    this.lblVBRquality.Text = this.tbVBRquality.Value.ToString();
                }
            }
            else if (this.GetSelectedCodec() == "lame")
            {
                this.tbVBRquality.Minimum = 0;
                this.tbVBRquality.Maximum = 150;
                this.tbVBRquality.TickFrequency = 5;
                if (num < 10.0)
                {
                    this.tbVBRquality.Value = 100;
                    this.lblVBRquality.Text = this.tbVBRquality.Value.ToString();
                }
                else if (num > 10.0)
                    this.tbVBRquality.Value = (int)num;
            }
            else if (this.GetSelectedCodec() == "fdk")
            {
                this.tbVBRquality.Minimum = 1;
                this.tbVBRquality.Maximum = 5;
                this.tbVBRquality.TickFrequency = 1;
                if (num > 9.0 || num < 1.0)
                {
                    this.tbVBRquality.Value = 3;
                    this.lblVBRquality.Text = this.tbVBRquality.Value.ToString();
                }
            }
            this.SetVBRDescription();
        }

        private void btnEditChapters_Click(object sender, EventArgs e)
        {
            this.myAudible.totalTime.Split(':');
            double totalSeconds = 0.0;
            string wFile = "";
            if (this.aaMode)
            {
                string[] strArray1 = this.txtInputFile.Text.Split(this.fileDeliminator);
                string[] strArray2 = this.getAudibleTotalTime(strArray1[0]).Split(':');
                totalSeconds = double.Parse(strArray2[0]) * 60.0 * 60.0 + double.Parse(strArray2[1]) * 60.0 + double.Parse(strArray2[2]);
                VirtualWAV virtualWav = new VirtualWAV();
                this.SetText("Decrypting original MP3...");
                string str = this.decryptMP3fromAA(strArray1[0], "", totalSeconds, this.IsXPMode());
                virtualWav.advancedOptions = this.myAdvancedOptions;
                virtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                virtualWav.aax2wavPath = this.supportLibs.aax2wavPath;
                virtualWav.sampleRate = 22050;
                virtualWav.channels = 1;
                virtualWav.AACtoWAV(str);
                virtualWav.totalSeconds = totalSeconds;
                AdvancedSplitting advancedSplitting = new AdvancedSplitting();
                advancedSplitting.soxPath = this.supportLibs.soxPath;
                advancedSplitting.ffmpegPath = this.supportLibs.ffmpegPath;
                advancedSplitting.sourceAacFile = str;
                if (this.myChapters.Count() == 0)
                {
                    if (this.IsXPMode() || this.myAudible.codec != "mp332")
                        this.myChapters.SetDoubleChapters(this.myAudible.getAudibleChapters(strArray1[0]));
                    else
                        this.myChapters.SetDoubleChapters(this.GetAAChapters(strArray1[0]));
                    this.myChapters.totalTime = totalSeconds;
                }
                advancedSplitting.myChapters = this.myChapters;
                advancedSplitting.m4bMode = true;
                virtualWav.totalSeconds = totalSeconds;
                advancedSplitting.myVirtualWav = virtualWav;
                advancedSplitting.Height = Screen.PrimaryScreen.Bounds.Height;
                advancedSplitting.Width = Screen.PrimaryScreen.Bounds.Width;
                int num = (int)advancedSplitting.ShowDialog();
                try
                {
                    System.IO.File.Delete(str);
                }
                catch
                {
                }
                if (advancedSplitting.applied)
                {
                    this.myChapters = advancedSplitting.myChapters;
                    this.myChapters.customChapters = true;
                    this.SetText("Custom chapters applied.");
                }
                advancedSplitting.Dispose();
                this.SetChapterUI();
            }
            else
            {
                if (!this.m4bTranscodeMode && this.myAdvancedOptions.decrypt && !this.cdProcessingMode)
                {
                    string[] files = this.txtInputFile.Text.Split(this.fileDeliminator);
                    string[] strArray = this.GetTotalTimeFromAAX(files[0]).Split(':');
                    totalSeconds = double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2]);
                    VirtualWAV myVirtualWav = new VirtualWAV();
                    myVirtualWav.advancedOptions = this.myAdvancedOptions;
                    this.SetText("Decrypting AAC from AAX...");
                    wFile = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".mp4";
                    this.SetEncodeMode();
                    string realTemp = Environment.GetEnvironmentVariable("tmp");
                    Environment.SetEnvironmentVariable("tmp", this.myAdvancedOptions.GetTempPath());
                    Audible.diskLogger("tmp=" + Environment.GetEnvironmentVariable("tmp") + ", realtemp=" + realTemp);
                    Task.Factory.StartNew((System.Action)(() =>
                   {
                       Environment.SetEnvironmentVariable("TEMP", this.myAdvancedOptions.GetTempPath());
                       if (!this.myAdvancedOptions.lowQualityPreview)
                           this.DecryptAacAudibleManager(myVirtualWav, files[0], wFile, totalSeconds);
                       if (this.myChapters.Count() == 0)
                           this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(files[0]));
                       if (!this.myAdvancedOptions.lowQualityPreview)
                           return;
                       this.SetText("Creating low-quality preview file...");
                       string str = Path.GetDirectoryName(this.outputFileMask) + "\\" + Path.GetFileNameWithoutExtension(this.outputFileMask) + ".wav";
                       Stopwatch stopwatch = new Stopwatch();
                       stopwatch.Start();
                       myVirtualWav.totalSeconds = totalSeconds;
                       string wavOutput = this.myAdvancedOptions.GetTempPath() + "\\inAudible-preview.wav";
                       this.VirtualWAV2PhysicalWAVffmpeg(files[0], myVirtualWav, wavOutput);
                       stopwatch.Stop();
                       TimeSpan elapsed = stopwatch.Elapsed;
                       Audible.diskLogger("Preview generated in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed.Hours, (object)elapsed.Minutes, (object)elapsed.Seconds, (object)(elapsed.Milliseconds / 10)));
                       try
                       {
                           System.IO.File.Delete(wFile);
                       }
                       catch
                       {
                       }
                       wFile = wavOutput;
                   })).ContinueWith((System.Action<Task>)(t =>
         {
                       AdvancedSplitting advancedSplitting = new AdvancedSplitting();
                       advancedSplitting.soxPath = this.supportLibs.soxPath;
                       advancedSplitting.ffmpegPath = this.supportLibs.ffmpegPath;
                       advancedSplitting.sourceAacFile = wFile;
                       this.myChapters.SetTotalTime(totalSeconds);
                       this.myChapters.SetEndChapter();
                       advancedSplitting.myChapters = this.myChapters;
                       advancedSplitting.m4bMode = true;
                       myVirtualWav.totalSeconds = totalSeconds;
                       advancedSplitting.myVirtualWav = myVirtualWav;
                       advancedSplitting.Height = Screen.PrimaryScreen.Bounds.Height;
                       advancedSplitting.Width = Screen.PrimaryScreen.Bounds.Width;
                       int num = (int)advancedSplitting.ShowDialog();
                       try
                       {
                           System.IO.File.Delete(wFile);
                       }
                       catch
                       {
                       }
                       if (advancedSplitting.applied)
                       {
                           this.SetText("Custom chapters applied.");
                           this.myChapters = advancedSplitting.myChapters;
                           this.myChapters.customChapterNames = !advancedSplitting.myChapters.generatedDescriptions;
                           this.myChapters.customChapters = true;
                           this.splitPoints = advancedSplitting.splitPoints;
                           this.SetChapterUI();
                       }
                       advancedSplitting.Dispose();
                       this.SetEncodeMode();
                       Environment.SetEnvironmentVariable("tmp", realTemp);
                   }), TaskScheduler.FromCurrentSynchronizationContext());
                }
                else if (this.m4bTranscodeMode)
                {
                    string realTemp = Environment.GetEnvironmentVariable("tmp");
                    Environment.SetEnvironmentVariable("tmp", this.myAdvancedOptions.GetTempPath());
                    string[] files = this.txtInputFile.Text.Split(this.fileDeliminator);
                    string[] strArray = this.GetTotalTimeFromAAX(files[0]).Split(':');
                    totalSeconds = double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2]);
                    this.SetEncodeMode();
                    AdvancedSplitting frmAdvancedSplitting = new AdvancedSplitting();
                    string wavOutput = "";
                    Task.Factory.StartNew((System.Action)(() =>
                   {
                       frmAdvancedSplitting.soxPath = this.supportLibs.soxPath;
                       frmAdvancedSplitting.ffmpegPath = this.supportLibs.ffmpegPath;
                       VirtualWAV myVirtualWav = new VirtualWAV();
                       myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                       myVirtualWav.aax2wavPath = this.supportLibs.aax2wavPath;
                       myVirtualWav.sampleRate = 22050;
                       myVirtualWav.channels = 2;
                       myVirtualWav.instarip = this.supportLibs.instaripPath;
                       myVirtualWav.aacMode = true;
                       if (this.myChapters.Count() == 0)
                       {
                           this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(files[0]));
                           this.myChapters.totalTime = totalSeconds / 1000.0;
                           this.myChapters.SetEndChapter();
                       }
                       if (this.myAdvancedOptions.lowQualityPreview)
                       {
                           Stopwatch stopwatch = new Stopwatch();
                           stopwatch.Start();
                           this.SetText("Creating low-quality preview file...");
                           wavOutput = Path.GetDirectoryName(files[0]) + "\\" + Path.GetFileNameWithoutExtension(files[0]) + ".wav";
                           myVirtualWav.AACtoWAV(files[0]);
                           wavOutput = this.myAdvancedOptions.GetTempPath() + "\\inAudible-preview.wav";
                           myVirtualWav.totalSeconds = totalSeconds;
                           this.VirtualWAV2PhysicalWAV(myVirtualWav, wavOutput);
                           if (new FileInfo(wavOutput).Length < 1000L)
                           {
                               Audible.diskLogger("ffmpeg blew up trying to stream this file.  Using alternate method");
                               this.aac2wav(files[0], wavOutput);
                           }
                           stopwatch.Stop();
                           TimeSpan elapsed = stopwatch.Elapsed;
                           Audible.diskLogger("Preview generated in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed.Hours, (object)elapsed.Minutes, (object)elapsed.Seconds, (object)(elapsed.Milliseconds / 10)));
                           frmAdvancedSplitting.sourceAacFile = wavOutput;
                       }
                       else
                           frmAdvancedSplitting.sourceAacFile = files[0];
                       frmAdvancedSplitting.myChapters = this.myChapters;
                       frmAdvancedSplitting.m4bMode = true;
                       if (this.myAdvancedOptions.lowQualityPreview)
                       {
                           myVirtualWav.M4BtoWAV(wavOutput);
                           this.GetSampleRateFromInput(wavOutput, ref myVirtualWav);
                       }
                       else
                       {
                           myVirtualWav.M4BtoWAV(files[0]);
                           this.GetSampleRateFromInput(files[0], ref myVirtualWav);
                       }
                       myVirtualWav.advancedOptions = this.myAdvancedOptions;
                       myVirtualWav.totalSeconds = totalSeconds;
                       frmAdvancedSplitting.myVirtualWav = myVirtualWav;
                       frmAdvancedSplitting.Height = Screen.PrimaryScreen.Bounds.Height;
                       frmAdvancedSplitting.Width = Screen.PrimaryScreen.Bounds.Width;
                   })).ContinueWith((System.Action<Task>)(t =>
         {
                       int num = (int)frmAdvancedSplitting.ShowDialog();
                       if (frmAdvancedSplitting.applied)
                       {
                           this.SetText("Custom chapters applied.");
                           this.myChapters = frmAdvancedSplitting.myChapters;
                           this.myChapters.customChapters = true;
                           this.myChapters.SetEndChapter();
                       }
                       frmAdvancedSplitting.Dispose();
                       try
                       {
                           System.IO.File.Delete(wavOutput);
                       }
                       catch
                       {
                       }
                       this.SetEncodeMode();
                       Environment.SetEnvironmentVariable("tmp", realTemp);
                       this.SetChapterUI();
                   }), TaskScheduler.FromCurrentSynchronizationContext());
                }
                else if (this.cdProcessingMode)
                {
                    VirtualWAV myVirtualWav = new VirtualWAV();
                    if (this.omniMode)
                        myVirtualWav = (VirtualWAV)this.omniWAV.Clone();
                    myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                    myVirtualWav.ffprobePath = this.supportLibs.ffprobePath;
                    myVirtualWav.supportLibs = this.supportLibs;
                    if (!this.omniMode)
                        myVirtualWav.ConstructCDWAV(this.txtInputFile.Text);
                    this.SetEncodeMode();
                    AdvancedSplitting frmAdvancedSplitting = new AdvancedSplitting();
                    string wavOutput = "";
                    string realTemp = Environment.GetEnvironmentVariable("tmp");
                    Environment.SetEnvironmentVariable("tmp", this.myAdvancedOptions.GetTempPath());
                    Task.Factory.StartNew((System.Action)(() =>
                   {
                       frmAdvancedSplitting.soxPath = this.supportLibs.soxPath;
                       frmAdvancedSplitting.ffmpegPath = this.supportLibs.ffmpegPath;
                       this.myChapters.totalTime = myVirtualWav.totalSeconds;
                       this.myChapters.SetEndChapter();
                       frmAdvancedSplitting.myChapters = this.myChapters;
                       if (this.myAdvancedOptions.lowQualityPreview)
                       {
                           Stopwatch stopwatch = new Stopwatch();
                           stopwatch.Start();
                           this.SetText("Creating low-quality preview file...");
                           string str = this.txtInputFile.Text.Split(this.fileDeliminator)[0];
                           wavOutput = this.myAdvancedOptions.GetTempPath() + "\\inAudible-preview.wav";
                           this.VirtualWAV2PhysicalWAV(myVirtualWav, wavOutput);
                           stopwatch.Stop();
                           TimeSpan elapsed = stopwatch.Elapsed;
                           Audible.diskLogger("Preview generated in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed.Hours, (object)elapsed.Minutes, (object)elapsed.Seconds, (object)(elapsed.Milliseconds / 10)));
                           frmAdvancedSplitting.sourceAacFile = wavOutput;
                           myVirtualWav.M4BtoWAV(wavOutput);
                           this.GetSampleRateFromInput(wavOutput, ref myVirtualWav);
                           myVirtualWav.advancedOptions = this.myAdvancedOptions;
                           myVirtualWav.totalSeconds = totalSeconds;
                           frmAdvancedSplitting.myVirtualWav = myVirtualWav;
                           frmAdvancedSplitting.m4bMode = true;
                       }
                       else if (this.omniMode)
                       {
                           this.SetText("Creating high quality monolithic file for editing...");
                           myVirtualWav.aacMode = true;
                           frmAdvancedSplitting.myChapters = this.myChapters;
                           frmAdvancedSplitting.m4bMode = true;
                           string str = this.txtInputFile.Text.Split(this.fileDeliminator)[0];
                           wavOutput = this.myAdvancedOptions.GetTempPath() + "\\inAudible-preview." + myVirtualWav.inputFileType;
                           Stopwatch stopwatch = new Stopwatch();
                           stopwatch.Start();
                           this.ConcatenateOmni(myVirtualWav, wavOutput);
                           stopwatch.Stop();
                           TimeSpan elapsed = stopwatch.Elapsed;
                           Audible.diskLogger("Preview generated in " + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", (object)elapsed.Hours, (object)elapsed.Minutes, (object)elapsed.Seconds, (object)(elapsed.Milliseconds / 10)));
                           frmAdvancedSplitting.sourceAacFile = wavOutput;
                           myVirtualWav.M4BtoWAV(wavOutput);
                           this.GetSampleRateFromInput(wavOutput, ref myVirtualWav);
                           myVirtualWav.advancedOptions = this.myAdvancedOptions;
                           myVirtualWav.totalSeconds = totalSeconds;
                           frmAdvancedSplitting.myVirtualWav = myVirtualWav;
                       }
                       else
                       {
                           frmAdvancedSplitting.myVirtualWav = myVirtualWav;
                           frmAdvancedSplitting.cdProcessingMode = true;
                       }
                       frmAdvancedSplitting.Height = Screen.PrimaryScreen.Bounds.Height;
                       frmAdvancedSplitting.Width = Screen.PrimaryScreen.Bounds.Width;
                   })).ContinueWith((System.Action<Task>)(t =>
         {
                       int num = (int)frmAdvancedSplitting.ShowDialog();
                       if (!this.myAdvancedOptions.lowQualityPreview)
                       {
                           if (!this.omniMode)
                               goto label_4;
                       }
                       try
                       {
                           System.IO.File.Delete(wavOutput);
                       }
                       catch
                       {
                       }
                       label_4:
                       if (frmAdvancedSplitting.applied)
                       {
                           this.SetText("Custom chapters applied.");
                           this.myChapters = frmAdvancedSplitting.myChapters;
                           this.myChapters.customChapters = true;
                       }
                       frmAdvancedSplitting.Dispose();
                       this.SetEncodeMode();
                       Environment.SetEnvironmentVariable("tmp", realTemp);
                       this.SetChapterUI();
                   }), TaskScheduler.FromCurrentSynchronizationContext());
                }
                this.SetChapterUI();
            }
        }

        private void ConcatenateOmni(VirtualWAV myVirtualWav, string wavOutput)
        {
            string ffmpegConcat = myVirtualWav.GetFFmpegConcat();
            string str = "-y -loglevel panic -f concat -i \"" + ffmpegConcat + "\" -c copy \"" + wavOutput + "\"";
            if (Path.GetExtension(wavOutput).ToLower() == ".flac")
                str = "-y -loglevel panic -f concat -i \"" + ffmpegConcat + "\" \"" + wavOutput + "\"";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = str;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            System.IO.File.Delete(ffmpegConcat);
        }

        private string ngGetKey(string file)
        {
            foreach (string ngKey in this.myAdvancedOptions.ngKeys)
            {
                if (!(ngKey == "") && this.ngVerifyKey(file, ngKey) != -99)
                    return ngKey;
            }
            string checksum = this.GetChecksum(file);
            this.SetText("Checksum: " + checksum);
            string str = this.CrackAAX(checksum);
            this.SetText("Derived key - " + str);
            string[] strArray = new string[this.myAdvancedOptions.ngKeys.Length + 1];
            for (int index = 0; index < this.myAdvancedOptions.ngKeys.Length; ++index)
                strArray[index] = this.myAdvancedOptions.ngKeys[index];
            strArray[this.myAdvancedOptions.ngKeys.Length] = str;
            this.myAdvancedOptions.ngKeys = strArray;
            return str;
        }

        private int ngVerifyKey(string file, string key)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-y -activation_bytes " + key + " -i \"" + file + "\"";
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            string end = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (end.Contains("[aax] mismatch in checksums"))
                return -99;
            return process.ExitCode;
        }

        private void CreatePreviewWAVFromAAX(string key, string aaxFile, string wavFile, int sampleRate, Lame myLame, double totalSeconds)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-y -activation_bytes " + key + " -i \"" + aaxFile + "\" -ac 1 -ar " + (object)sampleRate + " \"" + wavFile + "\"";
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.ErrorDataReceived += (DataReceivedEventHandler)((sender, e) =>
           {
               int num = Utilities.PercentageCompleteByDuration(Utilities.GetFFmpegTime(e.Data), totalSeconds);
               if (num <= -1)
                   return;
               myLame.percentComplete = num;
           });
            process.Start();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.CancelErrorRead();
        }

        private void VirtualWAV2PhysicalWAVffmpeg(string aaxFile, VirtualWAV myVirtualWav, string wavOutput)
        {
            Lame myLame1 = new Lame();
            myLame1.myAdvancedOptions = this.myAdvancedOptions;
            myLame1.ffmpegPath = this.supportLibs.ffmpegPath;
            int num1 = 0;
            int totalSeconds = (int)myVirtualWav.totalSeconds;
            EncodingOptions myEncodingOptions = this.GetEncodingOptions();
            myEncodingOptions.encoder = "fullwav";
            myEncodingOptions.startChap = (long)num1;
            myEncodingOptions.endChap = (long)totalSeconds;
            myLame1.soxPath = this.supportLibs.soxPath;
            myEncodingOptions.sampleRate = myVirtualWav.sampleRate;
            myEncodingOptions.channels = 1;
            myEncodingOptions.sampleRate = 11025;
            long num2 = (long)((double)myEncodingOptions.sampleRate * (double)myEncodingOptions.channels * 2.0 * (double)(myEncodingOptions.endChap - myEncodingOptions.startChap));
            if (num2 > 2500000000L)
            {
                Audible.diskLogger("File is " + (object)num2 + ". Downsampling to 6000hz");
                myEncodingOptions.sampleRate = 6000;
            }
            string aaxKey = this.ngGetKey(aaxFile);
            Task task = Task.Factory.StartNew((System.Action)(() => this.CreatePreviewWAVFromAAX(aaxKey, aaxFile, wavOutput, myEncodingOptions.sampleRate, myLame1, myVirtualWav.totalSeconds)));
            while (!task.IsCompleted)
            {
                Thread.Sleep(1000);
                try
                {
                    this.UpdateProgressBar(myLame1.percentComplete, "blue");
                }
                catch
                {
                }
            }
            this.UpdateProgressBar(0, "default");
        }

        private void VirtualWAV2PhysicalWAV(VirtualWAV myVirtualWav, string wavOutput)
        {
            Lame myLame1 = new Lame();
            myLame1.myAdvancedOptions = this.myAdvancedOptions;
            myLame1.ffmpegPath = this.supportLibs.ffmpegPath;
            int num1 = 0;
            int totalSeconds = (int)myVirtualWav.totalSeconds;
            EncodingOptions myEncodingOptions = this.GetEncodingOptions();
            myEncodingOptions.encoder = "fullwav";
            myEncodingOptions.startChap = (long)num1;
            myEncodingOptions.endChap = (long)totalSeconds;
            myLame1.soxPath = this.supportLibs.soxPath;
            myEncodingOptions.sampleRate = myVirtualWav.sampleRate;
            myEncodingOptions.channels = 1;
            myEncodingOptions.sampleRate = 11025;
            long num2 = (long)((double)myEncodingOptions.sampleRate * (double)myEncodingOptions.channels * 2.0 * (double)(myEncodingOptions.endChap - myEncodingOptions.startChap));
            if (num2 > 2500000000L)
            {
                Audible.diskLogger("File is " + (object)num2 + ". Downsampling to 6000hz");
                myEncodingOptions.sampleRate = 6000;
            }
            Task task = (Task)Task.Factory.StartNew<int>((System.Func<int>)(() => myLame1.PreprocessVirtualWav(myVirtualWav, wavOutput, myEncodingOptions)));
            while (!task.IsCompleted)
            {
                Thread.Sleep(1000);
                try
                {
                    this.UpdateProgressBar(myLame1.percentComplete, "blue");
                }
                catch
                {
                }
            }
            this.UpdateProgressBar(0, "default");
        }

        private void aac2wav(string source, string target)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = this.supportLibs.ffmpegPath;
            process.StartInfo.Arguments = "-y -i \"" + source + "\" -acodec pcm_s16le -ac 1 -ar 11025 \"" + target + "\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

        private void LoadWAVs()
        {
            try
            {
                this.cdWavPath = new FileIniDataParser().LoadFile(this.iniPath)["system"]["wav_path"];
            }
            catch
            {
            }
            if (CommonFileDialog.IsPlatformSupported)
            {
                CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
                commonOpenFileDialog.IsFolderPicker = true;
                commonOpenFileDialog.Title = "Select source folder";
                if (this.txtInputFile.Text.Trim() != "")
                    commonOpenFileDialog.InitialDirectory = this.txtInputFile.Text;
                else if (this.cdWavPath != "")
                    commonOpenFileDialog.InitialDirectory = this.cdWavPath;
                if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
                    return;
                this.txtInputFile.Text = commonOpenFileDialog.FileName;
                this.AddWAVFiles(commonOpenFileDialog.FileName);
                this.SetLossyMode();
                this.setEncodingUI();
                this.chkVerifyAudibleSplits.Checked = false;
                this.chkRemoveAudibleMarkers.Checked = false;
                this.m4bTranscodeMode = false;
                this.omniMode = false;
                this.finishedBatch = false;
                this.SetChapterUI();
            }
            else
            {
                FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx();
                folderBrowserDialogEx.Description = "Select a folder to extract to:";
                folderBrowserDialogEx.ShowNewFolderButton = true;
                folderBrowserDialogEx.ShowEditBox = true;
                if (this.txtInputFile.Text.Trim() != "")
                    folderBrowserDialogEx.SelectedPath = this.txtInputFile.Text;
                else if (this.cdWavPath != "")
                    folderBrowserDialogEx.SelectedPath = this.cdWavPath;
                folderBrowserDialogEx.ShowFullPathInEditBox = true;
                folderBrowserDialogEx.RootFolder = Environment.SpecialFolder.MyComputer;
                if (folderBrowserDialogEx.ShowDialog() != DialogResult.OK)
                    return;
                this.txtInputFile.Text = folderBrowserDialogEx.SelectedPath;
                this.AddWAVFiles(folderBrowserDialogEx.SelectedPath);
                this.setEncodingUI();
                this.chkVerifyAudibleSplits.Checked = false;
                this.chkRemoveAudibleMarkers.Checked = false;
                this.m4bTranscodeMode = false;
                this.finishedBatch = false;
            }
        }

        private void btnWAVinput_Click(object sender, EventArgs e)
        {
            this.LoadWAVs();
        }

        private void AddWAVFiles(string dir)
        {
            this.grpLame.Enabled = true;
            this.grpSplitting.Enabled = true;
            this.grpOutputType.Enabled = true;
            this.aaxMode = true;
            this.aaxOnce = true;
            this.studioProcessingMode = true;
            this.cdProcessingMode = true;
            string fileName = Path.GetFileName(dir);
            VirtualWAV virtualWav = new VirtualWAV();
            virtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            virtualWav.ffprobePath = this.supportLibs.ffprobePath;
            this.myChapters.SetDoubleChapters(virtualWav.ConstructCDWAV(dir));
            this.myAudible.title = fileName;
            this.myAudible.totalTime = virtualWav.GetFormattedTime();
            this.btnEditChapters.Visible = true;
            this.setAAXoptions(Form1.ProcessingMode.Other, "");
            this.SetText("WARNING-CD/WAV mode active.");
            string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string str2 = this.myAudible.title;
            foreach (char ch in str1)
                str2 = str2.Replace(ch.ToString(), "");
            this.outputFileMask = Path.GetDirectoryName(dir) + "\\" + str2 + ".mp3";
            this.txtOutputFile.Text = this.outputFileMask;
            this.saveFileDialog1.FileName = str2 + ".mp3";
            this.inputFileName = this.txtInputFile.Text;
            this.outputFileName = this.txtOutputFile.Text;
            this.SetText("Book: " + this.myAudible.title);
            this.SetText("Total Input WAVs = " + (object)virtualWav.GetTotalWavs().Count);
            this.SetText("Sample Rate = " + (object)virtualWav.sampleRate + " Hz");
            this.myAudible.nfo.sourceSampleRate = virtualWav.sampleRate.ToString();
            this.SetText("Channels = " + (object)virtualWav.channels);
            this.myAudible.nfo.sourceChannels = virtualWav.channels.ToString();
            this.myAudible.nfo.totalTime = this.getAudibleTotalTime(dir);
            this.SetText("Total Time = " + this.myAudible.nfo.totalTime);
            this.myAudible.nfo.sourceFormat = "CD/WAV";
            this.myAudible.nfo.sourceBitrate = virtualWav.originalBitrate.ToString();
            if (virtualWav.totalSeconds != 0.0)
                return;
            this.SetText("WARNING-Could not find any WAV files in this directory.");
        }

        private void AddWAVFiles(string[] files, string[] mp3Files, FormMP3Import.TITLE_MODE titleMode, FormMP3Import.TAG_MODE tagMode)
        {
            this.grpLame.Enabled = true;
            this.grpSplitting.Enabled = true;
            this.grpOutputType.Enabled = true;
            this.aaxMode = true;
            this.aaxOnce = true;
            this.studioProcessingMode = true;
            this.cdProcessingMode = true;
            string title = TagLib.File.Create(mp3Files[0]).Tag.Title;
            VirtualWAV virtualWav = new VirtualWAV();
            virtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            virtualWav.ffprobePath = this.supportLibs.ffprobePath;
            List<string> desc = new List<string>();
            if (tagMode == FormMP3Import.TAG_MODE.FILE)
            {
                foreach (string mp3File in mp3Files)
                    desc.Add(Path.GetFileNameWithoutExtension(mp3File));
                this.myChapters.SetChaptersAndDescriptions(virtualWav.ConstructMP3WAV(files), desc);
            }
            else if (tagMode == FormMP3Import.TAG_MODE.TAG)
            {
                foreach (string mp3File in mp3Files)
                {
                    string str = "";
                    try
                    {
                        str = TagLib.File.Create(mp3File).Tag.Title;
                    }
                    catch
                    {
                    }
                    if (str == null || str == "")
                        desc.Add(Path.GetFileNameWithoutExtension(mp3File));
                    else
                        desc.Add(str);
                }
                this.myChapters.SetChaptersAndDescriptions(virtualWav.ConstructMP3WAV(files), desc);
            }
            else if (tagMode == FormMP3Import.TAG_MODE.GENERATED)
                this.myChapters.SetDoubleChapters(virtualWav.ConstructMP3WAV(files));
            this.myAudible.title = title;
            this.myAudible.totalTime = virtualWav.GetFormattedTime();
            this.btnEditChapters.Visible = true;
            this.setAAXoptions(Form1.ProcessingMode.Other, "");
            string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string str2 = this.myAudible.title ?? Path.GetFileNameWithoutExtension(mp3Files[0]);
            if (titleMode == FormMP3Import.TITLE_MODE.FILE)
            {
                str2 = Path.GetFileNameWithoutExtension(mp3Files[0]);
                this.myAudible.title = str2;
            }
            else if (this.myAudible.title != null || this.myAudible.title != "")
                str2 = this.myAudible.title;
            foreach (char ch in str1)
                str2 = str2.Replace(ch.ToString(), "");
            this.outputFileMask = Path.GetDirectoryName(mp3Files[0]) + "\\" + str2 + ".mp3";
            this.txtOutputFile.Text = this.outputFileMask;
            this.saveFileDialog1.FileName = str2 + ".mp3";
            this.inputFileName = this.txtInputFile.Text;
            this.outputFileName = this.txtOutputFile.Text;
            this.SetText("Book: " + this.myAudible.title);
            this.SetText("Total Input WAVs = " + (object)virtualWav.GetTotalWavs().Count);
            this.SetText("Sample Rate = " + (object)virtualWav.sampleRate + " Hz");
            this.myAudible.nfo.sourceSampleRate = virtualWav.sampleRate.ToString();
            this.SetText("Channels = " + (object)virtualWav.channels);
            this.myAudible.nfo.sourceChannels = virtualWav.channels.ToString();
            this.myAudible.nfo.totalTime = this.getAudibleTotalTime(mp3Files[0]);
            this.SetText("Total Time = " + this.myAudible.nfo.totalTime);
            this.myAudible.nfo.sourceFormat = "MP3";
            this.myAudible.nfo.sourceBitrate = virtualWav.originalBitrate.ToString();
            if (virtualWav.totalSeconds != 0.0)
                return;
            this.SetText("WARNING-Could not find any WAV files in this directory.");
        }

        private void Add3GPFiles(string[] files, string[] mp3Files)
        {
            this.grpLame.Enabled = true;
            this.grpSplitting.Enabled = true;
            this.grpOutputType.Enabled = true;
            this.aaxMode = true;
            this.aaxOnce = true;
            this.studioProcessingMode = true;
            this.cdProcessingMode = true;
            VirtualWAV virtualWav = new VirtualWAV();
            virtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            virtualWav.ffprobePath = this.supportLibs.ffprobePath;
            List<string> desc = new List<string>();
            int num = 1;
            foreach (string mp3File in mp3Files)
            {
                if (Path.GetFileNameWithoutExtension(mp3File).EndsWith("ann"))
                    desc.Add("Annotations");
                else
                    desc.Add(num.ToString("D2"));
                ++num;
            }
            this.myChapters.SetChaptersAndDescriptions(virtualWav.ConstructMP3WAV(files), desc);
            this.myAudible.totalTime = virtualWav.GetFormattedTime();
            this.btnEditChapters.Visible = true;
            this.setAAXoptions(Form1.ProcessingMode.Other, "");
            string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string str2 = this.myAudible.title ?? Path.GetFileNameWithoutExtension(mp3Files[0]);
            foreach (char ch in str1)
                str2 = str2.Replace(ch.ToString(), "");
            this.outputFileMask = Path.GetDirectoryName(mp3Files[0]) + "\\" + str2 + ".mp3";
            this.txtOutputFile.Text = this.outputFileMask;
            this.saveFileDialog1.FileName = str2 + ".mp3";
            this.inputFileName = this.txtInputFile.Text;
            this.outputFileName = this.txtOutputFile.Text;
            this.myAudible.nfo.sourceSampleRate = virtualWav.sampleRate.ToString();
            this.myAudible.nfo.sourceChannels = virtualWav.channels.ToString();
            this.myAudible.nfo.totalTime = this.getAudibleTotalTime(mp3Files[0]);
            this.myAudible.nfo.sourceBitrate = virtualWav.originalBitrate.ToString();
            if (virtualWav.totalSeconds != 0.0)
                return;
            this.SetText("WARNING-Could not find any WAV files in this directory.");
        }

        private void btnAdvancedOptions_Click(object sender, EventArgs e)
        {
            frmAdvancedOptions frmAdvancedOptions = new frmAdvancedOptions();
            frmAdvancedOptions.advancedOptions = this.myAdvancedOptions;
            frmAdvancedOptions.SetFields();
            int num = (int)frmAdvancedOptions.ShowDialog();
            this.myAdvancedOptions = frmAdvancedOptions.advancedOptions;
            if (!(this.myAdvancedOptions.ffmpegPath != ""))
                return;
            this.fdkPath = this.myAdvancedOptions.ffmpegPath;
        }

        private void LaunchSingleRipper()
        {
            if (this.myAdvancedOptions.newRipper)
            {
                FormCDripCUE formCdripCue = new FormCDripCUE();
                formCdripCue.supportLibs = this.supportLibs;
                formCdripCue.cddaPath = this.supportLibs.cddaPath;
                formCdripCue.CreateDriveDropdown();
                int num = (int)formCdripCue.ShowDialog();
                if (formCdripCue.apply)
                {
                    this.txtInputFile.Text = formCdripCue.txtOutputPath.Text;
                    this.AddWAVFiles(this.txtInputFile.Text);
                    this.setEncodingUI();
                    this.chkVerifyAudibleSplits.Checked = false;
                    this.chkRemoveAudibleMarkers.Checked = false;
                    this.m4bTranscodeMode = false;
                    this.finishedBatch = false;
                }
                formCdripCue.Dispose();
            }
            else
            {
                frmCDrip frmCdrip = new frmCDrip();
                frmCdrip.cddaPath = this.supportLibs.cddaPath;
                frmCdrip.CreateDriveDropdown();
                int num = (int)frmCdrip.ShowDialog();
                if (frmCdrip.apply)
                {
                    this.txtInputFile.Text = frmCdrip.txtOutputPath.Text;
                    this.AddWAVFiles(this.txtInputFile.Text);
                    this.setEncodingUI();
                    this.chkVerifyAudibleSplits.Checked = false;
                    this.chkRemoveAudibleMarkers.Checked = false;
                    this.m4bTranscodeMode = false;
                    this.finishedBatch = false;
                }
                frmCdrip.Dispose();
            }
        }

        private void btnCDrip_Click(object sender, EventArgs e)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Single Drive");
            contextMenuStrip.Items.Add("Multi Drive");
            contextMenuStrip.Show((Control)this.btnCDrip, new Point(0, this.btnCDrip.Height));
            contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Multi Drive")
                this.LaunchMultiRipper();
            else
                this.LaunchSingleRipper();
        }

        private void chkTrueDecrypt_CheckedChanged(object sender, EventArgs e)
        {
            this.setEncodingUI();
        }

        private void btnSplitter_Click(object sender, EventArgs e)
        {
            if (this.myAdvancedOptions.renameOptions == "")
            {
                this.LoadPrefs("Other");
                this.myAdvancedOptions.okayToSaveSettings = true;
                this.aaxMode = true;
            }
            AdvancedSplitting advancedSplitting = new AdvancedSplitting();
            advancedSplitting.supportLibs = this.supportLibs;
            advancedSplitting.soxPath = this.supportLibs.soxPath;
            advancedSplitting.ffmpegPath = this.supportLibs.ffmpegPath;
            advancedSplitting.mp4boxPath = this.supportLibs.mp4boxPath;
            advancedSplitting.fileMode = true;
            advancedSplitting.myVirtualWav = new VirtualWAV()
            {
                advancedOptions = this.myAdvancedOptions,
                ffmpegPath = this.supportLibs.ffmpegPath,
                soxPath = this.supportLibs.soxPath
            };
            advancedSplitting.Height = Screen.PrimaryScreen.Bounds.Height;
            advancedSplitting.Width = Screen.PrimaryScreen.Bounds.Width;
            int num = (int)advancedSplitting.ShowDialog();
        }

        private void startDownload(string url, string targetFile)
        {
            this.SetText("Downloading to " + targetFile + "...");
            WebClient webClient = new WebClient();
            webClient.Headers["User-Agent"] = "Audible ADM 6.6.0.15;Windows Vista Service Pack 1 Build 7601";
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
            webClient.DownloadFileAsync(new Uri(url), targetFile, (object)targetFile);
            Form1.SetControlPropertyThreadSafe((Control)this.lblDownloadStatus, "Visible", (object)true);
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double d = double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100.0;
            if (!this.uiReadyForUpdate && d < 100.0)
                return;
            Form1.SetControlPropertyThreadSafe((Control)this.lblDownloadStatus, "Text", (object)("Downloaded " + e.BytesReceived.ToString("#,##0") + " of " + e.TotalBytesToReceive.ToString("#,##0")));
            this.UpdateProgressBar(int.Parse(Math.Truncate(d).ToString()), "default");
            this.uiReadyForUpdate = false;
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.myChapters.Clear();
            this.cdProcessingMode = false;
            this.flacUnpacked = false;
            this.Invoke((System.Action)(() =>
           {
               this.rtbLog.Clear();
               this.reRun = false;
               string[] inputFiles = new string[1]
          {
          (string) e.UserState
             };
               this.lblDownloadStatus.Text = "Completed";
               this.lblDownloadStatus.Visible = false;
               this.SetDownloadWindow(false);
               this.LoadAudibleFiles(inputFiles);
               if (!this.autoEncodeOnDownload)
                   return;
               this.btnConvert.PerformClick();
           }));
        }

        private void btnInputFile_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                string text = Clipboard.GetText();
                if (!text.ToLower().StartsWith("http"))
                    return;
                this.SetText(this.GetBookIdFromURL(text, true));
            }
            if (e.Button != MouseButtons.Right)
                return;
            string text1 = Clipboard.GetText();
            if (!text1.ToLower().StartsWith("http"))
                return;
            string bookIdFromUrl = this.GetBookIdFromURL(text1, false);
            if (bookIdFromUrl == "")
                return;
            this.SetText(bookIdFromUrl);
            if (MessageBox.Show("Download this book directly from Audible.com?", "Download AAX", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            string str = "inAudible.aax";
            string targetFile = Form1.thumbnailDir + "\\" + str;
            using (new WebClient())
                this.startDownload(bookIdFromUrl, targetFile);
        }

        private string GetBookIdFromURL(string url, bool pirate)
        {
            if (!pirate)
                this.GetAudibleIdFromPrefs();
            string str1 = "";
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "GET";
            string end;
            using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                end = streamReader.ReadToEnd();
            string str2 = "";
            string audibleCustomerId = this.myAdvancedOptions.audibleCustomerId;
            string str3 = end;
            char[] chArray1 = new char[1] { '\n' };
            foreach (string str4 in str3.Split(chArray1))
            {
                if (str4.Contains("product_sku"))
                    str2 = str4.Split(':')[1].Replace("\"", "").Replace(",", "");
            }
            if (pirate)
            {
                List<string> stringList = new List<string>();
                string str4 = end;
                char[] chArray2 = new char[1] { '\n' };
                foreach (string str5 in str4.Split(chArray2))
                {
                    if (str5.Contains("/listener/"))
                    {
                        string[] strArray = str5.Split('/');
                        for (int index = 0; index < strArray.Length; ++index)
                        {
                            if (strArray[index] == "listener")
                                stringList.Add(strArray[index + 2]);
                        }
                    }
                }
                str1 = "\r\n";
                foreach (string str5 in stringList)
                    str1 = str1 + "http://cds.audible.com/download?user_id=&product_id=" + str2 + "&domain=&order_number=&cust_id=" + str5 + "&codec=LC_64_22050_stereo&awtype=AAX\r\n\r\n";
            }
            else if (!(str2 == ""))
                str1 = "http://cds.audible.com/download?user_id=&product_id=" + str2 + "&domain=&order_number=&cust_id=" + audibleCustomerId + "&codec=LC_64_22050_stereo&awtype=AAX";
            return str1;
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            int num = (int)new FormRenamer().ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void advancedSplitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnSplitter.PerformClick();
        }

        private void chapterEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnEditChapters.PerformClick();
        }

        private void renamingToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRenamer formRenamer = new FormRenamer();
            if (this.myAdvancedOptions.renameOptions == "")
            {
                this.LoadPrefs("Other");
                this.myAdvancedOptions.okayToSaveSettings = true;
                this.aaxMode = true;
            }
            formRenamer.myAdvancedOptions = this.myAdvancedOptions;
            formRenamer.LoadPrefs();
            int num = (int)formRenamer.ShowDialog();
            this.myAdvancedOptions = formRenamer.myAdvancedOptions;
        }

        private void advancedSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnAdvancedOptions.PerformClick();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnInputFile.PerformClick();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.lblTitle.Text = this.Text;
            formAbout.audibleChaptersPath = this.supportLibs.audibleChaptersPath;
            formAbout.neroAACpath = this.supportLibs.neroAACpath;
            formAbout.mp4chapsPath = this.supportLibs.mp4chapsPath;
            formAbout.neroAACtagPath = this.supportLibs.neroAACtagPath;
            formAbout.soxPath = this.supportLibs.soxPath;
            formAbout.iTunesProxyPath = this.supportLibs.iTunesProxyPath;
            formAbout.lamePath = this.supportLibs.lamePath;
            formAbout.helixPath = this.supportLibs.helixPath;
            formAbout.mp3SplitPath = this.supportLibs.mp3SplitPath;
            formAbout.mp4boxPath = this.supportLibs.mp4boxPath;
            formAbout.ngPath = this.supportLibs.ngPath;
            formAbout.ffmpegPath = this.supportLibs.ffmpegPath;
            formAbout.ffprobePath = this.supportLibs.ffprobePath;
            formAbout.opusPath = this.supportLibs.opusPath;
            formAbout.oggPath = this.supportLibs.oggPath;
            formAbout.cddaPath = this.supportLibs.cddaPath;
            formAbout.mp4ArtPath = this.supportLibs.mp4ArtPath;
            formAbout.aax2wavPath = this.supportLibs.aax2wavPath;
            formAbout.instaripPath = this.supportLibs.instaripPath;
            formAbout.atomicParsleyPath = this.supportLibs.atomicParsleyPath;
            formAbout.trackDumpPath = this.supportLibs.trackDumpPath;
            formAbout.GetVersions();
            int num = (int)formAbout.ShowDialog();
        }

        private void cDRipperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnCDrip.PerformClick();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.btnInputFile.PerformClick();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            this.btnSplitter.PerformClick();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            this.btnEditChapters.PerformClick();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            FormRenamer formRenamer = new FormRenamer();
            if (this.myAdvancedOptions.renameOptions == "")
            {
                this.LoadPrefs("AAX");
                this.myAdvancedOptions.okayToSaveSettings = true;
                this.aaxMode = true;
            }
            formRenamer.myAdvancedOptions = this.myAdvancedOptions;
            formRenamer.LoadPrefs();
            int num = (int)formRenamer.ShowDialog();
            this.myAdvancedOptions = formRenamer.myAdvancedOptions;
            if (!formRenamer.loadSession)
                return;
            List<string> stringList = new List<string>();
            for (int index = 0; index < formRenamer.renameList.Count; ++index)
                stringList.Add(formRenamer.renameList[index].SourceFile);
            if (stringList.Count <= 0)
                return;
            this.LoadAudibleFiles(stringList.ToArray());
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            this.LaunchSingleRipper();
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.lblTitle.Text = this.Text;
            formAbout.audibleChaptersPath = this.supportLibs.audibleChaptersPath;
            formAbout.neroAACpath = this.supportLibs.neroAACpath;
            formAbout.mp4chapsPath = this.supportLibs.mp4chapsPath;
            formAbout.neroAACtagPath = this.supportLibs.neroAACtagPath;
            formAbout.soxPath = this.supportLibs.soxPath;
            formAbout.iTunesProxyPath = this.supportLibs.iTunesProxyPath;
            formAbout.lamePath = this.supportLibs.lamePath;
            formAbout.helixPath = this.supportLibs.helixPath;
            formAbout.mp3SplitPath = this.supportLibs.mp3SplitPath;
            formAbout.mp4boxPath = this.supportLibs.mp4boxPath;
            formAbout.ngPath = this.supportLibs.ngPath;
            formAbout.ffmpegPath = this.supportLibs.ffmpegPath;
            formAbout.ffprobePath = this.supportLibs.ffprobePath;
            formAbout.opusPath = this.supportLibs.opusPath;
            formAbout.oggPath = this.supportLibs.oggPath;
            formAbout.cddaPath = this.supportLibs.cddaPath;
            formAbout.mp4ArtPath = this.supportLibs.mp4ArtPath;
            formAbout.aax2wavPath = this.supportLibs.aax2wavPath;
            formAbout.instaripPath = this.supportLibs.instaripPath;
            formAbout.atomicParsleyPath = this.supportLibs.atomicParsleyPath;
            formAbout.trackDumpPath = this.supportLibs.trackDumpPath;
            formAbout.mySupportLibs = this.supportLibs;
            formAbout.StartMusic();
            formAbout.GetVersions();
            formAbout.StartScroll();
            int num = (int)formAbout.ShowDialog();
            formAbout.Dispose();
        }

        private void chkSplitByDuration_CheckedChanged(object sender, EventArgs e)
        {
            this.chkKeepMonolithicMP3.Enabled = false;
            this.chkKeepMonolithicMP3.Checked = false;
        }

        private void btnChapterMetadata_Click(object sender, EventArgs e)
        {
            string[] strArray1 = this.txtInputFile.Text.Split(this.fileDeliminator);
            if (this.myChapters.Count() == 0 && this.aaMode)
                this.myChapters.SetDoubleChapters(this.myAudible.getAudibleChapters(strArray1[0]));
            else if (this.myChapters.Count() == 0 && !this.cdProcessingMode)
                this.myChapters.SetDoubleChapters(this.myAudible.getAAXChapters(strArray1[0]));
            else if (this.myChapters.Count() == 0 && this.cdProcessingMode)
            {
                new VirtualWAV()
                {
                    ffmpegPath = this.supportLibs.ffmpegPath,
                    ffprobePath = this.supportLibs.ffprobePath
                }.ConstructCDWAV(this.txtInputFile.Text);
                AdvancedSplitting advancedSplitting = new AdvancedSplitting();
                advancedSplitting.soxPath = this.supportLibs.soxPath;
                advancedSplitting.ffmpegPath = this.supportLibs.ffmpegPath;
                this.myChapters.SetEndChapter();
                advancedSplitting.myChapters = this.myChapters;
            }
            FormChapterNames formChapterNames = new FormChapterNames();
            formChapterNames.chkUseChapterAsTitle.Checked = this.myAdvancedOptions.useChapterAsTitle;
            formChapterNames.includeChapterNumbers = this.myAdvancedOptions.includeChapterNumberInFilename;
            formChapterNames.chkChapterNumbers.Checked = this.myAdvancedOptions.includeChapterNumberInFilename;
            formChapterNames.chkNoTitleInFilename.Checked = this.myAdvancedOptions.noTitleInFilename;
            formChapterNames.chkTitleAndNumber.Checked = this.myAdvancedOptions.includeChapterAndTitleInTitleTag;
            formChapterNames.chapters = this.myChapters.GetDoubleList();
            this.myChapters.generatedDescriptions = false;
            formChapterNames.chapterNames = this.myChapters.GetChapterNames(false);
            this.myChapters.generatedDescriptions = true;
            formChapterNames.fileName = Path.GetFileNameWithoutExtension(this.txtOutputFile.Text);
            formChapterNames.init();
            formChapterNames.SetChapterDisplay();
            int num1 = (int)formChapterNames.ShowDialog();
            if (formChapterNames.applied)
            {
                double num2 = 0.0;
                if (this.aaMode)
                {
                    string[] strArray2 = this.getAudibleTotalTime(strArray1[0]).Split(':');
                    num2 = double.Parse(strArray2[0]) * 60.0 * 60.0 + double.Parse(strArray2[1]) * 60.0 + double.Parse(strArray2[2]);
                }
                else if (!this.cdProcessingMode)
                {
                    string[] strArray2 = this.GetTotalTimeFromAAX(strArray1[0]).Split(':');
                    num2 = double.Parse(strArray2[0]) * 60.0 * 60.0 + double.Parse(strArray2[1]) * 60.0 + double.Parse(strArray2[2]);
                }
                else if (this.cdProcessingMode)
                {
                    VirtualWAV virtualWav = new VirtualWAV();
                    virtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
                    virtualWav.ffprobePath = this.supportLibs.ffprobePath;
                    virtualWav.ConstructCDWAV(this.txtInputFile.Text);
                    num2 = virtualWav.totalSeconds;
                }
                this.myChapters.totalTime = num2;
                if (formChapterNames.chapterNames.Count != this.myChapters.Count())
                    this.myChapters.SetChaptersAndDescriptions(formChapterNames.chapters, formChapterNames.chapterNames);
                else
                    this.myChapters.SetChapterNames(formChapterNames.chapterNames);
                this.myChapters.SetEndChapter();
                this.myChapters.generatedDescriptions = false;
                this.myChapters.includeFileNumbers = formChapterNames.includeChapterNumbers;
                this.myAdvancedOptions.includeChapterNumberInFilename = this.myChapters.includeFileNumbers;
                this.myChapters.customChapterNames = true;
                this.myChapters.customChapters = true;
                this.myChapters.useAsTitle = formChapterNames.chkUseChapterAsTitle.Checked;
                this.myAdvancedOptions.useChapterAsTitle = this.myChapters.useAsTitle;
                this.myChapters.noFileNames = formChapterNames.chkNoTitleInFilename.Checked;
                this.myAdvancedOptions.noTitleInFilename = this.myChapters.noFileNames;
                this.myAdvancedOptions.includeChapterAndTitleInTitleTag = formChapterNames.chkTitleAndNumber.Checked;
                this.myChapters.useTrackAndTitleAsTitle = formChapterNames.chkTitleAndNumber.Checked;
            }
            formChapterNames.Dispose();
            this.SetChapterUI();
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            int num = (int)new FormOverdrive()
            {
                mySupportLibs = this.supportLibs
            }.ShowDialog();
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            if (this.rtbLog.Size.Height == 138)
            {
                this.rtbLog.Size = new Size(this.rtbLog.Size.Width, 40);
                this.Size = new Size(this.Size.Width, this.Size.Height - 98);
                this.btnConvert.Location = new Point(this.btnConvert.Location.X, this.btnConvert.Location.Y - 98);
                this.btnLog.Location = new Point(this.btnLog.Location.X, this.btnLog.Location.Y - 98);
                this.goldProgressBarEx1.Location = new Point(this.goldProgressBarEx1.Location.X, this.goldProgressBarEx1.Location.Y - 98);
            }
            else
            {
                this.rtbLog.Size = new Size(this.rtbLog.Size.Width, 138);
                this.Size = new Size(this.Size.Width, this.Size.Height + 98);
                this.btnConvert.Location = new Point(this.btnConvert.Location.X, this.btnConvert.Location.Y + 98);
                this.btnLog.Location = new Point(this.btnLog.Location.X, this.btnLog.Location.Y + 98);
                this.goldProgressBarEx1.Location = new Point(this.goldProgressBarEx1.Location.X, this.goldProgressBarEx1.Location.Y + 98);
            }
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            this.allowFormResizeToolStripMenuItem.Checked = !this.allowFormResizeToolStripMenuItem.Checked;
            this.myAdvancedOptions.AllowFormResize = this.allowFormResizeToolStripMenuItem.Checked;
            this.SetFormResizeMode(false, this.allowFormResizeToolStripMenuItem.Checked);
        }

        private void SetFormResizeMode(bool onInit, bool enable)
        {
            if (!enable)
            {
                this.AutoSize = true;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
            else
            {
                this.AutoSize = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.AutoSizeMode = AutoSizeMode.GrowOnly;
                this.txtInputFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.btnInputFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.btnSplitter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.btnBatchEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.btnCDrip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.txtOutputFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.btnOutputFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.btnLossless.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.btnOutputOptions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                this.goldProgressBarEx1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                this.btnConvert.Anchor = AnchorStyles.Bottom;
                this.btnLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                if (!onInit)
                {
                    this.Size = new Size(this.Size.Width, 530);
                    this.rtbLog.Size = new Size(this.rtbLog.Size.Width, 0);
                    int height = this.Size.Height;
                    int num = 68;
                    this.btnConvert.Location = new Point(this.btnConvert.Location.X, height - num);
                    this.btnLog.Location = new Point(this.btnLog.Location.X, height - num);
                }
                else
                {
                    Size size = this.Size;
                    try
                    {
                        if (this.myAdvancedOptions.SavedFormSize.Height <= 100 || this.myAdvancedOptions.SavedFormSize.Width <= 100)
                            return;
                        this.Size = this.myAdvancedOptions.SavedFormSize;
                    }
                    catch (Exception ex)
                    {
                        this.Size = size;
                        Audible.diskLogger("Failed to resize form: " + ex.ToString());
                    }
                }
            }
        }

        private void menuItem14_Click_1(object sender, EventArgs e)
        {
            this.btnConvert.PerformClick();
        }

        private void CleanupTmpMP3Wavs()
        {
            try
            {
                string tempPath = this.myAdvancedOptions.GetTempPath();
                string path = tempPath + "\\tmpWAVs";
                foreach (FileSystemInfo enumerateFile in new DirectoryInfo(tempPath).EnumerateFiles("inaudible*.*"))
                    enumerateFile.Delete();
                if (!Directory.Exists(path))
                    return;
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                Audible.diskLogger("Error cleaning up tmp wav files - " + ex.ToString());
            }
        }

        private void MP3Processing(string[] files)
        {
            this.CleanupTmpMP3Wavs();
            try
            {
                this.CheckForRAR(files[0]);
            }
            catch
            {
            }
            FormMP3Import formMp3Import = new FormMP3Import();
            formMp3Import.LoadFile(files[0]);
            int num1 = (int)formMp3Import.ShowDialog();
            FormMP3Import.TITLE_MODE titleMode = formMp3Import.titleMode;
            FormMP3Import.TAG_MODE tagMode = formMp3Import.tagMode;
            string tempPath = this.myAdvancedOptions.GetTempPath();
            List<double> chaps = new List<double>();
            string tmpWavDir = tempPath + "\\tmpWAVs";
            Directory.CreateDirectory(tmpWavDir);
            int num2 = 0;
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            List<string> stringList = new List<string>();
            foreach (string file in files)
                stringList.Add(file);
            IWorkItemResult<int>[] wir = new IWorkItemResult<int>[stringList.Count];
            List<string> wavFiles = new List<string>();
            for (int index = 0; index < stringList.Count; ++index)
            {
                int num3 = num2;
                string str = tmpWavDir + "\\temp-" + num3.ToString("D3") + ".wav";
                wir[index] = smartThreadPool.QueueWorkItem<string, string, int>(new Amib.Threading.Func<string, string, int>(this.ConvertMP3toWAV), stringList[index], str);
                wavFiles.Add(str);
                ++num2;
            }
            int done = 0;
            int prevDone = 0;
            Task.Factory.StartNew((System.Action)(() =>
           {
               this.SetText("Converting MP3's to temporary WAV files...");
               while (done != wir.Length)
               {
                   Thread.Sleep(1000);
                   done = 0;
                   for (int index = 0; index < wir.Length; ++index)
                   {
                       if (wir[index].IsCompleted)
                           ++done;
                   }
                   if (prevDone != done && done > 0)
                   {
                       for (int index = prevDone; index < done; ++index)
                       {
                           this.SetText("FYI-" + (object)(index + 1) + "/" + (object)wir.Length + " - " + Path.GetFileNameWithoutExtension(files[index]));
                           this.UpdateProgressBar((int)((double)done / (double)wir.Length * 100.0), "default");
                       }
                       prevDone = done;
                   }
               }
               smartThreadPool.WaitForIdle();
               smartThreadPool.Shutdown();
           })).ContinueWith((System.Action<Task>)(t =>
     {
               chaps = new VirtualWAV()
               {
                   ffmpegPath = this.supportLibs.ffmpegPath,
                   ffprobePath = this.supportLibs.ffprobePath
               }.ConstructCDWAV(tmpWavDir);
               this.txtInputFile.Text = files[0];
               this.AddWAVFiles(wavFiles.ToArray(), files, titleMode, tagMode);
               if (files.Length == 1)
                   this.myChapters = this.GetChaptersFromMP3(files[0]);
               VirtualWAV vw = new VirtualWAV();
               vw.audioFiles = new SourceAudio[1];
               vw.audioFiles[0] = new SourceAudio(this.supportLibs);
               vw.audioFiles[0].fileName = files[0];
               this.GetOmniMetaData(vw);
               if (titleMode == FormMP3Import.TITLE_MODE.FILE)
                   this.myAudible.title = Path.GetFileNameWithoutExtension(files[0]);
               this.SetLossyMode();
               this.setEncodingUI();
               this.chkVerifyAudibleSplits.Checked = false;
               this.chkRemoveAudibleMarkers.Checked = false;
               this.m4bTranscodeMode = false;
               this.finishedBatch = false;
               VirtualWAV myVirtualWav = new VirtualWAV();
               this.GetSampleRateFromInput(files[0], ref myVirtualWav);
               this.SetText("WARNING-Source is " + (object)myVirtualWav.originalBitrate + " kbits @ " + (object)myVirtualWav.sampleRate + "Hz, " + (object)myVirtualWav.channels + " channels");
               this.myAudible.nfo.sourceSampleRate = myVirtualWav.sampleRate.ToString();
               this.myAudible.nfo.sourceChannels = myVirtualWav.channels.ToString();
               this.myAudible.nfo.sourceBitrate = myVirtualWav.originalBitrate.ToString();
               if (new Overdrive(files[0])
               {
                   ffprobePath = (Path.GetDirectoryName(this.supportLibs.ffmpegPath) + "\\ffprobe.exe")
               }.HasOverdriveMetadata())
               {
                   this.SetText("MAROON-Found Overdrive tag. Parsing chapters...");
                   List<Overdrive> overdriveList = new List<Overdrive>();
                   int num3 = 0;
                   foreach (string file in files)
                   {
                       Overdrive overdrive = new Overdrive(file);
                       overdrive.ffprobePath = Path.GetDirectoryName(this.supportLibs.ffmpegPath) + "\\ffprobe.exe";
                       overdrive.ParseChapters();
                       if (num3 == 0)
                           overdrive.RemoveSubchapters(true);
                       else
                           overdrive.RemoveSubchapters(false);
                       overdriveList.Add(overdrive);
                       if (overdrive.errorText != "")
                           this.SetText("WARNING-" + overdrive.errorText);
                       num3 += overdrive.ReturnChapters().Count();
                   }
                   FormOverdrive formOverdrive = new FormOverdrive();
                   formOverdrive.overdriveFiles = overdriveList;
                   formOverdrive.RemoveRemainingDupes();
                   double num4 = 0.0;
                   this.myChapters.Clear();
                   for (int index = 0; index < formOverdrive.overdriveFiles.Count; ++index)
                   {
                       for (int pos = 0; pos < formOverdrive.overdriveFiles[index].ReturnChapters().Count(); ++pos)
                       {
                           if (Overdrive.mergeText != formOverdrive.overdriveFiles[index].ReturnChapters().GetDescription(pos))
                               this.myChapters.Add(formOverdrive.overdriveFiles[index].ReturnChapters().GetChapterDouble(pos) + num4, formOverdrive.overdriveFiles[index].ReturnChapters().GetDescription(pos));
                       }
                       num4 += formOverdrive.overdriveFiles[index].totalTime;
                   }
                   this.SetText("Found " + (object)this.myChapters.Count() + " Overdrive chapters.");
               }
               this.SetChapterUI();
           }), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void BARDProcessing(string[] files)
        {
            this.CleanupTmpMP3Wavs();
            string tempPath = this.myAdvancedOptions.GetTempPath();
            List<double> chaps = new List<double>();
            string tmpWavDir = tempPath + "\\tmpWAVs";
            Directory.CreateDirectory(tmpWavDir);
            int num1 = 0;
            SmartThreadPool smartThreadPool = new SmartThreadPool(10000, this.threads);
            List<string> stringList = new List<string>();
            foreach (string file in files)
                stringList.Add(file);
            IWorkItemResult<int>[] wir = new IWorkItemResult<int>[stringList.Count];
            List<string> wavFiles = new List<string>();
            for (int index = 0; index < stringList.Count; ++index)
            {
                int num2 = num1;
                string str = tmpWavDir + "\\temp-" + num2.ToString("D3") + ".wav";
                wir[index] = smartThreadPool.QueueWorkItem<string, string, int>(new Amib.Threading.Func<string, string, int>(this.Convert3PGtoWAV), stringList[index], str);
                wavFiles.Add(str);
                ++num1;
            }
            int done = 0;
            int prevDone = 0;
            Task.Factory.StartNew((System.Action)(() =>
           {
               this.SetText("Converting AMR-WB+'s to temporary WAV files. This could take a while...");
               while (done != wir.Length)
               {
                   Thread.Sleep(1000);
                   done = 0;
                   for (int index = 0; index < wir.Length; ++index)
                   {
                       if (wir[index].IsCompleted)
                           ++done;
                   }
                   if (prevDone != done && done > 0)
                   {
                       for (int index = prevDone; index < done; ++index)
                       {
                           this.SetText("FYI-" + (object)(index + 1) + "/" + (object)wir.Length + " - " + Path.GetFileNameWithoutExtension(files[index]));
                           this.UpdateProgressBar((int)((double)done / (double)wir.Length * 100.0), "default");
                       }
                       prevDone = done;
                   }
               }
               smartThreadPool.WaitForIdle();
               smartThreadPool.Shutdown();
           })).ContinueWith((System.Action<Task>)(t =>
     {
               VirtualWAV virtualWav = new VirtualWAV();
               virtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
               virtualWav.ffprobePath = this.supportLibs.ffprobePath;
               chaps = virtualWav.ConstructCDWAV(tmpWavDir);
               this.txtInputFile.Text = files[0];
               this.Add3GPFiles(wavFiles.ToArray(), files);
               virtualWav.audioFiles = new SourceAudio[1];
               virtualWav.audioFiles[0] = new SourceAudio(this.supportLibs);
               virtualWav.audioFiles[0].fileName = files[0];
               this.chkVerifyAudibleSplits.Checked = false;
               this.chkRemoveAudibleMarkers.Checked = false;
               this.myAudible.nfo.sourceFormat = "AMR-WB+";
               virtualWav.originalBitrate = 24;
               this.myAudible.nfo.sourceBitrate = virtualWav.originalBitrate.ToString();
               this.myAudible.nfo.sourceSampleRate = virtualWav.sampleRate.ToString();
               this.myAudible.nfo.sourceChannels = virtualWav.channels.ToString();
               this.SetText("Book: " + this.myAudible.title);
               this.SetText("Author: " + this.myAudible.author);
               this.SetText("Narrator: " + this.myAudible.narrator);
               this.SetText("Year: " + this.myAudible.year);
               this.SetText("Total Input Files: " + (object)virtualWav.GetTotalWavs().Count);
               this.SetText("Total Time = " + this.myAudible.nfo.totalTime);
               this.SetText("WARNING-Source is " + (object)virtualWav.originalBitrate + " kbits @ " + (object)virtualWav.sampleRate + "Hz, " + (object)virtualWav.channels + " channels");
               this.SetText("GREEN-You may begin your conversion, now.");
               this.setAAXoptions(Form1.ProcessingMode.Other, "");
               this.SetChapterUI();
               this.SetLossyMode();
               this.setEncodingUI();
               this.chkVerifyAudibleSplits.Checked = false;
               this.chkRemoveAudibleMarkers.Checked = false;
               this.m4bTranscodeMode = false;
               this.omniMode = false;
               this.finishedBatch = false;
           }), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public AdvancedSplitting.Chapters GetChaptersFromMP3(string file)
        {
            IEnumerable<Frame> frames = ((TagLib.Id3v2.Tag)TagLib.File.Create(file).GetTag(TagTypes.Id3v2)).GetFrames((ByteVector)"TXXX");
            string cueFile = "";
            foreach (Frame frame in frames)
            {
                if (frame.ToString().Contains("[CUESHEET]"))
                {
                    this.SetText("MAROON-Loading embedded chapters...");
                    cueFile = frame.ToString().Replace("[CUESHEET]", "").Trim();
                    cueFile = cueFile.Replace("/", "\n");
                }
            }
            return Utilities.ParseCueFile(cueFile);
        }

        private void CheckForRAR(string fileName)
        {
            byte[] numArray = new byte[512];
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Read(numArray, 0, numArray.Length);
                    fileStream.Close();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
            }
            string mimeType = MimeType.GetMimeType(numArray, fileName);
            Audible.diskLogger("Detected file type = " + mimeType);
            if (!(mimeType == "application/x-rar-compressed"))
                return;
            int num = (int)MessageBox.Show("This MP3 is really a RAR file. I will try to load it, anyway.\r\n\r\nWeird stuff may happen. You have been warned.\r\n\r\nIt is recommended that you open the file with WinRAR,\r\nextract its contents and load them in using MP3 Processing Mode.", "This is really a RAR file.");
        }

        private void cmbBitrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rdCBR.Checked = true;
        }

        private void cmbBitrate_TextChanged(object sender, EventArgs e)
        {
            this.rdCBR.Checked = true;
        }

        private void chkSameAsSource_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkSameAsSource.Checked)
                return;
            this.SetUI2Source(this.txtInputFile.Text.Split(this.fileDeliminator)[0]);
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Audio files (MP3/M4B)|*.mp3;*.mp4;*.m4b;*.m4a|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Array.Sort<string>(openFileDialog.FileNames);
                string str = Path.GetExtension(openFileDialog.FileNames[0]).TrimStart('.');
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = str;
                saveFileDialog.Filter = "Audio files (MP3/M4B)|*.mp3;*.mp4;*.m4b;*.m4a|All files (*.*)|*.*";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FormMP3Joiner formMp3Joiner = new FormMP3Joiner();
                    formMp3Joiner.inputFiles = openFileDialog.FileNames;
                    formMp3Joiner.outputFile = saveFileDialog.FileName;
                    formMp3Joiner.libs = this.supportLibs;
                    int num = (int)formMp3Joiner.ShowDialog();
                    formMp3Joiner.Dispose();
                    this.SetText("EAA-Merged MP3 \"" + saveFileDialog.FileName + "\" created.");
                }
            }
            openFileDialog.Dispose();
        }

        private void LaunchMultiRipper()
        {
            FormLHR formLhr = new FormLHR();
            formLhr.supportLibs = this.supportLibs;
            formLhr.init();
            int num = (int)formLhr.ShowDialog();
            if (formLhr.apply)
            {
                this.txtInputFile.Text = formLhr.txtTargetDir.Text;
                if (formLhr.rdWAV.Checked)
                {
                    this.AddWAVFiles(formLhr.txtTargetDir.Text);
                    this.omniMode = false;
                }
                else
                {
                    this.AddMediaFiles(formLhr.txtTargetDir.Text);
                    this.omniMode = true;
                }
                this.setEncodingUI();
                this.chkVerifyAudibleSplits.Checked = false;
                this.chkRemoveAudibleMarkers.Checked = false;
                this.m4bTranscodeMode = false;
                this.finishedBatch = false;
            }
            formLhr.Dispose();
        }

        private void menuItem20_Click(object sender, EventArgs e)
        {
            this.LaunchMultiRipper();
        }

        private void menuItem21_Click(object sender, EventArgs e)
        {
            this.OmniProcessorDir();
        }

        private void OmniProcessorDir()
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Select source folder";
            if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
                return;
            this.txtInputFile.Text = commonOpenFileDialog.FileName;
            this.AddMediaFiles(commonOpenFileDialog.FileName);
            this.SetLossyMode();
            this.setEncodingUI();
            this.chkVerifyAudibleSplits.Checked = false;
            this.chkRemoveAudibleMarkers.Checked = false;
            this.m4bTranscodeMode = false;
            this.omniMode = true;
            this.finishedBatch = false;
        }

        private void AddMediaFiles(string dir)
        {
            this.grpLame.Enabled = true;
            this.grpSplitting.Enabled = true;
            this.grpOutputType.Enabled = true;
            this.aaxMode = true;
            this.aaxOnce = true;
            this.studioProcessingMode = true;
            this.cdProcessingMode = true;
            string title = "";
            bool fileMode = false;
            string firstGoodFile = "";
            Audible.diskLogger("Media path = " + dir);
            if (!dir.Contains<char>(this.fileDeliminator))
            {
                if (System.IO.File.GetAttributes(dir) == FileAttributes.Directory)
                {
                    title = Path.GetFileName(dir);
                    goto label_5;
                }
            }
            try
            {
                string[] strArray = dir.Split('|');
                title = Path.GetFileNameWithoutExtension(strArray[0]);
                firstGoodFile = strArray[0];
                fileMode = true;
            }
            catch (Exception ex)
            {
                this.SetText("Problem with source dir: " + ex.Message);
            }
            label_5:
            VirtualWAV myVirtualWav = new VirtualWAV();
            myVirtualWav.omni = true;
            myVirtualWav.supportLibs = this.supportLibs;
            myVirtualWav.ffmpegPath = this.supportLibs.ffmpegPath;
            myVirtualWav.ffprobePath = this.supportLibs.ffprobePath;
            Task.Factory.StartNew((System.Action)(() =>
           {
               this.SetText("Performing deep analysis of source files...");
               try
               {
                   this.myChapters.SetDoubleChapters(myVirtualWav.ConstructOmniChapters(dir));
                   if (this.myChapters.Count() != 0)
                       return;
                   this.SetText("Failed to construct Virtual WAV.");
               }
               catch (Exception ex)
               {
                   this.SetText("Something bad happened constructing the virtual WAV: " + ex.ToString());
               }
           })).ContinueWith((System.Action<Task>)(t =>
     {
               this.GetOmniMetaData(myVirtualWav);
               if (this.myAudible.title == null || this.myAudible.title == "")
                   this.myAudible.title = title;
               this.myAudible.totalTime = myVirtualWav.GetFormattedTime();
               this.btnEditChapters.Visible = true;
               this.setAAXoptions(Form1.ProcessingMode.Other, "");
               string str1 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
               string str2 = this.myAudible.title;
               foreach (char ch in str1)
                   str2 = str2.Replace(ch.ToString(), "");
               this.outputFileMask = !fileMode ? Path.GetDirectoryName(dir) + "\\" + str2 + ".mp3" : Path.GetDirectoryName(firstGoodFile) + "\\" + str2 + ".mp3";
               this.txtOutputFile.Text = this.outputFileMask;
               this.saveFileDialog1.FileName = str2 + ".mp3";
               this.inputFileName = this.txtInputFile.Text;
               this.outputFileName = this.txtOutputFile.Text;
               this.SetText("Book: " + this.myAudible.title);
               this.SetText("Total " + myVirtualWav.inputFileType + " files = " + (object)myVirtualWav.audioFiles.Length);
               this.SetText("Sample Rate = " + (object)myVirtualWav.sampleRate + " Hz");
               this.myAudible.nfo.sourceSampleRate = myVirtualWav.sampleRate.ToString();
               this.SetText("Channels = " + (object)myVirtualWav.channels);
               this.myAudible.nfo.sourceChannels = myVirtualWav.channels.ToString();
               this.myAudible.nfo.totalTime = this.getAudibleTotalTime(dir);
               this.SetText("Total Time = " + this.myAudible.nfo.totalTime);
               this.myAudible.nfo.sourceFormat = myVirtualWav.inputFileType;
               this.myAudible.nfo.sourceBitrate = myVirtualWav.originalBitrate.ToString();
               if (myVirtualWav.totalSeconds == 0.0)
                   this.SetText("WARNING-Could not find any media files in this directory.");
               this.omniWAV = myVirtualWav;
               this.SetChapterUI();
           }), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Factory.StartNew((System.Action)(() =>
           {
               bool[] flagArray = (bool[])null;
               label_9:
               while (myVirtualWav.myJobProgress == null || !myVirtualWav.myJobProgress.completed)
               {
                   Thread.Sleep(200);
                   if (myVirtualWav.myJobProgress != null)
                   {
                       if (flagArray == null)
                       {
                           flagArray = new bool[myVirtualWav.myJobProgress.totalFiles];
                       }
                       else
                       {
                           int index = 0;
                           while (true)
                           {
                               if (index < myVirtualWav.myJobProgress.completedItems.Length && !myVirtualWav.myJobProgress.completed)
                               {
                                   if (myVirtualWav.myJobProgress.completedItems[index] && !flagArray[index])
                                   {
                                       flagArray[index] = true;
                                       this.SetText("FYI-" + Directory.GetParent(myVirtualWav.myJobProgress.filesNames[index]).Name + "\\" + Path.GetFileName(myVirtualWav.myJobProgress.filesNames[index]));
                                       this.UpdateProgressBar((int)((double)myVirtualWav.myJobProgress.totalCompleted / (double)myVirtualWav.myJobProgress.totalFiles * 100.0), "default");
                                   }
                                   ++index;
                               }
                               else
                                   goto label_9;
                           }
                       }
                   }
               }
               this.UpdateProgressBar(0, "default");
           })).ContinueWith((System.Action<Task>)(t => { }), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void GetOmniMetaData(VirtualWAV vw)
        {
            TagLib.File file = TagLib.File.Create(vw.audioFiles[0].fileName);
            this.myAudible.title = file.Tag.Title;
            try
            {
                this.myAudible.author = file.Tag.Performers[0];
            }
            catch
            {
            }
            this.myAudible.album = file.Tag.Album;
            this.myAudible.year = file.Tag.Year.ToString();
            this.myAudible.SetComments(file.Tag.Comment);
            try
            {
                this.myAudible.narrator = file.Tag.Composers[0];
            }
            catch
            {
            }
            bool gotCover = false;
            Task.Factory.StartNew((System.Action)(() => gotCover = this.GetCoverFromAnything(vw.audioFiles[0].fileName))).ContinueWith((System.Action<Task>)(t =>
          {
              try
              {
                  if (gotCover)
                  {
                      string filename = Form1.thumbnailDir + "\\" + this.myCoverArt.GetImage(Form1.thumbnailDir);
                      Image image = (Image)new Bitmap(filename);
                      this.pbCover.Image = image.GetThumbnailImage(89, 80, (Image.GetThumbnailImageAbort)null, new IntPtr());
                      image.Dispose();
                      this.myAudible.coverPath = filename;
                      this.pbCover.Visible = true;
                      this.hasCoverArt = true;
                      this.chkEmbedCover.Visible = true;
                      this.chkSaveCover.Visible = true;
                  }
                  else
                  {
                      this.bgWorker1.DoWork += new DoWorkEventHandler(this.myBackgroundWorker_DoWork);
                      this.bgWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.myBackgroundWorker_RunWorkerCompleted);
                      string[] files = Directory.GetFiles(Path.GetDirectoryName(vw.audioFiles[0].fileName), "*.jpg");
                      if (files.Length <= 0)
                          return;
                      this.SetText("MAROON-No cover art metadata found; using \"" + Path.GetFileName(files[0]) + "\" as cover.");
                      this.pbCover.Image = Image.FromFile(files[0]);
                      this.myAudible.coverPath = files[0];
                      this.hasCoverArt = true;
                  }
              }
              catch
              {
              }
          }), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void menuItem22_Click(object sender, EventArgs e)
        {
            this.OmniProcessorFiles();
        }

        private void OmniProcessorFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            this.OmniProcess(openFileDialog.FileNames);
        }

        private void OmniProcess(string[] files)
        {
            this.txtInputFile.Text = string.Join(this.fileDeliminator.ToString(), files);
            this.CheckForRAR(files[0]);
            this.AddMediaFiles(this.txtInputFile.Text);
            this.SetLossyMode();
            this.setEncodingUI();
            this.chkVerifyAudibleSplits.Checked = false;
            this.chkRemoveAudibleMarkers.Checked = false;
            this.m4bTranscodeMode = false;
            this.omniMode = true;
            this.finishedBatch = false;
        }

        private void menuItem23_Click(object sender, EventArgs e)
        {
            int num = (int)new FormTagEditor().ShowDialog();
        }

        private void cmbCodec_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setEncodingUI();
        }

        private void chkAdvancedCodecs_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterCodecs(this.chkAdvancedCodecs.Checked);
        }

        private void chapterMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnChapterMetadata.PerformClick();
        }

        private void wAVFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadWAVs();
        }

        private void cUEToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.newRipper = true;
            }
        }

        private void cDDA2WAVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.newRipper = false;
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.myAdvancedOptions.lowQualityPreview = ((ToolStripMenuItem)sender).Checked;
        }

        private void tempFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.myAdvancedOptions.chapterEditorTempFile = ((ToolStripMenuItem)sender).Checked;
        }

        private void mP3SPLTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.SplitMode = AdvancedOptions.SplitTypes.MP3SPLT;
            }
        }

        private void ffmpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.SplitMode = AdvancedOptions.SplitTypes.ffmpeg;
            }
        }

        private void directshowFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.aaDirectShow = ((ToolStripMenuItem)sender).Checked;
            }
        }

        private void audibleManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.aaDirectShow = false;
            }
        }

        private void inAudibleNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.aaDirectShow = false;
            }
        }

        private void iTunesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.iTunesMode = true;
            }
        }

        private void audibleManagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.decrypt = true;
            }
        }

        private void inAudibleNGToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection)((sender as ToolStripMenuItem).OwnerItem as ToolStripMenuItem).DropDownItems)
            {
                dropDownItem.Checked = dropDownItem == sender as ToolStripMenuItem;
                this.myAdvancedOptions.decrypt = true;
                this.myAdvancedOptions.ng = true;
            }
        }

        private void beepOnJobCompletionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.myAdvancedOptions.cylon = ((ToolStripMenuItem)sender).Checked;
        }

        private void createNFOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.myAdvancedOptions.nfo = ((ToolStripMenuItem)sender).Checked;
        }

        private void wizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LaunchWizard(false);
        }

        private void LaunchWizard(bool firstTime)
        {
            FormWizard formWizard = new FormWizard();
            formWizard.myWizardOptions.FirstTime = firstTime;
            formWizard.Init();
            int num = (int)formWizard.ShowDialog();
            WizardOptions wizardOptions = formWizard.myWizardOptions;
            if (wizardOptions.Applied)
                this.SetWizardOptions(wizardOptions);
            formWizard.Dispose();
        }

        private void SetWizardOptions(WizardOptions myWizardOptions)
        {
            this.LoadAudibleFiles(new string[1]
            {
        myWizardOptions.InputFile
            });
            this.txtOutputFile.Text = myWizardOptions.OutputFile;
            this.chkAudibleSplit.Checked = myWizardOptions.Split;
            this.chkCUEfile.Checked = myWizardOptions.CUE;
            this.chkChangeFileNumbering.Checked = myWizardOptions.ChaptersFirst;
            this.cmbSampleRate.Text = myWizardOptions.SampleRate;
            this.cmbBitrate.Text = myWizardOptions.Bitrate;
            if (myWizardOptions.Codec == "MP3" && this.aaxMode)
                this.SetSelectedCodec("lame");
            else if (this.aaxMode)
                this.SetSelectedCodec("lossless");
            else if (myWizardOptions.Codec == "MP3")
                this.SetSelectedCodec("lossless");
            else
                this.SetSelectedCodec("nero");
            if (myWizardOptions.Channels == "Auto")
                this.chkAutodetectChannels.Checked = true;
            else if (myWizardOptions.Channels == "Stereo")
            {
                this.chkAutodetectChannels.Checked = false;
                this.chkMono.Checked = false;
            }
            else
            {
                this.chkAutodetectChannels.Checked = false;
                this.chkMono.Checked = true;
            }
            if (!myWizardOptions.Start)
                return;
            this.btnConvert.PerformClick();
        }

        private void factoryResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This option will delete your current settings and decryption keys.  Are you sure that you want to do this?", "Factory Reset", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            this.FactoryReset();
        }

        private void FactoryReset()
        {
            try
            {
                System.IO.File.Delete(this.iniPath);
                Process.Start(AudibleConvertor.GLOBALS.ExecutablePath);
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                Audible.diskLogger("Couldn't restart: " + ex.ToString());
            }
        }

        private void btnBatchEdit_Click(object sender, EventArgs e)
        {
            this.myBatchFiles.outputPath = this.txtOutputFile.Text;
            this.myBatchFiles.myAudibles = this.myAudibles;
            this.myBatchFiles.ValidateBatch(this.chkAuthor.Checked, this.chkBookTitle.Checked, this.chkAuthorTitle.Checked, this.chkAddExension.Checked, this.GetSelectedCodecObject().Extension, this.chkStripUnabridged.Checked);
            this.myBatchFiles.AlreadyEdited = true;
            this.myBatchFiles.HasErrors = !this.ShowBadPaths(this.myBatchFiles);
        }

        private void audioEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormMain().Show();
        }

        private void inAudibleDownloadManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.myAdvancedOptions.OptionsLoaded && this.CurrentPrefs("AAX"))
            {
                this.firstRun = false;
                this.LoadPrefs("AAX");
            }
            FormDownloadManager formDownloadManager = new FormDownloadManager();
            formDownloadManager.txtDownoadDir.Text = this.myAdvancedOptions.DownloadPath;
            int num = (int)formDownloadManager.ShowDialog();
            if (!formDownloadManager.applied || !(formDownloadManager.txtDownoadDir.Text != ""))
                return;
            this.myAdvancedOptions.DownloadPath = formDownloadManager.downloadPath;
            this.myAdvancedOptions.okayToSaveSettings = true;
        }

        private void selectFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All supported formats|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            this.MP3Processing(openFileDialog.FileNames);
        }

        private void recursiveDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Select source folder";
            if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
                return;
            string fileName = commonOpenFileDialog.FileName;
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>((IEnumerable<string>)((IEnumerable<string>)Directory.GetDirectories(fileName)).OrderBy<string, string>((System.Func<string, string>)(f => new DirectoryInfo(f).Name)).ToArray<string>());
            for (int index = stringList2.Count - 1; index > -1; --index)
            {
                if (Directory.GetFiles(stringList2[index]).Length == 0 && Directory.GetDirectories(stringList2[index]).Length == 0)
                    stringList2.RemoveAt(index);
            }
            string[] strArray = stringList2.ToArray();
            if (strArray.Length == 0)
                strArray = new string[1] { fileName };
            foreach (string path1 in strArray)
            {
                Audible.diskLogger("dir = " + path1);
                IOrderedEnumerable<string> source = ((IEnumerable<string>)Directory.GetFiles(path1, "*.mp3")).OrderBy<string, string>((System.Func<string, string>)(f => new DirectoryInfo(f).Name));
                if (source.Count<string>() != 0)
                {
                    foreach (string path2 in (IEnumerable<string>)source)
                    {
                        if (Path.GetExtension(path2) == ".mp3")
                            stringList1.Add(path2);
                    }
                }
            }
            this.MP3Processing(stringList1.ToArray());
        }

        private void scrapeAudiblecomForAddedMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.myAdvancedOptions.AudibleScraping = ((ToolStripMenuItem)sender).Checked;
        }

        private void addAppleTagsToM4BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.myAdvancedOptions.AppleTags = ((ToolStripMenuItem)sender).Checked;
        }

        private void removeTinyVerySmallChaptersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.myAdvancedOptions.RemoveTinyChapters = ((ToolStripMenuItem)sender).Checked;
        }

        private void chkStripUnabridged_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkStripUnabridged.Checked)
                return;
            try
            {
                this.myAudible = this.StripUnabridged(this.myAudible);
                this.txtOutputFile.Text = Path.GetDirectoryName(this.txtOutputFile.Text) + "\\" + this.myAudible.GetASCIITag(this.myAudible.title) + Path.GetExtension(this.txtOutputFile.Text);
            }
            catch
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
            this.btnConvert = new Button();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.btnInputFile = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnOutputFile = new Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.rtbLog = new RichTextBoxEx();
            this.grpLame = new GroupBox();
            this.chkSaveCover = new CheckBox();
            this.chkDRC = new CheckBox();
            this.chkNormalize = new CheckBox();
            this.chkKeepMonolithicMP3 = new CheckBox();
            this.chkEmbedCover = new CheckBox();
            this.chkDoNotTag = new CheckBox();
            this.chkM4Bsplit = new CheckBox();
            this.chkCUEfile = new CheckBox();
            this.chkRemoveAudibleMarkers = new CheckBox();
            this.chkMultithread = new CheckBox();
            this.chkSameAsSource = new CheckBox();
            this.rdCBR = new RadioButton();
            this.cmbBitrate = new ComboBox();
            this.lblVBRquality = new Label();
            this.rdVBR = new RadioButton();
            this.tbVBRquality = new TrackBar();
            this.goldProgressBarEx1 = new ProgressBarEx();
            this.goldPlainBackgroundPainter1 = new PlainBackgroundPainter();
            this.goldGradientGlossPainter1 = new GradientGlossPainter();
            this.goldPlainBorderPainter1 = new PlainBorderPainter();
            this.goldPlainProgressPainter1 = new PlainProgressPainter();
            this.goldMiddleGlossPainter1 = new MiddleGlossPainter();
            this.goldRoundGlossPainter1 = new RoundGlossPainter();
            this.goldGradientGlossPainter2 = new GradientGlossPainter();
            this.lblDownloadStatus = new Label();
            this.chkMono = new CheckBox();
            this.grpOutputType = new GroupBox();
            this.cmbSampleRate = new ComboBox();
            this.chkAdvancedCodecs = new CheckBox();
            this.cmbCodec = new ComboBox();
            this.pbCover = new PictureBox();
            this.chkAutodetectChannels = new CheckBox();
            this.lblSampleRate = new Label();
            this.chkFileSplitting = new CheckBox();
            this.label3 = new Label();
            this.txtSplitThreshold = new System.Windows.Forms.TextBox();
            this.label4 = new Label();
            this.chkAudibleSplit = new CheckBox();
            this.grpSplitting = new GroupBox();
            this.chkMP3chapterTags = new CheckBox();
            this.chkEmbedM4BChapters = new CheckBox();
            this.txtDurationToSplit = new System.Windows.Forms.TextBox();
            this.chkSplitByDuration = new CheckBox();
            this.btnEditChapters = new Button();
            this.label9 = new Label();
            this.txtChapterThreshold = new System.Windows.Forms.TextBox();
            this.chkChapterThreshold = new CheckBox();
            this.chkVerifyAudibleSplits = new CheckBox();
            this.btnLog = new Button();
            this.btnOutputOptions = new Button();
            this.grpOutputOptions = new GroupBox();
            this.chkStripUnabridged = new CheckBox();
            this.btnChapterMetadata = new Button();
            this.chkAddExension = new CheckBox();
            this.btnMetadata = new Button();
            this.chkChangeFileNumbering = new CheckBox();
            this.chkAuthorTitle = new CheckBox();
            this.chkBookTitle = new CheckBox();
            this.chkAuthor = new CheckBox();
            this.grpITunes = new GroupBox();
            this.btnAdvancedOptions = new Button();
            this.chkRerun = new CheckBox();
            this.btnVCDSettings = new Button();
            this.txtItunesIdleCountdown = new System.Windows.Forms.TextBox();
            this.lblIdleCountdown = new Label();
            this.rdITunesNone = new RadioButton();
            this.txtITunesPassSize = new System.Windows.Forms.TextBox();
            this.label8 = new Label();
            this.rdITunesManual = new RadioButton();
            this.rdITunesDesperation = new RadioButton();
            this.rdITunesDefault = new RadioButton();
            this.lblResizer = new Label();
            this.btnCDrip = new Button();
            this.btnSplitter = new Button();
            this.btnLossless = new Button();
            this.menuStrip1 = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.openAudibleFilesToolStripMenuItem = new ToolStripMenuItem();
            this.audibleM4BToolStripMenuItem = new ToolStripMenuItem();
            this.mP3ProcessingToolStripMenuItem = new ToolStripMenuItem();
            this.selectFilesToolStripMenuItem = new ToolStripMenuItem();
            this.recursiveDirectoriesToolStripMenuItem = new ToolStripMenuItem();
            this.anyAudioFileToolStripMenuItem = new ToolStripMenuItem();
            this.directoryToolStripMenuItem = new ToolStripMenuItem();
            this.wAVFilesToolStripMenuItem = new ToolStripMenuItem();
            this.beginConversionToolStripMenuItem = new ToolStripMenuItem();
            this.wizardToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.quitToolStripMenuItem = new ToolStripMenuItem();
            this.chapterToolsToolStripMenuItem = new ToolStripMenuItem();
            this.advancedSplitterToolStripMenuItem = new ToolStripMenuItem();
            this.overdriveChapterizerToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.chapterEditorToolStripMenuItem = new ToolStripMenuItem();
            this.chapterMetadataToolStripMenuItem = new ToolStripMenuItem();
            this.cDRippingToolStripMenuItem = new ToolStripMenuItem();
            this.simpleRipperToolStripMenuItem = new ToolStripMenuItem();
            this.multiDriveRipperToolStripMenuItem = new ToolStripMenuItem();
            this.miscToolsToolStripMenuItem = new ToolStripMenuItem();
            this.tagEditorToolStripMenuItem = new ToolStripMenuItem();
            this.renamingToolToolStripMenuItem = new ToolStripMenuItem();
            this.mP3M4BJoinerToolStripMenuItem = new ToolStripMenuItem();
            this.audioEditorToolStripMenuItem = new ToolStripMenuItem();
            this.settingsToolStripMenuItem = new ToolStripMenuItem();
            this.advancedSettingsToolStripMenuItem = new ToolStripMenuItem();
            this.splitterToolStripMenuItem = new ToolStripMenuItem();
            this.previewToolStripMenuItem = new ToolStripMenuItem();
            this.tempFileToolStripMenuItem = new ToolStripMenuItem();
            this.libraryToolStripMenuItem = new ToolStripMenuItem();
            this.mP3SPLTToolStripMenuItem = new ToolStripMenuItem();
            this.ffmpegToolStripMenuItem = new ToolStripMenuItem();
            this.aARipperToolStripMenuItem = new ToolStripMenuItem();
            this.directshowFilterToolStripMenuItem = new ToolStripMenuItem();
            this.audibleManagerToolStripMenuItem = new ToolStripMenuItem();
            this.inAudibleNGToolStripMenuItem = new ToolStripMenuItem();
            this.aAXRipperToolStripMenuItem = new ToolStripMenuItem();
            this.iTunesToolStripMenuItem = new ToolStripMenuItem();
            this.audibleManagerToolStripMenuItem1 = new ToolStripMenuItem();
            this.inAudibleNGToolStripMenuItem1 = new ToolStripMenuItem();
            this.cDRipperToolStripMenuItem = new ToolStripMenuItem();
            this.cUEToolsToolStripMenuItem = new ToolStripMenuItem();
            this.cDDA2WAVToolStripMenuItem = new ToolStripMenuItem();
            this.otherToolStripMenuItem = new ToolStripMenuItem();
            this.beepOnJobCompletionToolStripMenuItem = new ToolStripMenuItem();
            this.createNFOToolStripMenuItem = new ToolStripMenuItem();
            this.scrapeAudiblecomForAddedMetadataToolStripMenuItem = new ToolStripMenuItem();
            this.internalToolStripMenuItem = new ToolStripMenuItem();
            this.addAppleTagsToM4BToolStripMenuItem = new ToolStripMenuItem();
            this.removeTinyVerySmallChaptersToolStripMenuItem = new ToolStripMenuItem();
            this.inAudibleDownloadManagerToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.factoryResetToolStripMenuItem = new ToolStripMenuItem();
            this.allowFormResizeToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.btnBatchEdit = new Button();
            this.grpDownloader = new GroupBox();
            this.lblDlCodec = new Label();
            this.lblDlProductId = new Label();
            this.lblDlTitle = new Label();
            this.grpLame.SuspendLayout();
            this.tbVBRquality.BeginInit();
            this.grpOutputType.SuspendLayout();
            ((ISupportInitialize)this.pbCover).BeginInit();
            this.grpSplitting.SuspendLayout();
            this.grpOutputOptions.SuspendLayout();
            this.grpITunes.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpDownloader.SuspendLayout();
            this.SuspendLayout();
            this.btnConvert.AccessibleDescription = "Starts the conversion process";
            this.btnConvert.AccessibleName = "Begin conversion";
            this.btnConvert.Image = (Image)componentResourceManager.GetObject("btnConvert.Image");
            this.btnConvert.ImageAlign = ContentAlignment.MiddleRight;
            this.btnConvert.Location = new Point(171, 608);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(130, 23);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Begin Conversion";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new EventHandler(this.button1_Click);
            this.txtInputFile.AccessibleDescription = "Source file to convert.  Usually Audible AA or AAX";
            this.txtInputFile.AccessibleName = "Source file";
            this.txtInputFile.Location = new Point(83, 30);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new Size(207, 20);
            this.txtInputFile.TabIndex = 1;
            this.btnInputFile.AccessibleDescription = "Load a file";
            this.btnInputFile.AccessibleName = "Load file button";
            this.btnInputFile.Location = new Point(296, 27);
            this.btnInputFile.Name = "btnInputFile";
            this.btnInputFile.Size = new Size(27, 23);
            this.btnInputFile.TabIndex = 2;
            this.btnInputFile.Text = "...";
            this.btnInputFile.UseVisualStyleBackColor = true;
            this.btnInputFile.Click += new EventHandler(this.btnInputFile_Click);
            this.btnInputFile.MouseDown += new MouseEventHandler(this.btnInputFile_MouseDown);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(4, 33);
            this.label1.Name = "label1";
            this.label1.Size = new Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Audible File(s):";
            this.label1.MouseDoubleClick += new MouseEventHandler(this.label1_MouseDoubleClick);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(18, 59);
            this.label2.Name = "label2";
            this.label2.Size = new Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Output File:";
            this.btnOutputFile.AccessibleDescription = "Select target location";
            this.btnOutputFile.AccessibleName = "Save file";
            this.btnOutputFile.Location = new Point(266, 54);
            this.btnOutputFile.Name = "btnOutputFile";
            this.btnOutputFile.Size = new Size(27, 23);
            this.btnOutputFile.TabIndex = 5;
            this.btnOutputFile.Text = "...";
            this.btnOutputFile.UseVisualStyleBackColor = true;
            this.btnOutputFile.Click += new EventHandler(this.btnOutputFile_Click);
            this.txtOutputFile.AccessibleDescription = "Name and location of file or files to create";
            this.txtOutputFile.AccessibleName = "Output file";
            this.txtOutputFile.Location = new Point(83, 56);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new Size(177, 20);
            this.txtOutputFile.TabIndex = 4;
            this.rtbLog.Location = new Point(12, 430);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new Size(449, 138);
            this.rtbLog.TabIndex = 7;
            this.rtbLog.Text = "";
            this.rtbLog.LinkClicked += new LinkClickedEventHandler(this.rtbLog_LinkClicked);
            this.grpLame.Controls.Add((Control)this.chkSaveCover);
            this.grpLame.Controls.Add((Control)this.chkDRC);
            this.grpLame.Controls.Add((Control)this.chkNormalize);
            this.grpLame.Controls.Add((Control)this.chkKeepMonolithicMP3);
            this.grpLame.Controls.Add((Control)this.chkEmbedCover);
            this.grpLame.Controls.Add((Control)this.chkDoNotTag);
            this.grpLame.Controls.Add((Control)this.chkM4Bsplit);
            this.grpLame.Controls.Add((Control)this.chkCUEfile);
            this.grpLame.Controls.Add((Control)this.chkRemoveAudibleMarkers);
            this.grpLame.Location = new Point(12, 221);
            this.grpLame.Name = "grpLame";
            this.grpLame.Size = new Size(449, 83);
            this.grpLame.TabIndex = 8;
            this.grpLame.TabStop = false;
            this.grpLame.Text = "Special Options";
            this.chkSaveCover.AutoSize = true;
            this.chkSaveCover.Checked = true;
            this.chkSaveCover.CheckState = CheckState.Checked;
            this.chkSaveCover.Location = new Point(317, 60);
            this.chkSaveCover.Name = "chkSaveCover";
            this.chkSaveCover.Size = new Size(126, 17);
            this.chkSaveCover.TabIndex = 23;
            this.chkSaveCover.Text = "Save cover art as file";
            this.chkSaveCover.UseVisualStyleBackColor = true;
            this.chkSaveCover.Visible = false;
            this.chkDRC.AccessibleDescription = "";
            this.chkDRC.AccessibleName = "Increase the volume for quiet segments";
            this.chkDRC.AutoSize = true;
            this.chkDRC.Location = new Point(170, 40);
            this.chkDRC.Name = "chkDRC";
            this.chkDRC.Size = new Size(105, 17);
            this.chkDRC.TabIndex = 22;
            this.chkDRC.Text = "DR Compression";
            this.chkDRC.UseVisualStyleBackColor = true;
            this.chkNormalize.AccessibleDescription = "";
            this.chkNormalize.AccessibleName = "Increase peak volume to 0 db";
            this.chkNormalize.AutoSize = true;
            this.chkNormalize.Location = new Point(171, 19);
            this.chkNormalize.Name = "chkNormalize";
            this.chkNormalize.Size = new Size(72, 17);
            this.chkNormalize.TabIndex = 21;
            this.chkNormalize.Text = "Normalize";
            this.chkNormalize.UseVisualStyleBackColor = true;
            this.chkKeepMonolithicMP3.AutoSize = true;
            this.chkKeepMonolithicMP3.Location = new Point(10, 60);
            this.chkKeepMonolithicMP3.Name = "chkKeepMonolithicMP3";
            this.chkKeepMonolithicMP3.Size = new Size(121, 17);
            this.chkKeepMonolithicMP3.TabIndex = 8;
            this.chkKeepMonolithicMP3.Text = "Keep Monolithic File";
            this.chkKeepMonolithicMP3.UseVisualStyleBackColor = true;
            this.chkKeepMonolithicMP3.Visible = false;
            this.chkEmbedCover.AccessibleDescription = "Choose whether or not embed the picture into the Mp3 or m4b";
            this.chkEmbedCover.AccessibleName = "Embed cover art";
            this.chkEmbedCover.AutoSize = true;
            this.chkEmbedCover.Checked = true;
            this.chkEmbedCover.CheckState = CheckState.Checked;
            this.chkEmbedCover.Location = new Point(317, 40);
            this.chkEmbedCover.Name = "chkEmbedCover";
            this.chkEmbedCover.Size = new Size(90, 17);
            this.chkEmbedCover.TabIndex = 6;
            this.chkEmbedCover.Text = "Embed Cover";
            this.chkEmbedCover.UseVisualStyleBackColor = true;
            this.chkEmbedCover.Visible = false;
            this.chkDoNotTag.AccessibleDescription = "Do not add metadata tags";
            this.chkDoNotTag.AccessibleName = "Disable tagging";
            this.chkDoNotTag.AutoSize = true;
            this.chkDoNotTag.Location = new Point(10, 39);
            this.chkDoNotTag.Name = "chkDoNotTag";
            this.chkDoNotTag.Size = new Size(76, 17);
            this.chkDoNotTag.TabIndex = 14;
            this.chkDoNotTag.Text = "Do not tag";
            this.chkDoNotTag.UseVisualStyleBackColor = true;
            this.chkM4Bsplit.AutoSize = true;
            this.chkM4Bsplit.Location = new Point(170, 60);
            this.chkM4Bsplit.Name = "chkM4Bsplit";
            this.chkM4Bsplit.Size = new Size(81, 17);
            this.chkM4Bsplit.TabIndex = 11;
            this.chkM4Bsplit.Text = "iOS splitting";
            this.chkM4Bsplit.UseVisualStyleBackColor = true;
            this.chkM4Bsplit.Visible = false;
            this.chkCUEfile.AccessibleDescription = "Create a CUE file which contains the chapter information";
            this.chkCUEfile.AccessibleName = "Create CUE file";
            this.chkCUEfile.AutoSize = true;
            this.chkCUEfile.Checked = true;
            this.chkCUEfile.CheckState = CheckState.Checked;
            this.chkCUEfile.Location = new Point(317, 19);
            this.chkCUEfile.Name = "chkCUEfile";
            this.chkCUEfile.Size = new Size(98, 17);
            this.chkCUEfile.TabIndex = 20;
            this.chkCUEfile.Text = "Create CUE file";
            this.chkCUEfile.UseVisualStyleBackColor = true;
            this.chkRemoveAudibleMarkers.AccessibleDescription = "Remove \"this is audible\" from the beginning of the file";
            this.chkRemoveAudibleMarkers.AccessibleName = "Remove Audible Clips";
            this.chkRemoveAudibleMarkers.AutoSize = true;
            this.chkRemoveAudibleMarkers.Location = new Point(10, 19);
            this.chkRemoveAudibleMarkers.Name = "chkRemoveAudibleMarkers";
            this.chkRemoveAudibleMarkers.Size = new Size(138, 17);
            this.chkRemoveAudibleMarkers.TabIndex = 5;
            this.chkRemoveAudibleMarkers.Text = "Remove \"Audible\" clips";
            this.chkRemoveAudibleMarkers.UseVisualStyleBackColor = true;
            this.chkMultithread.AccessibleDescription = "Turn multithreading on or off";
            this.chkMultithread.AccessibleName = "Toggle multithreading";
            this.chkMultithread.AutoSize = true;
            this.chkMultithread.Checked = true;
            this.chkMultithread.CheckState = CheckState.Checked;
            this.chkMultithread.Location = new Point(347, 111);
            this.chkMultithread.Name = "chkMultithread";
            this.chkMultithread.Size = new Size(92, 17);
            this.chkMultithread.TabIndex = 7;
            this.chkMultithread.Text = "Multithreading";
            this.chkMultithread.UseVisualStyleBackColor = true;
            this.chkSameAsSource.AccessibleDescription = "Check this to automatically select the best settings for this file";
            this.chkSameAsSource.AccessibleName = "Use same settings as source";
            this.chkSameAsSource.AutoSize = true;
            this.chkSameAsSource.Checked = true;
            this.chkSameAsSource.CheckState = CheckState.Checked;
            this.chkSameAsSource.Location = new Point(8, 70);
            this.chkSameAsSource.Name = "chkSameAsSource";
            this.chkSameAsSource.Size = new Size(161, 17);
            this.chkSameAsSource.TabIndex = 19;
            this.chkSameAsSource.Text = "Use same settings as source";
            this.chkSameAsSource.UseVisualStyleBackColor = true;
            this.chkSameAsSource.CheckedChanged += new EventHandler(this.chkSameAsSource_CheckedChanged);
            this.rdCBR.AccessibleDescription = "Encode with Constant Bit Rate";
            this.rdCBR.AccessibleName = "CBR mode";
            this.rdCBR.AutoSize = true;
            this.rdCBR.Checked = true;
            this.rdCBR.Location = new Point(9, 45);
            this.rdCBR.Name = "rdCBR";
            this.rdCBR.Size = new Size(82, 17);
            this.rdCBR.TabIndex = 18;
            this.rdCBR.TabStop = true;
            this.rdCBR.Text = "CBR bitrate:";
            this.rdCBR.UseVisualStyleBackColor = true;
            this.cmbBitrate.AccessibleDescription = "Select target bitrate";
            this.cmbBitrate.AccessibleName = "Target bitrate";
            this.cmbBitrate.FormattingEnabled = true;
            this.cmbBitrate.Items.AddRange(new object[5]
            {
        (object) "32",
        (object) "48",
        (object) "64",
        (object) "96",
        (object) "128"
            });
            this.cmbBitrate.Location = new Point(94, 43);
            this.cmbBitrate.Name = "cmbBitrate";
            this.cmbBitrate.Size = new Size(64, 21);
            this.cmbBitrate.TabIndex = 17;
            this.cmbBitrate.SelectedIndexChanged += new EventHandler(this.cmbBitrate_SelectedIndexChanged);
            this.cmbBitrate.TextChanged += new EventHandler(this.cmbBitrate_TextChanged);
            this.lblVBRquality.AccessibleDescription = "Displays variable bitrate quality level";
            this.lblVBRquality.AccessibleName = "VBR quality level";
            this.lblVBRquality.AutoSize = true;
            this.lblVBRquality.Location = new Point(254, 46);
            this.lblVBRquality.Name = "lblVBRquality";
            this.lblVBRquality.Size = new Size(27, 13);
            this.lblVBRquality.TabIndex = 12;
            this.lblVBRquality.Text = "N/A";
            this.rdVBR.AccessibleDescription = "Encode using Variable Bit Rate";
            this.rdVBR.AccessibleName = "VBR Mode";
            this.rdVBR.AutoSize = true;
            this.rdVBR.Location = new Point(176, 44);
            this.rdVBR.Name = "rdVBR";
            this.rdVBR.Size = new Size(80, 17);
            this.rdVBR.TabIndex = 11;
            this.rdVBR.Text = "VBR Mode:";
            this.rdVBR.UseVisualStyleBackColor = true;
            this.rdVBR.CheckedChanged += new EventHandler(this.rdVBR_CheckedChanged);
            this.tbVBRquality.AccessibleDescription = "Adjusts variable bit rate quality level";
            this.tbVBRquality.AccessibleName = "VBR quality level";
            this.tbVBRquality.Location = new Point(171, 64);
            this.tbVBRquality.Maximum = 9;
            this.tbVBRquality.Name = "tbVBRquality";
            this.tbVBRquality.Size = new Size(104, 45);
            this.tbVBRquality.TabIndex = 10;
            this.tbVBRquality.Value = 5;
            this.tbVBRquality.Scroll += new EventHandler(this.tbVBRquality_Scroll);
            this.goldProgressBarEx1.BackgroundPainter = (IProgressBackgroundPainter)this.goldPlainBackgroundPainter1;
            this.goldProgressBarEx1.BorderPainter = (IProgressBorderPainter)this.goldPlainBorderPainter1;
            this.goldProgressBarEx1.ForeColor = SystemColors.ControlText;
            this.goldProgressBarEx1.Location = new Point(12, 574);
            this.goldProgressBarEx1.MarqueePercentage = 25;
            this.goldProgressBarEx1.MarqueeSpeed = 30;
            this.goldProgressBarEx1.MarqueeStep = 1;
            this.goldProgressBarEx1.Maximum = 100;
            this.goldProgressBarEx1.Minimum = 0;
            this.goldProgressBarEx1.Name = "goldProgressBarEx1";
            this.goldProgressBarEx1.ProgressPadding = 0;
            this.goldProgressBarEx1.ProgressPainter = (IProgressPainter)this.goldPlainProgressPainter1;
            this.goldProgressBarEx1.ProgressType = ProgressType.Smooth;
            this.goldProgressBarEx1.ShowPercentage = true;
            this.goldProgressBarEx1.Size = new Size(447, 28);
            this.goldProgressBarEx1.TabIndex = 22;
            this.goldProgressBarEx1.Value = 0;
            this.goldPlainBackgroundPainter1.Color = Color.Gainsboro;
            this.goldPlainBackgroundPainter1.GlossPainter = (IGlossPainter)this.goldGradientGlossPainter1;
            this.goldGradientGlossPainter1.AlphaHigh = 240;
            this.goldGradientGlossPainter1.AlphaLow = 64;
            this.goldGradientGlossPainter1.Angle = 90f;
            this.goldGradientGlossPainter1.Color = Color.White;
            this.goldGradientGlossPainter1.PercentageCovered = 45;
            this.goldGradientGlossPainter1.Style = GlossStyle.Top;
            this.goldGradientGlossPainter1.Successor = (IGlossPainter)null;
            this.goldPlainBorderPainter1.Color = Color.DarkRed;
            this.goldPlainBorderPainter1.RoundedCorners = true;
            this.goldPlainBorderPainter1.Style = PlainBorderPainter.PlainBorderStyle.Flat;
            this.goldPlainProgressPainter1.Color = Color.Red;
            this.goldPlainProgressPainter1.GlossPainter = (IGlossPainter)this.goldMiddleGlossPainter1;
            this.goldPlainProgressPainter1.LeadingEdge = Color.Transparent;
            this.goldPlainProgressPainter1.ProgressBorderPainter = (IProgressBorderPainter)this.goldPlainBorderPainter1;
            this.goldMiddleGlossPainter1.AlphaHigh = 64;
            this.goldMiddleGlossPainter1.AlphaLow = 0;
            this.goldMiddleGlossPainter1.Color = Color.White;
            this.goldMiddleGlossPainter1.Style = GlossStyle.Both;
            this.goldMiddleGlossPainter1.Successor = (IGlossPainter)this.goldRoundGlossPainter1;
            this.goldMiddleGlossPainter1.TaperHeight = 10;
            this.goldRoundGlossPainter1.AlphaHigh = 240;
            this.goldRoundGlossPainter1.AlphaLow = 0;
            this.goldRoundGlossPainter1.Color = Color.Maroon;
            this.goldRoundGlossPainter1.Style = GlossStyle.Bottom;
            this.goldRoundGlossPainter1.Successor = (IGlossPainter)this.goldGradientGlossPainter2;
            this.goldRoundGlossPainter1.TaperHeight = 6;
            this.goldGradientGlossPainter2.AlphaHigh = 192;
            this.goldGradientGlossPainter2.AlphaLow = 0;
            this.goldGradientGlossPainter2.Angle = 90f;
            this.goldGradientGlossPainter2.Color = Color.White;
            this.goldGradientGlossPainter2.PercentageCovered = 45;
            this.goldGradientGlossPainter2.Style = GlossStyle.Top;
            this.goldGradientGlossPainter2.Successor = (IGlossPainter)null;
            this.lblDownloadStatus.Dock = DockStyle.Bottom;
            this.lblDownloadStatus.Location = new Point(3, 67);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new Size(194, 13);
            this.lblDownloadStatus.TabIndex = 16;
            this.lblDownloadStatus.Text = "0,000,000";
            this.lblDownloadStatus.TextAlign = ContentAlignment.MiddleRight;
            this.chkMono.AccessibleDescription = "Encode output as mono";
            this.chkMono.AccessibleName = "Mono";
            this.chkMono.AutoSize = true;
            this.chkMono.Enabled = false;
            this.chkMono.Location = new Point(162, 95);
            this.chkMono.Name = "chkMono";
            this.chkMono.Size = new Size(53, 17);
            this.chkMono.TabIndex = 9;
            this.chkMono.Text = "Mono";
            this.chkMono.UseVisualStyleBackColor = true;
            this.grpOutputType.Controls.Add((Control)this.cmbSampleRate);
            this.grpOutputType.Controls.Add((Control)this.chkAdvancedCodecs);
            this.grpOutputType.Controls.Add((Control)this.cmbCodec);
            this.grpOutputType.Controls.Add((Control)this.chkSameAsSource);
            this.grpOutputType.Controls.Add((Control)this.pbCover);
            this.grpOutputType.Controls.Add((Control)this.rdCBR);
            this.grpOutputType.Controls.Add((Control)this.chkAutodetectChannels);
            this.grpOutputType.Controls.Add((Control)this.chkMultithread);
            this.grpOutputType.Controls.Add((Control)this.cmbBitrate);
            this.grpOutputType.Controls.Add((Control)this.lblSampleRate);
            this.grpOutputType.Controls.Add((Control)this.rdVBR);
            this.grpOutputType.Controls.Add((Control)this.lblVBRquality);
            this.grpOutputType.Controls.Add((Control)this.chkMono);
            this.grpOutputType.Controls.Add((Control)this.tbVBRquality);
            this.grpOutputType.Location = new Point(12, 84);
            this.grpOutputType.Name = "grpOutputType";
            this.grpOutputType.Size = new Size(449, 131);
            this.grpOutputType.TabIndex = 10;
            this.grpOutputType.TabStop = false;
            this.grpOutputType.Text = "Output Type";
            this.cmbSampleRate.AccessibleDescription = "Select target bitrate";
            this.cmbSampleRate.AccessibleName = "Target bitrate";
            this.cmbSampleRate.FormattingEnabled = true;
            this.cmbSampleRate.Items.AddRange(new object[3]
            {
        (object) "22050",
        (object) "44100",
        (object) "48000"
            });
            this.cmbSampleRate.Location = new Point(84, 93);
            this.cmbSampleRate.Name = "cmbSampleRate";
            this.cmbSampleRate.Size = new Size(64, 21);
            this.cmbSampleRate.TabIndex = 20;
            this.cmbSampleRate.Text = "22050";
            this.chkAdvancedCodecs.AutoSize = true;
            this.chkAdvancedCodecs.Location = new Point(266, 20);
            this.chkAdvancedCodecs.Name = "chkAdvancedCodecs";
            this.chkAdvancedCodecs.Size = new Size(75, 17);
            this.chkAdvancedCodecs.TabIndex = 13;
            this.chkAdvancedCodecs.Text = "All codecs";
            this.chkAdvancedCodecs.UseVisualStyleBackColor = true;
            this.chkAdvancedCodecs.CheckedChanged += new EventHandler(this.chkAdvancedCodecs_CheckedChanged);
            this.cmbCodec.AccessibleName = "Output Codec";
            this.cmbCodec.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCodec.FormattingEnabled = true;
            this.cmbCodec.Location = new Point(10, 18);
            this.cmbCodec.Name = "cmbCodec";
            this.cmbCodec.Size = new Size(245, 21);
            this.cmbCodec.TabIndex = 12;
            this.cmbCodec.SelectedIndexChanged += new EventHandler(this.cmbCodec_SelectedIndexChanged);
            this.pbCover.AccessibleDescription = "Cover art extracted from the original file is displayed here";
            this.pbCover.AccessibleName = "Cover art";
            this.pbCover.BorderStyle = BorderStyle.FixedSingle;
            this.pbCover.Image = (Image)componentResourceManager.GetObject("pbCover.Image");
            this.pbCover.Location = new Point(347, 12);
            this.pbCover.Name = "pbCover";
            this.pbCover.Size = new Size(96, 96);
            this.pbCover.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbCover.TabIndex = 3;
            this.pbCover.TabStop = false;
            this.pbCover.Tag = (object)"";
            this.pbCover.Visible = false;
            this.pbCover.Click += new EventHandler(this.pbCover_Click);
            this.chkAutodetectChannels.AccessibleDescription = "Detect whether or not the source is stereo and set the output to match";
            this.chkAutodetectChannels.AccessibleName = "Auto detect stereo";
            this.chkAutodetectChannels.AutoSize = true;
            this.chkAutodetectChannels.Checked = true;
            this.chkAutodetectChannels.CheckState = CheckState.Checked;
            this.chkAutodetectChannels.Location = new Point(162, 111);
            this.chkAutodetectChannels.Name = "chkAutodetectChannels";
            this.chkAutodetectChannels.Size = new Size(115, 17);
            this.chkAutodetectChannels.TabIndex = 13;
            this.chkAutodetectChannels.Text = "Auto detect Stereo";
            this.chkAutodetectChannels.UseVisualStyleBackColor = true;
            this.chkAutodetectChannels.CheckedChanged += new EventHandler(this.chkAutodetectChannels_CheckedChanged);
            this.lblSampleRate.AutoSize = true;
            this.lblSampleRate.Location = new Point(7, 96);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new Size(71, 13);
            this.lblSampleRate.TabIndex = 12;
            this.lblSampleRate.Text = "Sample Rate:";
            this.chkFileSplitting.AccessibleDescription = "Split output file by silence";
            this.chkFileSplitting.AccessibleName = "Split by silence";
            this.chkFileSplitting.AutoSize = true;
            this.chkFileSplitting.Location = new Point(7, 18);
            this.chkFileSplitting.Name = "chkFileSplitting";
            this.chkFileSplitting.Size = new Size(96, 17);
            this.chkFileSplitting.TabIndex = 0;
            this.chkFileSplitting.Text = "Split by silence";
            this.chkFileSplitting.UseVisualStyleBackColor = true;
            this.chkFileSplitting.CheckedChanged += new EventHandler(this.chkFileSplitting_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 42);
            this.label3.Name = "label3";
            this.label3.Size = new Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Threshold:";
            this.txtSplitThreshold.AccessibleDescription = "Number of seconds of silence required to declare a chapter break";
            this.txtSplitThreshold.AccessibleName = "Silence threshold";
            this.txtSplitThreshold.Location = new Point(67, 39);
            this.txtSplitThreshold.Name = "txtSplitThreshold";
            this.txtSplitThreshold.Size = new Size(40, 20);
            this.txtSplitThreshold.TabIndex = 2;
            this.txtSplitThreshold.Text = "3.25";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(108, 42);
            this.label4.Name = "label4";
            this.label4.Size = new Size(162, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "(consecutive seconds of silence)";
            this.chkAudibleSplit.AccessibleDescription = "Split output into chapters";
            this.chkAudibleSplit.AccessibleName = "Split output into chapters";
            this.chkAudibleSplit.AutoSize = true;
            this.chkAudibleSplit.Location = new Point(284, 15);
            this.chkAudibleSplit.Name = "chkAudibleSplit";
            this.chkAudibleSplit.Size = new Size(141, 17);
            this.chkAudibleSplit.TabIndex = 4;
            this.chkAudibleSplit.Text = "Split by Chapter Markers";
            this.chkAudibleSplit.UseVisualStyleBackColor = true;
            this.chkAudibleSplit.CheckedChanged += new EventHandler(this.chkAudibleSplit_CheckedChanged);
            this.grpSplitting.Controls.Add((Control)this.chkMP3chapterTags);
            this.grpSplitting.Controls.Add((Control)this.chkEmbedM4BChapters);
            this.grpSplitting.Controls.Add((Control)this.txtDurationToSplit);
            this.grpSplitting.Controls.Add((Control)this.chkSplitByDuration);
            this.grpSplitting.Controls.Add((Control)this.btnEditChapters);
            this.grpSplitting.Controls.Add((Control)this.label9);
            this.grpSplitting.Controls.Add((Control)this.txtChapterThreshold);
            this.grpSplitting.Controls.Add((Control)this.chkChapterThreshold);
            this.grpSplitting.Controls.Add((Control)this.chkVerifyAudibleSplits);
            this.grpSplitting.Controls.Add((Control)this.chkAudibleSplit);
            this.grpSplitting.Controls.Add((Control)this.label4);
            this.grpSplitting.Controls.Add((Control)this.txtSplitThreshold);
            this.grpSplitting.Controls.Add((Control)this.label3);
            this.grpSplitting.Controls.Add((Control)this.chkFileSplitting);
            this.grpSplitting.Location = new Point(12, 310);
            this.grpSplitting.Name = "grpSplitting";
            this.grpSplitting.Size = new Size(449, 114);
            this.grpSplitting.TabIndex = 9;
            this.grpSplitting.TabStop = false;
            this.grpSplitting.Text = "Chapter Options";
            this.chkMP3chapterTags.AutoSize = true;
            this.chkMP3chapterTags.Checked = true;
            this.chkMP3chapterTags.CheckState = CheckState.Checked;
            this.chkMP3chapterTags.Location = new Point(284, 81);
            this.chkMP3chapterTags.Name = "chkMP3chapterTags";
            this.chkMP3chapterTags.Size = new Size(128, 17);
            this.chkMP3chapterTags.TabIndex = 23;
            this.chkMP3chapterTags.Text = "Embed MP3 chapters";
            this.chkMP3chapterTags.UseVisualStyleBackColor = true;
            this.chkMP3chapterTags.Visible = false;
            this.chkEmbedM4BChapters.AutoSize = true;
            this.chkEmbedM4BChapters.Checked = true;
            this.chkEmbedM4BChapters.CheckState = CheckState.Checked;
            this.chkEmbedM4BChapters.Location = new Point(284, 58);
            this.chkEmbedM4BChapters.Name = "chkEmbedM4BChapters";
            this.chkEmbedM4BChapters.Size = new Size(129, 17);
            this.chkEmbedM4BChapters.TabIndex = 22;
            this.chkEmbedM4BChapters.Text = "Embed M4B Chapters";
            this.chkEmbedM4BChapters.UseVisualStyleBackColor = true;
            this.chkEmbedM4BChapters.Visible = false;
            this.txtDurationToSplit.AccessibleDescription = "Number of minutes to split at";
            this.txtDurationToSplit.AccessibleName = "Split duration";
            this.txtDurationToSplit.Location = new Point(247, 16);
            this.txtDurationToSplit.Name = "txtDurationToSplit";
            this.txtDurationToSplit.Size = new Size(20, 20);
            this.txtDurationToSplit.TabIndex = 21;
            this.txtDurationToSplit.Text = "30";
            this.chkSplitByDuration.AccessibleDescription = "Split output into chunks of fixed duration in minutes.  Splits will be aligned to nearest silence";
            this.chkSplitByDuration.AccessibleName = "Fixed duration splits";
            this.chkSplitByDuration.AutoSize = true;
            this.chkSplitByDuration.Location = new Point(109, 18);
            this.chkSplitByDuration.Name = "chkSplitByDuration";
            this.chkSplitByDuration.Size = new Size(146, 17);
            this.chkSplitByDuration.TabIndex = 20;
            this.chkSplitByDuration.Text = "Fixed duration splits (min):";
            this.chkSplitByDuration.UseVisualStyleBackColor = true;
            this.chkSplitByDuration.CheckedChanged += new EventHandler(this.chkSplitByDuration_CheckedChanged);
            this.btnEditChapters.AccessibleDescription = "Adjust chapter markers for this file";
            this.btnEditChapters.AccessibleName = "Adjust chapters";
            this.btnEditChapters.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnEditChapters.Location = new Point(6, 86);
            this.btnEditChapters.Name = "btnEditChapters";
            this.btnEditChapters.Size = new Size(139, 23);
            this.btnEditChapters.TabIndex = 19;
            this.btnEditChapters.Text = "Adjust Chapters";
            this.btnEditChapters.UseVisualStyleBackColor = true;
            this.btnEditChapters.Click += new EventHandler(this.btnEditChapters_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(140, 65);
            this.label9.Name = "label9";
            this.label9.Size = new Size(29, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "secs";
            this.txtChapterThreshold.AccessibleDescription = "Chapter markers less than this value will be removed";
            this.txtChapterThreshold.AccessibleName = "Chapter threshold value";
            this.txtChapterThreshold.Location = new Point(110, 62);
            this.txtChapterThreshold.Name = "txtChapterThreshold";
            this.txtChapterThreshold.Size = new Size(30, 20);
            this.txtChapterThreshold.TabIndex = 16;
            this.txtChapterThreshold.Text = "45";
            this.txtChapterThreshold.TextAlign = HorizontalAlignment.Right;
            this.chkChapterThreshold.AccessibleDescription = "Remove chapter markers that are less than the specified number of seconds";
            this.chkChapterThreshold.AccessibleName = "Chapter threshold";
            this.chkChapterThreshold.AutoSize = true;
            this.chkChapterThreshold.Location = new Point(6, 64);
            this.chkChapterThreshold.Name = "chkChapterThreshold";
            this.chkChapterThreshold.Size = new Size(109, 17);
            this.chkChapterThreshold.TabIndex = 15;
            this.chkChapterThreshold.Text = "Chapter threshold";
            this.chkChapterThreshold.UseVisualStyleBackColor = true;
            this.chkVerifyAudibleSplits.AccessibleDescription = "Verify that split points fall during silence";
            this.chkVerifyAudibleSplits.AccessibleName = "Verify Audible Splits";
            this.chkVerifyAudibleSplits.AutoSize = true;
            this.chkVerifyAudibleSplits.Location = new Point(284, 36);
            this.chkVerifyAudibleSplits.Name = "chkVerifyAudibleSplits";
            this.chkVerifyAudibleSplits.Size = new Size(118, 17);
            this.chkVerifyAudibleSplits.TabIndex = 14;
            this.chkVerifyAudibleSplits.Text = "Verify Audible Splits";
            this.chkVerifyAudibleSplits.UseVisualStyleBackColor = true;
            this.btnLog.AccessibleDescription = "Display the debug log files";
            this.btnLog.AccessibleName = "Show logs";
            this.btnLog.Location = new Point(419, 608);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new Size(42, 23);
            this.btnLog.TabIndex = 12;
            this.btnLog.Text = "Logs";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new EventHandler(this.btnLog_Click);
            this.btnOutputOptions.AccessibleDescription = "Display (or hide) additional options";
            this.btnOutputOptions.AccessibleName = "Other options";
            this.btnOutputOptions.Location = new Point(371, 56);
            this.btnOutputOptions.Name = "btnOutputOptions";
            this.btnOutputOptions.Size = new Size(94, 23);
            this.btnOutputOptions.TabIndex = 14;
            this.btnOutputOptions.Text = "Other Options";
            this.btnOutputOptions.UseVisualStyleBackColor = true;
            this.btnOutputOptions.Click += new EventHandler(this.btnOutputOptions_Click);
            this.grpOutputOptions.Controls.Add((Control)this.chkStripUnabridged);
            this.grpOutputOptions.Controls.Add((Control)this.btnChapterMetadata);
            this.grpOutputOptions.Controls.Add((Control)this.chkAddExension);
            this.grpOutputOptions.Controls.Add((Control)this.btnMetadata);
            this.grpOutputOptions.Controls.Add((Control)this.chkChangeFileNumbering);
            this.grpOutputOptions.Controls.Add((Control)this.chkAuthorTitle);
            this.grpOutputOptions.Controls.Add((Control)this.chkBookTitle);
            this.grpOutputOptions.Controls.Add((Control)this.chkAuthor);
            this.grpOutputOptions.Location = new Point(477, 84);
            this.grpOutputOptions.Name = "grpOutputOptions";
            this.grpOutputOptions.Size = new Size(208, 139);
            this.grpOutputOptions.TabIndex = 15;
            this.grpOutputOptions.TabStop = false;
            this.grpOutputOptions.Text = "Output Options / Metadata";
            this.chkStripUnabridged.AutoSize = true;
            this.chkStripUnabridged.Location = new Point(5, 89);
            this.chkStripUnabridged.Name = "chkStripUnabridged";
            this.chkStripUnabridged.Size = new Size(121, 17);
            this.chkStripUnabridged.TabIndex = 12;
            this.chkStripUnabridged.Text = "Strip \"(Unabridged)\"";
            this.chkStripUnabridged.UseVisualStyleBackColor = true;
            this.chkStripUnabridged.CheckedChanged += new EventHandler(this.chkStripUnabridged_CheckedChanged);
            this.btnChapterMetadata.AccessibleName = "Edit chapter metadata";
            this.btnChapterMetadata.Enabled = false;
            this.btnChapterMetadata.Location = new Point(103, 108);
            this.btnChapterMetadata.Name = "btnChapterMetadata";
            this.btnChapterMetadata.Size = new Size(102, 23);
            this.btnChapterMetadata.TabIndex = 10;
            this.btnChapterMetadata.Text = "Chapter Metadata";
            this.btnChapterMetadata.UseVisualStyleBackColor = true;
            this.btnChapterMetadata.Click += new EventHandler(this.btnChapterMetadata_Click);
            this.chkAddExension.AutoSize = true;
            this.chkAddExension.Location = new Point(91, 43);
            this.chkAddExension.Name = "chkAddExension";
            this.chkAddExension.Size = new Size(93, 17);
            this.chkAddExension.TabIndex = 11;
            this.chkAddExension.Text = "Add extension";
            this.chkAddExension.UseVisualStyleBackColor = true;
            this.btnMetadata.AccessibleDescription = "Edit metadata fields";
            this.btnMetadata.AccessibleName = "Edit Metadata";
            this.btnMetadata.Location = new Point(5, 108);
            this.btnMetadata.Name = "btnMetadata";
            this.btnMetadata.Size = new Size(88, 23);
            this.btnMetadata.TabIndex = 9;
            this.btnMetadata.Text = "Edit Metadata";
            this.btnMetadata.UseVisualStyleBackColor = true;
            this.btnMetadata.Click += new EventHandler(this.btnMetadata_Click);
            this.chkChangeFileNumbering.AccessibleDescription = "Put file number at the beginning of the file name";
            this.chkChangeFileNumbering.AccessibleName = "Prepend file number";
            this.chkChangeFileNumbering.AutoSize = true;
            this.chkChangeFileNumbering.Location = new Point(6, 66);
            this.chkChangeFileNumbering.Name = "chkChangeFileNumbering";
            this.chkChangeFileNumbering.Size = new Size(176, 17);
            this.chkChangeFileNumbering.TabIndex = 6;
            this.chkChangeFileNumbering.Text = "Prepend file number (01 - name)";
            this.chkChangeFileNumbering.UseVisualStyleBackColor = true;
            this.chkAuthorTitle.AccessibleDescription = "Create folder with author and title  name";
            this.chkAuthorTitle.AccessibleName = "Author and title folder";
            this.chkAuthorTitle.AutoSize = true;
            this.chkAuthorTitle.Checked = true;
            this.chkAuthorTitle.CheckState = CheckState.Checked;
            this.chkAuthorTitle.Location = new Point(6, 43);
            this.chkAuthorTitle.Name = "chkAuthorTitle";
            this.chkAuthorTitle.Size = new Size(86, 17);
            this.chkAuthorTitle.TabIndex = 4;
            this.chkAuthorTitle.Text = "Author - Title";
            this.chkAuthorTitle.UseVisualStyleBackColor = true;
            this.chkBookTitle.AccessibleDescription = "Create folder with book's name";
            this.chkBookTitle.AccessibleName = "Title folder";
            this.chkBookTitle.AutoSize = true;
            this.chkBookTitle.Location = new Point(91, 20);
            this.chkBookTitle.Name = "chkBookTitle";
            this.chkBookTitle.Size = new Size(86, 17);
            this.chkBookTitle.TabIndex = 2;
            this.chkBookTitle.Text = "Book Title ->";
            this.chkBookTitle.UseVisualStyleBackColor = true;
            this.chkAuthor.AccessibleDescription = "Create folder with author's name";
            this.chkAuthor.AccessibleName = "Author folder";
            this.chkAuthor.AutoSize = true;
            this.chkAuthor.Location = new Point(6, 22);
            this.chkAuthor.Name = "chkAuthor";
            this.chkAuthor.Size = new Size(69, 17);
            this.chkAuthor.TabIndex = 0;
            this.chkAuthor.Text = "Author ->";
            this.chkAuthor.UseVisualStyleBackColor = true;
            this.grpITunes.Controls.Add((Control)this.btnAdvancedOptions);
            this.grpITunes.Controls.Add((Control)this.chkRerun);
            this.grpITunes.Controls.Add((Control)this.btnVCDSettings);
            this.grpITunes.Controls.Add((Control)this.txtItunesIdleCountdown);
            this.grpITunes.Controls.Add((Control)this.lblIdleCountdown);
            this.grpITunes.Controls.Add((Control)this.rdITunesNone);
            this.grpITunes.Controls.Add((Control)this.txtITunesPassSize);
            this.grpITunes.Controls.Add((Control)this.label8);
            this.grpITunes.Controls.Add((Control)this.rdITunesManual);
            this.grpITunes.Controls.Add((Control)this.rdITunesDesperation);
            this.grpITunes.Controls.Add((Control)this.rdITunesDefault);
            this.grpITunes.Location = new Point(477, 230);
            this.grpITunes.Name = "grpITunes";
            this.grpITunes.Size = new Size(210, 194);
            this.grpITunes.TabIndex = 16;
            this.grpITunes.TabStop = false;
            this.grpITunes.Text = "iTunes Automation";
            this.btnAdvancedOptions.AccessibleDescription = "Edit advanced settings";
            this.btnAdvancedOptions.AccessibleName = "Advanced settings";
            this.btnAdvancedOptions.Location = new Point(129, 144);
            this.btnAdvancedOptions.Name = "btnAdvancedOptions";
            this.btnAdvancedOptions.Size = new Size(75, 23);
            this.btnAdvancedOptions.TabIndex = 22;
            this.btnAdvancedOptions.Text = "Advanced";
            this.btnAdvancedOptions.UseVisualStyleBackColor = true;
            this.btnAdvancedOptions.Click += new EventHandler(this.btnAdvancedOptions_Click);
            this.chkRerun.AutoSize = true;
            this.chkRerun.Location = new Point(6, 172);
            this.chkRerun.Name = "chkRerun";
            this.chkRerun.Size = new Size(179, 17);
            this.chkRerun.TabIndex = 21;
            this.chkRerun.Text = "Allow re-run and do not clean up";
            this.chkRerun.UseVisualStyleBackColor = true;
            this.btnVCDSettings.Location = new Point(6, 143);
            this.btnVCDSettings.Name = "btnVCDSettings";
            this.btnVCDSettings.Size = new Size(117, 23);
            this.btnVCDSettings.TabIndex = 20;
            this.btnVCDSettings.Text = "Virtual CD Settings";
            this.btnVCDSettings.UseVisualStyleBackColor = true;
            this.btnVCDSettings.Click += new EventHandler(this.btnVCDSettings_Click);
            this.txtItunesIdleCountdown.Location = new Point(100, 116);
            this.txtItunesIdleCountdown.Name = "txtItunesIdleCountdown";
            this.txtItunesIdleCountdown.Size = new Size(30, 20);
            this.txtItunesIdleCountdown.TabIndex = 7;
            this.txtItunesIdleCountdown.Text = "10";
            this.lblIdleCountdown.AutoSize = true;
            this.lblIdleCountdown.Location = new Point(10, 119);
            this.lblIdleCountdown.Name = "lblIdleCountdown";
            this.lblIdleCountdown.Size = new Size(84, 13);
            this.lblIdleCountdown.TabIndex = 6;
            this.lblIdleCountdown.Text = "Idle Countdown:";
            this.rdITunesNone.AutoSize = true;
            this.rdITunesNone.Location = new Point(6, 19);
            this.rdITunesNone.Name = "rdITunesNone";
            this.rdITunesNone.Size = new Size(51, 17);
            this.rdITunesNone.TabIndex = 5;
            this.rdITunesNone.Text = "None";
            this.rdITunesNone.UseVisualStyleBackColor = true;
            this.txtITunesPassSize.Location = new Point(100, 96);
            this.txtITunesPassSize.Name = "txtITunesPassSize";
            this.txtITunesPassSize.Size = new Size(30, 20);
            this.txtITunesPassSize.TabIndex = 4;
            this.txtITunesPassSize.Text = "10";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(3, 99);
            this.label8.Name = "label8";
            this.label8.Size = new Size(91, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Pass Size (hours):";
            this.rdITunesManual.AutoSize = true;
            this.rdITunesManual.Location = new Point(6, 65);
            this.rdITunesManual.Name = "rdITunesManual";
            this.rdITunesManual.Size = new Size(90, 17);
            this.rdITunesManual.TabIndex = 2;
            this.rdITunesManual.Text = "Manual Mode";
            this.rdITunesManual.UseVisualStyleBackColor = true;
            this.rdITunesDesperation.AutoSize = true;
            this.rdITunesDesperation.Location = new Point(92, 19);
            this.rdITunesDesperation.Name = "rdITunesDesperation";
            this.rdITunesDesperation.Size = new Size(112, 17);
            this.rdITunesDesperation.TabIndex = 1;
            this.rdITunesDesperation.Text = "Desperation Mode";
            this.rdITunesDesperation.UseVisualStyleBackColor = true;
            this.rdITunesDefault.AutoSize = true;
            this.rdITunesDefault.Checked = true;
            this.rdITunesDefault.Location = new Point(6, 42);
            this.rdITunesDefault.Name = "rdITunesDefault";
            this.rdITunesDefault.Size = new Size(89, 17);
            this.rdITunesDefault.TabIndex = 0;
            this.rdITunesDefault.TabStop = true;
            this.rdITunesDefault.Text = "Default Mode";
            this.rdITunesDefault.UseVisualStyleBackColor = true;
            this.lblResizer.AutoSize = true;
            this.lblResizer.Location = new Point(659, 624);
            this.lblResizer.Name = "lblResizer";
            this.lblResizer.Size = new Size(35, 13);
            this.lblResizer.TabIndex = 17;
            this.lblResizer.Text = "label9";
            this.lblResizer.Visible = false;
            this.btnCDrip.AccessibleDescription = "Rip CD's to WAV files";
            this.btnCDrip.AccessibleName = "Launch CD ripper";
            this.btnCDrip.Location = new Point(413, 27);
            this.btnCDrip.Name = "btnCDrip";
            this.btnCDrip.Size = new Size(52, 23);
            this.btnCDrip.TabIndex = 20;
            this.btnCDrip.Text = "Rip CD";
            this.btnCDrip.UseVisualStyleBackColor = true;
            this.btnCDrip.Click += new EventHandler(this.btnCDrip_Click);
            this.btnSplitter.AccessibleDescription = "Split files use the advanced splitting tool";
            this.btnSplitter.AccessibleName = "Advanced splitter";
            this.btnSplitter.BackgroundImage = (Image)componentResourceManager.GetObject("btnSplitter.BackgroundImage");
            this.btnSplitter.BackgroundImageLayout = ImageLayout.Stretch;
            this.btnSplitter.Location = new Point(382, 27);
            this.btnSplitter.Name = "btnSplitter";
            this.btnSplitter.Size = new Size(25, 23);
            this.btnSplitter.TabIndex = 21;
            this.btnSplitter.UseVisualStyleBackColor = true;
            this.btnSplitter.Click += new EventHandler(this.btnSplitter_Click);
            this.btnLossless.AccessibleDescription = "Displays whether a conversion will be lossy or lossless";
            this.btnLossless.AccessibleName = "Quality indicator";
            this.btnLossless.Enabled = false;
            this.btnLossless.Location = new Point(299, 56);
            this.btnLossless.Name = "btnLossless";
            this.btnLossless.Size = new Size(66, 23);
            this.btnLossless.TabIndex = 12;
            this.btnLossless.Text = "Quality";
            this.btnLossless.UseVisualStyleBackColor = true;
            this.menuStrip1.Items.AddRange(new ToolStripItem[5]
            {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.chapterToolsToolStripMenuItem,
        (ToolStripItem) this.cDRippingToolStripMenuItem,
        (ToolStripItem) this.miscToolsToolStripMenuItem,
        (ToolStripItem) this.settingsToolStripMenuItem
            });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(694, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
            {
        (ToolStripItem) this.openAudibleFilesToolStripMenuItem,
        (ToolStripItem) this.beginConversionToolStripMenuItem,
        (ToolStripItem) this.wizardToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.quitToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.openAudibleFilesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
            {
        (ToolStripItem) this.audibleM4BToolStripMenuItem,
        (ToolStripItem) this.mP3ProcessingToolStripMenuItem,
        (ToolStripItem) this.anyAudioFileToolStripMenuItem,
        (ToolStripItem) this.directoryToolStripMenuItem,
        (ToolStripItem) this.wAVFilesToolStripMenuItem
            });
            this.openAudibleFilesToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("openAudibleFilesToolStripMenuItem.Image");
            this.openAudibleFilesToolStripMenuItem.Name = "openAudibleFilesToolStripMenuItem";
            this.openAudibleFilesToolStripMenuItem.Size = new Size(167, 22);
            this.openAudibleFilesToolStripMenuItem.Text = "Open";
            this.audibleM4BToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("audibleM4BToolStripMenuItem.Image");
            this.audibleM4BToolStripMenuItem.Name = "audibleM4BToolStripMenuItem";
            this.audibleM4BToolStripMenuItem.ShortcutKeys = Keys.O | Keys.Control;
            this.audibleM4BToolStripMenuItem.Size = new Size(273, 22);
            this.audibleM4BToolStripMenuItem.Text = "Audible / M4B";
            this.audibleM4BToolStripMenuItem.Click += new EventHandler(this.menuItem2_Click);
            this.mP3ProcessingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.selectFilesToolStripMenuItem,
        (ToolStripItem) this.recursiveDirectoriesToolStripMenuItem
            });
            this.mP3ProcessingToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("mP3ProcessingToolStripMenuItem.Image");
            this.mP3ProcessingToolStripMenuItem.Name = "mP3ProcessingToolStripMenuItem";
            this.mP3ProcessingToolStripMenuItem.Size = new Size(273, 22);
            this.mP3ProcessingToolStripMenuItem.Text = "MP3 Processing";
            this.selectFilesToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("selectFilesToolStripMenuItem.Image");
            this.selectFilesToolStripMenuItem.Name = "selectFilesToolStripMenuItem";
            this.selectFilesToolStripMenuItem.ShortcutKeys = Keys.M | Keys.Control;
            this.selectFilesToolStripMenuItem.Size = new Size(259, 22);
            this.selectFilesToolStripMenuItem.Text = "Select Files";
            this.selectFilesToolStripMenuItem.Click += new EventHandler(this.selectFilesToolStripMenuItem_Click);
            this.recursiveDirectoriesToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("recursiveDirectoriesToolStripMenuItem.Image");
            this.recursiveDirectoriesToolStripMenuItem.Name = "recursiveDirectoriesToolStripMenuItem";
            this.recursiveDirectoriesToolStripMenuItem.ShortcutKeys = Keys.M | Keys.Shift | Keys.Control;
            this.recursiveDirectoriesToolStripMenuItem.Size = new Size(259, 22);
            this.recursiveDirectoriesToolStripMenuItem.Text = "Recursive directories";
            this.recursiveDirectoriesToolStripMenuItem.Click += new EventHandler(this.recursiveDirectoriesToolStripMenuItem_Click);
            this.anyAudioFileToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("anyAudioFileToolStripMenuItem.Image");
            this.anyAudioFileToolStripMenuItem.Name = "anyAudioFileToolStripMenuItem";
            this.anyAudioFileToolStripMenuItem.ShortcutKeys = Keys.O | Keys.Shift | Keys.Control;
            this.anyAudioFileToolStripMenuItem.Size = new Size(273, 22);
            this.anyAudioFileToolStripMenuItem.Text = "Other non-Audible files";
            this.anyAudioFileToolStripMenuItem.Click += new EventHandler(this.menuItem22_Click);
            this.directoryToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("directoryToolStripMenuItem.Image");
            this.directoryToolStripMenuItem.Name = "directoryToolStripMenuItem";
            this.directoryToolStripMenuItem.ShortcutKeys = Keys.D | Keys.Control;
            this.directoryToolStripMenuItem.Size = new Size(273, 22);
            this.directoryToolStripMenuItem.Text = "Directory of non-Audible files";
            this.directoryToolStripMenuItem.Click += new EventHandler(this.menuItem21_Click);
            this.wAVFilesToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("wAVFilesToolStripMenuItem.Image");
            this.wAVFilesToolStripMenuItem.Name = "wAVFilesToolStripMenuItem";
            this.wAVFilesToolStripMenuItem.ShortcutKeys = Keys.W | Keys.Control;
            this.wAVFilesToolStripMenuItem.Size = new Size(273, 22);
            this.wAVFilesToolStripMenuItem.Text = "Directory of WAV files";
            this.wAVFilesToolStripMenuItem.Click += new EventHandler(this.wAVFilesToolStripMenuItem_Click);
            this.beginConversionToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("beginConversionToolStripMenuItem.Image");
            this.beginConversionToolStripMenuItem.Name = "beginConversionToolStripMenuItem";
            this.beginConversionToolStripMenuItem.Size = new Size(167, 22);
            this.beginConversionToolStripMenuItem.Text = "Begin Conversion";
            this.beginConversionToolStripMenuItem.Click += new EventHandler(this.menuItem14_Click_1);
            this.wizardToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("wizardToolStripMenuItem.Image");
            this.wizardToolStripMenuItem.Name = "wizardToolStripMenuItem";
            this.wizardToolStripMenuItem.Size = new Size(167, 22);
            this.wizardToolStripMenuItem.Text = "Wizard";
            this.wizardToolStripMenuItem.Click += new EventHandler(this.wizardToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(164, 6);
            this.quitToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("quitToolStripMenuItem.Image");
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = Keys.Q | Keys.Control;
            this.quitToolStripMenuItem.Size = new Size(167, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new EventHandler(this.menuItem3_Click);
            this.chapterToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
            {
        (ToolStripItem) this.advancedSplitterToolStripMenuItem,
        (ToolStripItem) this.overdriveChapterizerToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.chapterEditorToolStripMenuItem,
        (ToolStripItem) this.chapterMetadataToolStripMenuItem
            });
            this.chapterToolsToolStripMenuItem.Name = "chapterToolsToolStripMenuItem";
            this.chapterToolsToolStripMenuItem.Size = new Size(93, 20);
            this.chapterToolsToolStripMenuItem.Text = "Chapter Tools";
            this.advancedSplitterToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("advancedSplitterToolStripMenuItem.Image");
            this.advancedSplitterToolStripMenuItem.Name = "advancedSplitterToolStripMenuItem";
            this.advancedSplitterToolStripMenuItem.Size = new Size(238, 22);
            this.advancedSplitterToolStripMenuItem.Text = "Advanced Splitter (external file)";
            this.advancedSplitterToolStripMenuItem.Click += new EventHandler(this.menuItem6_Click);
            this.overdriveChapterizerToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("overdriveChapterizerToolStripMenuItem.Image");
            this.overdriveChapterizerToolStripMenuItem.Name = "overdriveChapterizerToolStripMenuItem";
            this.overdriveChapterizerToolStripMenuItem.Size = new Size(238, 22);
            this.overdriveChapterizerToolStripMenuItem.Text = "Overdrive Chapterizer";
            this.overdriveChapterizerToolStripMenuItem.Click += new EventHandler(this.menuItem13_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(235, 6);
            this.chapterEditorToolStripMenuItem.Enabled = false;
            this.chapterEditorToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("chapterEditorToolStripMenuItem.Image");
            this.chapterEditorToolStripMenuItem.Name = "chapterEditorToolStripMenuItem";
            this.chapterEditorToolStripMenuItem.Size = new Size(238, 22);
            this.chapterEditorToolStripMenuItem.Text = "Adjust Chapters";
            this.chapterEditorToolStripMenuItem.Click += new EventHandler(this.menuItem7_Click);
            this.chapterMetadataToolStripMenuItem.Enabled = false;
            this.chapterMetadataToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("chapterMetadataToolStripMenuItem.Image");
            this.chapterMetadataToolStripMenuItem.Name = "chapterMetadataToolStripMenuItem";
            this.chapterMetadataToolStripMenuItem.Size = new Size(238, 22);
            this.chapterMetadataToolStripMenuItem.Text = "Chapter Metadata";
            this.chapterMetadataToolStripMenuItem.Click += new EventHandler(this.chapterMetadataToolStripMenuItem_Click);
            this.cDRippingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.simpleRipperToolStripMenuItem,
        (ToolStripItem) this.multiDriveRipperToolStripMenuItem
            });
            this.cDRippingToolStripMenuItem.Name = "cDRippingToolStripMenuItem";
            this.cDRippingToolStripMenuItem.Size = new Size(79, 20);
            this.cDRippingToolStripMenuItem.Text = "CD Ripping";
            this.simpleRipperToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("simpleRipperToolStripMenuItem.Image");
            this.simpleRipperToolStripMenuItem.Name = "simpleRipperToolStripMenuItem";
            this.simpleRipperToolStripMenuItem.Size = new Size(136, 22);
            this.simpleRipperToolStripMenuItem.Text = "Single Drive";
            this.simpleRipperToolStripMenuItem.Click += new EventHandler(this.menuItem10_Click);
            this.multiDriveRipperToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("multiDriveRipperToolStripMenuItem.Image");
            this.multiDriveRipperToolStripMenuItem.Name = "multiDriveRipperToolStripMenuItem";
            this.multiDriveRipperToolStripMenuItem.Size = new Size(136, 22);
            this.multiDriveRipperToolStripMenuItem.Text = "Multi-Drive";
            this.multiDriveRipperToolStripMenuItem.Click += new EventHandler(this.menuItem20_Click);
            this.miscToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
            {
        (ToolStripItem) this.tagEditorToolStripMenuItem,
        (ToolStripItem) this.renamingToolToolStripMenuItem,
        (ToolStripItem) this.mP3M4BJoinerToolStripMenuItem,
        (ToolStripItem) this.audioEditorToolStripMenuItem
            });
            this.miscToolsToolStripMenuItem.Name = "miscToolsToolStripMenuItem";
            this.miscToolsToolStripMenuItem.Size = new Size(76, 20);
            this.miscToolsToolStripMenuItem.Text = "Misc Tools";
            this.tagEditorToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("tagEditorToolStripMenuItem.Image");
            this.tagEditorToolStripMenuItem.Name = "tagEditorToolStripMenuItem";
            this.tagEditorToolStripMenuItem.Size = new Size(167, 22);
            this.tagEditorToolStripMenuItem.Text = "Tag Editor";
            this.tagEditorToolStripMenuItem.Click += new EventHandler(this.menuItem23_Click);
            this.renamingToolToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("renamingToolToolStripMenuItem.Image");
            this.renamingToolToolStripMenuItem.Name = "renamingToolToolStripMenuItem";
            this.renamingToolToolStripMenuItem.Size = new Size(167, 22);
            this.renamingToolToolStripMenuItem.Text = "Renaming Tool";
            this.renamingToolToolStripMenuItem.Click += new EventHandler(this.menuItem8_Click);
            this.mP3M4BJoinerToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("mP3M4BJoinerToolStripMenuItem.Image");
            this.mP3M4BJoinerToolStripMenuItem.Name = "mP3M4BJoinerToolStripMenuItem";
            this.mP3M4BJoinerToolStripMenuItem.Size = new Size(167, 22);
            this.mP3M4BJoinerToolStripMenuItem.Text = "MP3 / M4B Joiner";
            this.mP3M4BJoinerToolStripMenuItem.Click += new EventHandler(this.menuItem18_Click);
            this.audioEditorToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("audioEditorToolStripMenuItem.Image");
            this.audioEditorToolStripMenuItem.Name = "audioEditorToolStripMenuItem";
            this.audioEditorToolStripMenuItem.Size = new Size(167, 22);
            this.audioEditorToolStripMenuItem.Text = "Audio Editor";
            this.audioEditorToolStripMenuItem.Click += new EventHandler(this.audioEditorToolStripMenuItem_Click);
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
            {
        (ToolStripItem) this.advancedSettingsToolStripMenuItem,
        (ToolStripItem) this.inAudibleDownloadManagerToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.factoryResetToolStripMenuItem,
        (ToolStripItem) this.allowFormResizeToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.aboutToolStripMenuItem
            });
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.advancedSettingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
            {
        (ToolStripItem) this.splitterToolStripMenuItem,
        (ToolStripItem) this.aARipperToolStripMenuItem,
        (ToolStripItem) this.aAXRipperToolStripMenuItem,
        (ToolStripItem) this.cDRipperToolStripMenuItem,
        (ToolStripItem) this.otherToolStripMenuItem,
        (ToolStripItem) this.internalToolStripMenuItem
            });
            this.advancedSettingsToolStripMenuItem.Enabled = false;
            this.advancedSettingsToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("advancedSettingsToolStripMenuItem.Image");
            this.advancedSettingsToolStripMenuItem.Name = "advancedSettingsToolStripMenuItem";
            this.advancedSettingsToolStripMenuItem.Size = new Size(232, 22);
            this.advancedSettingsToolStripMenuItem.Text = "Advanced Settings";
            this.advancedSettingsToolStripMenuItem.Click += new EventHandler(this.menuItem9_Click);
            this.splitterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.previewToolStripMenuItem,
        (ToolStripItem) this.tempFileToolStripMenuItem,
        (ToolStripItem) this.libraryToolStripMenuItem
            });
            this.splitterToolStripMenuItem.Name = "splitterToolStripMenuItem";
            this.splitterToolStripMenuItem.Size = new Size(134, 22);
            this.splitterToolStripMenuItem.Text = "Splitter";
            this.previewToolStripMenuItem.CheckOnClick = true;
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new Size(181, 22);
            this.previewToolStripMenuItem.Text = "Low Quality Preview";
            this.previewToolStripMenuItem.Click += new EventHandler(this.previewToolStripMenuItem_Click);
            this.tempFileToolStripMenuItem.CheckOnClick = true;
            this.tempFileToolStripMenuItem.Name = "tempFileToolStripMenuItem";
            this.tempFileToolStripMenuItem.Size = new Size(181, 22);
            this.tempFileToolStripMenuItem.Text = "Temp file";
            this.tempFileToolStripMenuItem.Click += new EventHandler(this.tempFileToolStripMenuItem_Click);
            this.libraryToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.mP3SPLTToolStripMenuItem,
        (ToolStripItem) this.ffmpegToolStripMenuItem
            });
            this.libraryToolStripMenuItem.Name = "libraryToolStripMenuItem";
            this.libraryToolStripMenuItem.Size = new Size(181, 22);
            this.libraryToolStripMenuItem.Text = "Library";
            this.mP3SPLTToolStripMenuItem.Name = "mP3SPLTToolStripMenuItem";
            this.mP3SPLTToolStripMenuItem.Size = new Size(124, 22);
            this.mP3SPLTToolStripMenuItem.Text = "MP3SPLT";
            this.mP3SPLTToolStripMenuItem.Click += new EventHandler(this.mP3SPLTToolStripMenuItem_Click);
            this.ffmpegToolStripMenuItem.Name = "ffmpegToolStripMenuItem";
            this.ffmpegToolStripMenuItem.Size = new Size(124, 22);
            this.ffmpegToolStripMenuItem.Text = "ffmpeg";
            this.ffmpegToolStripMenuItem.Click += new EventHandler(this.ffmpegToolStripMenuItem_Click);
            this.aARipperToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.directshowFilterToolStripMenuItem,
        (ToolStripItem) this.audibleManagerToolStripMenuItem,
        (ToolStripItem) this.inAudibleNGToolStripMenuItem
            });
            this.aARipperToolStripMenuItem.Name = "aARipperToolStripMenuItem";
            this.aARipperToolStripMenuItem.Size = new Size(134, 22);
            this.aARipperToolStripMenuItem.Text = "AA Ripper";
            this.directshowFilterToolStripMenuItem.Name = "directshowFilterToolStripMenuItem";
            this.directshowFilterToolStripMenuItem.Size = new Size(165, 22);
            this.directshowFilterToolStripMenuItem.Text = "Directshow Filter";
            this.directshowFilterToolStripMenuItem.Click += new EventHandler(this.directshowFilterToolStripMenuItem_Click);
            this.audibleManagerToolStripMenuItem.Name = "audibleManagerToolStripMenuItem";
            this.audibleManagerToolStripMenuItem.Size = new Size(165, 22);
            this.audibleManagerToolStripMenuItem.Text = "Audible Manager";
            this.audibleManagerToolStripMenuItem.Click += new EventHandler(this.audibleManagerToolStripMenuItem_Click);
            this.inAudibleNGToolStripMenuItem.Name = "inAudibleNGToolStripMenuItem";
            this.inAudibleNGToolStripMenuItem.Size = new Size(165, 22);
            this.inAudibleNGToolStripMenuItem.Text = "inAudible-NG";
            this.inAudibleNGToolStripMenuItem.Click += new EventHandler(this.inAudibleNGToolStripMenuItem_Click);
            this.aAXRipperToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.iTunesToolStripMenuItem,
        (ToolStripItem) this.audibleManagerToolStripMenuItem1,
        (ToolStripItem) this.inAudibleNGToolStripMenuItem1
            });
            this.aAXRipperToolStripMenuItem.Name = "aAXRipperToolStripMenuItem";
            this.aAXRipperToolStripMenuItem.Size = new Size(134, 22);
            this.aAXRipperToolStripMenuItem.Text = "AAX Ripper";
            this.iTunesToolStripMenuItem.Name = "iTunesToolStripMenuItem";
            this.iTunesToolStripMenuItem.Size = new Size(165, 22);
            this.iTunesToolStripMenuItem.Text = "iTunes";
            this.iTunesToolStripMenuItem.Click += new EventHandler(this.iTunesToolStripMenuItem_Click);
            this.audibleManagerToolStripMenuItem1.Name = "audibleManagerToolStripMenuItem1";
            this.audibleManagerToolStripMenuItem1.Size = new Size(165, 22);
            this.audibleManagerToolStripMenuItem1.Text = "Audible Manager";
            this.audibleManagerToolStripMenuItem1.Click += new EventHandler(this.audibleManagerToolStripMenuItem1_Click);
            this.inAudibleNGToolStripMenuItem1.Name = "inAudibleNGToolStripMenuItem1";
            this.inAudibleNGToolStripMenuItem1.Size = new Size(165, 22);
            this.inAudibleNGToolStripMenuItem1.Text = "inAudible-NG";
            this.inAudibleNGToolStripMenuItem1.Click += new EventHandler(this.inAudibleNGToolStripMenuItem1_Click);
            this.cDRipperToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.cUEToolsToolStripMenuItem,
        (ToolStripItem) this.cDDA2WAVToolStripMenuItem
            });
            this.cDRipperToolStripMenuItem.Name = "cDRipperToolStripMenuItem";
            this.cDRipperToolStripMenuItem.Size = new Size(134, 22);
            this.cDRipperToolStripMenuItem.Text = "CD Ripper";
            this.cUEToolsToolStripMenuItem.Name = "cUEToolsToolStripMenuItem";
            this.cUEToolsToolStripMenuItem.Size = new Size(138, 22);
            this.cUEToolsToolStripMenuItem.Text = "CUE Tools";
            this.cUEToolsToolStripMenuItem.Click += new EventHandler(this.cUEToolsToolStripMenuItem_Click);
            this.cDDA2WAVToolStripMenuItem.Name = "cDDA2WAVToolStripMenuItem";
            this.cDDA2WAVToolStripMenuItem.Size = new Size(138, 22);
            this.cDDA2WAVToolStripMenuItem.Text = "CDDA2WAV";
            this.cDDA2WAVToolStripMenuItem.Click += new EventHandler(this.cDDA2WAVToolStripMenuItem_Click);
            this.otherToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.beepOnJobCompletionToolStripMenuItem,
        (ToolStripItem) this.createNFOToolStripMenuItem,
        (ToolStripItem) this.scrapeAudiblecomForAddedMetadataToolStripMenuItem
            });
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new Size(134, 22);
            this.otherToolStripMenuItem.Text = "Other";
            this.beepOnJobCompletionToolStripMenuItem.CheckOnClick = true;
            this.beepOnJobCompletionToolStripMenuItem.Name = "beepOnJobCompletionToolStripMenuItem";
            this.beepOnJobCompletionToolStripMenuItem.Size = new Size(287, 22);
            this.beepOnJobCompletionToolStripMenuItem.Text = "Beep on job completion";
            this.beepOnJobCompletionToolStripMenuItem.Click += new EventHandler(this.beepOnJobCompletionToolStripMenuItem_Click);
            this.createNFOToolStripMenuItem.CheckOnClick = true;
            this.createNFOToolStripMenuItem.Name = "createNFOToolStripMenuItem";
            this.createNFOToolStripMenuItem.Size = new Size(287, 22);
            this.createNFOToolStripMenuItem.Text = "Create NFO";
            this.createNFOToolStripMenuItem.Click += new EventHandler(this.createNFOToolStripMenuItem_Click);
            this.scrapeAudiblecomForAddedMetadataToolStripMenuItem.CheckOnClick = true;
            this.scrapeAudiblecomForAddedMetadataToolStripMenuItem.Name = "scrapeAudiblecomForAddedMetadataToolStripMenuItem";
            this.scrapeAudiblecomForAddedMetadataToolStripMenuItem.Size = new Size(287, 22);
            this.scrapeAudiblecomForAddedMetadataToolStripMenuItem.Text = "Scrape Audible.com for added metadata";
            this.scrapeAudiblecomForAddedMetadataToolStripMenuItem.Click += new EventHandler(this.scrapeAudiblecomForAddedMetadataToolStripMenuItem_Click);
            this.internalToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.addAppleTagsToM4BToolStripMenuItem,
        (ToolStripItem) this.removeTinyVerySmallChaptersToolStripMenuItem
            });
            this.internalToolStripMenuItem.Name = "internalToolStripMenuItem";
            this.internalToolStripMenuItem.Size = new Size(134, 22);
            this.internalToolStripMenuItem.Text = "Internal";
            this.addAppleTagsToM4BToolStripMenuItem.CheckOnClick = true;
            this.addAppleTagsToM4BToolStripMenuItem.Name = "addAppleTagsToM4BToolStripMenuItem";
            this.addAppleTagsToM4BToolStripMenuItem.Size = new Size(221, 22);
            this.addAppleTagsToM4BToolStripMenuItem.Text = "Add Apple tags to M4B";
            this.addAppleTagsToM4BToolStripMenuItem.Click += new EventHandler(this.addAppleTagsToM4BToolStripMenuItem_Click);
            this.removeTinyVerySmallChaptersToolStripMenuItem.CheckOnClick = true;
            this.removeTinyVerySmallChaptersToolStripMenuItem.Name = "removeTinyVerySmallChaptersToolStripMenuItem";
            this.removeTinyVerySmallChaptersToolStripMenuItem.Size = new Size(221, 22);
            this.removeTinyVerySmallChaptersToolStripMenuItem.Text = "Remove very small chapters";
            this.removeTinyVerySmallChaptersToolStripMenuItem.Click += new EventHandler(this.removeTinyVerySmallChaptersToolStripMenuItem_Click);
            this.inAudibleDownloadManagerToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("inAudibleDownloadManagerToolStripMenuItem.Image");
            this.inAudibleDownloadManagerToolStripMenuItem.Name = "inAudibleDownloadManagerToolStripMenuItem";
            this.inAudibleDownloadManagerToolStripMenuItem.Size = new Size(232, 22);
            this.inAudibleDownloadManagerToolStripMenuItem.Text = "inAudible Download Manager";
            this.inAudibleDownloadManagerToolStripMenuItem.Click += new EventHandler(this.inAudibleDownloadManagerToolStripMenuItem_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(229, 6);
            this.factoryResetToolStripMenuItem.Name = "factoryResetToolStripMenuItem";
            this.factoryResetToolStripMenuItem.Size = new Size(232, 22);
            this.factoryResetToolStripMenuItem.Text = "Factory Reset";
            this.factoryResetToolStripMenuItem.Click += new EventHandler(this.factoryResetToolStripMenuItem_Click);
            this.allowFormResizeToolStripMenuItem.Enabled = false;
            this.allowFormResizeToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("allowFormResizeToolStripMenuItem.Image");
            this.allowFormResizeToolStripMenuItem.Name = "allowFormResizeToolStripMenuItem";
            this.allowFormResizeToolStripMenuItem.Size = new Size(232, 22);
            this.allowFormResizeToolStripMenuItem.Text = "Allow Form Resize";
            this.allowFormResizeToolStripMenuItem.Click += new EventHandler(this.menuItem16_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(229, 6);
            this.aboutToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("aboutToolStripMenuItem.Image");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new Size(232, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new EventHandler(this.menuItem11_Click);
            this.btnBatchEdit.Enabled = false;
            this.btnBatchEdit.Location = new Point(329, 27);
            this.btnBatchEdit.Name = "btnBatchEdit";
            this.btnBatchEdit.Size = new Size(48, 23);
            this.btnBatchEdit.TabIndex = 24;
            this.btnBatchEdit.Text = "Batch";
            this.btnBatchEdit.UseVisualStyleBackColor = true;
            this.btnBatchEdit.Click += new EventHandler(this.btnBatchEdit_Click);
            this.grpDownloader.Controls.Add((Control)this.lblDlCodec);
            this.grpDownloader.Controls.Add((Control)this.lblDlProductId);
            this.grpDownloader.Controls.Add((Control)this.lblDlTitle);
            this.grpDownloader.Controls.Add((Control)this.lblDownloadStatus);
            this.grpDownloader.Location = new Point(477, 440);
            this.grpDownloader.Name = "grpDownloader";
            this.grpDownloader.Size = new Size(200, 83);
            this.grpDownloader.TabIndex = 25;
            this.grpDownloader.TabStop = false;
            this.grpDownloader.Text = "Audible.com Download";
            this.grpDownloader.Visible = false;
            this.lblDlCodec.AutoSize = true;
            this.lblDlCodec.Location = new Point(6, 55);
            this.lblDlCodec.Name = "lblDlCodec";
            this.lblDlCodec.Size = new Size(58, 13);
            this.lblDlCodec.TabIndex = 19;
            this.lblDlCodec.Text = "lblDlCodec";
            this.lblDlProductId.AutoSize = true;
            this.lblDlProductId.Location = new Point(6, 37);
            this.lblDlProductId.Name = "lblDlProductId";
            this.lblDlProductId.Size = new Size(73, 13);
            this.lblDlProductId.TabIndex = 18;
            this.lblDlProductId.Text = "lblDlProductId";
            this.lblDlTitle.AutoSize = true;
            this.lblDlTitle.Location = new Point(6, 20);
            this.lblDlTitle.Name = "lblDlTitle";
            this.lblDlTitle.Size = new Size(47, 13);
            this.lblDlTitle.TabIndex = 17;
            this.lblDlTitle.Text = "lblDlTitle";
            this.AllowDrop = true;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ClientSize = new Size(694, 638);
            this.Controls.Add((Control)this.grpDownloader);
            this.Controls.Add((Control)this.btnBatchEdit);
            this.Controls.Add((Control)this.goldProgressBarEx1);
            this.Controls.Add((Control)this.btnLossless);
            this.Controls.Add((Control)this.btnSplitter);
            this.Controls.Add((Control)this.btnCDrip);
            this.Controls.Add((Control)this.lblResizer);
            this.Controls.Add((Control)this.grpITunes);
            this.Controls.Add((Control)this.grpOutputOptions);
            this.Controls.Add((Control)this.btnOutputOptions);
            this.Controls.Add((Control)this.btnLog);
            this.Controls.Add((Control)this.grpOutputType);
            this.Controls.Add((Control)this.grpSplitting);
            this.Controls.Add((Control)this.grpLame);
            this.Controls.Add((Control)this.rtbLog);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.btnOutputFile);
            this.Controls.Add((Control)this.txtOutputFile);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.btnInputFile);
            this.Controls.Add((Control)this.txtInputFile);
            this.Controls.Add((Control)this.btnConvert);
            this.Controls.Add((Control)this.menuStrip1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = nameof(Form1);
            this.Text = "inAudible 1.97";
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new EventHandler(this.Form1_Load);
            this.DragDrop += new DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new DragEventHandler(this.Form1_DragEnter);
            this.grpLame.ResumeLayout(false);
            this.grpLame.PerformLayout();
            this.tbVBRquality.EndInit();
            this.grpOutputType.ResumeLayout(false);
            this.grpOutputType.PerformLayout();
            ((ISupportInitialize)this.pbCover).EndInit();
            this.grpSplitting.ResumeLayout(false);
            this.grpSplitting.PerformLayout();
            this.grpOutputOptions.ResumeLayout(false);
            this.grpOutputOptions.PerformLayout();
            this.grpITunes.ResumeLayout(false);
            this.grpITunes.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpDownloader.ResumeLayout(false);
            this.grpDownloader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private delegate void SetTextCallback(string text);

        private enum ProcessingMode
        {
            AA,
            AAX,
            Other,
        }

        private delegate int GetBitrateCallback();

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
    }
}
