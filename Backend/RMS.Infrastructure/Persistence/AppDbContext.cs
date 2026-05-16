using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RMS.Domain.Entities;

namespace RMS.Infrastructure.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Billstatus> Billstatuses { get; set; }

    public virtual DbSet<Chargesetting> Chargesettings { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Menucategory> Menucategories { get; set; }

    public virtual DbSet<Menuitem> Menuitems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Orderitemstatus> Orderitemstatuses { get; set; }

    public virtual DbSet<Orderstatus> Orderstatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Paymentmethod> Paymentmethods { get; set; }

    public virtual DbSet<Paymentstatus> Paymentstatuses { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Restauranttable> Restauranttables { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rolepermission> Rolepermissions { get; set; }

    public virtual DbSet<Tablestatus> Tablestatuses { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RMS;Username=postgres;Password=Sendrock@1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("bills_pkey");

            entity.ToTable("bills");

            entity.HasIndex(e => e.OrderId, "uq_bills_orderid").IsUnique();

            entity.HasIndex(e => new { e.RestaurantId, e.BillNumber }, "uq_bills_restaurantid_billnumber").IsUnique();

            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.BillNumber)
                .HasMaxLength(50)
                .HasColumnName("bill_number");
            entity.Property(e => e.BillStatusId).HasColumnName("bill_status_id");
            entity.Property(e => e.CancellationReason)
                .HasMaxLength(500)
                .HasColumnName("cancellation_reason");
            entity.Property(e => e.CancelledAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelled_at");
            entity.Property(e => e.CancelledBy).HasColumnName("cancelled_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DiscountAmount)
                .HasPrecision(10, 2)
                .HasColumnName("discount_amount");
            entity.Property(e => e.GrandTotal)
                .HasPrecision(10, 2)
                .HasColumnName("grand_total");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.ServiceChargeAmount)
                .HasPrecision(10, 2)
                .HasColumnName("service_charge_amount");
            entity.Property(e => e.SubTotal)
                .HasPrecision(10, 2)
                .HasColumnName("sub_total");
            entity.Property(e => e.TaxAmount)
                .HasPrecision(10, 2)
                .HasColumnName("tax_amount");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.BillStatus).WithMany(p => p.Bills)
                .HasForeignKey(d => d.BillStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bills_billstatuses");

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.CancelledBy)
                .HasConstraintName("fk_bills_cancelledby_users");

            entity.HasOne(d => d.Order).WithOne(p => p.Bill)
                .HasForeignKey<Bill>(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bills_orders");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Bills)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bills_restaurants");
        });

        modelBuilder.Entity<Billstatus>(entity =>
        {
            entity.HasKey(e => e.BillStatusId).HasName("billstatuses_pkey");

            entity.ToTable("billstatuses");

            entity.HasIndex(e => e.StatusName, "billstatuses_status_name_key").IsUnique();

            entity.Property(e => e.BillStatusId).HasColumnName("bill_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Chargesetting>(entity =>
        {
            entity.HasKey(e => e.ChargeSettingId).HasName("chargesettings_pkey");

            entity.ToTable("chargesettings");

            entity.HasIndex(e => new { e.RestaurantId, e.ChargeName }, "uq_chargesettings_restaurantid_chargename").IsUnique();

            entity.Property(e => e.ChargeSettingId).HasColumnName("charge_setting_id");
            entity.Property(e => e.ChargeName)
                .HasMaxLength(100)
                .HasColumnName("charge_name");
            entity.Property(e => e.ChargeValue)
                .HasPrecision(10, 2)
                .HasColumnName("charge_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPercentage)
                .HasDefaultValue(true)
                .HasColumnName("is_percentage");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Chargesettings)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_chargesettings_restaurants");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("ingredients_pkey");

            entity.ToTable("ingredients");

            entity.HasIndex(e => new { e.RestaurantId, e.IngredientName }, "uq_ingredients_restaurantid_ingredientname").IsUnique();

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(150)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.MinimumStockLevel)
                .HasPrecision(10, 2)
                .HasColumnName("minimum_stock_level");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ingredients_restaurants");

            entity.HasOne(d => d.Unit).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ingredients_units");
        });

        modelBuilder.Entity<Menucategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("menucategories_pkey");

            entity.ToTable("menucategories");

            entity.HasIndex(e => new { e.RestaurantId, e.CategoryName }, "uq_menucategories_restaurantid_categoryname").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0)
                .HasColumnName("display_order");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Menucategories)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menucategories_restaurants");
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.MenuItemId).HasName("menuitems_pkey");

            entity.ToTable("menuitems");

            entity.HasIndex(e => new { e.RestaurantId, e.ItemName }, "uq_menuitems_restaurantid_itemname").IsUnique();

            entity.Property(e => e.MenuItemId).HasColumnName("menu_item_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ItemName)
                .HasMaxLength(150)
                .HasColumnName("item_name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Category).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menuitems_menucategories");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menuitems_restaurants");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => new { e.RestaurantId, e.OrderNumber }, "uq_orders_restaurantid_ordernumber").IsUnique();

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ClosedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("closed_at");
            entity.Property(e => e.ClosedBy).HasColumnName("closed_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.OpenedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("opened_at");
            entity.Property(e => e.OpenedBy).HasColumnName("opened_by");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .HasColumnName("order_number");
            entity.Property(e => e.OrderStatusId)
                .HasDefaultValue(1)
                .HasColumnName("order_status_id");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ClosedByNavigation).WithMany(p => p.OrderClosedByNavigations)
                .HasForeignKey(d => d.ClosedBy)
                .HasConstraintName("fk_orders_closedby_users");

            entity.HasOne(d => d.OpenedByNavigation).WithMany(p => p.OrderOpenedByNavigations)
                .HasForeignKey(d => d.OpenedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_openedby_users");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_orderstatuses");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_restaurants");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_restauranttables");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("orderitems_pkey");

            entity.ToTable("orderitems");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.CancellationReason)
                .HasMaxLength(500)
                .HasColumnName("cancellation_reason");
            entity.Property(e => e.CancelledAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelled_at");
            entity.Property(e => e.CancelledBy).HasColumnName("cancelled_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ItemName)
                .HasMaxLength(150)
                .HasColumnName("item_name");
            entity.Property(e => e.MenuItemId).HasColumnName("menu_item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderItemStatusId)
                .HasDefaultValue(1)
                .HasColumnName("order_item_status_id");
            entity.Property(e => e.PreparingAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("preparing_at");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReadyAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ready_at");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.SentToKitchenAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sent_to_kitchen_at");
            entity.Property(e => e.SpecialInstructions)
                .HasMaxLength(500)
                .HasColumnName("special_instructions");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.CancelledBy)
                .HasConstraintName("fk_orderitems_cancelledby_users");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_menuitems");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_orders");

            entity.HasOne(d => d.OrderItemStatus).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderItemStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_orderitemstatuses");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_restaurants");
        });

        modelBuilder.Entity<Orderitemstatus>(entity =>
        {
            entity.HasKey(e => e.OrderItemStatusId).HasName("orderitemstatuses_pkey");

            entity.ToTable("orderitemstatuses");

            entity.HasIndex(e => e.StatusName, "orderitemstatuses_status_name_key").IsUnique();

            entity.Property(e => e.OrderItemStatusId).HasColumnName("order_item_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("orderstatuses_pkey");

            entity.ToTable("orderstatuses");

            entity.HasIndex(e => e.StatusName, "orderstatuses_status_name_key").IsUnique();

            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.PaidAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paid_at");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentStatusId).HasColumnName("payment_status_id");
            entity.Property(e => e.ReceivedBy).HasColumnName("received_by");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Bill).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_bills");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_paymentmethods");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_paymentstatuses");

            entity.HasOne(d => d.ReceivedByNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReceivedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_receivedby_users");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_restaurants");
        });

        modelBuilder.Entity<Paymentmethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("paymentmethods_pkey");

            entity.ToTable("paymentmethods");

            entity.HasIndex(e => e.MethodName, "paymentmethods_method_name_key").IsUnique();

            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.MethodName)
                .HasMaxLength(50)
                .HasColumnName("method_name");
        });

        modelBuilder.Entity<Paymentstatus>(entity =>
        {
            entity.HasKey(e => e.PaymentStatusId).HasName("paymentstatuses_pkey");

            entity.ToTable("paymentstatuses");

            entity.HasIndex(e => e.StatusName, "paymentstatuses_status_name_key").IsUnique();

            entity.Property(e => e.PaymentStatusId).HasColumnName("payment_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.PermissionName, "permissions_permission_name_key").IsUnique();

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(150)
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("restaurants_pkey");

            entity.ToTable("restaurants");

            entity.HasIndex(e => e.Email, "uq_restaurants_email").IsUnique();

            entity.HasIndex(e => e.MobileNumber, "uq_restaurants_mobilenumber").IsUnique();

            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .HasColumnName("mobile_number");
            entity.Property(e => e.RestaurantName)
                .HasMaxLength(150)
                .HasColumnName("restaurant_name");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Restauranttable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("restauranttables_pkey");

            entity.ToTable("restauranttables");

            entity.HasIndex(e => new { e.RestaurantId, e.TableName }, "uq_restauranttables_restaurantid_tablename").IsUnique();

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.SeatingCapacity).HasColumnName("seating_capacity");
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .HasColumnName("table_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Restauranttables)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_restauranttables_restaurants");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Rolepermission>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId).HasName("rolepermissions_pkey");

            entity.ToTable("rolepermissions");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "uq_rolepermissions_roleid_permissionid").IsUnique();

            entity.Property(e => e.RolePermissionId).HasColumnName("role_permission_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.Rolepermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rolepermissions_permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolepermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rolepermissions_roles");
        });

        modelBuilder.Entity<Tablestatus>(entity =>
        {
            entity.HasKey(e => e.TableStatusId).HasName("tablestatuses_pkey");

            entity.ToTable("tablestatuses");

            entity.HasIndex(e => e.StatusName, "tablestatuses_status_name_key").IsUnique();

            entity.Property(e => e.TableStatusId).HasColumnName("table_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("units_pkey");

            entity.ToTable("units");

            entity.HasIndex(e => e.UnitName, "units_unit_name_key").IsUnique();

            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .HasColumnName("unit_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.RestaurantId, e.Email }, "uq_users_restaurantid_email").IsUnique();

            entity.HasIndex(e => new { e.RestaurantId, e.MobileNumber }, "uq_users_restaurantid_mobilenumber").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .HasColumnName("mobile_number");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .HasColumnName("password_hash");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Users)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_restaurants");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
