using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSAExample
{
    public partial class Form1 : Form
    {
        private UnicodeEncoding _byteConvert = new UnicodeEncoding();
        private RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider();
        private byte[] _plainText;
        private byte[] _encryptedText;

        public Form1()
        {
            InitializeComponent();
        }

        public byte[] Encrypt(byte[] data, RSAParameters rsaKey, bool doOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using(RSACryptoServiceProvider rsa=new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKey);
                    encryptedData = rsa.Encrypt(data, doOAEPPadding);
                }
                return encryptedData;
            }
            catch(CryptographicException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public byte[] Decrypt(byte[] data, RSAParameters rsaKey, bool doOAEPPading)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider rsa=new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKey);
                    decryptedData = rsa.Decrypt(data, doOAEPPading);
                }

                return decryptedData;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            this._plainText = _byteConvert.GetBytes(txtPlain.Text);
            this._encryptedText = this.Encrypt(this._plainText, this._rsa.ExportParameters(false), false);
            txtEncrypted.Text = Convert.ToBase64String(this._encryptedText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] decryptedText = Decrypt(this._encryptedText, this._rsa.ExportParameters(true), false);
            txtExpected.Text =  this._byteConvert.GetString(decryptedText);
        }
    }
}
