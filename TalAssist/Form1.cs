using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace TalAssist
{
    public partial class Form1 : Form
    {
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        public static void MouseEvent(MouseEventType eventType, POINT point)
        {
            SetCursorPos(point.X, point.Y);
            mouse_event((uint)eventType, (uint)point.X, (uint)point.Y, 0, UIntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEHOOKSTRUCT
        {
            public POINT pt;
            public IntPtr hwnd;
            public uint wHitTestCode;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public const int KEYEVENTF_EXTENDEDKEY = 0x1;
        public const int KEYEVENTF_KEYUP = 0x2;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelKeyboardProc _keyboardProc;

        private static IntPtr _mouseHookID = IntPtr.Zero;

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MBUTTONDOWN = 0x0207;

        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_MBUTTONUP = 0x0208;
        private const int WM_MOUSEMOVE = 0x0200;

        private bool _recording = false;
        private List<KeyCode> _recordedKeys = new List<KeyCode>();
        private static List<string> _recordedMouseEvents = new List<string>();


        public Form1()
        {
            InitializeComponent();
            LoadRunningProcesses();
            PopulateKeyComboBoxes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Stopped";
            LoadRunningProcesses();
        }

        private static LowLevelMouseProc _mouseProc = MouseHookCallback;

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MOUSEHOOKSTRUCT mouseInfo = (MOUSEHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MOUSEHOOKSTRUCT));
                int x = mouseInfo.pt.X;
                int y = mouseInfo.pt.Y;

                switch (wParam.ToInt32())
                {
                    case WM_LBUTTONDOWN:
                        _recordedMouseEvents.Add($"LeftButtonDown {x},{y}");
                        break;
                    case WM_RBUTTONDOWN:
                        _recordedMouseEvents.Add($"RightButtonDown {x},{y}");
                        break;
                    case WM_MBUTTONDOWN:
                        _recordedMouseEvents.Add($"MiddleButtonDown {x},{y}");
                        break;
                    case WM_LBUTTONUP:
                        _recordedMouseEvents.Add($"LeftButtonUp {x},{y}");
                        break;
                    case WM_RBUTTONUP:
                        _recordedMouseEvents.Add($"RightButtonUp {x},{y}");
                        break;
                    case WM_MBUTTONUP:
                        _recordedMouseEvents.Add($"MiddleButtonUp {x},{y}");
                        break;
                    case WM_MOUSEMOVE:
                        _recordedMouseEvents.Add($"MouseMove {x},{y}");
                        break;
                }
            }

            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }

        private async void btnPlayMacro_Click(object sender, EventArgs e)
        {
            btnPlayMacro.Enabled = false;

            string[] recordedEvents = File.ReadAllLines("macro.txt");

            foreach (string recordedEvent in recordedEvents)
            {
                string[] eventData = recordedEvent.Split(' ');

                switch (eventData[0])
                {
                    case "KeyDown":
                        SendKeys(new KeyCode[] { (KeyCode)Enum.Parse(typeof(KeyCode), eventData[1]) });
                        break;
                    case "KeyUp":
                        SendKeys(new KeyCode[] { (KeyCode)Enum.Parse(typeof(KeyCode), eventData[1]) }, true);
                        break;
                    case "LeftButtonDown":
                    case "RightButtonDown":
                    case "MiddleButtonDown":
                        MouseEvent((MouseEventType)Enum.Parse(typeof(MouseEventType), eventData[0]), new POINT(int.Parse(eventData[1].Split(',')[0]), int.Parse(eventData[1].Split(',')[1])));
                        break;
                    case "LeftButtonUp":
                    case "RightButtonUp":
                    case "MiddleButtonUp":
                        MouseEvent((MouseEventType)Enum.Parse(typeof(MouseEventType), eventData[0]), new POINT(int.Parse(eventData[1].Split(',')[0]), int.Parse(eventData[1].Split(',')[1])));
                        break;
                    case "MouseMove":
                        SetCursorPos(int.Parse(eventData[1].Split(',')[0]), int.Parse(eventData[1].Split(',')[1]));
                        break;
                }

                await Task.Delay(50); // Add a small delay between events. You can adjust the value as needed.
            }

            btnPlayMacro.Enabled = true;
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private void UnhookWindowsHookEx()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN && _recording)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                _recordedKeys.Add((KeyCode)vkCode);
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (!_recording)
            {
                _recording = true;
                recordButton.Text = "Stop Recording";
                _keyboardProc = HookCallback;
                _hookID = SetHook(_keyboardProc);
                _mouseHookID = SetWindowsHookEx(WH_MOUSE_LL, _mouseProc, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

            }
            else
            {
                _recording = false;
                recordButton.Text = "Record Macro";
                UnhookWindowsHookEx(_hookID);
                UnhookWindowsHookEx(_mouseHookID);

                SaveMacroToFile();
                _recordedKeys.Clear();
            }
        }

        private void SaveMacroToFile()
        {
            string fileName = "macro.txt";
            using (StreamWriter sw = new StreamWriter("macro.txt"))
            {
                foreach (KeyCode key in _recordedKeys)
                {
                    sw.WriteLine(key.ToString());
                }
                foreach (string mouseEvent in _recordedMouseEvents.Distinct())
                {
                    sw.WriteLine(mouseEvent);
                }
            }
        }

        private void PopulateKeyComboBoxes()
        {
            var keyCodes = Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().ToList();
            var keyCodeItems = new List<KeyCodeItem> { new KeyCodeItem { DisplayName = "Do Nothing", KeyCode = null } };
            keyCodeItems.AddRange(keyCodes.Select(kc => new KeyCodeItem { DisplayName = kc.ToString(), KeyCode = kc }));

            keyComboBox1.DisplayMember = "DisplayName";
            keyComboBox1.ValueMember = "KeyCode";
            keyComboBox1.DataSource = new List<KeyCodeItem>(keyCodeItems);

            keyComboBox2.DisplayMember = "DisplayName";
            keyComboBox2.ValueMember = "KeyCode";
            keyComboBox2.DataSource = new List<KeyCodeItem>(keyCodeItems);

            keyComboBox3.DisplayMember = "DisplayName";
            keyComboBox3.ValueMember = "KeyCode";
            keyComboBox3.DataSource = new List<KeyCodeItem>(keyCodeItems);
        }

        private void LoadRunningProcesses()
        {
            Process[] processes = Process.GetProcesses();
            comboBox1.DisplayMember = "ProcessName";
            comboBox1.ValueMember = "MainWindowHandle";
            comboBox1.DataSource = processes;
        }

        //public static void SendKeys(string keys)
        //{
        //    if (keys == "tab")
        //    {
        //        // Press the key
        //        keybd_event((byte)KeyCode.TAB, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
        //        // Release the key
        //        keybd_event((byte)KeyCode.TAB, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        //    }
        //    else
        //    {
        //        foreach (char key in keys)
        //        {
        //            // Press the key
        //            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
        //            // Release the key
        //            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        //        }
        //    }
        //}

        public static void SendKeys(KeyCode[] keys, bool keyUp = false)
        {
            uint flags = (uint)(keyUp ? KEYEVENTF_KEYUP : 0);

            foreach (KeyCode key in keys)
            {
                keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | flags, 0);
            }
        }

        public static void SendKeys(KeyCode key, bool keyUp = false)
        {
            uint flags = (uint)(keyUp ? KEYEVENTF_KEYUP : 0);
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | flags, 0);
        }

        private CancellationTokenSource _cancellationTokenSource;

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                button1.Text = "Stop";
                _cancellationTokenSource = new CancellationTokenSource();

                // Disable the textboxes
                keyComboBox1.Enabled = false;
                keyComboBox2.Enabled = false;
                keyComboBox3.Enabled = false;
                repeatBox1.Enabled = false;
                repeatBox2.Enabled = false;
                repeatBox3.Enabled = false;
                timeBox1.Enabled = false;
                timeBox2.Enabled = false;
                timeBox3.Enabled = false;
                comboBox1.Enabled = false;

                await StartSendingCommandsAsync(_cancellationTokenSource.Token);
            }
            else
            {
                button1.Text = "Start";
                label1.Text = "Stopped";
                _cancellationTokenSource.Cancel();

                // Enable the textboxes
                keyComboBox1.Enabled = true;
                keyComboBox2.Enabled = true;
                keyComboBox3.Enabled = true;
                repeatBox1.Enabled = true;
                repeatBox2.Enabled = true;
                repeatBox3.Enabled = true;
                timeBox1.Enabled = true;
                timeBox2.Enabled = true;
                timeBox3.Enabled = true;
                comboBox1.Enabled = true;
            }
        }


        private async Task StartSendingCommandsAsync(CancellationToken cancellationToken)
        {
            IntPtr hWnd = (IntPtr)comboBox1.SelectedValue;

            if (hWnd == IntPtr.Zero)
            {
                label1.Text = "Selected process does not have a valid window handle.";
            }
            else
            {
                label1.Text = "Started";
                SetForegroundWindow(hWnd);
                int.TryParse(timeBox1.Text, out int timeBoxVal);

                try
                {
                    do
                    {
                        if (keyComboBox1.SelectedItem != null && ((KeyCodeItem)keyComboBox1.SelectedItem).KeyCode != null)
                        {
                            int.TryParse(repeatBox1.Text, out int repeatBox1Val);
                            await SendCommand(((KeyCodeItem)keyComboBox1.SelectedItem).KeyCode.Value, repeatBox1Val, timeBoxVal, cancellationToken);
                        }

                        if (keyComboBox2.SelectedItem != null && ((KeyCodeItem)keyComboBox2.SelectedItem).KeyCode != null)
                        {
                            int.TryParse(repeatBox2.Text, out int repeatBox2Val);
                            await SendCommand(((KeyCodeItem)keyComboBox2.SelectedItem).KeyCode.Value, repeatBox2Val, timeBoxVal, cancellationToken);
                        }

                        if (keyComboBox3.SelectedItem != null && ((KeyCodeItem)keyComboBox3.SelectedItem).KeyCode != null)
                        {
                            int.TryParse(repeatBox3.Text, out int repeatBox3Val);
                            await SendCommand(((KeyCodeItem)keyComboBox3.SelectedItem).KeyCode.Value, repeatBox3Val, timeBoxVal, cancellationToken);
                        }

                    } while (repeatCheckBox.Checked && !cancellationToken.IsCancellationRequested);
                }
                catch (OperationCanceledException)
                {
                    label1.Text = "Stopped";
                }
                finally
                {
                    button1.Text = "Start";
                }
            }
        }

        private async Task SendCommand(KeyCode keyCode, int repeat, int delay, CancellationToken cancellationToken)
        {
            for (int i = 0; i < repeat; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                SendKeys(keyCode);
                await Task.Delay(delay * 1000, cancellationToken);
            }
        }

    }
}
