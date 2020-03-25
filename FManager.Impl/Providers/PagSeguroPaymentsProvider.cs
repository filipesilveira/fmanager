namespace FManager.Impl.Providers
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using FManager.Core.Dtos.Users;
    using FManager.Core.Helpers;
    using FManager.Core.Providers;
    using FManager.Core.Services;
    using FManager.Data.Entities.Shared;
    using FManager.Data.Entities.Users;

    /// <summary>
    /// PagSeguro Payments Provider
    /// </summary>
    public class PagSeguroPaymentsProvider : IPaymentsProvider
    {
        private readonly string pagSeguroCheckoutUrl = "xxx";
        private readonly string pagSeguroCheckoutPaymentUrl = "xxxx";
        private readonly string pagSeguroCheckoutTransactionUrl = "xxxx";

        private readonly string accountEmail = "xxxx";
        private readonly string accountToken = "xxxx";
        private readonly string accountCurrency = "BRL";
        private readonly string accountShippingAddressRequired = "false";

        private readonly IUsersService usersService;
        private readonly IPaymentPlansService paymentPlanService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagSeguroPaymentsProvider"/> class.
        /// </summary>
        public PagSeguroPaymentsProvider(
            IUsersService usersService,
            IPaymentPlansService paymentPlanService)
        {
            this.usersService = usersService;
            this.paymentPlanService = paymentPlanService;
        }

        public string Checkout(UserDto userDto, Guid paymentPlanId)
        {
            try
            {
                var paymentPlan = AsyncHelper.RunSync(() => this.paymentPlanService.GetByCriteriaAsync(w => w.PaymentPlanId == paymentPlanId));
                var reference = Guid.NewGuid().ToString();

                var postData = new NameValueCollection
                {
                    { "email", accountEmail },
                    { "token", accountToken },
                    { "currency", accountCurrency },
                    { "reference", reference },
                    { "senderName", userDto.Name },
                    { "senderEmail", userDto.Email },
                    { "shippingAddressRequired", accountShippingAddressRequired },
                    { "itemQuantity1", "1" },
                    { "itemId1", paymentPlan.PaymentPlanId.ToString() },
                    { "itemDescription1", paymentPlan.Description },
                    { "itemAmount1", paymentPlan.Amount.ToString() }
                };

                string xmlString = null;

                using (WebClient wc = new WebClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    var result = wc.UploadValues(pagSeguroCheckoutUrl, postData);

                    xmlString = Encoding.ASCII.GetString(result);
                }

                var xmlDoc = new XmlDocument();

                xmlDoc.LoadXml(xmlString);

                var code = xmlDoc.GetElementsByTagName("code")[0];
                var paymentUrl = string.Concat(pagSeguroCheckoutPaymentUrl, code.InnerText);

                AsyncHelper.RunSync(() => this.SavePaymentHistory(userDto.UserId, reference, paymentPlan, code.InnerText));

                return paymentUrl;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsPaymentCompleted(User user)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var today = DateTime.Now;
                var paymentHistory = user.PaymentHistories
                    .OrderByDescending(w => w.Expiration)
                    .FirstOrDefault(w => w.Expiration != null && w.Expiration >= today);

                if (paymentHistory == null)
                {
                    return false;
                }

                var uri = string.Format(this.pagSeguroCheckoutTransactionUrl, this.accountEmail, this.accountToken, paymentHistory.Reference);
                var request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Method = "GET";

                string xmlString = null;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            var xmlDoc = new XmlDocument();

                            xmlString = reader.ReadToEnd();
                            xmlDoc.LoadXml(xmlString);

                            var status = xmlDoc.GetElementsByTagName("status")[0];

                            reader.Close();
                            dataStream.Close();

                            if (status == null)
                            {
                                return false;
                            }

                            return int.Parse(status.InnerText) == 3;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetPaymentUrl(User user)
        {
            var today = DateTime.Now;

            var paymentHistory = user.PaymentHistories
                .OrderByDescending(w => w.Expiration)
                .FirstOrDefault(w => w.Expiration >= today);

            if (paymentHistory == null)
            {
                return string.Empty;
            }

            var paymentUrl = string.Concat(pagSeguroCheckoutPaymentUrl, paymentHistory.Code);

            return paymentUrl;
        }

        private async Task SavePaymentHistory(Guid userId, string reference, PaymentPlan paymentPlan, string paymentCode)
        {
            var user = await this.usersService.GetByCriteriaAsync(w => w.UserId == userId);

            user.PaymentHistories.Add(new PaymentHistory
            {
                PaymentHistoryId = Guid.NewGuid(),
                UserId = user.UserId,
                Reference = reference,
                Code = paymentCode,
                PaymentPlanId = paymentPlan.PaymentPlanId,
                PaymentStatusId = Guid.Parse("7B555CE0-49BE-4BDA-8D91-E1E0B3BC0328"),
                Expiration = DateTime.Now.AddDays(paymentPlan.Days),
                Change = DateTime.Now,
                CreationDate = DateTime.Now
            });

            await this.usersService.UpdateAsync(user);
        }
    }
}