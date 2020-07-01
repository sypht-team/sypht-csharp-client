namespace Sypht
{
    public sealed class FieldSet
    {

        private FieldSet() {}

        public static readonly string Invoice = "sypht.invoice";
        public static readonly string Document = "sypht.document";
        public static readonly string InvoiceWithLineItems = "sypht.invoice[lineitems]";
        public static readonly string Generic = "sypht.generic";
        public static readonly string BPay = "sypht.bpay";
        public static readonly string Bill = "sypht.bill";
        public static readonly string Bank = "sypht.bank";
    }
}
