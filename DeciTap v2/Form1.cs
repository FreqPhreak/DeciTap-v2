using MiFare;
using MiFare.Classic;
using MiFare.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace DeciTap
{
    public partial class MainForm : Form
    {
        private SmartCardReader reader;
        private MiFareCard card;
        private bool hasSentKeyboardWedge = false;
        private ManagementEventWatcher watcher;

        public MainForm()
        {
            InitializeComponent();
            GetDevices();
            pnlStatus.BackColor = Color.Red;
        }

        private void GetDevices()
        {
            try
            {
                IReadOnlyList<string> readers = CardReader.GetReaderNames();
                cboDevices.Items.AddRange(readers.ToArray());
                if (cboDevices.Items.Count > 0)
                {
                    cboDevices.SelectedIndex = 0;
                }
                else
                {
                    ShowMessage("No NFC readers found.");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error getting NFC readers: " + ex.Message);
            }
        }

        private async void ConnectToDevice(string name)
        {
            try
            {
                reader = await CardReader.FindAsync(name);
                if (reader == null)
                {
                    ShowMessage("Failed to connect to the NFC reader. It may be in use by another application.");
                    return;
                }
                reader.CardAdded += CardAdded;
                reader.CardRemoved += CardRemoved;
                ShowMessage("Place card on the reader to scan.");
            }
            catch (Exception ex)
            {
                ShowMessage("Error connecting to NFC reader: " + ex.Message);
            }
        }

        private void CardRemoved(object sender, EventArgs e)
        {
            try
            {
                if (card != null)
                {
                    // Safely attempt to dispose the card
                    try
                    {
                        card.Dispose();
                    }
                    catch (Exception innerEx)
                    {
                        // Ignore benign "Operation Successful" exceptions
                        if (!innerEx.Message.Contains("operation completed successfully"))
                        {
                            throw; // Re-throw actual errors
                        }
                        else
                        {
                            Console.WriteLine("Card disposal resulted in benign exception: " + innerEx.Message);
                        }
                    }

                    card = null;
                }

                // Reset the keyboard wedge flag
                hasSentKeyboardWedge = false;

                // Update the status panel to indicate no card present
                if (pnlStatus != null)
                    pnlStatus.BackColor = Color.Red;

                ShowMessage("Card removed. Place card on the reader to scan.");
            }
            catch (Exception ex)
            {
                ShowMessage("CardRemoved Exception: " + ex.Message);
            }
        }

        private async void CardAdded(object sender, CardEventArgs args)
        {
            try
            {
                card = args.SmartCard.CreateMiFareCard();
                await DisplayCardUid();
            }
            catch (Exception ex)
            {
                ShowMessage("Error reading card: " + ex.Message);
            }
        }

        private async Task DisplayCardUid()
        {
            if (card == null)
                return;

            try
            {
                var uid = await card.GetUid();
                string format = string.Empty;
                int charLength = 0;
                string stripCharacters = string.Empty;

                this.Invoke((Action)(() =>
                {
                    if (cboFormat?.SelectedItem != null)
                    {
                        format = cboFormat.SelectedItem.ToString();
                    }
                    if (txtCharLength != null && int.TryParse(txtCharLength.Text, out int length))
                    {
                        charLength = length;
                    }
                    if (txtStripLeading != null)
                    {
                        stripCharacters = txtStripLeading.Text;
                    }
                }));

                var outputValue = ConvertUid(uid, format, charLength, stripCharacters);

                this.Invoke((Action)(() =>
                {
                    txtCardId.Text = outputValue;
                    if (chkKeyboardWedge?.Checked == true && !hasSentKeyboardWedge)
                    {
                        SendKeys.Send(outputValue);
                        if (chkEnterAfterOutput?.Checked == true)
                        {
                            SendKeys.Send("{ENTER}");
                        }
                        hasSentKeyboardWedge = true;
                    }
                    if (pnlStatus != null)
                        pnlStatus.BackColor = Color.Green;
                }));
                ShowMessage("Card detected. Output: " + outputValue);
            }
            catch (Exception ex)
            {
                ShowMessage("Error processing card UID: " + ex.Message);
            }
        }

        private string ConvertUid(byte[] uid, string format, int charLength, string stripCharacters)
        {
            string normalizedFormat = format.Replace(" ", "").ToLower();
            string result = string.Empty;

            try
            {
                switch (normalizedFormat)
                {
                    case "decimal":
                        {
                            long dec = 0;
                            for (int i = 0; i < uid.Length; i++)
                            {
                                dec = (dec << 8) + uid[uid.Length - 1 - i];
                            }
                            result = dec.ToString();
                            break;
                        }
                    case "reverseddecimal":
                        {
                            long dec = 0;
                            for (int i = 0; i < uid.Length; i++)
                            {
                                dec = (dec << 8) + uid[i];
                            }
                            result = dec.ToString();
                            break;
                        }
                    case "hex":
                        {
                            long dec = 0;
                            for (int i = 0; i < uid.Length; i++)
                            {
                                dec = (dec << 8) + uid[uid.Length - 1 - i];
                            }
                            result = dec.ToString("X");
                            break;
                        }
                    case "reversedhex":
                        {
                            long dec = 0;
                            for (int i = 0; i < uid.Length; i++)
                            {
                                dec = (dec << 8) + uid[i];
                            }
                            result = dec.ToString("x");
                            break;
                        }
                    default:
                        throw new ArgumentException("Invalid format specified");
                }

                if (charLength > 0 && result.Length > charLength)
                {
                    result = result.Substring(0, charLength);
                }

                if (!string.IsNullOrEmpty(stripCharacters) && result.StartsWith(stripCharacters))
                {
                    result = result.Substring(stripCharacters.Length);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error converting UID: " + ex.Message);
            }

            return result;
        }

        private void ShowMessage(string message)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke((Action)(() =>
                {
                    lblMessage.Text = message;
                }));
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        private void StartDeviceMonitoring()
        {
            try
            {
                WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
                watcher = new ManagementEventWatcher(query);
                watcher.EventArrived += OnDeviceConnected;
                watcher.Start();
                ShowMessage("Monitoring for NFC reader connections...");
            }
            catch (Exception ex)
            {
                ShowMessage("Error initializing device monitoring: " + ex.Message);
            }
        }

        private void StopDeviceMonitoring()
        {
            try
            {
                if (watcher != null)
                {
                    watcher.Stop();
                    watcher.Dispose();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error stopping device monitoring: " + ex.Message);
            }
        }

        private void OnDeviceConnected(object sender, EventArrivedEventArgs e)
        {
            try
            {
                ShowMessage("A new USB device was connected. Refreshing NFC reader list...");
                Invoke((Action)(() =>
                {
                    cboDevices.Items.Clear();
                    GetDevices();
                    if (cboDevices.Items.Count > 0)
                    {
                        ConnectToDevice(cboDevices.SelectedItem.ToString());
                    }
                }));
            }
            catch (Exception ex)
            {
                ShowMessage("Error processing device connection event: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboDevices.SelectedItem != null)
                {
                    ConnectToDevice(cboDevices.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error refreshing device list: " + ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartDeviceMonitoring();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopDeviceMonitoring();
            base.OnFormClosing(e);
        }
    }
}