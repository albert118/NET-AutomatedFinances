using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace AutomatedFinances.Migrations
{
    [Migration(202111261314)]
    public class InsertPaymentMethods : Migration
    {
        private const string PaymentMethodTable = "PaymentMethod";

        public override void Down() {
            // wipe out all data as this is a seed migration
            if (Schema.Table(PaymentMethodTable).Exists()) {
                Delete.FromTable(PaymentMethodTable);
            }
        }

        public override void Up() {
            SeedPaymentMethodsTable();
        }

        private void SeedPaymentMethodsTable() {
            if (Schema.Table(PaymentMethodTable).Exists()) {
                Insert.IntoTable(PaymentMethodTable)
                    .Row(new { Method = "Credit" })
                    .Row(new { Method = "Cash" });
            }
        }
    }
}