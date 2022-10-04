using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S3Connector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            regionComboBox.Items.Add("EUWest2");
            regionComboBox.Items.Add("EUSouth1");
            regionComboBox.Items.Add("EUWest3");
            regionComboBox.Items.Add("USEast2");
            regionComboBox.Items.Add("EUWest1");
            regionComboBox.Items.Add("EUNorth1");
            regionComboBox.Items.Add("USWest1");
            regionComboBox.Items.Add("USWest2");
            regionComboBox.Items.Add("USEast1");
        }


        public void OnOKClick()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errorMessages = "";

            if( accessKeyIDTextBox.Text.Length <= 0 )
            {
                errorMessages += "Please fill Access Key ID.\n";
            }// if

            if (secretAccessKeyTextBox.Text.Length <= 0)
            {
                errorMessages += "Please fill Secret Access Key.\n";
            }// if

            if (regionComboBox.SelectedIndex <= -1)
            {
                errorMessages += "Please fill Region.\n";
            }// if

            if (localDirectoryTextBox.Text.Length <= 0)
            {
                errorMessages += "Please fill Local Directory.\n";
            }// if

            if (bucketLinkTextBox.Text.Length <= 0)
            {
                errorMessages += "Please fill Bucket Link.\n";
            }// if

            if(errorMessages.Length > 0)
            {
                MessageBox.Show(errorMessages);
                return;
            }

            IAmazonS3 client = new AmazonS3Client(accessKeyIDTextBox.Text, secretAccessKeyTextBox.Text, regionComboBox.Text);

        }
    }
}

namespace Amazon.DocSamples.S3
{
    class GetObjectTest
    {
        private string bucketName;



        private string keyName;

        GetObjectTest( string accessKeyID, string secretAccessKey, string selectedRegion, string bucketLink, string localDirectory )
        {

        }

        private const string bucketName = "bukkket";
        private const string keyName = "ТЕСТ";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest2;
        private static IAmazonS3 client;
       //public static void Main()
       //{
       //    client = new AmazonS3Client(bucketRegion);
       //    ReadObjectDataAsync().Wait();
       //}

        static async Task ReadObjectDataAsync()
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName

                };

                GetObjectResponse response = await client.GetObjectAsync(request);

                int numBytesToRead = (int)response.ContentLength;
                if (numBytesToRead > 0)
                {
                    int numBytesRead = 0;
                    byte[] buffer = new byte[32768];
                    using (FileStream fstream = new FileStream("D:\\3.txt", FileMode.Create))
                    {
                        using (Stream responseStream = response.ResponseStream)
                        {
                            do
                            {
                                numBytesRead = responseStream.Read(buffer, 0, buffer.Length);
                                fstream.Write(buffer, 0, numBytesRead);
                            }
                            while (numBytesRead > 0);
                        }
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                // If bucket or object does not exist
                Console.WriteLine("Error encountered ***. Message:'{0}' when reading object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when reading object", e.Message);
            }
        }
    }
}