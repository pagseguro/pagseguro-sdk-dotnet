namespace Uol.PagSeguro.Resources
{
    using System;

    public interface IPagSeguroConfiguration
    {
        string Email { get; }
        string Token { get; }

        string SandboxEmail { get; }

        string SandboxToken { get; }

        string FormUrlEncoded { get; }

        string Encoding { get; }

        string LibVersion { get; }

        int RequestTimeout { get; }

        Uri NotificationUrl { get; }

        Uri PaymentUrl { get; }
         
        Uri PaymentRedirectUrl { get;}

        Uri SearchUrl { get; }

        Uri PreApprovalUrl { get; }

        Uri PreApprovalRedirectUrl { get;  }

        Uri PreApprovalNotificationUrl { get;}

        Uri PreApprovalSearchUrl { get; }

        Uri PreApprovalCancelUrl { get; }

        Uri PreApprovalPaymentUrl { get; }

        void Load(bool sandbox);
    }
}