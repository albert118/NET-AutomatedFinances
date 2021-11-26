using FluentMigrator;

namespace AutomatedFinances.Migrations
{
    [Migration(202111261126)]
    public class AddFinanceDomainTables : Migration
    {
        private const string BizTable = "Business";
        private const string ExpTable = "Expenditure";
        private const string PaymentMethodTable = "PaymentMethod";
        private const string NoteTable = "Note";
        private const string CostTable = "Cost";
        
        // Order of operations is important here due to FKs
        public override void Down() {
            if (Schema.Table(CostTable).Exists()) {
                Delete.Table(CostTable);
            }
            if (Schema.Table(NoteTable).Exists()) {
                Delete.Table(NoteTable);
            }
            
            if (Schema.Table(PaymentMethodTable).Exists()) {
                Delete.Table(PaymentMethodTable);
            }
            
            if (Schema.Table(BizTable).Exists()) {
                Delete.Table(BizTable);
            }
            
            if (Schema.Table(ExpTable).Exists()) {
                Delete.Table(ExpTable);
            }
        }

        public override void Up() {
            // root tables
            CreateBusinessTable();
            CreateExpenditureTable();
            CreatePaymentMethodTable();

            // dependent tables - order is important!!
            CreateNoteTable();
            CreateCostTable();

            AddCostFkConstraints();
        }

        private void CreateBusinessTable() {
            if (!Schema.Table(BizTable).Exists()) {
                Create
                    .Table(BizTable)
                    .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefaultValue(SystemMethods.NewGuid)
                    .WithColumn("ACN").AsCustom("varchar(9)").Nullable()
                    .WithColumn("ABN").AsCustom("varchar(11)").Nullable()
                    .WithColumn("Name").AsCustom("varchar(100)").NotNullable();
            }
        }
        
        private void CreateExpenditureTable() {
            if (!Schema.Table(ExpTable).Exists()) {
                Create
                    .Table(ExpTable)
                    .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefaultValue(SystemMethods.NewGuid)
                    .WithColumn("Amount").AsDouble().NotNullable().WithDefaultValue(0.0)
                    .WithColumn("CurrencyCode").AsString().NotNullable().WithDefaultValue("None")
                    .WithColumn("Description").AsCustom("varchar(100)").WithDefaultValue("")
                    .WithColumn("Message").AsCustom("text").Nullable();
            }
        }
        
        private void CreatePaymentMethodTable() {
            if (!Schema.Table(PaymentMethodTable).Exists())  {
                Create
                    .Table(PaymentMethodTable)
                    .WithColumn("Id").AsInt16().PrimaryKey().Identity().NotNullable()
                    .WithColumn("Source").AsCustom("varchar(30)").WithDefaultValue("")
                    .WithColumn("Method").AsString().NotNullable().WithDefaultValue("None");
            }
        }
        
        private void CreateNoteTable() {
            if (!Schema.Table(NoteTable).Exists()) {
                Create
                    .Table(NoteTable)
                    .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefaultValue(SystemMethods.NewGuid)
                    .WithColumn("Body").AsCustom("text").NotNullable()
                    .WithColumn("CostId_FK").AsInt64().NotNullable();
            }
        }
        
        private void CreateCostTable() {
            if (!Schema.Table(CostTable).Exists()) {
                Create
                    .Table(CostTable)
                    .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefaultValue(SystemMethods.NewGuid)
                    .WithColumn("Category").AsCustom("varchar(100)").NotNullable().WithDefaultValue("None")
                    .WithColumn("BusinessId").AsGuid().Nullable()
                    .WithColumn("PaymentMethodId").AsInt16().NotNullable()
                    .WithColumn("NoteId").AsGuid().Nullable();
            }
        }

        private void AddCostFkConstraints() {
            // one-to-one
            Create.ForeignKey("FK_Cost_BusinessId_Business_Id")
                .FromTable(CostTable).ForeignColumn("BusinessId")
                .ToTable(BizTable).PrimaryColumn("Id");
            Create.UniqueConstraint()
                .OnTable(CostTable).Column("BusinessId");
            
            // one-to-one
            Create.ForeignKey("FK_Cost_PaymentMethodId_PaymentMethod_Id")
                .FromTable(CostTable).ForeignColumn("PaymentMethodId")
                .ToTable(PaymentMethodTable).PrimaryColumn("Id");
            Create.UniqueConstraint()
                .OnTable(CostTable).Column("PaymentMethodId");
            
            // many-to-many
            Create.ForeignKey("FK_Cost_NoteId_Note_Id")
                .FromTable(CostTable).ForeignColumn("NoteId")
                .ToTable(NoteTable).PrimaryColumn("Id");
        }
    }
}