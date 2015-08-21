namespace Uol.PagSeguro.Resources
{
    using System;
    using System.Xml;

    using Uol.PagSeguro.XmlParse;

    public class PagSeguroXmlConfiguration : IPagSeguroConfiguration
    {
        private readonly string filename;

        private const string defaultUrlXmlConfiguration = ".../.../Configuration/PagSeguroConfig.xml";

        public string Email { get; private set; }

        public string Token { get; private set; }

        public string SandboxEmail { get; private set; }

        public string SandboxToken { get; private set; }

        public string FormUrlEncoded { get; private set; }

        public string Encoding { get; private set; }

        public string LibVersion { get; private set; }

        public int RequestTimeout { get; private set; }

        public Uri NotificationUrl { get; private set; }

        public Uri PaymentUrl { get; private set; }

        public Uri PaymentRedirectUrl { get; private set; }

        public Uri SearchUrl { get; private set; }

        public Uri PreApprovalUrl { get; private set; }

        public Uri PreApprovalRedirectUrl { get; private set; }

        public Uri PreApprovalNotificationUrl { get; private set; }

        public Uri PreApprovalSearchUrl { get; private set; }

        public Uri PreApprovalCancelUrl { get; private set; }

        public Uri PreApprovalPaymentUrl { get; private set; }

        public PagSeguroXmlConfiguration(string xmlFile)
        {
            this.filename = xmlFile;
        }

        internal PagSeguroXmlConfiguration() : this(defaultUrlXmlConfiguration)
        {
            this.Load();
        }

        public void Load(bool sandbox = false)
        {
            var xml = new XmlDocument();
            xml.Load(filename);

            this.NotificationUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.Notification, sandbox));
            this.PaymentUrl = new Uri(GetUrlValue(xml, PagSeguroConfigSerializer.Payment, sandbox));
            this.RequestTimeout =
                Convert.ToInt32(this.GetDataConfiguration(xml, PagSeguroConfigSerializer.RequestTimeout));

            this.Email = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.Email);
            this.Token = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.Email);

            this.SandboxEmail = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.SandboxEmail);
            this.SandboxToken = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.SandboxToken);

            this.FormUrlEncoded = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.FormUrlEncoded);
            this.Encoding = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.Encoding);
            this.FormUrlEncoded = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.FormUrlEncoded);
            this.LibVersion = this.GetDataConfiguration(xml, PagSeguroConfigSerializer.LibVersion);
            this.PaymentRedirectUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.PaymentRedirect, sandbox));
            this.SearchUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.Search, sandbox));
            this.PreApprovalUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.PreApproval, sandbox));
            this.PreApprovalRedirectUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.PreApprovalRedirect, sandbox));
            this.PreApprovalNotificationUrl =
                new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.PreApprovalNotification, sandbox));
            this.PreApprovalSearchUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.PreApprovalSearch, sandbox));
            this.PreApprovalCancelUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.PreApprovalCancel, sandbox));
            this.PreApprovalPaymentUrl = new Uri(this.GetUrlValue(xml, PagSeguroConfigSerializer.PreApprovalPayment, sandbox));
        }

        private string GetDataConfiguration(XmlDocument doc, string data)
        {
            return PagSeguroConfigSerializer.GetDataConfiguration(doc, data);
        }

        private string GetUrlValue(XmlDocument xml, string url, bool sandBox)
        {
            const string pagseguroUrl = "pagseguro.uol";
            const string sandboxUrl = "sandbox.pagseguro.uol";

            var value = PagSeguroConfigSerializer.GetWebserviceUrl(xml, url);
            
            if (sandBox)
            {
                return value.Replace(pagseguroUrl, sandboxUrl);
            }
            else
            {
                return value.Replace(sandboxUrl, pagseguroUrl);
            }
        }
    }
}