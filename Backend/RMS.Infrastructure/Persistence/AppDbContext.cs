using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RMS.Infrastructure.Persistence.Models;

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
            entity.HasKey(e => e.Billid).HasName("bills_pkey");

            entity.ToTable("bills");

            entity.HasIndex(e => e.Orderid, "uq_bills_orderid").IsUnique();

            entity.HasIndex(e => new { e.Restaurantid, e.Billnumber }, "uq_bills_restaurantid_billnumber").IsUnique();

            entity.Property(e => e.Billid).HasColumnName("billid");
            entity.Property(e => e.Billnumber)
                .HasMaxLength(50)
                .HasColumnName("billnumber");
            entity.Property(e => e.Billstatusid).HasColumnName("billstatusid");
            entity.Property(e => e.Cancellationreason)
                .HasMaxLength(500)
                .HasColumnName("cancellationreason");
            entity.Property(e => e.Cancelledat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelledat");
            entity.Property(e => e.Cancelledby).HasColumnName("cancelledby");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Discountamount)
                .HasPrecision(10, 2)
                .HasColumnName("discountamount");
            entity.Property(e => e.Grandtotal)
                .HasPrecision(10, 2)
                .HasColumnName("grandtotal");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Servicechargeamount)
                .HasPrecision(10, 2)
                .HasColumnName("servicechargeamount");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Taxamount)
                .HasPrecision(10, 2)
                .HasColumnName("taxamount");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Billstatus).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Billstatusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bills_billstatuses");

            entity.HasOne(d => d.CancelledbyNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Cancelledby)
                .HasConstraintName("fk_bills_cancelledby_users");

            entity.HasOne(d => d.Order).WithOne(p => p.Bill)
                .HasForeignKey<Bill>(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bills_orders");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bills_restaurants");
        });

        modelBuilder.Entity<Billstatus>(entity =>
        {
            entity.HasKey(e => e.Billstatusid).HasName("billstatuses_pkey");

            entity.ToTable("billstatuses");

            entity.HasIndex(e => e.Statusname, "billstatuses_statusname_key").IsUnique();

            entity.Property(e => e.Billstatusid).HasColumnName("billstatusid");
            entity.Property(e => e.Statusname)
                .HasMaxLength(50)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Chargesetting>(entity =>
        {
            entity.HasKey(e => e.Chargesettingid).HasName("chargesettings_pkey");

            entity.ToTable("chargesettings");

            entity.HasIndex(e => new { e.Restaurantid, e.Chargename }, "uq_chargesettings_restaurantid_chargename").IsUnique();

            entity.Property(e => e.Chargesettingid).HasColumnName("chargesettingid");
            entity.Property(e => e.Chargename)
                .HasMaxLength(100)
                .HasColumnName("chargename");
            entity.Property(e => e.Chargevalue)
                .HasPrecision(10, 2)
                .HasColumnName("chargevalue");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Ispercentage)
                .HasDefaultValue(true)
                .HasColumnName("ispercentage");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Chargesettings)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_chargesettings_restaurants");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Ingredientid).HasName("ingredients_pkey");

            entity.ToTable("ingredients");

            entity.HasIndex(e => new { e.Restaurantid, e.Ingredientname }, "uq_ingredients_restaurantid_ingredientname").IsUnique();

            entity.Property(e => e.Ingredientid).HasColumnName("ingredientid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Ingredientname)
                .HasMaxLength(150)
                .HasColumnName("ingredientname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Minimumstocklevel)
                .HasPrecision(10, 2)
                .HasColumnName("minimumstocklevel");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Unitid).HasColumnName("unitid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ingredients_restaurants");

            entity.HasOne(d => d.Unit).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.Unitid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ingredients_units");
        });

        modelBuilder.Entity<Menucategory>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("menucategories_pkey");

            entity.ToTable("menucategories");

            entity.HasIndex(e => new { e.Restaurantid, e.Categoryname }, "uq_menucategories_restaurantid_categoryname").IsUnique();

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(100)
                .HasColumnName("categoryname");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Deletedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Displayorder)
                .HasDefaultValue(0)
                .HasColumnName("displayorder");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Menucategories)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menucategories_restaurants");
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.Menuitemid).HasName("menuitems_pkey");

            entity.ToTable("menuitems");

            entity.HasIndex(e => new { e.Restaurantid, e.Itemname }, "uq_menuitems_restaurantid_itemname").IsUnique();

            entity.Property(e => e.Menuitemid).HasColumnName("menuitemid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(500)
                .HasColumnName("imageurl");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isavailable)
                .HasDefaultValue(true)
                .HasColumnName("isavailable");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Itemname)
                .HasMaxLength(150)
                .HasColumnName("itemname");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Category).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menuitems_menucategories");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menuitems_restaurants");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => new { e.Restaurantid, e.Ordernumber }, "uq_orders_restaurantid_ordernumber").IsUnique();

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Closedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("closedat");
            entity.Property(e => e.Closedby).HasColumnName("closedby");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Openedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("openedat");
            entity.Property(e => e.Openedby).HasColumnName("openedby");
            entity.Property(e => e.Ordernumber)
                .HasMaxLength(50)
                .HasColumnName("ordernumber");
            entity.Property(e => e.Orderstatusid)
                .HasDefaultValue(1)
                .HasColumnName("orderstatusid");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Tableid).HasColumnName("tableid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.ClosedbyNavigation).WithMany(p => p.OrderClosedbyNavigations)
                .HasForeignKey(d => d.Closedby)
                .HasConstraintName("fk_orders_closedby_users");

            entity.HasOne(d => d.OpenedbyNavigation).WithMany(p => p.OrderOpenedbyNavigations)
                .HasForeignKey(d => d.Openedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_openedby_users");

            entity.HasOne(d => d.Orderstatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Orderstatusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_orderstatuses");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_restaurants");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Tableid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_restauranttables");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.Orderitemid).HasName("orderitems_pkey");

            entity.ToTable("orderitems");

            entity.Property(e => e.Orderitemid).HasColumnName("orderitemid");
            entity.Property(e => e.Cancellationreason)
                .HasMaxLength(500)
                .HasColumnName("cancellationreason");
            entity.Property(e => e.Cancelledat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelledat");
            entity.Property(e => e.Cancelledby).HasColumnName("cancelledby");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Itemname)
                .HasMaxLength(150)
                .HasColumnName("itemname");
            entity.Property(e => e.Menuitemid).HasColumnName("menuitemid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Orderitemstatusid)
                .HasDefaultValue(1)
                .HasColumnName("orderitemstatusid");
            entity.Property(e => e.Preparingat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("preparingat");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Readyat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("readyat");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Senttokitchenat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("senttokitchenat");
            entity.Property(e => e.Specialinstructions)
                .HasMaxLength(500)
                .HasColumnName("specialinstructions");
            entity.Property(e => e.Unitprice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CancelledbyNavigation).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Cancelledby)
                .HasConstraintName("fk_orderitems_cancelledby_users");

            entity.HasOne(d => d.Menuitem).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Menuitemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_menuitems");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_orders");

            entity.HasOne(d => d.Orderitemstatus).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Orderitemstatusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_orderitemstatuses");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderitems_restaurants");
        });

        modelBuilder.Entity<Orderitemstatus>(entity =>
        {
            entity.HasKey(e => e.Orderitemstatusid).HasName("orderitemstatuses_pkey");

            entity.ToTable("orderitemstatuses");

            entity.HasIndex(e => e.Statusname, "orderitemstatuses_statusname_key").IsUnique();

            entity.Property(e => e.Orderitemstatusid).HasColumnName("orderitemstatusid");
            entity.Property(e => e.Statusname)
                .HasMaxLength(50)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.HasKey(e => e.Orderstatusid).HasName("orderstatuses_pkey");

            entity.ToTable("orderstatuses");

            entity.HasIndex(e => e.Statusname, "orderstatuses_statusname_key").IsUnique();

            entity.Property(e => e.Orderstatusid).HasColumnName("orderstatusid");
            entity.Property(e => e.Statusname)
                .HasMaxLength(50)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Paymentid).HasColumnName("paymentid");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Billid).HasColumnName("billid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.Paidat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paidat");
            entity.Property(e => e.Paymentmethodid).HasColumnName("paymentmethodid");
            entity.Property(e => e.Paymentstatusid).HasColumnName("paymentstatusid");
            entity.Property(e => e.Receivedby).HasColumnName("receivedby");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Bill).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Billid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_bills");

            entity.HasOne(d => d.Paymentmethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Paymentmethodid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_paymentmethods");

            entity.HasOne(d => d.Paymentstatus).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Paymentstatusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_paymentstatuses");

            entity.HasOne(d => d.ReceivedbyNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Receivedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_receivedby_users");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payments_restaurants");
        });

        modelBuilder.Entity<Paymentmethod>(entity =>
        {
            entity.HasKey(e => e.Paymentmethodid).HasName("paymentmethods_pkey");

            entity.ToTable("paymentmethods");

            entity.HasIndex(e => e.Methodname, "paymentmethods_methodname_key").IsUnique();

            entity.Property(e => e.Paymentmethodid).HasColumnName("paymentmethodid");
            entity.Property(e => e.Methodname)
                .HasMaxLength(50)
                .HasColumnName("methodname");
        });

        modelBuilder.Entity<Paymentstatus>(entity =>
        {
            entity.HasKey(e => e.Paymentstatusid).HasName("paymentstatuses_pkey");

            entity.ToTable("paymentstatuses");

            entity.HasIndex(e => e.Statusname, "paymentstatuses_statusname_key").IsUnique();

            entity.Property(e => e.Paymentstatusid).HasColumnName("paymentstatusid");
            entity.Property(e => e.Statusname)
                .HasMaxLength(50)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Permissionname, "permissions_permissionname_key").IsUnique();

            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Permissionname)
                .HasMaxLength(150)
                .HasColumnName("permissionname");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Restaurantid).HasName("restaurants_pkey");

            entity.ToTable("restaurants");

            entity.HasIndex(e => e.Email, "uq_restaurants_email").IsUnique();

            entity.HasIndex(e => e.Mobilenumber, "uq_restaurants_mobilenumber").IsUnique();

            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Mobilenumber)
                .HasMaxLength(15)
                .HasColumnName("mobilenumber");
            entity.Property(e => e.Restaurantname)
                .HasMaxLength(150)
                .HasColumnName("restaurantname");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
        });

        modelBuilder.Entity<Restauranttable>(entity =>
        {
            entity.HasKey(e => e.Tableid).HasName("restauranttables_pkey");

            entity.ToTable("restauranttables");

            entity.HasIndex(e => new { e.Restaurantid, e.Tablename }, "uq_restauranttables_restaurantid_tablename").IsUnique();

            entity.Property(e => e.Tableid).HasColumnName("tableid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Seatingcapacity).HasColumnName("seatingcapacity");
            entity.Property(e => e.Tablename)
                .HasMaxLength(50)
                .HasColumnName("tablename");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Restauranttables)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_restauranttables_restaurants");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Rolename, "roles_rolename_key").IsUnique();

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Rolepermission>(entity =>
        {
            entity.HasKey(e => e.Rolepermissionid).HasName("rolepermissions_pkey");

            entity.ToTable("rolepermissions");

            entity.HasIndex(e => new { e.Roleid, e.Permissionid }, "uq_rolepermissions_roleid_permissionid").IsUnique();

            entity.Property(e => e.Rolepermissionid).HasColumnName("rolepermissionid");
            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Roleid).HasColumnName("roleid");

            entity.HasOne(d => d.Permission).WithMany(p => p.Rolepermissions)
                .HasForeignKey(d => d.Permissionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rolepermissions_permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolepermissions)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rolepermissions_roles");
        });

        modelBuilder.Entity<Tablestatus>(entity =>
        {
            entity.HasKey(e => e.Tablestatusid).HasName("tablestatuses_pkey");

            entity.ToTable("tablestatuses");

            entity.HasIndex(e => e.Statusname, "tablestatuses_statusname_key").IsUnique();

            entity.Property(e => e.Tablestatusid).HasColumnName("tablestatusid");
            entity.Property(e => e.Statusname)
                .HasMaxLength(50)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Unitid).HasName("units_pkey");

            entity.ToTable("units");

            entity.HasIndex(e => e.Unitname, "units_unitname_key").IsUnique();

            entity.Property(e => e.Unitid).HasColumnName("unitid");
            entity.Property(e => e.Unitname)
                .HasMaxLength(50)
                .HasColumnName("unitname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.Restaurantid, e.Email }, "uq_users_restaurantid_email").IsUnique();

            entity.HasIndex(e => new { e.Restaurantid, e.Mobilenumber }, "uq_users_restaurantid_mobilenumber").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Mobilenumber)
                .HasMaxLength(15)
                .HasColumnName("mobilenumber");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(500)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Users)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_restaurants");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
