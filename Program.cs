using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using BrgyLink.Services;
using BrgyLink.Hubs;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<PdfService>();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

// Register the EmailSender implementation
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add custom authorization policy
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});


builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

await SeedDatabaseAsync(app);

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map Razor Pages and SignalR Hub
app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub"); // Ensure this matches the correct class name

app.Run();

async Task SeedDatabaseAsync(WebApplication app)
{
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

	// Seed roles
	string[] roleNames = { "Admin", "Barangay Official", "User" };
	foreach (var roleName in roleNames)
	{
		if (await roleManager.FindByNameAsync(roleName) == null)
		{
			await roleManager.CreateAsync(new IdentityRole(roleName));
		}
	}

	// Seed an admin user (optional)
	var adminEmail = "admin@example.com";
	var adminPassword = "Admin123!";
	var adminUser = await userManager.FindByEmailAsync(adminEmail);
	if (adminUser == null)
	{
		adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
		await userManager.CreateAsync(adminUser, adminPassword);
		await userManager.AddToRoleAsync(adminUser, "Admin");
	}
}
